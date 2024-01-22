
using Gopet.Data.Collections;

public class Kiosk {

    private sbyte kioskType;

    public CopyOnWriteArrayList<SellItem> kioskItems = new  ();

    public Kiosk(sbyte kioskType_) {
        this.kioskType = kioskType_;
    }

    public void addKioskItem(Item item, int price, Player player)   {
        if (item != null) {
            if (!item.wasSell) {
                item.wasSell = true;
            }
            addKioskItem(new SellItem(item, price, GopetManager.HOUR_UPLOAD_ITEM), player);
            HistoryManager.addHistory(new History(player).setLog(String.format("Treo vật phẩm %s với giá %s ngọc", item.getTemp().getName(), Utilities.formatNumber(price))).setObj(item));
        } else {
            throw new NullPointerException("item is null");
        }
    }

    public void addKioskItem(Pet pet, int price, Player player)   {
        if (kioskType == GopetManager.KIOSK_PET) {
            if (!pet.wasSell) {
                pet.wasSell = true;
            }
            addKioskItem(new SellItem(price, pet, GopetManager.HOUR_UPLOAD_ITEM), player);
            HistoryManager.addHistory(new History(player).setLog(String.format("Treo pet %s với giá %s ngọc", pet.getPetTemplate().getName(), Utilities.formatNumber(price))).setObj(pet));
            return;
        }
    }

    private void addKioskItem(SellItem item, Player player) {
        item.user_id = player.user.user_id;
        kioskItems.add(item);
        while (true) {
            item.itemId = Utilities.nextInt(1, int.MAX_VALUE - 2);
            bool flag = true;
            for (SellItem item1 : kioskItems) {
                if (item1 != item) {
                    if (item1.itemId == item.itemId) {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag) {
                break;
            }
        }
        kioskItems.sort(new Comparator<SellItem>() {
            @Override
            public int compare(SellItem obj1, SellItem obj2) {
                return obj1.itemId - obj2.itemId;
            }
        });
    }

    public SellItem searchItem(int itemId)   {
        int left = 0;
        int right = kioskItems.size() - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            SellItem midItem = kioskItems.get(mid);
            if (midItem.itemId == itemId) {
                return midItem;
            }
            if (midItem.itemId < itemId) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        return null;
    }

    public void buy(int itemId, Player player)   {
          SellItem sellItem = searchItem(itemId);
        if (sellItem != null) {
            if (sellItem.user_id == player.user.user_id) {
                player.redDialog("Bạn không thể mua chính vật phẩm mà bạn bán");
            } else {
                player.controller.objectPerformed.put(MenuController.OBJKEY_KIOSK_ITEM, new Map.Entry< Kiosk, SellItem>() {
                    @Override
                    public Kiosk getKey() {
                        return Kiosk.this;
                    }

                    @Override
                    public SellItem getValue() {
                        return sellItem;
                    }

                    @Override
                    public SellItem setValue(SellItem value) {
                        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
                    }
                });
                MenuController.showYNDialog(MenuController.DIALOG_CONFIRM_BUY_KIOSK_ITEM, String.format("Bạn có chắc là muốn mua %s không?", sellItem.getName()), player);
            }
        } else {
            player.redDialog("Vật phẩm đã bị người khác mua rồi");
        }
    }

    public void confirmBuy(Player player, SellItem sellItem)   {
        if (!this.kioskItems.contains(sellItem)) {
            player.redDialog("Vật phẩm đã bị người khác mua!");
            return;
        }
        if (player.checkCoin(sellItem.price)) {
            if (!sellItem.hasSell) {
                player.addCoin(-sellItem.price);
                sellItem.setHasSell(true);
                if (sellItem.ItemSell != null) {
                    player.addItemToInventory(sellItem.ItemSell);
                } else {
                    player.playerData.addPet(sellItem.pet, player);
                }
                player.okDialog("Mua thành công");
                kioskItems.remove(sellItem);
                Player sellPlayer = PlayerManager.get(sellItem.user_id);
                long priceReiceived = Math.round(Utilities.getValueFromPercent(sellItem.price, 100f - GopetManager.KIOSK_PER_SELL));
                if (sellPlayer != null) {
                    sellPlayer.addCoin(priceReiceived);
                    sellPlayer.playerData.save();
                    HistoryManager.addHistory(new History(sellItem.user_id).setObj(sellItem).setLog("Bán thành công vật phẩm trong ki ốt người mua là " + player.playerData.name));
                } else {
                    Connection connection = MYSQLManager.create();
                    try {
                        MYSQLManager.updateSql(String.format("Update `player` set coin = coin + %s where user_id =%s", priceReiceived, sellItem.user_id), connection);
                        HistoryManager.addHistory(new History(sellItem.user_id).setObj(sellItem).setLog("Bán thành công vật phẩm trong ki ốt người mua là " + player.playerData.name));
                    } catch (Exception e) {
                        e.printStackTrace();
                        System.err.println("Error them tien khi ng choi mua vp , tien them là " + sellItem.price + " vào user_id = " + sellItem.user_id);
                    }
                    connection.close();
                }
            } else {
                player.redDialog("Vật phẩm đã này dược người khác mua rồi");
            }
        } else {
            player.controller.notEnoughCoin();
        }
    }

    public void setKioskItem(CopyOnWriteArrayList<SellItem> sellItem) {
        this.kioskItems = sellItem;
    }

    public SellItem getItemByUserId(int user_id) {
        for (SellItem kioskItem : kioskItems) {
            if (kioskItem.user_id == user_id) {
                return kioskItem;
            }
        }
        return null;
    }

    public sbyte getKioskType() {
        return kioskType;
    }

    public void update()   {
        for (SellItem kioskItem : kioskItems) {
            if (kioskItem.expireTime < System.currentTimeMillis()) {
                kioskItems.remove(kioskItem);
                Player player = PlayerManager.get(kioskItem.user_id);
                if (player != null) {
                    if (kioskItem.pet == null) {
                        player.addItemToInventory(kioskItem.ItemSell);
                    } else {
                        player.playerData.addPet(kioskItem.pet, player);
                    }
                    HistoryManager.addHistory(new History(kioskItem.user_id).setObj(kioskItem).setLog("Lưu vật phẩm ki ốt vào cơ sở dữ liệu thành công"));
                    continue;
                }
                Connection connection = MYSQLManager.create();
                try {
                    MYSQLManager.updateSql(String.format("INSERT INTO `kiosk_recovery`(`kioskType`, `user_id`, `item`) VALUES ('%s','%s','%s')", kioskType, kioskItem.user_id, JsonManager.ToJson(kioskItem)), connection);
                    HistoryManager.addHistory(new History(kioskItem.user_id).setObj(kioskItem).setLog("Lưu vật phẩm ki ốt vào cơ sở dữ liệu thành công"));
                } catch (Exception e) {
                    e.printStackTrace();
                    HistoryManager.addHistory(new History(kioskItem.user_id).setObj(kioskItem).setLog("Trao trả vật phẩm ki ốt thất bại"));
                }
                connection.close();
            }
        }
    }
}

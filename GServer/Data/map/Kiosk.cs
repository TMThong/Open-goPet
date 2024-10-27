using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Util;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Gopet.Data.Map
{
    public class Kiosk
    {

        public sbyte kioskType { get; set; }

        public CopyOnWriteArrayList<SellItem> kioskItems = new();

        public Kiosk(sbyte kioskType_)
        {
            kioskType = kioskType_;
        }

        public void addKioskItem(Item item, int price, Player player)
        {
            if (item != null)
            {
                if (!item.wasSell)
                {
                    item.wasSell = true;
                }
                addKioskItem(new SellItem(item, price, GopetManager.HOUR_UPLOAD_ITEM), player);
                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Treo vật phẩm %s với giá %s ngoc", item.getTemp().getName(player), Utilities.FormatNumber(price))).setObj(item));
            }
            else
            {
                throw new NullReferenceException("item is null");
            }
        }

        public void addKioskItem(Pet pet, int price, Player player)
        {
            if (kioskType == GopetManager.KIOSK_PET)
            {
                if (!pet.wasSell)
                {
                    pet.wasSell = true;
                }
                addKioskItem(new SellItem(price, pet, GopetManager.HOUR_UPLOAD_ITEM), player);
                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Treo pet %s với giá %s ngoc", pet.getPetTemplate().getName(player), Utilities.FormatNumber(price))).setObj(pet));
                return;
            }
        }

        sealed class SellItemComparer : IComparer<SellItem>
        {
            public int Compare(SellItem? obj1, SellItem? obj2)
            {
                return obj1.itemId - obj2.itemId;
            }
        }

        private void addKioskItem(SellItem item, Player player)
        {
            item.user_id = player.user.user_id;
            kioskItems.Add(item);
            while (true)
            {
                item.itemId = Utilities.nextInt(1, int.MaxValue - 2);
                bool flag = true;
                foreach (SellItem item1 in kioskItems)
                {
                    if (item1 != item)
                    {
                        if (item1.itemId == item.itemId)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            kioskItems.Sort(new SellItemComparer());
        }

        public SellItem searchItem(int itemId)
        {
            int left = 0;
            int right = kioskItems.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                SellItem midItem = kioskItems.get(mid);
                if (midItem.itemId == itemId)
                {
                    return midItem;
                }
                if (midItem.itemId < itemId)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return null;
        }

        public void buy(int itemId, Player player)
        {
            if (Maintenance.gI().isIsMaintenance())
            {
                player.redDialog(player.Language.CannotBuyThisItemByMaintenance);
                return;
            }

            SellItem sellItem = searchItem(itemId);
            if (sellItem != null)
            {
                if (sellItem.user_id == player.user.user_id)
                {
                    player.redDialog(player.Language.CannotBuyThisItemOfYourself);
                }
                else
                {
                    player.controller.objectPerformed.put(MenuController.OBJKEY_KIOSK_ITEM, new KeyValuePair<Kiosk, SellItem>(this, sellItem));
                    MenuController.showYNDialog(MenuController.DIALOG_CONFIRM_BUY_KIOSK_ITEM, player.Language.DoYouWantBuyIt, player);
                }
            }
            else
            {
                player.redDialog(player.Language.ItemWasSell);
            }
        }

        public void confirmBuy(Player player, SellItem sellItem)
        {
            if(Maintenance.gI().isIsMaintenance())
            {
                player.redDialog(player.Language.CannotBuyThisItemByMaintenance);
                return;
            }
            if (!kioskItems.Contains(sellItem))
            {
                player.redDialog(player.Language.ItemWasSell);
                return;
            }
            if (player.checkCoin(sellItem.price))
            {
                if (!sellItem.hasSell)
                {
                    player.addCoin(-sellItem.price);
                    sellItem.setHasSell(true);
                    if (sellItem.ItemSell != null)
                    {
                        player.addItemToInventory(sellItem.ItemSell);
                    }
                    else
                    {
                        player.playerData.addPet(sellItem.pet, player);
                    }
                    player.okDialog(player.Language.BuyOK);
                    kioskItems.remove(sellItem);
                    Player sellPlayer = PlayerManager.get(sellItem.user_id);
                    long priceReiceived = Utilities.round(Utilities.GetValueFromPercent(sellItem.price, 100f - GopetManager.KIOSK_PER_SELL));
                    if (sellPlayer != null)
                    {
                        sellPlayer.addCoin(priceReiceived);
                        sellPlayer.playerData.save();
                        HistoryManager.addHistory(new History(sellItem.user_id).setObj(sellItem).setLog("Bán thành công vật phẩm trong ki ốt người mua là " + player.playerData.name));
                        
                    }
                    else
                    {
                        using(var conn = MYSQLManager.create())
                        {
                            conn.Execute("Update `player` set coin = coin + @priceReiceived where user_id =@user_id",
                                new { priceReiceived = priceReiceived,  user_id = sellItem.user_id });
                            HistoryManager.addHistory(new History(sellItem.user_id).setObj(sellItem).setLog("Bán thành công vật phẩm trong ki ốt người mua là " + player.playerData.name));
                        }
                    }
                    HistoryManager.addHistory(new History(player.playerData.user_id).setObj(sellItem).setLog($"Mua thành công {sellItem.getName(player)}"));
                }
                else
                {
                    player.redDialog(player.Language.ItemWasSell);
                }
            }
            else
            {
                player.controller.notEnoughCoin();
            }
        }

        public void setKioskItem(CopyOnWriteArrayList<SellItem> sellItem)
        {
            kioskItems = sellItem;
        }

        public SellItem getItemByUserId(int user_id)
        {
            foreach (SellItem kioskItem in kioskItems)
            {
                if (kioskItem.user_id == user_id)
                {
                    return kioskItem;
                }
            }
            return null;
        }

        public sbyte getKioskType()
        {
            return kioskType;
        }

        public void update()
        {
            foreach (SellItem kioskItem in kioskItems)
            {
                if (kioskItem.expireTime < Utilities.CurrentTimeMillis)
                {
                    kioskItems.remove(kioskItem);
                    try
                    {
                        Player player = PlayerManager.get(kioskItem.user_id);
                        if (player != null)
                        {
                            if (kioskItem.pet == null)
                            {
                                player.addItemToInventory(kioskItem.ItemSell);
                            }
                            else
                            {
                                player.playerData.addPet(kioskItem.pet, player);
                            }
                            HistoryManager.addHistory(new History(kioskItem.user_id).setObj(kioskItem).setLog("Lưu vật phẩm ki ốt vào cơ sở dữ liệu thành công"));
                            continue;
                        }
                        using (var conn = MYSQLManager.create())
                        {
                            conn.Execute("INSERT INTO `kiosk_recovery`(`kioskType`, `user_id`, `item`) VALUES (@kioskType,@user_id,@jsonData)",
                                new { kioskType = kioskType, user_id = kioskItem.user_id, jsonData = JsonConvert.SerializeObject(kioskItem) });
                            HistoryManager.addHistory(new History(kioskItem.user_id).setObj(kioskItem).setLog("Lưu vật phẩm ki ốt vào cơ sở dữ liệu thành công"));
                        }
                    }
                    catch (Exception e)
                    {
                        e.printStackTrace();
                    }
                }
            }
        }
    }
}
using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Util;
using MySqlConnector;
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

        public void addKioskItem(Item item, int price, Player player, string assignedName = null)
        {
            if (item != null)
            {
                if (!item.wasSell)
                {
                    item.wasSell = true;
                }
                addKioskItem(new SellItem(item, price, GopetManager.HOUR_UPLOAD_ITEM)
                {
                    AssignedName = assignedName
                }, player);
                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Treo vật phẩm %s với giá %s ngoc", item.getTemp().getName(player), Utilities.FormatNumber(price))).setObj(item));
            }
            else
            {
                throw new NullReferenceException("item is null");
            }
        }

        public void addKioskItem(Pet pet, int price, Player player, string assignedName = null)
        {
            if (kioskType == GopetManager.KIOSK_PET)
            {
                if (!pet.wasSell)
                {
                    pet.wasSell = true;
                }
                addKioskItem(new SellItem(price, pet, GopetManager.HOUR_UPLOAD_ITEM)
                {
                    AssignedName = assignedName
                }, player);
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
                if (sellItem.AssignedName != null && !string.IsNullOrEmpty(sellItem.AssignedName) && sellItem.AssignedName != player.playerData.name)
                {
                    player.redDialog("Vật phẩm này chỉ bán cho người chơi có tên: {0}", sellItem.AssignedName);
                    return;
                }
                if (sellItem.user_id == player.user.user_id)
                {
                    player.redDialog(player.Language.CannotBuyThisItemOfYourself);
                }
                else
                {
                    player.controller.objectPerformed.put(MenuController.OBJKEY_KIOSK_ITEM, new KeyValuePair<Kiosk, SellItem>(this, sellItem));
                    if (!sellItem.IsRetail)
                    {
                        MenuController.showYNDialog(MenuController.DIALOG_CONFIRM_BUY_KIOSK_ITEM, player.Language.DoYouWantBuyIt, player);
                    }
                    else
                    {
                        MenuController.sendMenu(MenuController.MENU_OPTION_BUY_KIOSK_ITEM, player);
                    }
                }
            }
            else
            {
                player.redDialog(player.Language.ItemWasSell);
            }
        }

        public void buyRetail(int itemId, Player player, int count)
        {
            if (count <= 0)
            {
                player.redDialog(player.Language.BugWarning);
                return;
            }
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
                if (sellItem.AssignedName != null && !string.IsNullOrEmpty(sellItem.AssignedName) && sellItem.AssignedName != player.playerData.name)
                {
                    player.redDialog("Vật phẩm này chỉ bán cho người chơi có tên: {0}", sellItem.AssignedName);
                    return;
                }
                if (sellItem.pet == null)
                {
                    if (sellItem.ItemSell.count == count)
                    {
                        confirmBuy(player, sellItem);
                        return;
                    }
                }
                try
                {
                    sellItem.sellItemMutex.WaitOne();
                    if (sellItem.pet == null)
                    {
                        if (count > sellItem.ItemSell.count)
                        {
                            player.redDialog(player.Language.NotEnoughItemToBuy);
                            return;
                        }
                        long priceRetail = Math.Max(1, sellItem.price) / sellItem.TotalCount;
                        long price = Math.Max(1, priceRetail * count);
                        if (player.checkCoin(price))
                        {
                            player.addCoin(-price);
                            sellItem.sumVal += price;
                            sellItem.ItemSell.count -= count;
                            player.addItemToInventory(new Item(sellItem.ItemSell.itemTemplateId, count));
                            player.okDialog(player.Language.BuyOK);
                            HistoryManager.addHistory(new History(player.playerData.user_id).setObj(sellItem).setLog($"Mua thành công {sellItem.getName(player)} có sớ lượng {count}"));
                        }
                        else
                        {
                            player.controller.notEnoughCoin();
                        }
                    }
                }
                finally
                {
                    sellItem.sellItemMutex.ReleaseMutex();
                }
            }
            else
            {
                player.redDialog(player.Language.ItemWasSell);
            }
        }


        public void confirmBuy(Player player, SellItem sellItem)
        {
            if (Maintenance.gI().isIsMaintenance())
            {
                player.redDialog(player.Language.CannotBuyThisItemByMaintenance);
                return;
            }
            if (!kioskItems.Contains(sellItem))
            {
                player.redDialog(player.Language.ItemWasSell);
                return;
            }
            if (sellItem.AssignedName != null && !string.IsNullOrEmpty(sellItem.AssignedName) && sellItem.AssignedName != player.playerData.name)
            {
                player.redDialog("Vật phẩm này chỉ bán cho người chơi có tên: {0}", sellItem.AssignedName);
                return;
            }
            if (player.checkCoin(sellItem.price) || (sellItem.sumVal > 0 && player.checkCoin(sellItem.price - sellItem.sumVal)))
            {
                try
                {
                    sellItem.sellItemMutex.WaitOne();
                    if (!sellItem.hasSell)
                    {
                        if (sellItem.sumVal > 0) player.addCoin(-(sellItem.price - sellItem.sumVal));
                        else player.addCoin(-sellItem.price);
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
                            using (var conn = MYSQLManager.create())
                            {
                                conn.Execute("Update `player` set coin = coin + @priceReiceived where user_id =@user_id",
                                    new { priceReiceived = priceReiceived, user_id = sellItem.user_id });
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
                finally
                {
                    sellItem.sellItemMutex.ReleaseMutex();
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
                            if (kioskItem.sumVal > 0)
                            {
                                Utilities.round(Utilities.GetValueFromPercent(kioskItem.sumVal, 100f - GopetManager.KIOSK_PER_SELL));
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
using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.Dialog;
using Gopet.Data.GopetClan;
using Gopet.Data.GopetItem;
using Gopet.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MenuController
{
    public static void inputDialog(int dialogInputId, InputReader reader, Player player)
    {
        try
        {
            switch (dialogInputId)
            {
                case INPUT_DIALOG_KIOSK:
                    int priceItem = reader.readInt(0);
                    if (priceItem <= 0)
                    {
                        player.redDialog("Tính bug ha gì!");
                        return;
                    }
                    if (priceItem > 2000000000)
                    {
                        player.redDialog(Utilities.Format("Giới hạn (ngoc) là %s", Utilities.FormatNumber(2000000000)));
                        return;
                    }

                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_SELECT_SELL_ITEM) && player.controller.objectPerformed.ContainsKey(OBJKEY_MENU_OF_KIOSK))
                    {
                        int menuKioskId = (int)player.controller.objectPerformed.get(OBJKEY_MENU_OF_KIOSK);
                        Item item = null;
                        Pet pet = null;
                        if (menuKioskId != MENU_KIOSK_PET_SELECT)
                        {
                            item = (Item)player.controller.objectPerformed.get(OBJKEY_SELECT_SELL_ITEM);
                        }
                        else if (menuKioskId == MENU_KIOSK_PET_SELECT)
                        {
                            pet = (Pet)player.controller.objectPerformed.get(OBJKEY_SELECT_SELL_ITEM);
                        }

                        if (item == null && pet == null)
                        {
                            return;
                        }

                        if (item != null)
                        {
                            if (!item.getTemp().isCanTrade() || !item.canTrade)
                            {
                                player.redDialog("Vật phẩm này không thể giao dịch");
                                return;
                            }
                        }

                        player.controller.objectPerformed.Remove(OBJKEY_SELECT_SELL_ITEM);
                        player.controller.objectPerformed.Remove(OBJKEY_MENU_OF_KIOSK);
                        int count = 1;
                        if (player.controller.objectPerformed.ContainsKey(OBJKEY_COUNT_OF_ITEM_KIOSK))
                        {
                            count = (int)player.controller.objectPerformed.get(OBJKEY_COUNT_OF_ITEM_KIOSK);
                        }
                        MarketPlace marketPlace = (MarketPlace)player.getPlace();
                        switch (menuKioskId)
                        {
                            case MENU_KIOSK_PET_SELECT:
                                player.playerData.pets.remove(pet);
                                MarketPlace.getKiosk(GopetManager.KIOSK_PET).addKioskItem(pet, priceItem, player);
                                player.controller.showKiosk(GopetManager.KIOSK_PET);
                                break;
                            case MENU_KIOSK_HAT_SELECT:
                            case MENU_KIOSK_WEAPON_SELECT:
                            case MENU_KIOSK_AMOUR_SELECT:
                            case MENU_KIOSK_OHTER_SELECT:
                            case MENU_KIOSK_GEM_SELECT:
                                player.playerData.removeItem(menuKioskId != MENU_KIOSK_GEM_SELECT ? GopetManager.EQUIP_PET_INVENTORY : GopetManager.GEM_INVENTORY, item);
                                switch (menuKioskId)
                                {
                                    case MENU_KIOSK_HAT_SELECT:
                                        MarketPlace.getKiosk(GopetManager.KIOSK_HAT).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_HAT);
                                        break;
                                    case MENU_KIOSK_GEM_SELECT:
                                        MarketPlace.getKiosk(GopetManager.KIOSK_GEM).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_GEM);
                                        break;
                                    case MENU_KIOSK_WEAPON_SELECT:
                                        MarketPlace.getKiosk(GopetManager.KIOSK_WEAPON).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_WEAPON);
                                        break;
                                    case MENU_KIOSK_AMOUR_SELECT:
                                        MarketPlace.getKiosk(GopetManager.KIOSK_AMOUR).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_AMOUR);
                                        break;
                                    case MENU_KIOSK_PET_SELECT:
                                        MarketPlace.getKiosk(GopetManager.KIOSK_PET).addKioskItem(pet, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_PET);
                                        break;
                                    case MENU_KIOSK_OHTER_SELECT:
                                        if (GameController.checkCount(item, count))
                                        {
                                            Item itemCopy = new Item(item.itemTemplateId);
                                            itemCopy.count = count;
                                            MarketPlace.getKiosk(GopetManager.KIOSK_OTHER).addKioskItem(itemCopy, priceItem, player);
                                            player.controller.showKiosk(GopetManager.KIOSK_OTHER);
                                            player.controller.subCountItem(item, count, GopetManager.NORMAL_INVENTORY);
                                        }
                                        else
                                        {
                                            player.redDialog("Số lượng không đủ");
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                    break;
                case INPUT_TYPE_GIFT_CODE:
                    {
                        ClanMember clanMember = player.controller.getClan();
                        if (!player.controller.canTypeGiftCode())
                        {
                            player.redDialog("Thao tác quá nhanh vui lòng chờ tí!");
                            return;
                        }
                        String code = reader.readString(0);
                        if (code.Length != 0)
                        {
                            if (Utilities.CheckString(code, "^[a-z0-9A-Z]+$"))
                            {
                                player.okDialog("Vui lòng chờ");

                                using (MySqlConnection MySqlConnection = MYSQLManager.create())
                                {
                                    try
                                    {
                                        var keyL = MySqlConnection.QuerySingleOrDefault("SELECT GET_LOCK('gift_code_lock_@code', 10) as hasLock;", new { code = code });
                                        if (keyL != null)
                                        {
                                            bool hasLock = keyL.hasLock == 1;
                                            if (!hasLock)
                                            {
                                                player.redDialog("Quá nhiều người cố gắng dùng mã quà tặng này nên hệ thống quá tải!");
                                            }
                                            else
                                            {
                                                GiftCodeData giftCodeData = MySqlConnection.QuerySingleOrDefault<GiftCodeData>(Utilities.Format("SELECT * FROM `gift_code` WHERE `gift_code`.`code` = '%s';", code));
                                                if (giftCodeData != null)
                                                {
                                                    if (clanMember == null && giftCodeData.isClanCode)
                                                    {
                                                        player.controller.notClan();
                                                        goto EndGiftCode;
                                                    }
                                                    if (!giftCodeData.isClanCode)
                                                    {
                                                        if (giftCodeData.getUsersOfUseThis().Contains(player.user.user_id))
                                                        {
                                                            player.redDialog("Bạn đã sử dụng mã quà tặng này rồi");
                                                            goto EndGiftCode;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (giftCodeData.getUsersOfUseThis().Contains(clanMember.clan.clanId))
                                                        {
                                                            player.redDialog("Bang hội của bạn đã sử dụng mã quà tặng này rồi");
                                                            goto EndGiftCode;
                                                        }
                                                    }
                                                    if (giftCodeData.getCurUser() >= giftCodeData.getMaxUser())
                                                    {
                                                        player.redDialog("Số người dùng mã quà tặng này đã đạt giới hạn!");
                                                    }
                                                    else
                                                    {
                                                        if (giftCodeData.getExpire().GetTimeMillis() < Utilities.CurrentTimeMillis)
                                                        {
                                                            player.redDialog("Mã quà tặng đã hết hạn");
                                                        }
                                                        else
                                                        {
                                                            if(giftCodeData.isClanCode)
                                                                giftCodeData.getUsersOfUseThis().add(clanMember.clan.clanId);
                                                            else
                                                                giftCodeData.getUsersOfUseThis().add(player.user.user_id);
                                                            giftCodeData.currentUser++; ;
                                                            if (giftCodeData.getGift_data().Length <= 0)
                                                            {
                                                                player.redDialog("Mã quà tặng này chả tặng bạn được cái gì :)");
                                                            }
                                                            else
                                                            {
                                                                JArrayList<Popup> popups = player.controller.onReiceiveGift(giftCodeData.getGift_data());
                                                                JArrayList<String> textInfo = new();
                                                                foreach (Popup popup in popups)
                                                                {
                                                                    textInfo.add(popup.getText());
                                                                }
                                                                player.okDialog(Utilities.Format("Chức mừng bạn nhận được: %s", String.Join(",", textInfo)));
                                                            }
                                                            MySqlConnection.Execute("UPDATE `gift_code` SET `currentUser` = @currentUser , `usersOfUseThis` = @usersOfUseThis WHERE `id` =  @id;", giftCodeData);
                                                        }
                                                    }

                                                EndGiftCode:;
                                                }
                                                else
                                                {
                                                    player.redDialog("Không có mã quà tặng này");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog("Xảy ra sự cố mà ông trời cũng cả biết");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        e.printStackTrace();
                                    }
                                    finally
                                    {
                                        MySqlConnection.Execute("DO RELEASE_LOCK('gift_code_lock_@code');", new { code = code });
                                    }
                                }
                            }
                            else
                            {
                                player.redDialog("Có kí tự lạ");
                            }
                        }
                        else
                        {
                            player.redDialog("Vui lòng không bỏ trống");
                        }
                    }
                    break;
                case INPUT_DIALOG_CHALLENGE_INVITE:
                    int priceChallenge = reader.readInt(0);
                    if (priceChallenge <= 0)
                    {
                        player.redDialog("Tính bug ha gì!");
                        return;
                    }
                    if (priceChallenge > 100000)
                    {
                        player.redDialog(Utilities.Format("Giới hạn (ngoc) là %s", Utilities.FormatNumber(100000)));
                        return;
                    }

                    player.controller.sendChallenge((Player)player.controller.objectPerformed.get(OBJKEY_INVITE_CHALLENGE_PLAYER), priceChallenge);

                    break;
                case INPUT_DIALOG_COUNT_OF_KISOK_ITEM:
                    {
                        int count = reader.readInt(0);
                        if (count >= 1)
                        {
                            player.controller.objectPerformed.put(OBJKEY_COUNT_OF_ITEM_KIOSK, count);
                            player.controller.showInputDialog(INPUT_DIALOG_KIOSK, "Định giá", new String[] { "  " }, new sbyte[] { 0 });
                        }
                        else
                        {
                            player.redDialog("Số lượng không hợp lệ");
                        }
                    }
                    break;
                case INPUT_DIALOG_CAPTCHA:
                    GopetCaptcha captcha = player.playerData.captcha;
                    if (captcha != null)
                    {
                        if (captcha.key.Equals(reader.readString(0)))
                        {
                            player.playerData.captcha = null;
                            player.okDialog("Chính xác");
                        }
                        else
                        {
                            player.redDialog("Nhập mã sai");
                        }
                    }
                    break;
                case INPUT_DIALOG_ADMIN_GET_ITEM:
                    {
                        if (player.checkIsAdmin())
                        {
                            int itemTemplateId = reader.readInt(1);
                            int count = reader.readInt(0);
                            ItemTemplate itemTemplate = GopetManager.itemTemplate.get(itemTemplateId);
                            if (itemTemplate != null)
                            {
                                if (itemTemplate.isStackable)
                                {
                                    Item item = new Item(itemTemplateId);
                                    item.count = count;
                                    player.addItemToInventory(item);
                                    player.okDialog(item.getName());
                                }
                                else
                                {
                                    if (count < 1000)
                                    {
                                        for (int i = 0; i < count; i++)
                                        {
                                            Item item = new Item(itemTemplateId);
                                            item.count = 1;
                                            player.addItemToInventory(item);

                                        }
                                        player.okDialog(itemTemplate.getName() + " x" + count);
                                    }
                                    else
                                    {
                                        player.redDialog("Vui lòng lấy vật phẩm với số lượng < 1000 với các vật phẩm không gộp\n Tránh trường hợp đốt cpu server");
                                    }
                                }
                            }
                            else
                            {
                                player.redDialog("Không có item với id = " + itemTemplateId);
                            }
                        }
                    }
                    break;

                case INPUT_DIALOG_EXCHANGE_GOLD_TO_COIN:
                    {
                        /*
                        if (true)
                        {
                            player.redDialog("Cây ATM này hiện đã hết ngọc bạn vui lòng chờ nhân viên ngân hàng nạp thêm ngọc vào cây ATM này!");
                            return;
                        }*/
                        long value = Math.Abs(reader.readlong(0));
                        if (player.checkGold(value))
                        {
                            player.mineGold(value);
                            long valueCoin = value * GopetManager.PERCENT_EXCHANGE_GOLD_TO_COIN;
                            player.addCoin(valueCoin);
                            player.okDialog(Utilities.Format("Chúc mừng bạn đổi thành công %s (ngoc)", Utilities.FormatNumber(valueCoin)));
                        }
                        else
                        {
                            player.controller.notEnoughGold();
                        }
                    }
                    break;

                case INPUT_DIALOG_ADMIN_GET_HISTORY:
                    {
                        if (player.checkIsAdmin())
                        {

                        }
                    }
                    break;

                case INPUT_DIALOG_ADMIN_CHAT_GLOBAL:
                    {
                        if (player.checkIsAdmin())
                        {
                            String text = reader.readString(0);
                            PlayerManager.showBanner(text);
                        }
                    }
                    break;

                case INPUT_DIALOG_ADMIN_TELE_TO_PLAYER:
                    {
                        if (player.checkIsAdmin())
                        {
                            String namePlayer = reader.readString(0);
                            Player playerPassive = PlayerManager.get(namePlayer);
                            if (playerPassive != null)
                            {
                                GopetPlace gopetPlace = (GopetPlace)playerPassive.getPlace();
                                if (gopetPlace != null)
                                {
                                    gopetPlace.add(player);
                                }
                            }
                            else
                            {
                                player.redDialog("Người chơi đã offline");
                            }
                        }
                    }
                    break;

                case INPUT_DIALOG_ADMIN_UNLOCK_USER:
                    {
                        if (player.checkIsAdmin())
                        {
                            String namePlayer = reader.readString(0);
                            using (var gameconn = MYSQLManager.create())
                            {
                                using (var webconn = MYSQLManager.createWebMySqlConnection())
                                {
                                    dynamic queryData = gameconn.QueryFirstOrDefault("Select user_id from player where name ='" + namePlayer + "'");
                                    if (queryData != null)
                                    {
                                        webconn.Execute("Update `User` set isBenned = 0 where user_id = @user_id", new { user_id = queryData.user_id });
                                    }
                                    else
                                    {
                                        player.redDialog("Người chơi này không tồn tại");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case INPUT_DIALOG_ADMIN_LOCK_USER:
                    {
                        if (player.checkIsAdmin())
                        {
                            String namePlayer = reader.readString(3);
                            sbyte typeLock = reader.readsbyte(1);
                            int min = reader.readInt(2);
                            String reason = reader.readString(0);
                            using (var gameconn = MYSQLManager.create())
                            {
                                using (var webconn = MYSQLManager.createWebMySqlConnection())
                                {
                                    dynamic queryData = gameconn.QueryFirstOrDefault("Select user_id from player where name ='" + namePlayer + "'");
                                    if (queryData != null)
                                    {
                                        UserData.banBySQL(typeLock, reason, Utilities.CurrentTimeMillis + (min * 1000L * 60), queryData.user_id);
                                        Player playerPassive = PlayerManager.get(namePlayer);
                                        if (playerPassive != null)
                                        {
                                            playerPassive.session.Close();
                                        }
                                    }
                                    else
                                    {
                                        player.redDialog("Người chơi này không tồn tại");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case INPUT_TYPE_NAME_BUFF_ENCHANT_TATTOO:
                case INPUT_TYPE_NAME_TO_BUFF_ENCHANT:
                    {
                        if (player.checkIsAdmin())
                        {
                            String name = reader.readString(0);
                            Player playerOnline = PlayerManager.get(name);
                            if (playerOnline != null)
                            {
                                if (dialogInputId == INPUT_TYPE_NAME_TO_BUFF_ENCHANT)
                                {
                                    playerOnline.controller.setBuffEnchent(true);
                                    player.okDialog(Utilities.Format("Buff cho người chơi đập không thất bại thành công!", name));
                                }
                                else
                                {
                                    playerOnline.controller.IsBuffEnchantTatto = true;
                                    player.okDialog(Utilities.Format("Buff cho người chơi cường hóa xăm không thất bại thành công!", name));
                                }
                            }
                            else
                            {
                                player.redDialog("Người chơi đã offline");
                            }
                        }
                    }
                    break;
                case INPUT_TYPE_NAME_TO_BUFF_COIN:
                    {
                        if (player.checkIsAdmin())
                        {
                            String name = reader.readString(0);
                            int coin = reader.readInt(1);
                            using (var conn = MYSQLManager.createWebMySqlConnection())
                            {

                                UserData userData = conn.QueryFirstOrDefault<UserData>("SELECT * from user where username = @username", new { coin = conn, username = name });
                                if (userData != null)
                                {
                                    userData.mineCoin(-coin, userData.getCoin());
                                    player.okDialog($"Thành công người chơi đó hiện có {Utilities.FormatNumber(userData.getCoin())} vnd");
                                }
                                else
                                {
                                    player.redDialog("Sai tên tài khoản");
                                }
                            }

                        }
                    }
                    break;

                case INPUT_DIALOG_CHANGE_SLOGAN_CLAN:
                    {
                        ClanMember clanMember = player.controller.getClan();
                        if (clanMember != null)
                        {
                            if (clanMember.duty == Clan.TYPE_LEADER)
                            {
                                String slogan = reader.readString(0);
                                if (slogan.Length >= 500)
                                {
                                    player.redDialog("Khẩu hiệu không quá 500 từ");
                                }
                                else
                                {
                                    clanMember.getClan().setSlogan(slogan);
                                }
                            }
                            else
                            {
                                player.redDialog("Bạn không có quyền này, chỉ có bang chủ mới thao tác được.");
                            }
                        }
                        else
                        {
                            player.controller.notClan();
                        }
                    }
                    break;
                case INPUT_TYPE_NAME_PET_WHEN_BUY_PET:
                    {
                        string name = reader.readString(0);
                        if (name.Length > 30 || name.Length <= 5)
                        {
                            player.redDialog("Tên pet không dài quá 30 ký tự và phải lớn hơn 6 ký tự");
                            return;
                        }
                        player.controller.objectPerformed[OBJKEY_NAME_PET_WANT] = name;
                        selectMenu(player.controller.objectPerformed[OBJKEY_ID_MENU_BUY_PET_TO_NAME], player.controller.objectPerformed[OBJKEY_INDEX_MENU_BUY_PET_TO_NAME], player.controller.objectPerformed[OBJKEY_PAYMENT_INDEX_WANT_TO_NAME_PET], player);
                        break;
                    }
                case INPUT_TYPE_NAME_PLAYER_TO_GIVE_ITEM:
                case INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM:
                    {
                        string name = reader.readString(1);
                        int count = reader.readInt(0);
                        if (player.checkIsAdmin())
                        {
                            Player playerOnline = PlayerManager.get(name);
                            if (playerOnline != null)
                            {
                                if (dialogInputId == INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM)
                                {
                                    player.controller.objectPerformed[OBJKEY_PLAYER_GET_ITEM] = playerOnline;
                                    player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GET_BY_ADMIN] = count;
                                    sendMenu(MENU_SELECT_ITEM_TO_GET_BY_ADMIN, player);
                                }
                                else
                                {
                                    player.controller.objectPerformed[OBJKEY_PLAYER_GIVE_ITEM] = playerOnline;
                                    player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GIVE_BY_ADMIN] = count;
                                    sendMenu(MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN, player);
                                }
                            }
                            else
                            {
                                player.redDialog("Người chơi đã offline");
                            }
                        }
                        break;
                    }
                case INPUT_DIALOG_CREATE_CLAN:
                    {
                        String clanName = reader.readString(0);
                        if (player.HaveClan)
                        {
                            player.redDialog("Bạn đã có bang hội rồi");
                            return;
                        }
                        else
                        {
                            if (Utilities.CheckString(clanName, "^[a-z0-9]+$"))
                            {
                                if (clanName.Length >= 5 && clanName.Length <= 20)
                                {
                                    if (player.checkCoin(GopetManager.COIN_CREATE_CLAN) && player.checkGold(GopetManager.GOLD_CREATE_CLAN))
                                    {
                                        if (!ClanManager.clanHashMapName.ContainsKey(clanName))
                                        {
                                            try
                                            {
                                                Clan clan = new Clan(clanName, player.user.user_id, player.playerData.name);
                                                clan.create();
                                                ClanManager.addClan(clan);
                                                player.playerData.clanId = clan.getClanId();
                                                player.mineCoin(GopetManager.COIN_CREATE_CLAN);
                                                player.mineGold(GopetManager.GOLD_CREATE_CLAN);
                                                player.okDialog(Utilities.Format("Tạo bang %s thành công", clanName));
                                            }
                                            catch (MySqlException e)
                                            {
                                                if (e.Code == 1062)
                                                {
                                                    player.redDialog("Lỗi trùng lập tên bang hội theo thời gian thực, nó hiếm khi xảy ra");
                                                }
                                                e.printStackTrace();
                                            }
                                            catch (Exception e)
                                            {
                                                e.printStackTrace();
                                                player.redDialog("Lỗi tạo bang");
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog("Tên bang hội này đã tồn tại");
                                        }
                                    }
                                    else
                                    {
                                        player.redDialog("Không đủ (vang) và (ngoc)");
                                    }
                                }
                                else
                                {
                                    player.redDialog("Tên bang cần bé hơn 20 ký tự và lớn hơn 4 ký tự");
                                }
                            }
                            else
                            {
                                player.redDialog("Tên bang hội không có dấu và ký tự đặc biệt nhé");
                            }
                        }
                    }
                    break;
            }
        }
        catch (FormatException ex)
        {
            player.redDialog("Nhập sai, vui lòng nhập các con số");
        }
        catch (Exception e)
        {
            e.printStackTrace();
            player.redDialog("Đã xảy ra lõi, xD");
        }
    }
}


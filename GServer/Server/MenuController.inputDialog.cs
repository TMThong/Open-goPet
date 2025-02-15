using Dapper;
using Gopet.APIs;
using Gopet.Data.Collections;
using Gopet.Data.Dialog;
using Gopet.Data.Event.Year2025;
using Gopet.Data.GopetClan;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Manager;
using Gopet.Util;
using MySqlConnector;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                case INPUT_COUNT_OF_ITEM_TRASH_WANT_SELL:
                    {
                        if (!player.controller.objectPerformed.ContainsKey(OBJKEY_ITEM_TRASH_WANT_TO_SELL))
                        {
                            player.fastAction();
                            return;
                        }
                        int count = reader.readInt(0);
                        if (count > 0)
                        {
                            Item itemSell = player.controller.objectPerformed[OBJKEY_ITEM_TRASH_WANT_TO_SELL];
                            player.controller.sellItem(count, itemSell);
                        }
                        else
                        {
                            player.redDialog(player.Language.BugWarning);
                        }
                    }
                    break;
                case INPUT_DIALOG_KIOSK:
                    {
                        int priceItem = reader.readInt(0);
                        if (priceItem <= 0)
                        {
                            player.redDialog(player.Language.BugWarning);
                            return;
                        }
                        if (priceItem > 2000000000)
                        {
                            player.redDialog(string.Format(player.Language.GemLimitWarning, Utilities.FormatNumber(2000000000)));
                            return;
                        }
                        player.controller.objectPerformed[OBJKEY_PRICE_KIOSK_ITEM] = priceItem;
                        sendMenu(MENU_OPTION_KIOSK, player);
                    }
                    break;
                case INPUT_TYPE_GIFT_CODE:
                    {
                        ClanMember clanMember = player.controller.getClan();
                        if (!player.controller.canTypeGiftCode())
                        {
                            player.fastAction();
                            return;
                        }
                        String code = reader.readString(0);
                        if (code.Length != 0)
                        {
                            if (Utilities.CheckString(code, "^[a-z0-9A-Z]+$"))
                            {
                                player.okDialog(player.Language.PleaseWait);

                                using (MySqlConnection MySqlConnection = MYSQLManager.create())
                                {
                                    try
                                    {
                                        var keyL = MySqlConnection.QuerySingleOrDefault("SELECT GET_LOCK(@code, 10) as hasLock;", new { code = "gift_code_lock_" + code });
                                        if (keyL != null)
                                        {
                                            bool hasLock = keyL.hasLock == 1;
                                            if (!hasLock)
                                            {
                                                player.redDialog(player.Language.UseGiftCodeSystemDown);
                                            }
                                            else
                                            {
                                                GiftCodeData giftCodeData = MySqlConnection.QuerySingleOrDefault<GiftCodeData>("SELECT * FROM `gift_code` WHERE `gift_code`.`code` = @code;", new { code = code });
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
                                                            player.redDialog(player.Language.YouHaveUseThisGiftCode);
                                                            goto EndGiftCode;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (giftCodeData.getUsersOfUseThis().Contains(clanMember.clan.clanId))
                                                        {
                                                            player.redDialog(player.Language.YourClanHaveUseThisGiftCode);
                                                            goto EndGiftCode;
                                                        }
                                                    }
                                                    if (giftCodeData.getCurUser() >= giftCodeData.getMaxUser())
                                                    {
                                                        player.redDialog(player.Language.NumOfPlayerUseGiftCodeIsLimit);
                                                    }
                                                    else
                                                    {
                                                        if (giftCodeData.getExpire().GetTimeMillis() < Utilities.CurrentTimeMillis)
                                                        {
                                                            player.redDialog(player.Language.GiftCodeIsExpired);
                                                        }
                                                        else
                                                        {
                                                            if (giftCodeData.isClanCode)
                                                                giftCodeData.getUsersOfUseThis().add(clanMember.clan.clanId);
                                                            else
                                                                giftCodeData.getUsersOfUseThis().add(player.user.user_id);
                                                            giftCodeData.currentUser++; ;
                                                            if (giftCodeData.getGift_data().Length <= 0)
                                                            {
                                                                player.redDialog(player.Language.GiftCodeEmptyGift);
                                                            }
                                                            else
                                                            {
                                                                JArrayList<Popup> popups = player.controller.onReiceiveGift(giftCodeData.getGift_data());
                                                                JArrayList<String> textInfo = new();
                                                                foreach (Popup popup in popups)
                                                                {
                                                                    textInfo.add(popup.getText());
                                                                }
                                                                player.okDialog(string.Format(player.Language.GetGiftCodeOK, String.Join(",", textInfo)));
                                                            }
                                                            MySqlConnection.Execute("UPDATE `gift_code` SET `currentUser` = @currentUser , `usersOfUseThis` = @usersOfUseThis WHERE `id` =  @id;", giftCodeData);
                                                        }
                                                    }

                                                EndGiftCode:;
                                                }
                                                else
                                                {
                                                    player.redDialog(player.Language.NotHaveGiftCode);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.GiftCodeError);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        e.printStackTrace();
                                    }
                                    finally
                                    {
                                        MySqlConnection.Execute("DO RELEASE_LOCK(@code);", new { code = "gift_code_lock_" + code });
                                    }
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.HaveSpecialChar);
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.EmptyField);
                        }
                    }
                    break;
                case INPUT_DIALOG_CHALLENGE_INVITE:
                    int priceChallenge = reader.readInt(0);
                    if (priceChallenge <= 0)
                    {
                        player.redDialog(player.Language.BugWarning);
                        return;
                    }
                    if (priceChallenge > 100000)
                    {
                        player.redDialog(string.Format(player.Language.GemLimitWarning, Utilities.FormatNumber(100000)));
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
                            player.controller.showInputDialog(INPUT_DIALOG_KIOSK, player.Language.Pricing, new String[] { "  " }, new sbyte[] { 0 });
                        }
                        else
                        {
                            player.redDialog(player.Language.WrongNumOfItem);
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
                            player.okDialog(player.Language.Correct);
                        }
                        else
                        {
                            player.redDialog(player.Language.Incorrect);
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
                                    item.SourcesItem.Add(Gopet.Data.item.ItemSource.BUFF_BẨN);
                                    player.addItemToInventory(item);
                                    player.okDialog(item.getName(player));
                                }
                                else
                                {
                                    if (count < 1000)
                                    {
                                        for (int i = 0; i < count; i++)
                                        {
                                            Item item = new Item(itemTemplateId);
                                            item.count = 1;
                                            item.SourcesItem.Add(Gopet.Data.item.ItemSource.TỪ_GIFT_CODE);
                                            player.addItemToInventory(item);

                                        }
                                        player.okDialog(itemTemplate.getName(player) + " x" + count);
                                    }
                                    else
                                    {
                                        player.redDialog("Vui lòng lấy vật phẩm với số lượng < 1000 với các vật phẩm không gộp\n Tránh trường hợp đốt cpu server");
                                    }
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.CannotFoundItemWithId + itemTemplateId);
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
                        if (player.checkGold(value) && value > 0)
                        {
                            player.mineGold(value);
                            long valueCoin = value * GopetManager.PERCENT_EXCHANGE_GOLD_TO_COIN;
                            player.addCoin(valueCoin);
                            player.okDialog(string.Format(player.Language.ChangeGoldToCoinMessageOK + "(ngoc)", Utilities.FormatNumber(valueCoin)));
                        }
                        else
                        {
                            player.controller.notEnoughGold();
                        }
                    }
                    break;
                case INPUT_TYPE_EXCHANGE_LUA_TO_COIN:
                    {
                        long value = Math.Abs(reader.readlong(0));
                        if (player.checkLua(value) && value > 0)
                        {
                            player.MineLua(value);
                            long valueCoin = value * GopetManager.PERCENT_EXCHANGE_LUA_TO_COIN;
                            player.addCoin(valueCoin);
                            player.okDialog(string.Format(player.Language.ChangeGoldToCoinMessageOK + "(ngoc)", Utilities.FormatNumber(valueCoin)));
                        }
                        else
                        {
                            player.controller.notEnoughLua();
                        }
                    }
                    break;
                case INPUT_DIALOG_EXCHANGE_COIN_TO_LUA:
                    {
                        long value = Math.Abs(reader.readlong(0));
                        long sodu = value % GopetManager.PERCENT_EXCHANGE_CON_TO_LUA_1;
                        if (sodu != 0 || value == 0)
                        {
                            player.redDialog(player.Language.IncorrectCoinValueWhenExchangeLua, Utilities.FormatNumber(sodu));
                            return;
                        }
                        if (player.checkCoin(value))
                        {
                            player.mineCoin(value);
                            long valueLua = (value / GopetManager.PERCENT_EXCHANGE_CON_TO_LUA_1) * GopetManager.PERCENT_EXCHANGE_CON_TO_LUA_2;
                            player.AddLua(valueLua);
                            player.okDialog(string.Format(player.Language.ChangeGoldToCoinMessageOK + "(lua)", Utilities.FormatNumber(valueLua)));
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
                            PlayerManager.showBanner((l) => text);
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
                                player.redDialog(player.Language.PlayerOffline);
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
                                        webconn.Execute("Update `User` set isBaned = 0 where user_id = @user_id", new { user_id = queryData.user_id });
                                        player.okDialog(player.Language.OK);
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.PlayerNotFound);
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
                                        player.okDialog(player.Language.OK);
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.PlayerNotFound);
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
                                    player.okDialog(string.Format("Buff cho người chơi đập không thất bại thành công!", name));
                                }
                                else
                                {
                                    playerOnline.controller.IsBuffEnchantTatto = true;
                                    player.okDialog(string.Format("Buff cho người chơi cường hóa xăm không thất bại thành công!", name));
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.PlayerNotFound);
                            }
                        }
                    }
                    break;
                case INPUT_TYPE_NAME_TO_BUFF_COIN:
                    {
                        if (player.checkIsAdmin())
                        {
                            string name = reader.readString(0);
                            int coin = reader.readInt(1);
                            using (var conn = MYSQLManager.createWebMySqlConnection())
                            {
                                UserData userData = conn.QueryFirstOrDefault<UserData>("SELECT * from user where username = @username", new { coin = conn, username = name });
                                if (userData != null)
                                {
                                    userData.mineCoin(-coin, userData.getCoin());
                                    player.okDialog($"Thành công người chơi đó hiện có {Utilities.FormatNumber(userData.getCoin())} vnd");
                                    HistoryManager.addHistory(new History(player).setLog($"Cộng tiền {coin.ToString("###,###,###")}đ cho nhân vật {name}").setObj(new { Name = name, Coin = coin }));
                                }
                                else
                                {
                                    player.redDialog(player.Language.WrongUsername);
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
                                    player.redDialog(player.Language.ClanSetSloganLaw);
                                }
                                else
                                {
                                    clanMember.getClan().setSlogan(slogan);
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.YouEnoughPermissionOnlyLeader);
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
                            player.redDialog(player.Language.SetPetNameLaw);
                            return;
                        }
                        player.controller.objectPerformed[OBJKEY_NAME_PET_WANT] = name;
                        selectMenu(player.controller.objectPerformed[OBJKEY_ID_MENU_BUY_PET_TO_NAME], player.controller.objectPerformed[OBJKEY_INDEX_MENU_BUY_PET_TO_NAME], player.controller.objectPerformed[OBJKEY_PAYMENT_INDEX_WANT_TO_NAME_PET], player);
                        break;
                    }
                case INPUT_TYPE_NAME_LOCK_ITEM_PLAYER:
                case INPUT_TYPE_NAME_UNLOCK_ITEM_PLAYER:
                case INPUT_TYPE_NAME_PLAYER_TO_GIVE_ITEM:
                case INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM:
                    {
                        string name = reader.readString(0);
                        if (player.checkIsAdmin())
                        {
                            Player playerOnline = PlayerManager.get(name);
                            if (playerOnline != null)
                            {
                                switch (dialogInputId)
                                {
                                    case INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM:
                                        {
                                            player.controller.objectPerformed[OBJKEY_PLAYER_GET_ITEM] = playerOnline;
                                            sendMenu(MENU_SELECT_ITEM_TO_GET_BY_ADMIN, player);
                                        }
                                        break;
                                    case INPUT_TYPE_NAME_PLAYER_TO_GIVE_ITEM:
                                        {
                                            player.controller.objectPerformed[OBJKEY_PLAYER_GIVE_ITEM] = playerOnline;
                                            sendMenu(MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN, player);
                                        }
                                        break;
                                    case INPUT_TYPE_NAME_UNLOCK_ITEM_PLAYER:
                                        {
                                            player.controller.objectPerformed[OBJKEY_PLAYER_UNLOCK_ITEM] = playerOnline;
                                            sendMenu(MENU_UNLOCK_ITEM_PLAYER, player);
                                        }
                                        break;
                                    case INPUT_TYPE_NAME_LOCK_ITEM_PLAYER:
                                        {
                                            player.controller.objectPerformed[OBJKEY_PLAYER_LOCK_ITEM] = playerOnline;
                                            sendMenu(MENU_LOCK_ITEM_PLAYER, player);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.PlayerOffline);
                            }
                        }
                        break;
                    }
                case INPUT_TYPE_NAME_PLAYER_TO_ENBALE_MERGE_SERVER:
                    {
                        string name = reader.readString(0);
                        if (player.checkIsAdmin())
                        {
                            Player playerOnline = PlayerManager.get(name);
                            if (playerOnline != null)
                            {
                                playerOnline.playerData.IsMergeServer = false;
                                player.okDialog($"Người chơi {name} có thể gộp được rồi");
                            }
                            else
                            {
                                player.redDialog(player.Language.PlayerOffline);
                            }
                        }
                    }
                    break;
                case INPUT_TYPE_COUNT_ADMIN_GIVE:
                case INPUT_TYPE_COUNT_ADMIN_GET:
                    {
                        if (player.checkIsAdmin())
                        {
                            int count = reader.readInt(0);
                            if (dialogInputId == INPUT_TYPE_COUNT_ADMIN_GET)
                            {
                                player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GET_BY_ADMIN] = count;
                                sendMenu(MENU_OPTION_ADMIN_GET_ITEM, player);
                            }
                            else
                            {
                                player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GIVE_BY_ADMIN] = count;
                                sendMenu(MENU_OPTION_ADMIN_GIVE_ITEM, player);
                            }
                        }
                    }
                    break;
                case INPUT_TYPE_FAST_UP_ITEM:
                    {
                        if (player.checkIsAdmin())
                        {
                            int itemId = reader.readInt(0);
                            int LevelUpTier = reader.readInt(1);
                            int MaxTier = reader.readInt(2);
                            int count = reader.readInt(3);
                            int MaxOption = reader.readInt(4);
                            int EndLevel = reader.readInt(5);
                            int Fusion = reader.readInt(6);
                            ServerController.BuffItem(player.playerData.name, itemId, LevelUpTier, MaxTier, count, MaxOption == 1, EndLevel, (byte)Fusion);
                        }
                    }
                    break;
                case INPUT_DIALOG_CREATE_CLAN:
                    {
                        String clanName = reader.readString(0);
                        if (player.HaveClan)
                        {
                            player.redDialog(player.Language.YouHaveClan);
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
                                                player.okDialog(string.Format(player.Language.CreateClanOK, clanName));
                                            }
                                            catch (MySqlException e)
                                            {
                                                player.redDialog(player.Language.CreateClanDuplicateError);
                                                e.printStackTrace();
                                            }
                                            catch (Exception e)
                                            {
                                                e.printStackTrace();
                                                player.redDialog(player.Language.CreateClanError);
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.ClanDuplicateName);
                                        }
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.NotEnoughMaterial);
                                    }
                                }
                                else
                                {
                                    player.redDialog(player.Language.ClanCreateNameLaw);
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.HaveSpecialChar);
                            }
                        }
                    }
                    break;
                case INPUT_TYPE_COUNT_USE_BÓ_HOA:
                    {
                        int count = Math.Min(Math.Abs(reader.readInt(0)), 100000);
                        player.controller.objectPerformed[OBJKEY_COUNT_USE_BÓ_HOA] = count;
                        player.okDialog(player.Language.SetCountUseBoHoa, count);
                    }
                    break;
                case INPUT_OTP_2FA:
                    {
                        if (PlayerManager.OtpTracker.IsLimited(player.user.username))
                        {
                            if (!string.IsNullOrEmpty(player.user.email) && !PlayerManager.EmailTracker.IsLimited(player.user.username + player.user.email))
                            {
                                IPEndPoint iPEndPoint = (IPEndPoint)player.session.CSocket.RemoteEndPoint;
                                PlayerManager.EmailTracker.Add(player.user.username + player.user.email);
                                GopetManager.SendHtmlMailAsync(
                                    player.user.email, 
                                    "Gopet - Thông báo đăng nhập 2 lớp OTP", 
                                    $"Có vẻ ai đó có mật khẩu của bạn! Nếu không phải là bạn hãy nhanh chóng đổi mật khẩu. Otp đã được thử 10 lần không thành công. <br> Địa chỉ IP thử OTP là: <b>{iPEndPoint.Address.ToString()}</b>");
                            }
                            player.redDialog("Bạn đã thử OTP nhiều lần. Vui lòng thử lại sau 30 phút.");
                            return;
                        }
                        PlayerManager.OtpTracker.Add(player.user.username);
                        string text = reader.readString(0).Trim().Replace(" ", "");
                        if (int.TryParse(text, out var result))
                        {
                            if (text.Length != 6)
                            {
                                player.redDialog(player.Language.OTP2FALaw);
                                return;
                            }
                            Totp totp = new Totp(Base32Encoding.ToBytes(player.user.secretKey));
                            if (totp.VerifyTotp(text, out long timeStepMatched, new VerificationWindow(5, 5)))
                            {
                                player.IsLogin2FAOK = true;
                                using (var conn = MYSQLManager.createWebMySqlConnection())
                                {
                                    player.ProcessingUser(conn);
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.OTP2FAFail);
                            }
                        }
                        else player.redDialog(player.Language.WrongInputNumber);
                    }
                    break;
                case INPUT_NUM_DUNG_HỢP:
                    {
                        int num = reader.readInt(0);
                        if (player.checkIsAdmin())
                        {
                            if (player.controller.objectPerformed.ContainsKey(OBJKEY_ITEM_BUFF_DUNG_HỢP))
                            {
                                int itemId = player.controller.objectPerformed[OBJKEY_ITEM_BUFF_DUNG_HỢP];
                                Item item = player.controller.selectItemByItemId(itemId, GopetManager.EQUIP_PET_INVENTORY);
                                if (item != null)
                                {
                                    item.NumFusion = (byte)num;
                                    player.okDialog("Buff dung hợp lên cấp {0} thành công", item.NumFusion);
                                }
                            }
                        }
                    }
                    break;
                case INPUT_ASSIGNED_CHANGE_NAME_KIOSK:
                case INPUT_ASSIGNED_NAME_KIOSK:
                    {
                        string assignedName = reader.readString(0).Trim();
                        if (string.IsNullOrEmpty(assignedName))
                        {
                            player.redDialog("Tên người chỉ định rỗng");
                            return;
                        }
                        if (dialogInputId == INPUT_ASSIGNED_CHANGE_NAME_KIOSK)
                        {
                            sbyte typeKiosk = player.controller.objectPerformed.get(MenuController.OBJKEY_TYPE_SHOW_KIOSK);
                            Kiosk kiosk = MarketPlace.getKiosk(typeKiosk);
                            SellItem sellItem = kiosk.getItemByUserId(player.user.user_id);
                            if (sellItem != null && sellItem.AssignedName != null)
                            {
                                sellItem.AssignedName = assignedName;
                                player.okDialog("Thay đổi tên người chỉ định thành công");
                            }
                            else
                            {
                                player.redDialog("Trước đó bạn không có chỉnh định người nào");
                            }
                            return;
                        }
                        if (player.controller.objectPerformed.ContainsKey(OBJKEY_PRICE_KIOSK_ITEM))
                        {
                            int price = player.controller.objectPerformed[OBJKEY_PRICE_KIOSK_ITEM];
                            player.controller.objectPerformed.Remove(OBJKEY_PRICE_KIOSK_ITEM);
                            SellKioskItem(player, price, assignedName);
                        }
                    }
                    break;
                case INPUT_NUM_BUY_RETAIL_ITEM_KIOSK:
                    {
                        if (!player.controller.objectPerformed.ContainsKey(OBJKEY_KIOSK_ITEM))
                            return;
                        int count = Math.Abs(reader.readInt(0));
                        var obj = player.controller.objectPerformed.get(OBJKEY_KIOSK_ITEM);
                        var objENtry = (KeyValuePair<Kiosk, SellItem>)obj;
                        objENtry.Key.buyRetail(objENtry.Value.itemId, player, count);
                    }
                    break;
                case INPUT_DIALOG_SET_PET_SELECTED_INFo:
                    {
                        if (player.checkIsAdmin())
                        {
                            int lvl = reader.readInt(2);
                            int gym = reader.readInt(0);
                            int star = reader.readInt(1);
                            if (star > 100)
                            {
                                player.redDialog("Lag máy chủ anh ơi, hãy tưởng tượng số sao mà cái máy render chắc nổ luôn quá");
                                return;
                            }
                            if (player.playerData.petSelected == null)
                            {
                                player.petNotFollow();
                                return;
                            }
                            player.playerData.petSelected.lvl = lvl;
                            player.playerData.petSelected.tiemnang_point = gym;
                            player.playerData.petSelected.star = star;
                            player.okDialog("Set pet cấp {0} có {1} gym và {2} (sao) thành công", lvl, gym, star);
                        }
                    }
                    break;
                case INPUT_USE_NUM_ITEM:
                    {
                        int count = reader.readInt(0);
                        if (count > 0 && player.controller.objectPerformed.ContainsKey(OBJKEY_ID_ITEM_USE_ITEM_COUNT))
                        {
                            int itemId = player.controller.objectPerformed[OBJKEY_ID_ITEM_USE_ITEM_COUNT];
                            Item item = player.controller.selectItemsbytemp(itemId, GopetManager.NORMAL_INVENTORY);
                            if (item != null && GameController.checkCount(item, count))
                            {
                                switch (item.Template.itemId)
                                {
                                    case GameBirthdayEvent.ID_SQUARE_CAKE:
                                    case GameBirthdayEvent.ID_CYLINDRIAL_CAKE:
                                        EventManager.FindAndUseItemEvent(item.Template.itemId, player, count);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.BugWarning);
                        }
                    }
                    break;
            }
        }
        catch (FormatException ex)
        {
            player.redDialog(player.Language.WrongInputNumber);
        }
        catch (Exception e)
        {
            e.printStackTrace();
            player.redDialog("Đã xảy ra lõi, xD");
        }
    }
}


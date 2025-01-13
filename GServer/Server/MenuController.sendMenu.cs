

using Gopet.Battle;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.Dialog;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySqlConnector;
using Gopet.Data.item;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Gopet.Data.top;
using Gopet.Data.dialog;
using Gopet.Data.Clan;
using Gopet.Data.Event.Year2024;

public partial class MenuController
{
    public static void sendMenu(int menuId, Player player)
    {
        switch (menuId)
        {
            case MENU_SELL_TRASH_ITEM:
                {
                    List<Item> items = new List<Item>();
                    foreach (var itemKeypair in player.playerData.items)
                    {
                        items.AddRange(itemKeypair.Value);
                    }
                    JArrayList<MenuItemInfo> menuItemInfos = new();
                    foreach (var item in items)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo();
                        menuItemInfo.setCanSelect(true);
                        menuItemInfo.setDesc(string.Format(player.Language.PriceKioskDescription, item.Template.price) + " (ngoc)");
                        menuItemInfo.setTitleMenu(item.getName(player));
                        menuItemInfo.setImgPath(item.Template.iconPath);
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, item.Template.getName(player)));
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfos.Add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.SellYourItemMenuTitle, menuItemInfos);
                }
                break;
            case MENU_PET_REINCARNATION:
            case MENU_PET_SACRIFICE:
            case MENU_FUSION_MENU_PET:
            case MENU_SELECT_PET_TO_DEF_LEAGUE:
            case MENU_KIOSK_PET_SELECT:
            case MENU_PET_INVENTORY:
            case MENU_SELECT_PET_UPGRADE_ACTIVE:
            case MENU_SELECT_PET_UPGRADE_PASSIVE:
                {
                    player.controller.removePetTrial();
                    CopyOnWriteArrayList<Pet> listPet = (CopyOnWriteArrayList<Pet>)player.playerData.pets.clone();
                    JArrayList<MenuItemInfo> petItemInfos = new();
                    int i = 0;
                    if (menuId == MENU_PET_INVENTORY)
                    {
                        Pet p = player.getPet();
                        if (p != null)
                        {
                            i = -1;
                            listPet.add(0, p);
                        }
                    }
                    foreach (Pet pet in listPet)
                    {
                        MenuItemInfo menuItemInfo = new PetMenuItemInfo(pet, player);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, pet.getNameWithStar(player)));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        petItemInfos.add(menuItemInfo);
                        menuItemInfo.setItemId(i);
                        menuItemInfo.setHasId(true);
                        if (i == -1)
                        {
                            menuItemInfo.setTitleMenu(menuItemInfo.getTitleMenu() + player.Language.IsUsing);
                        }
                        i++;
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.YourPet, petItemInfos);
                }
                break;
            case MENU_CHOOSE_PET_FROM_PACKAGE_PET:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_ITEM_PACKAGE_PET_TO_USE))
                    {
                        Item item = player.controller.objectPerformed[OBJKEY_ITEM_PACKAGE_PET_TO_USE];
                        JArrayList<MenuItemInfo> petMenus = new();
                        foreach (int petId in item.Template.itemOptionValue)
                        {
                            if (GopetManager.PETTEMPLATE_HASH_MAP.ContainsKey(petId))
                            {
                                PetMenuItemInfo petMenuItemInfo = new PetMenuItemInfo(GopetManager.PETTEMPLATE_HASH_MAP.get(petId), player);
                                petMenuItemInfo.setCloseScreenAfterClick(true);
                                petMenuItemInfo.setShowDialog(true);
                                petMenuItemInfo.setDialogText(player.Language.DoWantSelectIt);
                                petMenuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                petMenus.add(petMenuItemInfo);
                            }
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.GiftOfTaskTitle, petMenus);
                    }
                }
                break;
            case MENU_SHOW_ALL_PLAYER_HAVE_ITEM_LVL_10:
                {

                }
                break;

            case MENU_SHOW_ALL_TATTO:
                {
                    JArrayList<MenuItemInfo> menuInfos = new();

                    foreach (var tattoKeyPair in GopetManager.tattos)
                    {
                        var tatto = tattoKeyPair.Value;
                        MenuItemInfo menuItemInfo = new MenuItemInfo(tatto.getName(player), $"{tatto.atk} (atk) {tatto.def}(def) {tatto.hp}(hp) {tatto.mp}(mp)", tatto.iconPath, false);
                        menuInfos.add(menuItemInfo);
                    }

                    player.controller.showMenuItem(menuId, TYPE_MENU_NONE, player.Language.AllTattoo, menuInfos);
                    break;
                }
            case MENU_ADMIN_SHOW_ALL_ACHIEVEMENT:
                {
                    if (player.checkIsAdmin())
                    {
                        JArrayList<MenuItemInfo> menuInfos = new();
                        foreach (var ach in GopetManager.achievements)
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(ach.getName(player), ach.getDescription(player), ach.IconPath, true);
                            menuInfos.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.Archievement, menuInfos);
                    }
                    break;
                }
            case MENU_USE_ACHIEVEMNT:
                {
                    JArrayList<MenuItemInfo> menuInfos = new();
                    foreach (var ach in player.playerData.achievements)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(ach.Template.getName(player), ach.Template.getDescription(player), ach.Template.IconPath, true);
                        menuInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.Archievement, menuInfos);
                }
                break;
            case MENU_USE_ACHIEVEMNT_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, player.Language.Use, 1));
                    if (player.playerData.CurrentAchievementId > 0)
                    {
                        list.Add(new Option(1, player.Language.UnequipCurrentAcrhievement, 1));
                    }
                    player.controller.sendListOption(menuId, player.Language.UseAcrhievement, CMD_CENTER_OK, list);
                }
                break;
            case MENU_ME_SHOW_ACHIEVEMENT:
                {
                    AnimationMenu animationMenu = new AnimationMenu(player.Language.Archievement);
                    animationMenu.Commands.Add(AnimationMenu.RightExitCMD);
                    foreach (var ach in player.playerData.achievements)
                    {
                        animationMenu.AddLabel(0, string.Format(player.Language.ArchievementDescription, ach.Template.getName(player)), FontStyle.SMALL);
                        animationMenu.AddImage(0, ach.Template.FramePath, ach.Template.FrameNum);
                        animationMenu.AddLabel(0, $"{player.Language.InfoDescrption} {ach.Template.Atk} (atk) {ach.Template.Def} (def) {ach.Template.Hp} (hp) {ach.Template.Mp} (mp)", FontStyle.SMALL);
                        animationMenu.AddLabel(0, $"{player.Language.ExpireDescrption} {(ach.Expire == null ? player.Language.InfinityExpire : Utilities.ToDateString(ach.Expire.Value))}", FontStyle.SMALL);
                    }
                    player.controller.showAnimationMenu(menuId, animationMenu);
                }
                break;
            case MENU_SHOW_ALL_ITEM:
                {
                    JArrayList<MenuItemInfo> menuInfos = new();

                    foreach (ItemTemplate itemTemplate in GopetManager.NonAdminItemList)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(itemTemplate.getNameViaType(player) + "((chienluc)" + itemTemplate.getItemId() + ")", itemTemplate.getDescriptionViaType(player), itemTemplate.getIconPath(), false);
                        menuInfos.add(menuItemInfo);
                    }

                    player.controller.showMenuItem(menuId, TYPE_MENU_NONE, player.Language.AllItems, menuInfos);
                }
                break;
            case MENU_LIST_REQUEST_ADD_FRIEND:
            case MENU_LIST_BLOCK_FRIEND:
            case MENU_LIST_FRIEND:
                {
                    IEnumerable<int> ints = null;
                    switch (menuId)
                    {
                        case MENU_LIST_FRIEND:
                            ints = player.playerData.ListFriends;
                            break;
                        case MENU_LIST_REQUEST_ADD_FRIEND:
                            ints = player.playerData.RequestAddFriends;
                            break;
                        case MENU_LIST_BLOCK_FRIEND:
                            ints = player.playerData.BlockFriendLists;
                            break;
                        default:
                            return;
                    }
                    JArrayList<MenuItemInfo> menuInfos = new();
                    if (ints.Any())
                    {
                        using (var conn = MYSQLManager.create())
                        {
                            var friendQuery = conn.Query($"SELECT  `name`, `avatarPath`, `LastTimeOnline` FROM `player` WHERE `player`.`user_id` IN ({ints.ToArray().Join(",")})");
                            foreach (var item in friendQuery)
                            {
                                DateTime LastTimeOnline = item.LastTimeOnline;
                                MenuItemInfo menuItemInfo = new MenuItemInfo(item.name, $"{player.Language.LastTimeOnline} {Utilities.ToDateString(LastTimeOnline)}", item.avatarPath, true);
                                menuItemInfo.setCloseScreenAfterClick(true);
                                menuInfos.add(menuItemInfo);
                            }
                        }
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_NONE, player.Language.FriendList, menuInfos);
                }
                break;
            case MENU_LIST_FRIEND_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, player.Language.GetPlayerLocation, true));
                    list.Add(new Option(1, player.Language.Remove, true));
                    list.Add(new Option(2, player.Language.RemoveAndBlock, true));
                    player.controller.sendListOption(menuId, player.Language.Friend, string.Empty, list);
                }
                break;
            case MENU_LIST_REQUEST_ADD_FRIEND_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, player.Language.Accept, true));
                    list.Add(new Option(1, player.Language.Refuse, true));
                    list.Add(new Option(3, player.Language.RefuseAllPlayer, true));
                    list.Add(new Option(2, player.Language.RefuseAndAddToBlock, true));
                    player.controller.sendListOption(menuId, player.Language.ListWaitingAddFriendList, string.Empty, list);
                }
                break;
            case MENU_LIST_BLOCK_FRIEND_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, player.Language.RemoveBlockFriend, true));
                    list.Add(new Option(1, player.Language.RemoveBlockFriendAll, true));
                    player.controller.sendListOption(menuId, player.Language.ListBlockFriend, string.Empty, list);
                }
                break;
            case MENU_UNEQUIP_SKIN:
            case MENU_UNEQUIP_PET:
                {
                    JArrayList<Option> list = new();
                    list.add(new Option(0, menuId == MENU_UNEQUIP_PET ? player.Language.YouNotHavePetFollow : player.Language.Unequip, Option.CAN_SELECT));
                    String titleStr = "";
                    switch (menuId)
                    {
                        case MENU_UNEQUIP_PET:
                            titleStr = player.Language.MenuUnequipPetTitle;
                            break;
                        case MENU_UNEQUIP_SKIN:
                            titleStr = player.Language.MenuUnequipSkinTitle;
                            break;
                    }
                    player.controller.sendListOption(menuId, titleStr, titleStr, list);
                }
                break;
            case MENU_SHOW_MY_LIST_TASK:
                {
                    CopyOnWriteArrayList<TaskData> taskDatas = player.controller.getTaskCalculator().getTaskDatas();
                    JArrayList<MenuItemInfo> taskMenuInfos = new();
                    foreach (TaskData taskData in taskDatas)
                    {
                        TaskTemplate taskTemplate = taskData.getTemplate();
                        MenuItemInfo menuItemInfo = new MenuItemInfo(taskTemplate.getName(player), taskTemplate.getDescription(player), "dialog/1.png", true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(TaskCalculator.getTaskText(taskData.task, taskData.taskInfo, taskData.timeTask, player));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        taskMenuInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.YourTask, taskMenuInfos);
                }
                break;
            case MENU_SELECT_MONEY_TO_PAY_FOR_ENCHANT_WING:
                {
                    if (!player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_WING_WANT_ENCHANT)) return;

                    Item wingItem = player.controller.findWingItemWantEnchant();

                    if (wingItem != null)
                    {
                        if (wingItem.lvl >= 0 && wingItem.lvl < GopetManager.MAX_LVL_ENCHANT_WING)
                        {
                            EnchantWingData enchantWingData = GopetManager.EnchantWingData[wingItem.lvl + 1];
                            JArrayList<Option> list = new();
                            if (enchantWingData.Coin > 0)
                            {
                                list.add(new Option(0, Utilities.FormatNumber(enchantWingData.Coin) + " (ngoc)", Option.CAN_SELECT));
                            }
                            if (enchantWingData.Gold > 0)
                            {
                                list.add(new Option(1, Utilities.FormatNumber(enchantWingData.Gold) + " (vang)", Option.CAN_SELECT));
                            }

                            player.controller.sendListOption(MENU_SELECT_MONEY_TO_PAY_FOR_ENCHANT_WING, player.Language.PayForEnchantWing, "", list);
                        }
                    }
                    else
                    {
                        throw new NullReferenceException("Tml nào bug index cánh");
                    }
                }
                break;
            case MENU_OPTION_TASK:
                {
                    JArrayList<Option> list = new();

                    list.add(new Option(0, player.Language.Update, Option.CAN_SELECT));
                    list.add(new Option(1, player.Language.TaskIsSuccessfully, Option.CAN_SELECT));
                    list.add(new Option(2, player.Language.CancelTask, Option.CAN_SELECT));

                    player.controller.sendListOption(MENU_OPTION_TASK, player.Language.OptionTask, "", list);
                }
                break;
            case MENU_SELECT_TYPE_PAYMENT_TO_ARENA_JOURNALISM:
            case MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO:
                {
                    JArrayList<Option> list = new();

                    list.add(new Option(0, $"{(menuId == MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO ? GopetManager.PRICE_GOLD_ENCHANT_TATTO : GopetManager.PRICE_GOLD_ARENA_JOURNALISM)} (vang)", Option.CAN_SELECT));
                    list.add(new Option(1, $"{(menuId == MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO ? GopetManager.PRICE_COIN_ENCHANT_TATTO : GopetManager.PRICE_COIN_ARENA_JOURNALISM)} (ngoc)", Option.CAN_SELECT));

                    player.controller.sendListOption(menuId, player.Language.SelectPaymentType, "", list);
                    break;
                }
            case MENU_MONEY_DISPLAY_SETTING:
                {
                    JArrayList<Option> list = new();
                    list.add(new Option(0, player.Language.Pin, Option.CAN_SELECT));
                    list.add(new Option(1, player.Language.Unpin, Option.CAN_SELECT));
                    player.controller.sendListOption(MENU_MONEY_DISPLAY_SETTING, player.Language.Manipulate, "", list);
                }
                break;
            case MENU_SHOW_LIST_TASK:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_NPC_ID_FOR_MAIN_TASK))
                    {
                        int npcId = (int)player.controller.objectPerformed.get(OBJKEY_NPC_ID_FOR_MAIN_TASK);
                        JArrayList<TaskTemplate> taskTemplates = player.controller.getTaskCalculator().getTaskTemplate(npcId);
                        if (taskTemplates.Count > 0)
                        {
                            JArrayList<MenuItemInfo> taskMenuInfos = new();
                            foreach (TaskTemplate taskTemplate in taskTemplates)
                            {
                                MenuItemInfo menuItemInfo = new MenuItemInfo(taskTemplate.getName(player), taskTemplate.getDescription(player), "dialog/1.png", true);
                                menuItemInfo.setShowDialog(true);
                                menuItemInfo.setDialogText(player.Language.DoYouWantGetThisTask + TaskCalculator.getTaskText(null, taskTemplate.getTask(player), taskTemplate.getTimeTask(), player));
                                menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                menuItemInfo.setCloseScreenAfterClick(true);
                                taskMenuInfos.add(menuItemInfo);
                            }
                            player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.Task, taskMenuInfos);
                        }
                        else
                        {
                            player.redDialog(player.Language.ThisNpcIsNotHaveTaskForYourself);
                        }
                    }
                }
                break;
            case MENU_LIST_PET_FREE:
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.GetPetFree, getPetFreeLst(player));
                break;
            case MENU_LEARN_NEW_SKILL:
                {
                    if (player.playerData.petSelected != null)
                    {
                        JArrayList<MenuItemInfo> skillMenuItem = new();
                        foreach (PetSkill petSkill in getPetSkills(player))
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(petSkill.getName(player), petSkill.getDescription(player), petSkill.skillID + "", true);
                            menuItemInfo.setImgPath(Utilities.Format("skills/skill_%s.png", petSkill.skillID));
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, petSkill.getName(player)));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            skillMenuItem.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.LearnSkillForPet, skillMenuItem);

                    }
                }
                break;
            case MENU_EXCHANGE_GOLD:
                {
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, string.Format(player.Language.ExchangeGoldLaw, Utilities.FormatNumber(player.user.getCoin())), EXCHANGE_ITEM_INFOS);
                }
                break;
            case MENU_SELECT_TYPE_CHANGE_GIFT:
                {
                    Option[] changeList = new Option[GopetManager.TradeGiftPrice.Count];
                    for (sbyte i = 0; i < GopetManager.TradeGiftPrice.Count; i++)
                    {
                        var tradeGiftTemplate = GopetManager.TradeGiftPrice[i];
                        changeList[i] = new Option(tradeGiftTemplate.Item3, string.Format(player.Language.OptionTradeGift, getMoneyText((sbyte)tradeGiftTemplate.Item1[0], tradeGiftTemplate.Item2[0], player), getMoneyText((sbyte)tradeGiftTemplate.Item1[1], tradeGiftTemplate.Item2[1], player)));
                    }
                    showNpcOption(GopetManager.NPC_TIEN_NU, player, changeList);
                }
                break;
            case MENU_ITEM_MONEY_INVENTORY:
                {
                    showInventory(player, GopetManager.MONEY_INVENTORY, MENU_ITEM_MONEY_INVENTORY, player.Language.ItemYouHave);
                }
                break;
            case MENU_APPROVAL_CLAN_MEMBER:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        JArrayList<MenuItemInfo> approvalElements = new();
                        foreach (ClanRequestJoin clanRequestJoin in clan.getRequestJoin())
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(clanRequestJoin.name, string.Format(player.Language.ApplyToClanWhen, Utilities.GetDate(clanRequestJoin.timeRequest)), "", true);
                            menuItemInfo.setImgPath(clanRequestJoin.getAvatar());
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, clanRequestJoin.name));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setHasId(true);
                            menuItemInfo.setItemId(clanRequestJoin.user_id);
                            approvalElements.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.MemberReview, approvalElements);
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_UPGRADE_MEMBER_DUTY:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        JArrayList<MenuItemInfo> approvalElements = new();
                        foreach (ClanMember clanMemberSelect in clan.getMembers())
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(clanMemberSelect.name + "(" + player.Language.PositionDescription + clanMemberSelect.getDutyName(player) + ")", string.Format(player.Language.ClanFundDescription, Utilities.FormatNumber(clanMemberSelect.fundDonate)), "", true);
                            menuItemInfo.setImgPath(clanMemberSelect.getAvatar());
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, clanMemberSelect.name));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setHasId(true);
                            menuItemInfo.setItemId(clanMemberSelect.user_id);
                            approvalElements.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.MemberReview, approvalElements);
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_ADMIN_MAP:
                {
                    if (player.checkIsAdmin())
                    {
                        JArrayList<MenuItemInfo> mapMenuItem = new();
                        foreach (GopetMap gopetMap in MapManager.mapArr)
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(gopetMap.mapTemplate.getName(player) + "  (" + gopetMap.mapID + ")", "", "", true);
                            menuItemInfo.setImgPath("npcs/mgo.png");
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, gopetMap.mapTemplate.getName(player)));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            mapMenuItem.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.TeleToMap, mapMenuItem);
                    }
                }
                break;

            case MENU_DELETE_TIEM_NANG:
                {
                    JArrayList<MenuItemInfo> menuItemInfos = new();
                    foreach (String option in player.controller.gym_options)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(Utilities.Format(player.Language.Remove + " %s", option), string.Format(player.Language.DeleteTiemNangTitle, option, PriceDeleteTiemNang), "", true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, Utilities.Format(player.Language.Remove + " %s", option)));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.DeleteGymTitle, menuItemInfos);
                }
                break;
            case SHOP_ENERGY:
            case SHOP_WEAPON:
            case SHOP_HAT:
            case SHOP_SKIN:
            case SHOP_ARMOUR:
            case SHOP_FOOD:
            case SHOP_THUONG_NHAN:
            case SHOP_PET:
            case SHOP_ARENA:
                showShop((sbyte)menuId, player);
                break;
            case MENU_WING_INVENTORY:
                showInventory(player, GopetManager.WING_INVENTORY, menuId, player.Language.MyWing);
                break;
            case MENU_NORMAL_INVENTORY:
                showInventory(player, GopetManager.NORMAL_INVENTORY, menuId, player.Language.Inventory);
                break;
            case MENU_SKIN_INVENTORY:
                showInventory(player, GopetManager.SKIN_INVENTORY, menuId, player.Language.SkinInventory);
                break;
            case MENU_ADMIN_BUFF_DUNG_HỢP:
            case MENU_FUSION_MENU_EQUIP:
            case MENU_UNLOCK_ITEM_PLAYER:
            case MENU_LOCK_ITEM_PLAYER:
            case MENU_SELECT_ALL_ITEM_MERGE:
            case MENU_SELECT_ITEM_TO_GET_BY_ADMIN:
            case MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN:
            case MENU_SELECT_MATERIAL1_TO_ENCHANT_TATOO:
            case MENU_SELECT_MATERIAL2_TO_ENCHANT_TATOO:
            case MENU_SELECT_MATERIAL_TO_ENCAHNT_WING:
            case MENU_SELECT_ENCHANT_MATERIAL1:
            case MENU_SELECT_ENCHANT_MATERIAL2:
            case MENU_MERGE_PART_PET:
            case MENU_SELECT_ITEM_UP_SKILL:
            case MENU_SELECT_ITEM_PK:
            case MENU_SELECT_ITEM_PART_FOR_STAR_PET:
            case MENU_SELECT_ITEM_GEN_TATTO:
            case MENU_SELECT_ITEM_REMOVE_TATTO:
            case MENU_SELECT_ITEM_SUPPORT_PET:
            case MENU_SELECT_GEM_ENCHANT_MATERIAL2:
            case MENU_SELECT_GEM_ENCHANT_MATERIAL1:
            case MENU_SELECT_GEM_UP_TIER:
            case MENU_MERGE_PART_ITEM:
            case MENU_SELECT_GEM_TO_INLAY:
            case MENU_MERGE_WING:
                {
                    CopyOnWriteArrayList<Item> listItems = null;
                    switch (menuId)
                    {
                        case MENU_MERGE_PART_ITEM:
                            listItems = getItemByMenuId(menuId, player, (item) => GopetManager.itemTemplate[item.Template.itemOptionValue[0]].type != GopetManager.WING_ITEM);
                            break;
                        case MENU_MERGE_WING:
                            listItems = getItemByMenuId(menuId, player, (item) => GopetManager.itemTemplate[item.Template.itemOptionValue[0]].type == GopetManager.WING_ITEM);
                            break;
                        default:
                            listItems = getItemByMenuId(menuId, player);
                            break;
                    }
                    JArrayList<MenuItemInfo> menuItemMaterial1Infos = new();
                    foreach (Item item in listItems)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(item.getTemp().isStackable ? item.getName(player) : item.getEquipName(player), item.getDescription(player), item.getTemp().getIconPath(), true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, item.getName(player)));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(CloseScreenAfterClick(menuId));
                        menuItemMaterial1Infos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.SelectMaterial, menuItemMaterial1Infos);

                }
                break;
            case MENU_SELECT_ITEM_ADMIN:
                {
                    JArrayList<MenuItemInfo> menuItemInfos = new((ADMIN_INFOS));
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "MENU ADMIN", menuItemInfos);
                    menuItemInfos.Clear();
                    HistoryManager.addHistory(new History(player).setObj(player.playerData).setLog("Thao tác gửi giao diện ADMIN"));
                }
                break;
            case MENU_KIOSK_HAT_SELECT:
            case MENU_KIOSK_WEAPON_SELECT:
            case MENU_KIOSK_GEM_SELECT:
            case MENU_KIOSK_AMOUR_SELECT:
                {
                    CopyOnWriteArrayList<Item> listItems = Item.search(typeSelectItemMaterial(menuId, player), player.playerData.getInventoryOrCreate(menuId != MENU_KIOSK_GEM_SELECT ? GopetManager.EQUIP_PET_INVENTORY : GopetManager.GEM_INVENTORY));
                    JArrayList<MenuItemInfo> menuItemEquipInfos = new();
                    foreach (Item item in listItems)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(item.getEquipName(player), item.getDescription(player), item.getTemp().getIconPath(), true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, item.getName(player)));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemEquipInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.SelectEquip, menuItemEquipInfos);
                }
                break;
            case MENU_KIOSK_OHTER_SELECT:
                {
                    CopyOnWriteArrayList<Item> listItems = player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY);
                    JArrayList<MenuItemInfo> menuItemEquipInfos = new();
                    foreach (Item item in listItems)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(item.getName(player), item.getDescription(player), item.getTemp().getIconPath(), true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, item.getName(player)));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemEquipInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.SelectItem, menuItemEquipInfos);
                }
                break;
            case MENU_SELECT_ITEM_MERGE:
                {
                    if (player.playerData.IsMergeServer)
                    {
                        return;
                    }

                    if (player.controller.MergePlayerData == null)
                    {
                        player.okDialog("Đang truy vấn");
                        using (var conn = MYSQLManager.createOld())
                        {
                            PlayerData playerData = conn.QueryFirstOrDefault<PlayerData>("Select * from player where user_id = @user_id", new { user_id = player.user.user_id });
                            if (playerData != null)
                            {
                                player.controller.MergePlayerData = playerData;
                            }
                            else
                            {
                                player.redDialog("Ở máy chủ cũ bạn không có tài khoản này");
                                return;
                            }
                        }
                    }

                    JArrayList<Option> approvalOptions = new();
                    approvalOptions.add(new Option(0, "Chọn vật phẩm ở server cũ", 1));
                    approvalOptions.add(new Option(1, "Chọn thú cưng ở server cũ", 1));
                    approvalOptions.add(new Option(2, "Bỏ chọn tất cả", 1));
                    approvalOptions.add(new Option(3, "Gộp", 1));
                    player.controller.sendListOption(menuId, "Gộp đồ", "", approvalOptions);
                    break;
                }
            case MENU_OPTION_ADMIN_GET_ITEM:
                {
                    JArrayList<Option> adminOption = new();
                    adminOption.add(new Option(0, "Chọn", 1));
                    adminOption.add(new Option(1, "Bỏ chọn", 1));
                    adminOption.add(new Option(2, "Lấy vật phẩm", 1));
                    player.controller.sendListOption(menuId, "Admin", "", adminOption);
                }
                break;
            case MENU_OPTION_ADMIN_GIVE_ITEM:
                {
                    JArrayList<Option> adminOption = new();
                    adminOption.add(new Option(0, "Chọn", 1));
                    adminOption.add(new Option(1, "Bỏ chọn", 1));
                    adminOption.add(new Option(2, "Đưa vật phẩm", 1));
                    player.controller.sendListOption(menuId, "Admin", "", adminOption);
                }
                break;
            case MENU_APPROVAL_CLAN_MEM_OPTION:
                {
                    JArrayList<Option> approvalOptions = new();
                    approvalOptions.add(new Option(0, player.Language.ApplyMember, (sbyte)1));
                    approvalOptions.add(new Option(1, player.Language.Remove, (sbyte)1));
                    approvalOptions.add(new Option(2, player.Language.RemoveAndBlock, (sbyte)1));
                    approvalOptions.add(new Option(3, player.Language.RemoveAll, (sbyte)1));
                    player.controller.sendListOption(menuId, player.Language.MemberReview, "", approvalOptions);
                }
                break;
            case MENU_SELECT_TYPE_MONEY_TO_RENT_SKILL_CLAN:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_CLAN_SKILL_TEMPLATE_RENT))
                    {
                        ClanSkillTemplate clanSkillTemplate = player.controller.objectPerformed[OBJKEY_CLAN_SKILL_TEMPLATE_RENT];
                        JArrayList<Option> approvalOptions = new();
                        for (global::System.Int32 i = 0; i < clanSkillTemplate.moneyType.Length; i++)
                        {
                            approvalOptions.add(new Option(i, getMoneyText(clanSkillTemplate.moneyType[i], clanSkillTemplate.price[i], player), (sbyte)1));
                        }
                        player.controller.sendListOption(menuId, player.Language.RentSkillClan, "", approvalOptions);
                    }
                    else
                    {
                        player.fastAction();
                    }
                }
                break;
            case MENU_SELECT_SKILL_CLAN_TO_RENT:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        if (clan.SkillInfo.Count > 0)
                        {
                            JArrayList<MenuItemInfo> skillMenuItemInfos = new();
                            foreach (var skill in clan.SkillInfo)
                            {
                                if (skill.Value > 0)
                                {
                                    ClanSkillTemplate clanSkillTemplate = GopetManager.ClanSkillViaId[skill.Key];
                                    MenuItemInfo menuItemInfo = new MenuItemInfo(clanSkillTemplate.name, clanSkillTemplate.description, "npcs/gopet.png", true);
                                    menuItemInfo.setShowDialog(true);
                                    menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, menuItemInfo.getTitleMenu()));
                                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                    menuItemInfo.setCloseScreenAfterClick(true);
                                    skillMenuItemInfos.add(menuItemInfo);
                                }
                            }
                            player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.SelectClanSkillRent, skillMenuItemInfos);
                        }
                        else
                        {
                            player.redDialog(player.Language.RentSkillClanLaw);
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_PLUS_SKILL_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        JArrayList<MenuItemInfo> skillMenuItemInfos = new();
                        foreach (var clanSkill in GopetManager.clanSkillTemplateList.Where(p => clanMember.clan.lvl >= p.lvlClanRequire))
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(clanSkill.name, clanSkill.description, "npcs/gopet.png", true);
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, menuItemInfo.getTitleMenu()));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setCloseScreenAfterClick(true);
                            skillMenuItemInfos.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.AddSkillClanLevelTitle, skillMenuItemInfos);
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_INTIVE_CHALLENGE:
                {
                    JArrayList<Option> list = new();
                    for (int i = 0; i < GopetManager.PRICE_BET_CHALLENGE.Length; i++)
                    {
                        long l = GopetManager.PRICE_BET_CHALLENGE[i];
                        Option option = new Option(i, Utilities.FormatNumber(l) + " (ngoc)", 1);
                        list.add(option);
                    }
                    player.controller.sendListOption(menuId, player.Language.Bet, CMD_CENTER_OK, list);
                }
                break;
            case MENU_ATM:
                {
                    JArrayList<Option> options = new();
                    options.add(new Option(0, player.Language.Change + " (vang)"));
                    options.add(new Option(1, player.Language.Change + " (ngoc)"));
                    options.add(new Option(2, player.Language.Change + " (ngoc)"));
                    player.controller.sendListOption(menuId, "ATM", CMD_CENTER_OK, options);
                }
                break;
            case MENU_KIOSK_OHTER:
            case MENU_KIOSK_PET:
            case MENU_KIOSK_GEM:
            case MENU_KIOSK_AMOUR:
            case MENU_KIOSK_WEAPON:
            case MENU_KIOSK_HAT:
                {
                    MarketPlace marketPlace = (MarketPlace)player.getPlace();
                    Kiosk kiosk = null;
                    switch (menuId)
                    {
                        case MENU_KIOSK_HAT:
                            kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_HAT);
                            break;
                        case MENU_KIOSK_WEAPON:
                            kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_WEAPON);
                            break;
                        case MENU_KIOSK_AMOUR:
                            kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_AMOUR);
                            break;
                        case MENU_KIOSK_GEM:
                            kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_GEM);
                            break;
                        case MENU_KIOSK_PET:
                            kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_PET);
                            break;
                        case MENU_KIOSK_OHTER:
                            kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_OTHER);
                            break;
                        default:
                            {
                                return;
                            }
                    }
                    switch (menuId)
                    {
                        case MENU_KIOSK_GEM:
                        case MENU_KIOSK_OHTER:
                        case MENU_KIOSK_AMOUR:
                        case MENU_KIOSK_WEAPON:
                        case MENU_KIOSK_HAT:
                            {
                                JArrayList<MenuItemInfo> arrayListEquip = new();
                                foreach (SellItem kioskItem in kiosk.kioskItems)
                                {
                                    MenuItemInfo menuItemInfo = new MenuItemInfo();
                                    menuItemInfo.setCanSelect(true);
                                    menuItemInfo.setTitleMenu((menuId != MENU_KIOSK_OHTER ? kioskItem.ItemSell.getEquipName(player) : kioskItem.ItemSell.getName(player)) + string.Format(player.Language.KioskIdItemDescription, kioskItem.itemId));
                                    menuItemInfo.setImgPath(kioskItem.getFrameImgPath());
                                    menuItemInfo.setDesc(string.Format(player.Language.PriceKioskDescription + "(ngoc) ", Utilities.FormatNumber(kioskItem.MathPrice)) + kioskItem.ItemSell.getTemp().getDescription(player));
                                    menuItemInfo.setCloseScreenAfterClick(true);
                                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                    menuItemInfo.setHasId(true);
                                    menuItemInfo.setItemId(kioskItem.itemId);
                                    menuItemInfo.setPaymentOptions(new MenuItemInfo.PaymentOption[]{
                                new MenuItemInfo.PaymentOption(0, kioskItem.MathPrice + " (ngoc)", checkMoney(GopetManager.MONEY_TYPE_COIN, kioskItem.MathPrice, player) ? (sbyte) 1 : (sbyte) 0)
                            });
                                    arrayListEquip.add(menuItemInfo);
                                }
                                player.controller.showMenuItem(menuId, TYPE_MENU_PAYMENT, player.Language.Kiosk, arrayListEquip);
                            }
                            break;
                        case MENU_KIOSK_PET:
                            {
                                JArrayList<MenuItemInfo> arrayListEquip = new();
                                foreach (SellItem kioskItem in kiosk.kioskItems)
                                {
                                    MenuItemInfo menuItemInfo = new MenuItemInfo();
                                    menuItemInfo.setCanSelect(true);
                                    menuItemInfo.setTitleMenu(kioskItem.pet.getNameWithStar(player) + string.Format(player.Language.KioskIdItemDescription, kioskItem.itemId));
                                    menuItemInfo.setImgPath(kioskItem.pet.getPetTemplate().icon);
                                    menuItemInfo.setDesc(string.Format(player.Language.PriceKioskDescription + "(ngoc) ", Utilities.FormatNumber(kioskItem.MathPrice)) + kioskItem.pet.getDesc(player));
                                    menuItemInfo.setCloseScreenAfterClick(true);
                                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                    menuItemInfo.setHasId(true);
                                    menuItemInfo.setItemId(kioskItem.itemId);
                                    menuItemInfo.setPaymentOptions(new MenuItemInfo.PaymentOption[]{
                                    new MenuItemInfo.PaymentOption(0, kioskItem.MathPrice + " (ngoc)", checkMoney(GopetManager.MONEY_TYPE_COIN, kioskItem.MathPrice, player) ? (sbyte) 1 : (sbyte) 0)});
                                    arrayListEquip.add(menuItemInfo);
                                }
                                player.controller.showMenuItem(menuId, TYPE_MENU_PAYMENT, menuId == MENU_KIOSK_PET ? player.Language.KioskPet : player.Language.Kiosk, arrayListEquip);
                            }
                            break;
                    }
                }
                break;
            case MENU_SELECT_SLOT_USE_SKILL_CARD:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, "Ô 1"),
                        new Option(1, "Ô 2"),
                        new Option(2, "Ô 3"),
                        new Option(3, "Huỷ"),
                    };
                    player.controller.sendListOption(menuId, "Chọn ô kỹ năng để học kỹ năng", "", options);
                }
                break;
            case MENU_SELECT_ALL_PET_MERGE:
                if (!player.playerData.IsMergeServer && player.controller.MergePlayerData != null)
                {
                    CopyOnWriteArrayList<Pet> listPet = (CopyOnWriteArrayList<Pet>)player.controller.MergePlayerData.pets.clone();
                    JArrayList<MenuItemInfo> petItemInfos = new();
                    Pet p = player.controller.MergePlayerData.petSelected;
                    if (p != null)
                    {
                        listPet.add(0, p);
                    }
                    foreach (Pet pet in listPet)
                    {
                        MenuItemInfo menuItemInfo = new PetMenuItemInfo(pet, player);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(string.Format(player.Language.DoYouWantSelectItem, pet.getNameWithStar(player)));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        petItemInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, player.Language.YourPet, petItemInfos);
                }
                return;
            case MENU_OPTION_USE_FLOWER:
                {
                    int num = 1;
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_COUNT_USE_BÓ_HOA))
                    {
                        num = Math.Max(1, player.controller.objectPerformed[OBJKEY_COUNT_USE_BÓ_HOA]);
                    }
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, $"Tặng {Utilities.FormatNumber(TeacherDay2024.Data[0].Item1 * num) } (vang) và {Utilities.FormatNumber(TeacherDay2024.Data[0].Item2 * num)} boá hoa"),
                        new Option(1, $"Tặng {Utilities.FormatNumber(TeacherDay2024.Data[1].Item1  * num)} (ngoc) và {Utilities.FormatNumber(TeacherDay2024.Data[1].Item2 * num)} boá hoa"),
                        new Option(2, "Huỷ"),
                        new Option(3, "Nhập số lượng.(Số lần tặng hoa)"),
                    };
                    player.controller.sendListOption(menuId, "Chọn phương thức tặng hoa", "", options);
                }
                break;
            case MENU_OPTION_SHOW_FUSION_MENU:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, player.Language.FusionEquip),
                        new Option(1, player.Language.FusionPet),
                    };
                    player.controller.sendListOption(menuId, player.Language.FusionTitle, "", options);
                }
                break;
            case MENU_FUSION_EQUIP_OPTION:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, player.Language.SelectMainFusionItem),
                        new Option(1, player.Language.SelectDeputyFusionItem),
                        new Option(2, player.Language.FusionTitle),
                    };
                    player.controller.sendListOption(menuId, player.Language.FusionTitle, "", options);
                }
                break;
            case MENU_FUSION_EQUIP_OPTION_COMFIRM:
                {
                    if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_MAIN_ITEM_ID_FUSION, OBJKEY_DEPUTY_ITEM_ID_FUSION))
                    {
                        Item itemMain = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_MAIN_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                        Item itemDeputy = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_DEPUTY_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                        if (itemDeputy == null || itemMain == null)
                        {
                            player.redDialog(player.Language.ItemNotFound);
                            return;
                        }
                        if (itemDeputy == itemMain)
                        {
                            player.redDialog(player.Language.DuplicateItem);
                            return;
                        }
                        if (itemMain.NumFusion < GopetManager.FusionGOLD.Length)
                        {
                            JArrayList<Option> options = new JArrayList<Option>()
                            {
                                new Option(0, string.Format(player.Language.FusionEquipByGOLD, Utilities.FormatNumber(GopetManager.FusionData[GopetManager.MONEY_TYPE_GOLD][itemMain.NumFusion].Item1), GopetManager.FusionData[GopetManager.MONEY_TYPE_GOLD][itemMain.NumFusion].Item2, itemMain.NumFusion + 1)),
                                new Option(1, string.Format(player.Language.FusionEquipByGEM, Utilities.FormatNumber(GopetManager.FusionData[GopetManager.MONEY_TYPE_COIN][itemMain.NumFusion].Item1), GopetManager.FusionData[GopetManager.MONEY_TYPE_COIN][itemMain.NumFusion].Item2, itemMain.NumFusion + 1)),
                            };
                            player.controller.sendListOption(menuId, player.Language.FusionTitle, "", options);
                        }
                    }
                }
                break;
            case MENU_FUSION_PET_OPTION:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, player.Language.SelectMainFusionPet),
                        new Option(1, player.Language.SelectDeputyFusionPet),
                        new Option(2, player.Language.FusionTitle),
                    };
                    player.controller.sendListOption(menuId, player.Language.FusionTitle, "", options);
                }
                break;
            case MENU_FUSION_PET_OPTION_COMFIRM:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, string.Format(player.Language.FusionPetGold, Utilities.FormatNumber(GopetManager.FusionPetPrice[0]))),
                        new Option(1, string.Format(player.Language.FusionPetGem, Utilities.FormatNumber(GopetManager.FusionPetPrice[1]))),
                    };
                    player.controller.sendListOption(menuId, player.Language.FusionTitle, "", options);
                }
                break;
            case MENU_OPTION_PET_REINCARNATION:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, string.Format(player.Language.ReincarnationPetGold, Utilities.FormatNumber(GopetManager.ReincarnationPetPrice[0]))),
                        new Option(1, string.Format(player.Language.ReincarnationPetGem, Utilities.FormatNumber(GopetManager.ReincarnationPetPrice[1]))),
                    };
                    player.controller.sendListOption(menuId, "Trùng sinh", "", options);
                }
                break;
            case MENU_OPTION_KIOSK:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, "Tiếp tục treo"),
                        new Option(1, string.Format("Tiếp tục treo và chỉ định người mua (Thú cưng {0} (vang), còn lại {1} (vang))", Utilities.FormatNumber(GopetManager.PRICE_ASSIGNED_PET), Utilities.FormatNumber(GopetManager.PRICE_ASSIGNED_ITEM))),
                        new Option(2, "Huỷ"),
                    };
                    player.controller.sendListOption(menuId, "Tuỳ chọn", "", options);
                }
                break;
            case MENU_OPTION_KIOSK_CANCEL_ITEM:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, "Huỷ bán vật phẩm này"),
                        new Option(1, "Đổi người chỉ định"),
                        new Option(2, "Cho phép người chơi khác mua lẻ món này"),
                        new Option(3, "Đóng"),
                    };
                    player.controller.sendListOption(menuId, "Tuỳ chọn", "", options);
                }
                break;
            case MENU_OPTION_BUY_KIOSK_ITEM:
                {
                    JArrayList<Option> options = new JArrayList<Option>()
                    {
                        new Option(0, "Mua tất cả"),
                        new Option(1, "Mua lẻ"),
                        new Option(2, "Đóng"),
                    };
                    player.controller.sendListOption(menuId, "Tuỳ chọn", "", options);
                }
                break;

        }
    }
}


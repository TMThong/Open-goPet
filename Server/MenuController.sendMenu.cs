

using Gopet.Battle;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.Dialog;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySql.Data.MySqlClient;
using Gopet.Data.item;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Gopet.Data.top;
using Gopet.Data.dialog;
using Gopet.Data.Clan;

public partial class MenuController
{
    public static void sendMenu(int menuId, Player player)
    {
        switch (menuId)
        {
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
                        MenuItemInfo menuItemInfo = new PetMenuItemInfo(pet);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", pet.getNameWithStar()));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        petItemInfos.add(menuItemInfo);
                        menuItemInfo.setItemId(i);
                        menuItemInfo.setHasId(true);
                        if (i == -1)
                        {
                            menuItemInfo.setTitleMenu(menuItemInfo.getTitleMenu() + " (Đang sử dụng)");
                        }
                        i++;
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Pet của bạn", petItemInfos);
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
                                PetMenuItemInfo petMenuItemInfo = new PetMenuItemInfo(GopetManager.PETTEMPLATE_HASH_MAP.get(petId));
                                petMenuItemInfo.setCloseScreenAfterClick(true);
                                petMenuItemInfo.setShowDialog(true);
                                petMenuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn nó không?"));
                                petMenuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                petMenus.add(petMenuItemInfo);
                            }
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Quà nhiệm vụ", petMenus);
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
                        MenuItemInfo menuItemInfo = new MenuItemInfo(tatto.name, $"{tatto.atk} (atk) {tatto.def}(def) {tatto.hp}(hp) {tatto.mp}(mp)", tatto.iconPath, false);
                        menuInfos.add(menuItemInfo);
                    }

                    player.controller.showMenuItem(menuId, TYPE_MENU_NONE, "Tất cả xăm", menuInfos);
                    break;
                }
            case MENU_ADMIN_SHOW_ALL_ACHIEVEMENT:
                {
                    if (player.checkIsAdmin())
                    {
                        JArrayList<MenuItemInfo> menuInfos = new();
                        foreach (var ach in GopetManager.achievements)
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(ach.Name, ach.Description, ach.IconPath, true);
                            menuInfos.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Danh hiệu", menuInfos);
                    }
                    break;
                }
            case MENU_USE_ACHIEVEMNT:
                {
                    JArrayList<MenuItemInfo> menuInfos = new();
                    foreach (var ach in player.playerData.achievements)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(ach.Template.Name, ach.Template.Description, ach.Template.IconPath, true);
                        menuInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Danh hiệu", menuInfos);
                }
                break;
            case MENU_USE_ACHIEVEMNT_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, "Sử dụng", 1));
                    if (player.playerData.CurrentAchievementId > 0)
                    {
                        list.Add(new Option(1, "Tháo danh hiệu hiện tại", 1));
                    }
                    player.controller.sendListOption(menuId, "Sử dụng danh hiệu", CMD_CENTER_OK, list);
                }
                break;
            case MENU_ME_SHOW_ACHIEVEMENT:
                {
                    AnimationMenu animationMenu = new AnimationMenu("Danh hiệu");
                    animationMenu.Commands.Add(AnimationMenu.RightExitCMD);
                    foreach (var ach in player.playerData.achievements)
                    {
                        animationMenu.AddLabel(0, $"Danh hiệu: {ach.Template.Name}", FontStyle.SMALL);
                        animationMenu.AddImage(0, ach.Template.FramePath, ach.Template.FrameNum);
                        animationMenu.AddLabel(0, $"Chỉ số: {ach.Template.Atk} (atk) {ach.Template.Def} (def) {ach.Template.Hp} (hp) {ach.Template.Mp} (mp)", FontStyle.SMALL);
                        animationMenu.AddLabel(0, $"Hạn sử dụng: {(ach.Expire == null ? "Vĩnh viễn" : Utilities.ToDateString(ach.Expire.Value))}", FontStyle.SMALL);
                    }
                    player.controller.showAnimationMenu(menuId, animationMenu);
                }
                break;
            case MENU_SHOW_ALL_ITEM:
                {
                    JArrayList<MenuItemInfo> menuInfos = new();

                    foreach (ItemTemplate itemTemplate in GopetManager.NonAdminItemList)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(itemTemplate.getNameViaType() + "((chienluc)" + itemTemplate.getItemId() + ")", itemTemplate.getDescriptionViaType(), itemTemplate.getIconPath(), false);
                        menuInfos.add(menuItemInfo);
                    }

                    player.controller.showMenuItem(menuId, TYPE_MENU_NONE, "Tất cả vật phẩm", menuInfos);
                }
                break;
            case MENU_LIST_REQUEST_ADD_FRIEND:
            case MENU_LIST_BLOCK_FRIEND:
            case MENU_LIST_FRIEND:
                {
                    IEnumerable<int> ints = null;
                    switch(menuId)
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
                    if(ints.Any())
                    {
                        using (var conn = MYSQLManager.create())
                        {
                            var friendQuery = conn.Query($"SELECT  `name`, `avatarPath`, `LastTimeOnline` FROM `player` WHERE `player`.`user_id` IN ({ints.ToArray().Join(",")})");
                            foreach (var item in friendQuery)
                            {
                                DateTime LastTimeOnline = item.LastTimeOnline;
                                MenuItemInfo menuItemInfo = new MenuItemInfo(item.name, $"Lần cuối online: {Utilities.ToDateString(LastTimeOnline)}", item.avatarPath, true);
                                menuItemInfo.setCloseScreenAfterClick(true);
                                menuInfos.add(menuItemInfo);
                            }
                        }
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_NONE, "Danh sách bạn bè", menuInfos);
                }
                break;
            case MENU_LIST_FRIEND_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, "Xem vị trí", true));  
                    list.Add(new Option(1, "Xóa", true));  
                    list.Add(new Option(2, "Xóa và chặn", true));  
                    player.controller.sendListOption(menuId, "Bạn bè", string.Empty, list);
                }
                break;
            case MENU_LIST_REQUEST_ADD_FRIEND_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, "Chấp nhận", true));
                    list.Add(new Option(1, "Từ chối", true));
                    list.Add(new Option(3, "Từ chối tất cả", true));
                    list.Add(new Option(2, "Từ chối và thêm vào sổ đen", true));
                    player.controller.sendListOption(menuId, "Danh sách chờ thêm bạn", string.Empty, list);
                }
                break;
            case MENU_LIST_BLOCK_FRIEND_OPTION:
                {
                    JArrayList<Option> list = new();
                    list.Add(new Option(0, "Bỏ chặn", true));
                    list.Add(new Option(1, "Bỏ chặn tất cả", true));
                    player.controller.sendListOption(menuId, "Sổ đen", string.Empty, list);
                }
                break;
            case MENU_UNEQUIP_SKIN:
            case MENU_UNEQUIP_PET:
                {
                    JArrayList<Option> list = new();
                    list.add(new Option(0, menuId == MENU_UNEQUIP_PET ? "Không cho thú cưng đi theo" : "Tháo", Option.CAN_SELECT));
                    String titleStr = "";
                    switch (menuId)
                    {
                        case MENU_UNEQUIP_PET:
                            titleStr = "Tùy chọn với thú cưng đi theo bạn";
                            break;
                        case MENU_UNEQUIP_SKIN:
                            titleStr = "Tùy chọn với trang phục đang mặc của bạn";
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
                        MenuItemInfo menuItemInfo = new MenuItemInfo(taskTemplate.getName(), taskTemplate.getDescription(), "dialog/1.png", true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(TaskCalculator.getTaskText(taskData.task, taskData.taskInfo, taskData.timeTask));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        taskMenuInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Nhiệm vụ của bạn", taskMenuInfos);
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

                            player.controller.sendListOption(MENU_SELECT_MONEY_TO_PAY_FOR_ENCHANT_WING, "Thanh toán cường hóa", "", list);
                        }
                    }
                }
                break;
            case MENU_OPTION_TASK:
                {
                    JArrayList<Option> list = new();

                    list.add(new Option(0, "Cập nhật", Option.CAN_SELECT));
                    list.add(new Option(1, "Hoàn thành nhiệm vụ", Option.CAN_SELECT));
                    list.add(new Option(2, "Hủy nhiệm vụ", Option.CAN_SELECT));

                    player.controller.sendListOption(MENU_OPTION_TASK, "Tùy chọn nhiệm vụ", "", list);
                }
                break;
            case MENU_SELECT_TYPE_PAYMENT_TO_ARENA_JOURNALISM:
            case MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO:
                {
                    JArrayList<Option> list = new();

                    list.add(new Option(0, $"{(menuId == MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO ? GopetManager.PRICE_GOLD_ENCHANT_TATTO : GopetManager.PRICE_GOLD_ARENA_JOURNALISM)} (vang)", Option.CAN_SELECT));
                    list.add(new Option(1, $"{(menuId == MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO ? GopetManager.PRICE_COIN_ENCHANT_TATTO : GopetManager.PRICE_COIN_ARENA_JOURNALISM)} (ngoc)", Option.CAN_SELECT));

                    player.controller.sendListOption(menuId, "Tùy chọn phương thức thanh toán", "", list);
                    break;
                }
            case MENU_MONEY_DISPLAY_SETTING:
                {
                    JArrayList<Option> list = new();
                    list.add(new Option(0, "Ghim", Option.CAN_SELECT));
                    list.add(new Option(1, "Bỏ ghim", Option.CAN_SELECT));
                    player.controller.sendListOption(MENU_MONEY_DISPLAY_SETTING, "Thao tác", "", list);
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
                                MenuItemInfo menuItemInfo = new MenuItemInfo(taskTemplate.getName(), taskTemplate.getDescription(), "dialog/1.png", true);
                                menuItemInfo.setShowDialog(true);
                                menuItemInfo.setDialogText("Bạn có chắc muốn nhận nhiệm vụ này ?" + TaskCalculator.getTaskText(null, taskTemplate.getTask(), taskTemplate.getTimeTask()));
                                menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                menuItemInfo.setCloseScreenAfterClick(true);
                                taskMenuInfos.add(menuItemInfo);
                            }
                            player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Nhiệm vụ", taskMenuInfos);
                        }
                        else
                        {
                            player.redDialog("Không có nhiệm vụ để cho bạn nhận");
                        }
                    }
                }
                break;
            case MENU_LIST_PET_FREE:
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Nhận pet miễn phí", PetFreeList);
                break;
            case MENU_LEARN_NEW_SKILL:
                {
                    if (player.playerData.petSelected != null)
                    {
                        JArrayList<MenuItemInfo> skillMenuItem = new();
                        foreach (PetSkill petSkill in getPetSkills(player))
                        {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(petSkill.name, petSkill.description, petSkill.skillID + "", true);
                            menuItemInfo.setImgPath(Utilities.Format("skills/skill_%s.png", petSkill.skillID));
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", petSkill.name));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            skillMenuItem.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Học kỹ năng cho thú cưng", skillMenuItem);

                    }
                }
                break;
            case MENU_EXCHANGE_GOLD:
                {
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, Utilities.Format("Đổi (vang) (Bạn hiện có: %svnđ)", Utilities.FormatNumber(player.user.getCoin())), EXCHANGE_ITEM_INFOS);
                }
                break;
            case MENU_SELECT_TYPE_CHANGE_GIFT:
                {
                    Option[] changeList = new Option[GopetManager.TradeGiftPrice.Count];
                    for (sbyte i = 0; i < GopetManager.TradeGiftPrice.Count; i++)
                    {
                        var tradeGiftTemplate = GopetManager.TradeGiftPrice[i];
                        changeList[i] = new Option(tradeGiftTemplate.Item3, $"Dùng {getMoneyText((sbyte)tradeGiftTemplate.Item1[0], tradeGiftTemplate.Item2[0])} và {getMoneyText((sbyte)tradeGiftTemplate.Item1[1], tradeGiftTemplate.Item2[1])} để đổi phần thưởng");
                    }
                    showNpcOption(GopetManager.NPC_TIEN_NU, player, changeList);
                }
                break;
            case MENU_ITEM_MONEY_INVENTORY:
                {
                    showInventory(player, GopetManager.MONEY_INVENTORY, MENU_ITEM_MONEY_INVENTORY, "Vật phẩm hiện có");
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
                            MenuItemInfo menuItemInfo = new MenuItemInfo(clanRequestJoin.name, Utilities.Format("Xin vào bang lúc: %s", Utilities.GetDate(clanRequestJoin.timeRequest)), "", true);
                            menuItemInfo.setImgPath(clanRequestJoin.getAvatar());
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", clanRequestJoin.name));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setHasId(true);
                            menuItemInfo.setItemId(clanRequestJoin.user_id);
                            approvalElements.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Duyệt thành viên", approvalElements);
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
                            MenuItemInfo menuItemInfo = new MenuItemInfo(clanMemberSelect.name + "(Chức vụ: " + clanMemberSelect.getDutyName() + ")", Utilities.Format("Đóng góp quỹ :%s", Utilities.FormatNumber(clanMemberSelect.fundDonate)), "", true);
                            menuItemInfo.setImgPath(clanMemberSelect.getAvatar());
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", clanMemberSelect.name));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setHasId(true);
                            menuItemInfo.setItemId(clanMemberSelect.user_id);
                            approvalElements.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Duyệt thành viên", approvalElements);
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
                            MenuItemInfo menuItemInfo = new MenuItemInfo(gopetMap.mapTemplate.name + "  (" + gopetMap.mapID + ")", "", "", true);
                            menuItemInfo.setImgPath("npcs/mgo.png");
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", gopetMap.mapTemplate.name));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            mapMenuItem.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Dịch chuyển tới map", mapMenuItem);
                    }
                }
                break;

            case MENU_DELETE_TIEM_NANG:
                {
                    JArrayList<MenuItemInfo> menuItemInfos = new();
                    foreach (String option in gym_options)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(Utilities.Format("Tẩy %s", option), Utilities.Format("Xóa 1 %s với giá %s (vang) và sẽ nhận lại 1 tiềm năng", option, PriceDeleteTiemNang), "", true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", Utilities.Format("Tẩy %s", option)));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Tẩy gym", menuItemInfos);
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
                showInventory(player, GopetManager.WING_INVENTORY, menuId, "Cánh của tôi");
                break;
            case MENU_NORMAL_INVENTORY:
                showInventory(player, GopetManager.NORMAL_INVENTORY, menuId, "Hành trang");
                break;
            case MENU_SKIN_INVENTORY:
                showInventory(player, GopetManager.SKIN_INVENTORY, menuId, "Tủ quần ảo");
                break;
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
                        MenuItemInfo menuItemInfo = new MenuItemInfo(item.getTemp().isStackable ? item.getName() : item.getEquipName(), item.getDescription(player), item.getTemp().getIconPath(), true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", item.getName()));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemMaterial1Infos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn nguyên liệu", menuItemMaterial1Infos);

                }
                break;
            case MENU_SELECT_ITEM_ADMIN:
                {
                    JArrayList<MenuItemInfo> menuItemInfos = new((ADMIN_INFOS));
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "MENU ADMIN", menuItemInfos);
                    menuItemInfos.Clear();
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
                        MenuItemInfo menuItemInfo = new MenuItemInfo(item.getEquipName(), item.getDescription(player), item.getTemp().getIconPath(), true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", item.getName()));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemEquipInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn trang bị", menuItemEquipInfos);
                }
                break;
            case MENU_KIOSK_OHTER_SELECT:
                {
                    CopyOnWriteArrayList<Item> listItems = player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY);
                    JArrayList<MenuItemInfo> menuItemEquipInfos = new();
                    foreach (Item item in listItems)
                    {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(item.getName(), item.getDescription(player), item.getTemp().getIconPath(), true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", item.getName()));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        menuItemEquipInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn vật phẩm", menuItemEquipInfos);
                }
                break;
            case MENU_APPROVAL_CLAN_MEM_OPTION:
                {
                    JArrayList<Option> approvalOptions = new();
                    approvalOptions.add(new Option(0, "Duyệt", (sbyte)1));
                    approvalOptions.add(new Option(1, "Xóa", (sbyte)1));
                    approvalOptions.add(new Option(2, "Xóa và cho vào danh sách chặn", (sbyte)1));
                    approvalOptions.add(new Option(3, "Xóa tất cả", (sbyte)1));
                    player.controller.sendListOption(menuId, "Duyệt thành viên", "", approvalOptions);
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
                            approvalOptions.add(new Option(i, getMoneyText(clanSkillTemplate.moneyType[i], clanSkillTemplate.price[i]), (sbyte)1));
                        }
                        player.controller.sendListOption(menuId, "Thuê kỹ năng bang hội", "", approvalOptions);
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
                                    menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", menuItemInfo.getTitleMenu()));
                                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                    menuItemInfo.setCloseScreenAfterClick(true);
                                    skillMenuItemInfos.add(menuItemInfo);
                                }
                            }
                            player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn kỹ năng bang hội", skillMenuItemInfos);
                        }
                        else
                        {
                            player.redDialog("Bang hội bạng chưa có kỹ năng nào\nVui lòng cộng tiềm năng vào kỹ năng để mở khóa");
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
                            menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", menuItemInfo.getTitleMenu()));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setCloseScreenAfterClick(true);
                            skillMenuItemInfos.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Tăng kỹ năng bang hội", skillMenuItemInfos);
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
                        Option option = new Option(i, Utilities.FormatNumber(l) + " ngọc", 1);
                        list.add(option);
                    }
                    player.controller.sendListOption(menuId, "Đặt cược", CMD_CENTER_OK, list);
                }
                break;
            case MENU_ATM:
                {
                    JArrayList<Option> options = new();
                    options.add(new Option(0, "Đổi (vang)"));
                    options.add(new Option(1, "Đổi (ngoc)"));
                    player.controller.sendListOption(menuId, "Cây ATM", CMD_CENTER_OK, options);
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
                                    menuItemInfo.setTitleMenu((menuId != MENU_KIOSK_OHTER ? kioskItem.ItemSell.getEquipName() : kioskItem.ItemSell.getName()) + Utilities.Format(" (Mã định danh:%s)", kioskItem.itemId));
                                    menuItemInfo.setImgPath(kioskItem.getFrameImgPath());
                                    menuItemInfo.setDesc(Utilities.Format("Giá %s (ngoc) ", Utilities.FormatNumber(kioskItem.price)) + kioskItem.ItemSell.getTemp().getDescription());
                                    menuItemInfo.setCloseScreenAfterClick(true);
                                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                    menuItemInfo.setHasId(true);
                                    menuItemInfo.setItemId(kioskItem.itemId);
                                    menuItemInfo.setPaymentOptions(new MenuItemInfo.PaymentOption[]{
                                new MenuItemInfo.PaymentOption(0, kioskItem.price + " (ngoc)", checkMoney(GopetManager.MONEY_TYPE_COIN, kioskItem.price, player) ? (sbyte) 1 : (sbyte) 0)
                            });
                                    arrayListEquip.add(menuItemInfo);
                                }
                                player.controller.showMenuItem(menuId, TYPE_MENU_PAYMENT, "Ki ốt", arrayListEquip);
                            }
                            break;
                        case MENU_KIOSK_PET:
                            {
                                JArrayList<MenuItemInfo> arrayListEquip = new();
                                foreach (SellItem kioskItem in kiosk.kioskItems)
                                {
                                    MenuItemInfo menuItemInfo = new MenuItemInfo();
                                    menuItemInfo.setCanSelect(true);
                                    menuItemInfo.setTitleMenu(kioskItem.pet.getNameWithStar() + Utilities.Format(" (Mã định danh:%s)", kioskItem.itemId));
                                    menuItemInfo.setImgPath(kioskItem.pet.getPetTemplate().icon);
                                    menuItemInfo.setDesc(Utilities.Format("Giá %s (ngoc) ", Utilities.FormatNumber(kioskItem.price)) + kioskItem.pet.getDesc());
                                    menuItemInfo.setCloseScreenAfterClick(true);
                                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                                    menuItemInfo.setHasId(true);
                                    menuItemInfo.setItemId(kioskItem.itemId);
                                    menuItemInfo.setPaymentOptions(new MenuItemInfo.PaymentOption[]{
                                    new MenuItemInfo.PaymentOption(0, kioskItem.price + " (ngoc)", checkMoney(GopetManager.MONEY_TYPE_COIN, kioskItem.price, player) ? (sbyte) 1 : (sbyte) 0)});
                                    arrayListEquip.add(menuItemInfo);
                                }
                                player.controller.showMenuItem(menuId, TYPE_MENU_PAYMENT, menuId == MENU_KIOSK_PET ? "Chợ pet" : "Ki ốt", arrayListEquip);
                            }
                            break;
                    }
                }
                break;
        }
    }
}


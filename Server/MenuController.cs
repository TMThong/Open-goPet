 
public class MenuController {

    /**
     * Danh sách nhận pet miễn phí
     */
    public const int MENU_LIST_PET_FREE = 0;

    /**
     * Túi pet
     */
    public const int MENU_PET_INVENTORY = 5;

    /**
     * Học kỹ năng mới
     */
    public const int MENU_LEARN_NEW_SKILL = 799;

    /**
     * Tẩy tiềm năng
     */
    public const int MENU_DELETE_TIEM_NANG = 800;
    public const int MENU_WING_INVENTORY = 801;
    public const int MENU_NORMAL_INVENTORY = 802;
    public const int MENU_SKIN_INVENTORY = 803;
    public const int MENU_SELECT_PET_UPGRADE_ACTIVE = 804;
    public const int MENU_SELECT_PET_UPGRADE_PASSIVE = 805;
    public const int MENU_KIOSK_HAT = 806;
    public const int MENU_KIOSK_WEAPON = 807;
    public const int MENU_KIOSK_AMOUR = 808;
    public const int MENU_KIOSK_GEM = 809;
    public const int MENU_KIOSK_PET = 810;
    public const int MENU_KIOSK_OHTER = 811;
    public const int MENU_SELECT_ENCHANT_MATERIAL1 = 1000;
    public const int MENU_SELECT_ENCHANT_MATERIAL2 = 1001;
    public const int MENU_EQUIP_PET_INVENTORY = 1002;
    public const int MENU_SELECT_EQUIP_PET_TIER = 1003;
    public const int MENU_MERGE_PART_PET = 1004;
    public const int MENU_SELECT_ITEM_UP_SKILL = 1005;
    public const int MENU_KIOSK_HAT_SELECT = 1006;
    public const int MENU_KIOSK_WEAPON_SELECT = 1007;
    public const int MENU_KIOSK_AMOUR_SELECT = 1008;
    public const int MENU_KIOSK_GEM_SELECT = 1009;
    public const int MENU_KIOSK_PET_SELECT = 1010;
    public const int MENU_KIOSK_OHTER_SELECT = 1011;
    public const int MENU_SELECT_ITEM_PK = 1012;
    public const int MENU_SELECT_ITEM_PART_FOR_STAR_PET = 1013;
    public const int MENU_SELECT_ITEM_GEN_TATTO = 1014;
    public const int MENU_SELECT_ITEM_REMOVE_TATTO = 1015;
    public const int MENU_SELECT_ITEM_SUPPORT_PET = 1016;
    public const int MENU_SELECT_ITEM_ADMIN = 1017;
    public const int MENU_SELECT_GEM_ENCHANT_MATERIAL2 = 1018;
    public const int MENU_SELECT_GEM_ENCHANT_MATERIAL1 = 1020;
    public const int MENU_ADMIN_MAP = 1019;
    public const int MENU_SELECT_GEM_UP_TIER = 1021;
    public const int MENU_MERGE_PART_ITEM = 1022;
    public const int MENU_SELECT_GEM_TO_INLAY = 1023;
    public const int MENU_APPROVAL_CLAN_MEMBER = 1024;
    public const int MENU_APPROVAL_CLAN_MEM_OPTION = 1025;
    public const int MENU_SELECT_TYPE_CHANGE_GIFT = 1026;
    public const int MENU_LIST_GIFT = 1027;
    public const int MENU_UPGRADE_MEMBER_DUTY = 1028;
    public const int MENU_SELECT_TYPE_UPGRADE_DUTY = 1029;
    public const int MENU_SELECT_SKILL_CLAN_TO_RENT = 1030;
    public const int MENU_PLUS_SKILL_CLAN = 1031;
    public const int MENU_INTIVE_CHALLENGE = 1032;
    public const int MENU_SHOW_LIST_TASK = 1033;
    public const int MENU_SHOW_MY_LIST_TASK = 1034;
    public const int MENU_OPTION_TASK = 1035;
    public const int MENU_UNEQUIP_PET = 1036;
    public const int MENU_UNEQUIP_WING = 1037;
    public const int MENU_UNEQUIP_SKIN = 1038;
    public const int MENU_ATM = 1039;
    public const int MENU_EXCHANGE_GOLD = 1040;
    public const int MENU_SHOW_ALL_ITEM = 1041;
    public const int MENU_SHOW_ALL_PLAYER_HAVE_ITEM_LVL_10 = 1042;
    public static MenuItemInfo[] ADMIN_INFOS = new MenuItemInfo[]{
        new AdminItemInfo("Đặt chỉ số pet đang đi theo", "Đặt chỉ số cho pet đi theo", "items/4000766.png"),
        new AdminItemInfo("Dịch chuyển đến người chơi", "Dịch chuyển đến người chơi chỉ định", "items/4000766.png"),
        new AdminItemInfo("Số lượng người chơi online", "Lấy tất cả số lượng người chơi online", "items/4000766.png"),
        new AdminItemInfo("Sô người chơi trong map này", "Lấy số người chơi trong map này", "items/4000766.png"),
        new AdminItemInfo("Dịch chuyển tới map", "Dịch chuyển tới map chỉ định", "items/4000766.png"),
        new AdminItemInfo("Khóa tài khoản", "Khóa tài khoản người chơi", "items/4000766.png"),
        new AdminItemInfo("Gỡ khóa tài khoản", "Gỡ khóa tài khoản người chơi", "items/4000766.png"),
        new AdminItemInfo("Chat thế giới", "Ghi nội dung và nó sẽ ở trên banner", "items/4000766.png"),
        new AdminItemInfo("Xem lịch sử", "Ghi tên nội dung và cột mốc lịch sử", "items/4000766.png"),
        new AdminItemInfo("Lấy pet", "Chọn 1 pet hiện có trên máy chủ nó sẽ vào hành trang", "items/4000766.png"),
        new AdminItemInfo("Lấy vật phẩm", "Tùy loại mà có thẻ lấy vật phẩm từ máy chủ", "items/4000766.png"),
        new AdminItemInfo("Xem còn bao nhiêu quái sẽ xuất hiện boss", "Hiển thị thông tin cần thiết về tình trạng boss ở khu vực", "items/4000766.png"),
        new AdminItemInfo("Thêm vàng", "Thêm vàng vào người chơi chỉ định đang online", "items/4000766.png"),
        new AdminItemInfo("Thêm ngọc", "Thêm vàng vào người chơi chỉ định đang online", "items/4000766.png"),
        new AdminItemInfo("Tìm người chơi có trang bị cấp 10", "Máy chủ sẽ trả về danh sách người chơi có vật phẩm trang bị cấp 10", "items/4000766.png"),
        new AdminItemInfo("Buff đập đồ", "Người được chỉ định buff sẽ không cường hóa bị thất bại", "items/4000766.png")
    };

    public const int ADMIN_INDEX_SET_PET_INFO = 0;
    public const int ADMIN_INDEX_TELE_TO_PLAYER = 1;
    public const int ADMIN_INDEX_COUNT_PLAYER = 2;
    public const int ADMIN_INDEX_COUNT_OF_MAP = 3;
    public const int ADMIN_INDEX_TELE_TO_MAP = 4;
    public const int ADMIN_INDEX_BAN_PLAYER = 5;
    public const int ADMIN_INDEX_UNBAN_PLAYER = 6;
    public const int ADMIN_INDEX_SHOW_BANNER = 7;
    public const int ADMIN_INDEX_SHOW_HISTORY = 8;
    public const int ADMIN_INDEX_SELECT_PET = 9;
    public const int ADMIN_INDEX_SELECT_ITEM = 10;
    public const int ADMIN_INDEX_GET_BOSS_PLACE = 11;
    public const int ADMIN_INDEX_ADD_GOLD = 12;
    public const int ADMIN_INDEX_ADD_COIN = 13;
    public const int ADMIN_INDEX_FIND_ITEM_LVL_10 = 14;
    public const int ADMIN_INDEX_BUFF_ENCHANT = 15;

    /**
     * Danh sách nhận pet miễn phí
     */
    public static ArrayList<  MenuItemInfo> PetFreeList = new();

    public static ArrayList<  MenuItemInfo> EXCHANGE_ITEM_INFOS = new();

    /**
     * Giá tẩy tiềm năng
     */
    public static long PriceDeleteTiemNang = 2;

    /**
     * Menu không có center dialog thì dùng mới OK Nếu mà có center dialog nó
     * kia chọn OK thì client sẽ không gửi về server
     */
    public const sbyte TYPE_MENU_NONE = 0;

    /**
     * Loại chọn
     */
    public const sbyte TYPE_MENU_SELECT_ELEMENT = 2;

    /**
     * Loại thanh toán
     */
    public const sbyte TYPE_MENU_PAYMENT = 3;

    /**
     * Tùy chòn luyện chỉ số
     */
    public const String[] gym_options = new String[]{"sức mạnh (str)", "tốc độ (agi)", "thông minh (int)",};

    public const int OP_MAIN_TASK = 0;

    /**
     * Tùy chọn nhận pet miễn phí
     */
    public const int OP_LIST_PET_FREE = 1;

    /**
     * Tùy chọn tẩy tiềm năng
     */
    public const int OP_DELETE_TIEM_NANG = 24;
    public const int OP_SHOP_PET = 2;
    public const int OP_TOP_PET = 3;
    public const int OP_TOP_GOLD = 4;
    public const int OP_CHANGE_GIFT = 5;
    public const int OP_LIST_GIFT = 6;
    public const int OP_UPGRADE_PET = 7;
    public const int OP_UPGRADE_STAR_PET = 8;
    public const int OP_CHALLENGE = 10;
    public const int OP_SHOP_ARENA = 11;
    public const int OP_MERGE_PART_PET = 15;
    public const int OP_PET_TATOO = 16;
    public const int OP_MERGE_ITEM = 19;
    public const int OP_SHOW_GEM_INVENTORY = 21;
    public const int OP_REVIVAL_PET_AFTER_PK = 22;
    public const int OP_KIOSK_HAT = 29;
    public const int OP_KIOSK_WEAPON = 31;
    public const int OP_KIOSK_AMOUR = 33;
    public const int OP_KIOSK_GEM = 35;
    public const int OP_KIOSK_PET = 37;
    public const int OP_KIOSK_OHTER = 39;
    public const int OP_OWNER_KIOSK_HAT = 30;
    public const int OP_OWNER_KIOSK_WEAPON = 32;
    public const int OP_OWNER_KIOSK_AMOUR = 34;
    public const int OP_OWNER_KIOSK_GEM = 36;
    public const int OP_OWNER_KIOSK_PET = 38;
    public const int OP_OWNER_KIOSK_OHTER = 40;
    public const int OP_SHOP_THUONG_NHAN_AND_XOA_XAM = 20;
    public const int OP_TOP_GEM = 41;
    public const int OP_GET_TASK_CLAN = 42;
    public const int OP_RESULT_TASK_CLAN = 43;
    public const int OP_ENTER_CLAN_PLACE = 44;
    public const int OP_TOP_LVL_CLAN = 45;
    public const int OP_CREATE_CLAN = 46;
    public const int OP_EVENT_OF_CLAN = 47;
    public const int OP_FAST_INFO_CLAN = 48;
    public const int OP_SHOP_CLAN = 49;
    public const int OP_UPGRADE_SHOP_CLAN = 50;
    public const int OP_APPROVAL_CLAN_MEMBER = 51;
    public const int OP_PLUS_CLAN_BUFF = 52;
    public const int OP_CLEAR_CLAN_BUFF = 53;
    public const int OP_UPGRADE_SKILL_HOUSE = 54;
    public const int OP_UPGRADE_MAIN_HOUSE = 55;
    public const int OP_UPGRADE_MEMBER_DUTY = 56;
    public const int OP_CHANGE_SLOGAN_CLAN = 57;
    public const int OP_OUT_CLAN = 58;
    public const int OP_TOP_SPEND_GOLD = 59;
    public const int OP_TYPE_GIFT_CODE = 60;
    public const int OP_NUM_OF_TASK = 61;
    public const int OP_SHOW_ALL_ITEM = 62;
    /**
     * Văn bản khi hiện center dialog
     */
    public const String CMD_CENTER_OK = "OK";

    /**
     * Cửa hàng vũ khí
     */
    public const sbyte SHOP_WEAPON = 1;

    /**
     * Cửa hàng giáp
     */
    public const sbyte SHOP_ARMOUR = 2;

    /**
     * Cửa hàng nón
     */
    public const sbyte SHOP_HAT = 3;
    public const sbyte SHOP_SKIN = 7;
    public const sbyte SHOP_FOOD = 4;
    public const sbyte SHOP_THUONG_NHAN = 6;
    public const sbyte SHOP_PET = 8;
    public const sbyte SHOP_ARENA = 9;

    public const sbyte SHOP_CLAN = 10;

    public const int OBJKEY_REMOVE_ITEM_EQUIP = 0;
    public const int OBJKEY_KIOSK_ITEM = 1;
    public const int OBJKEY_EQUIP_ITEM_ENCHANT = 2;
    public const int OBJKEY_EQUIP_ITEM_MATERIAL_ENCHANT = 3;
    public const int OBJKEY_EQUIP_ITEM_MATERIAL_CRYSTAL_ENCHANT = 4;
    public const int OBJKEY_ITEM_UP_TIER_ACTIVE = 5;
    public const int OBJKEY_ITEM_UP_TIER_PASSIVE = 6;
    public const int OBJKEY_ITEM_UP_SKILL = 7;
    public const int OBJKEY_SKILL_UP_ID = 8;
    public const int OBJKEY_SELECT_SELL_ITEM = 9;
    public const int OBJKEY_MENU_OF_KIOSK = 10;
    public const int OBJKEY_TYPE_SHOW_KIOSK = 11;
    public const int OBJKEY_INVITE_CHALLENGE_PLAYER = 12;
    public const int OBJKEY_USER_ID_PK = 13;
    public const int OBJKEY_ITEM_PK = 14;
    public const int OBJKEY_PRICE_BET_CHALLENGE = 15;
    public const int OBJKEY_COUNT_OF_ITEM_KIOSK = 16;
    public const int OBJKEY_TATTO_ID_REMOVE = 17;
    public const int OBJKEY_ID_GEM_REMOVE = 18;
    public const int OBJKEY_IS_ENCHANT_GEM = 19;
    public const int OBJKEY_ASK_UP_TIER_GEM_STR = 20;
    public const int OBJKEY_IS_KEEP_GOLD = 21;
    public const int OBJKEY_EQUIP_INLAY_GEM_ID = 22;
    public const int OBJKEY_GEM_INLAY_ID = 23;
    public const int OBJKEY_ID_ITEM_REMOVE_GEM = 24;
    public const int OBJKEY_ID_ITEM_FAST_REMOVE_GEM = 25;
    public const int OBJKEY_CLAN_NAME_REQUEST = 26;
    public const int OBJKEY_JOIN_REQUEST_SELECT = 27;
    public const int OBJKEY_MEM_ID_UPGRADE_DUTY = 28;
    public const int OBJKEY_INDEX_MENU_UPGRADE_DUTY = 29;
    public const int OBJKEY_INDEX_SLOT_SKILL_RENT = 30;
    public const int OBJKEY_NPC_ID_FOR_MAIN_TASK = 31;
    public const int OBJKEY_INDEX_TASK_IN_MY_LIST = 32;
    public const int DIALOG_CONFIRM_REMOVE_ITEM_EQUIP = 0;
    public const int DIALOG_CONFIRM_BUY_KIOSK_ITEM = 1;
    public const int DIALOG_ENCHANT = 3;
    public const int DIALOG_UP_TIER_ITEM = 4;
    public const int DIALOG_UP_SKILL = 5;
    public const int DIALOG_INVITE_CHALLENGE = 6;
    public const int DIALOG_CONFIRM_REMOVE_GEM = 7;
    public const int DIALOG_ASK_KEEP_GEM = 8;
    public const int DIALOG_ASK_REMOVE_GEM = 9;
    public const int DIALOG_ASK_FAST_REMOVE_GEM = 10;
    public const int DIALOG_ASK_REQUEST_JOIN_CLAN = 11;
    public const int DIALOG_ASK_REQUEST_UPGRADE_MAIN_HOUSE = 12;
    public const int DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN = 13;
    public const int DIALOG_ASK_UPGRADE_SKILL_HOUSE = 14;
    public const int DIALOG_ASK_UPGRADE_SHOP_HOUSE = 15;
    public const int INPUT_DIALOG_KIOSK = 0;
    public const int INPUT_DIALOG_CHALLENGE_INVITE = 2;
    public const int INPUT_DIALOG_COUNT_OF_KISOK_ITEM = 3;
    public const int INPUT_DIALOG_CAPTCHA = 4;
    public const int INPUT_DIALOG_SET_PET_SELECTED_INFo = 5;
    public const int INPUT_DIALOG_ADMIN_GET_ITEM = 6;
    public const int INPUT_DIALOG_ADMIN_TELE_TO_PLAYER = 7;
    public const int INPUT_DIALOG_ADMIN_LOCK_USER = 8;
    public const int INPUT_DIALOG_ADMIN_UNLOCK_USER = 9;
    public const int INPUT_DIALOG_ADMIN_CHAT_GLOBAL = 10;
    public const int INPUT_DIALOG_ADMIN_ADD_COIN = 11;
    public const int INPUT_DIALOG_ADMIN_ADD_GOLD = 12;
    public const int INPUT_DIALOG_ADMIN_GET_HISTORY = 13;
    public const int INPUT_DIALOG_CREATE_CLAN = 14;
    public const int INPUT_DIALOG_CHANGE_SLOGAN_CLAN = 15;
    public const int INPUT_DIALOG_EXCHANGE_GOLD_TO_COIN = 16;
    public const int INPUT_TYPE_GIFT_CODE = 17;
    public const int INPUT_TYPE_NAME_TO_BUFF_ENCHANT = 18;
    public const int IMGDIALOG_CAPTCHA = 0;

    public static void init() {
        for (int petFreeId : GopetManager.petFreeIds) {
            if (GopetManager.PETTEMPLATE_HASH_MAP.containsKey(petFreeId)) {
                PetMenuItemInfo petMenuItemInfo = new PetMenuItemInfo(GopetManager.PETTEMPLATE_HASH_MAP.get(petFreeId));
                petMenuItemInfo.setCloseScreenAfterClick(true);
                petMenuItemInfo.setShowDialog(true);
                petMenuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn nó không?"));
                petMenuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                PetFreeList.add(petMenuItemInfo);
            }
        }
    }

    public static void sendMenu(int menuId, Player player)   {
        switch (menuId) {
            case MENU_SELECT_PET_UPGRADE_PASSIVE, MENU_SELECT_PET_UPGRADE_ACTIVE, MENU_PET_INVENTORY, MENU_KIOSK_PET_SELECT -> {
                CopyOnWriteArrayList<Pet> listPet = (CopyOnWriteArrayList<Pet>) player.playerData.pets.clone();

                ArrayList<MenuItemInfo> petItemInfos = new();
                int i = 0;
                if (menuId == MENU_PET_INVENTORY) {
                    Pet p = player.getPet();
                    if (p != null) {
                        i = -1;
                        listPet.add(0, p);
                    }
                }
                for (Pet pet : listPet) {
                    MenuItemInfo menuItemInfo = new PetMenuItemInfo(pet);
                    menuItemInfo.setCloseScreenAfterClick(true);
                    menuItemInfo.setShowDialog(true);
                    menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", pet.getNameWithStar()));
                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                    petItemInfos.add(menuItemInfo);
                    menuItemInfo.setItemId(i);
                    menuItemInfo.setHasId(true);
                    if (i == -1) {
                        menuItemInfo.setTitleMenu(menuItemInfo.getTitleMenu() + " (Đang sử dụng)");
                    }
                    i++;
                }
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Pet của bạn", petItemInfos);
            }

            case MENU_SHOW_ALL_PLAYER_HAVE_ITEM_LVL_10 -> {
                if (player.checkIsAdmin()) {

                    MySqlConnection gameMySqlConnection = MYSQLManager.create();
                    try {
                        ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `player`", gameMySqlConnection);
                        ArrayList<MenuItemInfo> menuItemInfos = new();
                        while (resultSet.next()) {
                            PlayerData playerData = PlayerData.read(resultSet);
                            Player onlinePlayer = PlayerManager.get(playerData.user_id);
                            if (onlinePlayer != null) {
                                playerData = onlinePlayer.playerData;
                            }
                            int numItemLvl10 = (int) playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY).stream().filter(new Predicate<Item>() {
                                 
                                public bool test(Item t) {
                                    return t.lvl >= 10;
                                }
                            }).count();

                            if (numItemLvl10 <= 0) {
                                continue;
                            }

                            MenuItemInfo m = new MenuItemInfo(Utilities.Format("Người chơi %s có %s món cấp 10", playerData.name, numItemLvl10), Utilities.Format("Người chơi %s hiện đang sở hữu %s món đạt cấp 10", playerData.name, numItemLvl10), playerData.avatarPath, false);
                            m.setItemId(numItemLvl10);
                            menuItemInfos.add(m);
                        }
                        menuItemInfos.sort(new Comparator<MenuItemInfo>() {
                             
                            public int compare(MenuItemInfo o1, MenuItemInfo o2) {
                                return o2.getItemId() - o1.getItemId();
                            }
                        });
                        player.controller.showMenuItem(menuId, TYPE_MENU_NONE, "Danh sách người chơi có vật phẩm cấp 10", menuItemInfos);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }  ly {
                        gameMySqlConnection.close();
                    }
                }
            }

            case MENU_SHOW_ALL_ITEM -> {
                ArrayList<MenuItemInfo> menuInfos = new();

                for (ItemTemplate itemTemplate : GopetManager.NonAdminItemList) {
                    MenuItemInfo menuItemInfo = new MenuItemInfo(itemTemplate.getNameViaType() + "((chienluc)" + itemTemplate.getItemId() + ")", itemTemplate.getDescriptionViaType(), itemTemplate.getIconPath(), false);
                    menuInfos.add(menuItemInfo);
                }

                player.controller.showMenuItem(menuId, TYPE_MENU_NONE, "Tất cả vật phẩm", menuInfos);
            }

            case MENU_UNEQUIP_PET, MENU_UNEQUIP_SKIN, MENU_UNEQUIP_WING -> {
                ArrayList<Option> list = new();
                list.add(new Option(0, menuId == MENU_UNEQUIP_PET ? "Không cho thú cưng đi theo" : "Tháo", Option.CAN_SELECT));
                String titleStr = "";
                switch (menuId) {
                    case MENU_UNEQUIP_PET:
                        titleStr = "Tùy chọn với thú cưng đi theo bạn";
                        break;
                    case MENU_UNEQUIP_SKIN:
                        titleStr = "Tùy chọn với trang phục đang mặc của bạn";
                        break;
                    case MENU_UNEQUIP_WING:
                        titleStr = "Tùy chọn với cánh đang mặc của bạn";
                        break;
                }
                player.controller.sendListOption(menuId, titleStr, titleStr, list);
            }

            case MENU_SHOW_MY_LIST_TASK -> {
                CopyOnWriteArrayList<TaskData> taskDatas = player.controller.getTaskCalculator().getTaskDatas();
                ArrayList<MenuItemInfo> taskMenuInfos = new();
                for (TaskData taskData : taskDatas) {
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

            case MENU_OPTION_TASK -> {
                ArrayList<Option> list = new();

                list.add(new Option(0, "Cập nhật", Option.CAN_SELECT));
                list.add(new Option(1, "Hoàn thành nhiệm vụ", Option.CAN_SELECT));

                player.controller.sendListOption(MENU_OPTION_TASK, "Tùy chọn nhiệm vụ", "", list);
            }

            case MENU_SHOW_LIST_TASK -> {
                if (player.controller.objectPerformed.containsKey(OBJKEY_NPC_ID_FOR_MAIN_TASK)) {
                    int npcId = (int) player.controller.objectPerformed.get(OBJKEY_NPC_ID_FOR_MAIN_TASK);
                    ArrayList<TaskTemplate> taskTemplates = player.controller.getTaskCalculator().getTaskTemplate(npcId);
                    if (taskTemplates.Count > 0) {
                        ArrayList<MenuItemInfo> taskMenuInfos = new();
                        for (TaskTemplate taskTemplate : taskTemplates) {
                            MenuItemInfo menuItemInfo = new MenuItemInfo(taskTemplate.getName(), taskTemplate.getDescription(), "dialog/1.png", true);
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText("Bạn có chắc muốn nhận nhiệm vụ này ?" + TaskCalculator.getTaskText(null, taskTemplate.getTask(), taskTemplate.getTimeTask()));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setCloseScreenAfterClick(true);
                            taskMenuInfos.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Nhiệm vụ", taskMenuInfos);
                    } else {
                        player.redDialog("Không có nhiệm vụ để cho bạn nhận");
                    }
                }
            }

            case MENU_LIST_PET_FREE ->
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Nhận pet miễn phí", PetFreeList);
            case MENU_LEARN_NEW_SKILL -> {
                if (player.playerData.petSelected != null) {
                    ArrayList<MenuItemInfo> skillMenuItem = new();
                    for (PetSkill petSkill : getPetSkills(player)) {
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

            case MENU_EXCHANGE_GOLD -> {
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, Utilities.Format("Đổi (vang) (Bạn hiện có: %svnđ)", Utilities.formatNumber(player.user.getCoin())), EXCHANGE_ITEM_INFOS);
            }

            case MENU_SELECT_TYPE_CHANGE_GIFT -> {
                ArrayList<Option> changeList = new();
                changeList.add(new Option(0, "Đổi x1", 1));
                changeList.add(new Option(1, "Đổi x5", 1));
                player.controller.sendListOption(menuId, "Đổi thưởng", CMD_CENTER_OK, changeList);
            }

            case MENU_LIST_GIFT -> {
                ArrayList<MenuItemInfo> MenuItem = new();
                MenuItem.add(new MenuItemInfo("Mảnh sơ cấp pet", "", "npcs/gopet.png"));
//                MenuItem.add(new MenuItemInfo("Mảnh trung cấp pet", "", "npcs/gopet.png"));
//                MenuItem.add(new MenuItemInfo("Mảnh trang bị trung", "", "npcs/gopet.png"));
                for (int[] info : GopetManager.CHANGE_ITEM_DATA) {
                    switch (info[0]) {
                        case GopetManager.GIFT_ITEM_PERCENT_NO_DROP_MORE:
                            ItemTemplate template = GopetManager.itemTemplate.get(info[1]);
                            MenuItemInfo m = new AdminItemInfo(template.getName(), template.getDescription(), template.getIconPath());
                            MenuItem.add(m);
                            break;
                    }
                }

                player.controller.showMenuItem(menuId, TYPE_MENU_NONE, "Danh sách phần thưởng", MenuItem);
            }

            case MENU_APPROVAL_CLAN_MEMBER -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    ArrayList<MenuItemInfo> approvalElements = new();
                    for (ClanRequestJoin clanRequestJoin : clan.getRequestJoin()) {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(clanRequestJoin.name, Utilities.Format("Xin vào bang lúc: %s", Utilities.getDate(clanRequestJoin.timeRequest).toString()), "", true);
                        menuItemInfo.setImgPath(clanRequestJoin.getAvatar());
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", clanRequestJoin.name));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setHasId(true);
                        menuItemInfo.setItemId(clanRequestJoin.user_id);
                        approvalElements.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Duyệt thành viên", approvalElements);
                } else {
                    player.controller.notClan();
                }
            }

            case MENU_UPGRADE_MEMBER_DUTY -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    ArrayList<MenuItemInfo> approvalElements = new();
                    for (ClanMember clanMemberSelect : clan.getMembers()) {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(clanMemberSelect.name + "(Chức vụ: " + clanMemberSelect.getDutyName() + ")", Utilities.Format("Đóng góp quỹ :%s và %s điểm cống hiến", Utilities.formatNumber(clanMemberSelect.fundDonate), Utilities.formatNumber(clanMember.growthPointDonate)), "", true);
                        menuItemInfo.setImgPath(clanMemberSelect.getAvatar());
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", clanMemberSelect.name));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setHasId(true);
                        menuItemInfo.setItemId(clanMemberSelect.user_id);
                        approvalElements.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Duyệt thành viên", approvalElements);
                } else {
                    player.controller.notClan();
                }
            }

            case MENU_ADMIN_MAP -> {
                if (player.checkIsAdmin()) {
                    ArrayList<MenuItemInfo> mapMenuItem = new();
                    for (GopetMap gopetMap : MapManager.mapArr) {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(gopetMap.mapTemplate.getMapName() + "  (" + gopetMap.mapID + ")", "", "", true);
                        menuItemInfo.setImgPath("npcs/mgo.png");
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", gopetMap.mapTemplate.getMapName()));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        mapMenuItem.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Dịch chuyển tới map", mapMenuItem);
                }
            }
            case MENU_DELETE_TIEM_NANG -> {
                ArrayList<MenuItemInfo> menuItemInfos = new();
                for (String option : gym_options) {
                    MenuItemInfo menuItemInfo = new MenuItemInfo(Utilities.Format("Tẩy %s", option), Utilities.Format("Xóa 1 %s với giá %s (vang) và sẽ nhận lại 1 tiềm năng", option, PriceDeleteTiemNang), "", true);
                    menuItemInfo.setShowDialog(true);
                    menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", Utilities.Format("Tẩy %s", option)));
                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                    menuItemInfos.add(menuItemInfo);
                }
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Tẩy gym", menuItemInfos);
            }
            case SHOP_WEAPON, SHOP_HAT, SHOP_SKIN, SHOP_ARMOUR, SHOP_FOOD, SHOP_THUONG_NHAN, SHOP_PET, SHOP_ARENA ->
                showShop((sbyte) menuId, player);
            case MENU_WING_INVENTORY ->
                showInventory(player, GopetManager.WING_INVENTORY, menuId, "Cánh của tôi");
            case MENU_NORMAL_INVENTORY ->
                showInventory(player, GopetManager.NORMAL_INVENTORY, menuId, "Hành trang");
            case MENU_SKIN_INVENTORY ->
                showInventory(player, GopetManager.SKIN_INVENTORY, menuId, "Tủ quần ảo");
            case MENU_SELECT_ENCHANT_MATERIAL1, MENU_SELECT_ENCHANT_MATERIAL2, MENU_MERGE_PART_PET, MENU_SELECT_ITEM_UP_SKILL, MENU_SELECT_ITEM_PK, MENU_SELECT_ITEM_PART_FOR_STAR_PET, MENU_SELECT_ITEM_GEN_TATTO, MENU_SELECT_ITEM_REMOVE_TATTO, MENU_SELECT_ITEM_SUPPORT_PET, MENU_SELECT_GEM_ENCHANT_MATERIAL2, MENU_SELECT_GEM_ENCHANT_MATERIAL1, MENU_SELECT_GEM_UP_TIER, MENU_MERGE_PART_ITEM, MENU_SELECT_GEM_TO_INLAY -> {
                CopyOnWriteArrayList<Item> listItems = Item.search(typeSelectItemMaterial(menuId, player), player.playerData.getInventoryOrCreate(getTypeInventorySelect(menuId)));
                ArrayList<MenuItemInfo> menuItemMaterial1Infos = new();
                for (Item item : listItems) {
                    MenuItemInfo menuItemInfo = new MenuItemInfo(item.getTemp().isStackable() ? item.getName() : item.getEquipName(), item.getDescription(player), item.getTemp().getIconPath(), true);
                    menuItemInfo.setShowDialog(true);
                    menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", item.getName()));
                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                    menuItemInfo.setCloseScreenAfterClick(true);
                    menuItemMaterial1Infos.add(menuItemInfo);
                }
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn nguyên liệu", menuItemMaterial1Infos);

            }

            case MENU_SELECT_ITEM_ADMIN -> {
                ArrayList<MenuItemInfo> menuItemInfos = new(Arrays.asList(ADMIN_INFOS));
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "MENU ADMIN", menuItemInfos);
                menuItemInfos.Clear();
            }
            case MENU_KIOSK_AMOUR_SELECT, MENU_KIOSK_HAT_SELECT, MENU_KIOSK_WEAPON_SELECT, MENU_KIOSK_GEM_SELECT -> {
                CopyOnWriteArrayList<Item> listItems = Item.search(typeSelectItemMaterial(menuId, player), player.playerData.getInventoryOrCreate(menuId != MENU_KIOSK_GEM_SELECT ? GopetManager.EQUIP_PET_INVENTORY : GopetManager.GEM_INVENTORY));
                ArrayList<MenuItemInfo> menuItemEquipInfos = new();
                for (Item item : listItems) {
                    MenuItemInfo menuItemInfo = new MenuItemInfo(item.getEquipName(), item.getDescription(player), item.getTemp().getIconPath(), true);
                    menuItemInfo.setShowDialog(true);
                    menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", item.getName()));
                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                    menuItemInfo.setCloseScreenAfterClick(true);
                    menuItemEquipInfos.add(menuItemInfo);
                }
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn trang bị", menuItemEquipInfos);
            }

            case MENU_KIOSK_OHTER_SELECT -> {
                CopyOnWriteArrayList<Item> listItems = player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY);
                ArrayList<MenuItemInfo> menuItemEquipInfos = new();
                for (Item item : listItems) {
                    MenuItemInfo menuItemInfo = new MenuItemInfo(item.getName(), item.getDescription(player), item.getTemp().getIconPath(), true);
                    menuItemInfo.setShowDialog(true);
                    menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", item.getName()));
                    menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                    menuItemInfo.setCloseScreenAfterClick(true);
                    menuItemEquipInfos.add(menuItemInfo);
                }
                player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn vật phẩm", menuItemEquipInfos);
            }

            case MENU_APPROVAL_CLAN_MEM_OPTION -> {
                ArrayList<Option> approvalOptions = new();
                approvalOptions.add(new Option(0, "Duyệt", (sbyte) 1));
                approvalOptions.add(new Option(1, "Xóa", (sbyte) 1));
                approvalOptions.add(new Option(2, "Xóa và cho vào danh sách chặn", (sbyte) 1));
                approvalOptions.add(new Option(3, "Xóa tất cả", (sbyte) 1));
                player.controller.sendListOption(menuId, "Duyệt thành viên", "", approvalOptions);
            }

            case MENU_SELECT_SKILL_CLAN_TO_RENT -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    if (clan.getClanPotentialSkills().Count > 0) {
                        ArrayList<MenuItemInfo> skillMenuItemInfos = new();
                        for (ClanPotentialSkill clanPotentialSkill : clan.getClanPotentialSkills()) {
                            ClanBuffTemplate clanBuffTemplate = GopetManager.CLANBUFF_HASH_MAP.get(clanPotentialSkill.getBuffId());
                            MenuItemInfo menuItemInfo = new MenuItemInfo(ClanBuff.getName(clanBuffTemplate.getValuePerLevel() * clanPotentialSkill.getPoint(), clanPotentialSkill.getBuffId()), ClanBuff.getDesc(clanBuffTemplate.getValuePerLevel() * clanPotentialSkill.getPoint(), clanPotentialSkill.getBuffId()), "npcs/gopet.png", true);
                            menuItemInfo.setShowDialog(true);
                            menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", menuItemInfo.getTitleMenu()));
                            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                            menuItemInfo.setCloseScreenAfterClick(true);
                            skillMenuItemInfos.add(menuItemInfo);
                        }
                        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Chọn kỹ năng bang hội", skillMenuItemInfos);
                    } else {
                        player.redDialog("Bang hội bạng chưa có kỹ năng nào\nVui lòng cộng tiềm năng vào kỹ năng để mở khóa");
                    }
                } else {
                    player.controller.notClan();
                }
            }

            case MENU_PLUS_SKILL_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    ArrayList<MenuItemInfo> skillMenuItemInfos = new();
                    for (ClanBuffTemplate clanBuffTemplate : GopetManager.CLAN_BUFF_TEMPLATES) {
                        MenuItemInfo menuItemInfo = new MenuItemInfo(ClanBuff.getName(clanBuffTemplate.getValuePerLevel() * 1, clanBuffTemplate.getBuffId()) + " Yêu cấu bang cấp: " + clanBuffTemplate.getLvlClan(), ClanBuff.getDesc(clanBuffTemplate.getValuePerLevel() * 1, clanBuffTemplate.getBuffId()), "npcs/gopet.png", true);
                        menuItemInfo.setShowDialog(true);
                        menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", menuItemInfo.getTitleMenu()));
                        menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                        menuItemInfo.setCloseScreenAfterClick(true);
                        skillMenuItemInfos.add(menuItemInfo);
                    }
                    player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, "Tăng kỹ năng bang hội", skillMenuItemInfos);
                } else {
                    player.controller.notClan();
                }
            }

            case MENU_INTIVE_CHALLENGE -> {
                ArrayList<Option> list = new();
                for (int i = 0; i < GopetManager.PRICE_BET_CHALLENGE.Length; i++) {
                    long l = GopetManager.PRICE_BET_CHALLENGE[i];
                    Option option = new Option(i, Utilities.formatNumber(l) + " ngọc", 1);
                    list.add(option);
                }
                player.controller.sendListOption(menuId, "Đặt cược", CMD_CENTER_OK, list);
            }

            case MENU_ATM -> {
                ArrayList<Option> options = new();
                options.add(new Option(0, "Đổi (vang)"));
                options.add(new Option(1, "Đổi (ngoc)"));
                player.controller.sendListOption(menuId, "Cây ATM", CMD_CENTER_OK, options);
            }

            case MENU_KIOSK_HAT, MENU_KIOSK_WEAPON, MENU_KIOSK_AMOUR, MENU_KIOSK_GEM, MENU_KIOSK_PET, MENU_KIOSK_OHTER -> {
                MarketPlace marketPlace = (MarketPlace) player.getPlace();
                Kiosk kiosk = null;
                switch (menuId) {
                    case MENU_KIOSK_HAT ->
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_HAT);
                    case MENU_KIOSK_WEAPON ->
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_WEAPON);
                    case MENU_KIOSK_AMOUR ->
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_AMOUR);
                    case MENU_KIOSK_GEM ->
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_GEM);
                    case MENU_KIOSK_PET ->
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_PET);
                    case MENU_KIOSK_OHTER ->
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_OTHER);
                    default -> {
                        return;
                    }
                }
                switch (menuId) {
                    case MENU_KIOSK_HAT, MENU_KIOSK_WEAPON, MENU_KIOSK_AMOUR, MENU_KIOSK_OHTER, MENU_KIOSK_GEM -> {
                        ArrayList<MenuItemInfo> arrayListEquip = new();
                        for (SellItem kioskItem : kiosk.kioskItems) {
                            MenuItemInfo menuItemInfo = new MenuItemInfo();
                            menuItemInfo.setCanSelect(true);
                            menuItemInfo.setTitleMenu((menuId != MENU_KIOSK_OHTER ? kioskItem.ItemSell.getEquipName() : kioskItem.ItemSell.getName()) + Utilities.Format(" (Mã định danh:%s)", kioskItem.itemId));
                            menuItemInfo.setImgPath(kioskItem.getFrameImgPath());
                            menuItemInfo.setDesc(Utilities.Format("Giá %s (ngoc) ", Utilities.formatNumber(kioskItem.price)) + kioskItem.ItemSell.getTemp().getDescription());
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
                    case MENU_KIOSK_PET -> {
                        ArrayList<MenuItemInfo> arrayListEquip = new();
                        for (SellItem kioskItem : kiosk.kioskItems) {
                            MenuItemInfo menuItemInfo = new MenuItemInfo();
                            menuItemInfo.setCanSelect(true);
                            menuItemInfo.setTitleMenu(kioskItem.pet.getNameWithStar() + Utilities.Format(" (Mã định danh:%s)", kioskItem.itemId));
                            menuItemInfo.setImgPath(kioskItem.pet.getPetTemplate().getIcon());
                            menuItemInfo.setDesc(Utilities.Format("Giá %s (ngoc) ", Utilities.formatNumber(kioskItem.price)) + kioskItem.pet.getDesc());
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
                }
            }
        }
    }

    public static ArrayList<int> typeSelectItemMaterial(int menuId, Player player) {
        ArrayList<int> arrayList = new();
        switch (menuId) {

            case MENU_SELECT_ENCHANT_MATERIAL1, MENU_SELECT_GEM_ENCHANT_MATERIAL1 ->
                arrayList.add(!player.playerData.isOnSky ? GopetManager.MATERIAL_ENCHANT_ITEM : GopetManager.MATERIAL_ENCHANT_ITEM_SKY);
            case MENU_SELECT_ENCHANT_MATERIAL2 ->
                arrayList.add(GopetManager.ENCHANT_MATERIAL_CRYSTAL);
            case MENU_MERGE_PART_PET ->
                arrayList.add(GopetManager.ITEM_PART_PET);
            case MENU_SELECT_ITEM_UP_SKILL ->
                arrayList.add(GopetManager.ITEM_UP_SKILL_PET);
            case MENU_KIOSK_HAT_SELECT ->
                arrayList.add(GopetManager.PET_EQUIP_HAT);
            case MENU_KIOSK_WEAPON_SELECT ->
                arrayList.add(GopetManager.PET_EQUIP_WEAPON);

            case MENU_KIOSK_AMOUR_SELECT -> {
                arrayList.add(GopetManager.PET_EQUIP_ARMOUR);
                arrayList.add(GopetManager.PET_EQUIP_GLOVE);
                arrayList.add(GopetManager.PET_EQUIP_SHOE);
            }
            case MENU_SELECT_ITEM_PK -> {
                arrayList.add(GopetManager.ITEM_PK);
            }
            case MENU_SELECT_ITEM_PART_FOR_STAR_PET -> {
                arrayList.add(GopetManager.ITEM_PART_PET);
            }
            case MENU_SELECT_ITEM_GEN_TATTO ->
                arrayList.add(GopetManager.ITEM_GEN_TATTOO_PET);
            case MENU_SELECT_ITEM_REMOVE_TATTO ->
                arrayList.add(GopetManager.ITEM_REMOVE_TATTO);
            case MENU_SELECT_ITEM_SUPPORT_PET ->
                arrayList.add(GopetManager.ITEM_SUPPORT_PET_IN_BATTLE);
            case MENU_SELECT_GEM_ENCHANT_MATERIAL2 ->
                arrayList.add(GopetManager.ITEM_MATERIAL_EMCHANT_GEM);
            case MENU_SELECT_GEM_UP_TIER, MENU_SELECT_GEM_TO_INLAY, MENU_KIOSK_GEM_SELECT ->
                arrayList.add(GopetManager.ITEM_GEM);
            case MENU_MERGE_PART_ITEM ->
                arrayList.add(GopetManager.ITEM_PART_ITEM);
        }
        return arrayList;
    }

    public static void selectMenu(int menuId, int index, int paymentIndex, Player player)   {
        switch (menuId) {
            case MENU_UNEQUIP_PET, MENU_UNEQUIP_SKIN, MENU_UNEQUIP_WING: {
                if (player.getPlace() instanceof ChallengePlace) {
                    player.redDialog("Không thể thao tác khi đang đi ải");
                    return;
                }

                if (player.controller.getPetBattle() != null) {
                    player.redDialog("Không thể thao tác khi đang giao chiến");
                    return;
                }

                Pet p = player.getPet();
                GopetPlace place = (GopetPlace) player.getPlace();
                if (place == null) {
                    return;
                }
                switch (menuId) {
                    case MENU_UNEQUIP_PET: {
                        if (p != null) {
                            player.playerData.petSelected = null;
                            player.playerData.pets.add(p);
                            player.controller.unfollowPet(p);
                            player.okDialog("Thao tác thành công");
                            HistoryManager.addHistory(new History(player).setLog("Tháo pet"));
                        } else {
                            player.petNotFollow();
                        }
                    }
                    break;
                    case MENU_UNEQUIP_SKIN: {
                        Item it = player.playerData.skinItem;
                        if (it != null) {
                            player.playerData.skinItem = null;
                            player.addItemToInventory(it);
                            place.sendMySkin(player);
                            if (p != null) {
                                p.applyInfo(player);
                            }
                            player.okDialog("Thao tác thành công");
                            HistoryManager.addHistory(new History(player).setLog("Tháo cải trang " + it.getName()).setObj(it));
                        } else {
                            player.redDialog("Hiện tại bạn không có mang bất kỳ trang phục nào!");
                        }
                    }
                    break;

                    case MENU_UNEQUIP_WING: {
                        Item it = player.playerData.wingItem;
                        if (it != null) {
                            player.playerData.wingItem = null;
                            player.addItemToInventory(it);
                            place.sendUnEquipWing(player);
                            if (p != null) {
                                p.applyInfo(player);
                            }
                            player.okDialog("Thao tác thành công");
                            HistoryManager.addHistory(new History(player).setLog("Tháo cánh " + it.getName()).setObj(it));
                        } else {
                            player.redDialog("Hiện tại bạn không có mang bất kỳ cánh nào!");
                        }
                    }
                    break;
                }
            }
            break;

            case MENU_ATM: {
                switch (index) {
                    case 0:
                        sendMenu(MENU_EXCHANGE_GOLD, player);
                        break;
                    case 1:
                        player.controller.showInputDialog(INPUT_DIALOG_EXCHANGE_GOLD_TO_COIN, Utilities.Format("Tỉ lệ 1 (vang) lấy %s (ngoc)", GopetManager.PERCENT_EXCHANGE_GOLD_TO_COIN), new String[]{"Số gold :"});
                        break;
                }
            }
            break;

            case MENU_EXCHANGE_GOLD: {
                if (index >= 0 && index < EXCHANGE_ITEM_INFOS.Count) {
                    ExchangeItemInfo exchangeItemInfo = (ExchangeItemInfo) EXCHANGE_ITEM_INFOS.get(index);
                    int mycoin = player.user.getCoin();
                    if (mycoin >= exchangeItemInfo.getExchangeData().getAmount()) {
                        player.user.mineCoin(exchangeItemInfo.getExchangeData().getAmount(), mycoin);
                        if (player.user.getCoin() >= 0) {
                            player.addGold(exchangeItemInfo.getExchangeData().getGold());
                            player.okDialog(Utilities.Format("Đổi thành công %s (vang)", Utilities.formatNumber(exchangeItemInfo.getExchangeData().getGold())));
                            HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Đổi %s vàng trong game thành công", Utilities.formatNumber(exchangeItemInfo.getExchangeData().getGold()))));
                        } else {
                            UserData.banBySQL(UserData.BAN_INFINITE, "Bug gold", Long.MAX_VALUE, player.user.user_id);
                            player.session.close();
                        }
                    } else {
                        player.redDialog("Bạn không đủ tiền");
                    }
                }
            }
            break;

            case MENU_SHOW_LIST_TASK: {
                if (player.controller.objectPerformed.containsKey(OBJKEY_NPC_ID_FOR_MAIN_TASK)) {
                    int npcId = (int) player.controller.objectPerformed.get(OBJKEY_NPC_ID_FOR_MAIN_TASK);
                    ArrayList<TaskTemplate> taskTemplates = player.controller.getTaskCalculator().getTaskTemplate(npcId);
                    if (index >= 0 && index < taskTemplates.Count) {
                        TaskTemplate taskTemplate = taskTemplates.get(index);
                        player.playerData.tasking.add(taskTemplate.getTaskId());
                        player.playerData.task.add(new TaskData(taskTemplate));
                        player.controller.getTaskCalculator().update();
                        player.okDialog("Chúc mừng bạn đã nhận thành công nhiệm vụ");
                    } else {
                        player.redDialog("Trong khi nhận nhiệm vụ bạn vui lòng thao tác chậm lại!");
                    }
                }
            }
            break;

            case MENU_SHOW_MY_LIST_TASK: {
                player.controller.objectPerformed.put(OBJKEY_INDEX_TASK_IN_MY_LIST, index);

                sendMenu(MENU_OPTION_TASK, player);
            }
            break;

            case MENU_OPTION_TASK: {
                if (player.controller.objectPerformed.containsKey(OBJKEY_INDEX_TASK_IN_MY_LIST)) {
                    int indexTask = (int) player.controller.objectPerformed.get(OBJKEY_INDEX_TASK_IN_MY_LIST);
                    player.controller.objectPerformed.remove(OBJKEY_INDEX_TASK_IN_MY_LIST);

                    if (indexTask >= 0 && indexTask < player.playerData.task.Count) {
                        TaskData taskData = player.playerData.task.get(indexTask);
                        switch (index) {
                            case 0:
                                player.controller.getTaskCalculator().onUpdateTask(taskData);
                                player.okDialog("Cập nhật thành công");
                                break;
                            case 1:
                                if (player.controller.getTaskCalculator().taskSuccess(taskData)) {
                                    player.controller.getTaskCalculator().onTaskSucces(taskData);
                                } else {
                                    player.redDialog("Bạn chưa đủ điều kiện");
                                }
                                break;
                        }
                    }
                }
            }
            break;

            case MENU_LIST_PET_FREE:
                if (index >= 0 && index < PetFreeList.Count) {
                      PetMenuItemInfo petMenuItemInfo = (PetMenuItemInfo) PetFreeList.get(index);
                    if (!player.playerData.isFirstFree) {
                        player.playerData.isFirstFree = true;
                        Pet p = new Pet(petMenuItemInfo.getPetTemplate().getPetId());
                        player.playerData.addPet(p, player);
                        player.okDialog(Utilities.Format("Nhận pet %s thành công vào túi pet để xem", petMenuItemInfo.getPetTemplate().getName()));
                        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Nhận pet %s miễn phí tại NPC trân trân", petMenuItemInfo.getPetTemplate().getName())).setObj(p));
                    } else {
                        player.redDialog("Trước đó bạn đã nhận rồi");
                    }
                }
                break;
            case MENU_INTIVE_CHALLENGE: {
                if (index >= 0 && index < GopetManager.PRICE_BET_CHALLENGE.Length) {
                    int priceChallenge = (int) GopetManager.PRICE_BET_CHALLENGE[index];
                    if (priceChallenge <= 0) {
                        player.redDialog("Tính bug ha gì!");
                        return;
                    }
                    if (priceChallenge > 100000) {
                        player.redDialog(Utilities.Format("Giới hạn (ngoc) là %s", Utilities.formatNumber(100000)));
                        return;
                    }
                    player.controller.sendChallenge((Player) player.controller.objectPerformed.get(OBJKEY_INVITE_CHALLENGE_PLAYER), priceChallenge);

                }
            }
            break;
            case MENU_LEARN_NEW_SKILL:
                if (player.checkCoin(GopetManager.PriceLearnSkill)) {
                    PetSkill[] petSkills = getPetSkills(player);
                    Pet pet = player.playerData.petSelected;
                    if (index >= 0 && index < petSkills.Length && pet != null) {

                        for (int[] skillInfo : pet.skill) {
                            if (skillInfo[0] == petSkills[index].skillID) {
                                player.redDialog("Kỹ năng này học rồi");
                                return;
                            }
                        }

                        if (pet.skillPoint > 0 && player.skillId_learn == -1) {
                            pet.skillPoint--;
                            pet.addSkill(petSkills[index].skillID, 1);
                            player.addCoin(-GopetManager.PriceLearnSkill);
                            player.controller.magic(GopetCMD.MAGIC_LEARN_SKILL, true);
                            player.okDialog("Học kỹ năng thành công");
                            player.controller.getTaskCalculator().onLearnSkillPet();
                        } else if (player.skillId_learn != -1) {
                            if (pet.skill.Length > 0) {
                                bool flag = false;
                                int skillIndex = -1;
                                for (int i = 0; i < pet.skill.Length; i++) {
                                    int[] skillInfo = pet.skill[i];
                                    if (skillInfo[0] == player.skillId_learn) {
                                        flag = true;
                                        skillIndex = i;
                                        break;
                                    }
                                }
                                if (flag) {
                                    pet.skill[skillIndex][0] = petSkills[index].skillID;
                                    pet.skill[skillIndex][1] = 1;
                                    player.addCoin(-GopetManager.PriceLearnSkill);
                                    player.controller.magic(GopetCMD.MAGIC_LEARN_SKILL, true);
                                    player.okDialog("Học kỹ năng thành công");
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Học kỹ năng thành công cho pet %s", pet.getNameWithoutStar())).setObj(pet));
                                } else {
                                    player.redDialog("Không có kỹ năng này");
                                }
                            } else {
                                player.redDialog("Pet của bạn phải đạt mốc lvl 3, 5, 10 để học skill 1 2 3 nhé");
                            }
                        } else {
                            player.redDialog("Pet của bạn phải đạt mốc lvl 3, 5, 10 để học skill 1 2 3 nhé");
                        }
                    }
                } else {
                    player.controller.notEnoughCoin();
                }
                break;
            case MENU_DELETE_TIEM_NANG:
                if (player.getPet() != null) {
                    if (index >= 0 && index < gym_options.Length) {
                        Pet pet = player.getPet();
                        if (pet.tiemnang[index] > 0) {
                            if (player.checkGold(PriceDeleteTiemNang)) {
                                player.mineGold(PriceDeleteTiemNang);
                                pet.tiemnang[index]--;
                                pet.tiemnang_point++;
                                pet.applyInfo(player);
                                player.okDialog("Tẩy thành công");
                                HistoryManager.addHistory(new History(player).setLog("Tảy tìm năng cho pet" + pet.getNameWithoutStar()).setObj(pet));
                            } else {
                                player.controller.notEnoughGold();
                            }
                        } else {
                            player.redDialog("Chỉ số này đã xóa hết rồi");
                        }
                    }
                } else {
                    player.petNotFollow();
                }
                break;
            case MENU_PET_INVENTORY:
                if (index == -1) {
                    sendMenu(MENU_UNEQUIP_PET, player);
                    return;
                }
                if (index >= 0 && index < player.playerData.pets.Count) {
                    Pet pet = player.playerData.pets.get(index);
                    player.playerData.pets.remove(pet);
                    Pet oldPet = player.playerData.petSelected;
                    if (oldPet != null) {
                        player.playerData.addPet(oldPet, player);
                    }
                    player.playerData.petSelected = pet;
                    player.controller.updatePetSelected(false);
                }
                break;
            case MENU_SKIN_INVENTORY:
                if (index == -1) {
                    sendMenu(MENU_UNEQUIP_SKIN, player);
                    return;
                }
                CopyOnWriteArrayList<Item> listSkinItems = player.playerData.getInventoryOrCreate(GopetManager.SKIN_INVENTORY);
                if (index >= 0 && index < listSkinItems.Count) {
                    Item skinItem = listSkinItems.get(index);
                    Item oldSkinItem = player.playerData.skinItem;
                    if (oldSkinItem != null) {
                        listSkinItems.add(oldSkinItem);
                    }
                    listSkinItems.remove(skinItem);
                    player.playerData.skinItem = skinItem;
                    Pet p = player.getPet();
                    if (p != null) {
                        p.applyInfo(player);
                    }
                    player.controller.updateSkin();
                }
                break;
            case MENU_WING_INVENTORY:
                if (index == -1) {
                    sendMenu(MENU_UNEQUIP_WING, player);
                    return;
                }
                CopyOnWriteArrayList<Item> listWingItems = player.playerData.getInventoryOrCreate(GopetManager.WING_INVENTORY);
                if (index >= 0 && index < listWingItems.Count) {
                    Item wingItem = listWingItems.get(index);
                    Item oldWingItem = player.playerData.wingItem;
                    if (oldWingItem != null) {
                        listWingItems.add(oldWingItem);
                    }
                    listWingItems.remove(wingItem);
                    player.playerData.wingItem = wingItem;
                    Pet p = player.getPet();
                    if (p != null) {
                        p.applyInfo(player);
                    }
                    player.controller.updateWing();
                }
                break;
            case MENU_SELECT_SKILL_CLAN_TO_RENT: {
                int indexSlot = (int) player.controller.objectPerformed.get(OBJKEY_INDEX_SLOT_SKILL_RENT);
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    bool canEdit = clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER;
                    if (index >= 0 && index < clan.getClanPotentialSkills().Count) {
                        ClanPotentialSkill clanPotentialSkill = clan.getClanPotentialSkills().get(index);
                        ClanBuffTemplate clanBuffTemplate = GopetManager.CLANBUFF_HASH_MAP.get(clanPotentialSkill.getBuffId());
                        if (canEdit) {
                            ClanBuff dup = clan.getBuffByIdBuff(clanBuffTemplate.getBuffId());
                            if (dup != null) {
                                player.redDialog("Bang hội bạn đã thuê kỹ năng này rồi");
                                return;
                            }
                            if (clan.checkFund(GopetManager.PRICE_RENT_SKILL[0]) && clan.checkGrowthPoint(GopetManager.PRICE_RENT_SKILL[1])) {
                                clan.mineFund(GopetManager.PRICE_RENT_SKILL[0]);
                                clan.mineGrowthPoint(GopetManager.PRICE_RENT_SKILL[1]);

                                ClanBuff newBUff = new ClanBuff();
                                newBUff.setBuffId(clanBuffTemplate.getBuffId());
                                newBUff.setValue(clanBuffTemplate.getValuePerLevel() * clanPotentialSkill.getPoint());
                                newBUff.setTimeEndBuff(Utilities.TimeDay(30));
                                ClanBuff getBuffByIndex = clan.getBuff(indexSlot);
                                if (getBuffByIndex != null) {
                                    clan.getClanBuffs().set(indexSlot, newBUff);
                                } else {
                                    clan.getClanBuffs().add(newBUff);
                                }
                                player.controller.showSkillClan(player.user.user_id);
                                player.okDialog("Thuê thành công");
                            } else {
                                player.redDialog("Bang hội không đủ điểm");
                            }
                        } else {
                            player.redDialog("Bạn không đủ quyền");
                        }
                    }
                } else {
                    player.controller.notClan();
                }
            }
            break;
            case MENU_PLUS_SKILL_CLAN: {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    bool canEdit = clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER;
                    if (index >= 0 && index < GopetManager.CLAN_BUFF_TEMPLATES.Count) {
                        ClanBuffTemplate clanBuffTemplate = GopetManager.CLAN_BUFF_TEMPLATES.get(index);
                        if (clan.getLvl() >= clanBuffTemplate.getLvlClan()) {
                            if (canEdit) {
                                if (clanBuffTemplate.getPotentialPointNeed() <= clan.getPotentialPoint()) {
                                    ClanPotentialSkill clanPotentialSkill = clan.getClanPotentialSkillOrCreate(clanBuffTemplate.getBuffId());
                                    clanPotentialSkill.addPoint(1);
                                    clan.addPotentialPoint(-clanBuffTemplate.getPotentialPointNeed());
                                    player.okDialog("Nâng cấp thành công");
                                } else {
                                    player.redDialog("không đủ điểm tiềm năng");
                                }
                            } else {
                                player.redDialog("Bạn không đủ quyền");
                            }
                        } else {
                            player.redDialog("Bang của bạn chưa đủ cấp!");
                        }
                    }
                } else {
                    player.controller.notClan();
                }
            }
            break;
            case SHOP_CLAN:
            case SHOP_WEAPON:
            case SHOP_HAT:
            case SHOP_SKIN:
            case SHOP_ARMOUR:
            case SHOP_THUONG_NHAN:
            case SHOP_PET:
            case SHOP_FOOD:
            case SHOP_ARENA:
                ShopTemplate shopTemplate = getShop((sbyte) menuId, player);
                if ((index >= 0 && index < shopTemplate.getShopTemplateItems().Count && menuId != SHOP_CLAN) || menuId == SHOP_CLAN) {
                    ShopTemplateItem shopTemplateItem = null;
                    if (menuId != SHOP_CLAN) {
                        shopTemplateItem = shopTemplate.getShopTemplateItems().get(index);
                    } else {
                        ClanMember clanMember = player.controller.getClan();
                        if (clanMember != null) {
                            shopTemplateItem = clanMember.getClan().getShopClan().getShopTemplateItem(index);
                        } else {
                            player.controller.notClan();
                            return;
                        }
                    }

                    if (shopTemplateItem == null) {
                        player.redDialog("Vật phẩm này đã bị người khác mua");
                        return;
                    }
                    sbyte[] typeMoney = shopTemplateItem.getMoneyType();
                    int[] price = shopTemplateItem.getPrice();
                    if (paymentIndex >= 0 && paymentIndex < typeMoney.Length) {
                        if (checkMoney(typeMoney[paymentIndex], price[paymentIndex], player)) {
                            addMoney(typeMoney[paymentIndex], -price[paymentIndex], player);
                            if (shopTemplateItem.isNeedRemove()) {
                                shopTemplate.getShopTemplateItems().remove(shopTemplateItem);
                            }
                            if (!shopTemplateItem.isSpceial()) {
                                if (shopTemplateItem.isSellItem()) {
                                    Item item = new Item(shopTemplateItem.getItemTempalteId());
                                    item.count = shopTemplateItem.getCount();
                                    if (item.getTemp().expire > 0) {
                                        item.expire = Utilities.CurrentTimeMillis + item.getTemp().getExpire();
                                    }
                                    if (shopTemplateItem.getInventoryType() == GopetManager.NORMAL_INVENTORY) {
                                        player.addItemToNormalInventory(item);
                                    } else {
                                        player.playerData.addItem(shopTemplateItem.getInventoryType(), item);
                                    }
                                    player.okDialog(Utilities.Format("Bạn đã mua thành công %s", item.getTemp().getName()));
                                } else {
                                    Pet p = new Pet(shopTemplateItem.getPetId());
                                    player.playerData.addPet(p, player);
                                    player.okDialog(Utilities.Format("Bạn đã mua thành công %s", p.getPetTemplate().getName()));
                                }
                                if (shopTemplateItem.isCloseScreenAfterClick()) {
                                    sendMenu(menuId, player);
                                }

                                if (menuId == SHOP_WEAPON) {
                                    player.controller.getTaskCalculator().onBuyRandomWeapon();
                                }
                            } else {
                                shopTemplateItem.execute(player);
                            }
                        } else {
                            switch (typeMoney[paymentIndex]) {
                                case GopetManager.MONEY_TYPE_COIN ->
                                    player.controller.notEnoughCoin();
                                case GopetManager.MONEY_TYPE_GOLD ->
                                    player.controller.notEnoughGold();
                                case GopetManager.MONEY_TYPE_GOLD_BAR ->
                                    player.controller.notEnoughGoldBar();
                                case GopetManager.MONEY_TYPE_SILVER_BAR ->
                                    player.controller.notEnoughSilverBar();
                                case GopetManager.MONEY_TYPE_BLOOD_GEM ->
                                    player.controller.notEnoughBloodGem();
                                case GopetManager.MONEY_TYPE_FUND_CLAN ->
                                    player.controller.notEnoughFundClan();
                                case GopetManager.MONEY_TYPE_GROWTH_POINT_CLAN ->
                                    player.controller.notEnoughGrowthPointClan();
                            }
                        }
                    }
                }
                break;
            case MENU_SELECT_PET_UPGRADE_ACTIVE: {
                if (index >= 0 && index < player.playerData.pets.Count) {
                    Pet pet = player.playerData.pets.get(index);
                    player.controller.addPetUpgrade(pet, GopetCMD.PET_UPGRADE_ACTIVE, pet.petId);
                }
            }
            case MENU_SELECT_PET_UPGRADE_PASSIVE: {
                if (index >= 0 && index < player.playerData.pets.Count) {
                    Pet pet = player.playerData.pets.get(index);
                    player.controller.addPetUpgrade(pet, GopetCMD.PET_UPGRADE_PASSIVE, pet.petId);
                }
            }
            break;
            case MENU_SELECT_GEM_TO_INLAY:
            case MENU_SELECT_GEM_UP_TIER:
            case MENU_SELECT_ENCHANT_MATERIAL1:
            case MENU_SELECT_ENCHANT_MATERIAL2:
            case MENU_SELECT_GEM_ENCHANT_MATERIAL1:
            case MENU_SELECT_GEM_ENCHANT_MATERIAL2:
            case MENU_MERGE_PART_PET:
            case MENU_SELECT_ITEM_UP_SKILL:
            case MENU_SELECT_ITEM_PK:
            case MENU_SELECT_ITEM_PART_FOR_STAR_PET:
            case MENU_SELECT_ITEM_GEN_TATTO:
            case MENU_SELECT_ITEM_REMOVE_TATTO:
            case MENU_SELECT_ITEM_SUPPORT_PET:
            case MENU_MERGE_PART_ITEM:
                CopyOnWriteArrayList<Item> listItems = Item.search(typeSelectItemMaterial(menuId, player), player.playerData.getInventoryOrCreate(getTypeInventorySelect(menuId)));
                if (index >= 0 && listItems.Count > index) {
                    Item itemSelect = listItems.get(index);
                    switch (menuId) {
                        case MENU_SELECT_ENCHANT_MATERIAL1 ->
                            player.controller.selectMaterialEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 1);
                        case MENU_SELECT_ENCHANT_MATERIAL2 ->
                            player.controller.selectMaterialEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 2);
                        case MENU_SELECT_GEM_ENCHANT_MATERIAL1 -> {
                            player.controller.selectMaterialGemEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 1);
                            player.controller.selectGemM1 = true;
                        }
                        case MENU_SELECT_GEM_ENCHANT_MATERIAL2 -> {
                            player.controller.selectMaterialGemEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 12);
                            player.controller.selectGemM1 = false;
                        }
                        case MENU_MERGE_PART_PET -> {
                            if (itemSelect.count >= GopetManager.PART_NEED_MERGE_PET) {
                                player.controller.subCountItem(itemSelect, GopetManager.PART_NEED_MERGE_PET, GopetManager.NORMAL_INVENTORY);
                                int petTemplateId = itemSelect.getTemp().getOptionValue()[0];
                                Pet pet = new Pet(petTemplateId);
                                player.playerData.addPet(pet, player);
                                player.okDialog(Utilities.Format("Chức mừng bạn ghép thành công %s", pet.getNameWithStar()));
                            } else {
                                player.redDialog(Utilities.Format("Bạn không đủ", GopetManager.PART_NEED_MERGE_PET));
                            }
                        }
                        case MENU_SELECT_ITEM_UP_SKILL -> {
                            int skillId = (int) player.controller.objectPerformed.get(OBJKEY_SKILL_UP_ID);
                            Pet pet = player.getPet();
                            int skillIndex = pet.getSkillIndex(skillId);
                            PetSkill petSkill = GopetManager.PETSKILL_HASH_MAP.get(skillId);
                            if (itemSelect.count > 0) {
                                if (pet.skill[skillIndex][1] < 8) {
                                    player.controller.objectPerformed.put(OBJKEY_ITEM_UP_SKILL, itemSelect);
                                    showYNDialog(DIALOG_UP_SKILL, Utilities.Format("Bạn có chắc muốn nâng cấp kỹ năng %s lên cấp %s \n với tỉ lệ (%s/) + %s/ bằng %s/ không?", petSkill.name, pet.skill[skillIndex][1] + 1, GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]], itemSelect.getTemp().getOptionValue()[0], GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]] + itemSelect.getTemp().getOptionValue()[0]).replace("/", "%"), player);
                                } else {
                                    player.redDialog("Kỹ năng đạt cấp tối đa rồi");
                                }
                            }
                        }
                        case MENU_SELECT_ITEM_PK -> {
                            player.controller.objectPerformed.put(OBJKEY_ITEM_PK, itemSelect);
                            player.controller.confirmpk();
                        }

                        case MENU_SELECT_ITEM_PART_FOR_STAR_PET -> {
                            player.controller.upStarPet(itemSelect);
                        }
                        case MENU_SELECT_ITEM_GEN_TATTO -> {
                            player.controller.genTatto(itemSelect);
                        }

                        case MENU_SELECT_ITEM_REMOVE_TATTO -> {
                            player.controller.removeTatto(itemSelect, (int) player.controller.objectPerformed.get(OBJKEY_TATTO_ID_REMOVE));
                        }
                        case MENU_SELECT_ITEM_SUPPORT_PET -> {
                            PetBattle petBattle = player.controller.getPetBattle();
                            if (petBattle != null) {
                                petBattle.useItem(player, itemSelect);
                            }
                        }

                        case MENU_SELECT_GEM_UP_TIER -> {
                            player.controller.selectGemUpTier(itemSelect.itemId, itemSelect.getTemp().getIconPath(), itemSelect.getEquipName(), 1, itemSelect.lvl);
                        }

                        case MENU_MERGE_PART_ITEM -> {
                            int[] optionValue = itemSelect.getTemp().getOptionValue();
                            if (player.controller.checkCount(itemSelect.itemTemplateId, optionValue[1])) {
                                player.controller.subCountItem(itemSelect, optionValue[1], GopetManager.NORMAL_INVENTORY);
                                Item item = new Item(optionValue[0]);
                                item.count = 1;
                                player.addItemToInventory(item);
                                player.okDialog(Utilities.Format("Đổi thành công\n %s", item.getTemp().getName()));
                            } else {
                                player.redDialog(Utilities.Format("Bạn phải đủ %s mảnh mới đổi được", optionValue[1]));
                            }
                        }
                        case MENU_SELECT_GEM_TO_INLAY -> {
                            player.controller.inlayGem(itemSelect, (int) player.controller.objectPerformed.get(OBJKEY_EQUIP_INLAY_GEM_ID));
                            player.controller.objectPerformed.remove(OBJKEY_EQUIP_INLAY_GEM_ID);
                        }
                    }
                }
                break;
            case MENU_SELECT_EQUIP_PET_TIER:
                CopyOnWriteArrayList<Item> listItemEquip = player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY);
                if (index >= 0 && listItemEquip.Count > index) {
                    Item itemSelect = listItemEquip.get(index);
                    player.controller.selectMaterialEnchant(itemSelect.itemId, itemSelect.getTemp().getIconPath(), itemSelect.getEquipName(), int.MaxValue);
                }
                break;

            case MENU_NORMAL_INVENTORY:
                CopyOnWriteArrayList<Item> listItemNormal = player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY);
                if (index >= 0 && listItemNormal.Count > index) {
                    Item itemSelect = listItemNormal.get(index);
                    switch (itemSelect.getTemp().getType()) {
                        case GopetManager.ITEM_BUFF_EXP -> {
                            BuffExp buffExp = player.playerData.buffExp;
                            if (buffExp.getItemTemplateIdBuff() != itemSelect.itemTemplateId) {
                                buffExp.setBuffExpTime(0);
                                buffExp.set_buffPercent(0);
                                buffExp.setItemTemplateIdBuff(itemSelect.itemTemplateId);
                                buffExp.set_buffPercent(itemSelect.getTemp().getOptionValue()[0]);
                            }
                            player.playerData.buffExp.addTime(GopetManager.TIME_BUFF_EXP);
                            player.okDialog(Utilities.Format("Bạn đang được buff %s/ kinh nghiệm trong %s phút!", buffExp.getPercent(), Math.round(buffExp.getBuffExpTime() / 1000 / 60)).replace("/", "%"));
                            break;
                        }

                        case GopetManager.ITEM_ADMIN -> {
                            if (player.checkIsAdmin()) {
                                sendMenu(MENU_SELECT_ITEM_ADMIN, player);
                                return;
                            } else {
                                player.user.ban(UserData.BAN_INFINITE, "Dung VP ADMIN", Long.MAX_VALUE);
                                player.session.close();
                            }
                            return;
                        }
                        default -> {
                            player.redDialog("Không thể sử dụng vật phẩm này");
                            return;
                        }
                    }
                    player.controller.subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                }
                break;
            case MENU_KIOSK_HAT_SELECT:
            case MENU_KIOSK_AMOUR_SELECT:
            case MENU_KIOSK_WEAPON_SELECT:
            case MENU_KIOSK_GEM_SELECT:
            case MENU_KIOSK_OHTER_SELECT: {
                if (menuId == MENU_KIOSK_OHTER_SELECT) {
                    CopyOnWriteArrayList<Item> listEquipItems = player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY);
                    if (listEquipItems.Count > index && index >= 0) {
                        Item sItem = listEquipItems.get(index);
                        player.controller.objectPerformed.put(OBJKEY_SELECT_SELL_ITEM, sItem);
                        player.controller.objectPerformed.put(OBJKEY_MENU_OF_KIOSK, menuId);
                        if (sItem.count == 1) {
                            player.controller.showInputDialog(INPUT_DIALOG_KIOSK, "Định giá", new String[]{"  "}, new sbyte[]{0});
                            player.controller.objectPerformed.put(OBJKEY_COUNT_OF_ITEM_KIOSK, 1);
                        } else if (sItem.count > 1) {
                            player.controller.showInputDialog(INPUT_DIALOG_COUNT_OF_KISOK_ITEM, "Số lượng", new String[]{"  "}, new sbyte[]{0});
                        }
                    }
                } else {
                    CopyOnWriteArrayList<Item> listEquipItems = Item.search(typeSelectItemMaterial(menuId, player), player.playerData.getInventoryOrCreate(menuId != MENU_KIOSK_GEM_SELECT ? GopetManager.EQUIP_PET_INVENTORY : GopetManager.GEM_INVENTORY));
                    if (listEquipItems.Count > index && index >= 0) {
                        Item sItem = listEquipItems.get(index);
                        if (sItem != null) {
                            if (sItem.petEuipId <= 0) {
                                if (sItem.gemInfo == null) {
                                    player.controller.objectPerformed.put(OBJKEY_SELECT_SELL_ITEM, sItem);
                                    player.controller.objectPerformed.put(OBJKEY_MENU_OF_KIOSK, menuId);
                                    player.controller.showInputDialog(INPUT_DIALOG_KIOSK, "Định giá", new String[]{"  "}, new sbyte[]{0});
                                } else {
                                    player.redDialog("Vui lòng tháo ngọc ra");
                                }
                            } else {
                                player.redDialog("Vui lòng không treo trang bị đã được pet mang theo");
                            }
                        }
                    }
                }
            }
            break;
            case MENU_KIOSK_PET_SELECT: {
                if (index >= 0 && index < player.playerData.pets.Count) {
                    Pet pet = player.playerData.pets.get(index);
                    player.controller.objectPerformed.put(OBJKEY_SELECT_SELL_ITEM, pet);
                    player.controller.objectPerformed.put(OBJKEY_MENU_OF_KIOSK, menuId);
                    player.controller.showInputDialog(INPUT_DIALOG_KIOSK, "Định giá", new String[]{"  "}, new sbyte[]{0});
                }

            }
            break;
            case MENU_KIOSK_GEM:
            case MENU_KIOSK_HAT:
            case MENU_KIOSK_WEAPON:
            case MENU_KIOSK_AMOUR:
            case MENU_KIOSK_OHTER:
            case MENU_KIOSK_PET:
                MarketPlace marketPlace = (MarketPlace) player.getPlace();
                Kiosk kiosk = null;
                switch (menuId) {
                    case MENU_KIOSK_HAT:
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_HAT);
                        break;

                    case MENU_KIOSK_GEM:
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_GEM);
                        break;
                    case MENU_KIOSK_WEAPON:
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_WEAPON);
                        break;
                    case MENU_KIOSK_AMOUR:
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_AMOUR);
                        break;
                    case MENU_KIOSK_OHTER:
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_OTHER);
                        break;
                    case MENU_KIOSK_PET:
                        kiosk = marketPlace.getKiosk(GopetManager.KIOSK_PET);
                        break;
                }
                kiosk.buy(index, player);
                break;

            case MENU_APPROVAL_CLAN_MEMBER: {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    if (clanMember.duty != Clan.TYPE_NORMAL) {
                        ClanRequestJoin requestJoin = clan.getJoinRequestByUserId(index);
                        if (requestJoin != null) {
                            player.controller.objectPerformed.put(OBJKEY_JOIN_REQUEST_SELECT, requestJoin.user_id);
                            sendMenu(MENU_APPROVAL_CLAN_MEM_OPTION, player);
                        } else {
                            player.redDialog("Yêu cầu này đã được xét duyệt hoặc gỡ bỏ");
                        }
                    } else {
                        player.redDialog("Bạn chỉ là thành viên bình thường");
                    }
                } else {
                    player.controller.notClan();
                }
            }
            break;
            case MENU_APPROVAL_CLAN_MEM_OPTION: {
                int user_id = (int) player.controller.objectPerformed.get(OBJKEY_JOIN_REQUEST_SELECT);
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    if (clanMember.duty != Clan.TYPE_NORMAL) {
                        ClanRequestJoin requestJoin = clan.getJoinRequestByUserId(user_id);
                        if (requestJoin != null) {
                            switch (index) {
                                case 0:
                                    if (clan.canAddNewMember()) {
                                        MySqlConnection MySqlConnection = MYSQLManager.create();
                                        try {
                                            bool hasClan = false;
                                            Player onlinePlayer = PlayerManager.get(requestJoin.user_id);
                                            if (onlinePlayer == null) {
                                                ResultSet resultSet = MYSQLManager.jquery(Utilities.Format("SELECT * from `player` where user_id = %s AND clanId > 0", requestJoin.user_id), MySqlConnection);
                                                hasClan = resultSet.next();
                                                resultSet.close();
                                            } else {
                                                hasClan = onlinePlayer.playerData.clanId > 0;
                                            }
                                            if (!hasClan) {
                                                clan.addMember(user_id, requestJoin.name);
                                                clan.getRequestJoin().remove(requestJoin);
                                                if (onlinePlayer == null) {
                                                    MYSQLManager.updateSql(Utilities.Format("UPDATE `player` set clanId =%s where user_id =%s;", requestJoin.user_id, clanMember.getClan().getClanId()), MySqlConnection);
                                                } else {
                                                    onlinePlayer.playerData.clanId = clanMember.getClan().getClanId();
                                                    onlinePlayer.okDialog("Lời xin vào bang hội của bạn được chấp nhận");
                                                }
                                                player.okDialog("Duyệt thành công");
                                            } else {
                                                player.redDialog("Người chơi đã vào bang hội khác");
                                            }
                                        } catch (Exception e) {
                                            e.printStackTrace();
                                        }  ly {
                                            MySqlConnection.close();
                                        }
                                    } else {
                                        player.redDialog("Thành viên trong bang hội này đã đủ");
                                    }
                                    break;
                                case 1:
                                    clan.getRequestJoin().remove(requestJoin);
                                    player.okDialog("Xóa thành công");
                                    break;
                                case 2:
                                    clan.getRequestJoin().remove(requestJoin);
                                    clan.getBannedJoinRequestId().addIfAbsent(user_id);
                                    break;
                                case 3:
                                    clan.getRequestJoin().Clear();
                                    player.okDialog("Xóa tất cả thành công");
                                    break;
                            }
                        } else {
                            player.redDialog("Yêu cầu này đã được xét duyệt hoặc gỡ bỏ");
                        }
                    } else {
                        player.redDialog("Bạn chỉ là thành viên bình thường");
                    }
                } else {
                    player.controller.notClan();
                }
            }
            break;
            case MENU_SELECT_ITEM_ADMIN:
                GopetPlace place = (GopetPlace) player.getPlace();
                if (player.checkIsAdmin()) {
                    switch (index) {
                        case ADMIN_INDEX_SET_PET_INFO:
                            player.controller.showInputDialog(INPUT_DIALOG_SET_PET_SELECTED_INFo, "Đặt chỉ số pet đang đi theo", new String[]{"LVL:  ", "STAR:  ", "GYM:  "});
                            break;
                        case ADMIN_INDEX_COUNT_PLAYER:
                            player.okDialog(Utilities.Format("Online player: %s", PlayerManager.players.Count));
                            break;
                        case ADMIN_INDEX_COUNT_OF_MAP:
                            int numPlayerMap = 0;
                            for (Place place1 : place.map.places) {
                                numPlayerMap += place1.numPlayer;
                            }
                            player.okDialog(Utilities.Format("Online player %s: %s", place.map.mapTemplate.getMapName(), numPlayerMap));
                            break;
                        case ADMIN_INDEX_TELE_TO_MAP:
                            sendMenu(MENU_ADMIN_MAP, player);
                            break;
                        case ADMIN_INDEX_SELECT_ITEM:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_GET_ITEM, "Lấy vật phẩm", new String[]{"IdTemplate  :", "Số lượng   :"});
                            break;
                        case ADMIN_INDEX_TELE_TO_PLAYER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_TELE_TO_PLAYER, "Dịch chuyển tới người chơi", new String[]{"Tên \n người chơi :"});
                            break;
                        case ADMIN_INDEX_BAN_PLAYER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_LOCK_USER, "Khóa tài khoản người chơi", new String[]{"Tên \n người chơi :", "1 - phút, 2 - vĩnh viễn) :", "Thời gian khóa (phút) :", "Lý do  :"});
                            break;
                        case ADMIN_INDEX_UNBAN_PLAYER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_UNLOCK_USER, "Gỡ khóa tài khoản người chơi", new String[]{"Tên người chơi :"});
                            break;
                        case ADMIN_INDEX_SHOW_BANNER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_CHAT_GLOBAL, "Chát thế giới", new String[]{"Văn bản :"});
                            break;
                        case ADMIN_INDEX_SHOW_HISTORY:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_GET_HISTORY, "Lấy lịch sử", new String[]{"Tên nhân vật :", "Ngày/tháng/năm (dd/mm/YYYY) : "});
                            break;
                        case ADMIN_INDEX_FIND_ITEM_LVL_10:
                            sendMenu(MENU_SHOW_ALL_PLAYER_HAVE_ITEM_LVL_10, player);
                            break;
                        case ADMIN_INDEX_BUFF_ENCHANT:
                           player.controller.showInputDialog(INPUT_TYPE_NAME_TO_BUFF_ENCHANT, "Buff đập đồ", new String[]{"Tên nhân vật :"});
                            break;
                    }
                }
                break;
            case MENU_ADMIN_MAP:
                if (player.checkIsAdmin()) {
                    MapManager.mapArr.get(index).addRandom(player);
                }
                break;

            case MENU_SELECT_TYPE_CHANGE_GIFT: {
                int count = 1;
                switch (index) {
                    case 0:
                        count = 1;
                        break;
                    case 1:
                        count = 5;
                        break;
                }

                int price = count * GopetManager.PRICE_SILVER_BAR_CHANGE_GIFT;
                if (player.controller.checkSilverBar(price)) {
                    player.controller.mineSilverBar(price);
                    ArrayList<Popup> arrayList = new();
                    for (int i = 0; i < count; i++) {
                        for (Popup popup : player.controller.onReiceiveGift(GopetManager.CHANGE_ITEM_DATA)) {
                            arrayList.add(popup);
                        }

                    }

                    ArrayList<String> txtInfo = new();
                    for (Popup petBattleText : arrayList) {
                        txtInfo.add(petBattleText.getText());
                    }

                    player.okDialog(Utilities.Format("Chúc mừng bạn nhận được: %s", String.Join(",", txtInfo.toArray(new String[0]))));
                } else {
                    player.controller.notEnoughSilverBar();
                }
            }
            break;

            case MENU_SELECT_TYPE_UPGRADE_DUTY: {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    ClanMember memberSelect = clan.getMemberByUserId((int) player.controller.objectPerformed.get(OBJKEY_MEM_ID_UPGRADE_DUTY));
                    if (memberSelect == null) {
                        player.redDialog("Người chơi này không còn trong bang hội");
                    } else if (clanMember == memberSelect) {
                        player.redDialog("Không thể thao tác trên chính bản thân của mình");
                    } else {
                        player.controller.objectPerformed.put(OBJKEY_INDEX_MENU_UPGRADE_DUTY, index);
                        switch (index) {
                            case 0:
                                if (clanMember.duty == Clan.TYPE_LEADER) {
                                    showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, Utilities.Format("Bạn có chắc muốn nhường chúc vụ bang chủ cho người chơi %s không?", memberSelect.name), player);
                                } else {
                                    player.redDialog("Bạn không phải bang chủ!");
                                }
                                break;
                            case 1:
                                if (clanMember.duty == Clan.TYPE_LEADER) {
                                    showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, Utilities.Format("Bạn có chắc muốn phong chúc vụ bang phó cho người chơi %s không?", memberSelect.name), player);
                                } else {
                                    player.redDialog("Bạn không phải bang chủ!");
                                }
                                break;

                            case 2:
                                if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER) {
                                    showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, Utilities.Format("Bạn có chắc muốn phong chúc vụ trưởng lão cho người chơi %s không?", memberSelect.name), player);
                                } else {
                                    player.redDialog("Bạn không có quyền này!");
                                }
                                break;

                            case 3:
                                if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER) {
                                    showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, Utilities.Format("Bạn có chắc muốn phong chúc vụ thành viên cho người chơi %s không?", memberSelect.name), player);
                                } else {
                                    player.redDialog("Bạn không có quyền này!");
                                }
                                break;

                            case 4:
                                if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER || clanMember.duty == Clan.TYPE_SENIOR) {
                                    showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, Utilities.Format("Bạn có chắc muốn đuổi người chơi %s không?", memberSelect.name), player);
                                } else {
                                    player.redDialog("Bạn không có quyền này!");
                                }
                                break;
                        }
                    }
                } else {
                    player.controller.notClan();
                }
            }
            break;

            case MENU_UPGRADE_MEMBER_DUTY: {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    ClanMember memberSelect = clan.getMemberByUserId(index);
                    if (memberSelect == null) {
                        player.redDialog("Người chơi này không còn trong bang hội");
                    } else if (clanMember == memberSelect) {
                        player.redDialog("Không thể thao tác trên chính bản thân của mình");
                    } else {
                        player.controller.objectPerformed.put(OBJKEY_MEM_ID_UPGRADE_DUTY, index);
                        ArrayList<Option> list = new();
                        if (clanMember.duty == Clan.TYPE_LEADER) {
                            list.add(new Option(0, "Nhường chức", 1));
                            list.add(new Option(1, "Phong làm phó bang", 1));
                            list.add(new Option(2, "Phong làm trưởng lão", 1));
                            list.add(new Option(3, "Phong làm thành viên", 1));
                        } else if (clanMember.duty == Clan.TYPE_DEPUTY_LEADER) {
                            list.add(new Option(2, "Phong làm trưởng lão", 1));
                            list.add(new Option(3, "Phong làm thành viên", 1));
                        }

                        if (clanMember.duty != Clan.TYPE_NORMAL) {
                            list.add(new Option(4, "Đuổi", 1));
                        }

                        player.controller.sendListOption(MENU_SELECT_TYPE_UPGRADE_DUTY, "Phong tước?", CMD_CENTER_OK, list);
                    }
                } else {
                    player.controller.notClan();
                }
            }
            break;

            default: {
                player.redDialog("KHONG TON TAI MENU NAY");
                Thread.Sleep(1000);
            }
        }
    }

    public static void showNpcOption(int npcId, Player player)   {
        NpcTemplate npcTemplate = GopetManager.npcTemplate.get(npcId);
        if (npcTemplate != null) {
            Message ms = new Message(GopetCMD.COMMAND_GUIDER);
            ms.putsbyte(GopetCMD.NPC_OPTION);
            ms.putInt(npcId);
            int[] optionId = npcTemplate.getOptionId();
            String[] optionName = npcTemplate.getOptionName();
            int LengthOP = optionName.Length;
            ArrayList<TaskTemplate> taskTemplates = player.controller.getTaskCalculator().getTaskTemplate(npcId);
            if (taskTemplates.Count > 0) {
                LengthOP++;
            }
            ms.putInt(LengthOP);
            for (int i = 0; i < optionName.Length; i++) {
                ms.putInt(optionId[i]);
                ms.putUTF(optionName[i]);
            }

            if (taskTemplates.Count > 0) {
                ms.putInt(MenuController.OP_MAIN_TASK);
                ms.putUTF("Nhận nhiệm vụ chính");
                player.controller.objectPerformed.put(OBJKEY_NPC_ID_FOR_MAIN_TASK, npcId);
            }
            ms.cleanup();
            player.session.sendMessage(ms);
        }
    }

    private static PetSkill[] getPetSkills(Player player) {
        ArrayList<PetSkill> petSkills = GopetManager.NCLASS_PETSKILL_HASH_MAP.get(player.playerData.petSelected.getPetTemplate().getNclass());
        return petSkills.toArray(new PetSkill[0]);
    }

    static void selectNpcOption(int option, Player player)   {
        switch (option) {
            case OP_MAIN_TASK -> {
                if (player.controller.objectPerformed.containsKey(OBJKEY_NPC_ID_FOR_MAIN_TASK)) {
                    sendMenu(MenuController.MENU_SHOW_LIST_TASK, player);
                }
            }

            case OP_SHOP_PET ->
                showShop(SHOP_PET, player);
            case OP_LIST_PET_FREE ->
                sendMenu(MENU_LIST_PET_FREE, player);
            case OP_DELETE_TIEM_NANG ->
                sendMenu(MENU_DELETE_TIEM_NANG, player);
            case OP_TOP_PET ->
                showTop(TopPet.instance, player);
            case OP_TOP_GOLD ->
                showTop(TopGold.instance, player);
            case OP_TOP_GEM ->
                showTop(TopGem.instance, player);
            case OP_TOP_SPEND_GOLD -> {
                showTop(TopSpendGold.instance, player);
            }
            case OP_CHALLENGE -> {
                if (player.checkStar(GopetManager.STAR_JOIN_CHALLENGE)) {
                    player.MineStar(GopetManager.STAR_JOIN_CHALLENGE);
                    MapManager.maps.get(12).addRandom(player);
                } else {
                    player.notEnoughStar();
                }
            }
            case OP_SHOP_ARENA -> {
                sendMenu(SHOP_ARENA, player);
            }

            case OP_TYPE_GIFT_CODE -> {
                player.controller.showInputDialog(INPUT_TYPE_GIFT_CODE, "Nhập mã quà tặng", new String[]{"Giftcode:  "});
            }

            case OP_UPGRADE_PET -> {
                player.controller.setPetUpgradeInfo(new PetUpgradeInfo());
                player.controller.showUpgradePet();
            }
            case OP_MERGE_PART_PET ->
                sendMenu(MENU_MERGE_PART_PET, player);
            case OP_KIOSK_HAT ->
                sendMenu(MENU_KIOSK_HAT, player);
            case OP_KIOSK_WEAPON ->
                sendMenu(MENU_KIOSK_WEAPON, player);
            case OP_KIOSK_AMOUR ->
                sendMenu(MENU_KIOSK_AMOUR, player);
            case OP_KIOSK_GEM ->
                sendMenu(MENU_KIOSK_GEM, player);
            case OP_KIOSK_PET ->
                sendMenu(MENU_KIOSK_PET, player);
            case OP_KIOSK_OHTER ->
                sendMenu(MENU_KIOSK_OHTER, player);
            case OP_SHOP_THUONG_NHAN_AND_XOA_XAM ->
                sendMenu(SHOP_THUONG_NHAN, player);
            case OP_OWNER_KIOSK_HAT, OP_OWNER_KIOSK_WEAPON, OP_OWNER_KIOSK_AMOUR, OP_OWNER_KIOSK_GEM, OP_OWNER_KIOSK_PET, OP_OWNER_KIOSK_OHTER -> {
                sbyte typeKiosk = 0;
                switch (option) {
                    case OP_KIOSK_HAT ->
                        typeKiosk = GopetManager.KIOSK_HAT;
                    case OP_OWNER_KIOSK_WEAPON ->
                        typeKiosk = GopetManager.KIOSK_WEAPON;
                    case OP_OWNER_KIOSK_AMOUR ->
                        typeKiosk = GopetManager.KIOSK_AMOUR;
                    case OP_OWNER_KIOSK_GEM ->
                        typeKiosk = GopetManager.KIOSK_GEM;
                    case OP_OWNER_KIOSK_PET ->
                        typeKiosk = GopetManager.KIOSK_PET;
                    case OP_OWNER_KIOSK_OHTER ->
                        typeKiosk = GopetManager.KIOSK_OTHER;
                }
                player.controller.showKiosk(typeKiosk);
            }
            case OP_REVIVAL_PET_AFTER_PK -> {
                Pet pet = player.getPet();
                if (pet != null) {
                    if (pet.petDieByPK) {
                        if (player.checkCoin(GopetManager.PRICE_REVIVAL_PET_FATER_PK)) {
                            player.mineCoin(GopetManager.PRICE_REVIVAL_PET_FATER_PK);
                            pet.petDieByPK = false;
                            player.okDialog(Utilities.Format("Hồi sinh %s thành công, nhờ trân trọng nó nhé'", pet.getNameWithStar()));
                        } else {
                            player.controller.notEnoughCoin();
                        }
                    } else {
                        player.redDialog("Thứ cưng của bạn vẫn bình thường");
                    }
                } else {
                    player.petNotFollow();
                }
            }
            case OP_UPGRADE_STAR_PET ->
                sendMenu(MENU_SELECT_ITEM_PART_FOR_STAR_PET, player);
            case OP_PET_TATOO ->
                player.controller.showPetTattoUI();
            case OP_SHOW_GEM_INVENTORY ->
                player.controller.showGemInvenstory();
            case OP_MERGE_ITEM -> {
                sendMenu(MENU_MERGE_PART_ITEM, player);
            }
            case OP_CREATE_CLAN -> {
                player.controller.showInputDialog(INPUT_DIALOG_CREATE_CLAN, Utilities.Format("Tạo bang hội (Phí %s (ngoc) + %s (vang)", Utilities.formatNumber(GopetManager.COIN_CREATE_CLAN), Utilities.formatNumber(GopetManager.GOLD_CREATE_CLAN)), new String[]{"Tên bang hội: "});
            }

            case OP_NUM_OF_TASK -> {
                player.okDialog(Utilities.Format("Số nhiệm vụ đã hoàn thành là: ", player.playerData.wasTask.Count));
            }

            case OP_TOP_LVL_CLAN -> {
                showTop(TopLVLClan.instance, player);
            }
            case OP_ENTER_CLAN_PLACE -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    clanMember.getClan().getClanPlace().add(player);
                } else {
                    player.redDialog("Bạn chưa vào bang");
                }
            }
            case OP_EVENT_OF_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    player.okDialog("Bang hội hiện chưa có sự kiện gì...");
                } else {
                    player.controller.notClan();
                }
            }

            case OP_FAST_INFO_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    player.okDialog(Utilities.Format("Bạn đã quyên góp quỹ được:%s\n Điểm cống hiến:%s", Utilities.formatNumber(clanMember.fundDonate), Utilities.formatNumber(clanMember.growthPointDonate)));
                } else {
                    player.controller.notClan();
                }
            }

            case OP_APPROVAL_CLAN_MEMBER -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    if (clanMember.duty != Clan.TYPE_NORMAL) {
                        sendMenu(MENU_APPROVAL_CLAN_MEMBER, player);
                    } else {
                        player.redDialog("Bạn chỉ là thành viên.");
                    }
                } else {
                    player.controller.notClan();
                }
            }

            case OP_OUT_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    if (clanMember.duty == Clan.TYPE_LEADER) {
                        player.redDialog("Bang chủ không thể rời bang.");
                    } else {
                        clanMember.getClan().outClan(clanMember);
                        player.playerData.clanId = -1;
                        player.session.close();
                    }
                } else {
                    player.controller.notClan();
                }
            }

            case OP_UPGRADE_MAIN_HOUSE -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    if (clanMember.duty == Clan.TYPE_LEADER) {
                        showYNDialog(DIALOG_ASK_REQUEST_UPGRADE_MAIN_HOUSE, Utilities.Format("Bạn có muốn năng cấp nhà chính lên cấp %s không?", clanMember.getClan().getLvl() + 1), player);
                    } else {
                        player.redDialog("Bạn không có quyền này, chỉ có bang chủ mới thao tác được.");
                    }
                } else {
                    player.controller.notClan();
                }
            }

            case OP_SHOW_ALL_ITEM -> {
                sendMenu(MENU_SHOW_ALL_ITEM, player);
                break;
            }

            case OP_CHANGE_SLOGAN_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    if (clanMember.duty == Clan.TYPE_LEADER) {
                        player.controller.showInputDialog(INPUT_DIALOG_CHANGE_SLOGAN_CLAN, "Thay đổi khẩu hiệu bang hội", new String[]{"Khẩu hiệu: "});
                    } else {
                        player.redDialog("Bạn không có quyền này, chỉ có bang chủ mới thao tác được.");
                    }
                } else {
                    player.controller.notClan();
                }
            }

            case OP_CHANGE_GIFT -> {
                sendMenu(MENU_SELECT_TYPE_CHANGE_GIFT, player);
            }

            case OP_LIST_GIFT -> {
                sendMenu(MENU_LIST_GIFT, player);
            }
            case OP_PLUS_CLAN_BUFF -> {
                sendMenu(MENU_PLUS_SKILL_CLAN, player);
            }

            case OP_UPGRADE_MEMBER_DUTY -> {
                sendMenu(MENU_UPGRADE_MEMBER_DUTY, player);
            }

            case OP_SHOP_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    if (clan.getLvl() >= 1) {
//                        Calendar calendar = new GregorianCalendar();
//                        calendar.setTimeInMillis(Utilities.CurrentTimeMillis);
//                        if (calendar.get(Calendar.DAY_OF_WEEK) == Calendar.SATURDAY && calendar.get(Calendar.HOUR_OF_DAY) >= 20 && calendar.get(Calendar.HOUR_OF_DAY) < 24) {
                        showShop(SHOP_CLAN, player);
//                        } else {
//                            player.redDialog("Cửa hàng bang hội chỉ mở vào thứ 7 từ 20-24h");
//                        }
                    } else {
                        player.redDialog("Cửa hàng bang hội chưa đạt cấp 1 trở lên");
                    }
                } else {
                    player.controller.notClan();
                }
            }

            case OP_UPGRADE_SKILL_HOUSE -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    if (clanMember.duty == Clan.TYPE_LEADER) {
                        int marketHouse = clan.getbaseMarketLvl();
                        ClanHouseTemplate clanHouseTemplate = GopetManager.clanSkillHouseTemp.get(marketHouse + 1);
                        if (clanHouseTemplate != null) {
                            showYNDialog(DIALOG_ASK_UPGRADE_SHOP_HOUSE, Utilities.Format("Bạn có chắc muốn nâng nhà kỹ năng bang hội không ? Cần %s quỹ và %s cống hiến", Utilities.formatNumber(clanHouseTemplate.getFundNeed()), Utilities.formatNumber(clanHouseTemplate.getGrowthPointNeed())), player);
                        } else {
                            player.redDialog("Nhà kỹ năng đã đạt cấp tối đa");
                        }
                    } else {
                        clan.notEngouhPermission(player);
                    }
                }
            }

            case OP_UPGRADE_SHOP_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    Clan clan = clanMember.getClan();
                    if (clanMember.duty == Clan.TYPE_LEADER) {
                        int marketHouse = clan.getbaseMarketLvl();
                        ClanHouseTemplate clanHouseTemplate = GopetManager.clanMarketHouseTemp.get(marketHouse + 1);
                        if (clanHouseTemplate != null) {
                            showYNDialog(DIALOG_ASK_UPGRADE_SHOP_HOUSE, Utilities.Format("Bạn có chắc muốn nâng shop bang hội không ? Cần %s quỹ và %s cống hiến", Utilities.formatNumber(clanHouseTemplate.getFundNeed()), Utilities.formatNumber(clanHouseTemplate.getGrowthPointNeed())), player);
                        } else {
                            player.redDialog("Shop đã đạt cấp tối đa");
                        }
                    } else {
                        clan.notEngouhPermission(player);
                    }
                }
            }

            default ->
                player.redDialog("Tính năng đang được xây dựng");
        }
    }

    public static void showTop(    Top top,     Player player)   {
        ArrayList<MenuItemInfo> menuItemInfos = new();
        for (TopData data : top.datas) {
            MenuItemInfo menuItemInfo = new MenuItemInfo(data.title, data.desc, data.imgPath, false);
            menuItemInfos.add(menuItemInfo);
        }
        player.controller.showMenuItem(-1, TYPE_MENU_NONE, top.name, menuItemInfos);
    }

    public static void showShop(  sbyte type, Player player)   {
        ShopTemplate shopTemplate = getShop(type, player);
        if (shopTemplate == null) {
            player.redDialog("Xảy ra lỗi hoặc do bạn vừa mới bị đá ra khỏi bang\nVui lòng thao tác lại");
            return;
        }
        ArrayList<MenuItemInfo> menuItemInfos = new();
        for (ShopTemplateItem shopTemplateItem : shopTemplate.getShopTemplateItems()) {
            ItemTemplate itemTemplate = shopTemplateItem.getItemTemplate();
            MenuItemInfo menuItemInfo = new MenuItemInfo(shopTemplateItem.getName(), shopTemplateItem.getDesc(player), shopTemplateItem.isSpceial() ? "npcs/fone2.png" : shopTemplateItem.getIconPath(), true);
            MenuItemInfo.PaymentOption[] paymentOptions = new MenuItemInfo.PaymentOption[shopTemplateItem.getMoneyType().Length];
            for (int i = 0; i < shopTemplateItem.getMoneyType().Length; i++) {
                sbyte b = shopTemplateItem.getMoneyType()[i];
                MenuItemInfo.PaymentOption paymentOption = new MenuItemInfo.PaymentOption(i, getMoneyText(b, shopTemplateItem.getPrice()[i]), checkMoney(b, shopTemplateItem.getPrice()[i], player) ? (sbyte) 1 : (sbyte) 0);
                paymentOptions[i] = paymentOption;
            }
            menuItemInfo.setShowDialog(true);
            menuItemInfo.setDialogText("Bạn có chắc muốn mua nó không");
            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
            menuItemInfo.setPaymentOptions(paymentOptions);
            menuItemInfo.setCloseScreenAfterClick(shopTemplateItem.isCloseScreenAfterClick());
            menuItemInfo.setHasId(shopTemplateItem.isHasId());
            menuItemInfo.setItemId(shopTemplateItem.getMenuId());
            menuItemInfos.add(menuItemInfo);
        }

        player.controller.showMenuItem(type, TYPE_MENU_PAYMENT, shopTemplate.getName(), menuItemInfos);
    }

    public const ShopTemplate getShop(  sbyte type, Player player)   {
        switch (type) {
            case SHOP_ARENA:
                if (player.playerData.shopArena != null) {
                    player.playerData.shopArena.nextWhenNewDay();
                    return player.playerData.shopArena;
                }
                ShopArena shopTemplate = new ShopArena(type);
                player.playerData.shopArena = shopTemplate;
                return shopTemplate;
            case SHOP_CLAN:
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    return clanMember.getClan().getShopClan();
                }
                return null;
            default:
                return GopetManager.shopTemplate.get(type);
        }
    }

    public static String getMoneyText(  sbyte type, long value) {
        String str = Utilities.formatNumber(value);
        switch (type) {
            case GopetManager.MONEY_TYPE_COIN ->
                str += " (ngoc)";
            case GopetManager.MONEY_TYPE_GOLD ->
                str += " (vang)";
            case GopetManager.MONEY_TYPE_SILVER_BAR ->
                str += " thỏi bạc";
            case GopetManager.MONEY_TYPE_GOLD_BAR ->
                str += " thỏi vàng";
            case GopetManager.MONEY_TYPE_BLOOD_GEM ->
                str += " huyết ngọc";
            case GopetManager.MONEY_TYPE_FUND_CLAN ->
                str += " điểm góp quỹ cá nhân";

            case GopetManager.MONEY_TYPE_GROWTH_POINT_CLAN ->
                str += " điểm cống hiến cá nhân";
        }
        return str;
    }

    public static bool checkMoney(  sbyte type, long value, Player player)   {
        switch (type) {
            case GopetManager.MONEY_TYPE_COIN -> {
                return player.checkCoin(value);
            }
            case GopetManager.MONEY_TYPE_GOLD -> {
                return player.checkGold(value);
            }
            case GopetManager.MONEY_TYPE_SILVER_BAR -> {
                return player.controller.checkSilverBar((int) value);
            }
            case GopetManager.MONEY_TYPE_GOLD_BAR -> {
                return player.controller.checkGoldBar((int) value);
            }

            case GopetManager.MONEY_TYPE_BLOOD_GEM -> {
                return player.controller.checkBloodGem((int) value);
            }

            case GopetManager.MONEY_TYPE_FUND_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    return clanMember.fundDonate >= value;
                } else {
                    return false;
                }
            }

            case GopetManager.MONEY_TYPE_GROWTH_POINT_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    return clanMember.growthPointDonate >= value;
                } else {
                    return false;
                }
            }
        }
        return false;
    }

    public static void addMoney(  sbyte typeMoney, int value, Player player)   {
        switch (typeMoney) {
            case GopetManager.MONEY_TYPE_COIN ->
                player.addCoin(value);
            case GopetManager.MONEY_TYPE_GOLD ->
                player.addGold(value);
            case GopetManager.MONEY_TYPE_SILVER_BAR ->
                player.controller.addSilverBar(value);
            case GopetManager.MONEY_TYPE_GOLD_BAR ->
                player.controller.addGoldBar(value);
            case GopetManager.MONEY_TYPE_BLOOD_GEM ->
                player.controller.addBloodGem(value);

            case GopetManager.MONEY_TYPE_FUND_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    clanMember.fundDonate += value;
                }
            }

            case GopetManager.MONEY_TYPE_GROWTH_POINT_CLAN -> {
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null) {
                    clanMember.growthPointDonate += value;
                }
            }
        }
    }

    public static void showInventory(Player player, sbyte typeInventory, int menuId, String title)   {
        CopyOnWriteArrayList<Item> items = (CopyOnWriteArrayList<Item>) player.playerData.getInventoryOrCreate(typeInventory).clone();

        int i = 0;
        ArrayList<MenuItemInfo> menuList = new();
        switch (typeInventory) {
            case GopetManager.SKIN_INVENTORY: {
                Item it = player.playerData.skinItem;
                if (it != null) {
                    i = -1;
                    items.add(0, it);
                }
            }
            break;

            case GopetManager.WING_INVENTORY: {
                Item it = player.playerData.wingItem;
                if (it != null) {
                    i = -1;
                    items.add(0, it);
                }
            }
            break;
        }
        for (Item item : items) {
            ItemTemplate itemTemplate = item.getTemp();
            MenuItemInfo menuItemInfo = new MenuItemInfo(typeInventory == GopetManager.EQUIP_PET_INVENTORY ? item.getEquipName() : item.getName(), item.getDescription(player), "", true);
            menuItemInfo.setImgPath(itemTemplate.getIconPath());
            menuItemInfo.setShowDialog(true);
            menuItemInfo.setDialogText(Utilities.Format("Bạn có muốn chọn %s không?", itemTemplate.getName()));
            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
            menuItemInfo.setCloseScreenAfterClick(true);
            menuItemInfo.setHasId(true);
            menuItemInfo.setItemId(i);
            if (i == -1) {
                menuItemInfo.setTitleMenu(menuItemInfo.getTitleMenu() + " (Đang sử dụng)");
            }
            menuList.add(menuItemInfo);
            i++;
        }
        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, title, menuList);
    }

    public static void showYNDialog(int dialogId, String text, Player player)   {
        Message m = new Message(GopetCMD.SERVER_MESSAGE);
        m.putsbyte(GopetCMD.SEND_YES_NO);
        m.putInt(dialogId);
        m.putUTF(text);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public static void answerYesNo(int dialogId, bool ok, Player player)   {
        if (ok) {
            Object obj;
            switch (dialogId) {
                case DIALOG_CONFIRM_REMOVE_ITEM_EQUIP -> {
                    obj = player.controller.objectPerformed.get(OBJKEY_REMOVE_ITEM_EQUIP);
                    if (obj != null) {
                        player.controller.removeItemEquip((int) obj);
                        player.controller.objectPerformed.remove(OBJKEY_REMOVE_ITEM_EQUIP);
                    }
                }
                case DIALOG_CONFIRM_BUY_KIOSK_ITEM -> {
                    obj = player.controller.objectPerformed.get(OBJKEY_KIOSK_ITEM);
                    if (obj != null) {
                        Map.Entry<Kiosk, SellItem> objENtry = (Map.Entry<Kiosk, SellItem>) obj;
                        player.controller.objectPerformed.remove(OBJKEY_KIOSK_ITEM);
                        objENtry.getKey().confirmBuy(player, objENtry.getValue());
                    }
                }
                case DIALOG_ENCHANT ->
                    player.controller.enchantItem();
                case DIALOG_UP_TIER_ITEM ->
                    player.controller.upTierItem();
                case DIALOG_UP_SKILL ->
                    player.controller.upSkill();

                case DIALOG_INVITE_CHALLENGE -> {
                    player.controller.startChallenge();
                }

                case DIALOG_CONFIRM_REMOVE_GEM -> {
                    player.controller.removeGem((int) player.controller.objectPerformed.get(OBJKEY_ID_GEM_REMOVE));
                }

                case DIALOG_ASK_KEEP_GEM -> {
                    if (player.checkGold(GopetManager.PRICE_KEEP_GEM)) {
                        player.controller.objectPerformed.put(OBJKEY_IS_KEEP_GOLD, true);
                        MenuController.showYNDialog(MenuController.DIALOG_UP_TIER_ITEM, (String) player.controller.objectPerformed.get(OBJKEY_ASK_UP_TIER_GEM_STR), player);
                    } else {
                        player.controller.notEnoughGold();
                        return;
                    }
                }

                case DIALOG_ASK_REMOVE_GEM -> {
                    player.controller.confirmUnequipGem((int) player.controller.objectPerformed.get(OBJKEY_ID_ITEM_REMOVE_GEM));
                    player.controller.objectPerformed.remove(OBJKEY_ID_ITEM_REMOVE_GEM);
                }

                case DIALOG_ASK_FAST_REMOVE_GEM -> {
                    player.controller.confirmUnequipFastGem((int) player.controller.objectPerformed.get(OBJKEY_ID_ITEM_FAST_REMOVE_GEM));
                    player.controller.objectPerformed.remove(OBJKEY_ID_ITEM_FAST_REMOVE_GEM);
                }

                case DIALOG_ASK_REQUEST_JOIN_CLAN -> {
                    player.controller.requestJoinClan((String) player.controller.objectPerformed.get(OBJKEY_CLAN_NAME_REQUEST));
                    player.controller.objectPerformed.remove(OBJKEY_CLAN_NAME_REQUEST);
                }
                case DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN -> {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null) {
                        Clan clan = clanMember.getClan();
                        ClanMember memberSelect = clan.getMemberByUserId((int) player.controller.objectPerformed.get(OBJKEY_MEM_ID_UPGRADE_DUTY));
                        int index = (int) player.controller.objectPerformed.get(OBJKEY_INDEX_MENU_UPGRADE_DUTY);
                        if (memberSelect == null) {
                            player.redDialog("Người chơi này không còn trong bang hội");
                        } else if (clanMember == memberSelect) {
                            player.redDialog("Không thể thao tác trên chính bản thân của mình");
                        } else {
                            player.controller.objectPerformed.put(OBJKEY_INDEX_MENU_UPGRADE_DUTY, index);
                            switch (index) {
                                case 0:
                                    if (clanMember.duty == Clan.TYPE_LEADER) {
                                        memberSelect.duty = clanMember.duty;
                                        clanMember.duty = Clan.TYPE_NORMAL;
                                        player.okDialog("Nhường chức thành công");
                                        Player onPlayer = PlayerManager.get(memberSelect.name);
                                        if (onPlayer != null) {
                                            onPlayer.okDialog("Bạn đã được lên làm bang chủ bang hội");
                                        }
                                    } else {
                                        player.redDialog("Bạn không phải bang chủ!");
                                    }
                                    break;
                                case 1:
                                    if (clanMember.duty == Clan.TYPE_LEADER) {
                                        if (clan.checkDuty(clan.TYPE_DEPUTY_LEADER)) {
                                            memberSelect.duty = clan.TYPE_DEPUTY_LEADER;
                                            player.okDialog("Phong chức thành công");
                                            Player onPlayer = PlayerManager.get(memberSelect.name);
                                            if (onPlayer != null) {
                                                onPlayer.okDialog("Bạn đã được lên làm phó bang chủ bang hội");
                                            }
                                        } else {
                                            clan.showFullDuty(player);
                                        }
                                    } else {
                                        player.redDialog("Bạn không phải bang chủ!");
                                    }
                                    break;

                                case 2:
                                    if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER) {
                                        if (clan.checkDuty(clan.TYPE_SENIOR)) {
                                            memberSelect.duty = clan.TYPE_SENIOR;
                                            player.okDialog("Phong chức thành công");
                                            Player onPlayer = PlayerManager.get(memberSelect.name);
                                            if (onPlayer != null) {
                                                onPlayer.okDialog("Bạn đã được lên làm trưởng lão bang hội");
                                            }
                                        } else {
                                            clan.showFullDuty(player);
                                        }
                                    } else {
                                        player.redDialog("Bạn không có quyền này!");
                                    }
                                    break;
                                case 3:
                                    if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER) {
                                        memberSelect.duty = clan.TYPE_NORMAL;
                                        player.okDialog("Phong chức thành công");
                                        Player onPlayer = PlayerManager.get(memberSelect.name);
                                        if (onPlayer != null) {
                                            onPlayer.okDialog("Bạn đã bị hạ cấp về thành viên bình thường trong bang hội !");
                                        }
                                    } else {
                                        player.redDialog("Bạn không có quyền này!");
                                    }
                                    break;

                                case 4:
                                    if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER || clanMember.duty == Clan.TYPE_SENIOR) {
                                        if (clanMember.duty != Clan.TYPE_NORMAL) {
                                            if (clanMember.duty < memberSelect.duty) {
                                                clan.kick(memberSelect.user_id);
                                                player.okDialog("Đuổi thành công");
                                            } else {
                                                player.redDialog("Bạn không thể đuổi người có chức vụ cao hơn");
                                            }
                                        }
                                    } else {
                                        player.redDialog("Bạn không có quyền này!");
                                    }
                                    break;
                            }
                        }
                    } else {
                        player.controller.notClan();
                    }
                }

                case DIALOG_ASK_REQUEST_UPGRADE_MAIN_HOUSE -> {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null) {
                        if (clanMember.duty == Clan.TYPE_LEADER) {
                            Clan clan = clanMember.getClan();
                            if (clan.getLvl() < GopetManager.CLAN_MAX_LVL) {
                                ClanTemplate clanTemplate = GopetManager.clanTemp.get(clan.getLvl() + 1);
                                if (clanTemplate != null) {
                                    if (clan.checkFund(clanTemplate.getFundNeed()) && clan.checkGrowthPoint(clanTemplate.getGrowthPointNeed())) {
                                        clan.mineFund(clanTemplate.getFundNeed());
                                        clan.mineGrowthPoint(clanTemplate.getGrowthPointNeed());
                                        clan.setTemplate(clanTemplate);
                                    } else {
                                        player.redDialog("Không đủ điểm");
                                    }
                                } else {
                                    player.redDialog("ERROR AT UPGRADE MAIN HOUSE , PLEASE SEND THIS MESSAGE TO DEV OF GOPET");
                                }
                            } else {
                                player.redDialog("Nhà chính đạt cấp tối đa");
                            }
                        } else {
                            player.redDialog("Bạn không có quyền này, chỉ có bang chủ mới thao tác được.");
                        }
                    } else {
                        player.controller.notClan();
                    }
                }

                case DIALOG_ASK_UPGRADE_SHOP_HOUSE -> {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null) {
                        Clan clan = clanMember.getClan();
                        if (clanMember.duty == Clan.TYPE_LEADER) {
                            int marketHouse = clan.getbaseMarketLvl();
                            ClanHouseTemplate clanHouseTemplate = GopetManager.clanMarketHouseTemp.get(marketHouse + 1);
                            if (clanHouseTemplate != null) {
                                if (clan.getLvl() >= clanHouseTemplate.getNeedClanLvl()) {
                                    if (clan.checkFund(clanHouseTemplate.getFundNeed()) && clan.checkGrowthPoint(clanHouseTemplate.getGrowthPointNeed())) {
                                        clan.mineFund(clanHouseTemplate.getFundNeed());
                                        clan.mineGrowthPoint(clanHouseTemplate.getGrowthPointNeed());
                                        clan.setbaseMarketLvl(marketHouse + 1);
                                        player.okDialog(Utilities.Format("Shop của bang hội bạn đã lên cấp: %s", clan.getSkillHouseLvl()));
                                    } else {
                                        player.redDialog("Bang hội không đủ điểm");
                                    }
                                } else {
                                    player.redDialog(Utilities.Format("Bang hội cần đạt cấp %s", clanHouseTemplate.getNeedClanLvl()));
                                }
                            } else {
                                player.redDialog("Shop đã đạt cấp tối đa");
                            }
                        } else {
                            clan.notEngouhPermission(player);
                        }
                    }
                }

                case DIALOG_ASK_UPGRADE_SKILL_HOUSE -> {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null) {
                        Clan clan = clanMember.getClan();
                        if (clanMember.duty == Clan.TYPE_LEADER) {
                            int skillHouse = clan.getSkillHouseLvl();
                            ClanHouseTemplate clanHouseTemplate = GopetManager.clanSkillHouseTemp.get(skillHouse + 1);
                            if (clanHouseTemplate != null) {
                                if (clan.checkFund(clanHouseTemplate.getFundNeed()) && clan.checkGrowthPoint(clanHouseTemplate.getGrowthPointNeed())) {
                                    clan.mineFund(clanHouseTemplate.getFundNeed());
                                    clan.mineGrowthPoint(clanHouseTemplate.getGrowthPointNeed());
                                    clan.setSkillHouseLvl(skillHouse + 1);
                                    player.okDialog(Utilities.Format("Nhà kỹ năng của bang hội bạn đã lên cấp: %s", clan.getSkillHouseLvl()));
                                } else {
                                    player.redDialog("Bang hội không đủ điểm");
                                }
                            } else {
                                player.redDialog("Nhà kỹ năng đã đạt cấp tối đa");
                            }
                        } else {
                            clan.notEngouhPermission(player);
                        }
                    }
                }

                default -> {
                    player.redDialog("khong ton tai dialog nay");
                    Thread.Sleep(1000);
                }
            }
        } else {
            switch (dialogId) {
                case DIALOG_ASK_KEEP_GEM:
                    player.controller.objectPerformed.put(OBJKEY_IS_KEEP_GOLD, false);
                    MenuController.showYNDialog(MenuController.DIALOG_UP_TIER_ITEM, (String) player.controller.objectPerformed.get(OBJKEY_ASK_UP_TIER_GEM_STR), player);
                    break;
            }
        }
    }

    public static void inputDialog(int dialogInputId, InputReader reader, Player player)   {
        try {
            switch (dialogInputId) {
                case INPUT_DIALOG_KIOSK:
                    int priceItem = reader.readInt(0);
                    if (priceItem <= 0) {
                        player.redDialog("Tính bug ha gì!");
                        return;
                    }
                    if (priceItem > 2000000000) {
                        player.redDialog(Utilities.Format("Giới hạn (ngoc) là %s", Utilities.formatNumber(2000000000)));
                        return;
                    }

                    if (player.controller.objectPerformed.containsKey(OBJKEY_SELECT_SELL_ITEM) && player.controller.objectPerformed.containsKey(OBJKEY_MENU_OF_KIOSK)) {
                        int menuKioskId = (int) player.controller.objectPerformed.get(OBJKEY_MENU_OF_KIOSK);
                        Item item = null;
                        Pet pet = null;
                        if (menuKioskId != MENU_KIOSK_PET_SELECT) {
                            item = (Item) player.controller.objectPerformed.get(OBJKEY_SELECT_SELL_ITEM);
                        } else if (menuKioskId == MENU_KIOSK_PET_SELECT) {
                            pet = (Pet) player.controller.objectPerformed.get(OBJKEY_SELECT_SELL_ITEM);
                        }

                        if (item == null && pet == null) {
                            return;
                        }

                        if (item != null) {
                            if (!item.getTemp().isCanTrade()) {
                                player.redDialog("Vật phẩm này không thể giao dịch");
                                return;
                            }
                        }

                        player.controller.objectPerformed.remove(OBJKEY_SELECT_SELL_ITEM);
                        player.controller.objectPerformed.remove(OBJKEY_MENU_OF_KIOSK);
                        int count = 1;
                        if (player.controller.objectPerformed.containsKey(OBJKEY_COUNT_OF_ITEM_KIOSK)) {
                            count = (int) player.controller.objectPerformed.get(OBJKEY_COUNT_OF_ITEM_KIOSK);
                        }
                        MarketPlace marketPlace = (MarketPlace) player.getPlace();
                        switch (menuKioskId) {
                            case MENU_KIOSK_PET_SELECT:
                                player.playerData.pets.remove(pet);
                                marketPlace.getKiosk(GopetManager.KIOSK_PET).addKioskItem(pet, priceItem, player);
                                player.controller.showKiosk(GopetManager.KIOSK_PET);
                                break;
                            case MENU_KIOSK_HAT_SELECT:
                            case MENU_KIOSK_WEAPON_SELECT:
                            case MENU_KIOSK_AMOUR_SELECT:
                            case MENU_KIOSK_OHTER_SELECT:
                            case MENU_KIOSK_GEM_SELECT:
                                player.playerData.removeItem(menuKioskId != MENU_KIOSK_GEM_SELECT ? GopetManager.EQUIP_PET_INVENTORY : GopetManager.GEM_INVENTORY, item);
                                switch (menuKioskId) {
                                    case MENU_KIOSK_HAT_SELECT:
                                        marketPlace.getKiosk(GopetManager.KIOSK_HAT).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_HAT);
                                        break;
                                    case MENU_KIOSK_GEM_SELECT:
                                        marketPlace.getKiosk(GopetManager.KIOSK_GEM).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_GEM);
                                        break;
                                    case MENU_KIOSK_WEAPON_SELECT:
                                        marketPlace.getKiosk(GopetManager.KIOSK_WEAPON).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_WEAPON);
                                        break;
                                    case MENU_KIOSK_AMOUR_SELECT:
                                        marketPlace.getKiosk(GopetManager.KIOSK_AMOUR).addKioskItem(item, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_AMOUR);
                                        break;
                                    case MENU_KIOSK_PET_SELECT:
                                        marketPlace.getKiosk(GopetManager.KIOSK_PET).addKioskItem(pet, priceItem, player);
                                        player.controller.showKiosk(GopetManager.KIOSK_PET);
                                        break;
                                    case MENU_KIOSK_OHTER_SELECT:
                                        if (GameController.checkCount(item, count)) {
                                            Item itemCopy = new Item(item.itemTemplateId);
                                            itemCopy.count = count;
                                            marketPlace.getKiosk(GopetManager.KIOSK_OTHER).addKioskItem(itemCopy, priceItem, player);
                                            player.controller.showKiosk(GopetManager.KIOSK_OTHER);
                                            player.controller.subCountItem(item, count, GopetManager.NORMAL_INVENTORY);
                                        } else {
                                            player.redDialog("Số lượng không đủ");
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                    break;
                case INPUT_TYPE_GIFT_CODE: {
                    if (!player.controller.canTypeGiftCode()) {
                        player.redDialog("Thao tác quá nhanh vui lòng chờ tí!");
                        return;
                    }
                    String code = reader.readString(0);
                    if (!code.isEmpty()) {
                        if (Utilities.CheckString(code, "^[a-z0-9A-Z]+$")) {
                            player.okDialog("Vui lòng chờ");
                            MySqlConnection MySqlConnection = MYSQLManager.create();
                            try {
                                ResultSet keyLOCK = MYSQLManager.jquery(Utilities.Format("SELECT GET_LOCK('gift_code_lock_%s', 10) as hasLock;", code), MySqlConnection);
                                if (keyLOCK.next()) {
                                    bool hasLock = keyLOCK.getbool("hasLock");
                                    if (!hasLock) {
                                        player.redDialog("Quá nhiều người cố gắng dùng mã quà tặng này nên hệ thống quá tải!");
                                    } else {
                                        keyLOCK.close();
                                        ResultSet resultSet = MYSQLManager.jquery(Utilities.Format("SELECT * FROM `gift_code` WHERE `gift_code`.`code` = '%s';", code), MySqlConnection);
                                        if (resultSet.next()) {
                                            GiftCodeData giftCodeData = new GiftCodeData(resultSet);
                                            if (giftCodeData.getUsersOfUseThis().contains(player.user.user_id)) {
                                                player.redDialog("Bạn đã sử dụng mã quà tặng này rồi");
                                            } else {
                                                if (giftCodeData.getCurUser() >= giftCodeData.getMaxUser()) {
                                                    player.redDialog("Số người dùng mã quà tặng này đã đạt giới hạn!");
                                                } else {
                                                    if (giftCodeData.getExpire().getTime() < Utilities.CurrentTimeMillis) {
                                                        player.redDialog("Mã quà tặng đã hết hạn");
                                                    } else {
                                                        giftCodeData.getUsersOfUseThis().add(player.user.user_id);
                                                        giftCodeData.setCurUser(giftCodeData.getCurUser() + 1);
                                                        if (giftCodeData.getGift_data().Length <= 0) {
                                                            player.redDialog("Mã quà tặng này chả tặng bạn được cái gì :)");
                                                        } else {
                                                            ArrayList<Popup> popups = player.controller.onReiceiveGift(giftCodeData.getGift_data());
                                                            ArrayList<String> textInfo = new();
                                                            for (Popup popup : popups) {
                                                                textInfo.add(popup.getText());
                                                            }
                                                            player.okDialog(Utilities.Format("Chức mừng bạn nhận được: %s", String.Join(",", textInfo)));
                                                        }
                                                        resultSet.close();
                                                        MYSQLManager.updateSql(Utilities.Format("UPDATE `gift_code` SET `currentUser` = %s , `usersOfUseThis` = '%s' WHERE `id` = '%s';", giftCodeData.getCurUser(), JsonManager.ToJson(giftCodeData.getUsersOfUseThis()), giftCodeData.getId()), MySqlConnection);
                                                    }
                                                }
                                            }
                                        } else {
                                            player.redDialog("Không có mã quà tặng này");
                                        }
                                    }
                                } else {
                                    player.redDialog("Xảy ra sự cố mà ông trời cũng cả biết");
                                }
                            } catch (Exception e) {
                                e.printStackTrace();
                            }  ly {
                                MYSQLManager.updateSql(Utilities.Format("DO RELEASE_LOCK('gift_code_lock_%s');", code), MySqlConnection);
                                MySqlConnection.close();
                            }
                        } else {
                            player.redDialog("Có kí tự lạ");
                        }
                    } else {
                        player.redDialog("Vui lòng không bỏ trống");
                    }
                }
                break;
                case INPUT_DIALOG_CHALLENGE_INVITE:
                    int priceChallenge = reader.readInt(0);
                    if (priceChallenge <= 0) {
                        player.redDialog("Tính bug ha gì!");
                        return;
                    }
                    if (priceChallenge > 100000) {
                        player.redDialog(Utilities.Format("Giới hạn (ngoc) là %s", Utilities.formatNumber(100000)));
                        return;
                    }

                    player.controller.sendChallenge((Player) player.controller.objectPerformed.get(OBJKEY_INVITE_CHALLENGE_PLAYER), priceChallenge);

                    break;
                case INPUT_DIALOG_COUNT_OF_KISOK_ITEM: {
                    int count = reader.readInt(0);
                    if (count >= 1) {
                        player.controller.objectPerformed.put(OBJKEY_COUNT_OF_ITEM_KIOSK, count);
                        player.controller.showInputDialog(INPUT_DIALOG_KIOSK, "Định giá", new String[]{"  "}, new sbyte[]{0});
                    } else {
                        player.redDialog("Số lượng không hợp lệ");
                    }
                }
                break;
                case INPUT_DIALOG_CAPTCHA:
                    GopetCaptcha captcha = player.playerData.captcha;
                    if (captcha != null) {
                        if (captcha.getKey().equals(reader.readString(0))) {
                            player.playerData.captcha = null;
                            player.okDialog("Chính xác");
                        } else {
                            player.redDialog("Nhập mã sai");
                        }
                    }
                    break;
                case INPUT_DIALOG_ADMIN_GET_ITEM: {
                    if (player.checkIsAdmin()) {
                        int itemTemplateId = reader.readInt(1);
                        int count = reader.readInt(0);
                        ItemTemplate itemTemplate = GopetManager.itemTemplate.get(itemTemplateId);
                        if (itemTemplate != null) {
                            if (itemTemplate.isStackable()) {
                                Item item = new Item(itemTemplateId);
                                item.count = count;
                                player.addItemToInventory(item);
                                player.okDialog(item.getName());
                            } else {
                                if (count < 1000) {
                                    for (int i = 0; i < count; i++) {
                                        Item item = new Item(itemTemplateId);
                                        item.count = 1;
                                        player.addItemToInventory(item);

                                    }
                                    player.okDialog(itemTemplate.getName() + " x" + count);
                                } else {
                                    player.redDialog("Vui lòng lấy vật phẩm với số lượng < 1000 với các vật phẩm không gộp\n Tránh trường hợp đốt cpu server");
                                }
                            }
                        } else {
                            player.redDialog("Không có item với id = " + itemTemplateId);
                        }
                    }
                }
                break;

                case INPUT_DIALOG_EXCHANGE_GOLD_TO_COIN: {
                    long value = Math.abs(reader.readLong(0));
                    if (player.checkGold(value)) {
                        player.mineGold(value);
                        long valueCoin = value * GopetManager.PERCENT_EXCHANGE_GOLD_TO_COIN;
                        player.addCoin(valueCoin);
                        player.okDialog(Utilities.Format("Chúc mừng bạn đổi thành công %s (ngoc)", Utilities.formatNumber(valueCoin)));
                    } else {
                        player.controller.notEnoughGold();
                    }
                }
                break;

                case INPUT_DIALOG_ADMIN_GET_HISTORY: {
                    if (player.checkIsAdmin()) {
                        String namePlayer = reader.readString(0);
                        Date date = new Date(reader.readString(1));
                        int user_id = 0;
                        MySqlConnection MySqlConnectionWeb = MYSQLManager.createWebMySqlConnection();
                        MySqlConnection MySqlConnectionPlayer = MYSQLManager.create();
                        try {
                            ResultSet resultSet = MYSQLManager.jquery("Select user_id from player where name ='" + namePlayer + "'", MySqlConnectionPlayer);
                            if (resultSet.next()) {
                                user_id = resultSet.getInt("user_id");
                                resultSet.close();

                            } else {
                                resultSet.close();
                                MySqlConnectionPlayer.close();
                                MySqlConnectionWeb.close();
                                player.redDialog("Người chơi này không tồn tại");
                                return;
                            }
                        } catch (Exception e) {
                        }
                        MySqlConnectionPlayer.close();
                        MySqlConnectionWeb.close();
                    }
                }
                break;

                case INPUT_DIALOG_ADMIN_CHAT_GLOBAL: {
                    if (player.checkIsAdmin()) {
                        String text = reader.readString(0);
                        PlayerManager.showBanner(text);
                    }
                }
                break;

                case INPUT_DIALOG_ADMIN_TELE_TO_PLAYER: {
                    if (player.checkIsAdmin()) {
                        String namePlayer = reader.readString(0);
                        Player playerPassive = PlayerManager.get(namePlayer);
                        if (playerPassive != null) {
                            GopetPlace gopetPlace = (GopetPlace) playerPassive.getPlace();
                            if (gopetPlace != null) {
                                gopetPlace.add(player);
                            }
                        } else {
                            player.redDialog("Người chơi đã offline");
                        }
                    }
                }
                break;

                case INPUT_DIALOG_ADMIN_UNLOCK_USER: {
                    if (player.checkIsAdmin()) {
                        String namePlayer = reader.readString(0);
                        int user_id = 0;
                        MySqlConnection MySqlConnectionWeb = MYSQLManager.createWebMySqlConnection();
                        MySqlConnection MySqlConnectionPlayer = MYSQLManager.create();
                        try {
                            ResultSet resultSet = MYSQLManager.jquery("Select user_id from player where name ='" + namePlayer + "'", MySqlConnectionPlayer);
                            if (resultSet.next()) {
                                user_id = resultSet.getInt("user_id");
                                resultSet.close();
                                MYSQLManager.updateSql("Update `user` set isBenned = 0 where user_id = " + user_id, MySqlConnectionWeb);
                            } else {
                                resultSet.close();
                                MySqlConnectionPlayer.close();
                                MySqlConnectionWeb.close();
                                player.redDialog("Người chơi này không tồn tại");
                                return;
                            }
                        } catch (Exception e) {
                        }
                        MySqlConnectionPlayer.close();
                        MySqlConnectionWeb.close();
                    }
                }
                break;

                case INPUT_DIALOG_ADMIN_LOCK_USER: {
                    if (player.checkIsAdmin()) {
                        String namePlayer = reader.readString(3);
                        sbyte typeLock = reader.readsbyte(1);
                        int min = reader.readInt(2);
                        String reason = reader.readString(0);
                        int user_id = 0;
                        MySqlConnection MySqlConnectionWeb = MYSQLManager.createWebMySqlConnection();
                        MySqlConnection MySqlConnectionPlayer = MYSQLManager.create();
                        try {
                            ResultSet resultSet = MYSQLManager.jquery("Select user_id from player where name ='" + namePlayer + "'", MySqlConnectionPlayer);
                            if (resultSet.next()) {
                                user_id = resultSet.getInt("user_id");
                                resultSet.close();
                            } else {
                                resultSet.close();
                                MySqlConnectionPlayer.close();
                                MySqlConnectionWeb.close();
                                player.redDialog("Người chơi này không tồn tại");
                                return;
                            }
                        } catch (Exception e) {
                        }
                        MySqlConnectionPlayer.close();
                        MySqlConnectionWeb.close();
                        UserData.banBySQL(typeLock, reason, Utilities.CurrentTimeMillis + (min * 1000L * 60), user_id);
                        Player playerPassive = PlayerManager.get(namePlayer);
                        if (playerPassive != null) {
                            playerPassive.session.close();
                        }
                    }
                }
                break;

                case INPUT_TYPE_NAME_TO_BUFF_ENCHANT: {
                    if (player.checkIsAdmin()) {
                        String name = reader.readString(0);
                        Player playerOnline = PlayerManager.get(name);
                        if (playerOnline != null) {
                            playerOnline.controller.setBuffEnchent(true);
                            player.okDialog(Utilities.Format("Buff cho người chơi đập không thất bại thành công!", name));
                        } else {
                            player.redDialog("Người chơi đã offline");
                        }
                    }
                }
                break;

                case INPUT_DIALOG_CHANGE_SLOGAN_CLAN: {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null) {
                        if (clanMember.duty == Clan.TYPE_LEADER) {
                            String slogan = reader.readString(0);
                            if (slogan.Length() >= 500) {
                                player.redDialog("Khẩu hiệu không quá 500 từ");
                            } else {
                                clanMember.getClan().setSlogan(slogan);
                            }
                        } else {
                            player.redDialog("Bạn không có quyền này, chỉ có bang chủ mới thao tác được.");
                        }
                    } else {
                        player.controller.notClan();
                    }
                }
                break;

                case INPUT_DIALOG_CREATE_CLAN: {
//                    if (true) {
//                        player.redDialog("Tính tăng đã bị dẹp do chưa làm xong, toàn ib báo lỗi (star)");
//                        return;
//                    }
                    String clanName = reader.readString(0);
                    if (player.playerData.clanId > 0) {
                        player.redDialog("Bạn đã có bang hội rồi");
                        return;
                    } else {
                        if (Utilities.CheckString(clanName, "^[a-z0-9]+$")) {
                            if (clanName.Length() >= 5 && clanName.Length() <= 20) {
                                if (player.checkCoin(GopetManager.COIN_CREATE_CLAN) && player.checkGold(GopetManager.GOLD_CREATE_CLAN)) {
                                    if (!ClanManager.clanHashMapName.contains(clanName)) {
                                        try {
                                            Clan clan = new Clan(clanName, player.user.user_id, player.playerData.name);
                                            clan.create();
                                            ClanManager.addClan(clan);
                                            player.playerData.clanId = clan.getClanId();
                                            player.mineCoin(GopetManager.COIN_CREATE_CLAN);
                                            player.mineGold(GopetManager.GOLD_CREATE_CLAN);
                                            player.okDialog(Utilities.Format("Tạo bang %s thành công", clanName));
                                        } catch (SQLException e) {
                                            if (e.getErrorCode() == 1062) {
                                                System.out.println("Duplicate clan entry detected!");
                                                player.redDialog("Lỗi trùng lập tên bang hội theo thời gian thực, nó hiếm khi xảy ra");
                                            }
                                            e.printStackTrace();
                                        } catch (Exception e) {
                                            e.printStackTrace();
                                            player.redDialog("Lỗi tạo bang");
                                        }
                                    } else {
                                        player.redDialog("Tên bang hội này đã tồn tại");
                                    }
                                } else {
                                    player.redDialog("Không đủ (vang) và (ngoc)");
                                }
                            } else {
                                player.redDialog("Tên bang cần bé hơn 20 ký tự và lớn hơn 4 ký tự");
                            }
                        } else {
                            player.redDialog("Tên bang hội không có dấu và ký tự đặc biệt nhé");
                        }
                    }
                }
                break;
            }
        } catch (NumberFormatException ex) {
            player.redDialog("Nhập sai, vui lòng nhập các con số");
        } catch (Exception e) {
            e.printStackTrace();
            player.redDialog("Đã xảy ra lõi, xD");
        }
    }

    public static void selectImgDialog(int imgId, Player player)   {
        switch (imgId) {
            case IMGDIALOG_CAPTCHA:
                player.controller.showInputDialog(INPUT_DIALOG_CAPTCHA, "Nhập mã xác nhận", new String[]{" "}, new sbyte[]{0});
                break;
        }
    }

    public static sbyte[] getTypeInput(int dialogId)   {
        switch (dialogId) {
            case INPUT_DIALOG_EXCHANGE_GOLD_TO_COIN:
                return new sbyte[]{InputReader.FIELD_LONG};
            case INPUT_DIALOG_ADMIN_GET_HISTORY:
                return new sbyte[]{InputReader.FIELD_STRING, InputReader.FIELD_STRING, InputReader.FIELD_STRING};
            case INPUT_DIALOG_COUNT_OF_KISOK_ITEM:
            case INPUT_DIALOG_CHALLENGE_INVITE:
            case INPUT_DIALOG_KIOSK:
                return new sbyte[]{InputReader.FIELD_INT};
            case INPUT_TYPE_NAME_TO_BUFF_ENCHANT:
            case INPUT_TYPE_GIFT_CODE:
            case INPUT_DIALOG_CAPTCHA:
                return new sbyte[]{InputReader.FIELD_STRING};
            case INPUT_DIALOG_ADMIN_GET_ITEM:
                return new sbyte[]{InputReader.FIELD_INT, InputReader.FIELD_INT};
            case INPUT_DIALOG_SET_PET_SELECTED_INFo:
                return new sbyte[]{InputReader.FIELD_INT, InputReader.FIELD_INT, InputReader.FIELD_INT};
            case INPUT_DIALOG_CREATE_CLAN:
            case INPUT_DIALOG_ADMIN_ADD_GOLD:
            case INPUT_DIALOG_ADMIN_ADD_COIN:
            case INPUT_DIALOG_ADMIN_CHAT_GLOBAL:
            case INPUT_DIALOG_ADMIN_UNLOCK_USER:
            case INPUT_DIALOG_ADMIN_TELE_TO_PLAYER:
            case INPUT_DIALOG_CHANGE_SLOGAN_CLAN:
                return new sbyte[]{InputReader.FIELD_STRING};
            case INPUT_DIALOG_ADMIN_LOCK_USER:
                return new sbyte[]{InputReader.FIELD_STRING, InputReader.FIELD_sbyte, InputReader.FIELD_INT, InputReader.FIELD_STRING};
        }
        return null;
    }

    private static sbyte getTypeInventorySelect(int menuId)   {
        switch (menuId) {
            case MENU_SELECT_GEM_TO_INLAY:
            case MENU_SELECT_GEM_UP_TIER:
                return GopetManager.GEM_INVENTORY;
            default:
                return GopetManager.NORMAL_INVENTORY;
        }
    }
}

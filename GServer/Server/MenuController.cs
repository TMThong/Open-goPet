
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
using System.Numerics;
using System.Diagnostics.CodeAnalysis;

[NonController]
public partial class MenuController
{
    #region Const
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
    public const int MENU_WING_INVENTORY = 81040;
    public const int MENU_NORMAL_INVENTORY = 81004;
    public const int MENU_SKIN_INVENTORY = 803;
    public const int MENU_SELECT_PET_UPGRADE_ACTIVE = 804;
    public const int MENU_SELECT_PET_UPGRADE_PASSIVE = 805;
    public const int MENU_KIOSK_HAT = 806;
    public const int MENU_KIOSK_WEAPON = 807;
    public const int MENU_KIOSK_AMOUR = 808;
    public const int MENU_KIOSK_GEM = 809;
    public const int MENU_KIOSK_PET = 81028;
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
    public const int MENU_ITEM_MONEY_INVENTORY = 1027;
    public const int MENU_UPGRADE_MEMBER_DUTY = 1028;
    public const int MENU_SELECT_TYPE_UPGRADE_DUTY = 1029;
    public const int MENU_SELECT_SKILL_CLAN_TO_RENT = 1030;
    public const int MENU_PLUS_SKILL_CLAN = 1031;
    public const int MENU_INTIVE_CHALLENGE = 1032;
    public const int MENU_SHOW_LIST_TASK = 1033;
    public const int MENU_SHOW_MY_LIST_TASK = 1034;
    public const int MENU_OPTION_TASK = 1035;
    public const int MENU_UNEQUIP_PET = 1036;
    public const int MENU_UNEQUIP_SKIN = 1038;
    public const int MENU_ATM = 1039;
    public const int MENU_EXCHANGE_GOLD = 1040;
    public const int MENU_SHOW_ALL_ITEM = 1041;
    public const int MENU_SHOW_ALL_PLAYER_HAVE_ITEM_LVL_10 = 1042;
    public const int MENU_SELECT_MATERIAL_TO_ENCAHNT_WING = 1043;
    public const int MENU_SELECT_MONEY_TO_PAY_FOR_ENCHANT_WING = 1044;
    public const int MENU_CHOOSE_PET_FROM_PACKAGE_PET = 1045;
    public const int MENU_SELECT_MATERIAL1_TO_ENCHANT_TATOO = 1046;
    public const int MENU_SELECT_MATERIAL2_TO_ENCHANT_TATOO = 1047;
    public const int MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO = 1048;
    public const int MENU_SELECT_ITEM_TO_GET_BY_ADMIN = 1049;
    public const int MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN = 1050;
    public const int MENU_SELECT_PET_TO_DEF_LEAGUE = 1051;
    public const int MENU_SELECT_TYPE_PAYMENT_TO_ARENA_JOURNALISM = 1052;
    public const int MENU_MERGE_WING = 1053;
    public const int MENU_SHOW_ALL_TATTO = 1054;
    public const int MENU_ADMIN_SHOW_ALL_ACHIEVEMENT = 1055;
    public const int MENU_ME_SHOW_ACHIEVEMENT = 1056;
    public const int MENU_SELECT_TYPE_MONEY_TO_RENT_SKILL_CLAN = 1057;
    public const int MENU_USE_ACHIEVEMNT = 1058;
    public const int MENU_USE_ACHIEVEMNT_OPTION = 1059;
    public const int MENU_LIST_FRIEND = 1060;
    public const int MENU_LIST_FRIEND_OPTION = 1061;
    public const int MENU_LIST_REQUEST_ADD_FRIEND = 1062;
    public const int MENU_LIST_BLOCK_FRIEND = 1063;
    public const int MENU_LIST_BLOCK_FRIEND_OPTION = 1064;
    public const int MENU_LIST_REQUEST_ADD_FRIEND_OPTION = 1065;
    public const int MENU_MONEY_DISPLAY_SETTING = 1066;
    public const int MENU_SELL_TRASH_ITEM = 1067;
    public const int MENU_SELECT_ITEM_MERGE = 1068;
    public const int MENU_SELECT_ALL_ITEM_MERGE = 1069;
    public const int MENU_SELECT_ALL_PET_MERGE = 1070;
    public const int MENU_OPTION_ADMIN_GIVE_ITEM = 1071;
    public const int MENU_OPTION_ADMIN_GET_ITEM = 1072;
    public const int MENU_LOCK_ITEM_PLAYER = 1073;
    public const int MENU_UNLOCK_ITEM_PLAYER = 1074;
    /// <summary>
    /// Menu chọn ô để sử dụng thẻ kỹ năng
    /// </summary>
    public const int MENU_SELECT_SLOT_USE_SKILL_CARD = 1075;
    public const int MENU_OPTION_USE_FLOWER = 1076;
    public const int MENU_OPTION_SHOW_FUSION_MENU = 1077;
    public const int MENU_FUSION_MENU_EQUIP = 1078;
    public const int MENU_FUSION_MENU_PET = 1079;
    public const int MENU_FUSION_EQUIP_OPTION = 1080;
    public const int MENU_FUSION_EQUIP_OPTION_COMFIRM = 1081;
    public const int MENU_FUSION_PET_OPTION = 1082;
    public const int MENU_FUSION_PET_OPTION_COMFIRM = 1083;
    public const int MENU_PET_SACRIFICE = 1084;
    public const int MENU_PET_REINCARNATION = 1085;
    public const int MENU_OPTION_PET_REINCARNATION = 1086;
    public const int MENU_ADMIN_BUFF_DUNG_HỢP = 1087;
    public const int MENU_OPTION_KIOSK = 1088;
    public const int MENU_OPTION_KIOSK_CANCEL_ITEM = 1089;
    public const int MENU_OPTION_BUY_KIOSK_ITEM = 1090;
    public static readonly MenuItemInfo[] ADMIN_INFOS = new MenuItemInfo[]{
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
        new AdminItemInfo("Buff đập đồ", "Người được chỉ định buff sẽ không cường hóa bị thất bại", "items/4000766.png"),
        new AdminItemInfo("Cộng hoặc trừ tiền", "Cộng hoặc trừ tiền của tài khoản chỉ định", "items/4000766.png"),
        new AdminItemInfo("Lấy Id khu", "Trả về Id của khu vực nhân vật đang đứng", "items/4000766.png"),
        new AdminItemInfo("Lấy vật phẩm", "Lấy vật phẩm trong hành trang của người chỉ định và đem nó vào hành trang của bạn", "items/4000766.png"),
        new AdminItemInfo("Chuyển vật phẩm", "Chuyển vật phẩm trong hành trang của bạn đế người chỉ định", "items/4000766.png"),
        new AdminItemInfo("Dọn hành trang trang bị", "Xóa tất cả vật phẩm là trang bị", "items/4000766.png"),
        new AdminItemInfo("Dịch chuyển tất cả người chơi", "Dịch chuyển tất cả người chơi trên server vào chỗ bạn đứng", "items/4000766.png"),
        new AdminItemInfo("Thêm danh hiệu", "Thêm thanh hiệu vào nhân vật của bạn nhầm mục đích test", "items/4000766.png"),
        new AdminItemInfo("Cập nhật máy chủ", "Dùng sẽ hiện những server ẩn đi", "items/4000766.png"),
        new AdminItemInfo("Xóa tất cả cánh", "Dùng sẽ hiện xóa tất cả cánh trong túi đồ", "items/4000766.png"),
        new AdminItemInfo("Buff cường hóa xăm", "Dùng sẽ khiến người chỉ định luôn cường hóa xăm thành công", "items/4000766.png"),
        new AdminItemInfo("Vị trí đứng", "Dùng để lấy tọa độ trên map", "items/4000766.png"),
        new AdminItemInfo("Mở gộp cho nhân vật", "Cung cấp quyền gộp cho nhân vật", "items/4000766.png"),
        new AdminItemInfo("Khoá vật phẩm", "Khoá vật phẩm của người chơi", "items/4000766.png"),
        new AdminItemInfo("Mở khoá vật phẩm", "Mở khoá vật phẩm của người chơi", "items/4000766.png"),
        new AdminItemInfo("Đập đồ nhanh", "Dùng để đập đồ nhanh và nó sẽ tự động tiến cấp", "items/4000766.png"),
        new AdminItemInfo("Test kích ẩn", "Xem pet đang theo có kích ẩn gì", "items/4000766.png"),
        new AdminItemInfo("Buff dung hợp", "Buff dung hợp cho trang bị", "items/4000766.png"),
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
    public const int ADMIN_INDEX_COIN = 16;
    public const int ADMIN_INDEX_GET_ZONE_ID = 17;
    public const int ADMIN_INDEX_GET_ITEM_FROM_PLAYER = 18;
    public const int ADMIN_INDEX_GIVE_ITEM_TO_PLAYER = 19;
    public const int ADMIN_INDEX_DELETE_ALL_EQUIP_PET_ITEM = 20;
    public const int ADMIN_INDEX_TELEPORT_ALL_PLAYER_TO_ADMIN = 21;
    public const int ADMIN_INDEX_ADD_ACHIEVEMENT = 22;
    public const int ADMIN_INDEX_SHOW_LIST_SERVER = 23;
    public const int ADMIN_INDEX_DELETE_ALL_WING = 24;
    public const int ADMIN_INDEX_BUFF_ENCHANT_TATTOO = 25;
    public const int ADMIN_INDEX_PLAYER_LOCATION = 26;
    public const int ADMIN_INDEX_SET_MERGE_SERVER = 27;
    public const int ADMIN_INDEX_LOCK_ITEM_PLAYER = 28;
    public const int ADMIN_INDEX_UNLOCK_ITEM_PLAYER = 29;
    public const int ADMIN_INDEX_FAST_UP_ITEM = 30;
    public const int ADMIN_INDEX_VIEW_CUR_PET_HIDDEN_STAT = 31;
    public const int ADMIN_INDEX_BUFF_DUNG_HỢP = 32;

    /**
     * Danh sách nhận pet miễn phí
     */

    public static JArrayList<MenuItemInfo> EXCHANGE_ITEM_INFOS = new();

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


    public const int OP_MAIN_TASK = 0;

    /**
     * Tùy chọn nhận pet miễn phí
     */
    public const int OP_LIST_PET_FREE = 1;

    /**
     * Tùy chọn tẩy tiềm năng
     */
    public const int OP_DELETE_TIEM_NANG = 24;
    /// <summary>
    /// Cửa hàng thú cưng
    /// </summary>
    public const int OP_SHOP_PET = 2;
    /// <summary>
    /// Bảng xếp hạng Pet
    /// </summary>
    public const int OP_TOP_PET = 3;
    /// <summary>
    /// Bảng xếp hạng người có gold nhiều nhất
    /// </summary>
    public const int OP_TOP_GOLD = 4;
    /// <summary>
    /// Đổi thưởng
    /// </summary>
    public const int OP_CHANGE_GIFT = 5;
    public const int OP_LIST_GIFT = 6;
    public const int OP_UPGRADE_PET = 7;
    public const int OP_UPGRADE_STAR_PET = 8;
    public const int OP_REINCARNATION = 9;
    public const int OP_CHALLENGE = 10;
    public const int OP_SHOP_ARENA = 11;
    public const int OP_SHOP_ENERGY = 14;
    public const int OP_MERGE_PART_PET = 15;
    public const int OP_PET_TATOO = 16;
    public const int OP_MERGE_WING = 18;
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
    public const int OP_SHOW_TOP_ACCUMULATED_POINT = 63;
    public const int OP_PET_LEAGUE_BETA = 64;
    public const int OP_SELECT_PET_DEF_LEAGUE = 65;
    public const int OP_TOP_AREAN_POINT = 66;
    public const int OP_ARENA_JOURNALISM = 67;
    public const int OP_EVENT_TASK = 68;
    public const int OP_TOP_EVENT_TASK = 69;
    public const int OP_SHOW_ALL_TATTO = 70;
    public const int OP_SHOW_ME_ACHIEVEMENT = 71;
    public const int OP_EVENT_SUMMER_2024_MAKE_KITE_NORMAL = 72;
    public const int OP_EVENT_SUMMER_2024_MAKE_KITE_VIP = 73;
    public const int OP_EVENT_SUMMER_2024_TOP_KITE_NORMAL = 74;
    public const int OP_EVENT_SUMMER_2024_TOP_KITE_VIP = 75;
    /// <summary>
    /// Hướng dẫn sự kiện mùa hè 2024
    /// </summary>
    public const int OP_EVENT_SUMMER_2024_GUIDE = 76;
    /// <summary>
    /// Dùng danh hiệu
    /// </summary>
    public const int OP_USE_ACHIEVEMENT = 77;
    /// <summary>
    /// Hiển thị shop gian thương
    /// </summary>
    public const int OP_SHOP_GIAN_THUONG = 78;
    /// <summary>
    /// Bán vật phẩm để trống hành trang
    /// </summary>
    public const int OP_SELL_TRASH_ITEM = 79;
    public const int OP_SHOW_TOP_CHALLENGE = 80;
    public const int OP_MENU_MERGE_SERVER = 81;
    /// <summary>
    /// Sự kiện 20/11 2024
    /// Tuỳ chọn để hiển thị menu tặng hoa cho NPC
    /// </summary>
    public const int OP_TẶNG_HOA_NPC = 82;
    /// <summary>
    /// Sự kiện 20/11 2024
    /// Tuỳ chọn để hiển thị top người chơi tặng hoa + gold
    /// </summary>
    public const int OP_XEM_TOP_FLOWER_GOLD = 83;
    /// <summary>
    /// Sự kiện 20/11 2024
    /// Tuỳ chọn để hiển thị top người chơi tặng hoa + gem
    /// </summary>
    public const int OP_XEM_TOP_FLOWER_GEM = 84;
    /// <summary>
    /// Sự kiện 20/11 2024
    /// Tuỳ chọn để nhận quà mốc sự kiện nhà giáo
    /// </summary>
    public const int OP_NHẬN_QUÀ_MỐC = 85;
    /// <summary>
    /// Tuỳ chọn để hiện thị chức năng dung hợp
    /// </summary>
    public const int OP_DUNG_HỢP = 86;
    public const int OP_ĐIỂM_DANH = 87;
    public const int OP_HƯỚNG_DẪN_LÊN_THIÊN_ĐÌNH = 88;
    public const int OP_HIẾN_TẶNG_THÚ_CƯNG = 89;
    /// <summary>
    /// Làm bánh tét
    /// </summary>
    public const int OP_MAKE_CYLINDRICAL_STICKY_RICE_CAKE = 90;
    /// <summary>
    /// Làm bánh chưng
    /// </summary>
    public const int OP_MAKE_SQUARE_STICKY_RICE_CAKE = 91;
    /// <summary>
    /// Bảng xếp hạng nhai bánh tét
    /// </summary>
    public const int OP_TOP_USE_CYLINDRICAL_STICKY_RICE_CAKE = 92;
    /// <summary>
    /// Bảng xếp hạng nhai bánh chưng
    /// </summary>
    public const int OP_TOP_USE_SQUARE_STICKY_RICE_CAKE = 93;
    /// <summary>
    /// Hướng dẫn sự kiện sinh nhật game
    /// </summary>
    public const int OP_GUIDE_EVENT_GAME_BIRTHDAY = 94;
    /// <summary>
    /// Hiển thị đổi quà cửa hàng sinh nhật
    /// </summary>
    public const int OP_SHOW_SHOP_BIRTHDAY = 95;
    /// <summary>
    /// Nhận quà mốc sinh nhật sự kiện
    /// </summary>
    public const int OP_RECIVE_GIFT_MILISTONE_BIRTHDAY_EVNT = 96;
    /// <summary>
    /// Bảng xếp hạng sử dụng hộp quà
    /// </summary>
    public const int OP_TOP_USE_GIFT_BOX_2025 = 97;
    /// <summary>
    /// Option Custom
    /// Trao đổi thưởng bằng 
    /// </summary>
    public const int OP_TRADE_GIFT_COIN = 1000000000;
    public const int OP_TRADE_GIFT_GOLD = 1000000001;
    public const int OP_TRADE_GIFT_LUA = 1000000002;
    /// <summary>
    /// Văn bản khi hiện center dialog
    /// </summary>
    public const String CMD_CENTER_OK = "OK";

    /// <summary>
    /// Cửa hàng vũ khí
    /// </summary>
    public const sbyte SHOP_WEAPON = 1;

    /// <summary>
    /// Cửa hàng giáp
    /// </summary>
    public const sbyte SHOP_ARMOUR = 2;

    /// <summary>
    /// Cửa hàng nón
    /// </summary>
    public const sbyte SHOP_HAT = 3;
    public const sbyte SHOP_SKIN = 7;
    public const sbyte SHOP_FOOD = 4;
    public const sbyte SHOP_THUONG_NHAN = 6;
    public const sbyte SHOP_PET = 8;
    public const sbyte SHOP_ARENA = 9;

    public const sbyte SHOP_CLAN = 10;
    public const sbyte SHOP_ENERGY = 11;
    public const sbyte SHOP_GIAN_THUONG = 12;
    /// <summary>
    /// Cửa hàng vật phẩm sự kiện sinh nhật trò chơi
    /// </summary>
    public const sbyte SHOP_BIRTHDAY_EVENT = 13;

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
    public const int OBJKEY_INDEX_WING_WANT_ENCHANT = 33;
    public const int OBJKEY_TYPE_PAY_FOR_ENCHANT_WING = 34;
    public const int OBJKEY_ID_MATERIAL_ENCHANT_WING = 35;
    public const int OBJKEY_ID_MENU_BUY_PET_TO_NAME = 36;
    public const int OBJKEY_INDEX_MENU_BUY_PET_TO_NAME = 37;
    public const int OBJKEY_NAME_PET_WANT = 38;
    public const int OBJKEY_PAYMENT_INDEX_WANT_TO_NAME_PET = 39;
    public const int OBJKEY_ITEM_PACKAGE_PET_TO_USE = 40;
    public const int OBJKEY_ID_TATTO_TO_ENCHANT = 41;
    public const int OBJKEY_ID_MATERIAL1_TATTO_TO_ENCHANT = 42;
    public const int OBJKEY_ID_MATERIAL2_TATTO_TO_ENCHANT = 43;
    public const int OBJKEY_TYPE_PRICE_TATTO_TO_ENCHANT = 44;
    public const int OBJKEY_PLAYER_GET_ITEM = 45;
    public const int OBJKEY_PLAYER_GIVE_ITEM = 46;
    public const int OBJKEY_COUNT_ITEM_TO_GET_BY_ADMIN = 47;
    public const int OBJKEY_COUNT_ITEM_TO_GIVE_BY_ADMIN = 48;
    public const int OBJKEY_INDEX_ACHIEVEMNT_USE = 49;
    /// <summary>
    /// Kỹ năng mẫu mà người dùng muốn thuê
    /// </summary>
    public const int OBJKEY_CLAN_SKILL_TEMPLATE_RENT = 50;
    public const int OBJKEY_INDEX_FRIEND = 51;
    public const int OBJKEY_INDEX_ITEM_MONEY = 52;
    /// <summary>
    /// Vật phẩm muốn thanh lý
    /// </summary>
    public const int OBJKEY_ITEM_TRASH_WANT_TO_SELL = 53;
    public const int OBJKEY_ITEM_ADMIN_GET = 54;
    public const int OBJKEY_ITEM_ADMIN_GIVE = 55;
    public const int OBJKEY_ITEM_ADMIN_GET_COUNT = 56;
    public const int OBJKEY_ITEM_ADMIN_GIVE_COUNT = 57;
    public const int OBJKEY_PLAYER_LOCK_ITEM = 58;
    public const int OBJKEY_PLAYER_UNLOCK_ITEM = 59;
    /// <summary>
    /// Id mẫu thử của thẻ kỹ năng khi ấn sử dụng
    /// dùng khi menu chọn ô được gọi
    /// </summary>
    public const int OBJKEY_ITEM_SKILL_CARD_USE = 60;
    /// <summary>
    /// Khoá để lưu vật phẩm đang chọn hiện tại
    /// </summary>
    public const int OBJKEY_CURRENT_ITEM_ID_FUSION = 61;
    /// <summary>
    /// Khoá để lưu vật phẩm đang chọn hiện tại
    /// </summary>
    public const int OBJKEY_CURRENT_ITEM_TEMP_ID_FUSION = 62;
    /// <summary>
    /// Khoá để lưu vật phẩm dung hợp chính
    /// </summary>
    public const int OBJKEY_MAIN_ITEM_ID_FUSION = 63;
    /// <summary>
    /// Khoá để lưu vật phẩm dung hợp phụ
    /// </summary>
    public const int OBJKEY_DEPUTY_ITEM_ID_FUSION = 64;
    /// <summary>
    /// Khoá để lưu thú cưng dung hợp chính
    /// </summary>
    public const int OBJKEY_MAIN_PET_ID_FUSION = 65;
    /// <summary>
    /// Khoá để lưu thú cưng dung hợp phụ
    /// </summary>
    public const int OBJKEY_DEPUTY_PET_ID_FUSION = 66;
    public const int OBJKEY_PET_REINCARNATION = 67;
    /// <summary>
    /// Khoá để lưu thú cưng dung hợp đang được chọn hiện tại
    /// </summary>
    public const int OBJKEY_CURRENT_SELECT_PET_ID_FUSION = 67;
    public const int OBJKEY_COUNT_USE_BÓ_HOA = 68;
    public const int OBJKEY_ITEM_BUFF_DUNG_HỢP = 69;
    public const int OBJKEY_PRICE_KIOSK_ITEM = 70;
    public const int OBJKEY_ITEM_KIOSK_CANCEL = 71;
    public const int OBJKEY_BUY_ITEM_KIOSK_ITEM_ID = 72;
    public const int OBJKEY_ID_ITEM_USE_ITEM_COUNT = 73;
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
    public const int DIALOG_ASK_ENCHANT_WING = 16;
    public const int DIALOG_ASK_ENCHANT_TATTO = 17;
    public const int DIALOG_ASK_UNLOCK_SLOT_SKILL_CLAN = 18;
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
    public const int INPUT_TYPE_NAME_TO_BUFF_COIN = 19;
    public const int INPUT_TYPE_NAME_PET_WHEN_BUY_PET = 20;
    public const int INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM = 21;
    public const int INPUT_TYPE_NAME_PLAYER_TO_GIVE_ITEM = 22;
    public const int INPUT_TYPE_NAME_BUFF_ENCHANT_TATTOO = 23;
    public const int INPUT_COUNT_OF_ITEM_TRASH_WANT_SELL = 24;
    public const int INPUT_DIALOG_EXCHANGE_COIN_TO_LUA = 25;
    public const int INPUT_TYPE_NAME_PLAYER_TO_ENBALE_MERGE_SERVER = 26;
    public const int INPUT_TYPE_NAME_LOCK_ITEM_PLAYER = 27;
    public const int INPUT_TYPE_NAME_UNLOCK_ITEM_PLAYER = 28;
    public const int INPUT_TYPE_COUNT_ADMIN_GIVE = 29;
    public const int INPUT_TYPE_COUNT_ADMIN_GET = 30;
    public const int INPUT_TYPE_FAST_UP_ITEM = 31;
    public const int INPUT_TYPE_EXCHANGE_LUA_TO_COIN = 32;
    public const int INPUT_TYPE_COUNT_USE_BÓ_HOA = 33;
    public const int INPUT_OTP_2FA = 34;
    public const int INPUT_NUM_DUNG_HỢP = 35;
    public const int INPUT_ASSIGNED_NAME_KIOSK = 36;
    public const int INPUT_ASSIGNED_CHANGE_NAME_KIOSK = 37;
    public const int INPUT_NUM_BUY_RETAIL_ITEM_KIOSK = 38;
    public const int INPUT_USE_NUM_ITEM = 39;
    public const int IMGDIALOG_CAPTCHA = 0;
    #endregion
    public static JArrayList<MenuItemInfo> getPetFreeLst(Player player)
    {
        JArrayList<MenuItemInfo> menuItemInfos = new JArrayList<MenuItemInfo>();
        foreach (int petFreeId in GopetManager.petFreeIds)
        {
            if (GopetManager.PETTEMPLATE_HASH_MAP.ContainsKey(petFreeId))
            {
                PetMenuItemInfo petMenuItemInfo = new PetMenuItemInfo(GopetManager.PETTEMPLATE_HASH_MAP.get(petFreeId), player);
                petMenuItemInfo.setCloseScreenAfterClick(true);
                petMenuItemInfo.setShowDialog(true);
                petMenuItemInfo.setDialogText(player.Language.DoWantSelectIt);
                petMenuItemInfo.setLeftCmdText(CMD_CENTER_OK);
                menuItemInfos.add(petMenuItemInfo);
            }
        }
        return menuItemInfos;
    }


    static void Trade(sbyte type, Player player)
    {
        var price = GopetManager.TradeGiftPrice[type];
        for (int i = 0; i < price.Item1.Length; i++)
        {
            if (!checkMoney((sbyte)price.Item1[i], price.Item2[i], player))
            {
                NotEngouhMoney((sbyte)price.Item1[i], price.Item2[i], player);
                return;
            }
        }

        for (int i = 0; i < price.Item1.Length; i++)
        {
            addMoney((sbyte)price.Item1[i], -price.Item2[i], player);
        }
        string join = string.Empty;
        Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
        /*
         * FOR TESTER
         * for (int i = 0; i < 50000; i++)
         */
        {
            DateTime breakTime = DateTime.Now.AddMilliseconds(20);
            while (breakTime > DateTime.Now)
            {
                var queryItem = GopetManager.TradeGift[type].Where(t => t.Percent > Utilities.NextFloatPer()).ToArray();
                if (!queryItem.Any()) continue;
                TradeGiftTemplate tradeGift = Utilities.RandomArray(queryItem);
                var it = new Item(tradeGift.ItemTemplateId, tradeGift.Count);
                it.SourcesItem.Add(ItemSource.ĐỔI_THỎI);
                if (keyValuePairs.ContainsKey(tradeGift.ItemTemplateId))
                {
                    keyValuePairs[tradeGift.ItemTemplateId]++;
                }
                else
                {
                    keyValuePairs[tradeGift.ItemTemplateId] = 1;
                }
                player.addItemToInventory(it);
                //join += ($"{it.Template.getName(player)} x{tradeGift.Count},");
                break;
            }
        }
        foreach (var item in keyValuePairs)
        {
            join += ($"{GopetManager.itemTemplate[item.Key].name} x{item.Value},");
        }
        player.okDialog($"{player.Language.TradeOKMessage} {join}");
    }

    public static JArrayList<int> typeSelectItemMaterial(int menuId, Player player)
    {
        JArrayList<int> arrayList = new();
        switch (menuId)
        {
            case MENU_SELECT_MATERIAL2_TO_ENCHANT_TATOO:
            case MENU_SELECT_GEM_ENCHANT_MATERIAL1:
            case MENU_SELECT_ENCHANT_MATERIAL1:
                arrayList.add(GopetManager.MATERIAL_ENCHANT_ITEM);
                break;
            case MENU_SELECT_ENCHANT_MATERIAL2:
                arrayList.add(GopetManager.ENCHANT_MATERIAL_CRYSTAL);
                break;
            case MENU_MERGE_PART_PET:
                arrayList.add(GopetManager.ITEM_PART_PET);
                break;
            case MENU_SELECT_ITEM_UP_SKILL:
                arrayList.add(GopetManager.ITEM_UP_SKILL_PET);
                break;
            case MENU_KIOSK_HAT_SELECT:
                arrayList.add(GopetManager.PET_EQUIP_HAT);
                break;
            case MENU_KIOSK_WEAPON_SELECT:
                arrayList.add(GopetManager.PET_EQUIP_WEAPON);
                break;

            case MENU_KIOSK_AMOUR_SELECT:
                {
                    arrayList.add(GopetManager.PET_EQUIP_ARMOUR);
                    arrayList.add(GopetManager.PET_EQUIP_GLOVE);
                    arrayList.add(GopetManager.PET_EQUIP_SHOE);
                }
                break;
            case MENU_SELECT_ITEM_PK:
                {
                    arrayList.add(GopetManager.ITEM_PK);
                }
                break;
            case MENU_SELECT_ITEM_PART_FOR_STAR_PET:
                {
                    arrayList.add(GopetManager.ITEM_PART_PET);
                }
                break;
            case MENU_SELECT_ITEM_GEN_TATTO:
                arrayList.add(GopetManager.ITEM_GEN_TATTOO_PET);
                break;
            case MENU_SELECT_ITEM_REMOVE_TATTO:
                arrayList.add(GopetManager.ITEM_REMOVE_TATTO);
                break;
            case MENU_SELECT_ITEM_SUPPORT_PET:
                arrayList.add(GopetManager.ITEM_SUPPORT_PET_IN_BATTLE);
                break;
            case MENU_SELECT_GEM_ENCHANT_MATERIAL2:
                arrayList.add(GopetManager.ITEM_MATERIAL_EMCHANT_GEM);
                break;
            case MENU_KIOSK_GEM_SELECT:
            case MENU_SELECT_GEM_TO_INLAY:
            case MENU_SELECT_GEM_UP_TIER:
                arrayList.add(GopetManager.ITEM_GEM);
                break;
            case MENU_MERGE_WING:
            case MENU_MERGE_PART_ITEM:
                arrayList.add(GopetManager.ITEM_PART_ITEM);
                break;
            case MENU_SELECT_MATERIAL_TO_ENCAHNT_WING:
                arrayList.add(GopetManager.ITEM_MATERIAL_ENCHANT_WING);
                break;
            case MENU_SELECT_MATERIAL1_TO_ENCHANT_TATOO:
                arrayList.add(GopetManager.ITEM_MATERIAL_ENCHANT_TATOO);
                break;
        }
        return arrayList;
    }



    public static void showNpcOption(int npcId, Player player)
    {
        NpcTemplate npcTemplate = GopetManager.npcTemplate.get(npcId);
        if (npcTemplate != null)
        {
            Message ms = new Message(GopetCMD.COMMAND_GUIDER);
            ms.putsbyte(GopetCMD.NPC_OPTION);
            ms.putInt(npcId);
            int[] optionId = npcTemplate.getOptionId();
            String[] optionName = npcTemplate.getOptionName(player);
            int LengthOP = optionName.Length;
            JArrayList<TaskTemplate> taskTemplates = player.controller.getTaskCalculator().getTaskTemplate(npcId);
            if (taskTemplates.Count > 0)
            {
                LengthOP++;
            }
            ms.putInt(LengthOP);
            for (int i = 0; i < optionName.Length; i++)
            {
                ms.putInt(optionId[i]);
                ms.putUTF(optionName[i]);
            }

            if (taskTemplates.Count > 0)
            {
                ms.putInt(MenuController.OP_MAIN_TASK);
                ms.putUTF(player.Language.GetMainTask);
                player.controller.objectPerformed.put(OBJKEY_NPC_ID_FOR_MAIN_TASK, npcId);
            }
            ms.cleanup();
            player.session.sendMessage(ms);
        }
    }

    public static void showNpcOption(int npcId, Player player, Option[] options)
    {
        NpcTemplate npcTemplate = GopetManager.npcTemplate.get(npcId);
        if (npcTemplate != null)
        {
            Message ms = new Message(GopetCMD.COMMAND_GUIDER);
            ms.putsbyte(GopetCMD.NPC_OPTION);
            ms.putInt(npcId);

            ms.putInt(options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                ms.putInt(options[i].getOptionId());
                ms.putUTF(options[i].getOptionText());
            }
            ms.cleanup();
            player.session.sendMessage(ms);
        }
    }

    private static PetSkill[] getPetSkills(Player player)
    {
        if (GopetManager.SPECIAL_PET_TO_LEARN_ALL_SKILL.Contains(player.playerData.petSelected.Template.petId))
        {
            return GopetManager.PET_SKILLS.Where(p => p.nClass == GopetManager.Fighter || p.nClass == GopetManager.Assassin || p.nClass == GopetManager.Wizard).ToArray();
        }
        JArrayList<PetSkill> petSkills = GopetManager.NCLASS_PETSKILL_HASH_MAP.get(player.playerData.petSelected.getPetTemplate().nclass);
        return petSkills.ToArray();
    }



    public static void showTop(Top top, Player player)
    {
        JArrayList<MenuItemInfo> menuItemInfos = new();

        var myInfoTop = top.getMyInfo(player);
        if (myInfoTop != null)
        {
            MenuItemInfo menuItemInfo = new MenuItemInfo(myInfoTop.title, myInfoTop.desc, myInfoTop.imgPath, false);
            menuItemInfos.add(menuItemInfo);
            menuItemInfos.add(new MenuItemInfo("----------TOP----------", "", top.HrImagePath, false));
        }

        foreach (TopData data in top.datas)
        {
            MenuItemInfo menuItemInfo = new MenuItemInfo(data.title, data.desc, data.imgPath, false);
            menuItemInfos.add(menuItemInfo);
        }
        player.controller.showMenuItem(-1, TYPE_MENU_NONE, top.name, menuItemInfos);
    }

    public static void showShop(sbyte type, Player player)
    {
        ShopTemplate shopTemplate = getShop(type, player);
        if (shopTemplate == null)
        {
            player.redDialog(player.Language.GetErrorShowShopByKickClan);
            return;
        }
        JArrayList<MenuItemInfo> menuItemInfos = new();
        foreach (ShopTemplateItem shopTemplateItem in shopTemplate.getShopTemplateItems())
        {
            ItemTemplate itemTemplate = shopTemplateItem.getItemTemplate();
            MenuItemInfo menuItemInfo = new MenuItemInfo(shopTemplateItem.getName(player), shopTemplateItem.getDesc(player), shopTemplateItem.isSpceial ? "npcs/fone2.png" : shopTemplateItem.getIconPath(), true);
            MenuItemInfo.PaymentOption[] paymentOptions = new MenuItemInfo.PaymentOption[shopTemplateItem.getMoneyType().Length];
            for (int i = 0; i < shopTemplateItem.getMoneyType().Length; i++)
            {
                sbyte b = shopTemplateItem.getMoneyType()[i];
                MenuItemInfo.PaymentOption paymentOption = new MenuItemInfo.PaymentOption(i, getMoneyText(b, shopTemplateItem.getPrice()[i], player), checkMoney(b, shopTemplateItem.getPrice()[i], player) ? (sbyte)1 : (sbyte)0);
                paymentOptions[i] = paymentOption;
            }
            menuItemInfo.setShowDialog(true);
            menuItemInfo.setDialogText(player.Language.DoYouWantBuyIt);
            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
            menuItemInfo.setPaymentOptions(paymentOptions);
            menuItemInfo.setCloseScreenAfterClick(shopTemplateItem.isCloseScreenAfterClick());
            menuItemInfo.setHasId(shopTemplateItem.isHasId());
            menuItemInfo.setItemId(shopTemplateItem.getMenuId());
            menuItemInfos.add(menuItemInfo);
        }

        player.controller.showMenuItem(type, TYPE_MENU_PAYMENT, shopTemplate.getName(player), menuItemInfos);
    }

    public static ShopTemplate getShop(sbyte type, Player player)
    {
        switch (type)
        {
            case SHOP_ARENA:
                if (player.playerData.shopArena != null)
                {
                    player.playerData.shopArena.nextWhenNewDay();
                    return player.playerData.shopArena;
                }
                ShopArena shopTemplate = new ShopArena();
                player.playerData.shopArena = shopTemplate;
                player.playerData.shopArena.nextArena();
                return shopTemplate;
            case SHOP_CLAN:
                ClanMember clanMember = player.controller.getClan();
                if (clanMember != null)
                {
                    return clanMember.getClan().getShopClan();
                }
                return null;
            default:
                return GopetManager.shopTemplate.get(type);
        }
    }

    public static String getMoneyText(sbyte type, long value, Player player)
    {
        String str = Utilities.FormatNumber(value);
        switch (type)
        {
            case GopetManager.MONEY_TYPE_COIN:
                str += " (ngoc)";
                break;
            case GopetManager.MONEY_TYPE_GOLD:
                str += " (vang)"; break;
            case GopetManager.MONEY_TYPE_SILVER_BAR:
                str += player.Language.GetMoneyTextSilverBar; break;
            case GopetManager.MONEY_TYPE_GOLD_BAR:
                str += player.Language.GetMoneyTextGoldBar; break;
            case GopetManager.MONEY_TYPE_BLOOD_GEM:
                str += player.Language.GetMoneyTextBloodGem; break;
            case GopetManager.MONEY_TYPE_FUND_CLAN:
                str += player.Language.GetMoneyTextFundClan; break;
            case GopetManager.MONEY_TYPE_CRYSTAL_ITEM:
                str += player.Language.GetMoneyTextCrystalItem; break;
            case GopetManager.MONEY_TYPE_LUA:
                str += " (lua)"; break;
            case GopetManager.MONEY_TYPE_FLOWER_GOLD:
                str += " điểm hoa vàng"; break;
            case GopetManager.MONEY_TYPE_FLOWER_COIN:
                str += " điểm hoa ngọc"; break;
            case GopetManager.MONEY_TYPE_CYLINDRIAL_COIN:
                str += " điểm bánh tét"; break;
            case GopetManager.MONEY_TYPE_SQUARE_COIN:
                str += " điểm bánh chưng"; break;
        }
        return str;
    }

    public static void NotEngouhMoney(sbyte type, long value, Player player)
    {
        player.redDialog(player.Language.NotEnoughStr, getMoneyText(type, value, player));
    }

    public static bool checkMoney(sbyte type, long value, Player player)
    {
        switch (type)
        {
            case GopetManager.MONEY_TYPE_COIN:
                {
                    return player.checkCoin(value);
                }
            case GopetManager.MONEY_TYPE_GOLD:
                {
                    return player.checkGold(value);
                }
            case GopetManager.MONEY_TYPE_SILVER_BAR:
                {
                    return player.controller.checkSilverBar((int)value);
                }
            case GopetManager.MONEY_TYPE_GOLD_BAR:
                {
                    return player.controller.checkGoldBar((int)value);
                }
            case GopetManager.MONEY_TYPE_BLOOD_GEM:
                {
                    return player.controller.checkBloodGem((int)value);
                }
            case GopetManager.MONEY_TYPE_CRYSTAL_ITEM:
                {
                    return player.controller.checkCrystal((int)value);
                }
            case GopetManager.MONEY_TYPE_FUND_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        return clanMember.fundDonate >= value;
                    }
                    else
                    {
                        return false;
                    }
                }
            case GopetManager.MONEY_TYPE_LUA:
                return player.checkLua(value);
            case GopetManager.MONEY_TYPE_FLOWER_GOLD:
                return player.playerData.FlowerGold >= value;
            case GopetManager.MONEY_TYPE_FLOWER_COIN:
                return player.playerData.FlowerCoin >= value;
            case GopetManager.MONEY_TYPE_CYLINDRIAL_COIN:
                return player.playerData.NumEatCylindricalStickyRiceCoin >= value;
            case GopetManager.MONEY_TYPE_SQUARE_COIN:
                return player.playerData.NumEatSquareStickyRiceCoin >= value;
        }
        return false;
    }

    public static void addMoney(sbyte typeMoney, long value, Player player)
    {
        switch (typeMoney)
        {
            case GopetManager.MONEY_TYPE_COIN:
                player.addCoin(value); break;
            case GopetManager.MONEY_TYPE_GOLD:
                player.addGold(value); break;
            case GopetManager.MONEY_TYPE_SILVER_BAR:
                player.controller.addSilverBar((int)value); break;
            case GopetManager.MONEY_TYPE_GOLD_BAR:
                player.controller.addGoldBar((int)value); break;
            case GopetManager.MONEY_TYPE_BLOOD_GEM:
                player.controller.addBloodGem((int)value); break;
            case GopetManager.MONEY_TYPE_CRYSTAL_ITEM:
                player.controller.addGoldBar((int)value); break;
            case GopetManager.MONEY_TYPE_FUND_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        clanMember.fundDonate += value;
                    }
                }
                break;
            case GopetManager.MONEY_TYPE_LUA:
                player.AddLua(value);
                break;
            case GopetManager.MONEY_TYPE_FLOWER_GOLD:
                player.playerData.FlowerGold += (int)value;
                break;
            case GopetManager.MONEY_TYPE_FLOWER_COIN:
                player.playerData.FlowerCoin += (int)value;
                break;
            case GopetManager.MONEY_TYPE_CYLINDRIAL_COIN:
                player.playerData.NumEatCylindricalStickyRiceCoin += (int)value;
                break;
            case GopetManager.MONEY_TYPE_SQUARE_COIN:
                player.playerData.NumEatSquareStickyRiceCoin += (int)value;
                break;
        }
    }

    public static void showInventory(Player player, sbyte typeInventory, int menuId, String title)
    {
        CopyOnWriteArrayList<Item> items = (CopyOnWriteArrayList<Item>)player.playerData.getInventoryOrCreate(typeInventory).clone();

        int i = 0;
        JArrayList<MenuItemInfo> menuList = new();
        switch (typeInventory)
        {
            case GopetManager.SKIN_INVENTORY:
                {
                    Item it = player.playerData.skin;
                    if (it != null)
                    {
                        i = -1;
                        items.add(0, it);
                    }
                }
                break;
            case GopetManager.WING_INVENTORY:
                {
                    Item it = player.playerData.wing;
                    if (it != null)
                    {
                        i = -1;
                        items.add(0, it);
                    }
                }
                break;
        }
        foreach (Item item in items)
        {
            ItemTemplate itemTemplate = item.getTemp();
            MenuItemInfo menuItemInfo = new MenuItemInfo(typeInventory == GopetManager.EQUIP_PET_INVENTORY ? item.getEquipName(player) : item.getName(player), item.getDescription(player), "", true);
            menuItemInfo.setImgPath(itemTemplate.getIconPath());
            menuItemInfo.setShowDialog(true);
            menuItemInfo.setDialogText(string.Format(player.Language.DoWantSelectIt, itemTemplate.getName(player)));
            menuItemInfo.setLeftCmdText(CMD_CENTER_OK);
            menuItemInfo.setCloseScreenAfterClick(true);
            menuItemInfo.setHasId(true);
            menuItemInfo.setItemId(i);

            if (i == -1)
            {
                menuItemInfo.setTitleMenu(menuItemInfo.getTitleMenu() + player.Language.IsUsing);
            }
            menuList.add(menuItemInfo);
            i++;
        }

        player.controller.showMenuItem(menuId, TYPE_MENU_SELECT_ELEMENT, title, menuList);
    }

    public static void showYNDialog(int dialogId, String text, Player player)
    {
        Message m = new Message(GopetCMD.SERVER_MESSAGE);
        m.putsbyte(GopetCMD.SEND_YES_NO);
        m.putInt(dialogId);
        m.putUTF(text);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public static void selectImgDialog(int imgId, Player player)
    {
        switch (imgId)
        {
            case IMGDIALOG_CAPTCHA:
                player.controller.showInputDialog(INPUT_DIALOG_CAPTCHA, "Nhập mã xác nhận", new String[] { " " }, new sbyte[] { 0 });
                break;
        }
    }

    public static sbyte[] getTypeInput(int dialogId)
    {
        switch (dialogId)
        {
            case INPUT_TYPE_EXCHANGE_LUA_TO_COIN:
            case INPUT_DIALOG_EXCHANGE_COIN_TO_LUA:
            case INPUT_DIALOG_EXCHANGE_GOLD_TO_COIN:
                return new sbyte[] { InputReader.FIELD_LONG };
            case INPUT_DIALOG_ADMIN_GET_HISTORY:
                return new sbyte[] { InputReader.FIELD_STRING, InputReader.FIELD_STRING, InputReader.FIELD_STRING };
            case INPUT_USE_NUM_ITEM:
            case INPUT_NUM_BUY_RETAIL_ITEM_KIOSK:
            case INPUT_NUM_DUNG_HỢP:
            case INPUT_TYPE_COUNT_USE_BÓ_HOA:
            case INPUT_TYPE_COUNT_ADMIN_GIVE:
            case INPUT_TYPE_COUNT_ADMIN_GET:
            case INPUT_COUNT_OF_ITEM_TRASH_WANT_SELL:
            case INPUT_DIALOG_COUNT_OF_KISOK_ITEM:
            case INPUT_DIALOG_CHALLENGE_INVITE:
            case INPUT_DIALOG_KIOSK:
                return new sbyte[] { InputReader.FIELD_INT };
            case INPUT_ASSIGNED_CHANGE_NAME_KIOSK:
            case INPUT_ASSIGNED_NAME_KIOSK:
            case INPUT_OTP_2FA:
            case INPUT_TYPE_NAME_BUFF_ENCHANT_TATTOO:
            case INPUT_TYPE_NAME_PET_WHEN_BUY_PET:
            case INPUT_TYPE_NAME_TO_BUFF_ENCHANT:
            case INPUT_TYPE_GIFT_CODE:
            case INPUT_DIALOG_CAPTCHA:
                return new sbyte[] { InputReader.FIELD_STRING };
            case INPUT_DIALOG_ADMIN_GET_ITEM:
                return new sbyte[] { InputReader.FIELD_INT, InputReader.FIELD_INT };
            case INPUT_DIALOG_SET_PET_SELECTED_INFo:
                return new sbyte[] { InputReader.FIELD_INT, InputReader.FIELD_INT, InputReader.FIELD_INT };
            case INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM:
            case INPUT_TYPE_NAME_PLAYER_TO_GIVE_ITEM:
            case INPUT_TYPE_NAME_LOCK_ITEM_PLAYER:
            case INPUT_TYPE_NAME_UNLOCK_ITEM_PLAYER:
            case INPUT_TYPE_NAME_PLAYER_TO_ENBALE_MERGE_SERVER:
            case INPUT_DIALOG_CREATE_CLAN:
            case INPUT_DIALOG_ADMIN_ADD_GOLD:
            case INPUT_DIALOG_ADMIN_ADD_COIN:
            case INPUT_DIALOG_ADMIN_CHAT_GLOBAL:
            case INPUT_DIALOG_ADMIN_UNLOCK_USER:
            case INPUT_DIALOG_ADMIN_TELE_TO_PLAYER:
            case INPUT_DIALOG_CHANGE_SLOGAN_CLAN:
                return new sbyte[] { InputReader.FIELD_STRING };
            case INPUT_DIALOG_ADMIN_LOCK_USER:
                return new sbyte[] { InputReader.FIELD_STRING, InputReader.FIELD_SBYTE, InputReader.FIELD_INT, InputReader.FIELD_STRING };
            case INPUT_TYPE_NAME_TO_BUFF_COIN:
                return new sbyte[] { InputReader.FIELD_STRING, InputReader.FIELD_INT };
            case INPUT_TYPE_FAST_UP_ITEM:
                return new sbyte[] { InputReader.FIELD_INT, InputReader.FIELD_INT, InputReader.FIELD_INT, InputReader.FIELD_INT, InputReader.FIELD_INT, InputReader.FIELD_INT, InputReader.FIELD_INT };
        }
        return null;
    }
    [SuppressMessage("Style", "IDE0066:Use switch")]
    private static sbyte getTypeInventorySelect(int menuId)
    {
        switch (menuId)
        {
            case MENU_SELECT_GEM_TO_INLAY:
            case MENU_SELECT_GEM_UP_TIER:
                return GopetManager.GEM_INVENTORY;
            default:
                return GopetManager.NORMAL_INVENTORY;
        }
    }

    private static CopyOnWriteArrayList<Item> getItemByMenuId(int menuId, Player player, Func<Item, bool> filter = null)
    {
        switch (menuId)
        {
            case MENU_ADMIN_BUFF_DUNG_HỢP:
            case MENU_FUSION_MENU_EQUIP:
                if (player.controller.objectPerformed.ContainsKey(OBJKEY_CURRENT_ITEM_TEMP_ID_FUSION))
                {
                    return new CopyOnWriteArrayList<Item>(player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY).Where(x => x.Template.CanFusion && x.Template.itemId == player.controller.objectPerformed[OBJKEY_CURRENT_ITEM_TEMP_ID_FUSION]));
                }
                return new CopyOnWriteArrayList<Item>(player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY).Where(x => x.Template.CanFusion));
            case MENU_UNLOCK_ITEM_PLAYER:
            case MENU_LOCK_ITEM_PLAYER:
                {
                    CopyOnWriteArrayList<Item> items = new CopyOnWriteArrayList<Item>();
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_PLAYER_LOCK_ITEM) || player.controller.objectPerformed.ContainsKey(OBJKEY_PLAYER_UNLOCK_ITEM))
                    {
                        Player playerOnline = menuId == MENU_LOCK_ITEM_PLAYER ? player.controller.objectPerformed[OBJKEY_PLAYER_LOCK_ITEM] : player.controller.objectPerformed[OBJKEY_PLAYER_UNLOCK_ITEM];
                        if (PlayerManager.players.Contains(playerOnline))
                        {
                            foreach (var inventory in playerOnline.playerData.items)
                            {
                                foreach (var item in inventory.Value)
                                {
                                    items.Add(item);
                                }
                            }
                        }
                    }
                    return items;
                }
                break;

            case MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN:
            case MENU_SELECT_ITEM_TO_GET_BY_ADMIN:
                {
                    CopyOnWriteArrayList<Item> items = new CopyOnWriteArrayList<Item>();
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_PLAYER_GET_ITEM) || player.controller.objectPerformed.ContainsKey(OBJKEY_PLAYER_GIVE_ITEM))
                    {
                        Player playerOnline = menuId == MENU_SELECT_ITEM_TO_GET_BY_ADMIN ? player.controller.objectPerformed[OBJKEY_PLAYER_GET_ITEM] : player;
                        if (PlayerManager.players.Contains(playerOnline))
                        {
                            foreach (var inventory in playerOnline.playerData.items)
                            {
                                foreach (var item in inventory.Value)
                                {
                                    items.Add(item);
                                }
                            }
                        }
                    }
                    return items;
                }
            case MENU_SELECT_ALL_ITEM_MERGE:
                {
                    CopyOnWriteArrayList<Item> items = new CopyOnWriteArrayList<Item>();
                    if (player.controller.MergePlayerData != null)
                    {
                        if (player.controller.MergePlayerData.wing != null)
                        {
                            items.Add(player.controller.MergePlayerData.wing);
                        }
                        foreach (var item in player.controller.MergePlayerData.items)
                        {
                            items.AddRange(item.Value);
                        }
                    }
                    return items;
                }
            default:
                return Item.search(typeSelectItemMaterial(menuId, player), player.playerData.getInventoryOrCreate(getTypeInventorySelect(menuId)), filter);
        }
        return new CopyOnWriteArrayList<Item>();
    }

    public static bool CloseScreenAfterClick(int menuId)
    {
        switch (menuId)
        {
            case MENU_ADMIN_BUFF_DUNG_HỢP:
            case MENU_UNLOCK_ITEM_PLAYER:
            case MENU_LOCK_ITEM_PLAYER:
            case MENU_SELECT_ITEM_TO_GET_BY_ADMIN:
            case MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN:
            case MENU_SELECT_ALL_ITEM_MERGE:
                return false;
            default:
                return true;
        }

    }

    public static bool Move(Player From, Player To, CopyOnWriteArrayList<AdminSelectItemData> adminSelectItemDatas)
    {
        if (PlayerManager.player_name.TryGetValue(From.playerData.name, out From) && PlayerManager.player_name.TryGetValue(To.playerData.name, out To))
        {
            foreach (var item in adminSelectItemDatas)
            {
                sbyte Type = From.playerData.items.Where(x => x.Value.Contains(item.Item)).First().Key;
                if (item.Item.count - item.Count == 0 || !item.Item.Template.isStackable)
                {
                    From.playerData.items[Type].Remove(item.Item);
                    To.addItemToInventory(item.Item);
                }
                else
                {
                    item.Item.count -= item.Count;
                    To.addItemToInventory(new Item(item.Item.itemTemplateId, item.Count));
                }
            }
            return true;
        }
        else
        {
            From.redDialog(From.Language.PlayerOffline);
            To.redDialog(To.Language.PlayerOffline);
        }
        return false;
    }

    public static void ShowUseItemCountDialog(Player player, int itemId)
    {
        player.controller.objectPerformed[OBJKEY_ID_ITEM_USE_ITEM_COUNT] = itemId;
        player.controller.showInputDialog(INPUT_USE_NUM_ITEM, "Dùng vật phẩm theo số lượng", "Số lượng: ");
    }

    public static void SellKioskItem(Player player, int priceItem, string nameAssigned = null)
    {

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
                if (!item.Template.canTrade || !item.canTrade)
                {
                    player.redDialog(player.Language.ItemCanNotTrade);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(nameAssigned))
            {
                int priceGold = pet == null ? GopetManager.PRICE_ASSIGNED_PET : GopetManager.PRICE_ASSIGNED_ITEM;
                if (!player.checkGold(priceGold))
                {
                    player.controller.notEnoughGold();
                    return;
                }
                player.mineGold(priceGold);
            }
            player.controller.objectPerformed.Remove(OBJKEY_SELECT_SELL_ITEM);
            player.controller.objectPerformed.Remove(OBJKEY_MENU_OF_KIOSK);
            int count = 1;
            if (player.controller.objectPerformed.ContainsKey(OBJKEY_COUNT_OF_ITEM_KIOSK))
            {
                count = (int)player.controller.objectPerformed.get(OBJKEY_COUNT_OF_ITEM_KIOSK);
            }
            switch (menuKioskId)
            {
                case MENU_KIOSK_PET_SELECT:
                    player.playerData.pets.remove(pet);
                    MarketPlace.getKiosk(GopetManager.KIOSK_PET).addKioskItem(pet, priceItem, player, nameAssigned);
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
                            MarketPlace.getKiosk(GopetManager.KIOSK_HAT).addKioskItem(item, priceItem, player, nameAssigned);
                            player.controller.showKiosk(GopetManager.KIOSK_HAT);
                            break;
                        case MENU_KIOSK_GEM_SELECT:
                            MarketPlace.getKiosk(GopetManager.KIOSK_GEM).addKioskItem(item, priceItem, player, nameAssigned);
                            player.controller.showKiosk(GopetManager.KIOSK_GEM);
                            break;
                        case MENU_KIOSK_WEAPON_SELECT:
                            MarketPlace.getKiosk(GopetManager.KIOSK_WEAPON).addKioskItem(item, priceItem, player, nameAssigned);
                            player.controller.showKiosk(GopetManager.KIOSK_WEAPON);
                            break;
                        case MENU_KIOSK_AMOUR_SELECT:
                            MarketPlace.getKiosk(GopetManager.KIOSK_AMOUR).addKioskItem(item, priceItem, player, nameAssigned);
                            player.controller.showKiosk(GopetManager.KIOSK_AMOUR);
                            break;
                        case MENU_KIOSK_PET_SELECT:
                            MarketPlace.getKiosk(GopetManager.KIOSK_PET).addKioskItem(pet, priceItem, player, nameAssigned);
                            player.controller.showKiosk(GopetManager.KIOSK_PET);
                            break;
                        case MENU_KIOSK_OHTER_SELECT:
                            if (GameController.checkCount(item, count))
                            {
                                Item itemCopy = new Item(item.itemTemplateId);
                                itemCopy.count = count;
                                itemCopy.SourcesItem.Add(Gopet.Data.item.ItemSource.COPY_PHI_CHỢ);
                                MarketPlace.getKiosk(GopetManager.KIOSK_OTHER).addKioskItem(itemCopy, priceItem, player, nameAssigned);
                                player.controller.showKiosk(GopetManager.KIOSK_OTHER);
                                player.controller.subCountItem(item, count, GopetManager.NORMAL_INVENTORY);
                            }
                            else
                            {
                                player.redDialog(player.Language.EnoughCountOfItem);
                            }
                            break;
                    }
                    break;
            }
        }
    }
}

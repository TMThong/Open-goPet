
using Dapper;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.Mob;
using Gopet.Data.User;
using Gopet.Util;
using MySqlConnector;
using Newtonsoft.Json;
using Gopet.Data.Dialog;
using Gopet.Adapter;
using Gopet.Data.item;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Gopet.Data.user;
using Gopet.Data.Event.Year2024;
using System.Diagnostics;
using Gopet.Data.Clan;
using Gopet.Language;
using System.Runtime;
using Gopet.Data.pet;
using System.Diagnostics.CodeAnalysis;
using System.Configuration;
public class GopetManager
{
    public const string OldFeatures = "Chức năng cũ, đã bị xóa";
    /**
     * Chỉ số của quái từng cấp độ
     */
    public static HashMap<int, MobLvInfo> MOBLVLINFO_HASH_MAP = new();

    public static JArrayList<PetTemplate> PET_TEMPLATES = new();
    /**
     * Mẫu pet
     */
    public static HashMap<int, PetTemplate> PETTEMPLATE_HASH_MAP = new();

    public static HashMap<sbyte, JArrayList<PetTemplate>> typePetTemplate = new();
    /**
     * Cho thông tin Cấp độ quái giao động (từ - đến)
     */
    public static HashMap<int, MobLvlMap[]> MOBLVL_MAP = new();

    /**
     * Ví trí quái xuất hiện
     */
    public static HashMap<int, MobLocation[]> mobLocation = new();

    /**
     * Vật phẩm mẫu
     */
    public static HashMap<int, ItemTemplate> itemTemplate = new();

    /**
     * Tên chỉ số
     */
    public static HashMap<int, String> itemInfoName = new();

    /**
     * Chí số có là %
     */
    public static HashMap<int, bool> itemInfoIsPercent = new();

    /**
     * Chỉ số có thể định dạng
     */
    public static HashMap<int, bool> itemInfoCanFormat = new();

    /**
     * hệ lửa
     */
    public const sbyte FIRE_ELEMENT = 1;

    /**
     * hệ mộc
     */
    public const sbyte TREE_ELEMENT = 2;

    /**
     * hệ đá
     */
    public const sbyte ROCK_ELEMENT = 3;

    /**
     * hệ sét
     */
    public const sbyte THUNDER_ELEMENT = 4;

    /**
     * hệ nước
     */
    public const sbyte WATER_ELEMENT = 5;

    /**
     * hệ bóng tối
     */
    public const sbyte DARK_ELEMENT = 6;

    /**
     * hệ ánh sáng
     */
    public const sbyte LIGHT_ELEMENT = 7;

    /**
     * chiến binh
     */
    public const sbyte Fighter = 0;

    /**
     * sát thủ
     */
    public const sbyte Assassin = 1;

    /**
     * pháp sư
     */
    public const sbyte Wizard = 2;

    /**
     * thiên sứ
     */
    public const sbyte Angel = 3;

    /**
     * thiên binh
     */
    public const sbyte Archer = 4;

    /**
     * thiên ma
     *
     */
    public const sbyte Demon = 5;

    public const int ITEM_REMOVEABLE = -2;
    public const int ITEM_ADMIN = -1;

    /**
     * Trang bị của pet (nón)
     */
    public const int PET_EQUIP_HAT = 3;

    /**
     * Trang bị của pet (Giáp)
     */
    public const int PET_EQUIP_ARMOUR = 2;

    /**
     * Trang bị của pet (Vũ khí)
     */
    public const int PET_EQUIP_WEAPON = 1;

    /**
     * Trang bị của pet (Giày)
     */
    public const int PET_EQUIP_SHOE = 104;

    /**
     * Trang bị của pet (Găng tay)
     */
    public const int PET_EQUIP_GLOVE = 105;

    public const int SKIN_ITEM = 4;
    public const int WING_ITEM = 5;
    public const int MATERIAL_ENCHANT_ITEM = 6;
    public const int MATERIAL_ENCHANT_ITEM_SKY = 7;
    public const int ENCHANT_MATERIAL_CRYSTAL = 8;
    public const int ITEM_PART_PET = 9;
    public const int ITEM_BUFF_EXP = 10;
    public const int ITEM_UP_SKILL_PET = 11;
    public const int ITEM_PK = 12;
    public const int ITEM_GEN_TATTOO_PET = 13;
    public const int ITEM_REMOVE_TATTO = 14;
    public const int ITEM_SUPPORT_PET_IN_BATTLE = 15;
    public const int ITEM_MONEY = 16;
    public const int ITEM_GEM = 17;
    public const int ITEM_MATERIAL_EMCHANT_GEM = 18;
    public const int ITEM_PART_ITEM = 19;
    public const int ITEM_ENERGY = 20;
    public const int ITEM_MATERIAL_ENCHANT_WING = 21;
    public const int ITEM_PET_PACKAGE = 22;
    public const int ITEM_MATERIAL_ENCHANT_TATOO = 23;
    public const int ITEM_EVENT = 24;
    public const int ITEM_NEED_TO_TRAIN_COIN = 25;
    /// <summary>
    /// Vật phẩm này bán trong shop để đại diện cho danh hiệu
    /// Sau khi mua sẽ tự động sử dụng 
    /// Danh hiệu sẽ tự vào nhân vật
    /// </summary>
    public const int ITEM_NATIVE_TITLE = 26;
    public const int ITEM_THẺ_KỸ_NĂNG = 27;
    public const int ITEM_CARD_REINCARNATION = 28;
    public const int GIFT_GOLD = 0;
    public const int GIFT_COIN = 1;
    public const int GIFT_ITEM = 2;
    public const int GIFT_ITEM_PERCENT = 3;
    public const int GIFT_ITEM_PERCENT_NO_DROP_MORE = 4;
    public const int GIFT_ITEM_MERGE_PET = 5;
    public const int GIFT_ITEM_MERGE_ITEM = 6;
    public const int GIFT_EXP = 7;
    public const int GIFT_ENERGY = 8;
    public const int GIFT_RANDOM_ITEM = 9;
    public const int GIFT_ITEM_MAX_OPTION = 10;
    /// <summary>
    /// Quà là điểm sự kiện
    /// </summary>
    public const int GIFT_EVENT_POINT = 11;
    /// <summary>
    /// Quà là điểm cống hiến bang hội
    /// </summary>
    public const int GIFT_FUND_CLAN = 12;
    /// <summary>
    /// Danh hiệu
    /// </summary>
    public const int GIFT_TITLE = 13;
    /// <summary>
    /// Quà là trang phục
    /// </summary>
    public const int GIFT_SKIN = 14;
    /// <summary>
    /// Quà là thú cưng thử nghiệm
    /// </summary>
    public const int GIFT_PET_TRIAL = 15;
    /**
     * thời gian chờ lượt đánh (mili giây)
     */
    public const long TimeNextTurn = 1000 * 25;

    /**
     * Giá học kỹ năng cho pet
     */
    public const int PriceLearnSkill = 1000 * 20;

    /**
     * Kỹ năng của pet (theo skillID)
     */
    public static HashMap<int, PetSkill> PETSKILL_HASH_MAP = new();

    /**
     * Kỹ năng của pet (theo phái)
     */
    public static HashMap<sbyte, JArrayList<PetSkill>> NCLASS_PETSKILL_HASH_MAP = new();

    /**
     * Kỹ năng của pet (tất cả)
     */
    public static JArrayList<PetSkill> PET_SKILLS = new();

    /**
     * Kinh nghiệm của pet
     */
    public static HashMap<int, int> PetExp = new();

    public static HashMap<int, JArrayList<DropItem>> dropItem = new();

    public static HashMap<int, TierItem> tierItem = new();

    public static HashMap<int, PetTier> petTier = new();

    public static HashMap<int, PetTattoTemplate> tattos = new();

    public static HashMap<int, BossTemplate> boss = new();


    public static JArrayList<int> mapHasDropItemLvlRange = new();

    public static JArrayList<PetTemplate> petEnable = new();

    public static ShopArenaTemplate[] SHOP_ARENA_TEMPLATE;

    public static HashMap<int, ClanTemplate> clanTemp = new();

    public static HashMap<int, TaskTemplate> taskTemplate = new();

    public static JArrayList<TaskTemplate> taskTemplateList = new();

    public static HashMap<int, JArrayList<TaskTemplate>> taskTemplateByType = new();

    public static HashMap<int, JArrayList<TaskTemplate>> taskTemplateByNpcId = new();

    public static HashMap<int, int> tierItemHashMap = new();

    /// <summary>
    /// Danh sách mẫu kỹ năng bang hội theo cấp bang
    /// </summary>
    public static readonly Dictionary<int, ClanSkillTemplate> ClanSkillViaId = new();
    /// <summary>
    /// Danh sách mẫu kỹ năng bang hội
    /// </summary>
    public static readonly List<ClanSkillTemplate> clanSkillTemplateList = new();

    public static JArrayList<ExchangeData> EXCHANGE_DATAS = new();

    public static JArrayList<ItemTemplate> NonAdminItemList = new();


    public static readonly List<ItemTemplate> itemTemplates = new JArrayList<ItemTemplate>();

    public static readonly Dictionary<int, string> itemAssetsIcon = new();

    public static readonly Dictionary<int, AchievementTemplate> AchievementMAP = new();
    public static IEnumerable<AchievementTemplate> achievements = new List<AchievementTemplate>();
    public static IEnumerable<ServerInfo> ServerInfos = new List<ServerInfo>();
    public static List<int> ListPetMustntUpTier = new List<int>();

    /// <summary>
    /// Ghi nhật ký máy chủ
    /// Có hỗ trợ màu sắc
    /// </summary>
    public static Gopet.Logging.Monitor ServerMonitor { get; } = new Gopet.Logging.Monitor("Máy chủ");

    /// <summary>
    /// Truyền vào là pet của mình
    /// Rồi sau đó là pet của đối phương
    /// </summary>
    public static Dictionary<sbyte, Dictionary<sbyte, float>> MitigatePetData = new()
    {
        [FIRE_ELEMENT] = new()
        {
            [FIRE_ELEMENT] = 20f,
            [TREE_ELEMENT] = 30f,
            [ROCK_ELEMENT] = 0f,
            [THUNDER_ELEMENT] = 0f,
            [WATER_ELEMENT] = -50f,
            [DARK_ELEMENT] = 0f,
            [LIGHT_ELEMENT] = 0f
        },
        [TREE_ELEMENT] = new()
        {
            [FIRE_ELEMENT] = -50f,
            [TREE_ELEMENT] = 20f,
            [ROCK_ELEMENT] = 0f,
            [THUNDER_ELEMENT] = 0f,
            [WATER_ELEMENT] = 0f,
            [DARK_ELEMENT] = 0f,
            [LIGHT_ELEMENT] = 0f
        },
        [ROCK_ELEMENT] = new()
        {
            [FIRE_ELEMENT] = 0f,
            [TREE_ELEMENT] = -50f,
            [ROCK_ELEMENT] = 20f,
            [THUNDER_ELEMENT] = 30f,
            [WATER_ELEMENT] = 0f,
            [DARK_ELEMENT] = 0f,
            [LIGHT_ELEMENT] = 0f
        },
        [THUNDER_ELEMENT] = new()
        {
            [FIRE_ELEMENT] = 0f,
            [TREE_ELEMENT] = 0f,
            [ROCK_ELEMENT] = -50f,
            [WATER_ELEMENT] = 20f,
            [THUNDER_ELEMENT] = 20f,
            [DARK_ELEMENT] = 0f,
            [LIGHT_ELEMENT] = 0f
        },
        [WATER_ELEMENT] = new()
        {
            [FIRE_ELEMENT] = 30f,
            [TREE_ELEMENT] = 0f,
            [ROCK_ELEMENT] = 0f,
            [THUNDER_ELEMENT] = -50f,
            [WATER_ELEMENT] = 20f,
            [DARK_ELEMENT] = 0f,
            [LIGHT_ELEMENT] = 0f
        },
        [DARK_ELEMENT] = new()
        {
            [FIRE_ELEMENT] = -10f,
            [TREE_ELEMENT] = -10f,
            [ROCK_ELEMENT] = -10f,
            [THUNDER_ELEMENT] = -10f,
            [WATER_ELEMENT] = -10f,
            [DARK_ELEMENT] = 0f,
            [LIGHT_ELEMENT] = -50f,
        },
        [LIGHT_ELEMENT] = new()
        {
            [FIRE_ELEMENT] = -10f,
            [TREE_ELEMENT] = -10f,
            [ROCK_ELEMENT] = -10f,
            [THUNDER_ELEMENT] = -10f,
            [WATER_ELEMENT] = -10f,
            [DARK_ELEMENT] = -50f,
            [LIGHT_ELEMENT] = 0f,
        }
    };


    /**
     * Id các map được dịch chuyển
     */
    public static readonly int[] TeleMapId = new int[] { 11, 19, 21, 22, 24, 22, 27, 26, 28 };
    //public static int[] TeleMapId = new int[] { 11, 19, 24, 22 };
    /**
     * Giá nâng kỹ năng theo từng giai đoạn
     */
    public static readonly int[] PriceUPSkill = new int[] { 3000, 6000, 10000, 14000, 18000, 22000, 26000, 30000, 34000, 38000 };

    /**
     * Số lượt cần để hồi xong 1 kỹ năng
     */
    public const int MAX_SKILL_COOLDOWN = 3;

    /**
     * Mẫu của npc
     */
    public static HashMap<int, NpcTemplate> npcTemplate = new();

    /**
     * Id các pet trong danh sách nhận pet miễn phí
     */
    public static readonly int[] petFreeIds = new int[] { 1, 2, 3, 5, 6 };

    /**
     * Map mẫu
     */
    public static HashMap<int, MapTemplate> mapTemplate = new();

    /**
     * Cửa hàng mẫu
     */
    public static HashMap<sbyte, ShopTemplate> shopTemplate = new();

    /**
     * Hành trang trang bị của thú cưng
     */
    public const sbyte EQUIP_PET_INVENTORY = 0;

    /**
     * Hành trang hay túi đồ của nhân vật
     */
    public const sbyte NORMAL_INVENTORY = 1;
    public const sbyte SKIN_INVENTORY = 2;
    public const sbyte WING_INVENTORY = 3;
    public const sbyte GEM_INVENTORY = 4;
    public const sbyte MONEY_INVENTORY = 5;
    /// <summary>
    /// Ngọc
    /// </summary>
    public const sbyte MONEY_TYPE_COIN = 1;
    /// <summary>
    /// Vàng
    /// </summary>
    public const sbyte MONEY_TYPE_GOLD = 0;
    /// <summary>
    /// Thỏi bạc
    /// </summary>
    public const sbyte MONEY_TYPE_SILVER_BAR = 2;
    /// <summary>
    /// Thỏi vàng
    /// </summary>
    public const sbyte MONEY_TYPE_GOLD_BAR = 3;
    /// <summary>
    /// Huyết ngọc
    /// </summary>
    public const sbyte MONEY_TYPE_BLOOD_GEM = 4;
    /// <summary>
    /// Quỹ bang
    /// </summary>
    public const sbyte MONEY_TYPE_FUND_CLAN = 5;
    /// <summary>
    /// Điểm phát triển bang
    /// </summary>
    public const sbyte MONEY_TYPE_GROWTH_POINT_CLAN = 6;
    /// <summary>
    /// Tinh thạch
    /// </summary>
    public const sbyte MONEY_TYPE_CRYSTAL_ITEM = 7;
    /// <summary>
    /// Lúa
    /// </summary>
    public const sbyte MONEY_TYPE_LUA = 8;
    /// <summary>
    /// Hoa vàng
    /// </summary>
    public const sbyte MONEY_TYPE_FLOWER_GOLD = 9;
    /// <summary>
    /// Hoa bạc
    /// </summary>
    public const sbyte MONEY_TYPE_FLOWER_COIN = 10;
    /// <summary>
    /// Bánh chưng
    /// </summary>
    public const sbyte MONEY_TYPE_SQUARE_COIN = 11;
    /// <summary>
    /// Bánh tét
    /// </summary>
    public const sbyte MONEY_TYPE_CYLINDRIAL_COIN = 12;
    public const int DAILY_STAR = 20;
    public const int STAR_JOIN_CHALLENGE = 2;
    public const int ITEM_OP_HP = 7;
    public const int ITEM_OP_MP = 8;
    /// <summary>
    /// Đường dẫn ảnh rỗng
    /// </summary>
    public const string EMPTY_IMG_PATH = "dialog/empty.png";
    /// <summary>
    /// Ki ốt nón
    /// </summary>
    public const sbyte KIOSK_HAT = 0;
    /// <summary>
    /// Ki ốt vũ khí
    /// </summary>
    public const sbyte KIOSK_WEAPON = 1;
    /// <summary>
    /// Ki ốt giáp
    /// </summary>
    public const sbyte KIOSK_AMOUR = 2;
    /// <summary>
    /// Ki ốt ngọc
    /// </summary>
    public const sbyte KIOSK_GEM = 3;
    /// <summary>
    /// Ki ốt thú cưng
    /// </summary>
    public const sbyte KIOSK_PET = 4;
    /// <summary>
    /// Ki ốt khác
    /// </summary>
    public const sbyte KIOSK_OTHER = 5;
    public const int HOUR_UPLOAD_ITEM = 24;
    public const int DEBUFF_NONONSKY = 100;
    public const int TYPE_SELECT_ENCHANT_MATERIAL1 = 7;
    public const int TYPE_SELECT_ENCHANT_MATERIAL2 = 8;
    public const int TYPE_SELECT_ITEM_UP_SKILL = 9;
    public const int TYPE_SELECT_ITEM_UP_TIER = 123;
    public const long CHANGE_CHANNEL_DELAY = 30000;
    public static readonly int[] ENCHANT_INFO = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 20 };
    /// <summary>
    /// Tỉ lệ cường hoá
    /// </summary>
    public static readonly float[] PERCENT_ENCHANT = new float[] { 90f, 80f, 70f, 60f, 50f, 30f, 20f, 5f, -10f, -20f };
    /// <summary>
    /// Tỉ lệ cường hoá hiển thị
    /// </summary>
    public static readonly float[] DISPLAY_PERCENT_ENCHANT = new float[] { 90f, 80f, 70f, 60f, 50f, 30f, 20f, 15f, 10f, 5f };
    public static readonly float[] PERCENT_UP_SKILL = new float[] { 90, 80, 60, 50, 40, 30, 10, 5, 2, 0, 0 };
    public static float[] PERCENT_UP_SKILL_SKY = new float[] { };
    public static readonly int[] PRICE_ENCHANT = new int[] { 5000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 50000 };
    public const int PERCENT_LVL_ITEM = 5;
    public const int DELAY_TURN_PET_BATTLE = 3000;
    public const int PRICE_UP_TIER_ITEM = 100000;
    public const float PERCENT_ITEM_TIER_INFO = 70f;
    public const int PART_NEED_MERGE_PET = 160;
    public const int PRICE_UP_TIER_PET = 10000;
    public const long MAX_TIME_BUFF_EXP = 1000 * 60 * 60 * 3;
    public const long TIME_BUFF_EXP = 1000 * 60 * 30;
    public const int LVL_PET_REQUIER_UP_TIER = 25;
    public const float KIOSK_PER_SELL = 5f;
    public const int DELAY_INVITE_PLAYER_CHALLENGE = 40000;
    public const int MAX_PK_POINT = 10;
    public const long MIN_PET_EXP_PK = -20000000;
    public const long TIME_DECREASE_PK_POINT = 1000 * 60 * 30;
    public const long PRICE_REVIVAL_PET_FATER_PK = 3000;
    public const float BET_PRICE_PLAYER_CHALLENGE = 10f;
    public const int LVL_PET_PASSIVE_REQUIER_UP_TIER = 10;
    public const int SILVER_BAR_ID = 186;
    public const int GOLD_BAR_ID = 187;
    public const int BLOOD_GEM_ID = 188;
    public const int CRYSTAL_ID = 318;
    public static readonly int[] ID_BOSS_CHALLENGE = new int[] { 11, 12, 13, 14, 15 };
    public static readonly int[] ID_BOSS_TASK = new int[] { 16 };
    public static readonly int[] LVL_REQUIRE_PET_TATTO = new int[] { 3, 5, 10, 15, 20, 25, 30, 35 };
    public static readonly int[] SPECIAL_PET_TO_LEARN_ALL_SKILL = new int[] { 3091, 10011 };
    public const int MOB_NEED_CAPTCHA = 125;
    public const long TIME_BOSS_DISPOINTED = 1000 * 60 * 10;
    public static readonly float[] PERCENT_OF_ENCHANT_GEM = new float[] { 70f, 65f, 60f, 55f, 50f, 40f, 30f, 20f, 10f, 2f };
    public static readonly float[] PERCENT_OF_ENCHANT_TATOO = new float[] { 60f, 55f, 50f, 40f, 30f, 20f, 15f, 10f, 5f, 2f };
    public const int PRICE_KEEP_GEM = 5000;
    public const int MAX_SLOT_SHOP_ARENA = 10;
    public const int DEFAULT_FREE_RESET_ARENA_SHOP = 2;
    public const int PRICE_RESET_SHOP_ARENA = 1000;
    public const int MAX_RESET_SHOP_ARENA = 3;
    public const long TIME_UNEQUIP_GEM = 1000 * 60 * 60;
    public const int PRICE_UNEQUIP_GEM = 2500;
    public const long COIN_CREATE_CLAN = 200000;
    public const long GOLD_CREATE_CLAN = 20000;
    public const int CLAN_MAX_LVL = 10;
    public const int PRICE_SILVER_BAR_CHANGE_GIFT = 10;
    public static readonly sbyte[] LVL_CLAN_NEED_TO_ADD_SLOT_SKILL = new sbyte[] { 3, 5, 7 };
    public static readonly int[] PRICE_RENT_SKILL = new int[] { 550, 100 };
    public static readonly long[] PRICE_BET_CHALLENGE = new long[] { 2000l, 10000l, 15000l };
    public static readonly byte[] NUM_LVL_DROP_ENCHANT_TATTO_FAILED = new byte[] { 0, 0, 0, 0, 1, 1, 1, 2, 2, 3 };
    public const int MAX_TIMES_SHOW_CAPTCHA = 5;
    public const int PRICE_COIN_ENCHANT_TATTO = 100000;
    public const int PRICE_GOLD_ENCHANT_TATTO = 2000;
    public const int PRICE_COIN_ARENA_JOURNALISM = 2000;
    public const int PRICE_GOLD_ARENA_JOURNALISM = 500;
    public const int PERCENT_EXCHANGE_GOLD_TO_COIN = 10;
    public const int PERCENT_EXCHANGE_LUA_TO_COIN = 10500;
    public const int GOLD_NEED_CHAT_GLOBAL = 200;
    /// <summary>
    /// Cái này là cần bao nhiêu ngọc
    /// </summary>
    public const int PERCENT_EXCHANGE_CON_TO_LUA_1 = 10500;
    /// <summary>
    /// Cái này là đổi xong được bao nhiêu lúa
    /// </summary>
    public const int PERCENT_EXCHANGE_CON_TO_LUA_2 = 1;
    public const int MAX_LVL_ENCHANT_WING = 10;
    public const float PERCENT_ADD_WHEN_ENCHANT_WING = 10f;
    public const int POINT_WHEN_KILL_MOB_CHALLENGE = 3;
    public static readonly Dictionary<int, EnchantWingData> EnchantWingData = new();
    public static readonly int[] ID_ITEM_SILVER = new int[] { 392, 395, 398, 401, 405, 408, 411, 414, 417 };
    public static readonly int[] ID_ITEM_SILVER2 = new int[] { 31, 34, 37, 40, 44, 47, 50, 53, 56 };
    public static readonly int[] ID_ITEM_PET_TIER_ONE = new int[] { 726, 728, 730, 732, 734, 738 };
    public static readonly int[] ID_ITEM_PET_TIER_TWO = new int[] { 740, 742, 744, 746, 748, 750, 756, 760, 762, 764, 766 };
    public static readonly int[] ID_ITEM_PART_PET_TIER_THREE = new int[] { 851, 852, 849, 850, 845, 846, 835, 802, 803 };
    public static readonly int[] ID_ITEM_PART_PET_TIER_FOUR = new int[] { 819, 1005, 1006, 820, 1001, 1002, 821, 1003, 1004 };
    public static readonly int[] ID_ITEM_PART_PET_TIER_FIVE = new int[]
    {
        822,
        823,
        1009,
        1010,
        824,
        1011,
        1012,
        825,
        826,
        1007,
        1008,
        827,
        828,
        829
    };
    public static readonly int[] ID_ITEM_PART_WING_TIER_1 = new int[] { 506, 507, 508, 509, 510 };
    public static readonly int[] ID_ITEM_PART_WING_TIER_2 = new int[] { 511, 512, 513, 514, 515 };
    public static readonly int[] ID_ITEM_PART_WING_TIER_3 = new int[] { 5155, 5156, 5157, 5158, 5159 };
    public static readonly int[] ID_ITEM_PART_HAI_TAC = new int[] { 420, 423, 427, 431, 433 };
    public static readonly int[] ID_ITEM_PART_TINH_VAN = new int[] { 435, 438, 442, 446, 448 };
    public static readonly int[] ID_ITEM_PART_HOANG_KIM = new int[] { 450, 452, 454, 456, 458, 460, 462, 464, 466, 468, 470, 472, 474, 476, 478 };
    public static int[] ID_ITEM_PART_PET_AO_ANH;
    public static readonly Version VERSION_132 = Version.Parse("1.3.2");
    public static readonly Version VERSION_133 = Version.Parse("1.3.3");
    public static readonly Version VERSION_134 = Version.Parse("1.3.4");
    public static readonly Version VERSION_135 = Version.Parse("1.3.5");
    public static readonly Version VERSION_136 = Version.Parse("1.3.6");
    public static readonly Version VERSION_137 = Version.Parse("1.3.7");
    public static readonly Version VERSION_142 = Version.Parse("1.4.2");
    /// <summary>
    /// Giá tiền kích hoạt tài khoản
    /// </summary>
    public const int PRICE_ACTIVE_USER = 20000;
    public const int TIME_DELAY_HEAL_WHEN_MOB_KILL_PET = 30000;
    /// <summary>
    /// Giá mở khóa ô kỹ năng bang hội
    /// </summary>
    public static readonly int[] PRICE_UNLOCK_SLOT_SKILL_CLAN = new int[] { 1000000, 2000000, 3000000 };
    public const string VI_CODE = "vi";
    public const string EN_CODE = "en";
    public static readonly Dictionary<string, LanguageData> Language = new Dictionary<string, LanguageData>()
    {
        [VI_CODE] = new LanguageData(),
        [EN_CODE] = ReadJsonFile<LanguageData>("/Language/en.json")
    };

    public static readonly Dictionary<sbyte, TradeGiftTemplate[]> TradeGift = new();

    public static readonly Dictionary<sbyte, Tuple<int[], int[], int>> TradeGiftPrice = new()
    {
        [TradeGiftTemplate.TYPE_COIN] = new Tuple<int[], int[], int>(new int[] { GopetManager.MONEY_TYPE_SILVER_BAR, GopetManager.MONEY_TYPE_COIN }, new int[] { 3, 50000 }, MenuController.OP_TRADE_GIFT_COIN),
        [TradeGiftTemplate.TYPE_LUA] = new Tuple<int[], int[], int>(new int[] { GopetManager.MONEY_TYPE_SILVER_BAR, GopetManager.MONEY_TYPE_LUA }, new int[] { 3, 5 }, MenuController.OP_TRADE_GIFT_LUA),
        [TradeGiftTemplate.TYPE_GOLD] = new Tuple<int[], int[], int>(new int[] { GopetManager.MONEY_TYPE_GOLD_BAR, GopetManager.MONEY_TYPE_GOLD }, new int[] { 3, 2500 }, MenuController.OP_TRADE_GIFT_GOLD)
    };
    /// <summary>
    /// Id npc trần chân
    /// </summary>
    public const int NPC_TRAN_CHAN = -1;
    /// <summary>
    /// Id npc tiên nữ
    /// </summary>
    public const int NPC_TIEN_NU = -2;
    /// <summary>
    /// Dữ liệu góp quỹ bang
    /// </summary>
    public static readonly CopyOnWriteArrayList<ClanMemberDonateInfo> clanMemberDonateInfos = new(new ClanMemberDonateInfo[]
    {
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_COIN, 100000, 100),
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_COIN, 1000000, 1000),
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_COIN, 10000000, 10000),
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 10, 1),
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 100, 10),
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 1000, 100),
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 10000,  1000),
        new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 100000,  10000)
    });

    public const int MAX_ITEM_MERGE_SERVER = int.MaxValue;
    public const int MAX_PET_MERGE_SERVER = 0;

    public static readonly int[] ID_ITEM_MERGE_SERVER = new int[]
    {
        150,151,152,153,154,154111,154112,154113,154114,154115,154116,154117,154118,154119,154120,154020,154121,154122,154123,154124,154125,154126,154127,154128,154129,154130,154131,121,122,123,124,145146,147,148,149,
    };

    public static readonly int[] ID_ITEM_EQUIP_HAI_TAC_MERGE_SERVER = new int[]
    {
        59,60,61,13008,62,63,64,65,66,67,68,69,70,71,2001,72,73,2002
    };
    public static readonly int[] ID_ITEM_EQUIP_TINH_VAN_MERGE_SERVER = new int[]
    {
         74,75,76,13009,77,78,79,80,81,82,83,84,85,86,2003,87,88,2004
    };

    public static readonly int[] ID_ITEM_EQUIP_SILVER_MERGE_SERVER = new int[]
    {
         392,393,394,395,396,397,398,399,400,401,402,403,404,405,406,407,408,409,410,411,412,413,414,415,416,417,418,419,31,32 ,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,
    };

    public static readonly int[] ID_PET_MERGE_SERVER = new int[]
   {
       3091,
       92,
       2098,
       3098
   };

    public static BossTemplate[] HourDailyBoss;
    public static HiddenStatItemTemplate[] HiddentStatItemTemplates;
    /// <summary>
    /// Dữ liệu dung hợp bằng vàng
    /// </summary>
    public static readonly Tuple<int, float>[] FusionGOLD = new Tuple<int, float>[]
    {
        new Tuple<int, float>(20000, 90),
        new Tuple<int, float>(20000, 90),
        new Tuple<int, float>(20000, 90),
        new Tuple<int, float>(20000, 90),

        new Tuple<int, float>(50000, 60),
        new Tuple<int, float>(50000, 60),
        new Tuple<int, float>(50000, 60),
        new Tuple<int, float>(50000, 60),

        new Tuple<int, float>(100000, 40),
        new Tuple<int, float>(100000, 40),
        new Tuple<int, float>(100000, 40),
        new Tuple<int, float>(100000, 40),

        new Tuple<int, float>(200000, 20),
        new Tuple<int, float>(200000, 20),
        new Tuple<int, float>(200000, 20),
        new Tuple<int, float>(200000, 20)
    };
    /// <summary>
    /// Dữ liệu dung hợp bằng ngọc
    /// </summary>
    public static readonly Tuple<int, float>[] FusionCOIN = new Tuple<int, float>[]
    {
        new Tuple<int, float>(500000, 90),
        new Tuple<int, float>(500000, 90),
        new Tuple<int, float>(500000, 90),
        new Tuple<int, float>(500000, 90),

        new Tuple<int, float>(1000000, 60),
        new Tuple<int, float>(1000000, 60),
        new Tuple<int, float>(1000000, 60),
        new Tuple<int, float>(1000000, 60),

        new Tuple<int, float>(2000000, 40),
        new Tuple<int, float>(2000000, 40),
        new Tuple<int, float>(2000000, 40),
        new Tuple<int, float>(2000000, 40),

        new Tuple<int, float>(5000000, 20),
        new Tuple<int, float>(5000000, 20),
        new Tuple<int, float>(5000000, 20),
        new Tuple<int, float>(5000000, 20)
    };

    public static readonly Tuple<int, float>[][] FusionData = new Tuple<int, float>[][]
    {
        FusionGOLD,
        FusionCOIN
    };
    /// <summary>
    /// Giá dung hợp thú cưng
    /// </summary>
    public static readonly int[] FusionPetPrice = new int[]
    {
        25000,
        500000
    };

    public static readonly int[] ReincarnationPetPrice = new int[]
    {
        200000,
        80000000
    };


    public const int GIÁ_NGỌC_NÂNG_GYM = 1000;

    public static readonly Tuple<int[][]>[] NOEL_DAILYS = new Tuple<int[][]>[]
    {
        new Tuple<int[][]>(new int[][] { new int[] { GIFT_ITEM, 198, 1, 0 } }),
        new Tuple<int[][]>(new int[][] { new int[] { GIFT_ITEM, 199, 2, 0 } }),
        new Tuple<int[][]>(new int[][] { new int[] { GIFT_ITEM, 200, 1, 0 }, new int[] { GIFT_ITEM, 184, 4, 0 } }),
        new Tuple<int[][]>(new int[][] { new int[] { GIFT_ITEM, 200, 2, 0 }, new int[] { GIFT_ITEM, 185, 1, 0 } }),
        new Tuple<int[][]>(new int[][] { new int[] { GIFT_ITEM, 180, 4, 0 }, new int[] { GIFT_ITEM, 185, 2, 0 } }),
        new Tuple<int[][]>(new int[][] { new int[] { GIFT_ITEM, 180, 5, 0 }, new int[] { GIFT_ITEM, 121, 3, 0 } }),
        new Tuple<int[][]>(new int[][] { new int[] { GIFT_ITEM, 122, 1, 0 }, new int[] { GIFT_SKIN, 1000012, 0, 0, 0, 7 } }),
    };

    public static readonly Dictionary<int, PetReincarnation> Reincarnations = new();
    public const int ID_ITEM_CARD_REINCARNATION = 332;
    public const int PRICE_ASSIGNED_PET = 15000;
    public const int PRICE_ASSIGNED_ITEM = 10000;
    public static readonly Dictionary<int, PetEffectTemplate> PET_EFF_TEMP = new();
    public static readonly int[] PERCENT_BUFF_ATK_ITEM = new int[] { 0, 10, 10, 10, 10, 10, 10, 10, 10, 15, 20, 20 };
    public static readonly int[] PERCENT_BUFF_DEF_ITEM = new int[] { 0, 2, 2, 2, 5, 5, 5, 6, 6, 8, 10, 10 };
    public static readonly int[] PERCENT_BUFF_MP_ITEM = new int[] { 0, 2, 2, 2, 5, 5, 5, 6, 6, 8, 10, 10 };
    public static readonly int[] PERCENT_BUFF_HP_ITEM = new int[] { 0, 10, 10, 10, 10, 10, 10, 15, 15, 20, 30, 30 };
    public static EmailService EmailService;
    public static readonly string EmailContent = File.ReadAllText("assets/html/email.html");
    static GopetManager()
    {
        SqlMapper.AddTypeHandler(new JsonAdapter<int[]>());
        SqlMapper.AddTypeHandler(new JsonAdapter<int[][]>());
        SqlMapper.AddTypeHandler(new JsonAdapter<sbyte[]>());
        SqlMapper.AddTypeHandler(new JsonAdapter<string[]>());
        SqlMapper.AddTypeHandler(new JsonAdapter<JArrayList<int>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<HashMap<sbyte, CopyOnWriteArrayList<Item>>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<Pet>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<ClanRequestJoin>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<ClanMember>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<int>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<TaskData>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<Achievement>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<ClanMember>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<ClanSkill>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<CopyOnWriteArrayList<Letter>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Pet>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Item>());
        SqlMapper.AddTypeHandler(new JsonAdapter<BuffExp>());
        SqlMapper.AddTypeHandler(new JsonAdapter<GopetCaptcha>());
        SqlMapper.AddTypeHandler(new JsonAdapter<ShopArena>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Dictionary<int, int>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Dictionary<int, DateTime>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Waypoint[]>());
        SqlMapper.AddTypeHandler(new JsonAdapter<PetSkillInfo[]>());
        SqlMapper.AddTypeHandler(new JsonAdapter<ItemInfo[]>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Dictionary<DateTime, Item>>());
        SqlMapper.AddTypeHandler(new VersionAdapter());
        shopTemplate.put(MenuController.SHOP_ARMOUR, new ShopTemplate(MenuController.SHOP_ARMOUR));
        shopTemplate.put(MenuController.SHOP_SKIN, new ShopTemplate(MenuController.SHOP_SKIN));
        shopTemplate.put(MenuController.SHOP_HAT, new ShopTemplate(MenuController.SHOP_HAT));
        shopTemplate.put(MenuController.SHOP_WEAPON, new ShopTemplate(MenuController.SHOP_WEAPON));
        shopTemplate.put(MenuController.SHOP_THUONG_NHAN, new ShopTemplate(MenuController.SHOP_THUONG_NHAN));
        shopTemplate.put(MenuController.SHOP_PET, new ShopTemplate(MenuController.SHOP_PET));
        shopTemplate.put(MenuController.SHOP_FOOD, new ShopTemplate(MenuController.SHOP_FOOD));
        shopTemplate.put(MenuController.SHOP_CLAN, new ShopTemplate(MenuController.SHOP_CLAN));
        shopTemplate.put(MenuController.SHOP_ENERGY, new ShopTemplate(MenuController.SHOP_ENERGY));
        shopTemplate.put(MenuController.SHOP_GIAN_THUONG, new ShopTemplate(MenuController.SHOP_GIAN_THUONG));
        shopTemplate.put(MenuController.SHOP_BIRTHDAY_EVENT, new ShopTemplate(MenuController.SHOP_BIRTHDAY_EVENT));
    }

    public static void readMobLvl(String cmd, HashMap<int, MobLvInfo> hashMap)
    {
        using (var conn = MYSQLManager.create())
        {
            IEnumerable<MobLvInfo> data = conn.Query<MobLvInfo>(cmd);
            foreach (var mobLvInfo in data)
            {
                hashMap.put(mobLvInfo.lvl, mobLvInfo);
            }
        }
    }

    public static void init()
    {
        ServerMonitor.LogInfo($"Trình dọn rác: {GCSettings.IsServerGC}");
        PERCENT_UP_SKILL_SKY = new float[38];
        System.Array.Fill(PERCENT_UP_SKILL_SKY, 0f);
        for (int i = 0; i < PERCENT_UP_SKILL_SKY.Length; i++)
        {
            if (i >= 21)
            {
                PERCENT_UP_SKILL_SKY[i] = 10f;
                continue;
            }
            else if (i >= 11)
            {
                PERCENT_UP_SKILL_SKY[i] = 30f;
                continue;
            }
            else
            {
                PERCENT_UP_SKILL_SKY[i] = 50f;
                continue;
            }
        }
        using (var conn = MYSQLManager.create())
        {
            PET_TEMPLATES.AddRange(conn.Query<PetTemplate>("SELECT * FROM `gopet_pet`"));
            PET_TEMPLATES.ForEach(petTemplate =>
            {
                petEnable.add(petTemplate);
                if (!typePetTemplate.ContainsKey(petTemplate.type))
                {
                    typePetTemplate.put(petTemplate.type, new());
                }
                typePetTemplate.get(petTemplate.type).add(petTemplate);
                PETTEMPLATE_HASH_MAP.put(petTemplate.petId, petTemplate);
            });
            ServerMonitor.LogInfo("Tải dữ liệu thú cưng từ cơ sở dữ liệu OK");
            itemTemplates.AddRange(conn.Query<ItemTemplate>("SELECT * FROM `item`"));
            int assetsId = 1;
            itemTemplates.ForEach(itemTemp =>
            {
                itemTemp.setIconId(assetsId);
                itemAssetsIcon[assetsId] = itemTemp.getIconPath();
                itemTemplate.put(itemTemp.getItemId(), itemTemp);
                if (itemTemp.getType() != ITEM_ADMIN)
                {
                    NonAdminItemList.add(itemTemp);
                }
                assetsId++;
            });
            ServerMonitor.LogInfo("Tải dữ liệu vật phẩm từ cơ sở dữ liệu OK");
            IEnumerable<MobLvInfo> data = conn.Query<MobLvInfo>("SELECT * FROM `gopet_mob`");
            foreach (var mobLvInfo in data)
            {
                MOBLVLINFO_HASH_MAP[mobLvInfo.lvl] = mobLvInfo;
            }
            ServerMonitor.LogInfo("Tải dữ liệu quái từ cơ sở dữ liệu OK");
            IEnumerable<EnchantWingData> enchantWing = conn.Query<EnchantWingData>("SELECT * FROM `enchant_wing_data`");
            foreach (var wingData in enchantWing)
            {
                EnchantWingData[wingData.Level] = wingData;
            }
            ServerMonitor.LogInfo("Tải dữ liệu cường hóa cánh từ cơ sở dữ liệu OK");
            IEnumerable<ShopTemplateItem> shopitemTemplate = conn.Query<ShopTemplateItem>("SELECT * FROM `shop`");
            foreach (var shopTemplate1 in shopitemTemplate)
            {
                if (shopTemplate.ContainsKey(shopTemplate1.shopId))
                {
                    shopTemplate.get(shopTemplate1.shopId).getShopTemplateItems().add(shopTemplate1);
                }
                else
                {
                    throw new UnsupportedOperationException(" khong ho tro loai shop " + shopTemplate1.shopId);
                }
            }
            ServerMonitor.LogInfo("Tải dữ liệu cửa hàng từ cơ sở dữ liệu OK");
            IEnumerable<BossTemplate> bossTemArr = conn.Query<BossTemplate>("SELECT * FROM `boss`");
            foreach (var bossTemplate in bossTemArr)
            {
                boss[bossTemplate.bossId] = bossTemplate;
            }
            HourDailyBoss = bossTemArr.Where(x => x.typeBoss == 4).ToArray();
            ServerMonitor.LogInfo("Tải dữ liệu boss từ cơ sở dữ liệu OK");
            IEnumerable<MapTemplate> mapTemplates = conn.Query<MapTemplate>("SELECT * FROM `map` WHERE `map`.`enable` = true;");
            foreach (var mTem in mapTemplates)
            {
                mapTemplate[mTem.mapId] = mTem;
            }
            ServerMonitor.LogInfo("Tải dữ liệu map từ cơ sở dữ liệu OK");
            TradeGift[TradeGiftTemplate.TYPE_COIN] = conn.Query<TradeGiftTemplate>("SELECT * FROM `trade_gift` where Type = " + TradeGiftTemplate.TYPE_COIN).ToArray();
            TradeGift[TradeGiftTemplate.TYPE_GOLD] = conn.Query<TradeGiftTemplate>("SELECT * FROM `trade_gift` where Type = " + TradeGiftTemplate.TYPE_GOLD).ToArray();
            TradeGift[TradeGiftTemplate.TYPE_LUA] = TradeGift[TradeGiftTemplate.TYPE_COIN];
            ServerMonitor.LogInfo("Tải dữ liệu trao đổi thưởng từ cơ sở dữ liệu OK");
            SHOP_ARENA_TEMPLATE = conn.Query<ShopArenaTemplate>("SELECT * FROM `shoparena`").ToArray();
            ServerMonitor.LogInfo("Tải dữ liệu shop đấu trường từ cơ sở dữ liệu OK");
            var listExp = conn.Query("SELECT * FROM `petexp`");
            foreach (var exp in listExp)
            {
                PetExp.put(exp.petLvl, exp.exp);
            }
            ServerMonitor.LogInfo("Tải dữ liệu exps từ cơ sở dữ liệu OK");
            var listIteminfo = conn.Query("SELECT * FROM `iteminfo`");
            foreach (var item in listIteminfo)
            {
                int ID = item.ID;
                String name = item.name;
                bool isPercent = item.isPercent;
                bool canFormat = name.Contains("%s");
                itemInfoName.put(ID, name);
                itemInfoIsPercent.put(ID, isPercent);
                itemInfoCanFormat.put(ID, canFormat);
            }
            ServerMonitor.LogInfo("Tải dữ liệu các dòng từ cơ sở dữ liệu OK");
            var listSkill = conn.Query("SELECT * FROM `skill`");
            foreach (var skill in listSkill)
            {
                PetSkill petSkill = new PetSkill();
                petSkill.skillID = skill.skillID;
                petSkill.name = skill.name;
                petSkill.description = skill.description;
                petSkill.nClass = skill.nClass;
                petSkill.IsNeedCard = skill.IsNeedCard;
                var listSkillLv = conn.Query(Utilities.Format("SELECT * FROM `skilllv` WHERE skillID = %s ORDER BY lv ASC;", petSkill.skillID));
                foreach (var item in listSkillLv)
                {
                    PetSkillLv petSkillLv = new PetSkillLv();
                    petSkillLv.lv = item.lv;
                    petSkillLv.mpLost = item.mpLost;
                    petSkillLv.skillInfo = JsonConvert.DeserializeObject<PetSkillInfo[]>(item.skillInfo);
                    petSkill.skillLv.add(petSkillLv);
                }
                PETSKILL_HASH_MAP.put(petSkill.skillID, petSkill);
                if (!NCLASS_PETSKILL_HASH_MAP.ContainsKey(petSkill.nClass))
                {
                    NCLASS_PETSKILL_HASH_MAP.put(petSkill.nClass, new());
                }
                NCLASS_PETSKILL_HASH_MAP.get(petSkill.nClass).add(petSkill);
                PET_SKILLS.add(petSkill);
            }
            ServerMonitor.LogInfo("Tải dữ liệu kỹ năng pet từ cơ sở dữ liệu OK");
            HashMap<int, JArrayList<MobLvlMap>> mobLvlMap_ = new();
            var mobLvlMapList = conn.Query("SELECT * FROM `gopet_map_moblvl`");
            foreach (var item in mobLvlMapList)
            {
                MobLvlMap mobLvlMap = new MobLvlMap(item.mapID, item.lvlFrom, item.lvlTo, item.petId);
                if (!mobLvlMap_.ContainsKey(mobLvlMap.getMapId()))
                {
                    mobLvlMap_.put(mobLvlMap.getMapId(), new());
                }
                mobLvlMap_.get(mobLvlMap.getMapId()).add(mobLvlMap);
            }
            foreach (var entry in mobLvlMap_)
            {
                int key = entry.Key;
                JArrayList<MobLvlMap> val = entry.Value;
                MOBLVL_MAP.put(key, val.ToArray());
            }
            var mobLocationList = conn.Query("SELECT * FROM `gopet_mob_location`");
            HashMap<int, JArrayList<MobLocation>> mobLoc = new();
            foreach (var item in mobLocationList)
            {
                MobLocation mobLocation1 = new MobLocation(item.mapID, item.x, item.y);
                if (!mobLoc.ContainsKey(mobLocation1.getMapId()))
                {
                    mobLoc.put(mobLocation1.getMapId(), new());
                }
                mobLoc.get(mobLocation1.getMapId()).add(mobLocation1);
            }
            foreach (var entry in mobLoc)
            {
                int key = entry.Key;
                JArrayList<MobLocation> val = entry.Value;
                mobLocation.put(key, val.ToArray());
            }
            var npcList = conn.Query<NpcTemplate>("SELECT * FROM `npc`");
            foreach (var npcTemp in npcList)
            {
                npcTemplate.put(npcTemp.getNpcId(), npcTemp);
            }
            var tattoList = conn.Query<PetTattoTemplate>("SELECT * FROM `tattoo`");
            foreach (var petTattoTemplate in tattoList)
            {
                tattos.put(petTattoTemplate.tattooId, petTattoTemplate);
            }
            var dropItemList = conn.Query<DropItem>("SELECT * FROM `drop_item`");
            foreach (var dropItem1 in dropItemList)
            {
                if (!dropItem.ContainsKey(dropItem1.getMapId()))
                {
                    dropItem.put(dropItem1.getMapId(), new());
                }
                if (dropItem1.getPercent() < 0f)
                {
                    continue;
                }
                dropItem.get(dropItem1.getMapId()).add(dropItem1);
            }
            var itemTierList = conn.Query<TierItem>("SELECT * FROM `tier_item`");
            foreach (var tierItem1 in itemTierList)
            {
                tierItem.put(tierItem1.itemTemplateIdTier1, tierItem1);
            }
            var petTierList = conn.Query<PetTier>("SELECT * FROM `pet_tier`");
            foreach (var petTier1 in petTierList)
            {
                petTier.put(petTier1.getPetTemplateId1(), petTier1);
            }
            var clanTemplateData = conn.Query<ClanTemplate>("SELECT * FROM `clan_template` ORDER BY `clan_template`.`clanLvl` ASC");
            foreach (var clanTemplate in clanTemplateData)
            {
                clanTemp.put(clanTemplate.getLvl(), clanTemplate);
            }
            var taskDataTemp = conn.Query<TaskTemplate>("SELECT * FROM `task`");
            foreach (var taskTemp in taskDataTemp)
            {
                taskTemplate.put(taskTemp.getTaskId(), taskTemp);
                taskTemplateList.add(taskTemp);
                if (!taskTemplateByType.ContainsKey(taskTemp.getType()))
                {
                    taskTemplateByType.put(taskTemp.getType(), new());
                }
                taskTemplateByType.get(taskTemp.getType()).add(taskTemp);
                if (!taskTemplateByNpcId.ContainsKey(taskTemp.getFromNpc()))
                {
                    taskTemplateByNpcId.put(taskTemp.getFromNpc(), new());
                }
                taskTemplateByNpcId.get(taskTemp.getFromNpc()).add(taskTemp);
            }
            achievements = conn.Query<AchievementTemplate>("SELECT * FROM `achievement`");
            foreach (var item in achievements)
            {
                AchievementMAP[item.IdTemplate] = item;
            }
            ServerMonitor.LogInfo("Tải dữ liệu danh hiệu từ cơ sở dữ liệu OK");
            Summer2024Event.EventDatas = conn.Query<Summer2024Event.EventData>("SELECT * FROM `summer_2024_event`");
            var clanSKillTemplates = conn.Query<ClanSkillTemplate>("SELECT * FROM `clan_skill`");
            clanSkillTemplateList.AddRange(clanSKillTemplates);
            foreach (var template in clanSKillTemplates)
            {
                ClanSkillViaId[template.id] = template;
                template.clanSkillLvlTemplates = conn.Query<ClanSkillLvlTemplate>("SELECT * FROM `clan_skill_lvl` WHERE skillId = @skillId ORDER BY `clan_skill_lvl`.`lvl` ASC", new { skillId = template.id }).ToArray();
            }
            HiddentStatItemTemplates = conn.Query<HiddenStatItemTemplate>("SELECT * FROM `hidden_stat`").ToArray();
            var reincarnations = conn.Query<PetReincarnation>("SELECT * FROM reincarnation");
            foreach (var reincarnation in reincarnations)
            {
                Reincarnations[reincarnation.PetId] = reincarnation;
            }
            ServerMonitor.LogInfo("Tải dữ liệu trùng sinh thú cưng từ cơ sở dữ liệu OK");
            /*var petEffectTemplates = conn.Query<PetEffectTemplate>("SELECT * FROM `pet_eff`");
            foreach (var item in petEffectTemplates)
            {
                PET_EFF_TEMP[item.IdTemplate] = item;
            }*/
            ServerMonitor.LogInfo("Tải dữ liệu hiệu ứng thú cưng từ cơ sở dữ liệu OK");
        }
        using (var connWeb = MYSQLManager.createWebMySqlConnection())
        {
            EXCHANGE_DATAS.AddRange(connWeb.Query<ExchangeData>("SELECT * FROM `exchange`"));
            EXCHANGE_DATAS.ForEach(exchangeData => MenuController.EXCHANGE_ITEM_INFOS.add(new ExchangeItemInfo(exchangeData)));
            ServerInfos = connWeb.Query<ServerInfo>("SELECT * FROM `server`");
        }
        foreach (var petTemplate in PET_TEMPLATES)
        {
            if (!ListPetMustntUpTier.Contains(petTemplate.petId) && petTier.Where(p => p.Value.petTemplateId2 == petTemplate.petId).Any())
            {
                ListPetMustntUpTier.Add(petTemplate.petId);
            }
        }
        foreach (var entry in tierItem)
        {
            TierItem val = entry.Value;
            if (tierItemHashMap.ContainsKey(val.itemTemplateIdTier1) || tierItemHashMap.ContainsKey(val.itemTemplateIdTier2))
            {
                continue;
            }
            JArrayList<int> map = findListTierId(val);
            tierItemHashMap.put(val.itemTemplateIdTier1, 1);
            for (int i = 0; i < map.Count; i++)
            {
                int get = map.get(i);
                tierItemHashMap.put(get, i + 2);
            }
        }
        foreach (var item in npcTemplate)
        {
            Language[VI_CODE].NpcNameLanguage[item.Key] = item.Value.name;
            for (global::System.Int32 i = 0; i < item.Value.optionId.Length; i++)
            {
                Language[VI_CODE].NpcOptionLanguage[item.Value.optionId[i]] = item.Value.optionName[i];
            }
        }
        foreach (var item in itemTemplates)
        {
            Language[VI_CODE].ItemLanguage[item.itemId] = item.name;
            Language[VI_CODE].ItemDescLanguage[item.itemId] = item.description;
        }
        foreach (var item in tattos)
        {
            Language[VI_CODE].TattoLanguage[item.Key] = item.Value.name;
        }
        foreach (var item in mapTemplate)
        {
            Language[VI_CODE].MapLanguage[item.Key] = item.Value.name;
        }
        foreach (var item in taskTemplate)
        {
            Language[VI_CODE].TaskNameLanguage[item.Key] = item.Value.name;
            Language[VI_CODE].TaskDescLanguage[item.Key] = item.Value.description;
        }
        foreach (var item in PETTEMPLATE_HASH_MAP)
        {
            Language[VI_CODE].PetNameLanguage[item.Key] = item.Value.name;
        }
        foreach (var item in itemInfoName)
        {
            Language[VI_CODE].ItemInfoNameLanguage[item.Key] = item.Value;
        }
        foreach (var item in PET_SKILLS)
        {
            Language[VI_CODE].SkillNameLanguage[item.skillID] = item.name;
            Language[VI_CODE].SkillDescLanguage[item.skillID] = item.description;
        }
        foreach (var item in AchievementMAP)
        {
            Language[VI_CODE].AchievementNameLanguage[item.Key] = item.Value.Name;
            Language[VI_CODE].AchievementDescLanguage[item.Key] = item.Value.Description;
        }
        foreach (var item in boss)
        {
            Language[VI_CODE].BossNameLanguage[item.Key] = item.Value.name;
        }
        new AutoMaintenance().Start(5, 0);
        ID_ITEM_PART_PET_AO_ANH = petTier.Values.Select(x => itemTemplates.Where(item => item.type == ITEM_PART_PET && item.itemOption.Length > 0 && item.itemOption[0] == 4 && item.itemOptionValue[0] == x.petTemplateIdNeed).Select(m => m.itemId).FirstOrDefault()).ToArray();
        EmailService = new EmailService(ConfigurationManager.AppSettings.Get("email-serivce-config"));
        //File.WriteAllText(Directory.GetCurrentDirectory() + "/pet.json", JsonConvert.SerializeObject(ID_ITEM_PART_PET_AO_ANH.Select(x => new { id = x, name = GopetManager.itemTemplate[x].name })));
        //SaveJsonFile(Language["vi"], "/lang/vi.json");
    }

    public static T ReadJsonFile<T>(string targetPath)
    {
        if (File.Exists(Directory.GetCurrentDirectory() + targetPath))
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(Directory.GetCurrentDirectory() + targetPath));
        }
        return default(T);
    }

    public static void SaveJsonFile<T>(T json, string file)
    {
        if (!File.Exists(Directory.GetCurrentDirectory() + file))
        {
            FileInfo fileInfo = new FileInfo(Directory.GetCurrentDirectory() + file);
            fileInfo.Directory.Create();
            using (var st = fileInfo.Create())
            {
                StreamWriter streamWriter = new StreamWriter(st);
                streamWriter.Write(JsonConvert.SerializeObject(json, Formatting.Indented));
                streamWriter.Flush();
                streamWriter.Close();
            }
            return;
        }
        File.WriteAllText(Directory.GetCurrentDirectory() + file, JsonConvert.SerializeObject(json, Formatting.Indented));
    }

    public static JArrayList<int> findListTierId(TierItem tInfo)
    {
        JArrayList<int> list = new();
        list.add(tInfo.itemTemplateIdTier2);
        foreach (var entry in tierItem)
        {
            TierItem val = entry.Value;
            if (val.itemTemplateIdTier1 == tInfo.itemTemplateIdTier2)
            {
                list.AddRange(findListTierId(val));
            }
        }
        return list;
    }
    /// <summary>
    /// Tải dữ liệu chợ
    /// </summary>
    public static void loadMarket()
    {
        using (var conn = MYSQLManager.create())
        {
            var marketData = conn.QueryFirstOrDefault("SELECT *, UNIX_TIMESTAMP(TimeSave) * 1000 AS milliseconds FROM `market` ORDER BY `market`.`TimeSave` DESC");
            if (marketData != null)
            {
                MarketPlace.setKiosks(JsonConvert.DeserializeObject<Kiosk[]>(marketData.Data));
                conn.Execute("DELETE FROM `market` WHERE (UNIX_TIMESTAMP(TimeSave) * 1000) + 1000 * 60 * 60 * 24 * 7 < @TimeReigonNeedDetele",
                  new { TimeReigonNeedDetele = marketData.milliseconds });
            }
        }
    }


    private static DateTime OldDateTime = DateTime.Now;

    private static StreamWriter __writer;

    public static StreamWriter Writer
    {
        get
        {
            if (__writer == null)
            {
                FileInfo fileInfo = new FileInfo(Directory.GetCurrentDirectory() + $"/log/log_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}.txt");
                fileInfo.Directory.Create();
                __writer?.Close();
                Console.WriteLine(fileInfo.FullName);
                OldDateTime = DateTime.Now;
                __writer = new StreamWriter(fileInfo.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite), System.Text.Encoding.UTF8);
                return __writer;
            }
            return __writer;
        }
    }
    /// <summary>
    /// Lấy icon hiển thị nguyên tố
    /// </summary>
    /// <param name="typeE">Loại nguyên tố</param>
    /// <returns>Trả về icon text để client render</returns>
    public static string GetElementDisplay(sbyte typeE, Player player)
    {
        switch (typeE)
        {
            case FIRE_ELEMENT: return "(fire)";
            case WATER_ELEMENT: return "(water)";
            case LIGHT_ELEMENT: return "(light)";
            case DARK_ELEMENT: return "(dark)";
            case TREE_ELEMENT: return "(tree)";
            case ROCK_ELEMENT: return "(rock)";
            case THUNDER_ELEMENT: return "(thunder)";
        }
        return string.Empty;
    }
    [SuppressMessage("Style", "IDE0066:Dùng switch")]
    public static string GetClassDisplay(sbyte nclass, Player player)
    {
        switch (nclass)
        {
            case Assassin: return player.Language.Assassin;
            case Wizard: return player.Language.Wizard;
            case Fighter: return player.Language.Fighter;
            case Demon: return player.Language.Demon;
            case Angel: return player.Language.Angel;
            case Archer: return player.Language.Archer;
        }
        return string.Empty;
    }


    public static string GetElementDisplay(sbyte typeE, sbyte nClass, Player player)
    {
        return string.Concat(GetClassDisplay(nClass, player), " ", GetElementDisplay(typeE, player));
    }
    /// <summary>
    /// Lưu dữ liệu chợ
    /// </summary>
    public static void saveMarket()
    {
        using (var conn = MYSQLManager.create())
        {
            conn.Execute("INSERT INTO `market`(`Data`) VALUES (@Data)", new
            {
                Data = JsonConvert.SerializeObject(MarketPlace.kiosks)
            });
        }
    }


    public static Func<Version, bool> GreaterThan(Version version)
    {
        return (p) => p > version;
    }

    public static Func<Version, bool> LessThan(Version version)
    {
        return (p) => p < version;
    }

    public static Func<Version, bool> LessThanAndEquals(Version version)
    {
        return (p) => p <= version;
    }

    public static void SendHtmlMailAsync(string to,string title, string content)
    {
        //EmailService.SendEmailAsync(to, title, EmailContent.Replace("{0}", content), "html");
    }
}

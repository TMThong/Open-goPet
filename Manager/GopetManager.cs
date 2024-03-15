
using Dapper;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.Mob;
using Gopet.Data.User;
using Gopet.Util;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;
using Newtonsoft.Json;
using Gopet.Data.Dialog;
using Org.BouncyCastle.Asn1.Crmf;
using Gopet.Adapter;
using Gopet.Data.item;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

public class GopetManager
{

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

    public static JArrayList<ClanBuffTemplate> CLAN_BUFF_TEMPLATES = new();

    public static HashMap<int, ClanBuffTemplate> CLANBUFF_HASH_MAP = new();

    public static HashMap<int, ClanHouseTemplate> clanSkillHouseTemp = new();

    public static HashMap<int, ClanHouseTemplate> clanMarketHouseTemp = new();

    public static HashMap<int, JArrayList<ShopClanTemplate>> shopClanByLvl = new();

    public static HashMap<int, int> tierItemHashMap = new();


    public static JArrayList<ExchangeData> EXCHANGE_DATAS = new();

    public static JArrayList<ItemTemplate> NonAdminItemList = new();


    public static readonly List<ItemTemplate> itemTemplates = new JArrayList<ItemTemplate>();

    public static readonly Dictionary<int, string> itemAssetsIcon = new();

    public static Gopet.Logging.Monitor ServerMonitor { get; } = new Gopet.Logging.Monitor("Máy chủ");

    /*
     * Truyền vào là pet của mình
     * Rồi sau đó là pet của đối phương
     */
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
    public static int[] TeleMapId = new int[] { 11, 19, 24, 22, 27, 26, 28 };
    //public static int[] TeleMapId = new int[] { 11, 19, 24, 22 };
    /**
     * Giá nâng kỹ năng theo từng giai đoạn
     */
    public static int[] PriceUPSkill = new int[] { 3000, 6000, 10000, 14000, 18000, 22000, 26000, 30000, 34000, 38000 };

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
    public static int[] petFreeIds = new int[] { 1, 2, 3, 5, 6 };

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

    public const sbyte MONEY_TYPE_COIN = 1;

    public const sbyte MONEY_TYPE_GOLD = 0;

    public const sbyte MONEY_TYPE_SILVER_BAR = 2;

    public const sbyte MONEY_TYPE_GOLD_BAR = 3;

    public const sbyte MONEY_TYPE_BLOOD_GEM = 4;

    public const sbyte MONEY_TYPE_FUND_CLAN = 5;

    public const sbyte MONEY_TYPE_GROWTH_POINT_CLAN = 6;

    public const int DAILY_STAR = 20;

    public const int STAR_JOIN_CHALLENGE = 2;
    public const int ITEM_OP_HP = 7;
    public const int ITEM_OP_MP = 8;

    public const String EMPTY_IMG_PATH = "dialog/empty.png";
    public const sbyte KIOSK_HAT = 0;
    public const sbyte KIOSK_WEAPON = 1;
    public const sbyte KIOSK_AMOUR = 2;
    public const sbyte KIOSK_GEM = 3;
    public const sbyte KIOSK_PET = 4;
    public const sbyte KIOSK_OTHER = 5;
    public const int HOUR_UPLOAD_ITEM = 24;
    public const int DEBUFF_NONONSKY = 100;
    public const int TYPE_SELECT_ENCHANT_MATERIAL1 = 7;
    public const int TYPE_SELECT_ENCHANT_MATERIAL2 = 8;
    public const int TYPE_SELECT_ITEM_UP_SKILL = 9;
    public const int TYPE_SELECT_ITEM_UP_TIER = 123;
    public const long CHANGE_CHANNEL_DELAY = 30000;
    public static int[] ENCHANT_INFO = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 20 };
    public static float[] PERCENT_ENCHANT = new float[] { 90f, 80f, 70f, 60f, 50f, 30f, 20f, 5f, -10f, -20f };
    public static float[] DISPLAY_PERCENT_ENCHANT = new float[] { 90f, 80f, 70f, 60f, 50f, 30f, 20f, 15f, 10f, 5f };
    public static float[] PERCENT_UP_SKILL = new float[] { 90, 80, 60, 50, 40, 30, 10, 5, 2, 0 , 0};
    public static int[] PRICE_ENCHANT = new int[] { 5000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 50000 };
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
    public static int[] ID_BOSS_CHALLENGE = new int[] { 11, 12, 13, 14, 15 };
    public static int[] ID_BOSS_TASK = new int[] { 16 };
    public static int[] LVL_REQUIRE_PET_TATTO = new int[] { 3, 5, 10, 15, 20, 25, 30, 35 };
    public static int[] SPECIAL_PET_TO_LEARN_ALL_SKILL = new int[] { 3091 };
    public const int MOB_NEED_CAPTCHA = 125;
    public const long TIME_BOSS_DISPOINTED = 1000 * 60 * 10;
    public static float[] PERCENT_OF_ENCHANT_GEM = new float[] { 70f, 65f, 60f, 55f, 50f, 40f, 30f, 20f, 10f, 2f };
    public static float[] PERCENT_OF_ENCHANT_TATOO = new float[] { 60f, 55f, 50f, 40f, 30f, 20f, 15f, 10f, 5f, 2f };
    public const int PRICE_KEEP_GEM = 5000;
    public const int MAX_SLOT_SHOP_ARENA = 6;
    public const int DEFAULT_FREE_RESET_ARENA_SHOP = 2;
    public const int PRICE_RESET_SHOP_ARENA = 1000;
    public const int MAX_RESET_SHOP_ARENA = 5;
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
    public const int PERCENT_EXCHANGE_GOLD_TO_COIN = 1;
    public const int MAX_LVL_ENCHANT_WING = 10;
    public const float PERCENT_ADD_WHEN_ENCHANT_WING = 10f;
    public const int POINT_WHEN_KILL_MOB_CHALLENGE = 3;

    public static readonly Dictionary<int, EnchantWingData> EnchantWingData = new();

    public static readonly int[] ID_ITEM_SILVER = new int[] { 392, 395, 398, 401, 405, 408, 411, 414, 417 };
    public static readonly int[] ID_ITEM_PET_TIER_ONE = new int[] { 726, 728, 730, 732, 734, 738 };
    public static readonly int[] ID_ITEM_PET_TIER_TOW = new int[] { 740, 742, 744, 746, 748, 750, 756, 760, 762, 764, 766 };

    public const int PRICE_ACTIVE_USER = 20000;
    public const int TIME_DELAY_HEAL_WHEN_MOB_KILL_PET = 10000;

    public static readonly Dictionary<sbyte, TradeGiftTemplate[]> TradeGift = new();

    public static readonly Dictionary<sbyte, Tuple<int[], int[], int>> TradeGiftPrice = new()
    {
        [TradeGiftTemplate.TYPE_COIN] = new Tuple<int[], int[], int>(new int[] { GopetManager.MONEY_TYPE_SILVER_BAR, GopetManager.MONEY_TYPE_COIN }, new int[] { 3, 200 }, MenuController.OP_TRADE_GIFT_COIN),
        [TradeGiftTemplate.TYPE_GOLD] = new Tuple<int[], int[], int>(new int[] { GopetManager.MONEY_TYPE_GOLD_BAR, GopetManager.MONEY_TYPE_GOLD }, new int[] { 3, 100 }, MenuController.OP_TRADE_GIFT_GOLD)
    };

    public const int NPC_TRAN_CHAN = -1;
    public const int NPC_TIEN_NU = -2;

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
        SqlMapper.AddTypeHandler(new JsonAdapter<Pet>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Item>());
        SqlMapper.AddTypeHandler(new JsonAdapter<BuffExp>());
        SqlMapper.AddTypeHandler(new JsonAdapter<GopetCaptcha>());
        SqlMapper.AddTypeHandler(new JsonAdapter<ShopArena>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Dictionary<int, int>>());
        SqlMapper.AddTypeHandler(new JsonAdapter<Waypoint[]>());
        shopTemplate.put(MenuController.SHOP_ARMOUR, new ShopTemplate(MenuController.SHOP_ARMOUR));
        shopTemplate.put(MenuController.SHOP_SKIN, new ShopTemplate(MenuController.SHOP_SKIN));
        shopTemplate.put(MenuController.SHOP_HAT, new ShopTemplate(MenuController.SHOP_HAT));
        shopTemplate.put(MenuController.SHOP_WEAPON, new ShopTemplate(MenuController.SHOP_WEAPON));
        shopTemplate.put(MenuController.SHOP_THUONG_NHAN, new ShopTemplate(MenuController.SHOP_THUONG_NHAN));
        shopTemplate.put(MenuController.SHOP_PET, new ShopTemplate(MenuController.SHOP_PET));
        shopTemplate.put(MenuController.SHOP_FOOD, new ShopTemplate(MenuController.SHOP_FOOD));
        shopTemplate.put(MenuController.SHOP_CLAN, new ShopTemplate(MenuController.SHOP_CLAN));
        shopTemplate.put(MenuController.SHOP_ENERGY, new ShopTemplate(MenuController.SHOP_ENERGY));
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
            ServerMonitor.LogInfo("Tải dữ liệu boss từ cơ sở dữ liệu OK");

            IEnumerable<MapTemplate> mapTemplates = conn.Query<MapTemplate>("SELECT * FROM `map` WHERE `map`.`enable` = true;");
            foreach (var mTem in mapTemplates)
            {
                mapTemplate[mTem.mapId] = mTem;
            }
            ServerMonitor.LogInfo("Tải dữ liệu map từ cơ sở dữ liệu OK");

            TradeGift[TradeGiftTemplate.TYPE_COIN] = conn.Query<TradeGiftTemplate>("SELECT * FROM `trade_gift` where Type = " + TradeGiftTemplate.TYPE_COIN).ToArray();
            TradeGift[TradeGiftTemplate.TYPE_GOLD] = conn.Query<TradeGiftTemplate>("SELECT * FROM `trade_gift` where Type = " + TradeGiftTemplate.TYPE_GOLD).ToArray();

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
                tattos.put(petTattoTemplate.getTattooId(), petTattoTemplate);
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
        }

        using (var connWeb = MYSQLManager.createWebMySqlConnection())
        {
            EXCHANGE_DATAS.AddRange(connWeb.Query<ExchangeData>("SELECT * FROM `exchange`"));
            EXCHANGE_DATAS.ForEach(exchangeData => MenuController.EXCHANGE_ITEM_INFOS.add(new ExchangeItemInfo(exchangeData)));

        }
        /*
        resultSet = MYSQLManager.jquery("SELECT * FROM `shop_clan`");
        while (resultSet.next())
        {
            ShopClanTemplate shopTemplate1 = new ShopClanTemplate();
            shopTemplate1.setId(resultSet.getInt("Id"));
            shopTemplate1.setNeedShopClanLvl(resultSet.getInt("needShopClanLvl"));
            shopTemplate1.setComment(resultSet.getString("comment"));
            shopTemplate1.setPercent(resultSet.getFloat("percent"));
            shopTemplate1.setOption(JsonConvert.DeserializeObject<int[][]>(resultSet.getString("itemOptionValue")));
            if (!shopClanByLvl.ContainsKey(shopTemplate1.getNeedShopClanLvl()))
            {
                shopClanByLvl.put(shopTemplate1.getNeedShopClanLvl(), new());
            }
            shopClanByLvl.get(shopTemplate1.getNeedShopClanLvl()).add(shopTemplate1);
        }


        resultSet = MYSQLManager.jquery("SELECT * FROM `clan_buff_template`");
        while (resultSet.next())
        {
            ClanBuffTemplate clanBuffTemplate = new ClanBuffTemplate();
            clanBuffTemplate.setBuffId(resultSet.getInt("buffId"));
            clanBuffTemplate.setPotentialPointNeed(resultSet.getInt("potentialPointNeed"));
            clanBuffTemplate.setValuePerLevel(resultSet.getInt("valuePerlvl"));
            clanBuffTemplate.setLvlClan(resultSet.getInt("lvlClan"));
            clanBuffTemplate.setPercent(resultSet.getsbyte("isPercent") == 1);
            clanBuffTemplate.setName(resultSet.getString("name"));
            clanBuffTemplate.setDesc(resultSet.getString("descBuff"));
            clanBuffTemplate.setComment(resultSet.getString("comment"));
            CLAN_BUFF_TEMPLATES.add(clanBuffTemplate);
            CLANBUFF_HASH_MAP.put(clanBuffTemplate.getBuffId(), clanBuffTemplate);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `clan_market_house`");
        while (resultSet.next())
        {
            ClanHouseTemplate clanHouseTemplate = new ClanHouseTemplate();
            clanHouseTemplate.setLvl(resultSet.getInt("clanLvl"));
            clanHouseTemplate.setFundNeed(resultSet.getInt("fundNeed"));
            clanHouseTemplate.setGrowthPointNeed(resultSet.getInt("growthPoint"));
            clanHouseTemplate.setNeedClanLvl(resultSet.getInt("needClanLvl"));
            clanMarketHouseTemp.put(clanHouseTemplate.getLvl(), clanHouseTemplate);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `clan_skill_house`");
        while (resultSet.next())
        {
            ClanHouseTemplate clanHouseTemplate = new ClanHouseTemplate();
            clanHouseTemplate.setLvl(resultSet.getInt("clanLvl"));
            clanHouseTemplate.setFundNeed(resultSet.getInt("fundNeed"));
            clanHouseTemplate.setGrowthPointNeed(resultSet.getInt("growthPoint"));
            clanSkillHouseTemp.put(clanHouseTemplate.getLvl(), clanHouseTemplate);
        }
        resultSet.Close();
        */
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

    public static void loadMarket()
    {

        using (var conn = MYSQLManager.create())
        {
            var marketData = conn.QueryFirstOrDefault("SELECT *, UNIX_TIMESTAMP(TimeSave) * 1000 AS milliseconds FROM `market` ORDER BY `market`.`TimeSave` DESC");

            if (marketData != null)
            {
                MarketPlace.setKiosks(JsonConvert.DeserializeObject<Kiosk[]>(marketData.Data));
                conn.Execute("DELETE FROM `market` WHERE (UNIX_TIMESTAMP(TimeSave) * 1000) + 1000 * 60 * 60 * 24 * 31 < @TimeReigonNeedDetele",
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

    public static string GetElementDisplay(sbyte typeE)
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

    public static string GetClassDisplay(sbyte nclass)
    {
        switch (nclass)
        {
            case Assassin: return "Sát thủ";
            case Wizard: return "Pháp sư";
            case Fighter: return "Chiến binh";
            case Demon: return "Thiên binh";
            case Angel: return "Thiên sứ";
            case Archer: return "Thiên binh";
        }
        return string.Empty;
    }


    public static string GetElementDisplay(sbyte typeE, sbyte nClass)
    {
        return string.Concat(GetClassDisplay(nClass), " ", GetElementDisplay(typeE));
    }
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
}

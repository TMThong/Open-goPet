
using Gopet.Data.Collections;
using MySql.Data.MySqlClient;

public class GopetManager {

    /**
     * Chỉ số của quái từng cấp độ
     */
    public static HashMap<  int,   MobLvInfo> MOBLVLINFO_HASH_MAP = new();
    public static HashMap<  int,   MobLvInfo> MOBLVLINFO_CHALLENGE = new();

    public static ArrayList<  PetTemplate> PET_TEMPLATES = new();
    /**
     * Mẫu pet
     */
    public static HashMap<int, PetTemplate> PETTEMPLATE_HASH_MAP = new();

    public static HashMap<sbyte, ArrayList<  PetTemplate>> typePetTemplate = new();
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
    public const int GIFT_GOLD = 0;
    public const int GIFT_COIN = 1;
    public const int GIFT_ITEM = 2;
    public const int GIFT_ITEM_PERCENT = 3;
    public const int GIFT_ITEM_PERCENT_NO_DROP_MORE = 4;
    public const int GIFT_ITEM_MERGE_PET = 5;
    public const int GIFT_ITEM_MERGE_ITEM = 6;
    public const int GIFT_EXP = 7;
    public const int GIFT_ENERGY = 8;

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
    public static HashMap<  int,   PetSkill> PETSKILL_HASH_MAP = new();

    /**
     * Kỹ năng của pet (theo phái)
     */
    public static HashMap<  sbyte,   ArrayList<  PetSkill>> NCLASS_PETSKILL_HASH_MAP = new();

    /**
     * Kỹ năng của pet (tất cả)
     */
    public static ArrayList<  PetSkill> PET_SKILLS = new();

    /**
     * Kinh nghiệm của pet
     */
    public static HashMap<  int,   int> PetExp = new();

    public static HashMap<  int,   ArrayList<  DropItem>> dropItem = new();

    public static HashMap<  int,   TierItem> tierItem = new();

    public static HashMap<  int,   PetTier> petTier = new();

    public static HashMap<  int,   PetTattoTemplate> tattos = new();

    public static HashMap<  int,   BossTemplate> boss = new();

    public static HashMap<  int,   ArrayList<  ItemTemplate>> mergeItemPet = new();

    public static HashMap<  int,   ArrayList<  ItemTemplate>> mergeItemItem = new();

    public static ArrayList<  int> mapHasDropItemLvlRange = new();

    public static ArrayList<  PetTemplate> petEnable = new();

    public static HashMap<  int, String> itemAssetsIcon = new();

    public static ArrayList<  ShopArenaTemplate> SHOP_ARENA_TEMPLATE = new();

    public static HashMap<  int,   ClanTemplate> clanTemp = new();

    public static HashMap<  int,   TaskTemplate> taskTemplate = new();

    public static ArrayList<  TaskTemplate> taskTemplateList = new();

    public static HashMap<  int,   ArrayList<  TaskTemplate>> taskTemplateByType = new();

    public static HashMap<  int,   ArrayList<  TaskTemplate>> taskTemplateByNpcId = new();

    public static ArrayList<  ClanBuffTemplate> CLAN_BUFF_TEMPLATES = new();

    public static HashMap<  int,   ClanBuffTemplate> CLANBUFF_HASH_MAP = new();

    public static HashMap<  int,   ClanHouseTemplate> clanSkillHouseTemp = new();

    public static HashMap<  int,   ClanHouseTemplate> clanMarketHouseTemp = new();

    public static HashMap<  int,   ArrayList<  ShopClanTemplate>> shopClanByLvl = new();

    public static HashMap<  int,   int> tierItemHashMap = new();

    public static ArrayList<  ItemAttributeTemplate> ITEM_ATTRIBUTE_TEMPLATES = new();

    public static HashMap<  int,   ItemAttributeTemplate> ITEM_ATTRIBUTE_TEMPLATE_HASH_MAP = new();

    public static ArrayList<  ExchangeData> EXCHANGE_DATAS = new();

    public static ArrayList<  ItemTemplate> NonAdminItemList = new();

    public const RestUser DEFAUL_REST_USER = new RestUser("gopet", "384f9013c39a1503596493e36fe607e9cf69482c96fd7837201d2318630c0ee9322170932abcd193a2c4e48e7200cd164d860fd3555e67c95b5113d29910cb3b");

    /**
     * Id các map được dịch chuyển
     */
//    public const int[] TeleMapId = new int[]{11, 19, 24, 22, 27, 26, 28};
    public static int[] TeleMapId = new int[]{11, 19, 24, 22};
    /**
     * Giá nâng kỹ năng theo từng giai đoạn
     */
    public static int[] PriceUPSkill = new int[]{3000, 6000, 10000, 14000, 18000, 22000, 26000, 30000, 34000, 38000};

    /**
     * Tỷ lệ nâng kỵ năng theo từng giai đoạn
     */
    public static float[] PercentUpSkill = new float[]{90, 80, 70, 60, 50, 40, 30, 20, 10, 0};

    /**
     * Số lượt cần để hồi xong 1 kỹ năng
     */
    public const int MAX_SKILL_COOLDOWN = 5;

    /**
     * Mẫu của npc
     */
    public static HashMap<  int,   NpcTemplate> npcTemplate = new();

    /**
     * Id các pet trong danh sách nhận pet miễn phí
     */
    public static int[] petFreeIds = new int[]{1, 2, 3, 5, 6};

    /**
     * Map mẫu
     */
    public static HashMap<  int,   MapTemplate> mapTemplate = new();

    /**
     * Cửa hàng mẫu
     */
    public static HashMap<  sbyte,   ShopTemplate> shopTemplate = new();

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
    public static int[] ENCHANT_INFO = new int[]{3, 4, 5, 6, 7, 8, 9, 10, 11, 20};
    public static float[] PERCENT_ENCHANT = new float[]{90f, 80f, 70f, 60f, 50f, 30f, 20f, 5f, -10f, -20f};
    public static float[] DISPLAY_PERCENT_ENCHANT = new float[]{90f, 80f, 70f, 60f, 50f, 30f, 20f, 15f, 10f, 5f};
    public static float[] PERCENT_UP_SKILL = new float[]{60f, 50f, 40f, 30f, 20f, 10f, 0f, -10f, -20f, -30f, -40f};
    public static int[] PRICE_ENCHANT = new int[]{5000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000};
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
    public const long PRICE_REVIVAL_PET_FATER_PK = 5000;
    public const float BET_PRICE_PLAYER_CHALLENGE = 10f;
    public const int LVL_PET_PASSIVE_REQUIER_UP_TIER = 10;
    public const int SILVER_BAR_ID = 270;
    public const int GOLD_BAR_ID = 271;
    public const int BLOOD_GEM_ID = 273;
    public static int[] ID_BOSS_CHALLENGE = new int[]{11, 12, 13, 14, 15};

    public static int[] LVL_REQUIRE_PET_TATTO = new int[]{3, 5, 10, 15, 20, 25, 30, 35};
    public const int MOB_NEED_CAPTCHA = 125;
    public const long TIME_BOSS_DISPOINTED = 1000 * 60 * 10;
    public static float[] PERCENT_OF_ENCHANT_GEM = new float[]{70f, 65f, 60f, 55f, 50f, 40f, 30f, 20f, 10f, 2f};
    public const int PRICE_KEEP_GEM = 5000;
    public const int MAX_SLOT_SHOP_ARENA = 6;
    public const int DEFAULT_FREE_RESET_ARENA_SHOP = 2;
    public const int PRICE_RESET_SHOP_ARENA = 100;
    public const int MAX_RESET_SHOP_ARENA = 20;
    public const long TIME_UNEQUIP_GEM = 1000 * 60 * 60;
    public const int PRICE_UNEQUIP_GEM = 2500;
    public const long COIN_CREATE_CLAN = 200000;
    public const long GOLD_CREATE_CLAN = 20000;
    public const int CLAN_MAX_LVL = 10;
    public const int PRICE_SILVER_BAR_CHANGE_GIFT = 10;
    public static readonly sbyte[] LVL_CLAN_NEED_TO_ADD_SLOT_SKILL = new sbyte[]{3, 5, 7};
    public static readonly int[] PRICE_RENT_SKILL = new int[]{550, 100};
    public static readonly long[] PRICE_BET_CHALLENGE = new long[]{2000l, 10000l, 15000l};

    public static int[][] CHANGE_ITEM_DATA;
    public const int MAX_TIMES_SHOW_CAPTCHA = 5;

    public const int PERCENT_EXCHANGE_GOLD_TO_COIN = 20;

    static GopetManager() {
        shopTemplate.put(MenuController.SHOP_ARMOUR, new ShopTemplate(MenuController.SHOP_ARMOUR));
        shopTemplate.put(MenuController.SHOP_SKIN, new ShopTemplate(MenuController.SHOP_SKIN));
        shopTemplate.put(MenuController.SHOP_HAT, new ShopTemplate(MenuController.SHOP_HAT));
        shopTemplate.put(MenuController.SHOP_WEAPON, new ShopTemplate(MenuController.SHOP_WEAPON));
        shopTemplate.put(MenuController.SHOP_THUONG_NHAN, new ShopTemplate(MenuController.SHOP_THUONG_NHAN));
        shopTemplate.put(MenuController.SHOP_PET, new ShopTemplate(MenuController.SHOP_PET));
        shopTemplate.put(MenuController.SHOP_FOOD, new ShopTemplate(MenuController.SHOP_FOOD));
        shopTemplate.put(MenuController.SHOP_CLAN, new ShopTemplate(MenuController.SHOP_CLAN));
    }

    public static void readMobLvl(String cmd, HashMap<int, MobLvInfo> hashMap)   {
        ResultSet resultSet = MYSQLManager.jquery(cmd);
        while (resultSet.next()) {
            MobLvInfo mobLvInfo = new MobLvInfo();
            mobLvInfo.setLvl(resultSet.getInt("lvl"));
            mobLvInfo.setHp(resultSet.getInt("hp"));
            mobLvInfo.setStrength(resultSet.getInt("strength"));
            mobLvInfo.setExp(resultSet.getInt("exp"));
            hashMap.put(mobLvInfo.getLvl(), mobLvInfo);
        }
        resultSet.Close();
    }

    public static void init()   {
        ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `petexp`");
        while (resultSet.next()) {
            PetExp.put(resultSet.getInt("petLvl"), resultSet.getInt("exp"));
        }
        resultSet.Close();
        readMobLvl("SELECT * FROM `gopet_mob`", MOBLVLINFO_HASH_MAP);
        readMobLvl("SELECT * FROM `mob_challenge`", MOBLVLINFO_CHALLENGE);
        resultSet = MYSQLManager.jquery("SELECT * FROM `gopet_pet`");
        while (resultSet.next()) {
            PetTemplate petTemplate = new PetTemplate();
            petTemplate.setPetId(resultSet.getInt("petId"));
            petTemplate.setName(resultSet.getString("name"));
            petTemplate.setIcon(resultSet.getString("icon"));
            petTemplate.setFrameImg(resultSet.getString("frameImg"));
            petTemplate.setHp(resultSet.getInt("hp"));
            petTemplate.setMp(resultSet.getInt("mp"));
            petTemplate.setStr(resultSet.getInt("str"));
            petTemplate.setInt(resultSet.getInt("_int"));
            petTemplate.setAgi(resultSet.getInt("agi"));
            petTemplate.setType(resultSet.getsbyte("type"));
            petTemplate.setElement(resultSet.getsbyte("element"));
            petTemplate.setNclass(resultSet.getsbyte("nClass"));
            petTemplate.setEnable(resultSet.getsbyte("enable") == 1);
            if (petTemplate.isEnable()) {
                petEnable.add(petTemplate);
                if (!typePetTemplate.ContainsKey(petTemplate.getType())) {
                    typePetTemplate.put(petTemplate.getType(), new());
                }
                typePetTemplate.get(petTemplate.getType()).add(petTemplate);
            }

            PETTEMPLATE_HASH_MAP.put(petTemplate.getPetId(), petTemplate);
            PET_TEMPLATES.add(petTemplate);
        }
        resultSet.Close();
        MySqlConnection webMySqlConnection = MYSQLManager.createWebMySqlConnection();
        resultSet = MYSQLManager.jquery("SELECT * FROM `exchange`", webMySqlConnection);
        while (resultSet.next()) {
            ExchangeData exchangeData = new ExchangeData();
            exchangeData.setId(resultSet.getInt("id"));
            exchangeData.setGold(resultSet.getInt("gold"));
            exchangeData.setAmount(resultSet.getInt("amount"));
            EXCHANGE_DATAS.add(exchangeData);
            MenuController.EXCHANGE_ITEM_INFOS.add(new ExchangeItemInfo(exchangeData));
        }

        resultSet.Close();
        webMySqlConnection.Close();
        resultSet = MYSQLManager.jquery("SELECT * FROM `iteminfo`");
        while (resultSet.next()) {
            int ID = resultSet.getInt("ID");
            String name = resultSet.getString("name");
            bool isPercent = resultSet.getInt("isPercent") != 0;
            bool canFormat = name.Contains("%s");
            itemInfoName.put(ID, name);
            itemInfoIsPercent.put(ID, isPercent);
            itemInfoCanFormat.put(ID, canFormat);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `skill`");
        while (resultSet.next()) {
            PetSkill petSkill = new PetSkill();
            petSkill.skillID = resultSet.getInt("skillID");
            petSkill.name = resultSet.getString("name");
            petSkill.description = resultSet.getString("description");
            petSkill.nClass = resultSet.getsbyte("nClass");
            ResultSet skillLvResultSet = MYSQLManager.jquery(Utilities.Format("SELECT * FROM `skilllv` WHERE skillID = %s ORDER BY lv ASC;", petSkill.skillID));
            while (skillLvResultSet.next()) {
                PetSkillLv petSkillLv = new PetSkillLv();
                petSkillLv.lv = skillLvResultSet.getInt("lv");
                petSkillLv.mpLost = skillLvResultSet.getInt("mpLost");
                petSkillLv.skillInfo = (PetSkillInfo[]) JsonManager.LoadFromJson(skillLvResultSet.getString("skillInfo"), PetSkillInfo[].class);
                petSkill.skillLv.add(petSkillLv);
            }
            skillLvResultSet.Close();
            PETSKILL_HASH_MAP.put(petSkill.skillID, petSkill);
            if (!NCLASS_PETSKILL_HASH_MAP.ContainsKey(petSkill.nClass)) {
                NCLASS_PETSKILL_HASH_MAP.put(petSkill.nClass, new());
            }
            NCLASS_PETSKILL_HASH_MAP.get(petSkill.nClass).add(petSkill);
        }
        resultSet.Close();

        HashMap<int, ArrayList<MobLvlMap>> mobLvlMap_ = new();
        resultSet = MYSQLManager.jquery("SELECT * FROM `gopet_map_moblvl`");
        while (resultSet.next()) {
            MobLvlMap mobLvlMap = new MobLvlMap(resultSet.getInt("mapId"), resultSet.getInt("lvlFrom"), resultSet.getInt("lvlTo"), resultSet.getInt("petId"));
            if (!mobLvlMap_.ContainsKey(mobLvlMap.getMapId())) {
                mobLvlMap_.put(mobLvlMap.getMapId(), new());
            }
            mobLvlMap_.get(mobLvlMap.getMapId()).add(mobLvlMap);
        }
        resultSet.Close();
        for (Map.Entry<int, ArrayList<MobLvlMap>> entry : mobLvlMap_ ) {
            int key = entry.Key;
            ArrayList<MobLvlMap> val = entry.Value;
            MOBLVL_MAP.put(key, val.toArray(new MobLvlMap[0]));
        }
        HashMap<int, ArrayList<MobLocation>> mobLoc = new();
        resultSet = MYSQLManager.jquery("SELECT * FROM `gopet_mob_location`");
        while (resultSet.next()) {
            MobLocation mobLocation1 = new MobLocation(resultSet.getInt("mapId"), resultSet.getInt("x"), resultSet.getInt("y"));
            if (!mobLoc.ContainsKey(mobLocation1.getMapId())) {
                mobLoc.put(mobLocation1.getMapId(), new());
            }
            mobLoc.get(mobLocation1.getMapId()).add(mobLocation1);
        }
        resultSet.Close();

        for (Map.Entry<int, ArrayList<MobLocation>> entry : mobLoc ) {
            int key = entry.Key;
            ArrayList<MobLocation> val = entry.Value;
            mobLocation.put(key, val.toArray(new MobLocation[0]));
        }

        resultSet = MYSQLManager.jquery("SELECT * FROM `npc`");
        while (resultSet.next()) {
            NpcTemplate npcTemp = new NpcTemplate();
            npcTemp.setNpcId(resultSet.getInt("npcId"));
            npcTemp.setName(resultSet.getString("name"));
            npcTemp.setChat((String[]) JsonManager.LoadFromJson(resultSet.getString("chat"), String[].class));
            npcTemp.setOptionName((String[]) JsonManager.LoadFromJson(resultSet.getString("optionName"), String[].class));
            npcTemp.setOptionId((int[]) JsonManager.LoadFromJson(resultSet.getString("optionId"), int[].class));
            npcTemp.setX(resultSet.getInt("x"));
            npcTemp.setY(resultSet.getInt("y"));
            npcTemp.setImgPath(resultSet.getString("imgPath"));
            npcTemp.setType(resultSet.getsbyte("type"));
            npcTemp.setBounds((int[]) JsonManager.LoadFromJson(resultSet.getString("bounds"), int[].class));
            npcTemplate.put(npcTemp.getNpcId(), npcTemp);
        }
        resultSet.Close();
        resultSet = MYSQLManager.jquery("SELECT * FROM `map` WHERE `map`.`enable` = true;");
        while (resultSet.next()) {
            MapTemplate mapTemp = new MapTemplate();
            mapTemp.setMapId(resultSet.getInt("mapId"));
            mapTemp.setMapName(resultSet.getString("name"));
            mapTemp.setNpc((int[]) JsonManager.LoadFromJson(resultSet.getString("npc"), int[].class));
            int[] waypointX = (int[]) JsonManager.LoadFromJson(resultSet.getString("waypointX"), int[].class);
            int[] waypointY = (int[]) JsonManager.LoadFromJson(resultSet.getString("waypointY"), int[].class);
            String[] waypointName = (String[]) JsonManager.LoadFromJson(resultSet.getString("waypointName"), String[].class);
            Waypoint[] waypoints = new Waypoint[waypointName.Length];
            for (int i = 0; i < waypoints.Length; i++) {
                waypoints[i] = new Waypoint();
                waypoints[i].setName(waypointName[i]);
                waypoints[i].setX(waypointX[i]);
                waypoints[i].setY(waypointY[i]);
            }
            mapTemp.setWaypoints(waypoints);
            mapTemp.setBoss((int[]) JsonManager.LoadFromJson(resultSet.getString("boss"), int[].class));
            mapTemplate.put(mapTemp.getMapId(), mapTemp);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `tattoo`");
        while (resultSet.next()) {
            PetTattoTemplate petTattoTemplate = new PetTattoTemplate();
            petTattoTemplate.setTattooId(resultSet.getInt("tattooId"));
            petTattoTemplate.setName(resultSet.getString("name"));
            petTattoTemplate.setType(resultSet.getsbyte("type"));
            petTattoTemplate.setIconPath(resultSet.getString("iconPath"));
            petTattoTemplate.setAtk(resultSet.getInt("atk"));
            petTattoTemplate.setDef(resultSet.getInt("def"));
            petTattoTemplate.setHp(resultSet.getInt("hp"));
            petTattoTemplate.setMp(resultSet.getInt("mp"));
            petTattoTemplate.setPercent(resultSet.getFloat("percent"));
            tattos.put(petTattoTemplate.getTattooId(), petTattoTemplate);
        }
        resultSet.Close();

        int idAssets = 1;
        resultSet = MYSQLManager.jquery("SELECT * FROM `item`");
        while (resultSet.next()) {
            idAssets++;
            ItemTemplate itemTemp = new ItemTemplate();
            itemTemp.setItemId(resultSet.getInt("itemId"));
            itemTemp.setName(resultSet.getString("name"));
            itemTemp.setDescription(resultSet.getString("description"));
            itemTemp.setType(resultSet.getInt("type"));
            itemTemp.set_int(resultSet.getInt("_int"));
            itemTemp.setAgi(resultSet.getInt("agi"));
            itemTemp.setStr(resultSet.getInt("str"));
            itemTemp.setDef(resultSet.getInt("def"));
            itemTemp.setAtk(resultSet.getInt("atk"));
            itemTemp.setHp(resultSet.getInt("hp"));
            itemTemp.setMp(resultSet.getInt("mp"));
            itemTemp.setRequireAgi(resultSet.getInt("requireAgi"));
            itemTemp.setRequireInt(resultSet.getInt("requireInt"));
            itemTemp.setRequireStr(resultSet.getInt("requireStr"));
            itemTemp.setStackable(resultSet.getsbyte("isStackable") == 1);
            itemTemp.setCanTrade(resultSet.getsbyte("canTrade") == 1);
            itemTemp.setOption((int[]) JsonManager.LoadFromJson(resultSet.getString("itemOption"), int[].class));
            itemTemp.setOptionValue((int[]) JsonManager.LoadFromJson(resultSet.getString("itemOptionValue"), int[].class));
            itemTemp.setGender(resultSet.getsbyte("gender"));
            itemTemp.setFrameImgPath(resultSet.getString("frameImgPath"));
            itemTemp.setIconPath(resultSet.getString("iconPath"));
            itemTemp.setOnSky(resultSet.getsbyte("isOnSky") == 1);
            itemTemp.setNClass(resultSet.getsbyte("petNClass"));
            itemTemp.setElement(resultSet.getsbyte("element"));
            itemTemp.setTypeTier(resultSet.getInt("tierType"));
            itemTemp.setIconId(idAssets);
            itemAssetsIcon.put(idAssets, itemTemp.getIconPath());
            BigDecimal bigDecimal = resultSet.getBigDecimal("expire");
            if (!resultSet.wasNull()) {
                itemTemp.setExpire(bigDecimal.longValue());
            }
            itemTemplate.put(itemTemp.getItemId(), itemTemp);
            if (itemTemp.getType() == ITEM_PART_PET) {
                int[] optionValue = itemTemp.getOptionValue();
                if (optionValue.Length > 1) {
                    PetTemplate petTemplate = PETTEMPLATE_HASH_MAP.get(optionValue[0]);
                    if (petTemplate != null) {
                        int typePet = petTemplate.getType();
                        if (!mergeItemPet.ContainsKey(typePet)) {
                            mergeItemPet.put(typePet, new());
                        }
                        mergeItemPet.get(typePet).add(itemTemp);
                    }
                }
            } else if (itemTemp.getType() == ITEM_PART_ITEM) {
                int[] optionValue = itemTemp.getOptionValue();
                if (optionValue.Length > 1) {
                    int typePet = itemTemp.getTypeTier();
                    if (!mergeItemItem.ContainsKey(typePet)) {
                        mergeItemItem.put(typePet, new());
                    }
                    mergeItemItem.get(typePet).add(itemTemp);
                }
            }

            if (itemTemp.getType() != ITEM_ADMIN) {
                NonAdminItemList.add(itemTemp);
            }
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `shop`");
        while (resultSet.next()) {
            sbyte shopId = resultSet.getsbyte("ShopId");
            ShopTemplateItem shopTemplate1 = new ShopTemplateItem();
            shopTemplate1.setShopId(resultSet.getInt("shopId"));
            shopTemplate1.setItemTempalteId(resultSet.getInt("itemTemTempleId"));
            shopTemplate1.setCount(resultSet.getInt("count"));
            shopTemplate1.setMoneyType((sbyte[]) JsonManager.LoadFromJson(resultSet.getString("moneyType"), sbyte[].class));
            shopTemplate1.setPrice((int[]) JsonManager.LoadFromJson(resultSet.getString("price"), int[].class));
            shopTemplate1.setInventoryType(resultSet.getsbyte("inventoryType"));
            shopTemplate1.setClanLvl(resultSet.getInt("clanLvl"));
            shopTemplate1.setPerCount(resultSet.getInt("perCount"));
            shopTemplate1.setSellItem(resultSet.getbool("isSellItem"));
            shopTemplate1.setPetId(resultSet.getInt("petId"));
            if (shopTemplate.ContainsKey(shopId)) {
                shopTemplate.get(shopId).getShopTemplateItems().add(shopTemplate1);
            } else {
                //throw new UnsupportedOperationException(" khong ho tro loai shop " + shopId);
            }

        }
        resultSet = MYSQLManager.jquery("SELECT * FROM `shop_clan`");
        while (resultSet.next()) {
            ShopClanTemplate shopTemplate1 = new ShopClanTemplate();
            shopTemplate1.setId(resultSet.getInt("Id"));
            shopTemplate1.setNeedShopClanLvl(resultSet.getInt("needShopClanLvl"));
            shopTemplate1.setComment(resultSet.getString("comment"));
            shopTemplate1.setPercent(resultSet.getFloat("percent"));
            shopTemplate1.setOption((int[][]) JsonManager.LoadFromJson(resultSet.getString("optionValue"), int[][].class));
            if (!shopClanByLvl.ContainsKey(shopTemplate1.getNeedShopClanLvl())) {
                shopClanByLvl.put(shopTemplate1.getNeedShopClanLvl(), new());
            }
            shopClanByLvl.get(shopTemplate1.getNeedShopClanLvl()).add(shopTemplate1);
        }
        resultSet.Close();
        resultSet = MYSQLManager.jquery("SELECT * FROM `drop_item`");
        while (resultSet.next()) {
            DropItem dropItem1 = new DropItem();
            dropItem1.setDropId(resultSet.getInt("dropId"));
            dropItem1.setMapId(resultSet.getInt("mapId"));
            dropItem1.setItemTemplateId(resultSet.getInt("itemTemplateId"));
            dropItem1.setPercent(resultSet.getFloat("percent"));
            dropItem1.setCount(resultSet.getInt("count"));
            String lvlRange = resultSet.getString("lvlRange");
            if (!resultSet.wasNull()) {
                dropItem1.setLvlRange((int[]) JsonManager.LoadFromJson(lvlRange, int[].class));
                if (!mapHasDropItemLvlRange.Contains(dropItem1.getMapId())) {
                    mapHasDropItemLvlRange.add(dropItem1.getMapId());
                }
            }
            if (!dropItem.ContainsKey(dropItem1.getMapId())) {
                dropItem.put(dropItem1.getMapId(), new());
            }

            if (dropItem1.getPercent() < 0f) {
                continue;
            }
            dropItem.get(dropItem1.getMapId()).add(dropItem1);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `attributes` WHERE `enable` = TRUE;");
        while (resultSet.next()) {
            ItemAttributeTemplate attributeTemplate = new ItemAttributeTemplate();
            attributeTemplate.setAttrId(resultSet.getInt("attrId"));
            attributeTemplate.setName(resultSet.getString("name"));
            attributeTemplate.setListItemId((int[]) JsonManager.LoadFromJson(resultSet.getString("listItemId"), int[].class));
            attributeTemplate.setBuff((int[][]) JsonManager.LoadFromJson(resultSet.getString("buff"), int[][].class));
            ITEM_ATTRIBUTE_TEMPLATES.add(attributeTemplate);
            ITEM_ATTRIBUTE_TEMPLATE_HASH_MAP.put(attributeTemplate.getAttrId(), attributeTemplate);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `tier_item`");
        while (resultSet.next()) {
            TierItem tierItem1 = new TierItem();
            tierItem1.setTierId(resultSet.getInt("tierId"));
            tierItem1.setItemTemplateIdTier1(resultSet.getInt("itemTemplateIdTier1"));
            tierItem1.setItemTemplateIdTier2(resultSet.getInt("itemTemplateIdTier2"));
            tierItem1.setPercent(resultSet.getFloat("percent"));
            tierItem.put(tierItem1.getItemTemplateIdTier1(), tierItem1);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `pet_tier`");
        while (resultSet.next()) {
            PetTier petTier1 = new PetTier();
            petTier1.setTierId(resultSet.getInt("tierId"));
            petTier1.setPetTemplateId1(resultSet.getInt("petTemplateId1"));
            petTier1.setPetTemplateId2(resultSet.getInt("petTemplateId2"));
            petTier1.setPetTemplateIdNeed(resultSet.getInt("petTemplateIdNeed"));
            petTier.put(petTier1.getPetTemplateId1(), petTier1);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `boss`");
        while (resultSet.next()) {
            BossTemplate bossTemplate = new BossTemplate();
            bossTemplate.setBossId(resultSet.getInt("bossId"));
            bossTemplate.setPetTemplate(PETTEMPLATE_HASH_MAP.get(resultSet.getInt("petTemplateId")));
            bossTemplate.setName(resultSet.getString("name"));
            bossTemplate.setLvl(resultSet.getInt("lvl"));
            bossTemplate.setAtk(resultSet.getInt("atk"));
            bossTemplate.setDef(resultSet.getInt("def"));
            bossTemplate.setHp(resultSet.getInt("hp"));
            bossTemplate.setTypeBoss(resultSet.getsbyte("typeBoss"));
            bossTemplate.setGift((int[][]) JsonManager.LoadFromJson(resultSet.getString("gift"), int[][].class));
            boss.put(bossTemplate.getBossId(), bossTemplate);
            if (bossTemplate.getPetTemplate() == null) {
                resultSet.Close();
                throw new NullReferenceException("Bị rổng do id pet template không tồn tại");
            }
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `shoparena` WHERE `shoparena`.`enable` = 1");
        while (resultSet.next()) {
            ShopArenaTemplate shopArenaTemplate = new ShopArenaTemplate();
            shopArenaTemplate.setId(resultSet.getInt("Id"));
            shopArenaTemplate.setOption((int[][]) JsonManager.LoadFromJson(resultSet.getString("optionValue"), int[][].class));
            shopArenaTemplate.setComment(resultSet.getString("comment"));
            shopArenaTemplate.setPercent(resultSet.getFloat("percent"));
            SHOP_ARENA_TEMPLATE.add(shopArenaTemplate);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `clan_template` ORDER BY `clan_template`.`clanLvl` ASC");
        while (resultSet.next()) {
            ClanTemplate clanTemplate = new ClanTemplate();
            clanTemplate.setLvl(resultSet.getInt("clanLvl"));
            clanTemplate.setMaxMember(resultSet.getInt("maxMember"));
            clanTemplate.setTiemnangPoint(resultSet.getInt("tiemnangPoint"));
            clanTemplate.setFundNeed(resultSet.getBigDecimal("fundNeed").longValue());
            clanTemplate.setGrowthPointNeed(resultSet.getBigDecimal("growthPointNeed").longValue());
            clanTemplate.setPermission((int[]) JsonManager.LoadFromJson(resultSet.getString("permission"), int[].class));
            clanTemp.put(clanTemplate.getLvl(), clanTemplate);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `task`");
        while (resultSet.next()) {
            TaskTemplate taskTemp = new TaskTemplate();
            taskTemp.setTaskId(resultSet.getInt("taskId"));
            taskTemp.setType(resultSet.getInt("type"));
            taskTemp.setName(resultSet.getString("name"));
            taskTemp.setDescription(resultSet.getString("description"));
            taskTemp.setGuide(resultSet.getString("guide"));
            taskTemp.setTask((int[][]) JsonManager.LoadFromJson(resultSet.getString("task"), int[][].class));
            taskTemp.setGift((int[][]) JsonManager.LoadFromJson(resultSet.getString("gift"), int[][].class));
            taskTemp.setFromNpc(resultSet.getInt("fromNPC"));
            taskTemp.setTaskNeed((int[]) JsonManager.LoadFromJson(resultSet.getString("taskNeed"), int[].class));
            taskTemplate.put(taskTemp.getTaskId(), taskTemp);
            taskTemplateList.add(taskTemp);
            if (!taskTemplateByType.ContainsKey(taskTemp.getType())) {
                taskTemplateByType.put(taskTemp.getType(), new());
            }
            taskTemplateByType.get(taskTemp.getType()).add(taskTemp);

            if (!taskTemplateByNpcId.ContainsKey(taskTemp.getFromNpc())) {
                taskTemplateByNpcId.put(taskTemp.getFromNpc(), new());
            }

            taskTemplateByNpcId.get(taskTemp.getFromNpc()).add(taskTemp);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `clan_buff_template`");
        while (resultSet.next()) {
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
        while (resultSet.next()) {
            ClanHouseTemplate clanHouseTemplate = new ClanHouseTemplate();
            clanHouseTemplate.setLvl(resultSet.getInt("lvl"));
            clanHouseTemplate.setFundNeed(resultSet.getInt("fundNeed"));
            clanHouseTemplate.setGrowthPointNeed(resultSet.getInt("growthPoint"));
            clanHouseTemplate.setNeedClanLvl(resultSet.getInt("needClanLvl"));
            clanMarketHouseTemp.put(clanHouseTemplate.getLvl(), clanHouseTemplate);
        }
        resultSet.Close();

        resultSet = MYSQLManager.jquery("SELECT * FROM `clan_skill_house`");
        while (resultSet.next()) {
            ClanHouseTemplate clanHouseTemplate = new ClanHouseTemplate();
            clanHouseTemplate.setLvl(resultSet.getInt("lvl"));
            clanHouseTemplate.setFundNeed(resultSet.getInt("fundNeed"));
            clanHouseTemplate.setGrowthPointNeed(resultSet.getInt("growthPoint"));
            clanSkillHouseTemp.put(clanHouseTemplate.getLvl(), clanHouseTemplate);
        }
        resultSet.Close();

        CHANGE_ITEM_DATA = (int[][]) JsonManager.LoadFromJson("[[4,74,5000,1],[4,71,5500,1],[4,75,4500,1],[4,76,2000,1],[4,77,1000,1],[5,1,5000,1]]", int[][].class);

        for (Map.Entry<int, TierItem> entry : tierItem ) {

            TierItem val = entry.Value;

            if (tierItemHashMap.ContainsKey(val.getItemTemplateIdTier1()) || tierItemHashMap.ContainsKey(val.getItemTemplateIdTier2())) {
                continue;
            }

            ArrayList<int> map = findListTierId(val);

            tierItemHashMap.put(val.getItemTemplateIdTier1(), 1);
            for (int i = 0; i < map.Count; i++) {
                int get = map.get(i);
                tierItemHashMap.put(get, i + 2);
            }
        }

    }

    public static ArrayList<int> findListTierId(TierItem tInfo) {
        ArrayList<int> list = new();
        list.add(tInfo.getItemTemplateIdTier2());

        for (Map.Entry<int, TierItem> entry : tierItem ) {

            TierItem val = entry.Value;

            if (val.getItemTemplateIdTier1() == tInfo.getItemTemplateIdTier2()) {
                list.AddRange(findListTierId(val));
            }
        }

        return list;
    }

    public static void loadMarket()   {
        ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `market`");
        while (resultSet.next()) {
            sbyte typeKiosk = resultSet.getsbyte("type");
            String sellItem = resultSet.getString("sellItem");

            Type arrayType = new TypeToken< CopyOnWriteArrayList<SellItem>>() {
            }.getType();

            CopyOnWriteArrayList<SellItem> sellItems = (CopyOnWriteArrayList<SellItem>) JsonManager.LoadFromJson(sellItem, arrayType);

            Kiosk kiosk = MarketPlace.getKiosk(typeKiosk);
            if (kiosk != null) {
                kiosk.setKioskItem(sellItems);
            }
        }
        resultSet.Close();
        MYSQLManager.updateSql("DELETE FROM `market`");
    }

    public static void saveMarket()   {
        MySqlConnection MySqlConnection = MYSQLManager.create();
        try {
            for (Kiosk kiosk : MarketPlace.kiosks) {
                MYSQLManager.updateSql(Utilities.Format("INSERT INTO `market`(`type`,  `sellItem`) VALUES (%s,'%s')", kiosk.getKioskType(), JsonManager.ToJson(kiosk.kioskItems)), MySqlConnection);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        MySqlConnection.Close();
    }
}

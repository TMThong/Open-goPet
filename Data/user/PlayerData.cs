
using Gopet.Data.Collections;
using Gopet.Data.user;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

public class PlayerData
{
    public sbyte gender = 3;
    public String name;
    public long gold, coin, spendGold;
    public int charID, user_id;
    public ArrayList<int> friends = new ArrayList<int>();
    public ArrayList<int> favouriteList = new ArrayList<int>();
    public HashMap<sbyte, CopyOnWriteArrayList<Item>> items = new();
    public CopyOnWriteArrayList<Pet> pets = new();
    public CopyOnWriteArrayList<int> tasking = new();
    public CopyOnWriteArrayList<TaskData> task = new();
    public CopyOnWriteArrayList<int> wasTask = new();
    public Pet petSelected;
    public bool isFirstFree = false;
    public int x, y;
    public sbyte speed = 4;
    public sbyte faceDir = 3;
    public sbyte waypointIndex = -1;
    public long deltaTimeQuestion = Utilities.CurrentTimeMillis;
    public sbyte questIndex = 1;
    public DateTime loginDateTime;
    public int star = 0;
    public Item skinItem;
    public Item wingItem;
    public bool isOnSky = false;
    public BuffExp buffExp = new BuffExp();
    public int pkPoint = 0;
    public DateTime pkPointTime;
    public GopetCaptcha captcha;
    public bool isAdmin = false;
    public ShopArena shopArena;
    public int clanId;
    public String avatarPath;

    public PlayerData()
    {
        x = 24 * 4;
        y = 24 * 4;
    }

    public static PlayerData read(ResultSet result)
    {
        PlayerData playerData = new PlayerData();
        playerData.user_id = result.getInt("user_id");
        playerData.charID = result.getInt("ID");
        playerData.gender = (sbyte)result.getInt("gender");
        playerData.name = result.getString("name");
        playerData.loginDateTime = result.getDateTime("loginDateTime");
        playerData.coin = result.getBigDecimal("coin").longValue();
        playerData.gold = result.getBigDecimal("gold").longValue();
        playerData.star = result.getInt("star");
        playerData.isOnSky = result.getsbyte("isOnSky") == 1;
        playerData.pkPoint = result.getInt("pkPoint");
        playerData.pkPointTime = result.getDateTime("pkPointTime");
        playerData.isAdmin = result.getsbyte("isAdmin") == 1;
        playerData.clanId = result.getInt("clanId");
        playerData.avatarPath = result.getString("avatarPath");
        playerData.spendGold = result.getBigDecimal("spendGold").longValue();
        String friendJson = result.getString("friends");
        if (!string.IsNullOrEmpty(friendJson))
        {
            playerData.friends = JsonConvert.DeserializeObject<ArrayList<int>>(friendJson);
        }

        String items = result.getString("items");
        if (!string.IsNullOrEmpty(items))
        {

            playerData.items = JsonConvert.DeserializeObject<HashMap<sbyte, CopyOnWriteArrayList<Item>>>(items);
        }

        String favouriteList = result.getString("favouriteList");
        if (!string.IsNullOrEmpty(favouriteList))
        {

            playerData.favouriteList = JsonConvert.DeserializeObject<ArrayList<int>>(items);
        }

        playerData.isFirstFree = result.getsbyte("isFirstFree") == 1;

        String petsString = result.getString("pets");
        if (!string.IsNullOrEmpty(petsString))
        {
            playerData.pets = JsonConvert.DeserializeObject<CopyOnWriteArrayList<Pet>>(petsString);
        }
        String petSelectedString = result.getString("petSelected");
        if (!string.IsNullOrEmpty(petSelectedString))
        {
            playerData.petSelected = JsonConvert.DeserializeObject<Pet>(petSelectedString);
        }
        String skinString = result.getString("skin");
        if (!string.IsNullOrEmpty(skinString))
        {

            playerData.skinItem = JsonConvert.DeserializeObject<Item>(skinString);
        }
        String wingString = result.getString("wing");
        if (!string.IsNullOrEmpty(wingString))
        {

            playerData.wingItem = JsonConvert.DeserializeObject<Item>(wingString);
        }

        String buffExpStr = result.getString("buffExp");
        if (!string.IsNullOrEmpty(buffExpStr))
        {
            playerData.buffExp = JsonConvert.DeserializeObject<BuffExp>(buffExpStr);
            playerData.buffExp.loadCurrentTime();
        }

        String captchaStr = result.getString("captcha");
        if (!string.IsNullOrEmpty(captchaStr))
        {
            playerData.captcha = JsonConvert.DeserializeObject<GopetCaptcha>(captchaStr);
        }

        String shopArenaStr = result.getString("shopArena");
        if (!string.IsNullOrEmpty(shopArenaStr))
        {
            playerData.shopArena = JsonConvert.DeserializeObject<ShopArena>(shopArenaStr);
        }

        String taskStr = result.getString("task");
        if (!string.IsNullOrEmpty(taskStr))
        {

            playerData.task = JsonConvert.DeserializeObject<CopyOnWriteArrayList<TaskData>>(taskStr);
        }

        String taskingStr = result.getString("tasking");
        if (!string.IsNullOrEmpty(taskingStr))
        {

            playerData.tasking = JsonConvert.DeserializeObject<CopyOnWriteArrayList<int>>(taskingStr);
        }

        String wasTaskStr = result.getString("wasTask");
        if (!string.IsNullOrEmpty(wasTaskStr))
        {

            playerData.wasTask = JsonConvert.DeserializeObject<CopyOnWriteArrayList<int>>(wasTaskStr);
        }
        return playerData;
    }

    public static void create(int user_id, String name, sbyte gender)
    {
        MySqlConnection conn = MYSQLManager.create();
        MYSQLManager.updateSql(
                Utilities.Format(
                        "INSERT INTO `player` (`ID`, `user_id`, `name`, `gender` , `items`) VALUES (NULL, '%s', '%s', '%s' , NULL);",
                        user_id, name, gender), conn);
        conn.Close();
    }

    public void save()
    {
        MySqlConnection conn = MYSQLManager.create();
        MYSQLManager.updateSql(Utilities.Format("upDateTime player set  " + getGopetSQLString() + "   coin = %s , gold = %s  ,  friends = '%s'   ,    items = '%s',   favouriteList = '%s'  where ID = "
                + charID,
                coin,
                gold,
                JsonManager.ToJson(friends),
                JsonManager.ToJson(items),
                JsonManager.ToJson(favouriteList)
        ), conn);
        conn.Close();
    }

    public String getGopetSQLString()
    {
        String[] lines = new String[]{
            "pets = '" + JsonManager.ToJson(pets) + "'",
            "petSelected = '" + JsonManager.ToJson(petSelected) + "'",
            "isFirstFree = " + (isFirstFree ? "1" : "0"),
            "loginDateTime = '" + Utilities.ToDateString(loginDateTime) + "'",
            "star = " + star,
            "skin = '" + JsonManager.ToJson(skinItem) + "'",
            "wing = '" + JsonManager.ToJson(wingItem) + "'",
            "isOnSky = " + (isOnSky ? 1 : 0),
            "buffExp = '" + JsonManager.ToJson(buffExp) + "'",
            "pkPoint = " + pkPoint,
            "pkPointTime = '" + Utilities.ToDateString(pkPointTime) + "'",
            "captcha ='" + JsonManager.ToJson(captcha) + "'",
            "shopArena = '" + JsonManager.ToJson(shopArena) + "'",
            "clanId = " + clanId,
            "task = '" + JsonManager.ToJson(task) + "'",
            "tasking ='" + JsonManager.ToJson(tasking) + "'",
            "avatarPath = '" + avatarPath + "'",
            "wasTask = '" + JsonManager.ToJson(wasTask) + "'",
            "spendGold = " + spendGold
        };
        String str = String.Join(",", lines) + " ,";
        return str;
    }


    public CopyOnWriteArrayList<Item> getInventoryOrCreate(sbyte type)
    {
        if (items.ContainsKey(type))
        {
            return items.get(type);
        }
        else
        {
            CopyOnWriteArrayList<Item> list = new();
            items.put(type, list);
            return list;
        }
    }

    public void addItem(sbyte type, Item item)
    {
        CopyOnWriteArrayList<Item> list = getInventoryOrCreate(type);
        list.add(item);
        while (true)
        {
            item.itemId = Utilities.nextInt(10, int.MaxValue - 2);
            bool flag = true;
            foreach (Item item1 in getInventoryOrCreate(type))
            {
                if (item1 != item)
                {
                    if (item1.itemId == item.itemId)
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                break;
            }
        }
        list.Sort(new InventoryItemComparer());
    }

    public void removeItem(sbyte type, Item item)
    {
        getInventoryOrCreate(type).remove(item);
    }

    public void addPet(Pet pet, Player player)
    {
        pets.add(pet);
        bool flagId = false;
        while (true)
        {
            if (pet.petId > 0 && !flagId)
            {
                flagId = true;
            }
            else
            {
                pet.petId = Utilities.nextInt(10, int.MaxValue - 2);
            }
            bool flag = true;
            foreach (Pet item1 in pets)
            {
                if (item1 != pet)
                {
                    if (item1.petId == pet.petId)
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                break;
            }
        }
        pets.Sort(new InventoryPetComparer());
    }
}

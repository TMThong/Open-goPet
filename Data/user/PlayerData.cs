
using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.User;
using Gopet.Util;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

public class PlayerData
{
    public sbyte gender { get; set; } = 3;
    public String name { get; set; }
    public long gold { get; set; }
    public long coin { get; set; }
    public long spendGold { get; set; }
    public int ID { get; set; }

    public int user_id { get; set; }
    public ArrayList<int> friends { get; set; } = new ArrayList<int>();
    public ArrayList<int> favouriteList { get; set; } = new ArrayList<int>();
    public HashMap<sbyte, CopyOnWriteArrayList<Item>> items { get; set; } = new();
    public CopyOnWriteArrayList<Pet> pets { get; set; } = new();
    public CopyOnWriteArrayList<int> tasking { get; set; } = new();
    public CopyOnWriteArrayList<TaskData> task { get; set; } = new();
    public CopyOnWriteArrayList<int> wasTask { get; set; } = new();
    public Pet petSelected { get; set; }
    public bool isFirstFree { get; set; } = false;
    public int x, y;
    public sbyte speed = 4;
    public sbyte faceDir = 3;
    public sbyte waypointIndex = -1;
    public long deltaTimeQuestion { get; set; } = Utilities.CurrentTimeMillis;
    public sbyte questIndex { get; set; } = 1;
    public DateTime loginDate { get; set; }
    public int star { get; set; } = 0;
    public Item skin { get; set; }
    public Item wing { get; set; }
    public bool isOnSky { get; set; } = false;
    public BuffExp? buffExp { get; set; } = new BuffExp();
    public int pkPoint { get; set; } = 0;
    public DateTime pkPointTime { get; set; }
    public GopetCaptcha captcha { get; set; }
    public bool isAdmin { get; set; } = false;
    public ShopArena shopArena { get; set; }
    public int clanId { get; set; }
    public String avatarPath { get; set; }

    public int AccumulatedPoint { get; set; }

    public Dictionary<int, int> numUseEnergy { get; set; } = new();

    public PlayerData()
    {
        x = 24 * 4;
        y = 24 * 4;
    }


    public static void create(int user_id, String name, sbyte gender)
    {
        using (MySqlConnection conn = MYSQLManager.create())
        {
            conn.Execute("INSERT INTO `player` (`ID`, `user_id`, `name`, `gender` , `items`) VALUES (NULL, @user_id, @name, @gender , NULL);", new
            {
                user_id,
                name,
                gender
            });
        }
    }

    public void save()
    {
        saveStatic(this);
    }

    public static void saveStatic(PlayerData playerData)
    {
        using (var conn = MYSQLManager.create())
        {
            conn.Execute(@"Update `player` SET pets = @pets,
                            petSelected = @petSelected,
                            isFirstFree = @isFirstFree,
                            loginDate = @loginDate,
                            star = @star,
                            skin = @skin,
                            wing = @wing,
                            isOnSky = @isOnSky,  
                            buffExp = @buffExp,
                            pkPoint = @pkPoint, 
                            pkPointTime = @pkPointTime,
                            captcha = @captcha, 
                            shopArena = @shopArena, 
                            clanId = @clanId,
                            task = @task,
                            tasking = @tasking,
                            avatarPath = @avatarPath,
                            wasTask = @wasTask,
                            spendGold = @spendGold,
                            coin = @coin,
                            gold = @gold,
                            friends = @friends,
                            items = @items ,
                            favouriteList = @favouriteList,
                            numUseEnergy = @numUseEnergy,
                            AccumulatedPoint = @AccumulatedPoint
                            WHERE ID = @ID", playerData);
        }
    }


    public CopyOnWriteArrayList<Item> this[sbyte type]
    {
        get
        {
            return getInventoryOrCreate(type);
        }
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

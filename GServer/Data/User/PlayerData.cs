
using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.user;
using Gopet.Data.User;
using Gopet.Util;
using MySqlConnector;
using Newtonsoft.Json;

public class PlayerData
{
    public sbyte gender { get; set; } = 3;
    public string name { get; set; }
    public long gold { get; set; }
    public long coin { get; set; }
    public long lua { get; set; }
    public long spendGold { get; set; }
    public int ID { get; set; }
    public int user_id { get; set; }
    public JArrayList<int> favouriteList { get; set; } = new JArrayList<int>();
    public HashMap<sbyte, CopyOnWriteArrayList<Item>> items { get; set; } = new();
    public CopyOnWriteArrayList<Pet> pets { get; set; } = new();
    public CopyOnWriteArrayList<int> tasking { get; set; } = new();
    public CopyOnWriteArrayList<int> ClanTasked { get; set; } = new();
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
    public bool isOnSky { get; private set; } = false;
    public BuffExp? buffExp { get; set; } = new BuffExp();
    public int pkPoint { get; set; } = 0;
    public DateTime pkPointTime { get; set; }
    public GopetCaptcha captcha { get; set; }
    public bool isAdmin { get; set; } = false;
    public ShopArena shopArena { get; set; }
    public int clanId { get; set; }
    public string avatarPath { get; set; }

    public int AccumulatedPoint { get; set; }

    public Pet PetDefLeague { get; set; }

    public int EventPoint { get; set; }
    public int NumOfUseKiteNormal { get; set; }
    public int NumOfUseKiteVip { get; set; }

    public Dictionary<int, int> numUseEnergy { get; set; } = new();

    public CopyOnWriteArrayList<Achievement> achievements { get; set; } = new();

    public CopyOnWriteArrayList<Letter> letters { get; set; } = new();

    public int CurrentAchievementId { get; set; } = -1;

    public CopyOnWriteArrayList<int> RequestAddFriends { get; set; } = new();
    public CopyOnWriteArrayList<int> BlockFriendLists { get; set; } = new();
    public CopyOnWriteArrayList<int> ListFriends { get; set; } = new();

    public Dictionary<int, DateTime> LettersSendTime { get; set; } = new();

    public DateTime LastTimeOnline { get; set; }

    public CopyOnWriteArrayList<int> MoneyDisplays { get; set; } = new();
    public Dictionary<DateTime, Item> TrashItemBackup { get; set; } = new();

    public DateTime TimeDropCoin { get; set; } = DateTime.Now;

    public bool IsMergeServer { get; set; } = false;
    /// <summary>
    /// Trường thuộc sự kiện 20/11 2024
    /// Trường này để ghi lại số lượng hoa tặng dùng vàng
    /// </summary>
    public int NumGiveFlowerGold { get; set; } = 0;
   
    /// <summary>
    /// Trường thuộc sự kiện 20/11 2024
    /// Trường này để ghi lại số lượng hoa tặng dùng ngọc
    /// </summary>
    public int NumGiveFlowerGem { get; set; } = 0;
    /// <summary>
    /// Trường thuộc sự kiện 20/11 2024
    /// rường này để ghi lại chỉ mục của cột mốc quà sự kiện
    /// </summary>
    public byte IndexMilistoneTeacherEvent { get; set; } = 0;
    /// <summary>
    /// Trường thuộc sự kiện 20/11 2024
    /// Để ghi lại điểm sự kiện
    /// </summary>
    public int FlowerGold { get; set; } = -1;
    /// <summary>
    /// Trường thuộc sự kiện 20/11 2024
    /// Để ghi lại điểm sự kiện
    /// </summary>
    public int FlowerCoin { get; set; } = -1;
    /// <summary>
    /// Trường thuộc sự kiện Noel 2024
    /// Để ghi lại thời gian ngày hôm nay đã nhận quà Noel chưa
    /// </summary>
    public DateTime DailyNoelTime { get; set; } = DateTime.MinValue;
    /// <summary>
    /// Trường thuộc sự kiện Noel 2024
    /// Để ghi lại chỉ mục của quà ngày Noel
    /// </summary>
    public byte DailyNoelIndex { get; set; } = 0;
    /// <summary>
    /// Trường thuộc sự kiện sinh nhật trò chơi
    /// Để ghi tổng số lần ăn bánh chưng
    /// </summary>
    public int NumEatSquareStickyRice { get; set; } = 0;
    /// <summary>
    /// Trường thuộc sự kiện sinh nhật trò chơi
    /// Để ghi số điểm nhận được từ việc ăn bánh chưng
    /// </summary>
    public int NumEatSquareStickyRiceCoin { get; set; } = 0;
    /// <summary>
    /// Trường thuộc sự kiện sinh nhật trò chơi
    /// Để ghi tổng số lần ăn bánh tét
    /// </summary>
    public int NumEatCylindricalStickyRice { get; set; } = 0;
    /// <summary>
    /// Trường thuộc sự kiện sinh nhật trò chơi
    /// Để ghi số điểm nhận được từ việc ăn bánh tét
    /// </summary>
    public int NumEatCylindricalStickyRiceCoin { get; set; } = 0;
    /// <summary>
    /// Trường thuộc sự kiện sinh nhật trò chơi
    /// Để ghi chỉ mục của cột mốc quà sự kiện
    /// </summary>
    public sbyte IndexMilistoneBirthdayEvent { get; set; } = -1;
    /// <summary>
    /// Trường thuộc sự kiện sinh nhật trò chơi
    /// </summary>
    public int NumUseGiftBox2025 { get; set; } = 0;
    public PlayerData()
    {
        x = 24 * 4;
        y = 24 * 4;
    }

    /// <summary>
    /// Lấy thông tin người chơi
    /// </summary>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    /// <param name="gender"></param>
    public static void create(int user_id, string name, sbyte gender)
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
    /// <summary>
    /// Lưu thông tin người chơi
    /// </summary>
    public void save()
    {
        saveStatic(this);
    }
    /// <summary>
    /// Lưu thông tin người chơi
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="conn"></param>
    public static void saveStatic(PlayerData playerData, MySqlConnection conn)
    {
        playerData.LastTimeOnline = DateTime.Now;
        conn.Execute(@"     Update `player` SET 
                            pets = @pets,
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
                            items = @items,
                            favouriteList = @favouriteList,
                            numUseEnergy = @numUseEnergy,
                            AccumulatedPoint = @AccumulatedPoint,
                            PetDefLeague = @PetDefLeague,
                            EventPoint = @EventPoint,
                            achievements = @achievements,
                            NumOfUseKiteNormal = @NumOfUseKiteNormal,
                            NumOfUseKiteVip = @NumOfUseKiteVip,
                            CurrentAchievementId = @CurrentAchievementId,
                            letters = @letters,
                            RequestAddFriends = @RequestAddFriends,
                            BlockFriendLists = @BlockFriendLists,
                            ListFriends = @ListFriends,
                            LastTimeOnline = @LastTimeOnline,
                            LettersSendTime = @LettersSendTime,
                            MoneyDisplays = @MoneyDisplays,
                            TrashItemBackup = @TrashItemBackup,
                            ClanTasked = @ClanTasked,
                            TimeDropCoin = @TimeDropCoin,
                            lua = @lua,
                            IsMergeServer = @IsMergeServer,
                            NumGiveFlowerGold = @NumGiveFlowerGold,
                            NumGiveFlowerGem = @NumGiveFlowerGem,
                            IndexMilistoneTeacherEvent = @IndexMilistoneTeacherEvent,
                            FlowerGold = @FlowerGold,
                            FlowerCoin = @FlowerCoin,
                            DailyNoelTime = @DailyNoelTime,
                            DailyNoelIndex = @DailyNoelIndex,
                            NumEatSquareStickyRice = @NumEatSquareStickyRice,
                            NumEatSquareStickyRiceCoin = @NumEatSquareStickyRiceCoin,
                            NumEatCylindricalStickyRice = @NumEatCylindricalStickyRice,
                            NumEatCylindricalStickyRiceCoin = @NumEatCylindricalStickyRiceCoin,
                            IndexMilistoneBirthdayEvent = @IndexMilistoneBirthdayEvent,
                            NumUseGiftBox2025 = @NumUseGiftBox2025
                            WHERE ID = @ID", playerData);
    }
    /// <summary>
    /// Lưu thông tin người chơi
    /// </summary>
    /// <param name="playerData"></param>
    public static void saveStatic(PlayerData playerData)
    {
        using (var conn = MYSQLManager.create())
        {
            saveStatic(playerData, conn);
        }
    }
    /// <summary>
    /// Hành trang
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public CopyOnWriteArrayList<Item> this[sbyte type]
    {
        get
        {
            return getInventoryOrCreate(type);
        }
    }

    /// <summary>
    /// Lấy hành trang hoặc tạo mới nếu chưa có
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Lấy hành trang hoặc tạo mới nếu chưa có
    /// </summary>
    /// <param name="type"></param>
    /// <param name="item"></param>
    public void addItem(sbyte type, Item item)
    {
        CopyOnWriteArrayList<Item> list = getInventoryOrCreate(type);
        if (item.Template.isStackable)
        {
            var findDupcate = list.Where(p => p.Template.itemId == item.Template.itemId && p.canTrade == item.canTrade);
            if (findDupcate.Any())
            {
                findDupcate.First().count += item.count;
                foreach (var item1 in item.SourcesItem)
                {
                    findDupcate.First().SourcesItem.addIfAbsent(item1);
                }
                return;
            }
        }
        list.Add(item);
        list.BinaryObjectAdd(item);
        list.Sort(new BinaryCompare<Item>());
    }

    public void removeItem(sbyte type, Item item)
    {
        getInventoryOrCreate(type).remove(item);
    }

    public void addPet(Pet pet, Player player)
    {
        pets.Add(pet);
        pets.BinaryObjectAdd(pet);
        pets.Sort(new BinaryCompare<Pet>());
    }

    public void addAchivement(Achievement achievement, Player player)
    {
        achievements.Add(achievement);
        achievements.BinaryObjectAdd(achievement);
        achievements.Sort(new BinaryCompare<Achievement>());
    }

    public void addLetter(Letter letter)
    {
        letters.Add(letter);
        letters.BinaryObjectAdd(letter);
        letters.Sort(new BinaryCompare<Letter>());
    }

    public Letter FindLetter(int letterId)
    {
        return letters.BinarySearch(letterId);
    }
}

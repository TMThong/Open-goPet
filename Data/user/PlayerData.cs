package data.user;

import com.google.gson.reflect.TypeToken;
import data.item.Item;
import data.pet.Pet;
import data.shop.ShopArena;
import data.task.TaskData;
import java.lang.reflect.Type;
import java.sql.Connection;
import java.sql.Date;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import lombok.NonNull;
import manager.JsonManager;
import manager.MYSQLManager;
import server.Player;
import util.Utilities;

public class PlayerData {
    public byte gender = 3;
    public String name;
    public long gold, coin, spendGold;
    public int charID, user_id;
    public ArrayList<Integer> friends = new ArrayList();
    public ArrayList<Integer> favouriteList = new ArrayList<>();
    public HashMap<@NonNull Byte, @NonNull CopyOnWriteArrayList<Item>> items = new HashMap<>();
    public CopyOnWriteArrayList<Pet> pets = new CopyOnWriteArrayList<>();
    public CopyOnWriteArrayList<Integer> tasking = new CopyOnWriteArrayList<>();
    public CopyOnWriteArrayList<TaskData> task = new CopyOnWriteArrayList<>();
    public CopyOnWriteArrayList<Integer> wasTask = new CopyOnWriteArrayList<>();
    public Pet petSelected;
    public bool isFirstFree = false;
    public int x, y;
    public byte speed = 4;
    public byte faceDir = 3;
    public byte waypointIndex = -1;
    public long deltaTimeQuestion = System.currentTimeMillis();
    public byte questIndex = 1;
    public Date loginDate;
    public int star = 0;
    public Item skinItem;
    public Item wingItem;
    public bool isOnSky = false;
    public BuffExp buffExp = new BuffExp();
    public int pkPoint = 0;
    public Date pkPointTime;
    public GopetCaptcha captcha;
    public bool isAdmin = false;
    public ShopArena shopArena;
    public int clanId;
    public String avatarPath;

    public PlayerData() {
        x = 24 * 4;
        y = 24 * 4;
    }

    public static PlayerData read(ResultSet result)   {
        PlayerData playerData = new PlayerData();
        playerData.user_id = result.getInt("user_id");
        playerData.charID = result.getInt("ID");
        playerData.gender = (byte) result.getInt("gender");
        playerData.name = result.getString("name");
        playerData.loginDate = result.getDate("loginDate");
        playerData.coin = result.getBigDecimal("coin").longValue();
        playerData.gold = result.getBigDecimal("gold").longValue();
        playerData.star = result.getInt("star");
        playerData.isOnSky = result.getByte("isOnSky") == 1;
        playerData.pkPoint = result.getInt("pkPoint");
        playerData.pkPointTime = result.getDate("pkPointTime");
        playerData.isAdmin = result.getByte("isAdmin") == 1;
        playerData.clanId = result.getInt("clanId");
        playerData.avatarPath = result.getString("avatarPath");
        playerData.spendGold = result.getBigDecimal("spendGold").longValue();
        String friendJson = result.getString("friends");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<ArrayList<Integer>>() {
            }.getType();
            playerData.friends = (ArrayList<Integer>) JsonManager.LoadFromJson(friendJson, arrayType);
        }

        String items = result.getString("items");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<HashMap<@NonNull Byte, @NonNull CopyOnWriteArrayList<Item>>>() {
            }.getType();
            playerData.items = (HashMap<@NonNull Byte, @NonNull CopyOnWriteArrayList<Item>>) JsonManager.LoadFromJson(items, arrayType);
        }

        String favouriteList = result.getString("favouriteList");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<ArrayList<Integer>>() {
            }.getType();
            playerData.favouriteList = (ArrayList<Integer>) JsonManager.LoadFromJson(favouriteList, arrayType);
        }

        playerData.isFirstFree = result.getByte("isFirstFree") == 1;

        String petsString = result.getString("pets");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<CopyOnWriteArrayList<Pet>>() {
            }.getType();
            playerData.pets = (CopyOnWriteArrayList<Pet>) JsonManager.LoadFromJson(petsString, arrayType);
        }
        String petSelectedString = result.getString("petSelected");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<Pet>() {
            }.getType();
            playerData.petSelected = (Pet) JsonManager.LoadFromJson(petSelectedString, arrayType);
        }
        String skinString = result.getString("skin");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<Item>() {
            }.getType();
            playerData.skinItem = (Item) JsonManager.LoadFromJson(skinString, arrayType);
        }
        String wingString = result.getString("wing");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<Item>() {
            }.getType();
            playerData.wingItem = (Item) JsonManager.LoadFromJson(wingString, arrayType);
        }

        String buffExpStr = result.getString("buffExp");
        if (!result.wasNull()) {
            playerData.buffExp = (BuffExp) JsonManager.LoadFromJson(buffExpStr, BuffExp.class);
            playerData.buffExp.loadCurrentTime();
        }

        String captchaStr = result.getString("captcha");
        if (!result.wasNull()) {
            playerData.captcha = (GopetCaptcha) JsonManager.LoadFromJson(captchaStr, GopetCaptcha.class);
        }

        String shopArenaStr = result.getString("shopArena");
        if (!result.wasNull()) {
            playerData.shopArena = (ShopArena) JsonManager.LoadFromJson(shopArenaStr, ShopArena.class);
        }

        String taskStr = result.getString("task");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<CopyOnWriteArrayList<TaskData>>() {
            }.getType();
            playerData.task = (CopyOnWriteArrayList<TaskData>) JsonManager.LoadFromJson(taskStr, arrayType);
        }

        String taskingStr = result.getString("tasking");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<CopyOnWriteArrayList<Integer>>() {
            }.getType();
            playerData.tasking = (CopyOnWriteArrayList<Integer>) JsonManager.LoadFromJson(taskingStr, arrayType);
        }

        String wasTaskStr = result.getString("wasTask");
        if (!result.wasNull()) {
            Type arrayType = new TypeToken<CopyOnWriteArrayList<Integer>>() {
            }.getType();
            playerData.wasTask = (CopyOnWriteArrayList<Integer>) JsonManager.LoadFromJson(wasTaskStr, arrayType);
        }
        return playerData;
    }

    public static void create(int user_id, String name, byte gender)   {
        Connection connection = MYSQLManager.create();
        MYSQLManager.updateSql(
                String.format(
                        "INSERT INTO `player` (`ID`, `user_id`, `name`, `gender` , `items`) VALUES (NULL, '%s', '%s', '%s' , NULL);",
                        user_id, name, gender), connection);
        connection.close();
    }

    public void save()   {
        Connection connection = MYSQLManager.create();
        MYSQLManager.updateSql(String.format("update player set  " + getGopetSQLString() + "   coin = %s , gold = %s  ,  friends = '%s'   ,    items = '%s',   favouriteList = '%s'  where ID = "
                + charID,
                coin,
                gold,
                JsonManager.ToJson(friends),
                JsonManager.ToJson(items),
                JsonManager.ToJson(favouriteList)
        ), connection);
        connection.close();
    }

    public String getGopetSQLString()   {
        String[] lines = new String[]{
            "pets = '" + JsonManager.ToJson(pets) + "'",
            "petSelected = '" + JsonManager.ToJson(petSelected) + "'",
            "isFirstFree = " + (isFirstFree ? "1" : "0"),
            "loginDate = '" + Utilities.toDateString(loginDate) + "'",
            "star = " + star,
            "skin = '" + JsonManager.ToJson(skinItem) + "'",
            "wing = '" + JsonManager.ToJson(wingItem) + "'",
            "isOnSky = " + (isOnSky ? 1 : 0),
            "buffExp = '" + JsonManager.ToJson(buffExp) + "'",
            "pkPoint = " + pkPoint,
            "pkPointTime = '" + Utilities.toDateString(pkPointTime) + "'",
            "captcha ='" + JsonManager.ToJson(captcha) + "'",
            "shopArena = '" + JsonManager.ToJson(shopArena) + "'",
            "clanId = " + clanId,
            "task = '" + JsonManager.ToJson(task) + "'",
            "tasking ='" + JsonManager.ToJson(tasking) + "'",
            "avatarPath = '" + avatarPath + "'",
            "wasTask = '" + JsonManager.ToJson(wasTask) + "'",
            "spendGold = " + spendGold
        };
        String str = String.join(",", lines) + " ,";
        return str;
    }
 

    public CopyOnWriteArrayList<Item> getInventoryOrCreate(byte type) {
        if (items.containsKey(type)) {
            return items.get(type);
        } else {
            CopyOnWriteArrayList<Item> list = new CopyOnWriteArrayList<>();
            items.put(type, list);
            return list;
        }
    }

    public void addItem(byte type, Item item) {
        CopyOnWriteArrayList<Item> list = getInventoryOrCreate(type);
        list.add(item);
        while (true) {
            item.itemId = Utilities.nextInt(10, Integer.MAX_VALUE - 2);
            bool flag = true;
            for (Item item1 : getInventoryOrCreate(type)) {
                if (item1 != item) {
                    if (item1.itemId == item.itemId) {
                        flag = false;
                    }
                }
            }
            if (flag) {
                break;
            }
        }
        list.sort(new Comparator<Item>() {
            @Override
            public int compare(Item obj1, Item obj2) {
                return obj1.itemId - obj2.itemId;
            }
        });
    }

    public void removeItem(byte type, Item item) {
        getInventoryOrCreate(type).remove(item);
    }

    public void addPet(Pet pet, Player player) {
        pets.add(pet);
        bool flagId = false;
        while (true) {
            if (pet.petId > 0 && !flagId) {
                flagId = true;
            } else {
                pet.petId = Utilities.nextInt(10, Integer.MAX_VALUE - 2);
            }
            bool flag = true;
            for (Pet item1 : pets) {
                if (item1 != pet) {
                    if (item1.petId == pet.petId) {
                        flag = false;
                    }
                }
            }
            if (flag) {
                break;
            }
        }
        pets.sort(new Comparator<Pet>() {
            @Override
            public int compare(Pet obj1, Pet obj2) {
                return obj1.petId - obj2.petId;
            }
        });
    }
}

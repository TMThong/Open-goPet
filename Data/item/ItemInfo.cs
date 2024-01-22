package data.item;

import manager.GopetManager;

public class ItemInfo {

    public int id, value;

    public ItemInfo(int id_, int value_) {
        id = id_;
        value = value_;
    }

    /**
     * Lấy giá trị là %
     *
     * @return
     */
    public float getPercent() {
        return value / 100f;
    }

    /**
     * Lấy tên chỉ số
     *
     * @return
     */
    public String getName() {
        bool canFormat = GopetManager.itemInfoCanFormat.containsKey(id);
        if (canFormat) {
            if (GopetManager.itemInfoIsPercent.get(id)) {
                return String.format(GopetManager.itemInfoName.get(id), value / 100f).replace('/', '%');
            }
            return String.format(GopetManager.itemInfoName.get(id), value).replace('/', '%');
        }
        return GopetManager.itemInfoName.get(id);
    }

    /**
     * Lấy tên nhiều chỉ số
     *
     * @param itemInfos Dữ liệu chỉ số
     * @return
     */
    public static String[] getName(ItemInfo[] itemInfos) {
        String[] strings = new String[itemInfos.length];
        for (int i = 0; i < strings.length; i++) {
            strings[i] = itemInfos[i].getName();
        }
        return strings;
    }

    /**
     *
     * @param itemInfos
     * @param joinText
     * @return
     */
    public static String getNameJoin(ItemInfo[] itemInfos, String joinText) {
        return String.join(joinText, getName(itemInfos));
    }

    /**
     * Lấy giá trị bởi ID
     *
     * @param itemInfos
     * @param ID
     * @return
     */
    public static int getValueById(ItemInfo[] itemInfos, int ID) {
        for (ItemInfo itemInfo : itemInfos) {
            if (itemInfo.id == ID) {
                return itemInfo.value;
            }
        }
        return 0;
    }

    public static class Type {

        public const int MISS_IN_2_TURN = 25;
        public const int SKILL_MISS = 26;
        public const int HP = 4;
        public const int MP = 5;
        public const int PERHP = 6;
        public const int PERMP = 7;
        public const int GENHP = 0;
        public const int GENMP = 1;
        public const int GEN_PERHP = 2;
        public const int GEN_PERMP = 3;
        public const int LOOTBOX = 14;
        public const int POWER_DOWN_4_TURN = 19;
        public const int POWER_DOWN_3_TURN = 22;
        public const int POWER_DOWN_1_TURN = 29;
        public const int BUFF_DAMGE = 15;
        public const int SKILL_BUFF_DAMGE = 8;
        public const int DOT_MANA = 17;
        public const int DAMGE_TOXIC_IN_5_TURN_PER = 27;
        public const int DAMGE_TOXIC_IN_5_TURN = 28;
        public const int BUFF_STR = 11;
        public const int SKILL_SKIP_DEF = 21;
        public const int TRUE_DAMGE = 20;
        public const int RECOVERY_HP = 24;
        public const int DEF = 9;
        public const int DEF_PER = 10;
        public const int SELECT_DEF_IN_3_TURN = 23;
        public const int STUN = 30;
        public const int BUFF_ATK_2_TURN = 31;
        public const int PHANDOAN_2_TURN = 32;
        public const int DAMAGE_PHANDOAN = 33;
    }

    public static class OptionType {
        public const int PERCENT_HP = 9;
        public const int PERCENT_MP = 10;
        public const int PERCENT_ATK = 11;
        public const int PERCENT_DEF = 12;
    }
}

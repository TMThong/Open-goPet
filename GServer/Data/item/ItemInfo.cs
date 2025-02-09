using Gopet.Util;

namespace Gopet.Data.GopetItem
{
    public class ItemInfo
    {

        public int id, value;

        public ItemInfo(int id_, int value_)
        {
            id = id_;
            value = value_;
        }

        /**
         * Lấy giá trị là %
         *
         * @return
         */
        public float getPercent()
        {
            return value / 100f;
        }

        /**
         * Lấy tên chỉ số
         *
         * @return
         */
        public string getName(Player player)
        {
            bool canFormat = GopetManager.itemInfoCanFormat.ContainsKey(id);
            if (canFormat)
            {
                if (GopetManager.itemInfoIsPercent.get(id))
                {
                    return Utilities.Format(player.Language.ItemInfoNameLanguage[id], value / 100f).Replace('/', '%');
                }
                return Utilities.Format(player.Language.ItemInfoNameLanguage[id], value).Replace('/', '%');
            }
            return player.Language.ItemInfoNameLanguage[id];
        }

        /**
         * Lấy tên nhiều chỉ số
         *
         * @param itemInfos Dữ liệu chỉ số
         * @return
         */
        public static string[] getName(ItemInfo[] itemInfos, Player player)
        {
            string[] strings = new string[itemInfos.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = itemInfos[i].getName(player);
            }
            return strings;
        }

        /**
         *
         * @param itemInfos
         * @param joinText
         * @return
         */
        public static string getNameJoin(ItemInfo[] itemInfos, string joinText, Player player)
        {
            return string.Join(joinText, getName(itemInfos, player));
        }

        /**
         * Lấy giá trị bởi ID
         *
         * @param itemInfos
         * @param ID
         * @return
         */
        public static int getValueById(ItemInfo[] itemInfos, int ID)
        {
            foreach (ItemInfo itemInfo in itemInfos)
            {
                if (itemInfo.id == ID)
                {
                    return itemInfo.value;
                }
            }
            return 0;
        }

        public static bool hasId(ItemInfo[] itemInfos, int ID)
        {
            return itemInfos.Any(p => p.id == ID && p.value > 0);
        }


        public static class Type
        {
            public const int GENHP = 0;
            public const int GENMP = 1;
            public const int GEN_PERHP = 2;
            public const int GEN_PERMP = 3;
            public const int HP = 4;
            public const int MP = 5;
            public const int PERHP = 6;
            public const int PERMP = 7;
            public const int SKILL_BUFF_DAMGE = 8;
            public const int DEF = 9;
            public const int DEF_PER = 10;
            public const int BUFF_STR = 11;
            public const int LOOTBOX = 14;
            public const int BUFF_DAMGE = 15;
            public const int DOT_MANA = 17;
            public const int POWER_DOWN_4_TURN = 19;
            public const int TRUE_DAMGE = 20;
            public const int SKILL_SKIP_DEF = 21;
            public const int POWER_DOWN_3_TURN = 22;
            public const int SELECT_DEF_IN_3_TURN = 23;
            public const int RECOVERY_HP = 24;
            public const int MISS_IN_99999_TURN = 25;
            public const int SKILL_MISS = 26;
            public const int DAMGE_TOXIC_IN_3_TURN_PER = 27;
            public const int DAMGE_TOXIC_IN_5_TURN = 28;
            public const int POWER_DOWN_1_TURN = 29;
            public const int STUN = 30;
            public const int BUFF_ATK_3_TURN = 31;
            public const int PHANDOAN_4_TURN = 32;
            public const int DAMAGE_PHANDOAN = 33;
            public const int PER_STUN_1_TURN= 36;
            public const int PER_DEF_BUFF_3_TURN = 37;
            public const int DOT_MANA_BY_ATK = 38;
            public const int BLOODSUCKING_BASED_ON_DAMAGE_PASSIVE_BUFF = 39;
            public const int PERCENT_EXP = 40;
            public const int PERCENT_GEM = 41;
            public const int PERCENT_ALL_INFO = 42;
            public const int DAMGE_TOXIC_IN_999999_TURN = 43;
            public const int BUFF_DEF_IN_4_TURN = 44;
            public const int RECOVERY_HP_IN_4_TURN = 45;
            public const int KÍCH_ẨN_PHẢN_ĐÒN = 46;
            public const int KÍCH_ẨN_ĐỊNH_THÂN = 47;
            public const int KÍCH_ẨN_HÚT_MÁU = 48;
            public const int TỈ_LỆ_ĐỊNH_THÂN_KHI_ĐÁNH_TRÚNG = 49;
        }

        public static class OptionType
        {
            public const int PERCENT_HP = 9;
            public const int PERCENT_MP = 10;
            public const int PERCENT_ATK = 11;
            public const int PERCENT_DEF = 12;
            public const int OPTION_BATTLE = 13;
            public const int OPTION_BATTLE_TURN = 14;
            public const int OPTION_BATTLE_VALUE = 15;
            /// <summary>
            /// Buff dành cho người mang trang bị hay người đối diện
            /// </summary>
            public const int OPTION_BATTLE_IS_FOR_ACTIVE = 16;
            public const int OPTION_HOURS_UP_COIN = 17;
            public const int OPTION_ANTI_PK = 18;
        }
    }
}
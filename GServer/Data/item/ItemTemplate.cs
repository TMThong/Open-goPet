
using Gopet.Util;
using Newtonsoft.Json;
using System.Numerics;

namespace Gopet.Data.GopetItem
{
    /// <summary>
    /// Mẫu thử vật phẩm
    /// </summary>
    public class ItemTemplate
    {
        public int itemId { get; private set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string name { get; private set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string description { get; private set; }
        /// <summary>
        /// Yêu cầu sức mạnh
        /// </summary>
        public int requireStr { get; private set; }
        /// <summary>
        /// Yêu cầu nhanh nhẹn
        /// </summary>
        public int requireAgi { get; private set; }
        /// <summary>
        /// Yêu cầu thông minh
        /// </summary>
        public int requireInt { get; private set; }
        /// <summary>
        /// Loại
        /// </summary>
        public int type { get; private set; }
        /// <summary>
        /// Option của item dùng cho các vật phẩm đặc biệt
        /// </summary>
        public int[] itemOption { get; private set; } = new int[0];
        /// <summary>
        /// Giá trị option dùng cho cánh, skin v.v
        /// </summary>
        public int[] itemOptionValue { get; private set; } = new int[0];
        /// <summary>
        /// Khoảng random chỉ số healt point
        /// </summary>
        public int[] hpRange { get; private set; }
        /// <summary>
        /// Khoảng random chỉ số mana point
        /// </summary>
        public int[] mpRange { get; private set; }
        /// <summary>
        /// Khoảng random chỉ số tấn công
        /// </summary>
        public int[] atkRange { get; private set; }
        /// <summary>
        /// Khoảng random chỉ số phòng thủ
        /// </summary>
        public int[] defRange { get; private set; }
        /// <summary>
        /// Là gộp
        /// </summary>
        public bool isStackable { get; private set; }
        /// <summary>
        /// Đường dẫn khung hình cho các item skin, wing v.v
        /// </summary>
        public string frameImgPath { get; private set; }
        /// <summary>
        /// Đường dẫn icon
        /// </summary>
        public string iconPath { get; private set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public sbyte gender { get; private set; }
        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public long expire { get; private set; }
        /// <summary>
        /// Là của thiên đình
        /// </summary>
        public bool isOnSky { get; private set; }
        /// <summary>
        /// Có thể bán
        /// </summary>
        public bool canTrade { get; private set; }
        /// <summary>
        /// Phái thú cưng
        /// </summary>
        public sbyte petNClass { get; private set; }
        /// <summary>
        /// Đường dẫn icon nhưng cast theo id
        /// Dùng trong hiển thị ngọc nguyên tố
        /// </summary>
        public int iconId { get; private set; }
        /// <summary>
        /// Giá sàn
        /// </summary>
        public int price { get; private set; }
        /// <summary>
        /// Hệ
        /// </summary>
        public sbyte element { get; private set; }
        /// <summary>
        /// Có thể dung hợp
        /// </summary>
        public bool CanFusion
        {
            get
            {
                if (GopetManager.tierItemHashMap.ContainsKey(itemId) && IsEquip)
                {
                    return GopetManager.tierItemHashMap[itemId] == 1;
                }
                return false;
            }
        }

        public void setIconId(int iconId)
        {
            this.iconId = iconId;
        }

        public int getItemId()
        {
            return itemId;
        }

        public string getName(Player player)
        {
            return player.Language.ItemLanguage[itemId];
        }

        public string getDescription(Player player)
        {
            return player.Language.ItemDescLanguage[itemId];
        }


        public int getRequireStr()
        {
            return requireStr;
        }

        public int getRequireAgi()
        {
            return requireAgi;
        }

        public int getRequireInt()
        {
            return requireInt;
        }

        public int getType()
        {
            return type;
        }

        public int[] getOption()
        {
            return itemOption;
        }

        public int[] getOptionValue()
        {
            return itemOptionValue;
        }



        public string getFrameImgPath()
        {
            return frameImgPath;
        }

        public string getIconPath()
        {
            return iconPath;
        }


        public sbyte getNClass()
        {
            return petNClass;
        }

        /// <summary>
        /// Là trang bị
        /// </summary>
        [JsonIgnore]
        public bool IsEquip
        {
            get
            {
                switch (type)
                {
                    case GopetManager.PET_EQUIP_ARMOUR:
                    case GopetManager.PET_EQUIP_GLOVE:
                    case GopetManager.PET_EQUIP_SHOE:
                    case GopetManager.PET_EQUIP_WEAPON:
                    case GopetManager.PET_EQUIP_HAT:
                        return true;
                    default:
                        return false;
                }
            }
        }


        public string getNameViaType(Player player)
        {
            switch (type)
            {
                case GopetManager.PET_EQUIP_ARMOUR:
                case GopetManager.PET_EQUIP_GLOVE:
                case GopetManager.PET_EQUIP_SHOE:
                case GopetManager.PET_EQUIP_WEAPON:
                case GopetManager.PET_EQUIP_HAT:
                //return Utilities.Format("%s chỉ số (%s(atk) %s(def) %s(hp) %s(mp)) Yêu cầu (%s(str) %s(int) %s(agi))", name, atk, def, hp, mp, requireStr, requireInt, requireAgi);
                default:
                    return getName(player);
            }
        }

        public string getDescriptionViaType(Player player)
        {
            switch (type)
            {
                case GopetManager.ITEM_PART_PET:
                    {
                        if (itemOptionValue.Length >= 2)
                        {
                            int petId = itemOptionValue[0];
                            int count = itemOptionValue[1];
                            PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(petId);
                            if (petTemplate != null)
                            {
                                return string.Format(player.Language.ItemPartPetDesc, count, petTemplate.getName(player));
                            }
                        }
                        return getDescription(player);
                    }
                case GopetManager.ITEM_PART_ITEM:
                    {
                        if (itemOptionValue.Length >= 2)
                        {
                            int itemidTemp = itemOptionValue[0];
                            int count = itemOptionValue[1];
                            ItemTemplate itemTemplate = GopetManager.itemTemplate.get(itemidTemp);
                            if (itemTemplate != null)
                            {
                                return string.Format(player.Language.ItemPartItemDesc, count, itemTemplate.getName(player));
                            }
                        }
                        return getDescription(player);
                    }
                default:
                    return getDescription(player);
            }
        }


        public string getRange(string icon, int[] range)
        {
            if (range == null) return string.Format("[{0} ({2}) -{1} ({2}) ]", 0, 0, icon);

            if (range.Length == 0) return string.Empty;

            if (range.Length == 1) return range[0].ToString() + " (" + icon + ") ";

            return string.Format("[{0} ({2}) -{1} ({2}) ]", range[0], range[1], icon);
        }

        internal string getAtk()
        {
            return getRange("atk", this.atkRange);
        }

        internal object getDef()
        {
            return getRange("def", this.defRange);
        }

        internal object getHp()
        {
            return getRange("hp", this.hpRange);
        }

        internal object getMp()
        {
            return getRange("mp", this.mpRange);
        }
    }
}
using Gopet.Data.Collections;
using Gopet.Data.item;
using Gopet.Util;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace Gopet.Data.GopetItem
{
    public class Item : IBinaryObject<Item>
    {

        public int itemId;

        public int itemTemplateId = -1;

        public string itemUID = Utilities.GetUID();

        public int count = 0;

        public int petEuipId = -1;

        public int lvl = 0;

        public int[] option = new int[0];

        public int[] optionValue = new int[0];

        public long expire = -1;

        public int def;

        public int atk;

        public int hp;

        public int mp;

        public float[] gemOptionValue = null;
        /// <summary>
        /// Dữ liệu về ngọc
        /// </summary>
        public ItemGem gemInfo = null;
        /// <summary>
        /// Đã treo chợ
        /// </summary>
        public bool wasSell = false;
        /// <summary>
        /// Có thể treo chợ
        /// </summary>
        public bool canTrade { get; set; } = true;

        public ConcurrentDictionary<int, Tuple<int, int, int, int>> EnchantInfo { get; set; } = new ConcurrentDictionary<int, Tuple<int, int, int, int>>();
        /// <summary>
        /// Nguồn gốc của vật phẩm
        /// </summary>
        public CopyOnWriteArrayList<ItemSource> SourcesItem = new(ItemSource.TỰ_SINH_RA);
        /// <summary>
        /// Ngày vật phẩm sinh ra
        /// </summary>
        public DateTime TimeCreate { get; set; } = DateTime.Now;

        /// <summary>
        /// Số lần dung hợp
        /// </summary>
        public byte NumFusion { get; set; } = 0;

        public Version version { get; set; } = new Version(1, 0, 0, 0);

        public Item()
        {

        }

        public Item(int itemTemplateId, int count_ = 0, bool isSkipRandom = false)
        {
            this.version = new Version(1, 0, 0, 1);
            this.SourcesItem.Clear();
            this.count = count_;
            this.itemTemplateId = itemTemplateId;
            option = getTemp().getOption();
            optionValue = getTemp().getOptionValue();
            ItemTemplate itemTemplate = getTemp();
            if (!isSkipRandom)
            {
                atk = RandomInfoItem(itemTemplate.atkRange);
                def = RandomInfoItem(itemTemplate.defRange);
                hp = RandomInfoItem(itemTemplate.hpRange);
                mp = RandomInfoItem(itemTemplate.mpRange);
            }
        }
        /// <summary>
        /// THêm thông tin cho chỉ số trước
        /// </summary>
        public void AddEnchantInfo()
        {
            Tuple<int, int, int, int> tuple = new Tuple<int, int, int, int>(getHp(), getMp(), getAtk(), getDef());
            this.EnchantInfo[lvl] = tuple;
        }

        public virtual int HpItem
        {
            get
            {
                if (lvl == 0)
                {
                    return hp;
                }

                if (EnchantInfo.ContainsKey(lvl - 1))
                {
                    return EnchantInfo[lvl - 1].Item1;
                }

                return hp;
            }
        }

        public virtual int MpItem
        {
            get
            {
                if (lvl == 0)
                {
                    return mp;
                }

                if (EnchantInfo.ContainsKey(lvl - 1))
                {
                    return EnchantInfo[lvl - 1].Item2;
                }

                return mp;
            }
        }
        public virtual int AtkItem
        {
            get
            {
                if (lvl == 0)
                {
                    return atk;
                }

                if (EnchantInfo.ContainsKey(lvl - 1))
                {
                    return EnchantInfo[lvl - 1].Item3;
                }

                return atk;
            }
        }
        public virtual int DefItem
        {
            get
            {
                if (lvl == 0)
                {
                    return def;
                }

                if (EnchantInfo.ContainsKey(lvl - 1))
                {
                    return EnchantInfo[lvl - 1].Item4;
                }

                return def;
            }
        }


        public static int RandomInfoItem(int[] range)
        {
            if (range != null)
            {
                if (range.Length > 0)
                {
                    if (range.Length >= 2)
                    {
                        return Utilities.nextInt(range[0], range[1]);
                    }
                    return range[0];
                }
            }
            return 0;
        }

        public static int GetMaxOption(int[] range)
        {
            if (range != null)
            {
                if (range.Length > 0)
                {
                    if (range.Length >= 2)
                    {
                        return range[1];
                    }
                    return range[0];
                }
            }
            return 0;
        }

        public ItemTemplate getTemp()
        {
            return this.Template;
        }
        [JsonIgnore]
        public ItemTemplate Template
        {
            get
            {
                if (!GopetManager.itemTemplate.ContainsKey(itemTemplateId))
                {
                    return GopetManager.itemTemplate[-1];
                }
                return GopetManager.itemTemplate.get(itemTemplateId);
            }
        }

        [JsonIgnore]
        public Item Instance => this;

        public string getDescription(Player player)
        {
            ItemTemplate itemTemplate = getTemp();
            var strs = GopetManager.HiddentStatItemTemplates.Where(x => x.IdWeapon == itemTemplateId || x.IdHat == itemTemplateId || x.IdGlove == itemTemplateId || x.IdArmour == itemTemplateId || x.IdShoe == itemTemplateId).Select(x => x.Comment);
            switch (itemTemplate.getType())
            {
                case GopetManager.SKIN_ITEM:
                    {
                        string strExpire = "";
                        if (expire > 0)
                        {
                            strExpire = "(" + player.Language.ExpireDescrption + Utilities.ToDateString(Utilities.GetDate(expire)) + " )";
                        }
                        else
                        {
                            strExpire = player.Language.ExpireItemInfinityDescription;
                        }
                        return Utilities.Format("+%s (atk) +%s (def) +%s (hp) +%s (mp)", atk, def, hp, mp) + strExpire;

                    }
                case GopetManager.WING_ITEM:
                    {
                        string strExpire = "";
                        if (expire > 0)
                        {
                            strExpire = "(" + player.Language.ExpireDescrption + Utilities.GetFormatNumber(expire) + " )";
                        }
                        else
                        {
                            strExpire = player.Language.ExpireItemInfinityDescription;
                        }

                        string getSign(int value)
                        {
                            if (value > 0)
                                return "+";
                            return "-";
                        }

                        string wingBuffDesc = string.Empty;

                        var extractBuff = this.ExtractBattleOptions();
                        if (extractBuff.Length > 0)
                        {
                            foreach (var buff in extractBuff)
                            {
                                if (buff.OptionValue != 0)
                                {
                                    switch (buff.OptionId)
                                    {
                                        case ItemInfo.Type.BUFF_DAMGE:
                                            wingBuffDesc += getSign(buff.OptionValue) + Utilities.round(buff.OptionValue / 100f) + "%(atk)";
                                            break;
                                        case ItemInfo.Type.DEF_PER:
                                            wingBuffDesc += getSign(buff.OptionValue) + Utilities.round(buff.OptionValue / 100f) + "%(def)";
                                            break;
                                        case ItemInfo.Type.PHANDOAN_4_TURN:
                                            wingBuffDesc += player.Language.CounterAttack + Utilities.round(buff.OptionValue / 100f) + player.Language.PercentDamageItemDesc;
                                            break;
                                        case ItemInfo.Type.PER_STUN_1_TURN:
                                            wingBuffDesc += Utilities.round(buff.OptionValue / 100f) + player.Language.PercentStunItemDesc;
                                            break;
                                        case ItemInfo.Type.RECOVERY_HP:
                                            wingBuffDesc += player.Language.PercentBloodSuckingItemDesc + Utilities.round(buff.OptionValue / 100f) + "%";
                                            break;
                                    }
                                }
                            }
                        }
                        return strExpire + player.Language.ItemInfoApply + Utilities.Format(" +%s (atk) +%s (def) +%s (hp) +%s (mp) ", getAtk(), getDef(), getHp(), getMp()) + wingBuffDesc;

                    }
            }

            if (expire > 0)
            {
                return getTemp().getDescription(player) + "(" + player.Language.ExpireDescrption + Utilities.GetFormatNumber(expire) + " ) " + string.Join(",", strs);
            }
            return getTemp().getDescription(player) + string.Join(",", strs);
        }
        public static int GetPercentBuff(int index, int[] array)
        {
            if (index >= 0 && index < array.Length)
            {
                return array[index];
            }
            return 1;
        }

        public int getDef()
        {
            int value = DefItem;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)Utilities.GetValueFromPercent(value, GetPercentBuff(lvl, GopetManager.PERCENT_BUFF_DEF_ITEM));
            info += Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_DEF)));
            return GetFusionValue(info, NumFusion, 5f);
        }

        public int getAtk()
        {
            int value = AtkItem;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)Utilities.GetValueFromPercent(value, GetPercentBuff(lvl, GopetManager.PERCENT_BUFF_ATK_ITEM));
            info += Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_ATK)));
            return GetFusionValue(info, NumFusion, 10f);
        }


        public int getHp()
        {
            int value = HpItem;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)Utilities.GetValueFromPercent(value, GetPercentBuff(lvl, GopetManager.PERCENT_BUFF_HP_ITEM));
            info += Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_HP)));
            return GetFusionValue(info, NumFusion, 30f);
        }


        public int getMp()
        {
            int value = MpItem;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)Utilities.GetValueFromPercent(value, GetPercentBuff(lvl, GopetManager.PERCENT_BUFF_MP_ITEM));
            info += Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_MP)));
            return GetFusionValue(info, NumFusion, 5f);
        }

        public static int GetFusionValue(int value, int fusion, float Percent)
        {
            for (int i = 0; i < fusion; i++)
            {
                value += Utilities.round(Utilities.GetValueFromPercent(value, Percent));
            }
            return value;
        }

        /// <summary>
        /// Tìm kiếm vật phẩm tuần tự theo loai5
        /// </summary>
        /// <param name="type">Loại</param>
        /// <param name="listNeedSearchItems">Danh sách cần duyệt</param>
        /// <param name="filter">Bộ lọc</param>
        /// <returns>Danh sách các vật phẩm cùng loại</returns>

        public static CopyOnWriteArrayList<Item> search(int type, CopyOnWriteArrayList<Item> listNeedSearchItems, Func<Item, bool> filter = null)
        {
            CopyOnWriteArrayList<Item> arrayList = new();
            foreach (Item item in listNeedSearchItems)
            {
                if (item.getTemp().getType() == type)
                {
                    if (filter != null)
                    {
                        if (!filter.Invoke(item))
                        {
                            continue;
                        }
                    }

                    arrayList.Add(item);
                }
            }
            return arrayList;
        }

        public static CopyOnWriteArrayList<Item> search(JArrayList<int> types, CopyOnWriteArrayList<Item> listNeedSearchItems, Func<Item, bool> filter = null)
        {

            if (types == null) return listNeedSearchItems;

            CopyOnWriteArrayList<Item> arrayList = new();
            foreach (Item item in listNeedSearchItems)
            {

                if (types.Contains(item.getTemp().getType()))
                {
                    if (filter != null)
                    {
                        if (!filter.Invoke(item))
                        {
                            continue;
                        }
                    }
                    arrayList.Add(item);
                }
            }
            return arrayList;
        }

        public string getName(Player player)
        {
            if (getTemp().isStackable)
            {
                return getTemp().getName(player) + " x" + count + (canTrade ? "" : player.Language.LockItemDescrption);
            }
            if (Template.type == GopetManager.WING_ITEM)
            {
                return getTemp().getName(player) + player.Language.level + lvl + (canTrade ? "" : player.Language.LockItemDescrption);
            }
            return getTemp().getName(player) + (canTrade ? "" : player.Language.LockItemDescrption);
        }

        public string getEquipName(Player player)
        {
            var strs = GopetManager.HiddentStatItemTemplates.Where(x => x.IdWeapon == itemTemplateId || x.IdHat == itemTemplateId || x.IdGlove == itemTemplateId || x.IdArmour == itemTemplateId || x.IdShoe == itemTemplateId).Select(x => x.Comment);
            JArrayList<string> infoStrings = new();
            if (getAtk() > 0)
            {
                infoStrings.add(getAtk() + " (atk) ");
            }
            if (getDef() > 0)
            {
                infoStrings.add(getDef() + " (def) ");
            }
            if (getHp() > 0)
            {
                infoStrings.add(getHp() + " (hp) ");
            }
            if (getMp() > 0)
            {
                infoStrings.add(getMp() + " (mp) ");
            }

            switch (getTemp().getType())
            {
                case GopetManager.ITEM_GEM:
                    {
                        if (gemOptionValue == null)
                        {
                            updateGemOption();
                        }
                        for (int i = 0; i < option.Length; i++)
                        {
                            int j = option[i];
                            float info = gemOptionValue[i];
                            switch (j)
                            {
                                case ItemInfo.OptionType.PERCENT_HP:
                                    infoStrings.add(Utilities.Format(player.Language.Up + " %s/ ", info).Replace('/', '%') + " (hp) ");
                                    break;
                                case ItemInfo.OptionType.PERCENT_MP:
                                    infoStrings.add(Utilities.Format(player.Language.Up + " %s/ ", info).Replace('/', '%') + " (mp) ");
                                    break;
                                case ItemInfo.OptionType.PERCENT_ATK:
                                    infoStrings.add(Utilities.Format(player.Language.Up + " %s/ ", info).Replace('/', '%') + " (atk) ");
                                    break;
                                case ItemInfo.OptionType.PERCENT_DEF:
                                    infoStrings.add(Utilities.Format(player.Language.Up + " %s/ ", info).Replace('/', '%') + " (def) ");
                                    break;
                            }
                        }
                    }
                    break;
            }

            if (getTemp().getNClass() >= 0)
            {
                if (!getTemp().isOnSky)
                {
                    switch (getTemp().getNClass())
                    {
                        case GopetManager.Fighter:
                            infoStrings.add(player.Language.ForFighter);
                            break;
                        case GopetManager.Assassin:
                            infoStrings.add(player.Language.ForAssassin);
                            break;
                        case GopetManager.Wizard:
                            infoStrings.add(player.Language.ForWizard);
                            break;
                    }
                }
            }
            return getName(player) + "  " + getTemp().getDescription(player) + " " + Utilities.Format("up: %s ", lvl) + string.Join(" ", infoStrings.Concat(strs)) + (gemInfo == null ? "" : " " + gemInfo.getElementIcon());
        }

        public void updateGemOption()
        {
            if (gemInfo != null)
            {
                gemOptionValue = new float[gemInfo.option.Length];
                for (int i = 0; i < gemOptionValue.Length; i++)
                {
                    gemOptionValue[i] = gemInfo.optionValue[i] / 100f + (gemInfo.optionValue[i] / 100f * 2 + 80) / 100 * 4 / 2 * gemInfo.lvl;
                }
            }
            else if (getTemp().getType() == GopetManager.ITEM_GEM)
            {
                gemOptionValue = new float[option.Length];
                for (int i = 0; i < option.Length; i++)
                {
                    gemOptionValue[i] = optionValue[i] / 100f + (optionValue[i] / 100f * 2 + 80) / 100 * 4 / 2 * lvl;
                }
            }
            else
            {
                gemOptionValue = null;
            }
        }

        private float getPercentGemBuff(int idoption)
        {
            if (gemInfo != null)
            {
                if (gemInfo.option != null)
                    for (int i = 0; i < gemInfo.option.Length; i++)
                    {
                        int j = gemInfo.option[i];
                        if (j == idoption)
                        {
                            return gemOptionValue[i];
                        }
                    }
            }
            return 0;
        }

        public ItemBattleOptionBuff[] ExtractBattleOptions()
        {
            if (this.Template.itemOption.Contains(ItemInfo.OptionType.OPTION_BATTLE))
            {
                int flag = 0;
                ItemBattleOptionBuff[] itemBattleOptionBuffs = new ItemBattleOptionBuff[this.Template.itemOption.Where(p => p == ItemInfo.OptionType.OPTION_BATTLE).Count()];
                for (global::System.Int32 i = 0; i < this.Template.itemOption.Length;)
                {
                    if (this.Template.itemOption[i] == ItemInfo.OptionType.OPTION_BATTLE)
                    {
                        itemBattleOptionBuffs[flag] = new ItemBattleOptionBuff(this.optionValue[i], this.optionValue[i + 2], this.optionValue[i + 3] == 1, this.optionValue[i + 1] > 100 ? int.MaxValue - 10 : this.optionValue[i + 1]);
                        i += 3;
                    }
                    else
                    {
                        i++;
                    }
                }
                return itemBattleOptionBuffs;
            }
            return new ItemBattleOptionBuff[0];
        }

        public int GetId()
        {
            return this.itemId;
        }

        public void SetId(int id)
        {
            this.itemId = id;
        }
    }
}
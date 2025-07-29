
using Gopet.Data.Collections;
using Gopet.Data.GopetClan;
using Gopet.Data.GopetItem;
using Gopet.Data.item;
using Gopet.Data.pet;
using Gopet.Data.user;
using Gopet.Util;
using Newtonsoft.Json;

public class Pet : GameObject, IBinaryObject<Pet>
{


    public int petIdTemplate;

    public bool petDieByPK = false;
    public long TimeDieZ { get; set; } = Utilities.CurrentTimeMillis;

    public int petId;
    public int star = 0;

    public DateTime? Expire { get; set; } = null;

    public long exp = 0;


    public String name = null;

    public int str, agi, _int;

    /**
     * Điểm tiềm năng
     */

    public int tiemnang_point = 0;

    /// <summary>
    /// Điểm kỹ năng , dùng để học kỹ năng
    /// </summary>
    public int skillPoint = 0;

    /// <summary>
    /// Dữ liệu về skill skill[*][0] là skillId skill[*][1] là cấp của kỹ năng
    /// </summary>
    public int[][] skill = new int[0][];

    /**
     * Này là điểm tiềm năng Dùng trong luyện chỉ số Giúp tăng chỉ số
     * str,agi,int tiemnang[0] là str tiemnang[1] là agi tiemnang[2] là int
     */

    public int[] tiemnang = new int[] { 0, 0, 0 };

    /**
     * Hình xăm của pet chỉ thêm trong giao diện xăm hình
     */

    public CopyOnWriteArrayList<PetTatto> tatto = new();
    /**
     * Các vật phẩm mà pet đã trang bị
     */

    public JArrayList<int> equip = new();

    public bool isUpTier = false;

    public int pointTiemNangLvl = 3;

    public DateTime TimeCreated { get; set; } = DateTime.Now;

    public bool wasSell = false;

    public CopyOnWriteArrayList<int> HiddenStats = new CopyOnWriteArrayList<int>();

    public CopyOnWriteArrayList<PetEffect> PetEffects = new CopyOnWriteArrayList<PetEffect>();
    protected Pet()
    {

    }


    public Pet(int petIdTemplate)
    {
        this.petIdTemplate = petIdTemplate;
        maxHp = getPetTemplate().getHp();
        hp = maxHp;
        maxMp = getPetTemplate().getMp();
        mp = maxMp;
        agi = getPetTemplate().agi;
        str = getPetTemplate().str;
        _int = getPetTemplate()._int;
        pointTiemNangLvl = getPetTemplate().gymUpLevel;
    }

    public PetTemplate getPetTemplate()
    {
        return GopetManager.PETTEMPLATE_HASH_MAP.get(petIdTemplate);
    }

    public int getPetIdTemplate()
    {
        return petIdTemplate;
    }

    [JsonIgnore]
    public override PetTemplate Template
    {
        get
        {
            return GopetManager.PETTEMPLATE_HASH_MAP[this.petIdTemplate];
        }
    }

    public bool IsCrit
    {
        get
        {
            return isCrit();
        }
    }

    [JsonIgnore]
    public Pet Instance => this;

    public override int getAgi()
    {
        return agi + tiemnang[1];
    }

    public override int getInt()
    {
        return _int + tiemnang[2];
    }

    public override int getStr()
    {
        return str + tiemnang[0];
    }

    public void addExp(int exp)
    {
        this.exp += exp;
    }

    public void addHpPet(int i)
    {
        if (hp + i > maxHp)
        {
            hp = maxHp;
        }
        else
        {
            hp += i;
        }
    }

    public void subHp(int i)
    {
        if (hp - i <= 0)
        {
            hp = 0;
        }
        else
        {
            hp -= i;
        }
    }

    public void addMp(int i)
    {
        if (mp + i > maxMp)
        {
            mp = maxMp;
        }
        else
        {
            mp += i;
        }
    }

    public void addSkill(int skill, int skillLv)
    {
        int[][] oldSkillList = this.skill;
        this.skill = new int[oldSkillList.Length + 1][];
        this.skill[oldSkillList.Length] = new int[2];
        for (int i = 0; i < oldSkillList.Length; i++)
        {
            int[] is_ = oldSkillList[i];
            this.skill[i] = is_;
        }
        this.skill[oldSkillList.Length] = new int[] { skill, skillLv };
    }

    public PetSkill[] getSkills()
    {
        PetSkill[] petSkills = new PetSkill[skill.Length];
        for (int i = 0; i < skill.Length; i++)
        {
            int[] skillInfo = skill[i];
            petSkills[i] = GopetManager.PETSKILL_HASH_MAP.get(skillInfo[0]);
        }
        return petSkills;
    }

    public void lvlUP()
    {
        this.lvl++;
        if (Template.gymUpLevel > this.pointTiemNangLvl)
        {
            this.tiemnang_point += Template.gymUpLevel;
        }
        else
        {
            this.tiemnang_point += this.pointTiemNangLvl;
        }

        if (this.lvl == 3 || this.lvl == 5 || this.lvl == 10)
        {
            this.skillPoint++;
        }
    }

    public sbyte getNClassIcon()
    {
        switch (getPetTemplate().nclass)
        {
            case GopetManager.Fighter:
            case GopetManager.Archer:
                return 0;
            case GopetManager.Assassin:
            case GopetManager.Demon:
                return 1;

            case GopetManager.Angel:
            case GopetManager.Wizard:
                return 2;

        }
        return 99;
    }





    public String getNameWithoutStar(Player player)
    {
        if (name != null)
        {
            return name;
        }
        return getPetTemplate().getName(player);
    }

    public String getNameWithStar(Player player)
    {
        String name = getNameWithoutStar(player) + " ";
        for (int i = 0; i < star; i++)
        {
            name += "(sao)";
        }

        for (int i = 0; i < 5 - star; i++)
        {
            name += "(saoden)";
        }
        return name;
    }

    /**
     * Kiểm tra xem nó có phải là trang bị cho pet không
     *
     * @param item Trang bị dành cho pet
     * @return nếu không phải sẽ trả lại kết quá sai
     */
    public static bool canEuip(Item item)
    {
        if (item == null)
        {
            return false;
        }
        ItemTemplate itemTemplate = item.getTemp();
        switch (itemTemplate.getType())
        {
            case GopetManager.PET_EQUIP_ARMOUR:
            case GopetManager.PET_EQUIP_GLOVE:
            case GopetManager.PET_EQUIP_HAT:
            case GopetManager.PET_EQUIP_SHOE:
            case GopetManager.PET_EQUIP_WEAPON:
                return true;
        }
        return false;
    }



    /// <summary>
    /// Áp dụng chỉ số sao khi mặc đồ hoặc thay đổi
    /// </summary>
    /// <param name="player"></param>
    public void applyInfo(Player player)
    {
        this.atk = (this.getStr() * 30);
        this.def = (this.getAgi() * 20);
        this.maxHp = getHpViaPrice() + (this.getInt() * 50);
        this.maxMp = getMpViaPrice() + (this.getInt() * 50);
        IDictionary<int, int> ItemEquipType = new Dictionary<int, int>();
        foreach (var next in equip.ToArray())
        {
            Item it = player.controller.selectItemEquipByItemId(next);
            if (it == null)
            {
                equip.remove(next);
                continue;
            }
            if (it.petEuipId != petId)
            {
                equip.remove(next);
                it.petEuipId = -1;
                player.Popup(Utilities.Format("Hệ thống tự gỡ vật phẩm do phiên bản trước có lỗi, vui lòng đeo %s lại", it.getTemp().getName(player)));
                continue;
            }

            if (this.getAgi() < it.getTemp().getRequireAgi() || this.getStr() < it.getTemp().getRequireStr() || this.getInt() < it.getTemp().getRequireInt())
            {
                equip.remove(next);
                it.petEuipId = -1;
                player.Popup(Utilities.Format("Pet của bạn đã tự tháo trang bị %s do pet cảm thấy khó chịu vì không đủ chỉ số", it.getTemp().getName(player)));
                continue;
            }
            ItemEquipType[it.Template.type] = it.itemTemplateId;
            this.atk += it.getAtk();
            this.def += it.getDef();
            this.maxHp += it.getHp();
            this.maxMp += it.getMp();
        }

        foreach (PetTatto petTatto in tatto)
        {
            this.atk += petTatto.getAtk();
            this.def += petTatto.getDef();
            this.maxHp += petTatto.getHp();
            this.maxMp += petTatto.getMp();
        }

        JArrayList<Item> otherItems = new();
        Item skinItem = player.playerData.skin;
        if (skinItem != null)
        {
            otherItems.add(skinItem);
        }
        Item wingItem = player.playerData.wing;
        if (wingItem != null)
        {
            otherItems.add(wingItem);
        }


        var ach = player.controller.FindSeach(player.playerData.CurrentAchievementId);
        if (ach != null)
        {
            this.atk += ach.Template.Atk;
            this.def += ach.Template.Def;
            this.maxHp += ach.Template.Hp;
            this.maxMp += ach.Template.Mp;
        }

        /*List<AchievementTemplate> achievementTemplates = new List<AchievementTemplate>();

        foreach (var ach in player.playerData.achievements)
        {
            if (achievementTemplates.Contains(ach.Template))
            {
                continue;
            }
            achievementTemplates.Add(ach.Template);
            this.atk += ach.Template.Atk;
            this.def += ach.Template.Def;
            this.maxHp += ach.Template.Hp;
            this.maxMp += ach.Template.Mp;
        }*/

        /*ClanMember clanMember = player.controller.getClan();
        if (clanMember != null)
        {
            int valueBuff = clanMember.clan.Search(ItemInfo.Type.PERCENT_ALL_INFO).value;
            this.atk += (int)Utilities.GetValueFromPercent(this.atk, valueBuff / 100);
            this.def += (int)Utilities.GetValueFromPercent(this.def, valueBuff / 100);
            this.maxHp += (int)Utilities.GetValueFromPercent(this.maxHp, valueBuff / 100);
            this.maxMp += (int)Utilities.GetValueFromPercent(this.maxMp, valueBuff / 100);
        }*/
        foreach (Item it in otherItems)
        {
            this.atk += it.getAtk();
            this.def += it.getDef();
            this.maxHp += it.getHp();
            this.maxMp += it.getMp();
        }
        foreach (var it in otherItems)
        {
            if (it.Template.itemOption.Length > 0)
            {
                for (int i = 0; i < it.Template.itemOptionValue.Length; i++)
                {
                    switch (it.Template.itemOption[i])
                    {
                        case ItemInfo.OptionType.PERCENT_HP:
                            this.maxHp += (int)Utilities.GetValueFromPercent(this.maxHp, it.Template.itemOptionValue[i] / 100);
                            break;
                        case ItemInfo.OptionType.PERCENT_MP:
                            this.maxMp += (int)Utilities.GetValueFromPercent(this.maxMp, it.Template.itemOptionValue[i] / 100);
                            break;
                        case ItemInfo.OptionType.PERCENT_ATK:
                            this.atk += (int)Utilities.GetValueFromPercent(this.atk, it.Template.itemOptionValue[i] / 100);
                            break;
                        case ItemInfo.OptionType.PERCENT_DEF:
                            this.def += (int)Utilities.GetValueFromPercent(this.def, it.Template.itemOptionValue[i] / 100);
                            break;
                    }
                }
            }
        }
        this.HiddenStats.Clear();
        this.HiddenStats.AddRange(GopetManager.HiddentStatItemTemplates.Where(x =>
        (!x.IdWeapon.HasValue || (ItemEquipType.ContainsKey(GopetManager.PET_EQUIP_WEAPON) && ItemEquipType[GopetManager.PET_EQUIP_WEAPON] == x.IdWeapon)) &&
        (!x.IdHat.HasValue || (ItemEquipType.ContainsKey(GopetManager.PET_EQUIP_HAT) && ItemEquipType[GopetManager.PET_EQUIP_HAT] == x.IdHat)) &&
        (!x.IdArmour.HasValue || (ItemEquipType.ContainsKey(GopetManager.PET_EQUIP_ARMOUR) && ItemEquipType[GopetManager.PET_EQUIP_ARMOUR] == x.IdArmour)) &&
        (!x.IdShoe.HasValue || (ItemEquipType.ContainsKey(GopetManager.PET_EQUIP_SHOE) && ItemEquipType[GopetManager.PET_EQUIP_SHOE] == x.IdShoe)) &&
        (!x.IdGlove.HasValue || (ItemEquipType.ContainsKey(GopetManager.PET_EQUIP_GLOVE) && ItemEquipType[GopetManager.PET_EQUIP_GLOVE] == x.IdGlove)))
        .Select(m => m.Id));

        player.controller.checkExpire();
        player.controller.sendMyPetInfo();
    }

    public HiddenStatItemTemplate[] TakeAllHiddenStat() => GopetManager.HiddentStatItemTemplates.Where(x => this.HiddenStats.Contains(x.Id)).ToArray();

    public int getSkillIndex(int skillId)
    {
        for (int i = 0; i < skill.Length; i++)
        {
            int[] is2 = skill[i];
            if (is2[0] == skillId)
            {
                return i;
            }
        }
        return -1;
    }

    public void subExpPK(long expSub)
    {
        if (this.exp - expSub > GopetManager.MIN_PET_EXP_PK)
        {
            this.exp -= expSub;
        }
        else
        {
            this.exp = GopetManager.MIN_PET_EXP_PK;
        }
    }

    public PetTatto selectTattoById(int tattooId)
    {
        return tatto.BinarySearch(tattooId);
    }

    public void addTatto(PetTatto petTatto)
    {
        tatto.Add(petTatto);
        tatto.BinaryObjectAdd(petTatto);
        tatto.Sort(new BinaryCompare<PetTatto>());
    }

    public String getDesc(Player player)
    {
        JArrayList<String> infoStrings = new();
        infoStrings.add(Utilities.FormatNumber(exp) + " exp ");

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

        JArrayList<String> tattooStrings = new();

        bool flag = false;
        foreach (PetTatto petTatto in tatto)
        {
            if (!flag)
            {
                tattooStrings.add(". Xăm: " + petTatto.getName(player));
                flag = true;
                continue;
            }
            tattooStrings.add(petTatto.getName(player));
        }
        String desc = Utilities.Format("(str) %s (int) %s (agi) %s", getStr(), getInt(), getAgi());

        return desc + Utilities.Format("  lvl: %s , ", lvl) + String.Join(" , ", infoStrings) + String.Join(" , ", tattooStrings);
    }

    public int GetId()
    {
        return this.petId;
    }

    public void SetId(int id)
    {
        this.petId = id;
    }

    public IEnumerable<PetEffectTemplate> EffectTemplates
    {
        get
        {
            return new List<PetEffectTemplate>()
            {
            /*new PetEffectTemplate ()
            {
                FramePath = "peteff/output2.png",
                FrameNum = 6,
                IsDrawBefore = true,
                FrameTime = 80,
                vY = -100
            }*/
            };
        }
    }
}

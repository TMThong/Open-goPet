
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Util;

public class Pet : GameObject
{


    public int petIdTemplate;

    public bool petDieByPK = false;

    public int petId;
    public int star = 0;

    public int lvl = 1;

    public long exp = 0;


    public String name = null;
    public int str, agi, _int;

    /**
     * Điểm tiềm năng
     */

    public int tiemnang_point = 0;

    /**
     * Điểm kỹ năng , dùng để học kỹ năng
     */

    public int skillPoint = 0;

    /**
     * Dữ liệu về skill skill[*][0] là skillId skill[*][1] là cấp của kỹ năng
     */

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

    public ArrayList<int> equip = new();

    public bool isUpTier = false;

    public int pointTiemNangLvl = 3;


    public bool wasSell = false;

    public Pet(int petIdTemplate)
    {
        this.petIdTemplate = petIdTemplate;
        maxHp = getPetTemplate().getHp();
        hp = maxHp;
        maxMp = getPetTemplate().getMp();
        mp = maxMp;
        agi = getPetTemplate().getAgi();
        str = getPetTemplate().getStr();
        _int = getPetTemplate().getInt();
    }

    public PetTemplate getPetTemplate()
    {
        return GopetManager.PETTEMPLATE_HASH_MAP.get(petIdTemplate);
    }

    public int getPetIdTemplate()
    {
        return petIdTemplate;
    }

    public PetTemplate Template
    {
        get
        {
            return GopetManager.PETTEMPLATE_HASH_MAP[this.petIdTemplate];
        }
    }

    public int getAgi()
    {
        return agi + tiemnang[1];
    }

    public int getInt()
    {
        return _int + tiemnang[2];
    }

    public int getStr()
    {
        return str + tiemnang[0];
    }

    public void addExp(int exp)
    {
        this.exp += exp;
    }

    public void addHp(int i)
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
        this.skill[oldSkillList.Length - 1] = new int[2];
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

        this.tiemnang_point += pointTiemNangLvl;

        if (this.lvl == 3 || this.lvl == 5 || this.lvl == 10)
        {
            this.skillPoint++;
        }
    }

    public sbyte getNClassIcon()
    {
        switch (getPetTemplate().getNclass())
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

    public int getAtk()
    {
        switch (Template.nclass)
        {
            case GopetManager.Archer:
            case GopetManager.Fighter:
                return atk + (getStr() / 3) + 5;
            case GopetManager.Demon:
            case GopetManager.Assassin:
                return atk + (getAgi() / 3) + 5;
            case GopetManager.Angel:
            case GopetManager.Wizard:
                return atk + (getInt() / 3) + 5;
        }

        return atk + Utilities.round(str / 2) + (tiemnang[0] / 2);
    }

    public int getDef()
    {
        return def + getAgi() / 3;
    }

    public String getNameWithoutStar()
    {
        if (name != null)
        {
            return name;
        }
        return getPetTemplate().getName();
    }

    public String getNameWithStar()
    {
        String name = getNameWithoutStar() + " ";
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

    public int getHpViaPrice()
    {
        return lvl * 3 + getStr() * 4 + 20;
    }

    public int getMpViaPrice()
    {
        return lvl * 3 + getInt() * 5 + 20;
    }
    public virtual float SkipPercent
    {
        get
        {
            return 15 + getAgi() / 1000;
        }
    }

    public virtual float AccuracyPercent
    {
        get
        {
            return 100 + getAgi() / 1000;
        }
    }

    /**
     * Áp dụng chỉ số sao khi mặc đồ hoặc thay đổi
     */
    public void applyInfo(Player player)
    {
        this.atk = 0;
        this.def = 0;
        //        this.maxHp = 0;   
        //        this.maxMp = 0;
        this.maxHp = getHpViaPrice();
        this.maxMp = getMpViaPrice();
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
                player.Popup(Utilities.Format("Hệ thống tự gỡ vật phẩm do phiên bản trước có lỗi, vui lòng đeo %s lại", it.getTemp().getName()));
                continue;
            }

            if (this.getAgi() < it.getTemp().getRequireAgi() || this.getStr() < it.getTemp().getRequireStr() || this.getInt() < it.getTemp().getRequireInt())
            {
                equip.remove(next);
                it.petEuipId = -1;
                player.Popup(Utilities.Format("Pet của bạn đã tự tháo trang bị %s do pet cảm thấy khó chịu vì không đủ chỉ số", it.getTemp().getName()));
                continue;
            }

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

        ArrayList<Item> otherItems = new();
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
        foreach (Item it in otherItems)
        {
            this.atk += it.getAtk();
            this.def += it.getDef();
            this.maxHp += it.getHp();
            this.maxMp += it.getMp();
        }
        player.controller.checkExpire();
        player.controller.sendMyPetInfo();

    }



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
        int left = 0;
        int right = tatto.Count - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            PetTatto midTattoo = tatto.get(mid);
            if (midTattoo.tattoId == tattooId)
            {
                return midTattoo;
            }
            if (midTattoo.tattoId < tattooId)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return null;
    }

    sealed class PetTattoComparer : IComparer<PetTatto>
    {
        public int Compare(PetTatto? obj1, PetTatto? obj2)
        {
            return obj1.tattoId - obj2.tattoId;
        }
    }


    public void addTatto(PetTatto petTatto)
    {
        tatto.add(petTatto);
        while (true)
        {
            petTatto.tattoId = Utilities.nextInt(1, int.MaxValue - 2);
            bool flag = true;
            foreach (PetTatto item1 in tatto)
            {
                if (item1 != petTatto)
                {
                    if (item1.tattoId == petTatto.tattoId)
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
        tatto.Sort(new PetTattoComparer());
    }

    public String getDesc()
    {
        ArrayList<String> infoStrings = new();
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

        ArrayList<String> tattooStrings = new();

        bool flag = false;
        foreach (PetTatto petTatto in tatto)
        {
            if (!flag)
            {
                tattooStrings.add(". Xăm: " + petTatto.getName());
                flag = true;
                continue;
            }
            tattooStrings.add(petTatto.getName());
        }
        String desc = Utilities.Format("(str) %s (int) %s (agi) %s", getStr(), getInt(), getAgi());

        return desc + Utilities.Format("  lvl: %s , ", lvl) + String.Join(" , ", infoStrings) + String.Join(" , ", tattooStrings);
    }


}

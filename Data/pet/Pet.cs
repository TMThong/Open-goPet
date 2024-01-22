/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.pet;

import base.GameObject;
import com.google.gson.annotations.SerializedName;
import data.item.Item;
import data.item.ItemAttributeTemplate;
import data.item.ItemTemplate;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Iterator;
import java.util.concurrent.CopyOnWriteArrayList;
import manager.GopetManager;
import server.Player;
import util.Utilities;

/**
 *
 * @author MINH THONG
 */
public class Pet extends GameObject {

    @SerializedName("petIdTemplate")
    public int petIdTemplate;
    @SerializedName("petDieByPK")
    public bool petDieByPK = false;
    @SerializedName("petId")
    public int petId;
    public int star = 0;
    @SerializedName("lvl")
    public int lvl = 1;
    @SerializedName("exp")
    public long exp = 0;

    @SerializedName("name")
    public String name = null;
    public int str, agi, _int;

    /**
     * Điểm tiềm năng
     */
    @SerializedName("tiemnang_point")
    public int tiemnang_point = 0;

    /**
     * Điểm kỹ năng , dùng để học kỹ năng
     */
    @SerializedName("skillPoint")
    public int skillPoint = 0;

    /**
     * Dữ liệu về skill skill[*][0] là skillId skill[*][1] là cấp của kỹ năng
     */
    @SerializedName("skill")
    public int[][] skill = new int[0][0];

    /**
     * Này là điểm tiềm năng Dùng trong luyện chỉ số Giúp tăng chỉ số
     * str,agi,int tiemnang[0] là str tiemnang[1] là agi tiemnang[2] là int
     */
    @SerializedName("tiemnang")
    public int[] tiemnang = new int[]{0, 0, 0};

    /**
     * Hình xăm của pet chỉ thêm trong giao diện xăm hình
     */
    @SerializedName("tatto")
    public CopyOnWriteArrayList<PetTatto> tatto = new CopyOnWriteArrayList<>();
    /**
     * Các vật phẩm mà pet đã trang bị
     */
    @SerializedName("equip")
    public ArrayList<Integer> equip = new ArrayList<>();
    @SerializedName("isUpTier")
    public bool isUpTier = false;
    @SerializedName("pointTiemNangLvl")
    public int pointTiemNangLvl = 3;

    @SerializedName("wasSell")
    public bool wasSell = false;

    private transient int attrId = -1;

    public Pet(int petIdTemplate) {
        this.petIdTemplate = petIdTemplate;
        maxHp = getPetTemplate().getHp();
        hp = maxHp;
        maxMp = getPetTemplate().getMp();
        mp = maxMp;
        agi = getPetTemplate().getAgi();
        str = getPetTemplate().getStr();
        _int = getPetTemplate().getInt();
    }

    public PetTemplate getPetTemplate() {
        return GopetManager.PETTEMPLATE_HASH_MAP.get(petIdTemplate);
    }

    public int getPetIdTemplate() {
        return petIdTemplate;
    }

    public int getAgi() {
        return agi + tiemnang[1];
    }

    public int getInt() {
        return _int + tiemnang[2];
    }

    public int getStr() {
        return str + tiemnang[0];
    }

    public void addExp(int exp) {
        this.exp += exp;
    }

    public void addHp(int i) {
        if (hp + i > maxHp) {
            hp = maxHp;
        } else {
            hp += i;
        }
    }

    public void subHp(int i) {
        if (hp - i <= 0) {
            hp = 0;
        } else {
            hp -= i;
        }
    }

    public void addMp(int i) {
        if (mp + i > maxMp) {
            mp = maxMp;
        } else {
            mp += i;
        }
    }

    public void addSkill(int skill, int skillLv) {
        int[][] oldSkillList = this.skill;
        this.skill = new int[oldSkillList.length + 1][2];
        for (int i = 0; i < oldSkillList.length; i++) {
            int[] is = oldSkillList[i];
            this.skill[i] = is;
        }
        this.skill[oldSkillList.length] = new int[]{skill, skillLv};
    }

    public PetSkill[] getSkills() {
        PetSkill[] petSkills = new PetSkill[skill.length];
        for (int i = 0; i < skill.length; i++) {
            int[] skillInfo = skill[i];
            petSkills[i] = GopetManager.PETSKILL_HASH_MAP.get(skillInfo[0]);
        }
        return petSkills;
    }

    public void lvlUP() {
        this.lvl++;

        this.tiemnang_point += pointTiemNangLvl;

        if (this.lvl == 3 || this.lvl == 5 || this.lvl == 10) {
            this.skillPoint++;
        }
    }

    public byte getNClassIcon() {
        switch (getPetTemplate().getNclass()) {
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

    public int getAtk() {
        return atk + Math.round(str / 2) + (tiemnang[0] / 2);
    }

    public int getDef() {
        return def + tiemnang[1];
    }

    public String getNameWithoutStar() {
        if (name != null) {
            return name;
        }
        return getPetTemplate().getName();
    }

    public String getNameWithStar() {
        String name = getNameWithoutStar() + " ";
        for (int i = 0; i < star; i++) {
            name += "(sao)";
        }

        for (int i = 0; i < 5 - star; i++) {
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
    public static bool canEuip(Item item) {
        if (item == null) {
            return false;
        }
        ItemTemplate itemTemplate = item.getTemp();
        switch (itemTemplate.getType()) {
            case GopetManager.PET_EQUIP_ARMOUR:
            case GopetManager.PET_EQUIP_GLOVE:
            case GopetManager.PET_EQUIP_HAT:
            case GopetManager.PET_EQUIP_SHOE:
            case GopetManager.PET_EQUIP_WEAPON:
                return true;
        }
        return false;
    }

    /**
     * Áp dụng chỉ số sao khi mặc đồ hoặc thay đổi
     */
    public void applyInfo(Player player)   {
        updateAttr(player);
        this.atk = 0;
        this.def = 0;
//        this.maxHp = 0;   
//        this.maxMp = 0;
        this.maxHp = getPetTemplate().getHp() + (tiemnang[2] * 10);
        this.maxMp = getPetTemplate().getMp() + (tiemnang[2] * 10);
        for (Iterator<Integer> iterator = equip.iterator(); iterator.hasNext();) {
            Integer next = iterator.next();
            Item it = player.controller.selectItemEquipByItemId(next);
            if (it == null) {
                iterator.remove();
                continue;
            }
            if (it.petEuipId != petId) {
                iterator.remove();
                it.petEuipId = -1;
                player.Popup(String.format("Hệ thống tự gỡ vật phẩm do phiên bản trước có lỗi, vui lòng đeo %s lại", it.getTemp().getName()));
                continue;
            }

            if (this.getAgi() < it.getTemp().getRequireAgi() || this.getStr() < it.getTemp().getRequireStr() || this.getInt() < it.getTemp().getRequireInt()) {
                iterator.remove();
                it.petEuipId = -1;
                player.Popup(String.format("Pet của bạn đã tự tháo trang bị %s do pet cảm thấy khó chịu vì không đủ chỉ số", it.getTemp().getName()));
                continue;
            }

            this.atk += it.getAtk();
            this.def += it.getDef();
            this.maxHp += it.getHp();
            this.maxMp += it.getMp();
        }

        for (PetTatto petTatto : tatto) {
            this.atk += petTatto.getAtk();
            this.def += petTatto.getDef();
            this.maxHp += petTatto.getHp();
            this.maxMp += petTatto.getMp();
        }

        ArrayList<Item> otherItems = new ArrayList<>();
        Item skinItem = player.playerData.skinItem;
        if (skinItem != null) {
            otherItems.add(skinItem);
        }
        Item wingItem = player.playerData.wingItem;
        if (wingItem != null) {
            otherItems.add(wingItem);
        }
        for (Item it : otherItems) {
            this.atk += it.getAtk();
            this.def += it.getDef();
            this.maxHp += it.getHp();
            this.maxMp += it.getMp();
        }
        player.controller.checkExpire();
        player.controller.sendMyPetInfo();
        ItemAttributeTemplate attributeTemplate = getAttributeTemplate();
        if (attributeTemplate != null) {
            this.hp += attributeTemplate.findValueById(4);
            this.mp += attributeTemplate.findValueById(5);
            this.atk += attributeTemplate.findValueById(34);
        }
    }

    public ItemAttributeTemplate getAttributeTemplate() {
        return GopetManager.ITEM_ATTRIBUTE_TEMPLATE_HASH_MAP.get(attrId);
    }

    public int getSkillIndex(int skillId) {
        for (int i = 0; i < skill.length; i++) {
            int[] is = skill[i];
            if (is[0] == skillId) {
                return i;
            }
        }
        return -1;
    }

    public void subExpPK(long expSub) {
        if (this.exp - expSub > GopetManager.MIN_PET_EXP_PK) {
            this.exp -= expSub;
        } else {
            this.exp = GopetManager.MIN_PET_EXP_PK;
        }
    }

    public PetTatto selectTattoById(int tattooId)   {
        int left = 0;
        int right = tatto.size() - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            PetTatto midTattoo = tatto.get(mid);
            if (midTattoo.tattoId == tattooId) {
                return midTattoo;
            }
            if (midTattoo.tattoId < tattooId) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        return null;
    }

    public void addTatto(PetTatto petTatto) {
        tatto.add(petTatto);
        while (true) {
            petTatto.tattoId = Utilities.nextInt(1, Integer.MAX_VALUE - 2);
            bool flag = true;
            for (PetTatto item1 : tatto) {
                if (item1 != petTatto) {
                    if (item1.tattoId == petTatto.tattoId) {
                        flag = false;
                    }
                }
            }
            if (flag) {
                break;
            }
        }
        tatto.sort(new Comparator<PetTatto>() {
            @Override
            public int compare(PetTatto obj1, PetTatto obj2) {
                return obj1.tattoId - obj2.tattoId;
            }
        });
    }

    public String getDesc() {
        ArrayList<String> infoStrings = new ArrayList<>();
        infoStrings.add(Utilities.formatNumber(exp) + " exp ");

        if (getAtk() > 0) {
            infoStrings.add(getAtk() + " (atk) ");
        }
        if (getDef() > 0) {
            infoStrings.add(getDef() + " (def) ");
        }
        if (getHp() > 0) {
            infoStrings.add(getHp() + " (hp) ");
        }
        if (getMp() > 0) {
            infoStrings.add(getMp() + " (mp) ");
        }

        ArrayList<String> tattooStrings = new ArrayList<>();

        bool flag = false;
        for (PetTatto petTatto : tatto) {
            if (!flag) {
                tattooStrings.add(". Xăm: " + petTatto.getName());
                flag = true;
                continue;
            }
            tattooStrings.add(petTatto.getName());
        }
        String desc = String.format("(str) %s (int) %s (agi) %s", getStr(), getInt(), getAgi());

        return desc + String.format("  lvl: %s , ", lvl) + String.join(" , ", infoStrings) + String.join(" , ", tattooStrings);
    }

    private void updateAttr(Player player)   {
        for (int i = 0; i < GopetManager.ITEM_ATTRIBUTE_TEMPLATES.size(); i++) {
            ItemAttributeTemplate itemAttributeTemplate = GopetManager.ITEM_ATTRIBUTE_TEMPLATES.get(i);
            bool[] flag = new bool[itemAttributeTemplate.getListItemId().length];
            Arrays.fill(flag, false);
            for (int j : itemAttributeTemplate.getListItemId()) {
                if (equip.contains(j)) {
                    flag[i] = true;
                }
            }
            
            bool fl = true;
            
            for (bool b : flag) {
                if (b == false) {
                    fl = false;
                    break;
                }
            }
            
            if (!fl) {
                break;
            }

            this.attrId = itemAttributeTemplate.getAttrId();
            return;
        }
    }
}

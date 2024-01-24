
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;

public class PetSkill {

    public const int TOXIC = 107;
    public const int PHANDOAN = 110;

    public int skillID;
    public sbyte nClass;
    public String name, description;
    public ArrayList<PetSkillLv> skillLv = new ArrayList<PetSkillLv>();

    public String getDescription(PetSkillLv petSkillLv) {
        return String.Join("\n", ItemInfo.getName(petSkillLv.skillInfo));
    }

    /**
     * Có là kỹ năng buff hay không? So sánh bằng cách lấy skillId xem nó trong
     * ~ id skill buff Xem ở bản skill hoặc hình ảnh để biết những id này
     *
     * @return
     */
    public bool isSkillBuff() {
        return skillID == 103 || skillID == 109 || skillID == 115;
    }
}

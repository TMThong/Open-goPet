
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;

public class PetSkill {

    private const int TOXIC = 107;
    private const int TOXIC_SKY = 119;
    public const int PHANDOAN = 110;
    private const int PHANDON_SKY = 118;

    public int skillID;
    public sbyte nClass;
    public String name, description;
    public JArrayList<PetSkillLv> skillLv = new JArrayList<PetSkillLv>();
    public bool IsNeedCard {  get; set; } = false;

    public String getDescription(PetSkillLv petSkillLv, Player player) {
        return String.Join("\n", ItemInfo.getName(petSkillLv.skillInfo, player));
    }

    /**
     * Có là kỹ năng buff hay không? So sánh bằng cách lấy skillId xem nó trong
     * ~ id skill buff Xem ở bản skill hoặc hình ảnh để biết những id này
     *
     * @return
     */
    public bool isSkillBuff() {
        return skillID == 103 || skillID == 109 || skillID == 115 || skillID == 117 || skillID == 127;
    }

    public static int GetToxicSkill(GameObject gameObject)
    {
        if(gameObject.Template.nclass <= 2) return TOXIC; 
        else return TOXIC_SKY;
    }

    public static int GetTPhanDonSkill(GameObject gameObject)
    {
        if (gameObject.Template.nclass <= 2) return PHANDOAN;
        else return PHANDON_SKY;
    }

    public string getName(Player player) 
    { 
        return player.Language.SkillNameLanguage[this.skillID];
    }
    public string getDescription(Player player)
    {
        return player.Language.SkillDescLanguage[this.skillID];
    }
}

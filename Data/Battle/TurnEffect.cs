namespace Gopet.Battle
{
    public class TurnEffect
    {

        public sbyte type { get; }
        public int petId;
        public int skillId;
        public int hp;
        public int mp;

        public TurnEffect(sbyte type, int petId, int skillId, int hp, int mp)
        {
            this.type = type;
            this.petId = petId;
            this.skillId = skillId;
            this.hp = hp;
            this.mp = mp;
        }

        public const sbyte TYPE_EFFECT_NORMAL = 1;
        public const sbyte TYPE_EFFECT_WAIT = 4;
        public const sbyte SKILL_NORMAL = 0;
        public const sbyte SKILL_MISS = 1;
        public const sbyte SKILL_CRIT = 2;
        public const sbyte NONE = 0;

        public static TurnEffect createNormalAttack(int mp, int hp, int petId)
        {
            return new TurnEffect(TYPE_EFFECT_NORMAL, petId, 0, hp, mp);
        }

        public static TurnEffect createWait(int mp, int petId)
        {
            return new TurnEffect(TYPE_EFFECT_WAIT, petId, 0, 0, mp);
        }

        public static TurnEffect createWait(int hp, int mp, int petId)
        {
            return new TurnEffect(TYPE_EFFECT_WAIT, petId, 0, hp, mp);
        }
    }
}
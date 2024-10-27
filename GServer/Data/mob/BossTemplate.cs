namespace Gopet.Data.Mob
{
    public class BossTemplate
    {
        public int bossId { get; private set; }
        public string name { get; private set; }
        public int[][] gift { get; private set; }
        public sbyte typeBoss { get; private set; }
        public int petTemplateId { get; private set; }
        public int lvl { get; private set; }
        public int str { get; private set; }
        public int agi { get; private set; }
        public int _int { get; private set; }
        public int exp { get; private set; }
        public int[] HourSummon { get; private set; }
        public int[] BossMapSummon { get; private set; }

        public string getName(Player player)
        {
            return player.Language.BossNameLanguage[this.bossId];
        }
    }
}
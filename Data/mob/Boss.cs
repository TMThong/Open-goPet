namespace Gopet.Data.Mob
{
    public class Boss : Mob
    {

        private BossTemplate bossTemplate;

        public bool isTimeOut = false;

        public long timeoutMilis = 0L;

        public Boss(int bossTemplateId, MobLocation mobLocation)
        {
            bossTemplate = GopetManager.boss.get(bossTemplateId);
            this.petIdTemplate = Template.petTemplateId;
            this.setMobLocation(mobLocation);
            this.setMobLvInfo(new MobLvInfoImp(bossTemplate));
            initMob();
        }

        sealed class MobLvInfoImp : MobLvInfo
        {
            public MobLvInfoImp(BossTemplate bossTemplate)
            {
                this.bossTemplate = bossTemplate;
                this.agi = this.bossTemplate.agi;
                this.lvl = this.bossTemplate.lvl;
                this.exp = this.bossTemplate.exp;
                this._int = this.bossTemplate._int;
                this.str = this.bossTemplate.str;
            }



            public BossTemplate bossTemplate { get; }
        }



        internal BossTemplate Template
        {
            get { return bossTemplate; }
        }
    }
}
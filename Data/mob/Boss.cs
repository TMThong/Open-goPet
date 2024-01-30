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
            this.petIdTemplate = getBossTemplate().petTemplateId;
            this.setMobLocation(mobLocation);
            this.setMobLvInfo(new MobLvInfoImp(bossTemplate));
            initMob();
        }

        sealed class MobLvInfoImp : MobLvInfo
        {
            public MobLvInfoImp(BossTemplate bossTemplate)
            {
                this.bossTemplate = bossTemplate;
            }

            public BossTemplate bossTemplate { get; }

             
        }
         


        public string getName()
        {
            return bossTemplate.name;
        }

        internal BossTemplate getBossTemplate()
        {
            return bossTemplate;
        }
    }
}
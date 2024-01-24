namespace Gopet.Data.Mob
{
    public class BossTemplate : GameObject
    {

        private int bossId;
        private string name;
        private int[][] gift;
        private sbyte typeBoss;
        private PetTemplate petTemplate;
        private int lvl;

        public void setBossId(int bossId)
        {
            this.bossId = bossId;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setGift(int[][] gift)
        {
            this.gift = gift;
        }

        public void setTypeBoss(sbyte typeBoss)
        {
            this.typeBoss = typeBoss;
        }

        public void setPetTemplate(PetTemplate petTemplate)
        {
            this.petTemplate = petTemplate;
        }

        public void setLvl(int lvl)
        {
            this.lvl = lvl;
        }

        public int getBossId()
        {
            return bossId;
        }

        public string getName()
        {
            return name;
        }

        public int[][] getGift()
        {
            return gift;
        }

        public sbyte getTypeBoss()
        {
            return typeBoss;
        }

        public PetTemplate getPetTemplate()
        {
            return petTemplate;
        }

        public int getLvl()
        {
            return lvl;
        }

    }
}
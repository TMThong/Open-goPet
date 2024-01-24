namespace Gopet.Data.GopetClan
{
    public class ClanBuffTemplate
    {
        private int buffId;
        private int valuePerLevel;
        private bool isPercent;
        private string comment;
        private int potentialPointNeed;
        private int lvlClan;
        private string name;
        private string desc;

        public void setBuffId(int buffId)
        {
            this.buffId = buffId;
        }

        public void setValuePerLevel(int valuePerLevel)
        {
            this.valuePerLevel = valuePerLevel;
        }

        public void setPercent(bool isPercent_)
        {
            isPercent = isPercent_;
        }

        public void setComment(string comment)
        {
            this.comment = comment;
        }

        public void setPotentialPointNeed(int potentialPointNeed)
        {
            this.potentialPointNeed = potentialPointNeed;
        }

        public void setLvlClan(int lvlClan)
        {
            this.lvlClan = lvlClan;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setDesc(string desc)
        {
            this.desc = desc;
        }

        public int getBuffId()
        {
            return buffId;
        }

        public int getValuePerLevel()
        {
            return valuePerLevel;
        }



        public string getComment()
        {
            return comment;
        }

        public int getPotentialPointNeed()
        {
            return potentialPointNeed;
        }

        public int getLvlClan()
        {
            return lvlClan;
        }

        public string getName()
        {
            return name;
        }

        public string getDesc()
        {
            return desc;
        }

    }
}
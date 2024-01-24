namespace Gopet.Data.GopetItem
{
    public class DropItem
    {

        private int mapId;
        private int dropId;
        private int itemTemplateId;
        private float percent;
        private int[] lvlRange;
        private int count;

        public void setMapId(int mapId)
        {
            this.mapId = mapId;
        }

        public void setDropId(int dropId)
        {
            this.dropId = dropId;
        }

        public void setItemTemplateId(int itemTemplateId)
        {
            this.itemTemplateId = itemTemplateId;
        }

        public void setPercent(float percent)
        {
            this.percent = percent;
        }

        public void setLvlRange(int[] lvlRange)
        {
            this.lvlRange = lvlRange;
        }

        public void setCount(int count)
        {
            this.count = count;
        }

        public int getMapId()
        {
            return mapId;
        }

        public int getDropId()
        {
            return dropId;
        }

        public int getItemTemplateId()
        {
            return itemTemplateId;
        }

        public float getPercent()
        {
            return percent;
        }

        public int[] getLvlRange()
        {
            return lvlRange;
        }

        public int getCount()
        {
            return count;
        }

    }
}
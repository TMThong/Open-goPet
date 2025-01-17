namespace Gopet.Data.GopetItem
{
    public class DropItem
    {

        public int mapId;
        public int dropId;
        public int itemTemplateId;
        public float percent;
        public int[] lvlRange;
        public int count;

        public DropItem()
        {
             
        }

        public DropItem(int mapId, int dropId, int itemTemplateId, float percent, int[] lvlRange, int count)
        {
            this.mapId = mapId;
            this.dropId = dropId;
            this.itemTemplateId = itemTemplateId;
            this.percent = percent;
            this.lvlRange = lvlRange;
            this.count = count;
        }

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
namespace Gopet.Data.Mob
{
    public class MobLvlMap
    {

        private int mapId, lvlFrom, lvlTo, petId;

        public int getPetId()
        {
            return petId;
        }

        public MobLvlMap(int mapId, int lvlFrom, int lvlTo, int petId)
        {
            this.mapId = mapId;
            this.lvlFrom = lvlFrom;
            this.lvlTo = lvlTo;
            this.petId = petId;
        }

        public MobLvlMap()
        {
        }

        public int getMapId()
        {
            return mapId;
        }

        public int getLvlFrom()
        {
            return lvlFrom;
        }

        public int getLvlTo()
        {
            return lvlTo;
        }

        public void setMapId(int mapId)
        {
            this.mapId = mapId;
        }

        public void setLvlFrom(int lvlFrom)
        {
            this.lvlFrom = lvlFrom;
        }

        public void setLvlTo(int lvlTo)
        {
            this.lvlTo = lvlTo;
        }
    }
}
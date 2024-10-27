namespace Gopet.Data.Mob
{
    public class MobLocation
    {

        private int mapId, x, y;

        public MobLocation()
        {
        }

        public MobLocation(int mapId, int x, int y)
        {
            this.mapId = mapId;
            this.x = x;
            this.y = y;
        }

        public int getMapId()
        {
            return mapId;
        }

        public void setMapId(int mapId)
        {
            this.mapId = mapId;
        }

        public int getX()
        {
            return x;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public int getY()
        {
            return y;
        }

        public void setY(int y)
        {
            this.y = y;
        }

    }
}
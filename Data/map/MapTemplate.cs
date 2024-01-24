namespace Gopet.Data.Map
{
    public class MapTemplate
    {
        public int mapId;
        public string mapName;
        public int[] npc;
        public Waypoint[] waypoints;
        public int[] boss;

        public void setMapId(int mapId)
        {
            this.mapId = mapId;
        }

        public void setMapName(string mapName)
        {
            this.mapName = mapName;
        }

        public void setNpc(int[] npc)
        {
            this.npc = npc;
        }

        public void setWaypoints(Waypoint[] waypoints)
        {
            this.waypoints = waypoints;
        }

        public void setBoss(int[] boss)
        {
            this.boss = boss;
        }

        public int getMapId()
        {
            return mapId;
        }

        public string getMapName()
        {
            return mapName;
        }

        public int[] getNpc()
        {
            return npc;
        }

        public Waypoint[] getWaypoints()
        {
            return waypoints;
        }

        public int[] getBoss()
        {
            return boss;
        }

    }
}
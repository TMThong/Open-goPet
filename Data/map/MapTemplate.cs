
public class MapTemplate
{
    public int mapId;
    public String mapName;
    public int[] npc;
    public Waypoint[] waypoints;
    public int[] boss;

    public void setMapId(int mapId)
    {
        this.mapId = mapId;
    }

    public void setMapName(String mapName)
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
        return this.mapId;
    }

    public String getMapName()
    {
        return this.mapName;
    }

    public int[] getNpc()
    {
        return this.npc;
    }

    public Waypoint[] getWaypoints()
    {
        return this.waypoints;
    }

    public int[] getBoss()
    {
        return this.boss;
    }

}

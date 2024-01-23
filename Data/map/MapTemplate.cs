
public class MapTemplate
{
    private int mapId;
    private String mapName;
    private int[] npc;
    private Waypoint[] waypoints;
    private int[] boss;

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

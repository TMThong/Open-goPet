

using Gopet.Data.Collections;
using Gopet.Data.Map;

public class MapManager
{

    public static HashMap<int, GopetMap> maps = new();

    public static ArrayList<GopetMap> mapArr = new();

    public const int ID_LINH_THU_CITY = 11;

    public static void init()
    {
        mapArr.Clear();
        foreach (var entry in GopetManager.mapTemplate)
        {
            int mapid = entry.Key;
            MapTemplate mapTemplate = entry.Value;

            switch (mapid)
            {
                case 12:
                    put(mapid, new ChallengeMap(mapid, true, mapTemplate));
                    break;
                case 22:
                    put(mapid, new MarketMap(mapid, true, mapTemplate));
                    break;
                case 30:
                    put(mapid, new ClanMap(mapid, true, mapTemplate));
                    break;
                default:
                    put(mapid, new GopetMap(mapid, true, mapTemplate));
                    break;
            }
        }
    }

    public static void put(int mapID, GopetMap m)
    {
        maps.put(mapID, m);
        mapArr.add(m);
    }

    public static void stopUpdate()
    {
        foreach (var entry in maps)
        {
            int key = entry.Key;
            GopetMap value = entry.Value;
            value.isRunning = false;
        }
    }
}

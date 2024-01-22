package manager;

import data.map.ChallengeMap;
import data.map.ClanMap;
import data.map.GopetMap;
import data.map.MapTemplate;
import data.map.MarketMap;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class MapManager {

    public static HashMap<Integer, GopetMap> maps = new HashMap();

    public static ArrayList<GopetMap> mapArr = new ArrayList<>();
    
    public const int ID_LINH_THU_CITY = 11;

    public static void init()   {
        mapArr.clear();
        for (Map.Entry<Integer, MapTemplate> entry : GopetManager.mapTemplate.entrySet()) {
            Integer mapid = entry.getKey();
            MapTemplate mapTemplate = entry.getValue();

            switch (mapid) {
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

    public static void put(Integer mapID, GopetMap m) {
        maps.put(mapID, m);
        mapArr.add(m);
    }

    public static void stopUpdate() {
        for (Map.Entry<Integer, GopetMap> entry : maps.entrySet()) {
            Integer key = entry.getKey();
            GopetMap value = entry.getValue();
            value.isRunning = false;
        }
    }
}

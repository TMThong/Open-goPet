
using Gopet.Data.Clan;
using Gopet.Util;
public class ClanMap : GopetMap {
    public ClanMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) : base(mapId_, canUpdate, mapTemplate)
    {
    }

    public override void addPlace(Place place) {
        throw new UnsupportedOperationException("Place dua vao clan");
    }

    
    public override void addRandom(Player player)   {
        player.redDialog("Map này cần phải có bang hội mới vào được");
    }

     
    public override void update()   {
        try {
            foreach (Clan clan in ClanManager.clans) {
                clan.update();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    
    public override void createZoneDefault() {

    }
}

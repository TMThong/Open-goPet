 
public class ChallengeMap : GopetMap {

    public ChallengeMap(int mapId_, bool canUpdate, MapTemplate mapTemplate)  : base(mapId_, canUpdate, mapTemplate)
    {
        
    }

     
    public override void addRandom(Player player)   {
         foreach (Place place in places) {
            if (place.canAdd(player)) {
                place.add(player);
                return;
            }
        }
        Place place = new ChallengePlace(this, places.Count);
        place.add(player);
        addPlace(place);
    }

     
    public override void createZoneDefault() {

    }
}

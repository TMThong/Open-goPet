 
public class ChallengeMap extends GopetMap {

    public ChallengeMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) {
        base(mapId_, canUpdate, mapTemplate);
    }

    @Override
    public void addRandom(Player player)   {
         for (Place place : places) {
            if (place.canAdd(player)) {
                place.add(player);
                return;
            }
        }
        Place place = new ChallengePlace(this, places.size());
        place.add(player);
        addPlace(place);
    }

    @Override
    public void createZoneDefault() {

    }
}

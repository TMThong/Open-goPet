namespace Gopet.Data.Map
{
    public class ChallengeMap : GopetMap
    {

        public ChallengeMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) : base(mapId_, canUpdate, mapTemplate)
        {

        }


        public override void addRandom(Player player)
        {
            foreach (Place place_lc in places)
            {
                if (place_lc.canAdd(player))
                {
                    place_lc.add(player);
                    return;
                }
            }
            Place place = new ChallengePlace(this, places.Count);
            place.add(player);
            addPlace(place);
        }


        public override void createZoneDefault()
        {

        }
    }
}
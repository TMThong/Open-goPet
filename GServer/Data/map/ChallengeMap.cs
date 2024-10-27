using Gopet.Util;

namespace Gopet.Data.Map
{
    public class ChallengeMap : GopetMap
    {

        public ChallengeMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) : base(mapId_, canUpdate, mapTemplate)
        {

        }

        public override void run()
        {
            isRunning = true;
            while (isRunning)
            {
                try
                {
                    long lastTime = Utilities.CurrentTimeMillis;
                    update();
                    if (Utilities.CurrentTimeMillis - lastTime < 500)
                    {
                        Thread.Sleep(500);
                    }
                }
                catch (Exception e)
                {
                    try
                    {
                        e.printStackTrace();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        public override void update()
        {
            foreach (Place place in places)
            {
                try
                {
                    place.update();
                    if (place.needRemove())
                    {
                        place.removeAllPlayer();
                        places.remove(place);
                    }
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
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
            lock (this)
            {
                Place place = new ChallengePlace(this, places.Count);
                place.add(player);
                addPlace(place);
            }
        }


        public override void createZoneDefault()
        {

        }
    }
}
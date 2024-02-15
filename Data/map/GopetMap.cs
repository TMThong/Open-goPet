using Gopet.Data.Collections;
using Gopet.Util;

namespace Gopet.Data.Map
{
    public class GopetMap
    {

        public int mapID;
        public int numPlace = 0;
        public MapTemplate mapTemplate;
        public CopyOnWriteArrayList<Place> places = new CopyOnWriteArrayList<Place>();
        public bool isRunning = false;
        public Thread MyThread;
        public GopetMap(int mapId_, bool canUpdate, MapTemplate mapTemplate)
        {
            if(!GopetManager.dropItem.ContainsKey(mapId_))
            {
                GopetManager.dropItem[mapId_] = new();
            }
            mapID = mapId_;
            this.mapTemplate = mapTemplate;
            createZoneDefault();
            MyThread = new Thread(run)
            {
                Name = Utilities.Format("Thread of map %s and mapId = %s", mapTemplate.name, mapId_),
                IsBackground = true,
            };

            if (canUpdate)
            {
                start();
            }
        }

        private void start()
        {
            MyThread.Start();
        }

        public virtual void createZoneDefault()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    addPlace(new GopetPlace(this, i));
                }
                catch (Exception ex)
                {
                    ex.printStackTrace();
                }
            }
        }


        public virtual void run()
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
                    catch(Exception ex)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(ex);
                    }
                }
            }

        }

        public virtual void update()
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

        public virtual void addPlace(Place place)
        {
            places.add(place);
            numPlace++;
        }

        public virtual void removePlace(Place place)
        {
            places.remove(place);
            numPlace--;
        }

        public virtual void addRandom(Player player)
        {
            foreach (Place place in places)
            {
                if (place.canAdd(player) && place.numPlayer < place.maxPlayer / 2)
                {
                    place.add(player);
                    return;
                }
            }
            Place place_L = new GopetPlace(this, places.Count);
            place_L.add(player);
            addPlace(place_L);
        }
    }
}
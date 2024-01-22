

using Gopet.Data.Collections;

public class GopetMap   {

    public int mapID;
    public int numPlace = 0;
    public MapTemplate mapTemplate;
    public CopyOnWriteArrayList<Place> places = new CopyOnWriteArrayList<Place>();
    public bool isRunning = false;
    public static ThreadGroup threadGroup = new ThreadGroup("MapGopet");

    public GopetMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) {
         
        mapID = mapId_;
        this.mapTemplate = mapTemplate;
        createZoneDefault();
        setName(String.format("Thread of map %s and mapId = %s", mapTemplate.getMapName(), mapId_));
        if (canUpdate) {
            start();
        }
    }

    private void start()
    {
         
    }

    public void createZoneDefault() {
        for (int i = 0; i < 10; i++) {
            try {
                addPlace(new GopetPlace(this, i));
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
    }

     
    public void run() {
        isRunning = true;
        while (isRunning) {
            try {
                long lastTime = System.currentTimeMillis();
                update();
                if (System.currentTimeMillis() - lastTime < 500) {
                    Thread.sleep(500L);
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        System.out.println("Map " + mapTemplate.getMapName() + " stop updated");
    }

    public void update()   {
        for (Place place : places) {
            try {
                place.update();
                if (place.needRemove()) {
                    place.removeAllPlayer();
                    places.remove(place);
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public void addPlace(Place place) {
        places.add(place);
        numPlace++;
    }

    public void removePlace(Place place) {
        places.remove(place);
        numPlace--;
    }

    public void addRandom(Player player)   {
        for (Place place : places) {
            if (place.canAdd(player) && place.numPlayer < place.maxPlayer / 2) {
                place.add(player);
                return;
            }
        }
        Place place = new GopetPlace(this, places.size());
        place.add(player);
        addPlace(place);
    }
}

 
public class BXHManager : Thread {

    private long lastTime = 0;
    public static BXHManager instance = new BXHManager();
    public bool isRunning = false;

    public BXHManager() {
        setName("BXH Thread");
    }

    
    public void run() {
        if (!isRunning) {
            try {
                isRunning = true;
                update();
            } catch (Exception ex) {
                Logger.getLogger(BXHManager.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    public void update()   {
        while (isRunning) {
            if (lastTime < System.currentTimeMillis()) {
                for (Top next : listTop) {
                    next.update();
                }
                lastTime = System.currentTimeMillis() + 1000 * 60 * 15;
            }
            Thread.sleep(60000);
        }
    }

    public const CopyOnWriteArrayList<Top> listTop = new CopyOnWriteArrayList<>();

    static BXHManager
(){
        listTop.add(TopGold.instance);
        listTop.add(TopPet.instance);
        listTop.add(TopGem.instance);
        listTop.add(TopLVLClan.instance);
        listTop.add(TopSpendGold.instance);
    }
}

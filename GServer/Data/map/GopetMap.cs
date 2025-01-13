using Gopet.Data.Collections;
using Gopet.Util;

namespace Gopet.Data.Map
{
    /// <summary>
    /// Lớp GopetMap đại diện cho bản đồ trong trò chơi.
    /// </summary>
    public class GopetMap
    {
        // ID của bản đồ
        public int mapID;
        // Số lượng địa điểm trong bản đồ
        public int numPlace = 0;
        // Mẫu bản đồ
        public MapTemplate mapTemplate;
        // Danh sách các địa điểm trong bản đồ
        public CopyOnWriteArrayList<Place> places = new CopyOnWriteArrayList<Place>();
        // Trạng thái chạy của bản đồ
        public bool isRunning = false;
        // Luồng chạy của bản đồ
        public Thread MyThread;

        /// <summary>
        /// Hàm khởi tạo của lớp GopetMap.
        /// </summary>
        /// <param name="mapId_">ID của bản đồ</param>
        /// <param name="canUpdate">Có thể cập nhật hay không</param>
        /// <param name="mapTemplate">Mẫu bản đồ</param>
        public GopetMap(int mapId_, bool canUpdate, MapTemplate mapTemplate)
        {
            if (!GopetManager.dropItem.ContainsKey(mapId_))
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

        /// <summary>
        /// Bắt đầu luồng chạy của bản đồ.
        /// </summary>
        private void start()
        {
            MyThread.Start();
        }

        /// <summary>
        /// Tạo các khu vực mặc định trong bản đồ.
        /// </summary>
        public virtual void createZoneDefault()
        {
            for (int i = 0; i < 30; i++)
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

        /// <summary>
        /// Hàm chạy của bản đồ.
        /// </summary>
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
                    catch (Exception ex)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Cập nhật trạng thái của các địa điểm trong bản đồ.
        /// </summary>
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

        /// <summary>
        /// Thêm một địa điểm vào bản đồ.
        /// </summary>
        /// <param name="place">Địa điểm cần thêm</param>
        public virtual void addPlace(Place place)
        {
            places.Add(place);
            numPlace++;
        }

        /// <summary>
        /// Xóa một địa điểm khỏi bản đồ.
        /// </summary>
        /// <param name="place">Địa điểm cần xóa</param>
        public virtual void removePlace(Place place)
        {
            places.remove(place);
            numPlace--;
        }

        /// <summary>
        /// Thêm ngẫu nhiên một người chơi vào một địa điểm trong bản đồ.
        /// </summary>
        /// <param name="player">Người chơi cần thêm</param>
        public virtual void addRandom(Player player)
        {
            foreach (Place place in places)
            {
                if (place.canAdd(player) && place.players.Count < place.maxPlayer / 2)
                {
                    place.add(player);
                    return;
                }
            }
            Place place_L = new GopetPlace(this, places.Count);
            place_L.add(player);
            addPlace(place_L);
        }

        /// <summary>
        /// Thuộc tính cho phép thay đổi khu vực.
        /// </summary>
        public virtual bool CanChangeZone { get; set; } = true;
    }
}
using Gopet.Data.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.map
{
    public class ArenaMap : GopetMap
    {
        private Mutex mutex = new Mutex();
        public ArenaMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) : base(mapId_, canUpdate, mapTemplate)
        {

        }

        public override void addRandom(Player player)
        {

        }

        public void AddBattle(Player One, Player Two)
        {
            mutex.WaitOne();
            try
            {
                foreach (GopetPlace _Place in places)
                {
                    if (_Place is ArenaPlace place)
                    {
                        if (place.canAdd(One) && place.players.Count < place.maxPlayer / 2)
                        {
                            place.addArena(One, Two);
                            return;
                        }
                    }
                }
                ArenaPlace place_L = new ArenaPlace(this, places.Count);
                place_L.addArena(One, Two);
                addPlace(place_L);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public override void update()
        {
            base.update();
        }

        public override bool CanChangeZone => false;
    }
}

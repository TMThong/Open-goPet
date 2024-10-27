using Gopet.Util;

namespace Gopet.Data.Map
{
    public class MarketMap : GopetMap
    {

        public MarketMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) : base(mapId_, canUpdate, mapTemplate)
        {

        }


        public override void createZoneDefault()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    addPlace(new MarketPlace(this, i));
                }
                catch (Exception ex)
                {
                    ex.printStackTrace();
                }
            }
        }
    }
}
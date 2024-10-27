using Gopet.Data.GopetItem;

namespace Gopet.Battle
{
    public class Buff
    {

        public int turn = 0;
        public ItemInfo[] infos = new ItemInfo[0];

        public Buff()
        {
        }

        public Buff(ItemInfo[] infos, int turn)
        {
            this.infos = infos;
            this.turn = turn;
        }
    }
}
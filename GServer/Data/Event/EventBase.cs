using Gopet.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event
{
    public abstract class EventBase
    {
        public virtual string Name { get; set; }
        public virtual bool Condition { get; set; }
        public virtual bool NeedRemove { get; set; }

        public bool IsInitOK { get; set; } = false;

        public virtual int[] ItemsOfEvent { get; set; } = new int[0];

        protected EventBase() { }

        public virtual void Update()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Trả về true có nghĩa là sự kiện vẫn còn</returns>
        public virtual bool CheckEventStatus(Player player)
        {
            if (NeedRemove || !EventManager._events.Contains(this))
            {
                player.redDialog("Sự kiện đã kết thúc");
                return false;
            }
            return true;
        }

        public virtual void UseItem(int itemId, Player player)
        {

        }

        public virtual void UseItemCount(int itemId, Player player, int count)
        {

        }

        public virtual void NpcOption(Player player, int optionId)
        {

        }

        /// <summary>
        /// Khởi tạo sự kiện
        /// </summary>

        public virtual void Init()
        {

        }
    }
}

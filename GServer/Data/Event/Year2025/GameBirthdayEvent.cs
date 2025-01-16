using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event.Year2025
{
    public class GameBirthdayEvent : EventBase
    {
        public static readonly GameBirthdayEvent Instance = new GameBirthdayEvent();
        protected GameBirthdayEvent()
        {
            this.Name = "Sự kiện sinh nhật";
        }

        public override bool Condition => true;

        public override void UseItem(int itemId, Player player)
        {

        }

        public override void Update()
        {

        }

        public override void NpcOption(Player player, int optionId)
        {

        }
    }
}

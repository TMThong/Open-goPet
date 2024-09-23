using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event.Year2024
{
    internal class Winter2024Event : EventBase
    {
        public static readonly Winter2024Event Instance = new Winter2024Event();
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

        public override bool NeedRemove => false;
    }
}

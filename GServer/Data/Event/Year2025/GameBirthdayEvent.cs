using Gopet.Data.Map;
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

        public const int NPC_BIRTHDAY_CAKE = -41;

        protected GameBirthdayEvent()
        {
            this.Name = "Sự kiện sinh nhật";
        }

        public override bool Condition => true;


        public override void Init()
        {
            if (this.Condition)
            {
                BXHManager.listTop.Add(TopUseCylindricalStickyRiceCake.Instance);
                BXHManager.listTop.Add(TopUseSquareStickyRiceCake.Instance);
                MapTemplate mapTemplate = GopetManager.mapTemplate[MapTemplate.THÀNH_PHỐ_LINH_THÚ];
                if (!mapTemplate.npc.Contains(NPC_BIRTHDAY_CAKE))
                {
                    mapTemplate.npc = mapTemplate.npc.Concat(new int[] { NPC_BIRTHDAY_CAKE }).ToArray();
                }
            }
        }


        public override void UseItem(int itemId, Player player)
        {
            if (this.CheckEventStatus(player))
            {

            }
        }

        public override void Update()
        {

        }

        public override void NpcOption(Player player, int optionId)
        {
            if (this.CheckEventStatus(player))
            {

            }
        }

        protected class TopUseCylindricalStickyRiceCake : Top
        {
            public static readonly TopUseCylindricalStickyRiceCake Instance = new TopUseCylindricalStickyRiceCake();
            protected TopUseCylindricalStickyRiceCake() : base("use.cylindricalStickyRice")
            {
                this.name = "Top ăn bánh tét";
                this.desc = "Để chỉ số lần ăn bánh tét";
            }


        }

        protected class TopUseSquareStickyRiceCake : Top
        {
            public static readonly TopUseSquareStickyRiceCake Instance = new TopUseSquareStickyRiceCake();
            public TopUseSquareStickyRiceCake() : base("use.squareStickyRice")
            {
                this.name = "Top ăn bánh chưng";
                this.desc = "Để chỉ số lần ăn bánh chưng";
            }
        }
    }
}

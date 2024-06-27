using Gopet.Data.GopetItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.user
{
    public class Achievement
    {
        public int IdTemplate { get; set; }
        public int Id { get; set; }
        public DateTime? Expire { get; set; }

        public Achievement(int idTemplate)
        {
            IdTemplate = idTemplate;
            if (this.Template.Expire > 0)
            {
                Expire = DateTime.Now;
                Expire.Value.AddMilliseconds(this.Template.Expire);
            }
        }

        public AchievementTemplate Template
        {
            get
            {
                return GopetManager.AchievementMAP[IdTemplate];
            }
        }

        internal class AchievementComparer : IComparer<Achievement>
        {
            public int Compare(Achievement? obj1, Achievement? obj2)
            {
                return obj1.Id - obj2.Id;
            }
        }
    }
}

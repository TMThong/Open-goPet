using Gopet.Data.GopetItem;
using Gopet.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.user
{
    public class Achievement : IBinaryObject<Achievement>
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

        [JsonIgnore]
        public Achievement Instance => this;

        public int GetId()
        {
            return Id;
        }

        public void SetId(int id)
        {
            this.Id = id;
        }
    }
}

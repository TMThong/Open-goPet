using Gopet.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public record Letter(sbyte Type, string Title, string ShortContent, string Content, bool IsMark) : IBinaryObject<Letter>
    {
        public const sbyte ADMIN = 2;
        public const sbyte EVENT = 3;
        public const sbyte FRIEND = 1;
        public int LetterId { get; set; }

        [JsonIgnore]
        public Letter Instance => this;

        public int GetId()
        {
            return this.LetterId;
        }

        public void SetId(int id)
        {
            this.LetterId = id;
        }
    }
}

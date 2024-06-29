using Gopet.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public class Letter : IBinaryObject<Letter>
    {
        public const sbyte ADMIN = 2;
        public const sbyte EVENT = 3;
        public const sbyte FRIEND = 1;
        public int LetterId { get; set; }
        public bool IsMark { get; set; } = false;
        public sbyte Type { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public int userId { get; set; }
        [JsonIgnore]
        public int targetId { get; set; }
        [JsonIgnore]
        public DateTime time { get; set; }

        [JsonIgnore]
        public Letter Instance => this;

        public Letter()
        {
        }

        public Letter(bool isMark, sbyte type, string title, string shortContent, string content)
        {
            IsMark = isMark;
            Type = type;
            Title = title;
            ShortContent = shortContent;
            Content = content;
        }

        public Letter(int letterId, bool isMark, sbyte type, string title, string shortContent, string content)
        {
            LetterId = letterId;
            IsMark = isMark;
            Type = type;
            Title = title;
            ShortContent = shortContent;
            Content = content;
        }

        public Letter(sbyte type, string title, string shortContent, string content)
        {
            Type = type;
            Title = title;
            ShortContent = shortContent;
            Content = content;
        }

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

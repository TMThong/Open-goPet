using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.APIs
{
    internal static class GopetApiExtentsion
    {
        public static GopetRepository<string> OK_Repository = new GopetRepository<string>(1, "Thành công");
        public static GopetRepository<string> FAILED_Repository = new GopetRepository<string>(0, "Thất bại");

        public static GopetRepository<T> CreateOKRepository<T>(T data)
        {
            return new GopetRepository<T>(1, data);
        }

        public static GopetRepository<T> CreateFailedRepository<T>(T data)
        {
            return new GopetRepository<T>(0, data);
        }

        public static string UnicodeJson(string data)
        {
            var obj = JsonConvert.DeserializeObject(data);
            return JsonConvert.SerializeObject(obj);
        }
    }
}

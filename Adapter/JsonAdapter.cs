using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Adapter
{
    public class JsonAdapter<T> : SqlMapper.TypeHandler<T>
    {

        public static JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings()
        {
            DefaultValueHandling = DefaultValueHandling.Include,
            NullValueHandling = NullValueHandling.Include
        };

        public override T? Parse(object value)
        {
            if (value is string text)
            {
                return JsonConvert.DeserializeObject<T>(text, SerializerSettings);
            }
            else
            {
                return default(T);
            }
        }

        public override void SetValue(IDbDataParameter parameter, T? value)
        {
            if (value != null)
            {
                parameter.Value = JsonConvert.SerializeObject(value, SerializerSettings);
            }
        }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Adapter
{
    internal class VersionAdapter : SqlMapper.TypeHandler<Version>
    {
        public override Version? Parse(object value)
        {
            return Version.Parse(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Version? value)
        {
            parameter.Value = value.ToString();
        }
    }
}

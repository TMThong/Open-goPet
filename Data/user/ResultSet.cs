using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.user
{
    public class ResultSet
    {
        public MySqlDataReader SqlDataReader { get;  }

        public ResultSet(MySqlDataReader sqlDataReader)
        {
            SqlDataReader = sqlDataReader;
        }

    }
}

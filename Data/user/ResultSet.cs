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
        public MySqlDataReader SqlDataReader { get; }

        public MySqlCommand Command { get; }

        public ResultSet(MySqlCommand command)
        {
            Command = command;
            this.SqlDataReader = command.ExecuteReader();
        }

        public bool next()
        {
            return this.SqlDataReader.Read();
        }

        public int getInt(string columnName)
        {
            try
            {
                return this.SqlDataReader.GetInt32(columnName);
            }
            catch
            {
                return 0;
            }
        }

        public long getlong(string columnName)
        {
            return this.SqlDataReader.GetInt64(columnName);
        }

        public double getDouble(string columnName)
        {
            return this.SqlDataReader.GetDouble(columnName);
        }

        public float getFloat(string columnName)
        {
            return this.SqlDataReader.GetFloat(columnName);
        }

        public sbyte getByte(string columnName)
        {
            return (sbyte)(this.SqlDataReader.GetInt32(columnName));
        }

        public string getString(string columnName)
        {
            try
            {
                return this.SqlDataReader.GetString(columnName);
            }
            catch
            {
                return "";
            }
        }

        public BigInt getBigDecimal(string columnName)
        {
            return new BigInt(getlong(columnName));
        }

        internal void Close()
        {
            this.SqlDataReader.Close();
            this.Command.Dispose();
        }

        internal sbyte getsbyte(string v)
        {
            return getByte(v);
        }



        public DateTime getDateTime(string columnName)
        {
            return this.SqlDataReader.GetDateTime(columnName);
        }

        public bool getbool(string columnName)
        {
            return this.SqlDataReader.GetBoolean(columnName);
        }

        internal long getlongExpire(string expire)
        {
            try
            {
                object o = this.SqlDataReader[expire];
                if (o != null)
                {
                    return (long)o;
                }
            }
            catch { }
            return -1;
        }

        public class BigInt
        {
            public long Value { get; set; }

            public BigInt(long value)
            {
                Value = value;
            }

            public long longValue() => Value;
        }
    }
}

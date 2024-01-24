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
            return this.SqlDataReader.GetInt32(columnName);
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
            return this.SqlDataReader.GetSByte(columnName);
        }

        public string getString(string columnName)
        {
            return this.SqlDataReader.GetString(columnName);
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

        internal int getsbyte(string v)
        {
            throw new NotImplementedException();
        }

        

        public DateTime getDateTime(string columnName)
        {
            return this.SqlDataReader.GetDateTime(columnName);
        }

        public bool getbool(string columnName)
        {
            return this.SqlDataReader.GetBoolean(columnName);
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

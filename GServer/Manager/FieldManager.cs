using Dapper;
using Gopet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Manager
{
    public static class FieldManager
    {
        static Mutex mutex = new Mutex();

        public static IEnumerable<ServerField> ServerFields { get; private set; }

        public static Dictionary<string, ServerField> Fields { get; private set; } = new Dictionary<string, ServerField>();


        public static float PERCENT_EXP
        {
            get
            {
                return FindAndCastFloat("Server.Exp.Percent", 100f);
            }
        }
        public static float PERCENT_GEM
        {
            get
            {
                return FindAndCastFloat("Server.GEM.Percent", 100f);
            }
        }


        public static ServerField FindValue(string fieldName)
        {
            Fields.TryGetValue(fieldName, out var value);
            if (value == null)
                return null;
            return value;
        }


        public static float FindAndCastFloat(string fieldName, dynamic defaultValue)
        {
            ServerField value = FindValue(fieldName);
            if (value == null) return defaultValue;
            try
            {
                return float.Parse(value.Value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static void Init()
        {
            Update();
        }

        public static void Update()
        {
            mutex.WaitOne();
            using (var conn = MYSQLManager.create())
            {
                ServerFields = conn.Query<ServerField>("SELECT * FROM `field`");
            }

            Dictionary<string, ServerField> localField = new();

            foreach (var field in ServerFields)
            {
                localField[field.FieldName] = field;
            }
            Fields = localField;
            mutex.ReleaseMutex();
        }
    }
}

using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Runtime
{
    public class DBBackup : IRuntime
    {
        private DateTime _lastBackup = DateTime.Now;
        public void Update()
        {
            DateTime now = DateTime.Now;
            if (_lastBackup < DateTime.Now)
            {
                using (var gameConn = MYSQLManager.create())
                {
                    MYSQLManager.Backup(gameConn, Path.Combine(Directory.GetCurrentDirectory() + "/backup_sql", $"game{now.Day}-{now.Month}-{now.Year}_{now.Hour}-{now.Minute}.sql"));
                }

                using (var webConn = MYSQLManager.createWebMySqlConnection())
                {
                    MYSQLManager.Backup(webConn, Path.Combine(Directory.GetCurrentDirectory() + "/backup_sql", $"web{now.Day}-{now.Month}-{now.Year}_{now.Hour}-{now.Minute}.sql"));
                }
                _lastBackup = DateTime.Now.AddMinutes(300);
            }
        }
    }
}

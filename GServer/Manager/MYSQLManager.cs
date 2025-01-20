

using Gopet.Data.User;
using MySqlConnector;
using System.Configuration;

public class MYSQLManager
{
    public static MySqlConnection create()
    {
        var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GameConnectString"].ConnectionString);
        conn.Open();
        return conn;
    }

    public static MySqlConnection createLogConnection()
    {
        var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LogConnectString"].ConnectionString);
        conn.Open();
        return conn;
    }

    public static MySqlConnection createOld()
    {
        var conn = new MySqlConnection(GameOldSQLInfoStr);
        conn.Open();
        return conn;
    }

    public static string GameOldSQLInfoStr
    {
        get
        {
            return $"SERVER={MysqlSetting.SINGLETON.INSTANCE.host};DATABASE=gopettae_gopet;UID={MysqlSetting.SINGLETON.INSTANCE.username};PASSWORD={MysqlSetting.SINGLETON.INSTANCE.password};CharSet=utf8;";
        }
    }

    public static MySqlConnection createWebMySqlConnection()
    {
        var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["WebConnectString"].ConnectionString);
        conn.Open();
        return conn;
    }

    public static void Backup(MySqlConnection conn, string filePath)
    {
        using (var cmd = new MySqlCommand())
        {
            cmd.Connection = conn;
            using (var backup = new MySqlBackup(cmd))
            {
                FileInfo f = new FileInfo(filePath);
                f.Directory.Create();
                using (var stream = f.Open(FileMode.OpenOrCreate))
                {
                    backup.ExportToStream(stream);
                    stream.Flush();
                    stream.Close();
                }
            }
        }
    }
}

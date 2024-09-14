

using Gopet.Data.User;
using MySql.Data.MySqlClient;

public class MYSQLManager
{

    public static MySqlConnection create()
    {
        var conn = new MySqlConnection(GameSQLInfoStr);
        conn.Open();
        return conn;
    }

    public static MySqlConnection createOld()
    {
        var conn = new MySqlConnection(GameOldSQLInfoStr);
        conn.Open();
        return conn;
    }


    public static string GameSQLInfoStr
    {
        get
        {
            return $"SERVER={MysqlSetting.SINGLETON.INSTANCE.host};DATABASE={MysqlSetting.SINGLETON.INSTANCE.database};UID={MysqlSetting.SINGLETON.INSTANCE.username};PASSWORD={MysqlSetting.SINGLETON.INSTANCE.password};CharSet=utf8;";
        }
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
        string connStr = $"SERVER={MysqlSetting.SINGLETON.INSTANCE.host_web};DATABASE={MysqlSetting.SINGLETON.INSTANCE.database_web};UID={MysqlSetting.SINGLETON.INSTANCE.username_web};PASSWORD={MysqlSetting.SINGLETON.INSTANCE.password_web};CharSet=utf8;";
        var conn = new MySqlConnection(connStr);
        conn.Open();
        return conn;
    }
}

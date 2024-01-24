

using Gopet.Data.User;
using MySql.Data.MySqlClient;

public class MYSQLManager
{


    public MYSQLManager()
    {
    }

    public static void init()
    {

    }

    private static MySqlConnection MySqlConnection;

    public static MySqlConnection getMySqlConnection()
    {
        return create();
    }

    public static void resetConnect()
    {

    }

    public static ResultSet jquery(String sql)
    {
        return jquery(sql, getMySqlConnection());
    }

    public static void updateSql(String sql)
    {
        updateSql(sql, getMySqlConnection());
    }

    public static ResultSet jquery(String sql, MySqlConnection CONN)
    {
        return new ResultSet(new MySqlCommand(sql, CONN));
    }

    public static void updateSql(String sql, MySqlConnection CONN)
    {
        new MySqlCommand(sql, CONN).ExecuteNonQuery();
    }

    public static MySqlConnection create()
    {
        string connStr = $"SERVER={MysqlSetting.SINGLETON.INSTANCE.host};DATABASE={MysqlSetting.SINGLETON.INSTANCE.database};UID={MysqlSetting.SINGLETON.INSTANCE.username};PASSWORD={MysqlSetting.SINGLETON.INSTANCE.password};CharSet=utf8;";
        var conn = new MySqlConnection(connStr);
        conn.Open();
        return conn;
    }

    public static MySqlConnection createWebMySqlConnection()
    {
        string connStr = $"SERVER={MysqlSetting.SINGLETON.INSTANCE.host_web};DATABASE={MysqlSetting.SINGLETON.INSTANCE.database_web};UID={MysqlSetting.SINGLETON.INSTANCE.username_web};PASSWORD={MysqlSetting.SINGLETON.INSTANCE.password_web};CharSet=utf8;";
        var conn = new MySqlConnection(connStr);
        conn.Open();
        return conn;
    }
}



using Gopet.Data.user;
using MySql.Data.MySqlClient;

public class MYSQLManager {

    
    public MYSQLManager() {
    }

    public static void init()   {
         
    }

    private static MySqlConnection MySqlConnection;

    public static MySqlConnection getMySqlConnection()   {
         

        return MySqlConnection;
    }

    public static void resetConnect() {
         
    }

    public static ResultSet jquery(String sql)   {
        PreparedStatement pre
                = getMySqlConnection().prepareStatement(
                        sql, ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);

        //pre.setQueryTimeout(60);
        return pre.executeQuery();
    }

    public static void updateSql(String sql)   {
        PreparedStatement pre = getMySqlConnection().prepareStatement(sql);
        //pre.setQueryTimeout(60);
        pre.execute();
        pre.close();
    }

    public static ResultSet jquery(String sql, MySqlConnection MySqlConnection)   {
        PreparedStatement pre
                = MySqlConnection.prepareStatement(
                        sql, ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
        //pre.setQueryTimeout(60);
        return pre.executeQuery();
    }

    public static void updateSql(String sql, MySqlConnection MySqlConnection)   {
        PreparedStatement pre = MySqlConnection.prepareStatement(sql);
        //pre.setQueryTimeout(60);
        pre.execute();
        pre.close();
    }

    public static MySqlConnection create()   {
        return dataSource.getMySqlConnection();
    }

    public static MySqlConnection createWebMySqlConnection()   {
        return dataSourceWeb.getMySqlConnection();
    }
}

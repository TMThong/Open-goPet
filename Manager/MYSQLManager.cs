package manager;

import com.mysql.cj.jdbc.MysqlDataSource;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import settings.MysqlSetting;

public class MYSQLManager {

    public static MysqlDataSource dataSource = null;
    public static MysqlDataSource dataSourceWeb = null;

    public MYSQLManager() {
    }

    public static void init() throws SQLException {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
            System.exit(0);
        }
        MysqlSetting setting = MysqlSetting.getInstance();
        MysqlDataSource mysqlDataSource = new MysqlDataSource();
        mysqlDataSource.setURL(setting.getUrl());
        mysqlDataSource.setUser(setting.getUsername());
        mysqlDataSource.setPassword(setting.getPassword());
        mysqlDataSource.setMaxReconnects(Integer.MAX_VALUE);
        mysqlDataSource.setMaxAllowedPacket(Integer.MAX_VALUE);
        mysqlDataSource.setAutoReconnect(true);
        mysqlDataSource.setTcpNoDelay(true);
        mysqlDataSource.setUseSSL(true);
        dataSourceWeb = new MysqlDataSource();
        dataSourceWeb.setURL(setting.getUrlWeb());
        dataSourceWeb.setUser(setting.getUsername_web());
        dataSourceWeb.setPassword(setting.getPassword_web());
        dataSourceWeb.setMaxReconnects(Integer.MAX_VALUE);
        dataSourceWeb.setMaxAllowedPacket(Integer.MAX_VALUE);
        dataSourceWeb.setAutoReconnect(true);
        dataSourceWeb.setTcpNoDelay(true);
        dataSourceWeb.setUseSSL(true);
        // Lấy DataSource để sử dụng
        dataSource = mysqlDataSource;
    }

    private static Connection connection;

    public static Connection getConnection() throws SQLException {
        if (connection == null) {
            connection = dataSource.getConnection();
        }

        if (connection.isClosed()) {
            connection = dataSource.getConnection();
        }

        return connection;
    }

    public static void resetConnect() {
        try {
            connection = dataSource.getConnection();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static ResultSet jquery(String sql) throws SQLException {
        PreparedStatement pre
                = getConnection().prepareStatement(
                        sql, ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);

        //pre.setQueryTimeout(60);
        return pre.executeQuery();
    }

    public static void updateSql(String sql) throws SQLException {
        PreparedStatement pre = getConnection().prepareStatement(sql);
        //pre.setQueryTimeout(60);
        pre.execute();
        pre.close();
    }

    public static ResultSet jquery(String sql, Connection connection) throws SQLException {
        PreparedStatement pre
                = connection.prepareStatement(
                        sql, ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
        //pre.setQueryTimeout(60);
        return pre.executeQuery();
    }

    public static void updateSql(String sql, Connection connection) throws SQLException {
        PreparedStatement pre = connection.prepareStatement(sql);
        //pre.setQueryTimeout(60);
        pre.execute();
        pre.close();
    }

    public static Connection create() throws SQLException {
        return dataSource.getConnection();
    }

    public static Connection createWebConnection() throws SQLException {
        return dataSourceWeb.getConnection();
    }
}

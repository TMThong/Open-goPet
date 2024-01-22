package settings;

import lombok.Getter;

/**
 * @author outcast c-cute hột me 😳
 */
@Getter
public class MysqlSetting implements Settings {

    private String host;
    private int port;
    private String database;
    private String username;
    private String password;

    private String host_web;
    private int port_web;
    private String database_web;
    private String username_web;
    private String password_web;

    public MysqlSetting() {
        try {
            load(new SettingsFile("database.properties"));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void load(SettingsFile settingsFile) {
        host = settingsFile.getString("host", "localhost");
        port = settingsFile.getInteger("port", 3306);
        database = settingsFile.getString("database", "test");
        username = settingsFile.getString("username", "root");
        password = settingsFile.getString("password", "");
        host_web = settingsFile.getString("host_web", "localhost");
        port_web = settingsFile.getInteger("port_web", 3306);
        database_web = settingsFile.getString("database_web", "test");
        username_web = settingsFile.getString("username_web", "root");
        password_web = settingsFile.getString("password_web", "");
    }

    public String getUrl() {
        return "jdbc:mysql://" + host + ":" + port + "/" + database;
    }
    
    public String getUrlWeb() {
        return "jdbc:mysql://" + host_web + ":" + port_web + "/" + database_web;
    }

    public static MysqlSetting getInstance() {
        return SINGLETON.INSTANCE;
    }

    public static class SINGLETON {

        private const MysqlSetting INSTANCE = new MysqlSetting();
    }

    @Override
    public String toString() {
        return "MysqlSetting{" + "host=" + host + ", port=" + port + ", database=" + database + ", username=" + username + ", password=" + password + ", host_web=" + host_web + ", port_web=" + port_web + ", database_web=" + database_web + ", username_web=" + username_web + ", password_web=" + password_web + '}';
    }
}

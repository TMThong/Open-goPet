
public class MysqlSetting : Settings
{

    public String host {  get; protected set; }
    public int port { get; protected set; }
    public String database { get; protected set; }
    public String username { get; protected set; }
    public String password { get; protected set; }

    public String host_web { get; protected set; }
    public int port_web { get; protected set; }
    public String database_web { get; protected set; }
    public String username_web { get; protected set; }
    public String password_web { get; protected set; }

    public MysqlSetting()
    {
        load(new SettingsFile("database.json"));
    }

    public void load(SettingsFile settingsFile)
    {
        host = settingsFile.Data.host;
        port = settingsFile.Data.port;
        database = settingsFile.Data.database;
        username = settingsFile.Data.username;
        password = settingsFile.Data.password;
        host_web = settingsFile.Data.host_web;
        port_web = settingsFile.Data.port_web;
        database_web = settingsFile.Data.database_web;
        username_web = settingsFile.Data.username_web;
        password_web = settingsFile.Data.password_web;
    }

    public String getUrl()
    {
        return "jdbc:mysql://" + host + ":" + port + "/" + database;
    }

    public String getUrlWeb()
    {
        return "jdbc:mysql://" + host_web + ":" + port_web + "/" + database_web;
    }

    public static MysqlSetting getInstance()
    {
        return SINGLETON.INSTANCE;
    }

    public static class SINGLETON
    {

        public static readonly MysqlSetting INSTANCE = new MysqlSetting();
    }


    public String toString()
    {
        return "MysqlSetting{" + "host=" + host + ", port=" + port + ", database=" + database + ", username=" + username + ", password=" + password + ", host_web=" + host_web + ", port_web=" + port_web + ", database_web=" + database_web + ", username_web=" + username_web + ", password_web=" + password_web + '}';
    }
}


public class MysqlSetting : Settings
{

    public string host {  get; protected set; }
    public int port { get; protected set; }
    public string database { get; protected set; }
    public string username { get; protected set; }
    public string password { get; protected set; }

    public string host_web { get; protected set; }
    public int port_web { get; protected set; }
    public string database_web { get; protected set; }
    public string username_web { get; protected set; }
    public string password_web { get; protected set; }

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

    public static class SINGLETON
    {

        public static readonly MysqlSetting INSTANCE = new MysqlSetting();
    }
}


public class ServerSetting : Settings
{

    public static readonly ServerSetting instance = new ServerSetting();

    public int portGopetServer { get; protected set; }
    public int portHttpServer { get; protected set; }
    public string webDomainName { get; protected set; }
    public bool initLog { get; protected set; }
    public string outputFileName { get; protected set; }
    public string errorFileName { get; protected set; }
    public int hourMaintenance { get; protected set; }
    public int minMaintenance { get; protected set; }
    public bool isOnlyAdminLogin { get; protected set; }
    public bool isServerTest { get; protected set; }
    public bool isShowMessageWhenLogin { get; protected set; } = false;
    public string messageWhenLogin { get; protected set; }
    public string apiKey { get; protected set; }

    public ServerSetting()
    {

        load(new SettingsFile("server.json"));

    }


    public void load(SettingsFile settingsFile)
    {
        portGopetServer = settingsFile.Data.portGopetServer;
        portHttpServer = settingsFile.Data.portHttpServer;
        webDomainName = settingsFile.Data.webDomainName;
        initLog = settingsFile.Data.initLog;
        outputFileName = settingsFile.Data.outputFileName;
        errorFileName = settingsFile.Data.errorFileName;
        hourMaintenance = settingsFile.Data.hourMaintenance;
        minMaintenance = settingsFile.Data.minMaintenance;
        isOnlyAdminLogin = settingsFile.Data.isOnlyAdminLogin;
        isServerTest = settingsFile.Data.isServerTest;
        isShowMessageWhenLogin = settingsFile.Data.isShowMessageWhenLogin;
        messageWhenLogin = settingsFile.Data.messageWhenLogin;
        apiKey = settingsFile.Data.apiKey;
    }


    public string toString()
    {
        return "ServerSetting{" + "portGopetServer=" + portGopetServer + ", portHttpServer=" + portHttpServer + ", webDomainName=" + webDomainName + ", initLog=" + initLog + ", outputFileName=" + outputFileName + ", errorFileName=" + errorFileName + ", hourMaintenance=" + hourMaintenance + ", minMaintenance=" + minMaintenance + ", isOnlyAdminLogin=" + isOnlyAdminLogin + ", isServerTest=" + isServerTest + ", isShowMessageWhenLogin=" + isShowMessageWhenLogin + ", messageWhenLogin=" + messageWhenLogin + ", apiKey=" + apiKey + '}';
    }
}

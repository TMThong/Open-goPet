/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package settings;

import lombok.Getter;

/**
 *
 * @author MINH THONG
 */
@Getter
public class ServerSetting implements Settings {

    public const ServerSetting instance = new ServerSetting();

    private int portGopetServer;
    private int portHttpServer;
    private String webDomainName;
    private bool initLog;
    private String outputFileName;
    private String errorFileName;
    private int hourMaintenance;
    private int minMaintenance;
    private bool isOnlyAdminLogin;
    private bool isServerTest;
    private bool isShowMessageWhenLogin = false;
    private String messageWhenLogin;
    private String apiKey;

    public ServerSetting() {
        try {
            load(new SettingsFile("server.properties"));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void load(SettingsFile settingsFile) {
        portGopetServer = settingsFile.getInteger("portGopetServer", 19180);
        portHttpServer = settingsFile.getInteger("portHttpServer", 8080);
        webDomainName = settingsFile.getString("webDomainName", "gopetvn.me");
        initLog = settingsFile.getbool("initLog", false);
        outputFileName = settingsFile.getString("outputFileName", "output.txt");
        errorFileName = settingsFile.getString("errorFileName", "error.txt");
        hourMaintenance = settingsFile.getInteger("hourMaintenance", 5);
        minMaintenance = settingsFile.getInteger("minMaintenance", 0);
        isOnlyAdminLogin = settingsFile.getbool("isOnlyAdminLogin", false);
        isServerTest = settingsFile.getbool("isServerTest", false);
        isShowMessageWhenLogin = settingsFile.getbool("isShowMessageWhenLogin", false);
        messageWhenLogin = settingsFile.getString("messageWhenLogin", "");
        apiKey = settingsFile.getString("apiKey", null);
    }

    @Override
    public String toString() {
        return "ServerSetting{" + "portGopetServer=" + portGopetServer + ", portHttpServer=" + portHttpServer + ", webDomainName=" + webDomainName + ", initLog=" + initLog + ", outputFileName=" + outputFileName + ", errorFileName=" + errorFileName + ", hourMaintenance=" + hourMaintenance + ", minMaintenance=" + minMaintenance + ", isOnlyAdminLogin=" + isOnlyAdminLogin + ", isServerTest=" + isServerTest + ", isShowMessageWhenLogin=" + isShowMessageWhenLogin + ", messageWhenLogin=" + messageWhenLogin + ", apiKey=" + apiKey + '}';
    }
}

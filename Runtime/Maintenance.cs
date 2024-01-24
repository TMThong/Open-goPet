
using Gopet.App;
using Gopet.Util;

public class Maintenance : IRuntime {

    private long beginMaintenance;
    private int min;
    private static Maintenance instance;
    private bool isMaintenance = false;
    private bool needExit = true;
    private bool needRestart = false;

    /**
     * Cập nhật đến số phút yêu cầu sẽ tạm dừng máy chủ
     *
     * @ 
     */
     
    public void update()   {
        if (isMaintenance) {
            if (min > 0) {
                if (Utilities.CurrentTimeMillis - beginMaintenance >= 1000 * 60) {
                    beginMaintenance = Utilities.CurrentTimeMillis;
                    min--;
                    PlayerManager.showBanner(Utilities.Format("Sau %s phút nữa sẽ bảo trì, các người chơi vui lòng thoát game sớm tránh bị mất dữ liệu", min));
                }
            } else if (min <= 0) {
                if (needExit) {
                    if (needRestart) {
                        Main.server.stopServer();
                        String batchFilePath = PlatformHelper.currentDirectory() + "/run.bat";
                        /*Runtime.
                                getRuntime().
                                exec(new String[]{"cmd.exe", "/c", "start", batchFilePath});*/
                    }
                   
                }
            }
        }
    }

    /**
     * Bảo trì
     *
     * @param minute phút
     */
    public void setMaintenanceTime(int minute) {
        if (!isMaintenance) {
            beginMaintenance = Utilities.CurrentTimeMillis;
            min = minute + 1;
            isMaintenance = true;
        }
    }

    public static Maintenance gI() {
        if (instance == null) {
            instance = new Maintenance();
        }
        return instance;
    }

    public bool isNeedExit() {
        return needExit;
    }

    public void setNeedExit(bool needExit) {
        this.needExit = needExit;
    }

    public bool isNeedRestart() {
        return needRestart;
    }

    public void setNeedRestart(bool needRestart) {
        this.needRestart = needRestart;
    }

    public bool isIsMaintenance() {
        return isMaintenance;
    }
}

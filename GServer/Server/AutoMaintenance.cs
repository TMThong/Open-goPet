
using Gopet.Util;

public class AutoMaintenance
{
    private Timer timer;

    public AutoMaintenance()
    {
        timer = new Timer(MaintenanceTaskCallback);
    }

    public void Start(int hourMaintenance, int minMaintenance)
    {
        DateTime currentTime = DateTime.Now;
        DateTime maintenanceTime = new DateTime(
            currentTime.Year,
            currentTime.Month,
            currentTime.Day,
            hourMaintenance,
            minMaintenance,
            0
        );

        if (maintenanceTime <= currentTime)
        {
            maintenanceTime = maintenanceTime.AddDays(1);
        }

        GopetManager.ServerMonitor.LogWarning($"Thời gian bảo trì định kỳ: {maintenanceTime}");

        int delayMilliseconds = (int)(maintenanceTime - currentTime).TotalMilliseconds;
        timer.Change(delayMilliseconds, Timeout.Infinite);
    }

    private void MaintenanceTaskCallback(object state)
    {
        try
        {
            Maintenance.gI().setNeedRestart(true);
            Maintenance.gI().setNeedExit(true);
            Maintenance.gI().setMaintenanceTime(15);
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }
}

using Gopet.App;
using Gopet.Language;
using Gopet.Util;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Maintenance : IRuntime
{

    private long beginMaintenance;
    private int min;
    private static Maintenance instance;
    private bool isMaintenance = false;
    private bool needExit = true;
    private bool needRestart = false;

    /// <summary>
    /// Cập nhật đến số phút yêu cầu sẽ tạm dừng máy chủ
    /// </summary>
    public void Update()
    {
        if (isMaintenance)
        {
            if (min > 0)
            {
                if (Utilities.CurrentTimeMillis - beginMaintenance >= 1000 * 60)
                {
                    beginMaintenance = Utilities.CurrentTimeMillis;
                    min--;
                    PlayerManager.showBanner((l) => string.Format(l.BannerLanguage[LanguageData.BANNER_MANTENANCE_MESSAGE], min));
                }
            }
            else if (min <= 0)
            {
                if (needExit)
                {
                    if (needRestart)
                    {
                        reboot();
                    }
                    else
                    {
                        Main.shutdown();
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
        }
    }

    public void reboot()
    {
        Task.Run(() =>
        {
            Thread.Sleep(2000);
            Main.shutdown();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                startInfo.FileName = "Gopet.exe";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                startInfo.FileName = "Gopet";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                startInfo.FileName = "Gopet";
            else
                startInfo.FileName = "Gopet";
            startInfo.UseShellExecute = true; // Đặt thành false để có thể sử dụng cửa sổ dòng lệnh
            startInfo.CreateNoWindow = true; // Đặt thành false để hiển thị cửa sổ dòng lệnh
            startInfo.RedirectStandardInput = false; // Không đọc dữ liệu từ đầu vào tiêu chuẩn
            startInfo.RedirectStandardOutput = false; // Không ghi dữ liệu đầu ra tiêu chuẩn
            startInfo.RedirectStandardError = false; // Không ghi dữ liệu lỗi ra tiêu chuẩn
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            Process.GetCurrentProcess().Kill();
        });
    }

    /// <summary>
    /// Bảo trì
    /// </summary>
    /// <param name="minute">Phút</param>
    public void setMaintenanceTime(int minute)
    {
        if (!isMaintenance)
        {
            beginMaintenance = Utilities.CurrentTimeMillis;
            min = minute + 1;
            isMaintenance = true;
        }
    }

    public static Maintenance gI()
    {
        if (instance == null)
        {
            instance = new Maintenance();
        }
        return instance;
    }

    public bool isNeedExit()
    {
        return needExit;
    }

    public void setNeedExit(bool needExit)
    {
        this.needExit = needExit;
    }

    public bool isNeedRestart()
    {
        return needRestart;
    }

    public void setNeedRestart(bool needRestart)
    {
        this.needRestart = needRestart;
    }

    public bool isIsMaintenance()
    {
        return isMaintenance;
    }
}

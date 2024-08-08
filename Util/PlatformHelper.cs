
using Gopet.IO;

public class PlatformHelper
{

    private const bool isPC = true;

    public static String currentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public static String assetsPath;
    public static String dirPath;

    static PlatformHelper()
    {
        assetsPath = currentDirectory() + "/assets/";
    }

    public static sbyte[] loadAssets(String path)
    {
        sbyte[] buffer = null;
        try
        {
#if DEBUG_LOG
            GopetManager.ServerMonitor.LogWarning($"Load ASSETS: {path}");
#endif
            buffer = File.ReadAllBytes(Path.Combine(assetsPath + path.Replace('\\', '/'))).sbytes();
        }
        catch (Exception e)
        {
            throw e;
        }
        return buffer;
    }

    public static bool hasAssets(String path)
    {
        return File.Exists(assetsPath + path.Replace('\\', '/'));
    }
}

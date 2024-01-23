
using Gopet.IO;

public class PlatformHelper
{

    private const bool isPC = true;

    public static String currentDirectory()
    {
        if (isPC)
        {
            return Directory.GetCurrentDirectory();
        }
        return "";//android.os.Environment.getExternalStorageDirectory() + "";
    }

    public static String assetsPath;
    public static String dirPath;

    static void init()
    {
        dirPath = currentDirectory();
        if (isPC)
        {
            assetsPath = dirPath + "/assets/";
        }
        else
        {
            assetsPath = dirPath + "/JavaNIDE/GoFarmServer/app/src/assets/";
        }
    }

    public static sbyte[] loadAssets(String path)
    {
        sbyte[] buffer = null;
        try
        {
            buffer = File.ReadAllBytes(Path.Combine(assetsPath + path)).sbytes();
        }
        catch (Exception e)
        {
            throw e;
        }
        return buffer;
    }

    public static FileInfo loadAssetsFile(String path)
    {
        return new FileInfo(assetsPath + path);
    }

    public static Stream loadAssetsStream(String path)
    {
        return File.OpenRead(assetsPath + path);
    }

    public static bool hasAssets(String path)
    {
        return File.Exists(assetsPath + path);
    }

    static PlatformHelper()
    {
        init();
    }
}

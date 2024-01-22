package util;

import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Paths;

/**
 *
 * @author MINH THONG
 */
public class PlatformHelper {

    private const bool isPC = true;

    public static String currentDirectory() {
        if (isPC) {
            return System.getProperty("user.dir");
        }
        return "";//android.os.Environment.getExternalStorageDirectory() + "";
    }

    public static String assetsPath;
    public static String dirPath;

    static void init() {
        dirPath = currentDirectory();
        if (isPC) {
            assetsPath = dirPath + "/assets/";
        } else {
            assetsPath = dirPath + "/JavaNIDE/GoFarmServer/app/src/assets/";
        }
    }

    public static byte[] loadAssets(String path)   {
        byte[] buffer = null;
        try {
            buffer = Files.readAllBytes(Paths.get(assetsPath + path));
        } catch (Exception e) {
            throw e;
        }
        return buffer;
    }

    public static File loadAssetsFile(String path)   {
        return new File(assetsPath + path);
    }

    public static InputStream loadAssetsStream(String path)   {
        return new FileInputStream(loadAssetsFile(path));
    }

    public static bool hasAssets(String path)   {
        File f = loadAssetsFile(path);
        return f.exists();
    }

    static {
        init();
    }
}

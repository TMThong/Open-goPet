package vn.me.ui.common;

import java.io.DataInputStream;
import javax.microedition.lcdui.Image;
import vn.me.core.BaseCanvas;

/* renamed from: Class194  reason: default package */
 /* loaded from: gopet_repackage.jar:Class194.class */
public final class Resource {

    private static String bfs_filename;
    private static int[] Field1304;
    private static byte[] Field1303 = null;
    private static int Field1305 = 0;

    public static void Method402(String str) {
        Field1304 = null;
        bfs_filename = str;
    }

    public static Image setFileTable(String str, int i) {
        String str2 = bfs_filename;
        Method402(str);
        Image image = createImage(i);
        Method402(str2);
        return image;
    }

    private static void Method404() {
        if (Field1304 == null) {
            try {
                DataInputStream dataInputStream = null;
                if (BaseCanvas.iPlatformSDK == null) {
                    dataInputStream = new DataInputStream(ResourceManager.getResource(bfs_filename));
                } else {
                    dataInputStream = new DataInputStream(BaseCanvas.iPlatformSDK.getAssetSDK().load(bfs_filename));
                }
                int readInt = dataInputStream.readInt();
                Field1304 = new int[readInt];
                int i = (readInt << 2) + 4;
                for (int i2 = 0; i2 < readInt; i2++) {
                    Field1304[i2] = dataInputStream.readInt() + i;
                }
                dataInputStream.close();
            } catch (Exception unused) {
            }
        }
    }

    private static byte[] Method405(int i) {
        Method404();
        byte[] bArr = new byte[1];
        try {
            DataInputStream dataInputStream = null;
            if (BaseCanvas.iPlatformSDK == null) {
                dataInputStream = new DataInputStream(ResourceManager.getResource(bfs_filename));
            } else {
                dataInputStream = new DataInputStream(BaseCanvas.iPlatformSDK.getAssetSDK().load(bfs_filename));
            }
            bArr = new byte[Field1304[i + 1] - Field1304[i]];
            dataInputStream.skip(Field1304[i]);
            dataInputStream.read(bArr);
            dataInputStream.close();
        } catch (Exception unused) {
        }
        Field1305++;
        return bArr;
    }

    public static Image createImage(int i) {
        Method404();
        Image createImage = Image.createImage(1, 1);
        try {
            byte[] Method405 = Method405(i);
            createImage = Image.createImage(Method405, 0, Method405.length);
        } catch (Exception unused) {
            unused.printStackTrace();
        }
        Field1305++;
        return createImage;
    }
}

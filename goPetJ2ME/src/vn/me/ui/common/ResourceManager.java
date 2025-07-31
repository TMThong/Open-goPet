package vn.me.ui.common;

import java.io.IOException;
import vn.me.ui.Font;
import java.io.InputStream;
import javax.microedition.lcdui.Image;
import vn.me.core.BaseCanvas;

/* renamed from: Class195  reason: default package */
 /* loaded from: gopet_repackage.jar:Class195.class */
public final class ResourceManager {

    public static String Field1306 = "/mui.dat";
    public static FrameImage Field1307;
    public static Image Field1308;
    public static Font boldFont;
    public static Font defaultFont;

    public static void init() {
        Resource.Method402(Field1306);
        Field1307 = new FrameImage(Resource.createImage(7), 4);
        Image Method406 = Resource.createImage(2);
        Font.emotionsImage = Method406;
        Font.emoSize = Method406.getHeight();
        Resource.createImage(1);
    }

    public static void initFont() {
        boldFont = new Font(" 0123456789.,:!?()+*<>/-%abcdefghijklmnopqrstuvwxyzáàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđABCDEFGHIJKLMNOPQRSTUVWXYZĐ$ĂÁÂ=", new byte[]{4, 6, 5, 6, 6, 7, 6, 6, 6, 6, 6, 3, 3, 3, 4, 5, 4, 4, 6, 5, 8, 8, 6, 6, 10, 6, 7, 5, 7, 6, 4, 7, 7, 3, 4, 6, 3, 9, 7, 7, 7, 7, 5, 5, 4, 7, 6, 9, 6, 7, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 6, 6, 3, 3, 3, 5, 3, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 7, 7, 7, 7, 8, 7, 7, 7, 7, 7, 6, 6, 7, 7, 3, 5, 7, 6, 10, 8, 7, 7, 7, 6, 7, 7, 7, 7, 9, 7, 7, 8, 8, 6, 7, 7, 7, 9}, 13, Resource.createImage(4), 0);
        defaultFont = new Font(" 0123456789.,:!?()+*$#/-%abcdefghijklmnopqrstuvwxyzáàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđABCDEFGHIJKLMNOPQRSTUVWXYZĐ@Á=", new byte[]{4, 6, 4, 6, 6, 6, 6, 6, 6, 6, 6, 2, 2, 2, 2, 6, 4, 3, 6, 5, 6, 7, 3, 3, 10, 6, 6, 5, 6, 6, 4, 6, 6, 2, 2, 6, 2, 10, 6, 6, 6, 6, 4, 6, 3, 6, 5, 9, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 3, 2, 3, 4, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 6, 6, 6, 6, 6, 8, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 7, 7, 7, 8, 8, 7, 6, 8, 8, 2, 5, 8, 7, 8, 8, 8, 7, 8, 8, 7, 7, 8, 7, 9, 7, 7, 7, 8, 9, 7, 7, 9}, 14, Resource.createImage(3), 0);
    }

    public static InputStream getResource(String str)  {
        if (BaseCanvas.iPlatformSDK != null) {
            try {
                return BaseCanvas.iPlatformSDK.getAssetSDK().load(str);
            } catch (IOException ex) {
                ex.printStackTrace();
            }
        }
        return new byte[0].getClass().getResourceAsStream(str);
    }
}

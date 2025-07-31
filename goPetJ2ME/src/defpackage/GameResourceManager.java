package defpackage;

import vn.me.ui.common.FrameImage;
import vn.me.ui.common.Resource;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.Font;
import java.util.Hashtable;
import java.util.Vector;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;

/* renamed from: Class98  reason: default package */
/* loaded from: gopet_repackage.jar:Class98.class */
public final class GameResourceManager {
    public static Image[] Field584;
    public static Image[] Field585;
    private static Font defaultFont;
    public static Font boldFont;
    public static Font italicFont;
    public static Font smallFont;
    public static Font regularFont;
    public static Font largeFont;
    private static Font Field592;
    private static Font Field593;
    private static Font Field594;
    public static Image Field595;
    public static Image Field596;
    public static Image Field597;
    public static Image Field598;
    public static Image Field599;
    public static Image Field600;
    public static Image Field601;
    public static Image Field602;
    public static Image Field603;
    public static Image Field604;
    public static Image Field605;
    private static Image Field609;
    public static String avatarPath = "/avatar.dat";
    private static Hashtable Field606 = new Hashtable();
    private static Hashtable Field607 = new Hashtable();
    private static Vector Field608 = new Vector();

    public static void loadResources() {
        Resource.Method402("/common.dat");
        FrameImage class204 = new FrameImage(Resource.createImage(18), 3);
        Field584 = new Image[3];
        for (int i = 0; i < 3; i++) {
            Field584[i] = class204.getImage(i);
        }
        Field595 = Resource.createImage(24);
        Field596 = Resource.createImage(14);
        Resource.createImage(21);
        Resource.createImage(7);
        Image Method406 = Resource.createImage(17);
        Field597 = Method406;
        Field598 = Image.createImage(Method406, Field597.getWidth() - 14, 0, 14, 14, 0);
        Field599 = Resource.createImage(22);
        FrameImage class2042 = new FrameImage(Resource.createImage(23), 4);
        Field585 = new Image[4];
        for (int i2 = 0; i2 < 4; i2++) {
            Field585[i2] = class2042.getImage(i2);
        }
        Field600 = Resource.createImage(3);
        Field601 = Resource.createImage(6);
        Field602 = Resource.createImage(16);
        Field603 = Resource.createImage(0);
        Field604 = Resource.createImage(1);
        Class128.Method0();
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static void Method113() {
        italicFont = new Font(ResourceManager.defaultFont, 0, -12887656);
        boldFont = new Font(ResourceManager.boldFont, 0, -256);
        regularFont = new Font(ResourceManager.boldFont, 0, -12887656);
        smallFont = new Font(ResourceManager.defaultFont, 0, -1);
        defaultFont = new Font(Method117(), -1, -1508019);
        largeFont = new Font(ResourceManager.boldFont, 0, -65536);
    }

    public static Font getDefaultFont() {
        if (defaultFont == null) {
            defaultFont = new Font(Method117(), -1, -1508019);
        }
        return defaultFont;
    }

    public static Font getSpecialFont() {
        if (Field594 == null) {
            Field594 = new Font(" 0123456789.,:!?()-'/ABCDEFGHIJKLMNOPQRSTUVWXYZÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ", new byte[]{4, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 4, 4, 4, 4, 8, 6, 6, 6, 3, 7, 10, 10, 10, 10, 8, 8, 10, 10, 5, 8, 9, 8, 13, 11, 10, 10, 10, 10, 10, 9, 10, 10, 13, 11, 11, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10}, 20, Resource.setFileTable("/common.dat", 5), 0);
        }
        return Field594;
    }

    public static Font Method116() {
        if (Field593 == null) {
            Field593 = new Font(" 0123456789.,:!?()+*$#/-%abcdefghijklmnopqrstuvwxyzáàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđABCDEFGHIJKLMNOPQRSTUVWXYZĐ~Ớ", new byte[]{4, 7, 6, 7, 7, 8, 7, 7, 7, 7, 7, 4, 4, 4, 5, 6, 5, 5, 7, 6, 9, 9, 7, 7, 11, 7, 8, 6, 8, 7, 5, 8, 8, 4, 5, 7, 4, 10, 8, 8, 8, 8, 6, 6, 5, 8, 7, 10, 7, 8, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 7, 7, 4, 4, 4, 6, 4, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 8, 8, 8, 8, 9, 8, 8, 8, 8, 8, 7, 7, 8, 8, 4, 6, 8, 7, 11, 9, 8, 8, 8, 7, 8, 8, 8, 8, 10, 8, 8, 9, 9, 7, 8}, 15, Resource.setFileTable("/common.dat", 4), -1);
        }
        return Field593;
    }

    public static Font Method117() {
        if (Field592 == null) {
            Field592 = new Font(" 0123456789.+-%$:ABCDEFGHIJKLMNOPQRSTUVWXYZ/", new byte[]{3, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 4, 5, 5, 7, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 7, 6, 5, 5, 5, 5, 5, 5, 5, 5, 7, 5, 5, 5, 7}, 8, Resource.setFileTable("/common.dat", 9), -1);
        }
        return Field592;
    }

    public static Font Method118(int i) {
        Integer num = new Integer(i);
        Font class171 = (Font) Field606.get(num);
        if (class171 != null) {
            return class171;
        }
        Font class1712 = new Font(Method117(), -1, i);
        Field606.put(num, class1712);
        return class1712;
    }

    public static void Method119() {
        Field606.clear();
    }

    public static Image Method120() {
        if (Field609 == null) {
            Image createImage = Image.createImage(1, 1);
            Field609 = createImage;
            Graphics graphics = createImage.getGraphics();
            graphics.setColor(128, 128, 128);
            graphics.fillRect(0, 0, 10, 10);
        }
        return Field609;
    }

    public static void releaseImgCache() {
        Field607.clear();
    }

    public static Image loadResourceImg(String str, byte b) {
        String stringBuffer = new StringBuffer("gui").append((int) b).append("_").append(str).toString();
        Image image = (Image) Field607.get(stringBuffer);
        if (image != null) {
            return image;
        }
        if (Field608.contains(stringBuffer)) {
            return null;
        }
        GlobalService.requestImg(str, b);
        Field608.addElement(stringBuffer);
        return null;
    }

    public static void addGuiderImg(String str, Image image, byte b) {
        String stringBuffer = new StringBuffer("gui").append((int) b).append("_").append(str).toString();
        Field607.put(stringBuffer, image);
        Field608.removeElement(stringBuffer);
    }

    public static Font Method124(byte b) {
        switch (b) {
            case 0:
                return ResourceManager.boldFont;
            case 1:
                return ResourceManager.defaultFont;
            case 2:
                return getDefaultFont();
            case 3:
                return getSpecialFont();
            case 4:
                return Method117();
            case 5:
                return Method116();
            case 6:
                return italicFont;
            case 7:
                return boldFont;
            case 8:
                return regularFont;
            case 9:
                return smallFont;
            case 10:
                return largeFont;
            default:
                return null;
        }
    }
}

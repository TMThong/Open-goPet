package defpackage;

import vn.me.ui.common.Resource;
import vn.me.core.BaseCanvas;
import java.util.Vector;
import javax.microedition.lcdui.Image;

/* renamed from: Class128  reason: default package */
/* loaded from: gopet_repackage.jar:Class128.class */
public final class Class128 extends mObject {
    public static Image Field835;
    private static int Field843;
    private static int Field844;
    private static int Field845;
    private static int Field846;
    private static int Field847;
    private static int Field848;
    private static int Field849;
    private static Image[][] Field827 = new Image[2][2];
    private static Image[][] Field828 = new Image[2][2];
    private static Image[][] Field829 = new Image[2][2];
    private static Image[][] Field830 = new Image[2][2];
    private static Image[][] Field831 = new Image[2][2];
    private static Image[][] Field832 = new Image[2][2];
    private static Image[] Field833 = new Image[2];
    private static Image[] Field834 = new Image[2];
    private static short[][][] Field836 = {
        new short[][]{
            new short[]{26, 42, 22}, 
            new short[]{24, 42, 24}}, 
        new short[][]{
            new short[]{22, 5, 27}, 
            new short[]{0, 20, 67}}, 
        new short[][]{
            new short[]{23, 43, 26}, 
            new short[]{23, 43, 26}}, 
        new short[][]{
            new short[]{9, 0, 47}, 
            new short[]{23, 42, 26}}, 
        new short[][]{
            new short[]{26, 41, 23}}, 
        new short[][]{
            new short[]{24, 43, 25}}, 
        new short[][]{
            new short[]{25, 41, 23}}
    };
    private static final int Field837 = 27;
    private static final int Field838 = 10;
    private static final int Field839 = 22;
    private static final int[] Field840 = {0, 21, 20};
    private static final int Field841 = 60;
    private static final byte[][] Field842 = {new byte[]{0, 0, 33, 42}, new byte[]{0, 42, 11, 14}, new byte[]{11, 42, 16, 14}};

    public static void drawChar(Vector vector, byte b, int i, int i2, int i3, boolean z, int i4, int i5) {
        int i6 = BaseCanvas.ticks + i4;
        boolean z2 = z && i6 % 8 < 4;
        boolean z3 = z2;
        char c = z2 ? (char) 2 : (char) 1;
        int i7 = z ? 0 : (i6 >> 5) % 2;
        int i8 = i - Field837;
        int i9 = i2 - 74;
        BaseCanvas.g.translate(i8, i9);
        BaseCanvas.g.drawImage(Field835, Field837, 68, 17);
        if (i3 == 1) {
            BaseCanvas.g.drawRegion(Field833[0], Field842[0][0], Field842[0][1], Field842[0][2], Field842[0][3], 0, Field838, Field839 + i7, 0);
            BaseCanvas.g.drawRegion(Field833[0], Field842[c][0], Field842[c][1], Field842[c][2], Field842[c][3], 0, Field840[c], Field841, 0);
        } else {
            BaseCanvas.g.drawRegion(Field833[1], (33 - Field842[0][0]) - Field842[0][2], Field842[0][1], Field842[0][2], Field842[0][3], 0, (54 - Field838) - 33, Field839 + i7, 0);
            BaseCanvas.g.drawRegion(Field833[1], (33 - Field842[c][0]) - Field842[c][2], Field842[c][1], Field842[c][2], Field842[c][3], 0, (54 - Field840[c]) - Field842[c][2], Field841, 0);
        }
        Vector vector2 = new Vector();
        Method150(vector2, b);
        GameResourceManager.Method120();
        for (int i10 = 0; i10 < vector2.size(); i10++) {
            Class139 class139 = (Class139) vector2.elementAt(i10);
            Image image = i3 == 1 ? class139.Field962 : class139.Field963;
            Field843 = class139.Field965;
            Field844 = class139.Field966;
            Field845 = image.getWidth();
            Field846 = image.getHeight();
            if (class139.Field964 == 4 && (i6 / 5) % 16 == 13) {
                if (i3 == 1) {
                    BaseCanvas.g.drawImage(Field834[0], 21, i7 + 38, 0);
                } else {
                    BaseCanvas.g.drawRegion(Field834[1], 0, 0, 18, 11, 0, 15, i7 + 38, 0);
                }
            } else if (class139.Field964 == 8 || class139.Field964 == 9) {
                Field847 = 54 - Field843;
                if (class139.Field964 == 9) {
                    Field848 = (Field845 - Field847) - 3;
                } else {
                    Field848 = (Field845 - Field847) + 1;
                }
                Field849 = Field845 - Field848;
                if (i3 == 1) {
                    if (z3) {
                        BaseCanvas.g.drawRegion(image, Field848, 0, Field849, Field846, 0, 0, Field844, 0);
                    } else {
                        BaseCanvas.g.drawRegion(image, 0, 0, Field847, Field846, 0, Field843, Field844, 0);
                    }
                } else if (z3) {
                    BaseCanvas.g.drawRegion(image, 0, 0, Field847, Field846, 0, (Field843 + Field847) - Field849, Field844, 0);
                } else {
                    BaseCanvas.g.drawRegion(image, Field848, 0, Field849, Field846, 0, Field847 - Field849, Field844, 0);
                }
            } else if (i3 == 1) {
                BaseCanvas.g.drawImage(image, Field843, Field844 + i7, 0);
            } else {
                BaseCanvas.g.drawImage(image, (54 - Field843) - Field845, Field844 + i7, 0);
            }
        }
        BaseCanvas.g.translate(-i8, -i9);
    }

    private static void Method150(Vector vector, int i) {
        if (vector == null) {
            return;
        }
        if (i == -1) {
            i = 0;
        }
        boolean[] zArr = {false, false, false, false, false, false};
        int size = vector.size();
        for (int i2 = 0; i2 < size; i2++) {
            switch (((Class139) vector.elementAt(i2)).Field964) {
                case 2:
                    zArr[1] = true;
                    break;
                case 3:
                    zArr[4] = true;
                    break;
                case 4:
                    zArr[0] = true;
                    break;
                case 7:
                    zArr[3] = true;
                    break;
                case 8:
                    zArr[2] = true;
                    break;
                case 9:
                    zArr[5] = true;
                    break;
            }
        }
        if (!zArr[0]) {
            vector.addElement(Method151(4, (byte) i));
        }
        if (!zArr[1]) {
            vector.addElement(Method151(2, (byte) i));
        }
        if (!zArr[2]) {
            vector.addElement(Method151(8, (byte) i));
        }
        if (!zArr[3]) {
            vector.addElement(Method151(7, (byte) i));
        }
        if (!zArr[4]) {
            vector.addElement(Method151(3, (byte) i));
        }
        if (!zArr[5]) {
            vector.addElement(Method151(9, (byte) i));
        }
        int size2 = vector.size();
        for (int i3 = 0; i3 < size2 - 1; i3++) {
            for (int i4 = i3 + 1; i4 < size2; i4++) {
                Class139 class139 = (Class139) vector.elementAt(i3);
                Class139 class1392 = (Class139) vector.elementAt(i4);
                if (class1392.Field967 < class139.Field967) {
                    vector.setElementAt(class1392, i3);
                    vector.setElementAt(class139, i4);
                }
            }
        }
    }

    private static Class139 Method151(int i, byte b) {
        switch (i) {
            case 2:
                return new Class139(2, b == 0 ? 0 : 7, 15, -5, Field828[b][0], Field828[b][1]);
            case 3:
                return new Class139(3, b == 0 ? -1 : 0, 14, -4, Field829[b][0], Field829[b][1]);
            case 4:
                return new Class139(4, 18, b == 0 ? 35 : 36, -4, Field827[b][0], Field827[b][1]);
            case 5:
            case 6:
            default:
                return null;
            case 7:
                return new Class139(7, b == 0 ? 22 : 18, b == 0 ? 51 : 50, -2, Field831[b][0], Field831[b][1]);
            case 8:
                return new Class139(8, b == 0 ? 22 : 21, 59, -3, Field830[b][0], Field830[b][1]);
            case 9:
                return new Class139(9, 21, 65, -4, Field832[b][0], Field832[b][1]);
        }
    }

    public static void Method0() {
        Resource.Method402(GameResourceManager.avatarPath);
        Field833[0] = Resource.createImage(0);
        Field833[1] = Ulti.mirror(Field833[0]);
        Field834[0] = Resource.createImage(1);
        Field834[1] = Ulti.mirror(Field834[0]);
        Field835 = Resource.createImage(15);
        Field827[0][0] = Resource.createImage(8);
        Field827[0][1] = Ulti.mirror(Field827[0][0]);
        Field827[1][0] = Resource.createImage(7);
        Field827[1][1] = Ulti.mirror(Field827[1][0]);
        Field828[0][0] = Resource.createImage(14);
        Field828[0][1] = Ulti.mirror(Field828[0][0]);
        Field828[1][0] = Resource.createImage(13);
        Field828[1][1] = Ulti.mirror(Field828[1][0]);
        Field830[0][0] = Resource.createImage(12);
        Field830[0][1] = Ulti.mirror(Field830[0][0]);
        Field830[1][0] = Resource.createImage(11);
        Field830[1][1] = Ulti.mirror(Field830[1][0]);
        Field831[0][0] = Resource.createImage(4);
        Field831[0][1] = Ulti.mirror(Field831[0][0]);
        Field831[1][0] = Resource.createImage(3);
        Field831[1][1] = Ulti.mirror(Field831[1][0]);
        Field829[0][0] = Resource.createImage(10);
        Field829[0][1] = Ulti.mirror(Field829[0][0]);
        Field829[1][0] = Resource.createImage(9);
        Field829[1][1] = Ulti.mirror(Field829[1][0]);
        Field832[0][0] = Resource.createImage(6);
        Field832[0][1] = Ulti.mirror(Field832[0][0]);
        Field832[1][0] = Resource.createImage(5);
        Field832[1][1] = Ulti.mirror(Field832[1][0]);
    }

    public static void drawChar(Image image, int i, int y_dst, int i3, boolean z) {
        BaseCanvas.g.drawRegion(image, (z && (BaseCanvas.ticks + 1000) % 8 < 4 ? 1 : 0) * (image.getWidth() >> 1), 0, image.getWidth() >> 1, image.getHeight(), i3 == 0 ? 2 : 0, i, y_dst, 33);
    }
}

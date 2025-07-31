package defpackage;

import java.util.Random;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;

/* renamed from: Class20  reason: default package */
/* loaded from: gopet_repackage.jar:Class20.class */
public final class Ulti {
    private static long Field80;
    private static Random r = new Random();

    public static String formatNumber(long j) {
        String str;
        String str2 = "";
        String stringBuffer = new StringBuffer().append(j).toString();
        while (true) {
            str = stringBuffer;
            if (str.length() <= 3) {
                break;
            }
            str2 = str2.length() == 0 ? str.substring(str.length() - 3, str.length()) : new StringBuffer().append(str.substring(str.length() - 3, str.length())).append(".").append(str2).toString();
            stringBuffer = str.substring(0, str.length() - 3);
        }
        if (str2.length() == 0) {
            return str;
        }
        return (j >= 0 || !str.equals("-")) ? new StringBuffer().append(str).append(".").append(str2).toString() : new StringBuffer().append(str).append(str2).toString();
    }

    public static int Method369(int i) {
        return i > 0 ? i : -i;
    }

    public static int Method370(int i) {
        Field80++;
        r.setSeed(Field80);
        return Math.abs(r.nextInt()) % i;
    }

    public static String Method371(String str, int i) {
        return str.length() <= 7 ? str : str.substring(0, 7);
    }

    public static boolean Method372(int i, int i2) {
        return ((i >> i2) & 1) != 0;
    }

    private static byte[] Method373(long j) {
        int i = (int) (j / 60);
        int i2 = i / 60;
        return new byte[]{(byte) (i2 % 24), (byte) (i % 60), (byte) (j % 60), (byte) (i2 / 24)};
    }

    public static String Method374(long j) {
        byte[] Method373 = Method373(j);
        String stringBuffer = Method373[1] == 0 ? new StringBuffer().append("").append("00:").toString() : Method373[1] < 10 ? new StringBuffer().append("").append("0").append((int) Method373[1]).append(":").toString() : new StringBuffer().append("").append((int) Method373[1]).append(":").toString();
        return Method373[2] == 0 ? new StringBuffer().append(stringBuffer).append("00").toString() : Method373[2] < 10 ? new StringBuffer().append(stringBuffer).append("0").append((int) Method373[2]).toString() : new StringBuffer().append(stringBuffer).append((int) Method373[2]).toString();
    }

    public static String Method375(long j) {
        byte[] Method373 = Method373(j);
        String stringBuffer = Method373[0] == 0 ? new StringBuffer().append("").append("00:").toString() : Method373[0] < 10 ? new StringBuffer().append("").append("0").append((int) Method373[0]).append(":").toString() : new StringBuffer().append("").append((int) Method373[0]).append(":").toString();
        String stringBuffer2 = Method373[1] == 0 ? new StringBuffer().append(stringBuffer).append("00:").toString() : Method373[1] < 10 ? new StringBuffer().append(stringBuffer).append("0").append((int) Method373[1]).append(":").toString() : new StringBuffer().append(stringBuffer).append((int) Method373[1]).append(":").toString();
        return Method373[2] == 0 ? new StringBuffer().append(stringBuffer2).append("00").toString() : Method373[2] < 10 ? new StringBuffer().append(stringBuffer2).append("0").append((int) Method373[2]).toString() : new StringBuffer().append(stringBuffer2).append((int) Method373[2]).toString();
    }

    public static void Method376(int i, int i2, int i3, int i4, Graphics graphics) {
        graphics.translate(i, i2);
        graphics.fillRect(1, 1, 1, 1);
        graphics.fillRect(2, 0, i3 - 4, 1);
        graphics.fillRect(i3 - 2, 1, 1, 1);
        graphics.fillRect(0, 2, 1, i4 - 4);
        graphics.fillRect(i3 - 1, 2, 1, i4 - 4);
        graphics.fillRect(1, i4 - 2, 1, 1);
        graphics.fillRect(i3 - 2, i4 - 2, 1, 1);
        graphics.fillRect(2, i4 - 1, i3 - 4, 1);
        graphics.translate(-i, -i2);
    }

    public static void Method377(int i, int i2, int i3, int i4, Graphics graphics) {
        graphics.fillRect(i + 1, i2 + 1, i3 - 2, i4 - 2);
        Method376(i, i2, i3, i4, graphics);
    }

    public static Image mirror(Image image) {
        int width = image.getWidth();
        int height = image.getHeight();
        int i = width >> 1;
        int[] iArr = new int[width * height];
        image.getRGB(iArr, 0, width, 0, 0, width, height);
        for (int i2 = 0; i2 < height; i2++) {
            int i3 = i2 * width;
            int i4 = (i3 + width) - 1;
            for (int i5 = 0; i5 < i; i5++) {
                int i6 = iArr[i3 + i5];
                iArr[i3 + i5] = iArr[i4 - i5];
                iArr[i4 - i5] = i6;
            }
        }
        return Image.createRGBImage(iArr, width, height, true);
    }

    public static boolean Method379(Class198 class198, Class198 class1982, Class198 class1983) {
        return (class198.Field1315 == class1982.Field1315 && class1982.Field1315 == class1983.Field1315 && ((class198.Field1314 <= class1982.Field1314 && class1982.Field1314 <= class1983.Field1314) || (class1983.Field1314 <= class1982.Field1314 && class1982.Field1314 <= class198.Field1314))) || (class198.Field1314 == class1982.Field1314 && class1982.Field1314 == class1983.Field1314 && ((class198.Field1315 <= class1982.Field1315 && class1982.Field1315 <= class1983.Field1315) || (class1983.Field1315 <= class1982.Field1315 && class1982.Field1315 <= class198.Field1315)));
    }

    private static int Method380(long j, int i) {
        int length = String.valueOf(j).length() - 2;
        for (int i2 = 0; i2 < length; i2++) {
            j /= 10;
        }
        return (int) j;
    }

    public static String Method381(long j) {
        if (j >= 1000000000) {
            int i = (int) (j / 1000000000);
            return new StringBuffer().append(i).append(".").append(Method380((int) (j - (i * 1000000000)), 2)).append("G").toString();
        } else if (j >= 1000000) {
            int i2 = (int) (j / 1000000);
            return new StringBuffer().append(i2).append(".").append(Method380((int) (j - (i2 * 1000000)), 2)).append("M").toString();
        } else if (j >= 1000) {
            int i3 = (int) (j / 1000);
            return new StringBuffer().append(i3).append(".").append(Method380((int) (j - (i3 * 1000)), 2)).append("k").toString();
        } else {
            return String.valueOf(j);
        }
    }
}

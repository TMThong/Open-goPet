package vn.me.ui.common;

import java.util.Enumeration;
import java.util.Hashtable;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;

/* renamed from: Class191  reason: default package */
 /* loaded from: gopet_repackage.jar:Class191.class */
public final class Effects {

    private static boolean Field1276 = true;
    public static boolean isRadialGradientCache = true;
    private static Hashtable linearGradientCache;
    private static Hashtable radialGradientCache;

    public static Image show0(Image image, int i) {
        int width = (image.getWidth() * i) / image.getHeight();
        int width2 = image.getWidth();
        int height = image.getHeight();
        if (width2 == width && height == i) {
            return image;
        }
        int[] iArr = new int[width * i];
        int i2 = (height << 16) / i;
        int i3 = (width2 << 16) / width;
        int i4 = i3 >> 1;
        int i5 = i2 >> 1;
        for (int i6 = 0; i6 < i; i6++) {
            int[] Method442 = getRGB(image, 0, i5 >> 16, width2, 1);
            for (int i7 = 0; i7 < width; i7++) {
                int i8 = i4 >> 16;
                int i9 = i7 + (i6 * width);
                if (i9 >= 0 && i9 < iArr.length && i8 < Method442.length) {
                    iArr[i9] = Method442[i8];
                }
                i4 += i3;
            }
            i5 += i2;
            i4 = i3 >> 1;
        }
        return Image.createRGBImage(iArr, width, i, true);
    }

    public static void show1(Graphics graphics, int i, int i2, int i3, int i4, int i5, int i6, boolean z) {
        if (i5 < 0 || i6 < 0) {
            return;
        }
        if (!Field1276) {
            show3(graphics, i, i2, i3, i4, i5, i6, z);
            return;
        }
        Image show8 = show8(linearGradientCache, i, i2, 0, i5, i6, z ? 0 : 1);
        Image image = show8;
        if (show8 == null) {
            Image createImage = Image.createImage(i5, i6);
            image = createImage;
            show3(createImage.getGraphics(), i, i2, 0, 0, i5, i6, z);
            if (linearGradientCache == null) {
                linearGradientCache = new Hashtable();
            }
            show7(image, linearGradientCache, i, i2, 0, i5, i6, z ? 0 : 1);
        }
        graphics.drawImage(image, i3, i4, 20);
    }

    public static void show2(Graphics graphics, int i, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10) {
        if (!isRadialGradientCache) {
            graphics.setColor(i2);
            graphics.fillRect(0, 0, i5, i6);
            show4(graphics, i, i2, 0, -10, i9, i10);
            return;
        }
        Image show8 = show8(radialGradientCache, i, i2, 1, i9, i10, 0);
        if (show8 != null) {
            graphics.drawImage(show8, 0, 0, 20);
            return;
        }
        Image createImage = Image.createImage(i5, i6);
        Graphics graphics2 = createImage.getGraphics();
        graphics2.setColor(i2);
        graphics2.fillRect(0, 0, i5, i6);
        show4(graphics2, i, i2, 0, -10, i9, i10);
        graphics.drawImage(createImage, 0, 0, 20);
        if (radialGradientCache == null) {
            radialGradientCache = new Hashtable();
        }
        show7(createImage, radialGradientCache, i, i2, 1, i9, i10, 0);
    }

    private static void show3(Graphics graphics, int i, int i2, int i3, int i4, int i5, int i6, boolean z) {
        int i7 = (i >> 16) & 255;
        int i8 = (i >> 8) & 255;
        int i9 = i & 255;
        int i10 = (i2 >> 16) & 255;
        int i11 = (i2 >> 8) & 255;
        int i12 = i2 & 255;
        int i13 = i3 + i5;
        int i14 = i4 + i6;
        if (z) {
            for (int i15 = 0; i15 < i5; i15++) {
                show5(graphics, i7, i8, i9, i10, i11, i12, i5, i15);
                graphics.drawLine(i3 + i15, i4, i3 + i15, i14);
            }
            return;
        }
        for (int i16 = 0; i16 < i6; i16++) {
            show5(graphics, i7, i8, i9, i10, i11, i12, i6, i16);
            graphics.drawLine(i3, i4 + i16, i13, i4 + i16);
        }
    }

    private static void show4(Graphics graphics, int i, int i2, int i3, int i4, int i5, int i6) {
        int i7 = (i >> 16) & 255;
        int i8 = (i >> 8) & 255;
        int i9 = i & 255;
        int i10 = (i2 >> 16) & 255;
        int i11 = (i2 >> 8) & 255;
        int i12 = i2 & 255;
        while (i5 > 0 && i6 > 0) {
            show5(graphics, i7, i8, i9, i10, i11, i12, i6, i6);
            graphics.fillArc(i3, i4, i5, i6, 0, 360);
            i3++;
            i4++;
            i5 -= 2;
            i6 -= 2;
        }
    }

    private static void show5(Graphics graphics, int i, int i2, int i3, int i4, int i5, int i6, int i7, int i8) {
        graphics.setColor(((show6(i, i4, i7, i8) << 16) & 16711680) | ((show6(i2, i5, i7, i8) << 8) & 65280) | (show6(i3, i6, i7, i8) & 255));
    }

    private static int show6(int i, int i2, int i3, int i4) {
        if (i == i2) {
            return i;
        }
        int abs = (Math.abs(i - i2) * ((i4 << 10) / i3)) >> 10;
        return i > i2 ? i - abs : i + abs;
    }

    private static void show7(Image image, Hashtable hashtable, int i, int i2, int i3, int i4, int i5, int i6) {
        hashtable.put(new int[]{i, i2, i3, i4, i5, i6}, image);
    }

    private static Image show8(Hashtable hashtable, int i, int i2, int i3, int i4, int i5, int i6) {
        if (hashtable != null) {
            Enumeration keys = hashtable.keys();
            while (keys.hasMoreElements()) {
                int[] iArr = (int[]) keys.nextElement();
                if (iArr[0] == i && iArr[1] == i2 && iArr[2] == i3 && iArr[3] == i4 && iArr[4] == i5 && iArr[5] == i6) {
                    return (Image) hashtable.get(iArr);
                }
            }
            return null;
        }
        return null;
    }

    public static Image changeColor(Image image, int toColor) {
        int w = image.getWidth();
        int h = image.getHeight();
        int[] argb = getRGB(image);
        int i = argb.length;
        while (true) {
            i--;
            if (i >= 0) {
                if (((argb[i] >> 24) & 255) > 0) {
                    argb[i] = toColor;
                }
            } else {
                return Image.createRGBImage(argb, w, h, true);
            }
        }
    }

    public static Image changeColor(Image image, int i, int i2) {
        int width = image.getWidth();
        int height = image.getHeight();
        int[] Method441 = getRGB(image);
        if ((i >>> 24) <= 0) {
            int length = Method441.length;
            while (true) {
                length--;
                if (length < 0) {
                    break;
                } else if ((Method441[length] >>> 24) > 0) {
                    Method441[length] = i2;
                }
            }
        } else {
            int length2 = Method441.length;
            while (true) {
                length2--;
                if (length2 < 0) {
                    break;
                } else if (Method441[length2] == i) {
                    Method441[length2] = i2;
                }
            }
        }
        return Image.createRGBImage(Method441, width, height, true);
    }

    private static int[] getRGB(Image image) {
        return getRGB(image, 0, 0, image.getWidth(), image.getHeight());
    }

    private static int[] getRGB(Image image, int x, int y, int widght, int height) {
        int length = widght * height;
        int[] mask1 = new int[length];
        int[] mask2 = new int[length];
        Image createImage = Image.createImage(widght, height);
        Graphics graphics = createImage.getGraphics();
        graphics.setColor(0);
        graphics.fillRect(0, 0, widght, height);
        graphics.drawRegion(image, 0, y, widght, height, 0, 0, 0, 20);
        createImage.getRGB(mask1, 0, widght, 0, 0, widght, height);
        graphics.setColor(16777215);
        graphics.fillRect(0, 0, widght, height);
        graphics.drawRegion(image, 0, y, widght, height, 0, 0, 0, 20);
        createImage.getRGB(mask2, 0, widght, 0, 0, widght, height);
        int[] imageRGB = new int[length];
        image.getRGB(imageRGB, 0, widght, 0, y, widght, height);
        for (int i = 0; i < length; i++) {
            if ((mask1[i] & 16777215) == 0 && (mask2[i] & 16777215) == 16777215) {
                imageRGB[i] = 0;
            }
        }
        return imageRGB;
    }

    public static void clearCache() {
        if (linearGradientCache != null) {
            linearGradientCache.clear();
            linearGradientCache = null;
        }
        if (radialGradientCache != null) {
            radialGradientCache.clear();
            radialGradientCache = null;
        }
    }
}

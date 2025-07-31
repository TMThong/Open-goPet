package defpackage;

import javax.microedition.lcdui.Graphics;

/* renamed from: Class18  reason: default package */
/* loaded from: gopet_repackage.jar:Class18.class */
public final class Class18 {
    private String[] Field66;
    private int Field67;
    private int Field68;
    private int Field69;

    public final void Method317(String str) {
        this.Field69 = GameResourceManager.italicFont.getHeight() - 1;
        this.Field67 = GameResourceManager.italicFont.getWidth(str);
        if (this.Field67 > 80) {
            int i = this.Field67;
            while (true) {
                if (i < 40) {
                    break;
                }
                if (i / ((this.Field67 * this.Field69) / i) <= 2) {
                    this.Field67 = i;
                    break;
                }
                i -= 10;
            }
           
            this.Field66 = GameResourceManager.italicFont.wrap(str, this.Field67);
            this.Field67 = 0;
            for (int i2 = 0; i2 < this.Field66.length; i2++) {
                int Method330 = GameResourceManager.italicFont.getWidth(this.Field66[i2]);
                if (this.Field67 < Method330) {
                    this.Field67 = Method330;
                }
            }
            this.Field68 = (this.Field66.length * this.Field69) + 8;
        } else {
            this.Field66 = new String[1];
            this.Field66[0] = str;
            this.Field68 = this.Field69 + 8;
        }
        this.Field67 = this.Field67 < 30 ? 36 : this.Field67 + 6;
    }

    public final void Method318(Graphics graphics, int i, int i2) {
        if (this.Field66 == null) {
            return;
        }
        int i3 = i - (this.Field67 >> 1);
        int i4 = (i2 - this.Field68) - 6;
        graphics.translate(i3, i4);
        Method320(graphics, this.Field67, this.Field68);
        for (int i5 = 0; i5 < this.Field66.length; i5++) {
            if (i5 < this.Field66.length) {
                GameResourceManager.italicFont.drawString(graphics, this.Field66[i5], this.Field67 >> 1, 2 + (i5 * this.Field69), 17);
            }
        }
        Method319(graphics, i3, i4, this.Field67, this.Field68);
        graphics.translate(-i3, -i4);
    }

    public static void Method319(Graphics graphics, int i, int i2, int i3, int i4) {
        graphics.setColor(0);
        graphics.drawLine(6, 0, i3 - 6, 0);
        graphics.fillRect(0, 6, 1, i4 - 12);
        graphics.fillRect(6, i4 - 1, i3 - 12, 1);
        graphics.fillRect(i3 - 1, 6, 1, i4 - 12);
        graphics.drawRegion(GameResourceManager.Field595, 0, 0, 6, 6, 0, 0, 0, 0);
        graphics.drawRegion(GameResourceManager.Field595, 7, 0, 6, 6, 0, i3 - 6, 0, 0);
        graphics.drawRegion(GameResourceManager.Field595, 0, 6, 6, 6, 0, 0, i4 - 6, 0);
        graphics.drawRegion(GameResourceManager.Field595, 7, 6, 6, 6, 0, i3 - 6, i4 - 6, 0);
        graphics.drawRegion(GameResourceManager.Field595, 2, 12, 9, 6, 0, i3 >> 1, i4 - 1, 17);
    }

    public static void Method320(Graphics graphics, int i, int i2) {
        graphics.setColor(16777215);
        graphics.fillRect(6, 0, i - 12, 3);
        graphics.fillRect(6, i2 - 3, i - 12, 3);
        graphics.fillRect(0, 6, 3, i2 - 12);
        graphics.fillRect(i - 3, 6, 3, i2 - 12);
        graphics.fillRect(3, 3, i - 6, i2 - 6);
    }
}

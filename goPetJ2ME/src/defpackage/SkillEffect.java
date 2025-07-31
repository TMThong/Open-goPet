package defpackage;

import javax.microedition.lcdui.Graphics;

/* renamed from: Class187  reason: default package */
 /* loaded from: gopet_repackage.jar:Class187.class */
public final class SkillEffect {

    public Class186 Field1258;
    public int Field1259;
    private boolean Field1260 = true;
    private boolean Field1261 = true;
    private long Field1262;

    public SkillEffect(Class186 class186) {
        this.Field1258 = class186;
    }

    public final void Method344() {
        this.Field1262 = System.currentTimeMillis();
        this.Field1259 = 0;
    }

    public final void Method345(Graphics graphics, int i, int i2, int i3) {
        Method348(graphics, this.Field1259, i, i2, i3);
    }

    public final void update(long j) {
       
        if (!this.Field1260 || j - this.Field1262 <= this.Field1258.Field1255[this.Field1259]) {
            return;
        }
        this.Field1259++;
        this.Field1262 = j;
        if (this.Field1259 > this.Field1258.Field1254.length - 1) {
            this.Field1259 = 0;
            if (this.Field1261) {
                return;
            }
            this.Field1260 = false;
        }
    }

    public final void draw(Graphics graphics, int i, int i2, int i3) {
        if (this.Field1260) {
            long currentTimeMillis = System.currentTimeMillis();
            if (currentTimeMillis - this.Field1262 > this.Field1258.Field1255[this.Field1259]) {
                this.Field1259++;
                this.Field1262 = currentTimeMillis;
                if (this.Field1259 > this.Field1258.Field1254.length - 1) {
                    this.Field1259 = 0;
                    if (!this.Field1261) {
                        this.Field1260 = false;
                    }
                }
            }
        }
        Method348(graphics, this.Field1259, i, i2, 0);
    }

    private void Method348(Graphics g, int i, int i2, int i3, int i4) {
        Class189 class189 = this.Field1258.skillAniData.Field1264[this.Field1258.Field1254[i]];
        for (int i5 = 0; i5 < class189.Field1267.length; i5++) {
            Class190 class190 = this.Field1258.skillAniData.Field1265[class189.Field1267[i5]];
            int i6 = (i4 & 2) == 0 ? class189.Field1268[i5] + i2 : ((-class189.Field1268[i5]) - class190.Field1274) + i2;
            int i7 = (i4 & 1) == 0 ? class189.Field1269[i5] + i3 : ((-class189.Field1269[i5]) - class190.Field1275) + i3;
            byte b = class189.Field1270[i5];
            if (i4 == 2) {
                b = (byte) ((class189.Field1270[i5] + i4) % 4);
            } else if (i4 == 1) {
                b = (byte) (class189.Field1270[i5] ^ i4);
            }
            g.drawRegion(this.Field1258.imgSkill, class190.Field1272, class190.Field1273, class190.Field1274, class190.Field1275, b, i6, i7, 0);
        }
    }
}

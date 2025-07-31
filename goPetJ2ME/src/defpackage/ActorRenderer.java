package defpackage;

import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;

/* renamed from: Class55  reason: default package */
/* loaded from: gopet_repackage.jar:Class55.class */
public abstract class ActorRenderer {
    private ActorFactory actorFactory;
    private int Field360;
    private int Field361;
    private int Field362;
    private int Field363;
    private int Field364;
    private int Field365;

    public ActorRenderer(ActorFactory class59) {
        this.actorFactory = class59;
    }

    public final void Method472(int i) {
        this.Field360 = 0;
        int i2 = this.Field360 << 1;
        this.Field362 = (this.actorFactory.Field398[i2 + 1] - this.actorFactory.Field398[i2]) + 1;
        Method473(0);
        Method481();
    }

    public final void Method473(int i) {
        this.Field361 = i;
        this.Field364 = 0;
        this.Field365 = this.actorFactory.Field399[(this.actorFactory.Field398[this.Field360 << 1] + i) << 2];
    }

    public final void Method474(int i) {
        this.Field363 = -1;
    }

    public final void Method475() {
        if (this.Field364 < this.actorFactory.Field399[((this.actorFactory.Field398[this.Field360 << 1] + this.Field361) << 2) + 1]) {
            this.Field364++;
            return;
        }
        if (this.Field361 >= this.Field362 - 1) {
            if (this.Field363 < 0) {
                Method482();
                return;
            }
            this.Field361 = this.Field363 - 1;
        }
        Method473(this.Field361 + 1);
        int i = this.actorFactory.Field398[this.Field360 << 1] + this.Field361;
        short s = this.actorFactory.Field399[(i << 2) + 2];
        short s2 = this.actorFactory.Field399[(i << 2) + 3];
        Method479(Method480() == 1 ? -s : s, Method480() == 2 ? -s2 : s2);
        this.Field364++;
    }

    public final void paint(Graphics graphics) {
        short s;
        short s2;
        short s3;
        short s4;
        short s5;
        short s6;
        short s7;
        short s8;
        byte b = 0;
        short s9;
        short s10;
        short height;
        short s11;
        int i = this.actorFactory.Field407[this.Field365 << 1];
        short s12 = this.actorFactory.Field407[(this.Field365 << 1) + 1];
        int clipX = graphics.getClipX();
        int clipY = graphics.getClipY();
        int clipWidth = graphics.getClipWidth();
        int clipHeight = graphics.getClipHeight();
        while (i < s12) {
            char c = (char) i;
            int i2 = i + 1;
            short s13 = this.actorFactory.Field400[c];
            int i3 = i2 + 1;
            short s14 = this.actorFactory.Field400[i2];
            int i4 = i3 + 1;
            short s15 = this.actorFactory.Field400[i3];
            i = i4 + 1;
            byte b2 = (byte) this.actorFactory.Field400[i4];
            if ((b2 & 1) == 0) {
                byte b3 = (byte) ((b2 & 248) >> 3);
                byte b4 = (byte) (((byte) (b2 & 7)) >> 1);
                short s16 = s15;
                short s17 = s14;
                int i5 = s13 << 2;
                int i6 = i5 + 1;
                short s18 = this.actorFactory.Field401[i5];
                int i7 = i6 + 1;
                short s19 = this.actorFactory.Field401[i6];
                short s20 = this.actorFactory.Field401[i7];
                short s21 = this.actorFactory.Field401[i7 + 1];
                byte Method480 = Method480();
                if (b4 == Method480) {
                    b = 0;
                } else if (b4 == 0 || Method480 == 0) {
                    b = (byte) (b4 + Method480);
                } else {
                    System.out.println("FLIP H and FLIP V, cannot be used at a same time, use your own implementation");
                }
                if (Method480 == 1) {
                    s10 = (short) ((-s17) - s20);
                    s9 = s16;
                } else {
                    s10 = s17;
                    s9 = s16;
                    if (Method480 == 2) {
                        s9 = (short) ((-s16) - s21);
                        s10 = s17;
                    }
                }
                if (this.actorFactory.Field408) {
                    Image image = ((Image[][]) this.actorFactory.Field409.elementAt(b3))[s13 - this.actorFactory.Field406[b3]][0];
                    int Method477 = s10 + Method477();
                    int Method478 = s9 + Method478();
                    if (b == 0) {
                        graphics.drawImage(image, Method477, Method478, 20);
                    } else if (b == 1) {
                        graphics.drawRegion(image, 0, 0, s20, s21, 2, Method477, Method478, 20);
                    } else if (b == 2) {
                        graphics.drawRegion(image, 0, 0, s20, s21, 2, Method477, Method478, 20);
                    }
                } else {
                    Image[] imageArr = (Image[]) this.actorFactory.Field409.elementAt(b3);
                    if (b == 1) {
                        s11 = (short) ((imageArr[0].getWidth() - s20) - s18);
                        height = s19;
                    } else {
                        s11 = s18;
                        height = s19;
                        if (b == 1) {
                            height = (short) ((imageArr[0].getHeight() - s21) - s19);
                            s11 = s18;
                        }
                    }
                    int Method4772 = s10 + Method477();
                    int Method4782 = s9 + Method478();
                    graphics.clipRect(Method4772, Method4782, s20, s21);
                    if (b == 0) {
                        graphics.drawImage(imageArr[0], Method4772 - s11, Method4782 - height, 20);
                    } else if (b == 1) {
                        graphics.drawRegion(imageArr[0], 0, 0, imageArr[0].getWidth(), imageArr[0].getHeight(), 2, Method4772 - s11, Method4782 - height, 20);
                    } else if (b == 2) {
                        graphics.drawRegion(imageArr[0], 0, 0, s20, s21, 2, Method4772 - s11, Method4782 - height, 20);
                    }
                }
            } else if (b2 == 1 || b2 == 3) {
                int i8 = s13 * 5;
                int i9 = this.actorFactory.Field402[i8];
                int i10 = this.actorFactory.Field402[i8 + 1];
                int i11 = this.actorFactory.Field402[i8 + 2];
                int i12 = this.actorFactory.Field402[i8 + 3];
                int i13 = this.actorFactory.Field402[i8 + 4];
                boolean z = b2 == 3;
                short s22 = s15;
                short s23 = s14;
                byte Method4802 = Method480();
                if (Method4802 == 1) {
                    s2 = (short) ((-s23) - i9);
                    s = s22;
                } else {
                    s2 = s23;
                    s = s22;
                    if (Method4802 == 2) {
                        s = (short) ((-s22) - i10);
                        s2 = s23;
                    }
                }
                int Method4773 = s2 + Method477();
                int Method4783 = s + Method478();
                graphics.setColor(i13);
                if (z) {
                    graphics.fillArc(Method4773, Method4783, i9, i10, i11, i12);
                } else {
                    graphics.drawArc(Method4773, Method4783, i9, i10, i11, i12);
                }
            } else if (b2 == 5) {
                int i14 = s13 * 3;
                int i15 = this.actorFactory.Field403[i14];
                int i16 = this.actorFactory.Field403[i14 + 1];
                int i17 = this.actorFactory.Field403[i14 + 2];
                int i18 = i16;
                int i19 = i15;
                short s24 = s15;
                short s25 = s14;
                byte Method4803 = Method480();
                if (Method4803 == 1) {
                    s8 = (short) -s25;
                    i19 = -i19;
                    s7 = s24;
                } else {
                    s8 = s25;
                    s7 = s24;
                    if (Method4803 == 2) {
                        s7 = (short) -s24;
                        i18 = -i18;
                        s8 = s25;
                    }
                }
                int Method4774 = s8 + Method477();
                int Method4775 = i19 + Method477();
                graphics.setColor(i17);
                graphics.drawLine(Method4774, s7 + Method478(), Method4775, i18 + Method478());
            } else if (b2 == 7 || b2 == 9) {
                int i20 = s13 * 3;
                int i21 = this.actorFactory.Field404[i20];
                int i22 = this.actorFactory.Field404[i20 + 1];
                int i23 = this.actorFactory.Field404[i20 + 2];
                boolean z2 = b2 == 9;
                short s26 = s15;
                short s27 = s14;
                graphics.setColor(i23);
                byte Method4804 = Method480();
                if (Method4804 == 1) {
                    s4 = (short) ((-s27) - i21);
                    s3 = s26;
                } else {
                    s4 = s27;
                    s3 = s26;
                    if (Method4804 == 2) {
                        s3 = (short) ((-s26) - i22);
                        s4 = s27;
                    }
                }
                int Method4776 = s4 + Method477();
                int Method4784 = s3 + Method478();
                if (z2) {
                    graphics.fillRect(Method4776, Method4784, i21, i22);
                } else {
                    graphics.drawRect(Method4776, Method4784, i21, i22);
                }
            } else if (b2 == 11 || b2 == 13) {
                int i24 = s13 * 5;
                int i25 = this.actorFactory.Field405[i24];
                int i26 = this.actorFactory.Field405[i24 + 1];
                int i27 = this.actorFactory.Field405[i24 + 2];
                int i28 = this.actorFactory.Field405[i24 + 3];
                int i29 = this.actorFactory.Field405[i24 + 4];
                boolean z3 = b2 == 13;
                short s28 = s15;
                short s29 = s14;
                byte Method4805 = Method480();
                if (Method4805 == 1) {
                    s6 = (short) ((-s29) - i25);
                    s5 = s28;
                } else {
                    s6 = s29;
                    s5 = s28;
                    if (Method4805 == 2) {
                        s5 = (short) ((-s28) - i26);
                        s6 = s29;
                    }
                }
                int Method4777 = s6 + Method477();
                int Method4785 = s5 + Method478();
                graphics.setColor(i29);
                if (z3) {
                    graphics.fillRoundRect(Method4777, Method4785, i25, i26, i27, i28);
                } else {
                    graphics.drawRoundRect(Method4777, Method4785, i25, i26, i27, i28);
                }
            }
            graphics.setClip(clipX, clipY, clipWidth, clipHeight);
        }
    }

    protected abstract int Method477();

    protected abstract int Method478();

    protected abstract void Method479(int i, int i2);

    protected abstract byte Method480();

    protected abstract void Method481();

    protected abstract void Method482();
}

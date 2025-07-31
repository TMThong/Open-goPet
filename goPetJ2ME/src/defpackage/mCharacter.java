package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.ui.common.Resource;
import vn.me.core.BaseCanvas;
import vn.me.ui.Font;
import vn.me.screen.GameScene;
import java.util.Random;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.Animation;

/* renamed from: Class126  reason: default package */
 /* loaded from: gopet_repackage.jar:Class126.class */
public class mCharacter extends mObject {

    public boolean Field792;
    public int Id;
    public String Field794;
    public String nameUpperCase;
    protected int nameWitdth;
    public int Field799;
    private long Field800;
    private long Field801;
    private int Field802;
    private int Field803;
    public int Field806;
    public int Field807;
    private static Image Field809;
    private int Field810;
    private boolean Field812;
    public byte Field813;
    public SceneManage Field814;
    public boolean Field815;
    private int Field816;
    private String Field819;
    private byte Field820;
    private int Field821;
    private byte Field822;
    long Field823;
    Class18 Field824;
    public String Field797 = "";
    public boolean Field798 = true;
    private Vector Field804 = new Vector();
    public byte Field808 = -1;
    public boolean Field811 = false;
    public boolean useCollisionRectToDetectCollision = false;
    public int Field818 = 16777215;
    public byte speed = 8;
    private Animation[] animations = new Animation[0];

    public mCharacter(int Id, byte b, SceneManage class140) {
        this.Field814 = class140;
        this.Id = Id;
        this.Field813 = b;
        setCollisionRec(new Rectangle(-8, -5, 16, 5));
        if (Field809 == null) {
            Field809 = Resource.setFileTable("/common.dat", 10);
        }
        this.xChar = 0;
        this.yChar = 0;
        this.Field802 = -100;
        this.Field803 = -100;
        stand(-1L);
        this.Field816 = ((0 - GameResourceManager.Field601.getHeight()) - 74) + 5;
        this.Field821 = new Random(System.currentTimeMillis()).nextInt() % 1000;
    }

    public final void Method158(String str) {
        this.Field797 = str;
        if ("".equals(str)) {
            this.Field798 = true;
        } else {
            this.Field798 = false;
        }
    }

    public void update(long j) {
        Class125 Method163;
        Class125 Method1632;
        Class125 Method1633;
        super.update(j);
        if (this.Field824 != null && j - this.Field823 > 5000) {
            this.Field824 = null;
        }
        this.Field812 = false;
        switch (this.Field799) {
            case 0:
                if (!this.Field792 || this.Field801 == -1 || j - this.Field800 < this.Field801 || (Method1633 = Method163()) == null) {
                    return;
                }
                Method164(Method1633);
                return;
            case 1:
                this.Field812 = true;
                if ((this.Field802 == -100 && this.Field803 == -100) || (this.xChar == this.Field802 && this.yChar == this.Field803)) {
                    if (!this.Field792 || (Method1632 = Method163()) == null) {
                        return;
                    }
                    Method164(Method1632);
                    return;
                }
                if (Ulti.Method369(this.Field802 - this.xChar) > Ulti.Method369(this.Field803 - this.yChar)) {
                    if (this.Field802 > this.xChar) {
                        this.xChar += this.speed;
                    } else {
                        this.xChar -= this.speed;
                    }
                    if (this.Field802 != this.xChar) {
                        if (this.Field802 > this.xChar) {
                            this.yChar += (this.speed * (this.Field803 - this.yChar)) / (this.Field802 - this.xChar);
                        } else {
                            this.yChar += (this.speed * (this.yChar - this.Field803)) / (this.Field802 - this.xChar);
                        }
                    }
                } else {
                    if (this.Field803 > this.yChar) {
                        this.yChar += this.speed;
                    } else {
                        this.yChar -= this.speed;
                    }
                    if (this.Field803 != this.yChar) {
                        if (this.Field803 > this.yChar) {
                            this.xChar += (this.speed * (this.Field802 - this.xChar)) / (this.Field803 - this.yChar);
                        } else {
                            this.xChar += (this.speed * (this.xChar - this.Field802)) / (this.Field803 - this.yChar);
                        }
                    }
                }
                this.Field810 = (this.Field810 + 1) % 100;
                if (Ulti.Method369(this.Field803 - this.yChar) > this.speed || Ulti.Method369(this.Field802 - this.xChar) > this.speed) {
                    return;
                }
                this.xChar = this.Field802;
                this.yChar = this.Field803;
                this.Field802 = -100;
                this.Field803 = -100;
                if (!this.Field792 || (Method163 = Method163()) == null) {
                    return;
                }
                Method164(Method163);
                return;
            default:
                return;
        }
    }

    private void renderAnimation(int xRender, int yRender, boolean isDrawEnd) {
        for (int j = 0; j < animations.length; j++) {
            Animation animation = animations[j];
            if (animation.isDrawEnd == isDrawEnd) {
                if (animation.image == null) {
                    animation.image = PetGameModel.Field284.requestImg(animation.frameImgPath);
                }
                if (animation.image != null) {
                    int frameWidth = animation.image.getWidth() / animation.numFrame;
                    switch (animation.type) {
                        case Animation.TYPE_ARCHIVENMENT: {
                            if (BaseCanvas.ticks % 5 == 0) {
                                animation.ticks++;
                            }
                            int frameTicks = animation.ticks % animation.numFrame;
                            BaseCanvas.g.drawRegion(animation.image, frameWidth * frameTicks, 0, frameWidth, animation.image.getHeight(), 0, (this.xChar - xRender + animation.vX) - (frameWidth / 2), this.yChar - yRender + animation.vY - 74 - (GameResourceManager.Method117().getHeight() * 2) - animation.image.getHeight(), 0);
                        }
                        break;
                    }
                }
            }
        }
    }

    public void paintInMap(int xRender, int yRender) {
        Image Method455;
        if (this.Field864) {
            renderAnimation(xRender, yRender, false);
            if (this.Field819 != null && !"".equals(this.Field819) && (Method455 = PetGameModel.Field284.requestImg(this.Field819)) != null) {
                int Method369 = (Ulti.Method369(BaseCanvas.ticks + this.Field821) >> 3) % 2;
                int width = Method455.getWidth() >> 1;
                BaseCanvas.g.drawRegion(Method455, width * Method369, 0, width, Method455.getHeight(), 0, (this.xChar - xRender) - width, ((this.yChar - yRender) - 74) + this.Field820, 0);
                BaseCanvas.g.drawRegion(Method455, width * Method369, 0, width, Method455.getHeight(), 2, this.xChar - xRender, ((this.yChar - yRender) - 74) + this.Field820, 0);
            }
            if (this.Field798) {
                Class128.drawChar(null, (byte) (this.Field813 == 0 ? 1 : 0), this.xChar - xRender, this.yChar - yRender, this.Field807 == 1 ? 1 : -1, this.Field812, this.Field821, 0);
            } else {
                Image img = PetGameModel.Field284.Method459(this.Field797);
                if (img == null) {
                    Class128.drawChar(null, (byte) (this.Field813 == 0 ? 1 : 0), this.xChar - xRender, this.yChar - yRender, this.Field807 == 1 ? 1 : -1, this.Field812, this.Field821, 0);
                } else {
                    BaseCanvas.g.drawImage(Class128.Field835, this.xChar - xRender, ((this.yChar - yRender) - 74) + 68, 17);
                    Class128.drawChar(img, this.xChar - xRender, this.yChar - yRender, this.Field807, this.Field812);
                }
            }
            if (Ulti.Method372(this.Field808, 0)) {
                BaseCanvas.g.drawImage(Field809, ((this.xChar - xRender) - 10) - (this.nameWitdth >> 1), ((this.yChar - 74) - yRender) + 5, 0);
            }
            Font Method114 = Ulti.Method372(this.Field808, 1) ? GameResourceManager.getDefaultFont() : this.Field818 == 16777215 ? GameResourceManager.Method117() : GameResourceManager.Method118(this.Field818);
            int i3 = Ulti.Method372(this.Field808, 2) ? 1 : 0;
            int i4 = ((this.yChar - 74) + 3) - yRender;
            Method114.drawString(BaseCanvas.g, this.nameUpperCase, (this.xChar - xRender) - (this.nameWitdth >> 1), i4, 0, i3);
            if (this.Field794 != null && !"".equals(this.Field794)) {
                i4 -= GameResourceManager.Method117().getHeight() + 2;
                GameResourceManager.Method117().drawString(BaseCanvas.g, this.Field794, this.xChar - xRender, i4, 17, 1);
            }
            if (this.Field822 != 0) {
                GameResourceManager.Method117().drawString(BaseCanvas.g, new StringBuffer("PK ").append((int) this.Field822).toString(), this.xChar - xRender, i4 - (GameResourceManager.Method117().getHeight() + 2), 17, 1);
            }
            if (this.Field824 != null) {
                Method162(xRender, yRender + 10);
            }
            renderAnimation(xRender, yRender, true);
        }
    }

    public final void Method160(int i, int i2) {
        byte b;
        this.Field812 = true;
        this.Field802 = i;
        this.Field803 = i2;
        int i3 = i - this.xChar;
        int i4 = i2 - this.yChar;
        if (i3 > 0) {
            this.Field806 = 3;
        } else if (i3 < 0) {
            this.Field806 = 2;
        } else if (i4 < 0) {
            this.Field806 = 0;
        } else {
            this.Field806 = 1;
        }
        if (this.Field806 == 3) {
            b = 1;
        } else if (this.Field806 == 2) {
            b = 0;
        } else {
            b = (byte) (Method126() ? 0 : 1);
        }
        if (this.Field799 != 1 || ((this.Field806 == 3 && Method126()) || (this.Field806 == 2 && !Method126()))) {
            this.Field807 = b;
        }
        this.Field799 = 1;
    }

    public final boolean Method126() {
        return this.Field807 == 0;
    }

    public final void stand(long j) {
        this.Field801 = j;
        this.Field800 = System.currentTimeMillis();
        this.Field799 = 0;
        this.Field807 = (byte) (Method126() ? 0 : 1);
    }

    public void paintObj(int i, int i2) {
        if (this.Field864) {
            BaseCanvas.g.drawImage(GameResourceManager.Field601, this.xChar - i, (((-i2) + this.yChar) - this.Field873) + this.Field816, 17);
        }
    }

    public final void Method41(String str) {
        this.Field823 = System.currentTimeMillis();
        if (this.Field824 == null) {
            this.Field824 = new Class18();
        }
        this.Field824.Method317(str);
    }

    protected void Method162(int i, int i2) {
        this.Field824.Method318(BaseCanvas.g, this.xChar - i, (((-i2) + this.yChar) - 54) - 8);
    }

    private Class125 Method163() {
        synchronized (this.Field804) {
            if (this.Field804.isEmpty()) {
                return null;
            }
            Class125 class125 = (Class125) this.Field804.firstElement();
            this.Field804.removeElementAt(0);
            return class125;
        }
    }

    private void Method164(Class125 class125) {
        switch (class125.Field788) {
            case 0:
                stand(class125.Field791);
                return;
            case 1:
                Method160(class125.Field789, class125.Field790);
                return;
            default:
                return;
        }
    }

    public final void autoMove(byte b, int[] iArr) {
        Class125 Method163;
        synchronized (this.Field804) {
            if (!this.Field804.isEmpty()) {
                int i = 0;
                while (i < this.Field804.size()) {
                    Class125 class125 = (Class125) this.Field804.elementAt(i);
                    if (class125.Field788 == 0) {
                        this.Field804.removeElement(class125);
                    } else {
                        i++;
                    }
                }
            }
            for (int i2 = 0; i2 < (iArr.length >> 1); i2++) {
                Class125 class1252 = new Class125((byte) 1);
                class1252.Field789 = iArr[i2 << 1];
                class1252.Field790 = iArr[(i2 << 1) + 1];
                this.Field804.addElement(class1252);
            }
            Class125 class1253 = new Class125((byte) 0);
            class1253.Field791 = -1L;
            this.Field804.addElement(class1253);
            if (this.Field799 == 0 && (Method163 = Method163()) != null) {
                Method164(Method163);
            }
        }
    }

    public final void setName(String str) {
        this.name = str;
        this.nameUpperCase = str.toUpperCase();
        this.nameWitdth = GameResourceManager.Method117().getWidth(this.nameUpperCase);
    }

    public void Method166(GameScene class134) {
    }

    public final void Method167(String str, byte b) {
        this.Field819 = str;
        this.Field820 = b;
    }

    public final void Method22(byte b) {
        this.Field822 = b;
    }

    public final void setAnimation(Animation[] animations_) {
        this.animations = animations_;
    }
}

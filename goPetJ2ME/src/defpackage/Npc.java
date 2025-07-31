package defpackage;

import vn.me.ui.MessageDialog;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.Button;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import java.util.Vector;
import javax.microedition.lcdui.Image;

/* renamed from: Class138  reason: default package */
/* loaded from: gopet_repackage.jar:Class138.class */
public final class Npc extends mCharacter implements IActionListener {
    private int Field944;
    private String[] Field945;
    private int Field946;
    public int Field947;
    private Vector Field948;
    private Vector Field949;
    private Dialog Field950;
    private boolean Field951;
    public int frameNum;
    public String imageId;
    private Class18 Field954;
    public boolean isHuman;
    private long Field956;
    private long Field957;
    private long Field958;
    private boolean Field959;
    private boolean Field960;
    private boolean Field961;

    public Npc(int i, SceneManage class140) {
        super(i, (byte) 0, class140);
        this.Field947 = 128;
        this.Field948 = new Vector();
        this.Field949 = new Vector();
        this.Field951 = false;
        this.frameNum = 1;
        this.Field792 = true;
        this.name = "NPC";
        this.centerObjectCMD = new Command(0, ActorFactory.gL(419), this);
        this.Field950 = new MessageDialog(ActorFactory.gL(363), null, new Command(1, ActorFactory.gL(41), this), null, 2);
        this.Field806 = 3;
        this.Field958 = (Ulti.Method370(5) * 1000) + 3000;
    }

    public final void addGuide(String str, Class102 class102) {
        this.Field948.addElement(str);
        this.Field949.addElement(class102);
        class102.cmdCenter = new Command(3, ActorFactory.gL(337), this);
    }

    public final void setTexts(String[] strArr, int i) {
        this.Field944 = i;
        this.Field945 = strArr;
        this.Field875 = System.currentTimeMillis();
        if (this.Field954 == null) {
            this.Field954 = new Class18();
        }
        this.Field824 = this.Field954;
        this.Field946 = 0;
        this.Field824.Method317(strArr[this.Field946]);
    }

    ///////@Override // defpackage.Class126, defpackage.Class133
    public final void paintInMap(int i, int i2) {
        if (this.img == null) {
            if (this.imageId != null) {
                this.img = GameResourceManager.loadResourceImg(this.imageId, (byte) 2);
                if (this.img != null) {
                    Image image = this.img;
                    this.img = image;
                    image.getWidth();
                    int i3 = this.frameNum;
                    this.Field947 = image.getHeight();
                    return;
                }
                return;
            }
            return;
        }
        BaseCanvas.g.drawImage(GameResourceManager.Field603, (this.xChar - 13) - i, (this.yChar - 6) - i2, 0);
        int i4 = this.Field806 == 2 ? 2 : 0;
        if (this.Field959) {
            int height = this.img.getHeight() - (this.img.getHeight() >> 2);
            BaseCanvas.g.drawRegion(this.img, 0, 0, this.img.getWidth(), height, i4, this.xChar - i, ((this.yChar - this.Field947) - i2) + 1, 17);
            BaseCanvas.g.drawRegion(this.img, 0, height, this.img.getWidth(), this.img.getHeight() - height, i4, this.xChar - i, ((this.yChar - this.Field947) - i2) + height, 17);
        } else if (i4 == 0) {
            BaseCanvas.g.drawImage(this.img, this.xChar - i, (this.yChar - this.Field947) - i2, 17);
        } else {
            BaseCanvas.g.drawRegion(this.img, 0, 0, this.img.getWidth(), this.img.getHeight(), 2, this.xChar - i, (this.yChar - this.Field947) - i2, 17);
        }
        if (this.isHuman) {
            GameResourceManager.Method117().drawString(BaseCanvas.g, this.nameUpperCase, (this.xChar - i) - (this.nameWitdth >> 1), ((this.yChar - this.Field947) - i2) - 8, 0);
        }
        Method162(i, i2);
    }

    ///////@Override // defpackage.Class126, defpackage.Class133
    public final void paintObj(int i, int i2) {
        BaseCanvas.g.drawImage(GameResourceManager.Field601, this.xChar - i, (((-(this.img == null ? i2 - 3 : (i2 + this.Field947) - 3)) + this.yChar) - GameResourceManager.Field601.getHeight()) - this.Field873, 17);
    }

    ///////@Override // defpackage.Class126
    protected final void Method162(int i, int i2) {
        if (this.Field824 != null) {
            this.Field824.Method318(BaseCanvas.g, this.xChar - i, (((-i2) + this.yChar) - this.Field947) - 9);
        }
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        if (Command.cmdId >= 100) {
            this.Field950.show(true);
            GlobalService.selectNpcOption(this.Id, Command.cmdId - 100);
            return;
        }
        switch (Command.cmdId) {
            case 0:
                this.Field950.show(true);
                GlobalService.Method263(this.Id);
                return;
            case 1:
                this.Field950.Method274();
                return;
            case 2:
            default:
                return;
            case 3:
                Method0();
                return;
            case 4:
                ((Class102) Command.objPerfomed).show(true);
                return;
        }
    }

    public final void Method174(String[] strArr, int[] iArr) {
        BaseCanvas.currentScreen.Method309();
        Button[] class165Arr = new Button[strArr.length];
        for (int i = 0; i < strArr.length; i++) {
            class165Arr[i] = new Button(new Command(iArr[i] + 100, strArr[i], this));
        }
        SceneManage class140 = this.Field814;
        SceneManage.Method224(this, class165Arr);
    }

    public final void Method0() {
        BaseCanvas.currentScreen.Method309();
        if (this.Field949.isEmpty()) {
            return;
        }
        Button[] class165Arr = new Button[this.Field949.size()];
        for (int i = 0; i < this.Field949.size(); i++) {
            class165Arr[i] = new Button(new Command(4, (String) this.Field948.elementAt(i), (Class102) this.Field949.elementAt(i), this));
        }
        SceneManage class140 = this.Field814;
        SceneManage.Method224(this, class165Arr);
    }

    public final void Method18() {
        this.Field949.removeAllElements();
        this.Field948.removeAllElements();
    }

    ///////@Override // defpackage.Class126, defpackage.Class133
    public final void update(long j) {
        super.update(j);
        if (this.isHuman) {
            if (this.Field945 != null) {
                if (this.Field824 != null) {
                    if (j - this.Field875 > 5000) {
                        this.Field824 = null;
                        this.Field875 = j;
                    }
                } else if (j - this.Field875 > this.Field944 * 1000) {
                    this.Field946 = (this.Field946 + 1) % this.Field945.length;
                    this.Field824 = this.Field954;
                    this.Field824.Method317(this.Field945[this.Field946]);
                    this.Field875 = j;
                }
            }
            if (this.Field960 && j - this.Field956 > 400) {
                this.Field956 = j;
                this.Field959 = !this.Field959;
            }
            if (this.Field961) {
                if (j - this.Field957 > this.Field958) {
                    this.Field957 = j;
                    if (this.Field806 == 2) {
                        this.Field806 = 3;
                    } else {
                        this.Field806 = 2;
                    }
                    this.Field958 = (Ulti.Method370(5) * 1000) + 3000;
                }
            }
        }
    }

    public final void setType(byte b) {
        switch (b) {
            case 0:
                this.Field961 = false;
                this.Field960 = false;
                break;
            case 1:
                this.Field961 = false;
                this.Field960 = true;
                break;
            case 2:
                this.Field961 = true;
                this.Field960 = false;
                break;
            case 3:
                this.Field961 = true;
                this.Field960 = true;
                break;
        }
        this.isHuman = b != 0;
    }
}

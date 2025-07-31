package vn.me.screen;

import defpackage.ActorFactory;
import vn.me.core.BaseCanvas;
import defpackage.mCharacter;
import defpackage.Class127;
import defpackage.Class131;
import defpackage.mObject;
import defpackage.Class135;
import vn.me.ui.ImageButton;
import defpackage.SceneManage;
import defpackage.Class16;
import vn.me.ui.Button;
import defpackage.mMapObject;
import defpackage.Class198;
import vn.me.ui.geom.Rectangle;
import defpackage.Ulti;
import defpackage.MEService;
import defpackage.MyCharacter;
import defpackage.Command;
import vn.me.ui.Dialog;
import vn.me.ui.EditField;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import vn.me.ui.common.T;
import vn.me.ui.WidgetGroup;
import defpackage.mMap;
import vn.me.screen.Screen;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import thong.auto.AutoManager;
import thong.sdk.ISoundManagerSDK;
import vn.me.ui.interfaces.IActionListener;

/* renamed from: Class134  reason: default package */
 /* loaded from: gopet_repackage.jar:Class134.class */
public class GameScene extends Screen implements IActionListener {
    
    public mMap Field879;
    public Class127 listChar;
    protected Vector Field881;
    private Vector Field882;
    private Vector Field883;
    public mCharacter Field884;
    public mObject petCenterCmd;
    private int Field886;
    private int Field887;
    public Class16 Field888;
    public Class16 Field889;
    public int Field890;
    private boolean[] Field891;
    private boolean Field892;
    private int Field893;
    private Vector Field894;
    private boolean Field895;
    private Command Field896;
    public Command Field897;
    private Command Field898;
    public boolean Field899;
    private int Field900;
    private WidgetGroup Field901;
    public boolean canMove;
    private static String Field903 = "";
    private static String Field904 = "";
    public SceneManage Field905;
    private boolean Field906;
    public IActionListener Field907;
    protected boolean Field908;
    private int Field909;
    private int Field910;
    private int Field911;
    private int Field912;
    private long Field913;
    public static String[] nameBgSound = new String[]{"s_outMap_0", "s_outMap_1"};
    
    public GameScene(SceneManage class140) {
        super(true);
        this.listChar = new Class127();
        this.Field881 = new Vector();
        this.Field882 = new Vector();
        this.Field883 = new Vector();
        this.Field884 = null;
        this.petCenterCmd = null;
        this.Field888 = new Class16();
        this.Field889 = new Class16();
        this.Field891 = new boolean[4];
        this.Field892 = false;
        this.Field894 = new Vector();
        this.Field895 = false;
        this.Field900 = 27;
        this.canMove = true;
        this.Field906 = true;
        this.Field908 = true;
        this.Field909 = -1000;
        this.Field910 = -1000;
        this.Field911 = 50;
        this.Field912 = 0;
        this.Field905 = class140;
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.Field901 = new WidgetGroup(0, 0, BaseCanvas.w, BaseCanvas.h);
        this.Field901.isLoop = true;
        this.Field901.isScrollableX = true;
        this.Field886 = BaseCanvas.w;
        this.Field887 = BaseCanvas.h;
        this.Field896 = new Command(1, ActorFactory.gL(419), this);
        this.chatEditField = new EditField(0, BaseCanvas.h - (2 * LAF.Field1293), BaseCanvas.w, LAF.Field1293);
        this.chatEditField.cmdCenter = new Command(-2, T.gL(1), this);
        this.chatEditField.cmdLeft = new Command(-3, T.gL(16), this);
        this.chatEditField.isVisible = false;
        this.chatEditField.Field1097 = this;
        this.cmdRight = new Command(100, ActorFactory.gL(275), this);
        this.Field897 = new Command(201, ActorFactory.gL(58), this);
        this.cmdLeft = new Command(200, ActorFactory.gL(3), this);
        this.cmdLeft.Field1321 = ActorFactory.gL(3);
        this.Field898 = new Command(2, "", this);
    }
    
    public static void playBgRandom() {
        ISoundManagerSDK.playBgSound(nameBgSound[BaseCanvas.ticks % nameBgSound.length]);
    }

    /* JADX INFO: Access modifiers changed from: protected */
    public final void Method189() {
        if (BaseCanvas.instance == null || !BaseCanvas.instance.hasPointerEvents()) {
            return;
        }
        BaseCanvas.g.setColor(10264217);
        int i = this.Field909 - (this.Field911 >> 1);
        int i2 = this.Field910 - (this.Field911 >> 1);
        int i3 = this.Field911 >> 1;
        BaseCanvas.g.drawArc(i, i2, this.Field911, this.Field911, 0, 360);
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.drawLine((i + i3) - 2, i2 + i3, i + i3 + 2, i2 + i3);
        BaseCanvas.g.drawLine(i + i3, (i2 + i3) - 2, i + i3, i2 + i3 + 2);
        Image image = GameResourceManager.Field585[2];
        if (!this.Field892) {
            BaseCanvas.g.drawImage(image, this.Field909 - (image.getWidth() >> 1), this.Field910 - (image.getHeight() >> 1), 0);
        } else if (SceneManage.myCharacter.Field806 == 0) {
            BaseCanvas.g.drawImage(image, this.Field909 - (image.getWidth() >> 1), (this.Field910 - (image.getHeight() >> 1)) - i3, 0);
        } else if (SceneManage.myCharacter.Field806 == 1) {
            BaseCanvas.g.drawImage(image, this.Field909 - (image.getWidth() >> 1), (this.Field910 - (image.getHeight() >> 1)) + i3, 0);
        } else if (SceneManage.myCharacter.Field806 == 2) {
            BaseCanvas.g.drawImage(image, (this.Field909 - (image.getWidth() >> 1)) - i3, this.Field910 - (image.getHeight() >> 1), 0);
        } else {
            BaseCanvas.g.drawImage(image, (this.Field909 - (image.getWidth() >> 1)) + i3, this.Field910 - (image.getHeight() >> 1), 0);
        }
    }
    
    protected void Method190() {
    }
    
    public final void Method191() {
        for (int i = 0; i < this.Field891.length; i++) {
            this.Field891[i] = false;
        }
        this.Field912 = 0;
    }

    ///////@Override // defpackage.Screen
    public final void onChat(String str) {
        GlobalService.Method234(str);
        SceneManage.myCharacter.Method41(str);
        Method202();
    }

    ///////@Override // defpackage.Screen
    public final boolean checkKeys(int i, int i2) {
        if (super.checkKeys(i, i2)) {
            return true;
        }
        switch (i2) {
            case -4:
                this.Field891[3] = i == 0;
                return true;
            case -3:
                this.Field891[2] = i == 0;
                return true;
            case -2:
                this.Field891[1] = i == 0;
                return true;
            case -1:
                this.Field891[0] = i == 0;
                return true;
            default:
                return super.checkKeys(i, i2);
        }
    }
    
    public final void Method192(mCharacter class126) {
        synchronized (this.Field883) {
            this.listChar.Method157(class126);
            this.Field883.removeElement(class126);
        }
    }
    
    protected void Method7() {
    }
    
    protected void drawGUI() {
    }
    
    public static void updateMapIndicator() {
        if (GameController.Field483 != null) {
            int i = 0;
            while (true) {
                if (i >= GameController.Field483.length) {
                    break;
                } else if (GameController.myInfo.mGoMapId == GameController.Field483[i]) {
                    Field903 = GameController.Field482[i];
                    break;
                } else {
                    i++;
                }
            }
        }
        Field904 = String.valueOf(GameController.myInfo.mGoMapChanel);
    }
    
    public static void Method194() {
        int i = BaseCanvas.w - 28;
        BaseCanvas.g.drawImage(GameResourceManager.Field602, i, 1, 0);
        GameResourceManager.Method116().drawString(BaseCanvas.g, Field904, i + 12, 1, 17);
        GameResourceManager.Method117().drawString(BaseCanvas.g, Field903, i - 5, 4, 24);
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        int i = this.Field888.Field57;
        int i2 = this.Field889.Field57;
        int length = this.Field879.xObject.length;
        this.Field879.Method183(i, i2, false);
        synchronized (this.Field883) {
            for (int i3 = 0; i3 < this.Field883.size() - 1; i3++) {
                for (int i4 = i3 + 1; i4 < this.Field883.size(); i4++) {
                    mObject class133 = (mObject) this.Field883.elementAt(i3);
                    mObject class1332 = (mObject) this.Field883.elementAt(i4);
                    if (class133.yChar > class1332.yChar) {
                        this.Field883.setElementAt(class1332, i3);
                        this.Field883.setElementAt(class133, i4);
                    }
                }
            }
        }
        int i5 = 0;
        int i6 = 0;
        this.Field883.elementAt(0);
        while (i5 < length && i6 < this.Field883.size()) {
            mObject class1333 = (mObject) this.Field883.elementAt(i6);
            if (this.Field879.yObject[i5] < class1333.yChar) {
                Class131 class131 = this.Field879.Field941[this.Field879.Field938[i5]];
                Rectangle Method207 = class131.getPosition();
                short s = this.Field879.xObject[i5];
                Method207.x += s;
                int i7 = this.Field879.yObject[i5] - this.Field879.Field940[i5];
                Method207.y += i7;
                if (class131.Field864 && Method207.intersect(i, i2, this.Field886, this.Field887)) {
                    class131.Field859 = this.Field879.Field939[i5];
                    class131.draw(s - i, i7 - i2);
                }
                i5++;
            } else {
                Rectangle Method2072 = class1333.getPosition();
                if (class1333.Field864 && Method2072.intersect(i, i2, this.Field886, this.Field887)) {
                    class1333.paintInMap(i, i2);
                }
                i6++;
            }
        }
        for (int i8 = i5; i8 < length; i8++) {
            Class131 class1312 = this.Field879.Field941[this.Field879.Field938[i8]];
            Rectangle Method2073 = class1312.getPosition();
            short s2 = this.Field879.xObject[i8];
            Method2073.x += s2;
            int i9 = this.Field879.yObject[i8] - this.Field879.Field940[i8];
            Method2073.y += i9;
            if (class1312.Field864 && Method2073.intersect(i, i2, this.Field886, this.Field887)) {
                class1312.Field859 = this.Field879.Field939[i8];
                class1312.draw(s2 - i, i9 - i2);
            }
        }
        for (int i10 = i6; i10 < this.Field883.size(); i10++) {
            mObject class1334 = (mObject) this.Field883.elementAt(i10);
            Rectangle Method2074 = class1334.getPosition();
            if (class1334.Field864 && Method2074.intersect(i, i2, this.Field886, this.Field887)) {
                class1334.paintInMap(i, i2);
            }
        }
        if (this.Field884 != null) {
            this.Field884.paintObj(i, i2);
        }
        if (this.petCenterCmd != null) {
            this.petCenterCmd.paintObj(i, i2);
        }
        int length2 = this.Field879.Field942.length;
        while (true) {
            length2--;
            if (length2 < 0) {
                drawGUI();
                return;
            }
            this.Field879.Field942[length2].paintInMap(i, i2);
        }
    }

    public  void pointerPressed(int i, int i2) {
        if (currentDialog != null || this.currentMenu != null || this.Field899) {
            super.pointerPressed(i, i2);
            this.Field892 = false;
            return;
        }
        super.pointerPressed(i, i2);
        this.Field909 = i;
        this.Field910 = i2;
    }

    ///////@Override // defpackage.Screen
    public final void pointerDragged(int i, int i2) {
        if (currentDialog != null || this.currentMenu != null || this.Field899) {
            super.pointerDragged(i, i2);
            this.Field892 = false;
            return;
        }
        super.pointerDragged(i, i2);
        this.Field912 = 0;
        int i3 = this.Field911 >> 1;
        byte b = SceneManage.myCharacter.speed;
        if (i == this.Field909 && i2 == this.Field910) {
            return;
        }
        int abs = Math.abs(i - this.Field909);
        int i4 = abs > i3 ? i3 : abs;
        int abs2 = Math.abs(i2 - this.Field910);
        int max = Math.max(i4, abs2 > i3 ? i3 : abs2);
        this.Field912 = b - ((max * b) / i3);
        int i5 = i - this.Field909;
        int i6 = i2 - this.Field910;
        this.Field893 = Math.abs(i5) > Math.abs(i6) ? i5 < 0 ? 2 : 3 : i6 < 0 ? 0 : 1;
        this.Field892 = true;
        if (max < 10) {
            this.Field892 = false;
        }
    }


    public final void pointerReleased(int i, int i2) {
        if (currentDialog != null || this.currentMenu != null || this.Field899) {
            super.pointerReleased(i, i2);
            return;
        }
        super.pointerReleased(i, i2);
        Method191();
        this.Field909 = -1000;
        this.Field910 = -1000;
        this.Field892 = false;
    }

    public void update() {
        int i = SceneManage.currentScene.Field879.mapWidthPixel;
        this.Field886 = i < BaseCanvas.w ? i : BaseCanvas.w;
        int i2 = SceneManage.currentScene.Field879.mapHeightPixel;
        this.Field887 = i2 < BaseCanvas.h ? i2 : BaseCanvas.h;
        mCharacter class126 = SceneManage.myCharacter;
        int i3 = class126.Field799;
        long currentTimeMillis = System.currentTimeMillis();
        for (int i4 = 0; i4 < this.listChar.Method156(); i4++) {
            mCharacter Method155 = this.listChar.Method155(i4);
            if (Method155 != SceneManage.myCharacter) {
                Method155.update(currentTimeMillis);
            }
        }
        for (int i5 = 0; i5 < this.Field882.size(); i5++) {
            ((mObject) this.Field882.elementAt(i5)).update(currentTimeMillis);
        }
        int i6 = this.Field893;
        if (!this.Field892) {
            i6 = -1;
        }
        boolean z = false;
        if ((this.Field891[0] || i6 == 0) && this.canMove) {
            Method79(0);
            z = true;
            if (SceneManage.myCharacter.Method126()) {
                Method104(0);
            } else {
                Method104(1);
            }
        } else if ((this.Field891[1] || i6 == 1) && this.canMove) {
            Method79(1);
            z = true;
            if (SceneManage.myCharacter.Method126()) {
                Method104(0);
            } else {
                Method104(1);
            }
        } else if ((this.Field891[2] || i6 == 2) && this.canMove) {
            Method79(2);
            z = true;
            Method104(0);
        } else if ((this.Field891[3] || i6 == 3) && this.canMove) {
            Method79(3);
            z = true;
            Method104(1);
        }
        this.petCenterCmd = null;
        int i7 = 1000000000;
        int i8 = SceneManage.myCharacter.Method126() ? SceneManage.myCharacter.xChar - 5 : SceneManage.myCharacter.xChar + 5;
        int i9 = -1;
        for (int i10 = 0; i10 < this.listChar.Method156(); i10++) {
            mCharacter Method1552 = this.listChar.Method155(i10);
            if (Method1552 != SceneManage.myCharacter && Method1552.Field876) {
                if (!Method1552.useCollisionRectToDetectCollision) {
                    int i11 = ((Method1552.xChar - i8) * (Method1552.xChar - i8)) + ((Method1552.yChar - SceneManage.myCharacter.yChar) * (Method1552.yChar - SceneManage.myCharacter.yChar));
                    if (i11 < i7) {
                        i7 = i11;
                        i9 = i10;
                    }
                } else if (SceneManage.myCharacter.position.intersect(Method1552.position)) {
                    this.petCenterCmd = Method1552;
                }
            }
        }
        if (i7 > 1225 || i9 < 0 || i9 >= this.listChar.Method156()) {
            this.Field884 = null;
        } else {
            this.Field884 = this.listChar.Method155(i9);
        }
        if (this.petCenterCmd == null) {
            int i12 = 0;
            while (true) {
                if (i12 >= this.Field882.size()) {
                    break;
                }
                mObject class133 = (mObject) this.Field882.elementAt(i12);
                if (class133.Field876 && class133.centerObjectCMD != null && SceneManage.myCharacter.position.intersect(class133.position)) {
                    this.petCenterCmd = class133;
                    break;
                }
                i12++;
            }
        }
        if (this.Field884 != null && this.petCenterCmd != null) {
            this.cmdCenter = this.Field896;
        } else if (this.Field884 != null) {
            this.cmdCenter = this.Field884.centerObjectCMD;
        } else if (this.petCenterCmd != null) {
            this.cmdCenter = this.petCenterCmd.centerObjectCMD;
        } else {
            this.cmdCenter = this.Field898;
        }
        if (z) {
            int i13 = 0;
            while (true) {
                if (i13 >= this.Field882.size()) {
                    break;
                }
                mObject class1332 = (mObject) this.Field882.elementAt(i13);
                if (SceneManage.myCharacter.position.intersect(class1332.position)) {
                    if (class1332.Field872 && !class1332.Field878) {
                        Command[] CommandArr = {class1332.centerObjectCMD};
                        Method191();
                        class1332.centerObjectCMD.Field1322.actionPerformed(CommandArr);
                        class1332.Field878 = true;
                        break;
                    }
                } else if (class1332.Field872) {
                    class1332.Field878 = false;
                }
                i13++;
            }
            if (class126.Field815) {
                class126.Field815 = false;
                MEService.show(false);
                ((MyCharacter) class126).ownerPet.Method0();
            }
        } else {
            class126.stand(-1L);
            if (class126.Method126()) {
                Method104(0);
            } else {
                Method104(1);
            }
        }
        if (z) {
            if (i3 == 0 && !this.Field895) {
                Method6();
            } else if (this.Field895) {
                if (currentTimeMillis - this.Field913 >= 2000) {
                    Method62();
                    Method6();
                } else {
                    Class198 class198 = new Class198(SceneManage.myCharacter.xChar, SceneManage.myCharacter.yChar, 0);
                    if (this.Field894.size() >= 2) {
                        Class198 class1982 = (Class198) this.Field894.elementAt(this.Field894.size() - 2);
                        Class198 class1983 = (Class198) this.Field894.elementAt(this.Field894.size() - 1);
                        if (Ulti.Method379(class1982, class1983, class198)) {
                            this.Field894.removeElement(class1983);
                        }
                    }
                    this.Field894.addElement(class198);
                }
            }
        } else if (this.Field895 && currentTimeMillis - this.Field913 > 2000) {
            Method62();
        }
        class126.update(currentTimeMillis);
        this.Field888.Method293();
        this.Field889.Method293();
        for (int i14 = 0; i14 < this.Field881.size(); i14++) {
            Class135 class135 = (Class135) this.Field881.elementAt(i14);
            if (class135.Field914) {
                this.Field881.removeElement(class135);
            } else {
                class135.Method188();
            }
        }
        AutoManager.update();
        super.update();
    }
    
    private void Method6() {
        this.Field913 = System.currentTimeMillis();
        this.Field895 = true;
        this.Field894.removeAllElements();
    }
    
    private void Method62() {
        this.Field895 = false;
        this.Field894.addElement(new Class198(SceneManage.myCharacter.xChar, SceneManage.myCharacter.yChar, 0));
        if (this.Field894.isEmpty()) {
            return;
        }
        int[] iArr = new int[this.Field894.size() << 1];
        for (int i = 0; i < this.Field894.size(); i++) {
            Class198 class198 = (Class198) this.Field894.elementAt(i);
            iArr[i << 1] = class198.Field1314;
            iArr[(i << 1) + 1] = class198.Field1315;
        }
        GlobalService.Method253(this.Field890, SceneManage.myCharacter.Id, (byte) (SceneManage.myCharacter.Method126() ? 0 : 1), iArr);
    }
    
    public final void Method196(mMap class137) {
        this.Field879 = class137;
        this.Field882.removeAllElements();
        int i = 0;
        while (i < this.Field883.size()) {
            if (this.Field883.elementAt(i) instanceof mMapObject) {
                this.Field883.removeElementAt(i);
                i--;
            }
            i++;
        }
        if (class137.Field942 != null) {
            for (int i2 = 0; i2 < class137.Field942.length; i2++) {
                addObject(class137.Field942[i2]);
            }
        }
    }
    
    public final void addObject(mObject class133) {
        this.Field882.addElement(class133);
    }
    
    public final void removeGameObject(mObject obj) {
        this.Field882.removeElement(obj);
        synchronized (this.Field883) {
            this.Field883.removeElement(obj);
        }
    }
    
    public final void addActor(mCharacter class126, int i, int i2, boolean z) {
        this.listChar.Method153(class126);
        if (z) {
            Method104(1);
        } else {
            class126.Field792 = true;
        }
        class126.xChar = i;
        class126.yChar = i2;
        Method200(class126);
        class126.Field864 = true;
    }
    
    public final void Method200(mObject class133) {
        this.Field883.addElement(class133);
    }
    
    private int Method201(int i, boolean z) {
        int i2 = i;
        if (z) {
            if (i < 0) {
                i2 = 0;
            } else if (i > this.Field879.mapWidthPixel - this.Field886) {
                i2 = this.Field879.mapWidthPixel - this.Field886;
            }
        } else if (i < 0) {
            i2 = 0;
        } else if (i > this.Field879.mapHeightPixel - this.Field887) {
            i2 = this.Field879.mapHeightPixel - this.Field887;
        }
        return i2;
    }

    /* JADX INFO: Access modifiers changed from: protected */
    public final void Method104(int i) {
        int i2 = 0;
        int i3 = 0;
        switch (i) {
            case 0:
                i2 = SceneManage.myCharacter.xChar - ((this.Field886 / 3) << 1);
                i3 = SceneManage.myCharacter.yChar - ((this.Field887 / 3) << 1);
                break;
            case 1:
                i2 = SceneManage.myCharacter.xChar - (this.Field886 / 3);
                i3 = SceneManage.myCharacter.yChar - ((this.Field887 / 3) << 1);
                break;
        }
        int Method201 = Method201(i2, true);
        int Method2012 = Method201(i3, false);
        this.Field888.Field56 = Method201;
        this.Field889.Field56 = Method2012;
    }

    ///////@Override // defpackage.Screen, defpackage.Class200
    public void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 0:
                GlobalService.Method259();
                return;
            case 1:
                //((Command) ((Object[]) obj)[0].Field1213 = new Command(101, Class59.Method486(419), this);
                Button[] class165Arr = {new Button(new StringBuffer().append(ActorFactory.gL(419)).append(" ").append(this.Field884.name).toString()), new Button(new StringBuffer().append(ActorFactory.gL(419)).append(" ").append(this.petCenterCmd.name).toString())};
                class165Arr[1].cmdCenter = new Command(102, ActorFactory.gL(419), this);
                SceneManage class140 = this.Field905;
                SceneManage.Method224(SceneManage.myCharacter, class165Arr);
                return;
            case 2:
                if (this.Field899) {
                    return;
                }
                Method26();
                this.Field899 = true;
                return;
            case 15:
                GameController.Method25();
                return;
            case 23:
                return;
            case 51:
                GameController.waitDialog();
                GlobalService.Method269();
                return;
            case 66:
                Method202();
                return;
            case 85:
                return;
            case 100:
                Method7();
                return;
            case 101:
                if (this.Field884 == null || this.Field884.centerObjectCMD == null) {
                    return;
                }
                this.Field884.centerObjectCMD.actionPerformed(new Object[]{this.Field884.centerObjectCMD, this.Field884});
                return;
            case 102:
                this.petCenterCmd.centerObjectCMD.actionPerformed(new Object[]{this.petCenterCmd.centerObjectCMD, this.petCenterCmd});
                return;
            case 200:
                Method190();
                return;
            case 201:
                if (this.chatEditField != null && currentDialog == null && this.currentMenu == null) {
                    addWidget(this.chatEditField);
                    this.chatEditField.requestFocus();
                    this.chatEditField.isVisible = true;
                    return;
                }
                return;
            case 202:
                new MessageScreen().switchToMe(1, true);
                return;
            case 10000:
                GameController.destroyApp();
                return;
            default:
                super.actionPerformed(obj);
                return;
        }
    }
    
    public final void Method202() {
        if (this.Field899) {
            if (BaseCanvas.instance.hasPointerEvents()) {
                this.cmdLeft = new Command(200, ActorFactory.gL(3), this);
            } else {
                this.cmdLeft = new Command(200, ActorFactory.gL(3), this);
                this.cmdLeft.Field1321 = ActorFactory.gL(3);
            }
            this.container.removeWidget(this.Field901);
            this.Field901.removeAll();
        }
        this.Field899 = false;
    }

    /* JADX INFO: Access modifiers changed from: protected */
    public final void Method203(ImageButton[] class136Arr) {
        if (this.Field906) {
            this.cmdLeft = new Command(66, ActorFactory.gL(64), this);
            int length = (class136Arr.length * 28) + ((class136Arr.length + 1) * 3);
            int i = 3;
            int i2 = 3;
            for (int i3 = 0; i3 < class136Arr.length; i3++) {
                if (length < BaseCanvas.w - 10 || i3 >= class136Arr.length / 2) {
                    i2 += 31;
                } else {
                    i += 31;
                }
            }
            int i4 = SceneManage.myCharacter.xChar - this.Field888.Field57;
            int i5 = SceneManage.myCharacter.yChar - this.Field889.Field57;
            int i6 = (BaseCanvas.w - i) >> 1;
            int i7 = (BaseCanvas.w - i2) >> 1;
            int i8 = ((((BaseCanvas.h - LAF.Field1293) - this.Field900) - 5) - 28) - 3;
            int i9 = i8 + 28 + 3;
            int i10 = 0;
            while (i10 < class136Arr.length) {
                class136Arr[i10].x = i4;
                class136Arr[i10].y = i5;
                if (i <= 3 || i10 >= class136Arr.length / 2) {
                    class136Arr[i10].destX = i7;
                    i7 += 31;
                    class136Arr[i10].destY = i9;
                } else {
                    class136Arr[i10].destX = i6;
                    i6 += 31;
                    class136Arr[i10].destY = i8;
                }
                class136Arr[i10].setFocused(i10 == 0);
                this.Field901.addWidget(class136Arr[i10]);
                i10++;
            }
            this.Field901.setFocused(true);
            this.container.addWidget(this.Field901);
            this.Field899 = true;
        }
    }
    
    private void Method79(int i) {
        mCharacter class126 = SceneManage.myCharacter;
        int i2 = 0;
        int i3 = 0;
        int i4 = 0;
        switch (i) {
            case 0:
                i2 = class126.xChar;
                i3 = (class126.yChar - class126.speed) + this.Field912;
                i4 = 0;
                break;
            case 1:
                i2 = class126.xChar;
                i3 = (class126.yChar + class126.speed) - this.Field912;
                i4 = 0;
                break;
            case 2:
                i2 = (class126.xChar - class126.speed) + this.Field912;
                i3 = class126.yChar;
                i4 = class126.x;
                break;
            case 3:
                i2 = (class126.xChar + class126.speed) - this.Field912;
                i3 = class126.yChar;
                i4 = class126.x + class126.position.size.width;
                break;
        }
        if (this.Field879.Method182(i2 + i4, i3 + class126.y)) {
            class126.Method160(i2, i3);
        }
    }

    ///////@Override // defpackage.Screen
    public final void showDialog(Dialog Dialog) {
        super.showDialog(Dialog);
        Method191();
    }

    ///////@Override // defpackage.Screen
    public final void close() {
        super.close();
        Method191();
    }
    
    public void Method26() {
    }
    
    public void Method49() {
    }
    
    public final void show(boolean z) {
        this.Field908 = false;
    }
    
    public final void Method205(boolean z) {
        this.Field906 = false;
    }
}

package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.geom.Rectangle;
import vn.me.ui.common.Resource;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.screen.MGOMessageScreen;
import vn.me.screen.GameScene;
import vn.me.screen.MessageScreen;
import vn.me.screen.MenuScreen;
import vn.me.screen.PetGameScreen;
import vn.me.screen.LoginScreen;
import vn.me.screen.Screen;
import java.io.IOException;
import java.util.Hashtable;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.network.Cmd;

/* renamed from: Class147  reason: default package */
 /* loaded from: gopet_repackage.jar:Class147.class */
public final class GlobalMessageHandler implements IMessageListener {

    public Class157 Field985;
    protected static GlobalMessageHandler instance;
    public SceneManage sceneManager;
    private MGOMessageScreen Field989;
    private Class144 Field984 = new Class144();
    private Hashtable Field988 = new Hashtable();

    public static GlobalMessageHandler Method208() {
        if (instance == null) {
            instance = new GlobalMessageHandler();
        }
        return instance;
    }

    ///////@Override // defpackage.Class158
    public final void Method209() {
        Class144 class144 = this.Field984;
        GlobalService.Method237();
        GlobalService.Method265();
        LoginScreen.Field634 = false;
    }

    ///////@Override // defpackage.Class158
    public final void Method210() {
        this.Field984.Method0();
    }

    ///////@Override // defpackage.Class158
    public final void onDisconnected() {
        Class144 class144 = this.Field984;
        Class144.Method18();
    }

    private byte[] readImageByteArray(Message msg) throws IOException {
        int size = msg.reader().readInt();
        if (size <= 0) {
            return null;
        }
        byte[] data = new byte[size];
        msg.reader().read(data);
        return data;
    }

    public final void onMessage(final Message msg) {
        Exception printStackTrace;
        byte readByte;
        int readInt;
        String readUTF;
        int readInt2;
        int readInt3;
        try {
            switch (msg.id) {
                case -36:
                    if (msg.reader().readByte() != 1) {
                        this.Field984.Method0();
                        return;
                    }
                    Class144 class144 = this.Field984;
                    if (GlobalService.Field1001 != null) {
                        GlobalService.Field1001.actionPerformed(null);
                        GlobalService.Field1001 = null;
                    }
                    return;
                case 3:
                    Class13 class13 = new Class13();
                    GameController.myInfo = class13;
                    class13.Field48 = msg.reader().readInt();
                    GameController.myInfo.Field49 = msg.reader().readUTF();
                    GlobalService.Field1000 = msg.reader().readUTF();
                    Class144 class1442 = this.Field984;
                    Class13 class132 = GameController.myInfo;
                    String str = LoginScreen.Field635;
                    class132.Field49 = str;
                    Ulti.Method371(str, 7);
                    try {
                        if (GameController.Field455) {
                            ActorFactory.saveUTF("nick", LoginScreen.Field635);
                            ActorFactory.saveUTF("pass", LoginScreen.Field636);
                        } else {
                            ActorFactory.deleteRecord("nick");
                            ActorFactory.deleteRecord("pass");
                        }
                    } catch (Exception unused) {
                    }
                    MessageScreen.Field21.removeAllElements();
                    MessageScreen.Field20.removeAllElements();
                    MessageScreen.Field22.removeAllElements();
                    PetGameScreen.hasLetter = false;
                    if (this.sceneManager == null) {
                        this.sceneManager = new SceneManage();
                        SceneManage class140 = this.sceneManager;
                        Resource.Method402("/buttonicon.dat");
                        class140.Field970 = new Image[2];
                        class140.Field970[0] = Resource.createImage(1);
                        class140.Field970[1] = Resource.createImage(2);
                        class140.Field971 = Resource.createImage(0);
                    }
                    Method208().Field985 = new PetGameModel();
                    BaseCanvas.instance.Field169 = (Class29) Method208().Field985;
                    return;
                case 4:
                    Class144 class1443 = this.Field984;
                    GameController.Method46(msg.reader().readUTF(), true);
                    return;
                case 7:
                    Vector vector = new Vector();
                    while (msg.reader().available() > 0) {
                        Class141 class141 = new Class141();
                        class141.Field973 = msg.reader().readInt();
                        class141.Field974 = msg.reader().readInt();
                        msg.reader().readBoolean();
                        class141.Field975 = msg.reader().readInt();
                        Ulti.formatNumber(class141.Field975);
                        vector.addElement(class141);
                    }
                    GameController.Method19(vector);
                    return;
                case 9:
                    if (this.sceneManager != null) {
                        int readInt4 = msg.reader().readInt();
                        String readUTF2 = msg.reader().readUTF();
                        if (readUTF2.length() > 0) {
                            this.sceneManager.Method221(readInt4, readUTF2);
                            return;
                        }
                        return;
                    }
                    return;
                case 10:
                    BaseCanvas.currentScreen.Method309();
                    GameController.Method47(msg.reader().readUTF());
                    return;
                case 20:
                    int readByte2 = msg.reader().readByte();
                    for (int i = 0; i < readByte2; i++) {
                        IwinMesssage class143 = new IwinMesssage();
                        class143.Field979 = msg.reader().readInt();
                        class143.Field980 = msg.reader().readUTF();
                        class143.Field981 = msg.reader().readUTF();
                        class143.Field982 = msg.reader().readByte();
                        switch (class143.Field982) {
                            case 0:
                                MessageScreen.Field21.addElement(class143);
                                break;
                            case 1:
                            default:
                                MessageScreen.Field22.addElement(class143);
                                break;
                            case 2:
                            case 3:
                                MessageScreen.Field20.addElement(class143);
                                break;
                        }
                    }
                    if (readByte2 != 0) {
                        BaseCanvas.getCurrentScreen();
                        Screen.Method313(ActorFactory.gL(203));
                        return;
                    }
                    return;
                case 21:
                    byte readByte3 = msg.reader().readByte();
                    if (readByte3 == 0) {
                        msg.reader().readInt();
                        msg.reader().readInt();
                        Class144 class1444 = this.Field984;
                        GameController.Method7();
                    } else if (readByte3 == 1) {
                        BaseCanvas.getCurrentScreen().hideDialog();
                        GameController.startOKDlg(ActorFactory.gL(491));
                    } else if (readByte3 == 2) {
                        String readUTF3 = msg.reader().readUTF();
                        Class144 class1445 = this.Field984;
                        GameController.Method60(readUTF3);
                    }
                    try {
                        readByte = msg.reader().readByte();
                        byte readByte4 = msg.reader().readByte();
                        switch (readByte) {
                            case 0:
                                if (readByte4 != 10 && readByte4 != 11) {
                                    String readUTF4 = msg.reader().readUTF();
                                    readInt = msg.reader().readInt();
                                    if (readInt != 0) {
                                        byte[] bArr = new byte[readInt];
                                        msg.reader().read(bArr);
                                        Image createImage = Image.createImage(bArr, 0, bArr.length);
                                        if (readByte4 != 4) {
                                            GameResourceManager.addGuiderImg(readUTF4, createImage, readByte4);
                                        }
                                    }
                                }
                                return;
                            case 12:
                                Method215(msg, readByte4);
                                if (readByte4 != 10) {
                                    String readUTF42 = msg.reader().readUTF();
                                    readInt = msg.reader().readInt();
                                    if (readInt != 0) {
                                    }
                                    break;
                                }
                                return;
                            default:
                                return;
                        }
                    } catch (IOException unused2) {
                        return;
                    }
                case 23:
                    msg.reader().readByte();
                    GameController.myInfo.mGoMapId = msg.reader().readInt();
                    mMap.Method178(GameController.myInfo.mGoMapId, msg.reader().readInt());
                    int i2 = GameController.myInfo.mGoMapId;
                    byte[] bArr2 = new byte[msg.reader().readInt()];
                    msg.reader().read(bArr2);
                    mMap.Method179(bArr2, i2);
                    return;
                case 24:
                    if (GameController.myInfo == null) {
                        GameController.myInfo = new Class13();
                    }
                    readPlayerInfor(true, msg);
                    return;
                case 25:
                    GameController.Method46(msg.reader().readUTF(), true);
                    return;
                case 27:
                    if (this.sceneManager != null) {
                        int readInt5 = msg.reader().readInt();
                        byte readByte5 = msg.reader().readByte();
                        int readInt6 = msg.reader().readInt();
                        int readInt7 = msg.reader().readInt();
                        if (readInt7 > 0) {
                            int[] iArr = new int[readInt7];
                            for (int i3 = 0; i3 < readInt7; i3++) {
                                iArr[i3] = msg.reader().readInt();
                            }
                            SceneManage class1402 = this.sceneManager;
                            if (SceneManage.currentScene == null || iArr == null || readInt6 != SceneManage.currentScene.Field890) {
                                return;
                            }
                            mCharacter Method220 = SceneManage.Method220(readInt5);
                            if (Method220 != null && Method220.Field792 && iArr.length > 0) {
                                Method220.autoMove(readByte5, iArr);
                            }
                            return;
                        }
                        return;
                    }
                    return;
                case 29:
                    if (this.sceneManager != null) {
                        GameController.myInfo.mGoMapId = msg.reader().readInt();
                        GameController.myInfo.mGoMapChanel = msg.reader().readInt();
                        byte readByte6 = msg.reader().readByte();
                        int readInt8 = msg.reader().readInt();
                        int readInt9 = msg.reader().readInt();
                        SceneManage class1403 = this.sceneManager;
                        SceneManage.Method0();
                        PetGameModel.Method18();
                        Integer num = (Integer) this.Field988.get(new Integer(GameController.myInfo.mGoMapId));
                        int i4 = GameController.myInfo.mGoMapId;
                        if (num != null) {
                            i4 = num.intValue();
                        }
                        mMap class137 = new mMap(i4, Method208().sceneManager, readInt8, readInt9);
                        PetGameScreen class49 = new PetGameScreen(this.sceneManager);
                        if (readByte6 >= 0) {
                            for (int i = 0; i < class137.Field935.length; i++) {
                                if (class137.Field935[i].Field861 == readByte6) {
                                    readInt8 = class137.Field935[i].Field862;
                                    readInt9 = class137.Field935[i].Field863;
                                    break;
                                }
                            }

                        }
                        this.sceneManager.Method223(class49, class137, GameController.myInfo.mGoMapId, readInt8, readInt9);
                        SceneManage.currentScene = class49;

                        while (msg.reader().available() > 0) {
                            readPlayerInfor(false, msg);
                        }
                        class49.switchToMe(0);
                        if (GameController.myInfo.mGoMapId != 12) {
                            String stringBuffer = new StringBuffer().append(ActorFactory.gL(666)).append(' ').append(String.valueOf(GameController.myInfo.mGoMapChanel)).toString();
                            if (BaseCanvas.w - 20 < GameResourceManager.getSpecialFont().getWidth(stringBuffer)) {
                                stringBuffer = new StringBuffer().append(ActorFactory.gL(666)).append(' ').append(String.valueOf(GameController.myInfo.mGoMapChanel)).toString();
                            }
                            BigTextEffect class22 = new BigTextEffect(stringBuffer);
                            class22.start();
                            GameScene.animationEffects.addElement(class22);
                        }
                        GameScene.updateMapIndicator();
                        GameScene.playBgRandom();
                        return;
                    }
                    return;
                case 30:
                    if (this.sceneManager != null) {
                        int readInt10 = msg.reader().readInt();
                        msg.reader().readByte();
                        int readInt11 = msg.reader().readInt();
                        if (readInt11 <= 0) {
                            this.sceneManager.Method79(readInt10);
                            return;
                        }
                        int[] iArr2 = new int[readInt11];
                        for (int i6 = 0; i6 < readInt11; i6++) {
                            iArr2[i6] = msg.reader().readInt();
                        }
                        this.sceneManager.Method79(readInt10);
                        return;
                    }
                    return;
                case 31:
                    if (GameController.myInfo == null) {
                        GameController.myInfo = new Class13();
                    }
                    GameController.myInfo.Field48 = msg.reader().readInt();
                    GameController.myInfo.Field50 = msg.reader().readUTF();
                    GameController.myInfo.Field45 = msg.reader().readInt();
                    mCharacter Method487 = ActorFactory.Method487(GameController.myInfo.Field48, (byte) GameController.myInfo.Field45, this.sceneManager);
                    SceneManage.myCharacter = Method487;
                    Method487.setName(GameController.myInfo.Field50);
                    SceneManage.myCharacter.Field808 = msg.reader().readByte();
                    GameController.Field454 = msg.reader().readInt();
                    return;
                case 34:
                    while (msg.reader().available() > 0) {
                        switch (msg.reader().readByte()) {
                            case 0:
                                if (this.sceneManager != null) {
                                    int readInt12 = msg.reader().readInt();
                                    int readInt13 = msg.reader().readInt();
                                    int readInt14 = msg.reader().readInt();
                                    int readInt15 = msg.reader().readInt();
                                    int npcId = msg.reader().readInt();
                                    String imgPath = msg.reader().readUTF();
                                    if (imgPath.equals("")) {
                                        imgPath = null;
                                    }
                                    int readInt17 = msg.reader().readInt();
                                    int[] iArr3 = new int[readInt17];
                                    for (int i7 = 0; i7 < readInt17; i7++) {
                                        iArr3[i7] = msg.reader().readInt();
                                    }
                                    int readInt18 = msg.reader().readInt();
                                    int readInt19 = msg.reader().readInt();
                                    String[] strArr = null;
                                    if (readInt19 != 0) {
                                        strArr = new String[readInt19];
                                        for (int i8 = 0; i8 < readInt19; i8++) {
                                            strArr[i8] = msg.reader().readUTF();
                                        }
                                    }
                                    Npc npc = new Npc(npcId, this.sceneManager);
                                    npc.setName(msg.reader().readUTF());
                                    npc.setType(msg.reader().readByte());
                                    npc.setCollisionRec(new Rectangle(readInt12, readInt13, readInt14, readInt15));
                                    npc.imageId = imgPath;
                                    if (iArr3.length > 2) {
                                        npc.autoMove((byte) 0, iArr3);
                                        npc.frameNum = 10;
                                    } else {
                                        npc.stand(-1L);
                                    }
                                    if (strArr != null) {
                                        npc.setTexts(strArr, readInt18);
                                    }

                                    if (SceneManage.currentScene != null) {
                                        SceneManage.currentScene.addActor(npc, iArr3[0], iArr3[1], false);
                                        if (npc.isHuman) {
                                            npc.useCollisionRectToDetectCollision = true;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 35:
                    String readUTF6 = msg.reader().readUTF();
                    boolean readBoolean = msg.reader().readBoolean();
                    String readUTF7 = msg.reader().readUTF();
                    String readUTF8 = msg.reader().readUTF();
                    Class144 class1446 = this.Field984;
                    GameController.instance.Method54(readUTF6, readBoolean, readUTF7, readUTF8);
                    return;
                case 42:
                    switch (msg.reader().readByte()) {
                        case 12:
                            int readByte7 = msg.reader().readByte();
                            GameController.Field481 = new String[readByte7];
                            GameController.Field482 = new String[readByte7];
                            GameController.Field484 = new byte[readByte7];
                            GameController.Field483 = new byte[readByte7];
                            for (int i9 = 0; i9 < readByte7; i9++) {
                                GameController.Field483[i9] = msg.reader().readByte();
                                GameController.Field481[i9] = msg.reader().readUTF();
                                GameController.Field482[i9] = msg.reader().readUTF();
                                GameController.Field484[i9] = msg.reader().readByte();
                            }
                            return;
                        case 13:
                            this.Field988.clear();
                            int readInt20 = msg.reader().readInt();
                            for (int i10 = 0; i10 < readInt20; i10++) {
                                this.Field988.put(new Integer(msg.reader().readInt()), new Integer(msg.reader().readInt()));
                            }
                            break;
                    }
                    return;
                case 44:
                    GameController.Method6();
                    Vector vector2 = new Vector();
                    while (msg.reader().available() > 0) {
                        byte readByte8 = msg.reader().readByte();
                        Class68 class68 = new Class68(readByte8);
                        if (readByte8 == 2 || readByte8 == 4) {
                            class68.Field435 = msg.reader().readByte();
                        }
                        class68.Field436 = msg.reader().readUTF();
                        class68.Field437 = msg.reader().readUTF();
                        class68.Field438 = msg.reader().readUTF();
                        if (readByte8 == 0) {
                            class68.Field439 = msg.reader().readUTF();
                        }
                        vector2.addElement(class68);
                    }
                    GameController.Method59(vector2);
                    return;
                case 45:
                    switch (msg.reader().readByte()) {
                        case 0:
                            GameController.Method40(msg.reader().readUTF(), true);
                            return;
                        case 1:
                            String readUTF9 = msg.reader().readUTF();
                            BaseCanvas.getCurrentScreen();
                            Screen.Method313(readUTF9);
                            return;
                        case 2:
                            this.Field984.Method133(msg.reader().readUTF(), msg.reader().readUTF());
                            return;
                        case 4:
                            final int readInt21 = msg.reader().readInt();
                            String readUTF10 = msg.reader().readUTF();
                            final Screen getCurrentScreen = BaseCanvas.getCurrentScreen();
                            Dialog.Method276(readUTF10, new Command(ActorFactory.gL(580), new IActionListener() {
                                ///////@Override
                                public void actionPerformed(Object obj) {
                                    GlobalService.Method264(readInt21, true);
                                    getCurrentScreen.hideDialog();
                                }
                            }), null, new Command(ActorFactory.gL(300), new IActionListener() {
                                ///////@Override
                                public void actionPerformed(Object obj) {
                                    GlobalService.Method264(readInt21, false);
                                    getCurrentScreen.hideDialog();
                                }
                            }), true);
                            return;
                        case 5:
                            if (BaseCanvas.getCurrentScreen() != null) {
                                Popup class27 = new Popup(msg.reader().readUTF());
                                class27.start();
                                Screen.animationEffects.addElement(class27);
                                return;
                            }
                            break;
                        case 6:
                            final int readInt22 = msg.reader().readInt();
                            String readUTF11 = msg.reader().readUTF();
                            String readUTF12 = msg.reader().readUTF();
                            final Screen getCurrentScreen2 = BaseCanvas.getCurrentScreen();
                            Command Command = new Command(ActorFactory.gL(580), new IActionListener() {
                                public void actionPerformed(Object obj) {
                                    GlobalService.Method264(readInt22, true);
                                    getCurrentScreen2.hideDialog();
                                }
                            });
                            Command Command2 = new Command(ActorFactory.gL(300), new IActionListener() {
                                public void actionPerformed(Object obj) {
                                    GlobalService.Method264(readInt22, false);
                                    getCurrentScreen2.hideDialog();
                                }
                            });
                            GameController.Method68(readInt22, readUTF11, readUTF12, (byte) 1);
                            if (Screen.currentDialog != null) {
                                Screen.currentDialog.cmdLeft = Command;
                                Screen.currentDialog.cmdRight = Command2;
                                break;
                            }
                            break;
                    }
                    return;
                case 48:
                    this.Field984.Method133(msg.reader().readUTF(), msg.reader().readUTF());
                    return;
                case 61:
                    msg.reader().readInt();
                    msg.reader().readLong();
                    msg.reader().readLong();
                    msg.reader().readLong();
                    Class144 class1447 = this.Field984;
                    return;
                case 64:
                    int readInt23 = msg.reader().readInt();
                    Vector vector3 = new Vector();
                    for (int i11 = 0; i11 < readInt23; i11++) {
                        vector3.addElement(new Server(msg.reader().readUTF().trim(), msg.reader().readUTF().trim(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readInt()));
                    }
                    for (int i12 = 0; i12 < readInt23; i12++) {
                        Server class15 = (Server) vector3.elementAt(i12);
                        class15.Field54 = msg.reader().readBoolean();
                        class15.Field55 = msg.reader().readBoolean();
                    }
                    GlobalService.serverList = vector3;
                    LoginScreen.Method131();
                    Screen getCurrentScreen3 = BaseCanvas.getCurrentScreen();
                    if ((getCurrentScreen3 instanceof LoginScreen) && LoginScreen.Field634) {
                        ((LoginScreen) getCurrentScreen3).showSelectServerModal();
                        return;
                    }
                    return;
                case 65:
                    return;
                case 71:
                    BaseCanvas.getCurrentScreen().hideDialog();
                    msg.reader().readByte();
                    GameController.startOKDlg(msg.reader().readUTF());
                    return;
                case 72:
                    byte readByte9 = msg.reader().readByte();
                    byte b = readByte9;
                    if (readByte9 == -1) {
                        b = msg.reader().readByte();
                        readUTF = null;
                    } else {
                        readUTF = msg.reader().readUTF();
                    }
                    Class144 class1448 = this.Field984;
                    String str2 = readUTF;
                    switch (b) {
                        case 1:
                            GameController.Method48(str2);
                            return;
                        case 2:
                        case 9:
                            GameController.Method51(str2);
                            return;
                        case 3:
                        case 10:
                            Dialog.Method276(ActorFactory.gL(30), null, new Command(2, ActorFactory.gL(337), str2, class1448), null, true);
                            break;
                    }
                    return;
                case 73:
                    byte readByte10 = msg.reader().readByte();
                    String readUTF13 = msg.reader().readUTF();
                    String readUTF14 = msg.reader().readUTF();
                    Class144 class1449 = this.Field984;
                    GameController.instance.Method53(readByte10, readUTF13, readUTF14);
                    return;
                case 74:
                    GlobalService.Method240(msg.reader().readUTF(), new StringBuffer("sms://").append(msg.reader().readUTF()).toString(), new IActionListener() {
                        ///////@Override
                        public void actionPerformed(Object obj) {
                            try {
                                GameController.startOKDlg(msg.reader().readUTF());
                            } catch (IOException ex) {
                                ex.printStackTrace();
                            }
                        }
                    }, new IActionListener() {
                        ///////@Override
                        public void actionPerformed(Object obj) {

                        }
                    });
                    return;
                case 76:
                    Class144 class14410 = this.Field984;
                    ActorFactory.deleteAllRecordStore();
                    return;
                case 83:
                    BaseCanvas.instance.midlet.platformRequest(msg.reader().readUTF());
                    return;
                case 96:
                    readByte = msg.reader().readByte();
                    byte imageType = msg.reader().readByte();
                    switch (readByte) {

                        default:
                            if (imageType == 11 || imageType == 10) {
                                break;
                            }
                            String imgName = msg.reader().readUTF();
                            int len = msg.reader().readInt();
                            if (len != 0) {
                                byte[] data = new byte[len];
                                msg.reader().read(data);
                                Image itemImage = Image.createImage(data, 0, data.length);
                                if (imageType == 4) {
//                                GameResourceManager.addShopItemImage(Integer.parseInt(imgName), itemImage);
                                } else {
                                    GameResourceManager.addGuiderImg(imgName, itemImage, imageType);
                                }
                            }
                            break;
                    }
                    break;
                case Cmd.LETTER_COMMAND:
                    switch (msg.reader().readInt()) {
                        case 18:
                            PetGameScreen.hasLetter = msg.reader().readByte() == 1;
                            return;
                        case Cmd.LETTER_BOX:
                            if (this.Field989 == null) {
                                this.Field989 = new MGOMessageScreen();
                            } else {
                                this.Field989.clear();
                            }
                            MGOMessageScreen screen = this.Field989;
                            int size = msg.reader().readInt();
                            for (int i13 = 0; i13 < size; i13++) {
                                int id = msg.reader().readInt();
                                byte type = msg.reader().readByte();
                                String title = msg.reader().readUTF();
                                String shortContent = msg.reader().readUTF();
                                String content = msg.reader().readUTF();
                                boolean isMark = msg.reader().readBoolean();
                                switch (type) {
                                    case 1:
                                        screen.setLetter0(id, title, shortContent, content, isMark);
                                        break;
                                    case 2:
                                        screen.setLetter2(id, title, shortContent, content, isMark);
                                        break;
                                    case 3:
                                        screen.setLetter1(id, title, shortContent, content, isMark);
                                        break;
                                }
                            }
                            screen.switchToMe(1, true);
                            break;
                    }
                    return;
                case Cmd.COMMAND_GUIDER:
                    switch (msg.reader().readByte()) {
                        case 1:
                            if (this.sceneManager != null && GameController.Field460 == 0) {
                                int readInt26 = msg.reader().readInt();
                                boolean z = false;
                                Vector vector4 = Screen.dialogs;
                                int i14 = 0;
                                while (i14 < vector4.size()) {

                                    Dialog Dialog = (Dialog) vector4.elementAt(i14);
                                    if ((Dialog instanceof Class102) && ((Class102) Dialog).Field623 == readInt26) {
                                        z = true;
                                    } else {
                                        i14++;
                                    }
                                }
                                if (!z) {
                                    msg.reader().readUTF();
                                    int readInt27 = msg.reader().readInt();
                                    String[] strArr2 = new String[readInt27];
                                    for (int i15 = 0; i15 < readInt27; i15++) {
                                        strArr2[i15] = msg.reader().readUTF();
                                    }
                                    String readUTF18 = msg.reader().readUTF();
                                    msg.reader().readInt();
                                    new Class102(readInt26, readUTF18, strArr2, SceneManage.myCharacter.yChar - 54).show(false);
                                    if (SceneManage.currentScene != null) {
                                        SceneManage.currentScene.Method191();
                                    }
                                    return;
                                }
                            }
                            break;
                        case 2:
                            if (this.sceneManager != null && (readInt3 = msg.reader().readInt()) < 0) {
                                SceneManage class1404 = this.sceneManager;
                                Npc class1382 = (Npc) SceneManage.Method220(readInt3);
                                if (class1382 != null) {
                                    int readInt28 = msg.reader().readInt();
                                    class1382.Method18();
                                    for (int i16 = 0; i16 < readInt28; i16++) {
                                        int readInt29 = msg.reader().readInt();
                                        String readUTF19 = msg.reader().readUTF();
                                        int readInt30 = msg.reader().readInt();
                                        String[] strArr3 = new String[readInt30];
                                        for (int i17 = 0; i17 < readInt30; i17++) {
                                            strArr3[i17] = msg.reader().readUTF();
                                        }
                                        class1382.addGuide(readUTF19, new Class102(readInt29, msg.reader().readUTF(), strArr3, class1382.yChar - class1382.Field947));
                                    }
                                    class1382.Method0();
                                    return;
                                }
                            }
                            break;
                        case 3:
                            int readInt31 = msg.reader().readInt();
                            if (msg.reader().readInt() == 0) {
                                GameController.Method47(msg.reader().readUTF());
                                return;
                            }
                            String readUTF20 = msg.reader().readUTF();
                            msg.reader().readUTF();
                            int readInt32 = msg.reader().readInt();
                            int[] iArr4 = new int[readInt32];
                            String[] strArr4 = new String[readInt32];
                            byte[] bArr3 = new byte[readInt32];
                            Command[] CommandArr = new Command[readInt32];
                            for (int i18 = 0; i18 < readInt32; i18++) {
                                iArr4[i18] = msg.reader().readInt();
                                strArr4[i18] = msg.reader().readUTF();
                                bArr3[i18] = msg.reader().readByte();
                                if (bArr3[i18] != 0) {
                                    CommandArr[i18] = new Command(43, ActorFactory.gL(419), new int[]{readInt31, iArr4[i18]}, GameController.instance);
                                }
                            }
                            GameController.Method63(readInt31, readUTF20, iArr4, strArr4, bArr3, CommandArr).show(true);
                            return;
                        case 5:
                            if (this.sceneManager != null && (readInt2 = msg.reader().readInt()) < 0) {
                                SceneManage class1405 = this.sceneManager;
                                Npc class1383 = (Npc) SceneManage.Method220(readInt2);
                                if (class1383 != null) {
                                    int readInt33 = msg.reader().readInt();
                                    int[] iArr5 = new int[readInt33];
                                    String[] strArr5 = new String[readInt33];
                                    for (int i19 = 0; i19 < readInt33; i19++) {
                                        iArr5[i19] = msg.reader().readInt();
                                        strArr5[i19] = msg.reader().readUTF();
                                    }
                                    class1383.Method174(strArr5, iArr5);
                                    return;
                                }
                            }
                            break;
                        case 6:
                            int readInt34 = msg.reader().readInt();
                            String readUTF21 = msg.reader().readUTF();
                            int readInt35 = msg.reader().readInt();
                            Vector vector5 = new Vector();
                            for (int i20 = 0; i20 < readInt35; i20++) {
                                vector5.addElement(new MenuItemInfo(msg.reader().readInt(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte()));
                            }
                            Method217(readInt34, vector5, (byte) -1, readUTF21);
                            return;
                        case 7:
                            int readInt36 = msg.reader().readInt();
                            String readUTF22 = msg.reader().readUTF();
                            int readInt37 = msg.reader().readInt();
                            String[] strArr6 = new String[readInt37];
                            byte[] bArr4 = new byte[readInt37];
                            for (int i21 = readInt37 - 1; i21 >= 0; i21--) {
                                strArr6[i21] = msg.reader().readUTF();
                                bArr4[i21] = msg.reader().readByte();
                            }
                            GameController.Method65(122, 7, readInt36, readUTF22, strArr6, bArr4);
                            break;
                        case 8:
                            int readInt38 = msg.reader().readInt();
                            byte readByte12 = msg.reader().readByte();
                            String readUTF23 = msg.reader().readUTF();
                            int readInt39 = msg.reader().readInt();
                            Vector vector6 = new Vector();
                            for (int i22 = 0; i22 < readInt39; i22++) {
                                MenuItemInfo class5 = new MenuItemInfo(msg.reader().readInt(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte());
                                vector6.addElement(class5);
                                class5.Field7 = msg.reader().readBoolean();
                                if (class5.Field7) {
                                    class5.Field8 = msg.reader().readUTF();
                                    class5.Field9 = msg.reader().readUTF();
                                    msg.reader().readUTF();
                                }
                                class5.saleStatus = msg.reader().readByte();
                                class5.Field11 = msg.reader().readBoolean();
                                int readInt40 = msg.reader().readInt();
                                class5.Field13 = new int[readInt40];
                                class5.Field12 = new String[readInt40];
                                class5.Field14 = new byte[readInt40];
                                for (int i23 = 0; i23 < readInt40; i23++) {
                                    class5.Field13[i23] = msg.reader().readInt();
                                    class5.Field12[i23] = msg.reader().readUTF();
                                    class5.Field14[i23] = msg.reader().readByte();
                                }
                            }
                            Method217(readInt38, vector6, readByte12, readUTF23);
                            return;
                        case 10:
                            int readInt41 = msg.reader().readInt();
                            byte readByte13 = msg.reader().readByte();
                            String readUTF24 = msg.reader().readUTF();
                            int readInt42 = msg.reader().readInt();
                            Vector vector7 = new Vector();
                            for (int i24 = 0; i24 < readInt42; i24++) {
                                MenuItemInfo class52 = new MenuItemInfo(msg.reader().readInt(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte());
                                vector7.addElement(class52);
                                class52.Field7 = msg.reader().readBoolean();
                                if (class52.Field7) {
                                    class52.Field8 = msg.reader().readUTF();
                                    class52.Field9 = msg.reader().readUTF();
                                    msg.reader().readUTF();
                                }
                                class52.Field11 = msg.reader().readBoolean();
                            }
                            Method217(readInt41, vector7, readByte13, readUTF24);
                            return;
                        case 11:
                            new Class93(msg.reader().readInt(), msg.reader().readUTF(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readInt()).show(false);
                            return;
                    }
                    return;
                case 125:
                    switch (msg.reader().readByte()) {
                        case 1:
                            GameController.Method68(msg.reader().readInt(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte());
                            break;
                    }
                    return;
                default:
                    if (this.Field985 != null) {
                        this.Field985.onMssage(msg);
                        return;
                    }
                    return;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    private void readPlayerInfor(boolean z, Message message) {
        mCharacter class126;
        try {
            int readInt = message.reader().readInt();
            String readUTF = message.reader().readUTF();
            byte readByte = message.reader().readByte();
            byte readByte2 = message.reader().readByte();
            byte mSpeed = message.reader().readByte();
            message.reader().readByte();
            byte b = -1;
            if (z) {
                b = message.reader().readByte();
            }
            int readInt2 = message.reader().readInt();
            int readInt3 = message.reader().readInt();
            if (b >= 0 && SceneManage.currentScene != null) {
                mMap class137 = SceneManage.currentScene.Field879;
                int i = 0;
                while (true) {
                    if (i >= class137.Field935.length) {
                        break;
                    } else if (class137.Field935[i].Field861 == b) {
                        readInt2 = class137.Field935[i].Field862;
                        readInt3 = class137.Field935[i].Field863;
                        break;
                    } else {
                        i++;
                    }
                }
            }
            SceneManage class140 = this.sceneManager;
            int i2 = readInt2;
            int i3 = readInt3;
            if (SceneManage.currentScene == null) {
                class126 = null;
            } else {
                mCharacter Method220 = SceneManage.Method220(readInt);
                mCharacter class1262 = Method220;
                if (Method220 == null) {
                    mCharacter Method487 = ActorFactory.Method487(readInt, readByte, class140);
                    class1262 = Method487;
                    Method487.setName(readUTF);
                    SceneManage.currentScene.addActor(class1262, i2, i3, false);
                    class1262.xChar = i2;
                    class1262.yChar = i3;
                    SceneManage.currentScene.listChar.Method153(SceneManage.myCharacter);
                    class1262.centerObjectCMD = new Command(900, ActorFactory.gL(419), class140);
                    class1262.stand(-1L);
                    class1262.Field808 = readByte2;
                }
                class126 = class1262;
            }
            mCharacter class1263 = class126;
            if (class126 != null) {
                class1263.speed = mSpeed;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static byte[] Method214(Message message) {
        try {
            int readInt = message.reader().readInt();
            if (readInt <= 0) {
                return null;
            }
            byte[] bArr = new byte[readInt];
            message.reader().read(bArr);
            return bArr;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    private void Method215(Message message, byte b) {
        try {
            switch (b) {
                case 7:
                    int readInt = message.reader().readInt();
                    message.reader().readByte();
                    message.reader().readInt();
                    message.reader().readInt();
                    message.reader().readInt();
                    byte[] Method214 = Method214(message);
                    Image createImage = Image.createImage(Method214, 0, Method214.length);
                    if (this.sceneManager.Method219(readInt) != null) {
                        createImage.getWidth();
                        return;
                    }
                    return;
                default:
                    return;
            }
        } catch (IOException unused) {
        }
    }

    public static void Method216() {
        SceneManage.Method18();
        Method208().sceneManager = null;
        Method208().Field985 = null;
    }

    private static boolean Method217(int i, Vector vector, byte b, String str) {
        Screen getCurrentScreen = BaseCanvas.getCurrentScreen();
        if (getCurrentScreen instanceof MenuScreen) {
            getCurrentScreen.Method309();
            MenuScreen class6 = (MenuScreen) getCurrentScreen;
            if (class6.menuId == i) {
                class6.setMenuItemList(vector);
                class6.setMenuId(i);
                return true;
            }
        }
        MenuScreen class62 = new MenuScreen();
        class62.menuType = b;
        class62.setMenuItemList(vector);
        class62.setTitle(str);
        class62.setMenuId(i);
        class62.switchToMe(1, true);
        return false;
    }
}

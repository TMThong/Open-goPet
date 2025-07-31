package defpackage;

import vn.me.ui.EmotionDialog;
import vn.me.ui.MessageDialog;
import vn.me.ui.common.LAF;
import vn.me.screen.MoneyScreen;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.Button;
import vn.me.ui.InputDialog;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.screen.BoardListScreen;
import vn.me.ui.Dialog;
import vn.me.ui.Label;
import vn.me.ui.EditField;
import vn.me.ui.WidgetGroup;
import vn.me.ui.Widget;
import vn.me.ui.geom.Dimension;
import vn.me.screen.GameScene;
import vn.me.screen.SplashScreen;
import vn.me.screen.MessageScreen;
import vn.me.screen.LoginScreen;
import vn.me.screen.Screen;
import java.util.Vector;
import javax.microedition.io.ConnectionNotFoundException;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.Displayable;
import javax.microedition.midlet.MIDlet;
import vn.me.network.Cmd;
import vn.me.ui.common.T;

public final class GameController extends Screen implements IActionListener, CommandListener {

    public static Class13 myInfo;
    public static int Field454;
    public static boolean Field455;
    private static String Field456;
    public static GameController instance;
    private String Field458;
    private String Field459;
    public static int Field460;
    public static String Field461;
    public static String Field462;
    public static Command Field463;
    public static Command Field464;
    public static Command Field465;
    public static Command Field466;
    public static Command Field467;
    public static Command Field468;
    public static Command Field469;
    public static Command Field470;
    private static Command Field471;
    private static Command Field472;
    private static Command Field473;
    private static Command Field474;
    public static Command letterCmd;
    public static Command Field476;
    private static Command Field477;
    private static Command Field478;
    public static Command Field479;
    public static String[] Field481;
    public static String[] Field482;
    public static byte[] Field483;
    public static byte[] Field484;
    private static String[] Field480 = {ActorFactory.gL(559), ActorFactory.gL(129)};
    private static boolean Field485 = true;

    private static void Method26() {
        Field463.Field1321 = ActorFactory.gL(337);
        Field464.Field1321 = ActorFactory.gL(41);
        Field465.Field1321 = ActorFactory.gL(40);
        Field466.Field1321 = ActorFactory.gL(63);
        Field467.Field1321 = ActorFactory.gL(64);
        Field468.Field1321 = ActorFactory.gL(222);
        Field469.Field1321 = ActorFactory.gL(249);
        Field470.Field1321 = ActorFactory.gL(503);
        Field472.Field1321 = ActorFactory.gL(337);
        Field473.Field1321 = ActorFactory.gL(123);
        letterCmd.Field1321 = ActorFactory.gL(276);
        Field471.Field1321 = ActorFactory.gL(495);
        Field478.Field1321 = ActorFactory.gL(18);
        Field479.Field1321 = ActorFactory.gL(284);
    }

    public static String Method33() {
        return BaseCanvas.instance.midlet.getAppProperty("MIDlet-Name");
    }

    public static String Method34() {
        return BaseCanvas.instance.midlet.getAppProperty("MIDlet-Version");
    }

    public GameController() {
        Field463 = new Command(0, ActorFactory.gL(337), this);
        Field464 = new Command(1, ActorFactory.gL(41), this);
        Field465 = new Command(3, ActorFactory.gL(40), this);
        Field466 = new Command(4, ActorFactory.gL(63), this);
        Field467 = new Command(5, ActorFactory.gL(64), this);
        Field468 = new Command(6, ActorFactory.gL(222), this);
        Field469 = new Command(7, ActorFactory.gL(249), this);
        Field470 = new Command(8, ActorFactory.gL(503), this);
        Field472 = new Command(10, ActorFactory.gL(337), this);
        EditField.Field1091 = new Command(11, ActorFactory.gL(123), this);
        letterCmd = new Command(17, ActorFactory.gL(276), this);
        Field476 = new Command(18, ActorFactory.gL(24), this);
        Field471 = new Command(24, ActorFactory.gL(495), this);
        Field473 = new Command(26, ActorFactory.gL(123), this);
        Field478 = new Command(39, ActorFactory.gL(18), this);
        Field477 = new Command(41, ActorFactory.gL(668), this);
        Field479 = new Command(51, ActorFactory.gL(284), this);
        Integer Method493 = ActorFactory.loadInt("guide");
        Field460 = Method493 == null ? 0 : Method493.intValue();
        Integer Method4932 = ActorFactory.loadInt("vibrate");
        Field485 = Method4932 == null || Method4932.intValue() != 0;
    }

    public static void startApp(MIDlet mIDlet) {
        BaseCanvas.create(mIDlet).Method0();
        GlobalService.Field1008 = Integer.parseInt(BaseCanvas.instance.midlet.getAppProperty("ProviderId"));
        new SplashScreen(0).switchToMe(0, false);
    }

    public static void Method36() {
        BaseCanvas.instance.resetScreen();
    }

    public static void destroyApp() {
        BaseCanvas.isRunning = false;
        instance = null;
        if (GlobalService.session != null) {
            GlobalService.session.close();
        }
        if (BaseCanvas.instance != null) {
            BaseCanvas.instance.midlet.notifyDestroyed();
        }
        BaseCanvas.instance = null;
    }

    public static void pauseApp() {
        BaseCanvas.isPause = true;
    }

    public static void Method38() {
        BaseCanvas.isPause = false;
        BaseCanvas.instance.resetScreen();
    }

    public static void startOKDlg(String str) {
        Method40(str, false);
    }

    public static void Method40(String str, boolean z) {
        Dialog.Method276(str, null, Field463, null, z);
    }

    public static void Method41(String str) {
        Method44(str, false);
    }

    public static void waitDialog() {
        show(false);
    }

    public static void show(boolean z) {
        Method44(ActorFactory.gL(363), z);
    }

    public static void Method44(String str, boolean z) {
        new MessageDialog(str, null, Field464, null, 2).show(z);
    }

    public static void Method45(String str, Command Command, Command Command2) {
        Dialog.Method276(str, null, Command, Command2, true);
    }

    public static void Method46(String str, boolean z) {
        Dialog.Method277(str, null, Field463, null, z);
    }

    public static void Method47(String str) {
        Method46(str, true);
    }

    public static void Method48(String str) {
        try {
            BaseCanvas.getCurrentScreen();
            Screen.hideDialog(Screen.currentDialog);
            BaseCanvas.instance.midlet.platformRequest(new StringBuffer("sms:?body=").append(str).toString());
        } catch (Exception unused) {
            Class177 class177 = new Class177(ActorFactory.gL(222), str, instance, Display.getDisplay(BaseCanvas.instance.midlet));
            BaseCanvas.isPause = true;
            Display.getDisplay(BaseCanvas.instance.midlet).setCurrent(class177);
        }
    }

    public final void actionPerformed(Object obj) {
        String str;
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case -1:
                Method38();
                return;
            case 0:
            case 1:
                BaseCanvas.getCurrentScreen();
                Screen.hideDialog(Screen.currentDialog);
                return;
            case 2:
                Method49();
                return;
            case 3:
                GlobalService.Field1001 = new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        GameController.waitDialog();
                        GlobalService.Method243(2);
                    }
                };
                GlobalService.Field1002 = new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        GameController.Method51(null);
                    }
                };
                if (GlobalService.Method233()) {
                    GlobalService.Field1001.actionPerformed(null);
                    return;
                }
                Method44(ActorFactory.gL(83), false);
                GlobalService.Method235();
                return;
            case 4:
                String Method486 = ActorFactory.gL(87);
                if (BaseCanvas.w <= 128) {
                    String trim = Method486.trim();
                    str = trim.equals(T.gL(T.CONTENT_STR)) ? T.gL(T.CONTENT_STR) : trim;
                } else {
                    str = Method486;
                }
                Method45(ActorFactory.gL(73), new Command(401, str, this), new Command(402, ActorFactory.gL(103), this));
                return;
            case 5:
                BaseCanvas.getCurrentScreen().Method297(null);
                return;
            case 6:
                show(false);
                new Thread(new Class80(this)).start();
                return;
            case 7:
                try {
                    BaseCanvas.instance.midlet.platformRequest(GlobalService.Field1011);
                    return;
                } catch (ConnectionNotFoundException unused) {
                    return;
                }
            case 8:
                GlobalService.Field1001 = new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        GameController.Method41(ActorFactory.gL(363));
                        GlobalService.Method243(1);
                    }
                };
                if (GlobalService.Method233()) {
                    GlobalService.Field1001.actionPerformed(null);
                    GlobalService.Field1001 = null;
                    return;
                }
                Method44(ActorFactory.gL(83), false);
                GlobalService.Method235();
                return;
            case 9:
                Method56(ActorFactory.gL(532), new String[]{ActorFactory.gL(299), ActorFactory.gL(376)}, new int[]{0, 1}, Field472, Field464);
                return;
            case 10:
                InputDialog class172 = (InputDialog) Screen.currentDialog;
                String Method327 = class172.Method327(0);
                String Method3272 = class172.Method327(1);
                try {
                    Object[] objArr = new Object[2];
                    objArr[0] = Method327;
                    int parseInt = Integer.parseInt(Method3272.length() == 0 ? "0" : Method3272);
                    objArr[1] = new Integer(parseInt);
                    Dialog.Method275(new StringBuffer().append(ActorFactory.gL(299)).append(" ").append(Method327).append(". ").append(ActorFactory.gL(376)).append(" ").append(parseInt).append(".").toString(), new Command(1001, ActorFactory.gL(580), this), null, Field464);
                    return;
                } catch (Exception unused2) {
                    return;
                }
            case 11:
                Widget Method315 = BaseCanvas.getCurrentScreen().getCurrentRoot().getFocusedWidget(true);
                if (Method315 instanceof EditField) {
                    Method61((EditField) Method315);
                    return;
                }
                return;
            case 16:
                String trim2 = ((InputDialog) Screen.currentDialog).txtInput.getText().trim();
                if ("".equals(trim2)) {
                    return;
                }
                BaseCanvas.getCurrentScreen();
                Screen.hideDialog(Screen.currentDialog);
                show(false);
                GlobalService.Method250(trim2, Class100.Field615);
                return;
            case 17:
                GameController.waitDialog();
                Message message21 = new Message(Cmd.LETTER_COMMAND);
                message21.putByte(Cmd.LETTER_BOX);
                GlobalService.session.sendMessage(message21);
                message21.cleanup();
                return;
            case 18:
                show(false);
                GlobalService.Method257();
                return;
            case 25:
                InputDialog class1722 = (InputDialog) Screen.currentDialog;
                if ("".equals(class1722.txtInput.getText())) {
                    BaseCanvas.getCurrentScreen();
                    Screen.hideDialog(Screen.currentDialog);
                    return;
                }
                GlobalService.Method256(class1722.txtInput.getText());
                Method40(ActorFactory.gL(504), true);
                return;
            case 26:
                Method61(BaseCanvas.getCurrentScreen().chatEditField);
                return;
            case 30:
                Vector vector = new Vector();
                for (int i = 0; i < Field480.length; i++) {
                    vector.addElement(new Command(i + 31, Field480[i], this));
                }
                BaseCanvas.getCurrentScreen().showMenu(vector, 2);
                return;
            case 31:
                try {
                    ((Button) Screen.currentDialog.getFocusedWidget(true)).text = Field480[0];
                    ActorFactory.saveInt("language", 0);
                    Method26();
                    Method62();
                    return;
                } catch (Exception unused3) {
                    return;
                }
            case 32:
                try {
                    ((Button) Screen.currentDialog.getFocusedWidget(true)).text = Field480[1];
                    ActorFactory.saveInt("language", 1);
                    Method26();
                    Method62();
                    return;
                } catch (Exception unused4) {
                    return;
                }
            case 39:
                show(false);
                GlobalService.Method269();
                return;
            case 41:
                if (Field483 == null) {
                    GlobalService.Method270();
                    return;
                } else if (Field483 == null || Field483.length == 0) {
                    return;
                } else {
                    Dialog Dialog = new Dialog(0, BaseCanvas.h, BaseCanvas.w, BaseCanvas.h);
                    Label class173 = new Label("Bản đồ", ResourceManager.boldFont);
                    class173.setMetrics(0, 0, Dialog.w, class173.h);
                    class173.padding = 0;
                    class173.border = 0;
                    class173.align = 17;
                    Dialog.addWidget(class173);
                    Dialog.addWidget(new Class14());
                    int i2 = 0;
                    int i3 = 0;
                    for (int i4 = 0; i4 < Field481.length; i4++) {
                        if (Field483[i4] != myInfo.mGoMapId) {
                            Button class165 = new Button(new StringBuffer().append(i2 + 1).append(".").append(Field481[i4]).toString(), ResourceManager.boldFont);
                            class165.setMetrics(0, 0, Dialog.w, class165.h);
                            class165.padding = 0;
                            class165.border = 0;
                            class165.align = 17;
                            class165.cmdCenter = new Command(42, ActorFactory.gL(419), instance);
                            class165.cmdCenter.objPerfomed = new Integer[]{new Integer(Field483[i4]), new Integer(Field484[i4])};
                            Dialog.addWidget(class165);
                            i2++;
                            i3 += 4 + class165.h;
                        }
                    }
                    Dialog.setViewMode(1);
                    Dialog.h = class173.h + 2 + (LAF.LOT_PADDING << 1) + i3;
                    Dialog.destY = BaseCanvas.Field158 - (Dialog.h >> 1);
                    Dialog.cmdRight = Field464;
                    Dialog.isLoop = true;
                    Dialog.show(true);
                    return;
                }
            case 42:
                Integer[] numArr = (Integer[]) Command.objPerfomed;
                BaseCanvas.currentScreen.Method309();
                if (numArr != null || numArr.length == 2) {
                    SceneManage.warp(numArr[0].intValue(), numArr[1].intValue(), mMap.getMapVersion(numArr[0].intValue()));
                    show(false);
                    return;
                }
                return;
            case 43:
                int[] iArr = (int[]) Command.objPerfomed;
                show(true);
                GlobalService.Method267(iArr[0], iArr[1]);
                return;
            case 44:
                int[] iArr2 = (int[]) Command.objPerfomed;
                show(true);
                GlobalService.Method268(iArr2[0], iArr2[1]);
                return;
            case 45:
                BaseCanvas.currentScreen.Method309();
                return;
            case 46:
                if (!Method66(Screen.currentDialog)) {
                    Method46(ActorFactory.gL(473), false);
                    return;
                }
                String[] Method67 = Method67(Screen.currentDialog);
                if (Method67 != null) {
                    int[] iArr3 = (int[]) Command.objPerfomed;
                    GlobalService.Method248(iArr3[0], iArr3[1], iArr3[2], Method67);
                    BaseCanvas.currentScreen.Method309();
                    return;
                }
                return;
            case 47:
                try {
                    Button class1652 = (Button) currentDialog.getFocusedWidget(true);
                    if (class1652 != null) {
                        class1652.Field1048 = !class1652.Field1048;
                        Field485 = class1652.Field1048;
                        return;
                    }
                    return;
                } catch (Exception unused5) {
                    return;
                }
            case 48:
                ActorFactory.saveInt("vibrate", Field485 ? 1 : 0);
                BaseCanvas.getCurrentScreen();
                Screen.hideDialog(Screen.currentDialog);
                return;
            case 51:
                Vector vector2 = new Vector();
                vector2.addElement(Field477);
                vector2.addElement(new Command(1103, new StringBuffer(T.gL(T.CHANGE_ZONE) + " (").append(myInfo.mGoMapChanel).append(")").toString(), ((GameScene) BaseCanvas.currentScreen).Field905));
                BaseCanvas.currentScreen.showMenu(vector2, 1);
                return;
            case 52:
                Vector vector3 = new Vector();
                vector3.addElement(new Command(53, ActorFactory.gL(670), this));
                vector3.addElement(new Command(54, ActorFactory.gL(671), this));
                BaseCanvas.currentScreen.showMenu(vector3, 1);
                return;
            case 100:
                Method50();
                return;
            case 101:
                destroyApp();
                return;
            case 131:
                try {
                    int parseInt2 = Integer.parseInt(((InputDialog) Screen.currentDialog).Method327(0));
                    show(true);
                    GlobalService.Method247(79, 3, parseInt2);
                    return;
                } catch (Exception unused6) {
                    return;
                }
            case 401:
                ActorFactory.deleteAllRecordStore();
                Method40(ActorFactory.gL(390), true);
                return;
            case 402:
                BaseCanvas.getCurrentScreen();
                Screen.hideDialog(Screen.currentDialog);
                return;
            default:
                return;
        }
    }

    private void Method49() {
        GlobalService.Method240(this.Field458, new StringBuffer("sms://").append(this.Field459).toString(), new IActionListener() {
            ///////@Override
            public void actionPerformed(Object obj) {
                BaseCanvas.getCurrentScreen().hideDialog();
                GameController.startOKDlg(ActorFactory.gL(387));
                GlobalService.Method241(GameController.Method76(GameController.this), GameController.Method77(GameController.this));
            }
        }, new IActionListener() {
            ///////@Override
            public void actionPerformed(Object obj) {
                BaseCanvas.getCurrentScreen().hideDialog();
                GameController.startOKDlg(ActorFactory.gL(46));
            }
        });
    }

    public static void Method50() {
        Field474 = new Command(101, ActorFactory.gL(580), instance);
        Dialog.Method275(new StringBuffer().append(ActorFactory.gL(604)).append((GlobalService.session.recvByteCount + GlobalService.session.sendByteCount) >> 10).append(ActorFactory.gL(140)).toString(), Field474, null, Field464);
    }

    public static void Method51(String str) {
        try {
            if (str == null) {
                BaseCanvas.instance.midlet.platformRequest(new StringBuffer("tel:").append(Method52()).toString());
                return;
            }
            BaseCanvas.instance.midlet.platformRequest(new StringBuffer("tel:").append(str).toString());
            try {
                ActorFactory.saveBuffer("Hotline", str.getBytes());
            } catch (Exception unused) {
            }
            Field456 = str;
        } catch (ConnectionNotFoundException unused2) {
        }
    }

    public static String Method52() {
        if (Field456 == null) {
            byte[] Method488 = ActorFactory.loadBuffer("Hotline", 1);
            String str = Method488 == null ? null : new String(Method488);
            Field456 = str;
            if (str == null) {
                Field456 = BaseCanvas.instance.midlet.getAppProperty("Hotline");
            }
        }
        return Field456;
    }

    public final void Method53(byte b, String str, String str2) {
        switch (b) {
            case 1:
                if (Field462 == null || Field462.length() == 0) {
                    Method40(ActorFactory.gL(178), true);
                    return;
                }
                Method40(ActorFactory.gL(435), true);
                GlobalService.Method240(new StringBuffer().append(str.substring(0, str.indexOf("?"))).append(Field462).toString(), new StringBuffer("sms://").append(str2).toString(), new IActionListener() {
                    public void actionPerformed(Object obj) {
                        GameController.Method40(ActorFactory.gL(350), true);
                    }
                }, new IActionListener() {
                    public void actionPerformed(Object obj) {
                        GameController.Method40(ActorFactory.gL(291), true);
                    }
                });
                Field462 = null;
                return;
            default:
                return;
        }
    }

    public final void Method54(String str, boolean z, String str2, String str3) {
        this.Field458 = str2;
        this.Field459 = str3;
        if (z) {
            Method49();
        } else {
            Method40(new StringBuffer().append(str).append(" ").append(ActorFactory.gL(167)).toString(), true);
        }
    }

    public static void Method55(String str, String str2, String str3) {
        Screen Screen = new Screen(true);
        Screen.title = str2;
        Screen.screenId = str;
        Class183 class183 = new Class183();
        Screen.cmdCenter = Field467;
        class183.setMetrics(0, LAF.Field1293, BaseCanvas.w, BaseCanvas.h - (LAF.Field1293 << 1));
        Screen.addWidget(class183);
        class183.Method367(str3, ResourceManager.boldFont);
        Screen.switchToMe(1, true);
    }

    public static void Method19(Vector vector) {
        BoardListScreen class64 = new BoardListScreen(ActorFactory.gL(384));
        class64.Method19(vector);
        class64.switchToMe(1, true);
    }

    public static InputDialog Method56(String str, String[] strArr, int[] iArr, Command Command, Command Command2) {
        InputDialog class172 = new InputDialog(str, strArr, iArr, Command, Command2);
        class172.Method204(null);
        return class172;
    }

    public static InputDialog Method57(String str, Command Command, Command Command2, int i) {
        InputDialog class172 = new InputDialog(str, Command, Command2 == null ? Field464 : Command2, i);
        class172.show(false);
        return class172;
    }

    public static InputDialog Method58(String[] strArr, int[] iArr, Command Command, final int i) {
        InputDialog Method56 = Method56(BaseCanvas.w <= 128 ? new StringBuffer().append(ActorFactory.gL(5)).append(" mGold: ").append(PetGameModel.mGoldStr).toString() : new StringBuffer().append(ActorFactory.gL(5)).append(" mGold: ").append(PetGameModel.mGoldStr).toString(), strArr, iArr, Command, Field464);
        final EditField class168 = (EditField) Method56.getWidgetAt(2);
        final EditField class1682 = (EditField) Method56.getWidgetAt(4);
        class1682.isFocusable = false;
        class1682.Field1070 = 15;
        class168.Field1097 = new IActionListener() {
            ///////@Override
            public void actionPerformed(Object obj) {
                if (class168.getText().trim().equals("")) {
                    class1682.setText("0");
                    return;
                }
                try {
                    long parseLong = Long.parseLong(class168.getText().trim());
                    if (parseLong <= PetGameModel.mGold) {
                        class1682.setText(Ulti.formatNumber(parseLong * i));
                        return;
                    }
                    class168.setText(new StringBuffer().append(PetGameModel.mGold).toString());
                } catch (Exception unused) {
                }
            }
        };
        return Method56;
    }

    public static void Method59(Vector vector) {
        Message.isiWin = false;
        MoneyScreen class9 = new MoneyScreen();
        class9.Method19(vector);
        class9.switchToMe(1, true);
    }

    public static void Method6() {
        if (Screen.currentDialog != null && (Screen.currentDialog instanceof MessageDialog) && ((MessageDialog) Screen.currentDialog).Field1159 == 2) {
            Screen.currentDialog.Method274();
        }
    }

    public static void Method60(String str) {
        new InputDialog(str, new Command(16, ActorFactory.gL(337), instance), Field464, 0).show(false);
    }

    public static void Method7() {
        LoginScreen LoginScreen = (LoginScreen) BaseCanvas.getCurrentScreen();
        LoginScreen.Method309();
        LoginScreen.Field660 = false;
        new Class100().show(true);
    }

    private static void Method61(EditField class168) {
        new EmotionDialog(class168).show(false);
    }

    public final void commandAction(javax.microedition.lcdui.Command command, Displayable displayable) {
    }

    public static void Method25() {
        int i = BaseCanvas.w < 128 ? BaseCanvas.w - 4 : BaseCanvas.w - 20;
        Dialog Dialog = new Dialog();
        Field480 = new String[]{ActorFactory.gL(559), ActorFactory.gL(129)};
        Label class173 = new Label(ActorFactory.gL(459));
        class173.align = 17;
        Class14 class14 = new Class14();
        Label class1732 = new Label(new StringBuffer().append(ActorFactory.gL(247)).append(":").toString());
        class1732.setMetrics(0, 0, Dialog.w, class1732.normalfont.getHeight() + 8);
        class1732.isFocusable = true;
        Integer Method493 = ActorFactory.loadInt("language");
        Button class165 = Method493 != null ? new Button(Field480[Method493.intValue()]) : new Button(Field480[0]);
        class165.align = 17;
        class165.onFocusAction = instance;
        Dialog.isScrollableY = true;
        Dialog.isLoop = true;
        Dialog.spacing = 0;
        Dialog.addWidget(class173);
        Dialog.addWidget(class14);
        Dialog.addWidget(class1732);
        Dialog.addWidget(class165);
        Label class1733 = new Label(ActorFactory.gL(611), ResourceManager.boldFont);
        class1733.setMetrics(0, 0, Dialog.w, class1733.normalfont.getHeight() + 10);
        Button class1652 = new Button(1);
        class1652.Method158(ActorFactory.gL(558));
        class1652.setMetrics(0, 0, Dialog.w - ((Dialog.border + Dialog.padding) << 1), ResourceManager.boldFont.getHeight() + 10);
        class1652.setImage(ResourceManager.Field1308, new Dimension(ResourceManager.Field1308.getWidth(), ResourceManager.Field1308.getWidth()));
        Integer Method4932 = ActorFactory.loadInt("vibrate");
        Field485 = Method4932 == null || Method4932.intValue() != 0;
        class1652.Field1048 = Field485;
        class1652.cmdCenter = new Command(47, ActorFactory.gL(419), instance);
        Dialog.addWidget(class1733);
        Dialog.addWidget(class1652);
        int i2 = (BaseCanvas.w - i) >> 1;
        int i3 = (class173.h * 3) + (class165.h << 1) + class14.h + ((Dialog.border + Dialog.padding) << 2) + 5 + class1733.h + class1652.h;
        Dialog.setMetrics(i2, ((BaseCanvas.h - i3) - LAF.Field1293) - 10, i, i3);
        Dialog.setViewMode(1);
        Dialog.cmdRight = new Command(48, ActorFactory.gL(41), instance);
        Dialog.show(true);
    }

    private static void Method62() {
        Dialog Dialog = new Dialog();
        Field480 = new String[]{ActorFactory.gL(559), ActorFactory.gL(129)};
        Label class173 = new Label(ActorFactory.gL(459));
        class173.align = 17;
        Class14 class14 = new Class14();
        Label class1732 = new Label(new StringBuffer().append(ActorFactory.gL(247)).append(":").toString(), GameResourceManager.boldFont);
        Integer Method493 = ActorFactory.loadInt("language");
        Button class165 = Method493 != null ? new Button(Field480[Method493.intValue()]) : new Button(Field480[0]);
        class165.align = 17;
        int i = (class173.h * 3) + (class165.h << 1) + class14.h + ((Dialog.border + Dialog.padding) << 1) + 5;
        int i2 = BaseCanvas.w < 128 ? BaseCanvas.w - 4 : BaseCanvas.w - 20;
        Dialog.setMetrics((BaseCanvas.w - i2) >> 1, (BaseCanvas.h - i) >> 1, i2, i);
        Dialog.spacing = 0;
        Dialog.addWidget(class173);
        Dialog.addWidget(class14);
        Dialog.addWidget(class1732);
        Dialog.addWidget(class165);
        Dialog.setViewMode(1);
        Dialog.cmdRight = Field464;
        Dialog.show(true);
    }

    public static Dialog Method63(int i, String str, int[] iArr, String[] strArr, byte[] bArr, Command[] CommandArr) {
        Button class165;
        Dialog Dialog = new Dialog();
        Dialog.padding = LAF.LOT_PADDING;
        Dialog.cmdRight = Field464;
        Dialog.w = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        Dialog.preferredSize.width = Dialog.w - (2 * Dialog.padding);
        Class183 class183 = new Class183();
        class183.w = Dialog.w - (Dialog.padding << 1);
        class183.Field1203 = 0;
        class183.padding = 0;
        class183.Field1202 = 17;
        class183.Method367(str, ResourceManager.boldFont);
        class183.isFocusable = false;
        class183.setMetrics(0, 0, Dialog.getMaxContentWidth(), class183.preferredSize.height);
        Dialog.addWidget(class183);
        Dialog.addWidget(new Class14(), true);
        int i2 = ((Dialog.border + Dialog.padding) << 1) + class183.preferredSize.height + 4 + Dialog.spacing;
        WidgetGroup class185 = new WidgetGroup(0, 0, Dialog.getMaxContentWidth(), 60, 1);
        class185.padding = 0;
        class185.border = 0;
        class185.isScrollableY = true;
        int i3 = 0;
        for (int i4 = 0; i4 < strArr.length; i4++) {
            if (bArr[i4] == 0) {
                class165 = new Button(strArr[i4], GameResourceManager.boldFont);
            } else {
                class165 = new Button(strArr[i4]);
                int[] iArr2 = {i, iArr[i4]};
            }
            class165.cmdCenter = CommandArr[i4];
            i3 += Dialog.spacing + class165.h;
            class185.addWidget(class165);
        }
        int i5 = i2 + i3;
        int i6 = i5;
        if (i5 < 60) {
            i6 = 60;
        }
        int i7 = (BaseCanvas.h - LAF.Field1293) - (LAF.LOT_PADDING << 1);
        if (i6 > i7) {
            i6 = i7;
        }
        class185.setMetrics(0, 0, class185.w, i6 - i2);
        Dialog.setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - LAF.Field1293) - LAF.LOT_PADDING) - i6, Dialog.w, i6);
        Dialog.addWidget(class185);
        Dialog.setViewMode(1);
        int i8 = 0;
        while (true) {
            if (i8 >= class185.count()) {
                break;
            }
            Widget Method350 = class185.getWidgetAt(i8);
            if (bArr[i8] != 0) {
                Method350.requestFocus();
                break;
            }
            i8++;
        }
        return Dialog;
    }

    public static Dialog Method64(int i, int i2, int i3, String str, String[] strArr, byte[] bArr, Command Command) {
        int Method330 = (ResourceManager.defaultFont.getWidth("n") * 45) + (LAF.Field1295 << 1);
        int i4 = Method330 < BaseCanvas.w ? Method330 : BaseCanvas.w;
        Dialog Dialog = new Dialog(LAF.Field1295 + ((BaseCanvas.w - i4) >> 1), LAF.LOT_PADDING, i4 - (LAF.Field1295 << 1), BaseCanvas.h - (LAF.Field1295 << 1));
        Label class173 = new Label(str);
        class173.align = 17;
        Dialog.addWidget(class173);
        Dialog.addWidget(new Class14(), true);
        Dialog.h = (strArr.length * 20) + class173.h + 2 + (LAF.LOT_PADDING << 1);
        WidgetGroup class185 = new WidgetGroup(0, class173.x + class173.h + LAF.Field1295, Dialog.w, Dialog.h - ((LAF.LOT_PADDING + class173.h) + LAF.Field1295));
        if (Dialog.h > BaseCanvas.h) {
            Dialog.h = BaseCanvas.h;
        }
        class185.preferredSize.width = Dialog.w;
        class185.preferredSize.height = strArr.length * (20 + LAF.LOT_PADDING);
        int i5 = 0;
        for (int length = strArr.length - 1; length >= 0; length--) {
            if (i5 < ResourceManager.boldFont.getWidth(strArr[length])) {
                i5 = ResourceManager.boldFont.getWidth(strArr[length]);
            }
        }
        int i6 = i5 + LAF.LOT_PADDING;
        int length2 = strArr.length;
        for (int i7 = 0; i7 < length2; i7++) {
            Label class1732 = new Label(strArr[i7]);
            class1732.setMetrics(0, i7 * 20, i6, class1732.h + (LAF.LOT_PADDING << 1));
            EditField class168 = new EditField(i6 + LAF.LOT_PADDING, i7 * 20, (class185.w - i6) - (LAF.LOT_PADDING << 2), 20);
            if (bArr[i7] == 0) {
                class168.Method79(0);
            } else {
                class168.Method79(1);
            }
            class185.addWidget(class1732);
            class185.addWidget(class168);
            if (i7 == 0) {
                class168.requestFocus();
            }
        }
        class185.setViewMode(0);
        class185.isScrollableY = true;
        Dialog.addWidget(class185);
        Dialog.y = ((BaseCanvas.h - Dialog.h) - (LAF.Field1295 << 1)) - LAF.LOT_ITEM_HEIGHT;
        Dialog.setMetrics(Dialog.x, Dialog.y, Dialog.w, Dialog.h);
        Dialog.cmdLeft = Field464;
        if (Command == null) {
            Dialog.cmdCenter = new Command(46, ActorFactory.gL(337), new int[]{i, i2, i3}, instance);
        } else {
            Dialog.cmdCenter = Command;
        }
        Dialog.setViewMode(1);
        Dialog.show(true);
        return Dialog;
    }

    public static Dialog Method65(int i, int i2, int i3, String str, String[] strArr, byte[] bArr) {
        return Method64(122, 7, i3, str, strArr, bArr, null);
    }

    private static boolean Method66(Dialog Dialog) {
        try {
            WidgetGroup class185 = (WidgetGroup) Dialog.getWidgetAt(2);
            for (int Method351 = class185.count() - 1; Method351 >= 0; Method351--) {
                if ((class185.getWidgetAt(Method351) instanceof EditField) && ((EditField) class185.getWidgetAt(Method351)).getText().trim().length() == 0) {
                    return false;
                }
            }
            return true;
        } catch (Exception unused) {
            return false;
        }
    }

    public static String[] Method67(Dialog Dialog) {
        try {
            WidgetGroup class185 = (WidgetGroup) Dialog.getWidgetAt(2);
            String[] strArr = new String[class185.count() >> 1];
            int Method351 = class185.count();
            for (int i = 1; i < Method351; i += 2) {
                if (class185.getWidgetAt(i) instanceof EditField) {
                    strArr[i >> 1] = ((EditField) class185.getWidgetAt(i)).getText().trim();
                }
            }
            return strArr;
        } catch (Exception unused) {
            return null;
        }
    }

    public static void Method68(int i, String str, String str2, byte b) {
        Command Command = null;
        Command Command2 = null;
        Command Command3 = null;
        int[] iArr = {i, b};
        switch (b) {
            case 0:
                Command = new Command(44, ActorFactory.gL(101), iArr, instance);
                Command3 = Field464;
                break;
            case 1:
                Command = new Command(44, ActorFactory.gL(4), iArr, instance);
                Command3 = Field464;
                break;
            case 2:
                Command2 = Field463;
                Command3 = null;
                break;
        }
        Method69(str, str2, Command, Command2, Command3, true);
    }

    public static void Method69(String str, String str2, Command Command, Command Command2, Command Command3, boolean z) {
        Dialog Dialog = new Dialog();
        Dialog.padding = LAF.LOT_PADDING;
        Dialog.cmdLeft = Command;
        Dialog.cmdCenter = Command2;
        Dialog.cmdRight = Command3;
        Dialog.w = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        Dialog.preferredSize.width = Dialog.w - (2 * Dialog.padding);
        Class183 class183 = new Class183();
        class183.w = Dialog.w - (Dialog.padding << 1);
        class183.Field1203 = 0;
        class183.padding = 0;
        class183.Field1202 = 17;
        class183.Method367(str, ResourceManager.boldFont);
        class183.isFocusable = false;
        Class183 class1832 = new Class183();
        class1832.w = class183.w;
        class1832.Field1203 = 0;
        class1832.padding = 0;
        class1832.Method367(str2, GameResourceManager.smallFont);
        Dialog.h = class183.preferredSize.height + class1832.preferredSize.height + ((Dialog.padding + Dialog.border) << 1) + 4 + (Dialog.spacing << 1);
        if (Dialog.h < 60) {
            Dialog.h = 60;
        }
        int i = (BaseCanvas.h - LAF.Field1293) - (LAF.LOT_PADDING << 1);
        if (Dialog.h > i) {
            Dialog.h = i;
        }
        Dialog.setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - LAF.Field1293) - LAF.LOT_PADDING) - Dialog.h, Dialog.w, Dialog.h);
        class183.setMetrics(0, 0, Dialog.getMaxContentWidth(), class183.preferredSize.height);
        class1832.setMetrics(0, 0, Dialog.getMaxContentWidth(), (((Dialog.h - class183.h) - ((Dialog.padding + Dialog.border) << 1)) - 4) - (Dialog.spacing << 1));
        Dialog.addWidget(class183);
        Dialog.addWidget(new Class14(), true);
        Dialog.addWidget(class1832, true);
        Dialog.setViewMode(1);
        Dialog.show(true);
    }

    public static String Method70() {
        try {
            String property = System.getProperty("lac");
            String str = property;
            if (property == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.nokia.mid.lac");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("LocAreaCode");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("phone.lac");
            }
            return str == null ? "" : str;
        } catch (Exception unused) {
            return "";
        }
    }

    public static String Method71() {
        try {
            String property = System.getProperty("mcc");
            String str = property;
            if (property == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("phone.mcc");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.nokia.mid.mcc");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.nokia.mid.countrycode");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.lge.cmcc");
            }
            return str == null ? "" : str;
        } catch (Exception unused) {
            return "";
        }
    }

    public static String Method72() {
        try {
            String property = System.getProperty("mnc");
            String str = property;
            if (property == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("phone.mnc");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.nokia.mid.networkid");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.nokia.mid.mnc");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.lge.cmnc");
            }
            return str == null ? "" : str;
        } catch (Exception unused) {
            return "";
        }
    }

    public static String Method73() {
        try {
            String property = System.getProperty("Cell-ID");
            String str = property;
            if (property == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("CellID");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.nokia.mid.cellid");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("phone.cid");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.samsung.cellid");
            }
            if (str == null || str.equals("null") || str.equals("")) {
                str = System.getProperty("com.siemens.cellid");
            }
            return str == null ? "" : str;
        } catch (Exception unused) {
            return "";
        }
    }

    public static String Method74() {
        String[] classLoader = new String[]{"com.nokia.mid.mnc", "IMSI", "phone.imsi", "com.nokia.mid.mobinfo.IMSI", "com.nokia.mid.imsi", "com.sonyericsson.sim.subscribernumber", "imsi", "com.sonyericsson.imsi", "com.siemens.imei", "com.samsung.imei", "com.samsung.IMEI", "com.nokia.mid.networkid", "com.siemens.mid.networkid", "com.sonyericsson.mid.networkid", "com.motorola.mid.networkid", "com.samsung.mid.networkid"};
        String str = "";
        for (int i = 0; i < classLoader.length; i++) {
            String str2 = classLoader[i];
            String property = System.getProperty(str2);
            str = property;
            if (property != null && !str.equals("null") && !str.equals("")) {
                break;
            }
        }

        return str;
    }

    public static Class72 Method75(Widget[] class184Arr) {
        Class72 class72 = new Class72(0, 0, BaseCanvas.Field157, BaseCanvas.Field158, true);
        for (int i = 0; i < class184Arr.length; i++) {
            class184Arr[i].parent = class72;
            if (class184Arr[i].cmdCenter != null) {
                class184Arr[i].cmdCenter.Field1321 = ActorFactory.gL(419);
            }
            if (class184Arr[i] instanceof Button) {
                ((Button) class184Arr[i]).Method324(ResourceManager.defaultFont, GameResourceManager.smallFont);
                ((Button) class184Arr[i]).align = 17;
            } else if (class184Arr[i] instanceof Label) {
                ((Label) class184Arr[i]).Method324(GameResourceManager.regularFont, null);
            }
        }
        class72.children = class184Arr;
        class72.isAutoFit = true;
        class72.setViewMode(1);
        class72.cmdRight = Field464;
        return class72;
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static String Method76(GameController class74) {
        return class74.Field458;
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static String Method77(GameController class74) {
        return class74.Field459;
    }
}

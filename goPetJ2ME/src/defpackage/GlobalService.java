package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.network.MsgReader;
import vn.me.network.MobileClient;
import vn.me.network.MsgSender;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import vn.me.screen.LoginScreen;
import java.io.IOException;
import java.io.InputStream;
import java.util.Vector;
import javax.microedition.io.Connector;
import javax.microedition.io.HttpConnection;
import vn.me.ui.common.T;

/* renamed from: Class154  reason: default package */
/* loaded from: gopet_repackage.jar:Class154.class */
public final class GlobalService {
    public static MobileClient session;
    public static String Field1000;
    public static IActionListener Field1001;
    public static IActionListener Field1002;
    public static int Field1003 = 19180;
    public static String Field1004 = "127.0.0.1";
    public static String Field1005 = "127.0.0.1";
    public static int Field1006 = 19180;
    private static byte Field1007 = 0;
    public static int Field1008 = 0;
    public static String Field1009 = "http://ip.qmobi.net/gopetServer.txt";
    public static String Field1010 = "http://gopetmoi.com/help.txt";
    public static String Field1011 = "http://mgo.vn/index.php/staticpage/legal";
    public static Vector serverList = new Vector();

    public static boolean Method233() {
        return session != null && session.isConnected() && Field1005.equals(session.currentIp) && Field1006 == session.currentPort;
    }

    public static void Method234(String str) {
        Message message = new Message(9);
        message.putString(str);
        session.sendMessage(message);
    }

    public static void Method235() {
        Method236();
        BaseCanvas.instance.Field170 = new MobileClient();
        MobileClient class160 = BaseCanvas.instance.Field170;
        session = class160;
        class160.setHandler(GlobalMessageHandler.Method208());
        session.setReader(new MsgReader(session));
        session.setSender(new MsgSender(session));
        session.Method288(Field1005, Field1006);
    }

    public static void Method236() {
        if (session != null) {
            session.close();
            session.messageHandler = null;
            session = null;
        }
    }

    public static void Method237() {
        Message message = new Message(-36, true);
        message.putByte(0);
        message.putInt(Field1008);
        message.putString(GameController.Method34());
        message.putString(new StringBuffer().append(System.getProperty("microedition.platform")).append(";").append(System.getProperty("microedition.configuration")).append(";").append(System.getProperty("microedition.profiles")).append(";").append(System.getProperty("microedition.hostname")).append(";gopet").toString());
        message.putInt(BaseCanvas.w);
        message.putInt(BaseCanvas.h);
        message.putString(T.getLangCodeStr());
        message.putString("2.4.9");
        session.sendMessage(message);
    }

    public static void Method238(String str, String str2, String str3) {
        Message message = new Message(1, true);
        message.putString(str);
        message.putString(str2);
        message.putString(BaseCanvas.instance.midlet.getAppProperty("RefCode"));
        message.putString(str3);
        String Method73 = GameController.Method73();
        String Method71 = GameController.Method71();
        String Method72 = GameController.Method72();
        String Method70 = GameController.Method70();
        String Method74 = GameController.Method74();
        message.putString(Method73);
        message.putString(Method71);
        message.putString(Method72);
        message.putString(Method70);
        if (Method74 != null) {
            message.putString(Method74);
        }
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method239(String str, String str2) {
        Message message = new Message(35, true);
        message.putString(str);
        message.putString(str2);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method240(String str, String str2, IActionListener class200, IActionListener class2002) {
        new Thread(new Class155(str2, str, class200, class2002)).start();
    }

    public static void Method241(String str, String str2) {
        Message message = new Message(57);
        message.putString(str);
        message.putString(str2);
        session.sendMessage(message);
    }

    public static void Method242(int i) {
        Message message = new Message(73);
        message.putByte(1);
        session.sendMessage(message);
    }

    public static void Method243(int i) {
        Message message = new Message(72);
        message.putByte(i);
        session.sendMessage(message);
    }

    public static void Method244(String str, IActionListener class200, IActionListener class2002) {
        HttpConnection httpConnection = null;
        try {
            HttpConnection httpConnection2 = (HttpConnection) Connector.open(str);
            httpConnection = httpConnection2;
            httpConnection2.setRequestMethod("GET");
            httpConnection.setRequestProperty("Content-Type", "//text plain");
            httpConnection.setRequestProperty("Connection", "close");
            if (httpConnection.getResponseCode() == 200) {
                InputStream openInputStream = httpConnection.openInputStream();
                int length = (int) httpConnection.getLength();
                if (length != -1) {
                    byte[] bArr = new byte[length];
                    openInputStream.read(bArr);
                    String str2 = new String(bArr, "UTF-8");
                    if (class200 != null) {
                        class200.actionPerformed(str2);
                        if (httpConnection != null) {
                            try {
                                httpConnection.close();
                                return;
                            } catch (IOException unused) {
                                return;
                            }
                        }
                        return;
                    }
                }
            }
            if (httpConnection != null) {
                try {
                    httpConnection.close();
                } catch (IOException unused2) {
                }
            }
        } catch (IOException unused3) {
            if (httpConnection != null) {
                try {
                    httpConnection.close();
                } catch (IOException unused4) {
                }
            }
        } catch (Throwable th) {
            if (httpConnection != null) {
                try {
                    httpConnection.close();
                } catch (IOException unused5) {
                    unused5.printStackTrace();
                }
            }
             th.printStackTrace();
        }
        class2002.actionPerformed(null);
    }

    public static void Method245(int i, int[] iArr) {
        Message message = new Message(24);
        for (int j = 0; j < iArr.length; j++) {
            message.putInt(iArr[j]);
        }
        
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method246(int i, byte b, int i2) {
        Message message = new Message(81);
        message.putByte(44);
        message.putInt(i2);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method247(int i, int i2, int i3) {
        Message message = new Message(79);
        message.putInt(i2);
        message.putInt(i3);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method248(int i, int i2, int i3, String[] strArr) {
        Message message = new Message(i);
        message.putByte(i2);
        message.putInt(i3);
        message.putInt(strArr.length);
        for (int j = 0; j < strArr.length; j++) {
            String str = strArr[j];
            message.putString(str);
        }
         
        session.sendMessage(message);
        message.cleanup();
    }

    public static void requestImg(String str, byte b) {
        Message msg = new Message(96);
        msg.putByte(0);
        msg.putByte(b);
        msg.putString(str);
        session.sendMessage(msg);
        msg.cleanup();
    }

    public static void Method250(String str, int i) {
        Message message = new Message(21);
        message.putString(str);
        message.putByte((byte) i);
        session.sendMessage(message);
        message.cleanup();
    }

    private static void Method251(int i) {
        Message message = new Message(i);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method252(String str, String str2) {
        Message message = new Message(93);
        message.putByte(2);
        message.putByte(7);
        message.putString(str);
        message.putString(str2);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method253(int i, int i2, byte b, int[] iArr) {
        if (session == null) {
            return;
        }
        Message message = new Message(27);
        message.putInt(i2);
        message.putByte(b);
        message.putInt(i);
        message.putInt(iArr.length);
        for (int j = 0; j < iArr.length; j++) {
            int k = iArr[j];
            message.putInt(k);
        }
        
        if (session != null) {
            session.sendMessage(message);
        }
        message.cleanup();
    }

    public static void Method254(int i, int i2, int i3) {
        Message message = new Message(25);
        message.putInt(i);
        message.putInt(i2);
        message.putInt(i3);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method255(int i) {
        Message message = new Message(7);
        message.putInt(i);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method256(String str) {
        Message message = new Message(101);
        message.putString(str);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method257() {
        Method251(44);
    }

    public static void Method258(Class68 class68, String str, String str2) {
        Message message = new Message(71, true);
        message.putByte(class68.Field440);
        message.putByte(class68.Field435);
        message.putString(str);
        if (str2 != null) {
            message.putString(str2);
        }
        message.putString(BaseCanvas.instance.midlet.getAppProperty("RefCode"));
        session.sendMessage(message);
    }

    public static void Method259() {
        if (Method233()) {
            Method236();
            SceneManage class140 = GlobalMessageHandler.instance.sceneManager;
            SceneManage.Method0();
            new LoginScreen().switchToMe(0);
            Effects.clearCache();
            SceneManage.myCharacter = null;
            GlobalMessageHandler.Method216();
            Field1001 = null;
        }
    }

    public static void Method260(IActionListener class200, IActionListener class2002) {
        new Thread(new Class156(class200, null)).start();
    }

    public static void Method261(String str) {
        serverList.removeAllElements();
        if (str.indexOf(";") == -1 && str.length() > 0) {
            serverList.addElement(Method262(str));
            return;
        }
        while (str.indexOf(";") != -1) {
            serverList.addElement(Method262(str.substring(0, str.indexOf(";"))));
            String substring = str.substring(str.indexOf(";") + 1);
            str = substring;
            if (substring.indexOf(";") == -1 && str.length() > 0) {
                serverList.addElement(Method262(str));
            }
        }
    }

    private static Server Method262(String str) {
        String substring = str.substring(0, str.indexOf("|"));
        String substring2 = str.substring(str.indexOf("|") + 1);
        return new Server(substring, substring2.substring(0, substring2.indexOf(":")), Integer.parseInt(substring2.substring(substring2.indexOf(":") + 1).trim()), 0, 0);
    }

    public static void Method263(int i) {
        Message message = new Message(122);
        message.putByte(2);
        message.putInt(i);
        session.sendMessage(message);
        message.cleanup();
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static void Method264(int i, boolean z) {
        Message message = new Message(45);
        message.putByte(4);
        message.putInt(i);
        message.putBoolean(z);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method265() {
        Method251(64);
    }

    public static void selectNpcOption(int npcId, int option) {
        Message message = new Message(122);
        message.putByte(5);
        message.putInt(npcId);
        message.putInt(option);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method267(int i, int i2) {
        Message message = new Message(122);
        message.putByte(3);
        message.putInt(i);
        message.putInt(i2);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method268(int i, int i2) {
        Message message = new Message(125);
        message.putByte(1);
        message.putInt(i);
        message.putByte(i2);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method269() {
        Message message = new Message(125);
        message.putByte(2);
        session.sendMessage(message);
        message.cleanup();
    }

    public static void Method270() {
        Message message = new Message(42);
        message.putByte(12);
        session.sendMessage(message);
        message.cleanup();
    }
    
    
}

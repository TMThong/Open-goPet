package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.Resource;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.EditField;
import vn.me.ui.WidgetGroup;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

public final class ChatPlace extends WidgetGroup implements IActionListener {

    private static boolean Field745;
    private EditField Field748;
    public static Image Field750;
    private Image Field751;
    private Command Field752;
    private Command Field753;
    private Command Field754;
    private ChatData Field755;
    private boolean Field757;
    private long Field758;
    private Image Field759;
    private int Field760;
    public static boolean Field743 = false;
    public static int typeChat = -1;
    public static final int TYPE_CHAT_GLBAL = 1;
    private static Vector VectChatNormal = new Vector();
    private static Vector VectChatGlobal = new Vector();
    private static final Object lock_obj = new Object();
    private int Field749 = GameResourceManager.regularFont.getHeight();
    private int Field746 = 0;
    private Class16 Field747 = new Class16();

    public ChatPlace() {
        typeChat = -1;
        int i = BaseCanvas.h > 180 ? BaseCanvas.Field158 : BaseCanvas.h - LAF.Field1292;
        setMetrics(1, (BaseCanvas.h - i) - LAF.Field1293, BaseCanvas.w - 2, i);
        this.Field748 = new EditField(LAF.LOT_PADDING << 2, ((this.h - LAF.Field1293) - LAF.LOT_PADDING) - 1, this.w - (LAF.LOT_PADDING << 3), LAF.Field1293);
        addWidget(this.Field748, false);
        Field750 = Resource.setFileTable("/common.dat", 19);
        this.Field759 = Resource.setFileTable("/common.dat", 8);
        if (this.Field751 == null) {
            int[] iArr = new int[900];
            int[] iArr2 = new int[3 * this.Field759.getWidth()];
            int width = this.Field759.getWidth();
            int i2 = (width << 1) + (width >> 1);
            this.Field759.getRGB(iArr2, 0, this.Field759.getWidth(), 0, 0, this.Field759.getWidth(), 3);
            for (int i3 = 0; i3 < iArr.length; i3++) {
                iArr[i3] = iArr2[i2];
            }
            this.Field751 = Image.createRGBImage(iArr, 30, 30, true);
        }
        this.Field752 = new Command(1, T.gL(T.SELECT_STR), this);
        this.Field753 = new Command(2, T.gL(T.ANSWER_STR), this);
        this.Field754 = new Command(3, T.gL(T.CHAT_STR), this);
        this.cmdLeft = new Command(0, T.gL(T.CLOSE_STR), this);
        this.cmdCenter = this.Field754;
    }

    public static void setNormalChat(String str, String str2) {
        synchronized (lock_obj) {
            int size = VectChatNormal.size();
            if (size >= 50) {
                for (int i = 0; i < size - 1; i++) {
                    ChatData class117 = (ChatData) VectChatNormal.elementAt(i + 1);
                    class117.Field765 = i;
                    VectChatNormal.setElementAt(class117, i);
                }
                VectChatNormal.setElementAt(new ChatData(size - 1, str, str2), size - 1);
            } else {
                VectChatNormal.addElement(new ChatData(size, str, str2));
            }
            Field745 = true;
        }
    }

    public static void setGlobalChat(String str, String str2) {
        synchronized (lock_obj) {
            int size = VectChatGlobal.size();
            if (size >= 200) {
                for (int i = 0; i < size - 1; i++) {
                    ChatData class117 = (ChatData) VectChatGlobal.elementAt(i + 1);
                    class117.Field765 = i;
                    VectChatGlobal.setElementAt(class117, i);
                }
                VectChatGlobal.setElementAt(new ChatData(size - 1, str, str2), size - 1);
            } else {
                VectChatGlobal.addElement(new ChatData(size, str, str2));
                if (VectChatGlobal.size() == 1) {
                    VectChatGlobal.addElement(new ChatData(size, str, str2));
                }
            }
        }
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final boolean checkKeys(int i, int i2) {
        Vector vector = getVect();
        if (i == 0) {
            switch (i2) {
                case -4:
                case -3:
                    return true;
                case -2:
                    this.Field746++;
                    if (this.Field746 >= vector.size()) {
                        this.Field746 = 0;
                    }
                    Method7();
                    this.cmdCenter = this.Field752;
                    return true;
                case -1:
                    this.Field746--;
                    if (this.Field746 < 0) {
                        this.Field746 = vector.size() - 1;
                    }
                    Method7();
                    this.cmdCenter = this.Field752;
                    return true;
            }
        }
        if (!this.Field748.isFocused) {
            BaseCanvas.getCurrentScreen().requestFocus(this.Field748);
        }
        if (this.cmdCenter.cmdId == this.Field752.cmdId && i2 != -5 && i2 != -3 && i2 != -4 && i2 != -1 && i2 != -2) {
            this.cmdCenter = this.Field754;
        }
        return super.checkKeys(i, i2);
    }

    private void Method7() {
        Vector vector = getVect();
        if (this.Field746 < 0 || this.Field746 >= vector.size()) {
            return;
        }
        int i = 0;
        int i2 = ((ChatData) vector.elementAt(this.Field746)).Field764;
        for (int i3 = 0; i3 <= this.Field746; i3++) {
            i += ((ChatData) vector.elementAt(i3)).Field764;
        }
        if (i + this.Field747.Field56 <= 16) {
            this.Field747.Field56 = (-i) + i2 + LAF.LOT_PADDING;
        } else if (i + this.Field747.Field56 > (this.h - LAF.Field1293) - 10) {
            this.Field747.Field56 = (-i) + ((this.h - LAF.Field1292) - 10);
        }
    }

    private Vector getVect() {
        switch (typeChat) {
            case TYPE_CHAT_GLBAL:
                return VectChatGlobal;
        }
        return VectChatNormal;
    }

    public final void paintBackground() {
        int clipX = BaseCanvas.g.getClipX();
        int clipY = BaseCanvas.g.getClipY();
        int clipWidth = BaseCanvas.g.getClipWidth();
        int clipHeight = BaseCanvas.g.getClipHeight();
        BaseCanvas.g.clipRect(1, 1, this.w - 2, this.h - 2);
        int i = (this.w / 30) + 1;
        while (true) {
            i--;
            if (i < 0) {
                break;
            }
            int i2 = (this.h / 30) + 1;
            while (true) {
                i2--;
                if (i2 >= 0) {
                    BaseCanvas.g.drawImage(this.Field751, i * 30, i2 * 30, 0);
                } else {
                    break;
                }
            }
        }
        BaseCanvas.g.setClip(clipX, clipY, clipWidth, clipHeight);
        BaseCanvas.g.setColor(11315353);
        Ulti.Method376(0, 0, this.w, this.h, BaseCanvas.g);
        BaseCanvas.g.setColor(16777215);
        Ulti.Method376(1, 1, this.w - 2, this.h - 2, BaseCanvas.g);
        BaseCanvas.g.clipRect(LAF.LOT_PADDING << 1, LAF.LOT_PADDING, BaseCanvas.w - (LAF.LOT_PADDING << 2), this.h - (LAF.LOT_PADDING << 1));
        int i3 = this.Field747.Field57;
        Vector vector2 = VectChatNormal;
        switch (typeChat) {
            case TYPE_CHAT_GLBAL:
                vector2 = VectChatGlobal;
                break;
        }
        int size = vector2.size();
        for (int i4 = 0; i4 < size; i4++) {
            ChatData class117 = (ChatData) vector2.elementAt(i4);
            if (i3 >= this.Field749 && i3 < this.h) {
                boolean z = this.Field746 == class117.Field765;
                int i5 = LAF.LOT_PADDING;
                int i6 = i3;
                boolean z2 = z;
                BaseCanvas.g.translate(i5, i6);
                GameResourceManager.Method116().drawString(BaseCanvas.g, class117.Field762, 20, LAF.LOT_PADDING, 20);
                ResourceManager.defaultFont.drawString(BaseCanvas.g, class117.Field763[0], GameResourceManager.Method116().getWidth(class117.Field762) + LAF.LOT_PADDING + 20, LAF.LOT_PADDING, 20);
                int Method332 = ResourceManager.defaultFont.getHeight() + 1;
                for (int i7 = 1; i7 < class117.Field763.length; i7++) {
                    ResourceManager.defaultFont.drawString(BaseCanvas.g, class117.Field763[i7], 20, Method332, 20);
                    Method332 += ResourceManager.defaultFont.getHeight() + 1;
                }
                if (z2) {
                    BaseCanvas.g.drawImage(Field750, LAF.LOT_PADDING << 1, 5, 0);
                }
                BaseCanvas.g.translate(-i5, -i6);
            }
            i3 += class117.Field764;
        }
        BaseCanvas.g.setClip(clipX, clipY, clipWidth, clipHeight);
    }

    public final void paint() {
        super.paint();
        if (this.Field757) {
            GameResourceManager.largeFont.drawString(BaseCanvas.g, new StringBuffer(T.gL(T.PLEASE_WAIT_DOT_STR)).append((int) (5 - ((System.currentTimeMillis() - this.Field758) / 1000))).toString(), BaseCanvas.Field157, this.Field748.y + LAF.LOT_PADDING, 17);
        }
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final void update() {
        Vector vector = getVect();
        super.update();
        this.Field747.Method293();
        if (Field745) {
            Field745 = false;
            this.Field746 = vector.size() - 1;
            this.cmdCenter = this.Field752;
            Method7();
        }
        if (this.Field757) {
            this.Field748.setText("");
            if (System.currentTimeMillis() - this.Field758 >= 5000) {
                this.Field757 = false;
            }
        }
    }

    /* JADX WARN: Can't fix incorrect switch cases order, some code will duplicate */
    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        ChatData class117;
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 0:
                synchronized (lock_obj) {
                    Field743 = false;
                    BaseCanvas.currentScreen.removeWidget(this);
                    VectChatNormal.removeAllElements();
                    lock_obj.notifyAll();
                }
                return;
            case 1:
                if (this.Field757) {
                    return;
                }
                int size = VectChatNormal.size();
                while (true) {
                    size--;
                    if (size > 0) {
                        class117 = ((ChatData) VectChatNormal.elementAt(size)).Field765 == this.Field746 ? (ChatData) VectChatNormal.elementAt(size) : null;
                    }
                }
            /*this.Field755 = class117;
                if (this.Field755 == null || Method134(this.Field755.Field762).equals(Class74.Field453.Field50)) {
                    return;
                }
                this.Field1213 = this.Field753;
                Class116 class116 = new Class116(this, -1, new StringBuffer("Gửi: ").append(Method134(this.Field755.Field762)).toString(), null, new byte[]{0}, new Command(4, Command.Field1325.Field1321, this), this.Field1213, null);
                class116.Field1059 = true;
                class116.Field1058 = true;
                class116.show(true);*/
            case 2:
                if (this.Field757) {
                    return;
                }
                String str = GameController.myInfo.Field50;
                if (this.Field755 != null && Command.objPerfomed != null) {
                    String Method33 = ((EditField) Command.objPerfomed).getText();
                    if (Method33.length() > 0) {
                        BaseCanvas.currentScreen.hideDialog();
                        String Method134 = Method134(this.Field755.Field762);
                        if (!Method134.equals(str)) {
                            MEService.chat(new StringBuffer().append(GameController.myInfo.Field50).append(">").append(Method134).toString(), Method33);
                        }
                        this.Field757 = true;
                        this.Field758 = System.currentTimeMillis();
                    }
                }
                this.Field748.setText("");
                this.cmdCenter = this.Field752;
                return;
            case 3:
                break;
            case 4:
                BaseCanvas.currentScreen.hideDialog();
                this.cmdCenter = this.Field754;
                break;
            case 5:
                if (!this.Field757 && this.Field748.getText().length() > 0) {
                    int i = this.Field760;
                    String Method332 = this.Field748.getText();
                    Message message = new Message(81);
                    message.putByte(91);
                    message.putByte(21);
                    message.putInt(i);
                    message.putString(Method332);
                    GlobalService.session.sendMessage(message);
                    message.cleanup();
                    this.Field748.setText("");
                    this.Field757 = true;
                    this.Field758 = System.currentTimeMillis();
                    return;
                }
                return;
            default:
                return;
        }
        switch (typeChat) {
            case TYPE_CHAT_GLBAL: {
                if (!this.Field757 && this.Field748.getText().length() > 0) {
                    MEService.chatGlobal(this.Field748.getText());
                    this.Field748.setText("");
                    this.Field757 = true;
                    this.Field758 = System.currentTimeMillis();
                }
            }
            break;
            default: {
                if (!this.Field757 && this.Field748.getText().length() > 0) {
                    MEService.chat(GameController.myInfo.Field50, this.Field748.getText());
                    this.Field748.setText("");
                    this.Field757 = true;
                    this.Field758 = System.currentTimeMillis();
                }
                break;
            }
        }
    }

    private static String Method134(String str) {
        int indexOf = str.indexOf(">");
        if (indexOf > 0) {
            str = str.substring(0, indexOf);
        }
        return str;
    }

    public final void addWidget() {
        BaseCanvas.currentScreen.addWidget(this);
        BaseCanvas.getCurrentScreen().requestFocus(this.Field748);
        Field743 = true;
    }

    public final void Method79(int i) {
        this.Field754 = new Command(5, T.gL(T.CHAT_STR), this);
        this.cmdCenter = this.Field754;
        this.Field760 = i;
    }

    public static Image Method136(ChatPlace class115) {
        return class115.Field759;
    }
}

package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.WidgetGroup;
import vn.me.screen.Screen;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

public final class Class89 extends Screen {
    private int Field551;
    private int[] Field552;
    private int[] Field553;
    private String[] Field554;
    private String[] Field555;
    private String[] Field556;
    private String[] Field557;
    private WidgetGroup Field558;
    private int Field559;
    private int Field560;
    private Image Field561;

    public Class89(int i) {
        super(true);
        this.Field551 = 0;
        this.Field552 = new int[3];
        this.Field553 = new int[3];
        this.Field554 = new String[3];
        this.Field555 = new String[3];
        this.Field556 = new String[3];
        this.Field558 = new WidgetGroup();
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.cmdLeft = GameController.Field467;
        this.Field559 = i;
        this.container.addWidget(this.Field558);
        this.Field558.setMetrics(74, 42, (BaseCanvas.w - 64) - 20, 100);
        this.Field558.setPreferredSize(100, 100);
        this.Field558.isScrollableY = true;
        this.Field558.setViewMode(1);
    }

    public final void Method78(int[] iArr, int[] iArr2, String[] strArr, String[] strArr2, String[] strArr3) {
        this.Field552 = iArr;
        this.Field553 = iArr2;
        this.Field554 = strArr;
        this.Field555 = strArr2;
        this.Field556 = strArr3;
        this.Field558.removeAll();
        for (int i = 0; i < 3; i++) {
            Class91 class91 = new Class91(this, i);
            class91.setSize((BaseCanvas.w - 85) - 17, 20);
            class91.setPosition(10, 5 + (i << 5));
            this.Field558.addWidget(class91);
            if (this.Field559 == 1) {
                String str = "";
                int i2 = 0;
                switch (iArr[i]) {
                    case -1:
                        i2 = 11;
                        str = T.gL(T.UNLOCK);
                        break;
                    case 0:
                        i2 = 12;
                        str = T.gL(T.HIRE);
                        break;
                    case 1:
                        i2 = 13;
                        str = T.gL(T.CHANGE);
                        break;
                }
                class91.cmdCenter = new Command(i2, str, this);
            }
        }
        requestFocus(this.Field558.getWidgetAt(0));
    }

    public final void Method79(int i) {
        this.Field551 = i;
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method447();
        GameResourceManager.Method117().drawString(BaseCanvas.g, PetGameModel.Field277, 17, 22, 0);
        BaseCanvas.g.drawRegion(GameResourceManager.Field599, PetRenderer.Field302 * 14, 0, 14, 14, 0, 2, 20, 0);
        GameResourceManager.Method117().drawString(BaseCanvas.g, new StringBuffer("(chien)").append(String.valueOf(this.Field551)).toString(), BaseCanvas.Field159, 22, 0);
        LAF.Method413(10, 42, 64, 100);
        LAF.Method413(74, 42, (BaseCanvas.w - 20) - 64, 100);
        mCharacter class126 = SceneManage.myCharacter;
        if (class126.Field798) {
            Class128.drawChar(null, (byte) (class126.Field813 == 0 ? 1 : 0), 45, 132, 1, false, 0, 0);
        } else {
            Image Method459 = PetGameModel.Field284.Method459(class126.Field797);
            if (Method459 == null) {
                Class128.drawChar(null, (byte) (class126.Field813 == 0 ? 1 : 0), 45, 132, 1, false, 0, 0);
            } else {
                BaseCanvas.g.drawImage(Class128.Field835, 20, 20, 17);
                byte b = class126.Field813;
                Class128.drawChar(Method459, 45, 132, 1, false);
            }
        }
        LAF.Method413(10, 142, BaseCanvas.w - 20, ((BaseCanvas.h - 142) - 5) - LAF.Field1293);
        int i = 150;
        if (this.Field557 != null) {
            for (int i2 = 0; i2 < this.Field557.length; i2++) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field557[i2], 16, i, 0);
                i += ResourceManager.boldFont.getHeight();
            }
        }
    }

    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 11:
                GameController.waitDialog();
                Message message = new Message(81);
                message.putByte(91);
                message.putByte(25);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 12:
                GameController.waitDialog();
                MEService.Method445(this.Field553[this.Field560]);
                return;
            case 13:
                GameController.Method45(T.gL(T.CHANGE_OTHER_SKILLS), new Command(14, T.gL(T.YES), this), GameController.Field464);
                return;
            case 14:
                GameController.waitDialog();
                MEService.Method445(this.Field553[this.Field560]);
                return;
            default:
                return;
        }
    }

    public static int[] Method80(Class89 class89) {
        return class89.Field552;
    }

    public static Image Method81(Class89 class89) {
        return class89.Field561;
    }

    public static Image Method82(Class89 class89, Image image) {
        class89.Field561 = image;
        return image;
    }

    public static String[] Method83(Class89 class89) {
        return class89.Field554;
    }

    public static String[] Method84(Class89 class89) {
        return class89.Field556;
    }

    public static int Method85(Class89 class89, int i) {
        class89.Field560 = i;
        return i;
    }

    public static String[] Method86(Class89 class89, String[] strArr) {
        class89.Field557 = strArr;
        return strArr;
    }

    
    public static String[] Method87(Class89 class89) {
        return class89.Field555;
    }
}

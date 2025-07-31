package vn.me.screen;

import defpackage.ActorFactory;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Resource;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.common.FrameImage;
import vn.me.ui.Font;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import vn.me.network.Message;
import vn.me.screen.LoginScreen;
import vn.me.screen.Screen;
import javax.microedition.lcdui.Image;
import thong.auto.AutoManager;
import thong.sdk.ISoundManagerSDK;

public final class SplashScreen extends Screen implements Runnable {

    private int type;
    private int Field83;
    private Image Field84;
    private byte[][] Field85;
    private byte[] Field86;
    private byte[] Field87;
    private byte Field88;
    private boolean Field89;
    private Image Field90;

    public SplashScreen(int i) {
        super(true);
        this.Field83 = 40;
        this.type = i;
        if (i == 0) {
            Resource.Method402(ResourceManager.Field1306);
            ResourceManager.initFont();
            Method79(i);
        } else if (i == 1) {
            Method79(i);
            this.Field85 = new byte[][]{new byte[]{0, 16}, new byte[]{17, 12}, new byte[]{29, 14}, new byte[]{43, 15}, new byte[]{64, 10}, new byte[]{75, 12}, new byte[]{88, 5}, new byte[]{97, 6}, new byte[]{105, 6}, new byte[]{113, 6}};
            this.Field86 = new byte[10];
            this.Field87 = new byte[]{0, -2, 0, 3};
        }
    }

    public final void switchToMe(int i, boolean z) {
        super.switchToMe(i, z);
        this.Field89 = false;
        this.run();
    }

    public final void update() {
        switch (this.type) {
            case 0:
                if (this.Field83 > 0 || !this.Field89) {
                    this.Field83--;
                    return;
                } else {
                    new LoginScreen().switchToMe(0, false);
                    return;
                }
            case 1:
                for (int i = 0; i < 10; i++) {
                    if (this.Field86[i] > 0) {
                        byte[] bArr = this.Field86;
                        int i2 = i;
                        bArr[i2] = (byte) (bArr[i2] - 1);
                    }
                }
                if (this.Field88 < 10) {
                    this.Field86[this.Field88] = 3;
                }
                this.Field88 = (byte) (this.Field88 + 1);
                if (this.Field88 > 15) {
                    this.Field88 = (byte) 0;
                    return;
                }
                return;
            default:
                return;
        }
    }

    public final void paint() {
        switch (this.type) {
            case 0:
                BaseCanvas.g.setColor(0);
                BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
                BaseCanvas.g.drawImage(this.Field90, BaseCanvas.Field157, BaseCanvas.Field158, 3);
                BaseCanvas.g.setColor(16736256);
                int i = BaseCanvas.ticks % 40;
                int height = BaseCanvas.Field158 + (this.Field90.getHeight() / 2) + 5;
                if (i > 30) {
                    BaseCanvas.g.fillRect(BaseCanvas.Field157 + 8, height, 4, 4);
                }
                if (i > 20) {
                    BaseCanvas.g.fillRect(BaseCanvas.Field157 - 2, height, 4, 4);
                }
                if (i > 10) {
                    BaseCanvas.g.fillRect(BaseCanvas.Field157 - 12, height, 4, 4);
                    return;
                }
                return;
            case 1:
                BaseCanvas.g.setColor(0);
                BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
                int height2 = this.Field84.getHeight();
                for (int i2 = 0; i2 < 10; i2++) {
                    BaseCanvas.g.drawRegion(this.Field84, this.Field85[i2][0], 0, this.Field85[i2][1], height2, 0, ((BaseCanvas.w - this.Field84.getWidth()) - 10) + this.Field85[i2][0], (BaseCanvas.h - this.Field87[this.Field86[i2]]) - 5, 36);
                }
                return;
            default:
                return;
        }
    }

    public static void Method6() {
        Message.isiWin = false;
        LAF.Method408((byte) 1);
        if (GlobalService.session != null) {
            GlobalService.session.isSYNC = false;
        }
        if (ResourceManager.defaultFont != null) {
            ResourceManager.defaultFont = new Font(ResourceManager.defaultFont, -16777216);
        }
    }

    private void Method79(int i) {
        try {
            if (i == 0) {
                this.Field90 = Image.createImage("/meLogo.png");
            } else if (i == 1) {
                Resource.Method402("/common.dat");
                this.Field84 = Resource.createImage(15);
            }
        } catch (Exception unused) {
            unused.printStackTrace();
        }
    }

    public final void run() {
        switch (this.type) {
            case 0:
                GameController.instance = new GameController();
                Method6();
                ResourceManager.init();
                Resource.createImage(5);
                Resource.createImage(6);
                ResourceManager.Field1308 = Resource.createImage(0);
                ResourceManager.Field1307 = new FrameImage(Resource.createImage(7), 6);
                GameResourceManager.Method113();
                GameResourceManager.loadResources();
                AutoManager.init();
                ISoundManagerSDK.loadMusicState();
                ISoundManagerSDK.saveMusicState();
                ISoundManagerSDK.playBgSound("s_login");
                try {
                    int lang = ActorFactory.loadInt(ActorFactory.languageRMS).intValue();
                    if (lang >= 0) {
                        ActorFactory.langueCode = lang;
                    }
                } catch (Exception e) {
                }
                break;
        }
        this.Field89 = true;
    }
}

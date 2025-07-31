package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import vn.me.ui.Dialog;
import vn.me.screen.SplashScreen;
import vn.me.screen.LoginScreen;
import vn.me.screen.Screen;
import javax.microedition.io.ConnectionNotFoundException;

/* renamed from: Class144  reason: default package */
 /* loaded from: gopet_repackage.jar:Class144.class */
public final class Class144 implements IActionListener {

    public final void Method0() {
        Dialog.Method276(ActorFactory.gL(82), new Command(1, ActorFactory.gL(580), this), null, GameController.Field464, true);
    }

    public static void Method18() {
        SplashScreen.Method6();
        if (GlobalMessageHandler.instance.sceneManager != null) {
            SceneManage class140 = GlobalMessageHandler.instance.sceneManager;
            SceneManage.Method0();
        }
        new LoginScreen().switchToMe(0);
        try {
            Thread.sleep(100L);
        } catch (InterruptedException unused) {
            unused.printStackTrace();
        }
        Effects.clearCache();
        SceneManage.myCharacter = null;
        GlobalMessageHandler.Method216();
        GlobalService.Field1001 = null;
    }

    public final void Method133(String str, String str2) {
        GameController.Field461 = str2;
        Dialog.Method275(str, new Command(48, ActorFactory.gL(116), this), null, GameController.Field464);
        Screen.currentDialog.Field1058 = true;
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 1:
                GameController.show(true);
                new Thread(new Runnable() {
                    ///////@Override
                    public void run() {
                        GlobalService.Method260(new IActionListener() {
                            ///////@Override
                            public void actionPerformed(Object obj) {
                                Screen getCurrentScreen = BaseCanvas.getCurrentScreen();
                                if (getCurrentScreen instanceof LoginScreen) {
                                    ((LoginScreen) getCurrentScreen).showSelectServerModal();
                                }
                            }
                        }, null);
                    }
                }).start();
                return;
            case 2:
                try {
                    BaseCanvas.instance.midlet.platformRequest((String) Command.objPerfomed);
                    return;
                } catch (ConnectionNotFoundException unused) {
                    return;
                }
            case 48:
                try {
                    BaseCanvas.getCurrentScreen().hideDialog();
                    BaseCanvas.instance.midlet.platformRequest(GameController.Field461);
                    return;
                } catch (ConnectionNotFoundException unused2) {
                    return;
                }
            default:
                return;
        }
    }
}

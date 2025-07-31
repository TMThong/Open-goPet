package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.Button;
import vn.me.core.BaseCanvas;
import vn.me.ui.Label;
import vn.me.ui.Widget;
import vn.me.screen.SplashScreen;
import vn.me.screen.GameScene;
import vn.me.screen.MessageScreen;
import java.util.Hashtable;
import javax.microedition.lcdui.Image;


public final class SceneManage implements IActionListener {
    public static mCharacter myCharacter;
    public static GameScene currentScene;
    public Image[] Field970;
    public Image Field971;
    private final Hashtable Field972 = new Hashtable();

    public final Class129 Method219(int i) {
        return (Class129) this.Field972.get(new Integer(i));
    }

    public static mCharacter Method220(int i) {
        mCharacter class126 = null;
        if (currentScene != null) {
            class126 = currentScene.listChar.getChar(i);
        }
        return class126;
    }

    public final void Method79(int i) {
        mCharacter Method220;
        if (currentScene == null || (Method220 = Method220(i)) == null || !Method220.Field792) {
            return;
        }
        currentScene.Method192(Method220);
        Method220.Method166(currentScene);
    }

    public final void Method221(int i, String str) {
        mCharacter Method220;
        if (currentScene == null || str == null || (Method220 = Method220(i)) == null || !Method220.Field792) {
            return;
        }
        Method220.Method41(str);
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 101:
                new MessageScreen().switchToMe(1, true);
                return;
            case 900:
                mCharacter class126 = currentScene.Field884;
                int i = class126.Id;
                String str = class126.name;
                Ulti.Method372(class126.Field808, 0);
                currentScene.Method49();
                return;
            case 1103:
                GameController.waitDialog();
                GlobalService.Method255(currentScene.Field890);
                return;
            case 1204:
                Method224(currentScene.petCenterCmd, new Widget[]{new Label(ActorFactory.gL(574)), new Button(new Command(1202, ActorFactory.gL(134), this)), new Button(new Command(1203, ActorFactory.gL(471), this))});
                return;
            case 2020:
                new MessageScreen().switchToMe(1, true);
                return;
            default:
                return;
        }
    }

    public static void warp(int i, int i2, int i3) {
        GameController.waitDialog();
        GlobalService.Method254(i, i2, i3);
    }

    public static void Method0() {
        if (currentScene != null && currentScene.Field907 != null) {
            currentScene.Field907.actionPerformed(null);
        }
        new SplashScreen(1).switchToMe(0);
        try {
            Thread.sleep(50L);
        } catch (InterruptedException unused) {
        }
        Method18();
    }

    public final GameScene Method223(GameScene class134, mMap class137, int i, int i2, int i3) {
        class134.Field890 = i;
        class134.Method196(class137);
        for (int i4 = 0; i4 < 4; i4++) {
            Class130 class130 = new Class130();
            class130.xChar = Ulti.Method370(class134.Field879.mapWidthPixel);
            class130.yChar = Ulti.Method370(class134.Field879.mapHeightPixel);
            class130.Field854 = class130.xChar;
            class130.Field855 = class130.yChar;
            class130.Field850 = class134.Field879.mapWidthPixel;
            class130.Field851 = class134.Field879.mapHeightPixel;
            class134.addObject(class130);
            class134.Method200(class130);
        }
        class134.addActor(myCharacter, i2, i3, true);
        class134.Method104(myCharacter.Method126() ? 0 : 1);
        class134.Field888.Field57 = class134.Field888.Field56;
        class134.Field889.Field57 = class134.Field889.Field56;
        class134.Method191();
        return class134;
    }

    public static void Method224(mObject class133, Widget[] class184Arr) {
        Class72 Method75 = GameController.Method75(class184Arr);
        if (class133 instanceof mCharacter) {
            Method75.Method29(class133.xChar - currentScene.Field888.Field57, (class133.yChar - 54) - currentScene.Field889.Field57);
        } else {
            Method75.Method29(class133.xChar - currentScene.Field888.Field57, class133.yChar - currentScene.Field889.Field57);
        }
        Method75.findNextFocus(true, -1, 1);
        BaseCanvas.currentScreen.showDialog(Method75, false);
    }

    public static void Method18() {
        if (PetGameModel.Field284 != null) {
            PetGameModel.Field284.Field307.clear();
        }
        GameResourceManager.releaseImgCache();
        GameResourceManager.Method119();
        currentScene = null;
        System.gc();
    }
}

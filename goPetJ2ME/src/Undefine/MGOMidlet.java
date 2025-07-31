package Undefine;

import vn.me.core.BaseCanvas;
import defpackage.GameController;
import javax.microedition.lcdui.Display;
import javax.microedition.midlet.MIDlet;

/* renamed from: Class205  reason: default package */
 /* loaded from: gopet_repackage.jar:Class205.class */
public class MGOMidlet extends MIDlet {

    protected void destroyApp(boolean z) {
        GameController.destroyApp();
    }

    protected void pauseApp() {
        GameController.pauseApp();
    }

    protected void startApp() {
        if (Display.getDisplay(this).getCurrent() != null) {
            GameController.Method38();
            return;
        }
        if (BaseCanvas.instance == null) {
            GameController.startApp(this);
        }
        GameController.Method36();
    }

    public void RunApp() {
        this.startApp();
    }
}

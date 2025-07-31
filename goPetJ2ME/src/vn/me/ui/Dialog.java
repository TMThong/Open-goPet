package vn.me.ui;

import vn.me.core.BaseCanvas;
import defpackage.Command;
import vn.me.ui.common.LAF;
import vn.me.ui.common.T;
import vn.me.ui.Widget;
import vn.me.ui.WidgetGroup;
import vn.me.screen.Screen;

/* renamed from: Dialog  reason: default package */
/* loaded from: gopet_repackage.jar:Dialog.class */
public class Dialog extends WidgetGroup {
    public boolean Field1055;
    public boolean Field1056;
    public Widget Field1057;
    public boolean Field1058;
    public boolean Field1059;

    public Dialog() {
        this(0, 0, 0, 0);
    }

    public Dialog(int i, int i2, int i3, int i4) {
        super(i, i2, i3, i4);
        this.Field1055 = true;
        this.Field1058 = false;
        this.Field1059 = false;
        this.isLoop = true;
        this.border = 3;
    }

    public final void Method204(Dialog Dialog) {
        BaseCanvas.getCurrentScreen().showDialog(this);
    }

    public final void show(boolean z) {
        if (z && Screen.currentDialog != null && !Screen.currentDialog.Field1059) {
            BaseCanvas.currentScreen.hideDialog();
        }
        BaseCanvas.currentScreen.showDialog(this);
    }

    public final void Method274() {
        Screen Screen = BaseCanvas.currentScreen;
        Screen.hideDialog(this);
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public void paintBorder() {
        super.paintBorder();
        LAF.Method412(0, 0, this.w, this.h);
    }

    ///////@Override // defpackage.Class184
    public void paintBackground() {
        LAF.Method414(this);
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public void update() {
        super.update();
        if (!this.Field1055 || this.x < BaseCanvas.w) {
            return;
        }
        this.Field1056 = true;
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final boolean checkKeys(int i, int i2) {
        super.checkKeys(i, i2);
        return true;
    }

    public static void Method275(String str, Command Command, Command Command2, Command Command3) {
        Method276(str, Command, null, Command3, false);
    }

    public static void Method276(String str, Command Command, Command Command2, Command Command3, boolean z) {
        new MessageDialog(str, Command, Command2, Command3, 0).show(z);
    }

    public static void Method277(String str, Command Command, Command Command2, Command Command3, boolean z) {
        new MessageDialog(str, null, Command2, null, 1).show(z);
    }

    public static void Method158(String str) {
        Method40(str, false);
    }

    public static void Method40(String str, boolean z) {
        Method276(str, null, Command.Field1324, null, z);
    }

    public static void Method7() {
        new MessageDialog(T.gL(22), null, Command.Field1325, null, 2).show(true);
    }

    public static void Method45(String str, Command Command, Command Command2) {
        Method276(str, null, Command, Command2, true);
    }

    public static void Method41(String str) {
        Method277(str, null, Command.Field1324, null, true);
    }

    public static void Method25() {
        if (Screen.currentDialog != null && (Screen.currentDialog instanceof MessageDialog) && ((MessageDialog) Screen.currentDialog).Field1159 == 2) {
            Screen.currentDialog.Method274();
        }
    }

    public static InputDialog Method57(String str, Command Command, Command Command2, int i) {
        InputDialog class172 = new InputDialog(str, Command, Command2 == null ? Command.Field1325 : Command2, 0);
        class172.show(true);
        return class172;
    }

    public final void Method26() {
        if (this.Field1055) {
            this.destX = BaseCanvas.w;
        } else {
            this.Field1056 = true;
        }
    }
}

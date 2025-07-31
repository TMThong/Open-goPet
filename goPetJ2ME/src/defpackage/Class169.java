package defpackage;

import vn.me.ui.common.T;
import vn.me.core.BaseCanvas;
import vn.me.ui.EditField;
import javax.microedition.lcdui.Command;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Displayable;
import javax.microedition.lcdui.TextBox;

/* renamed from: Class169  reason: default package */
/* loaded from: gopet_repackage.jar:Class169.class */
public final class Class169 implements CommandListener {
    private final TextBox Field1100;
    private final EditField Field1101;

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class169(EditField class168, TextBox textBox) {
        this.Field1101 = class168;
        this.Field1100 = textBox;
    }

    public final void commandAction(Command command, Displayable displayable) {
        if (command.getLabel().equals(T.gL(6))) {
            this.Field1101.setText(this.Field1100.getString());
        }
        BaseCanvas.instance.resetScreen();
    }
}

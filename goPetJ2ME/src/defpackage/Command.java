package defpackage;

import vn.me.ui.common.T;
import vn.me.ui.interfaces.IActionListener;
import vn.me.screen.Screen;

/* renamed from: Command  reason: default package */
 /* loaded from: gopet_repackage.jar:Command.class */
public final class Command {

    public int cmdId;
    public Object objPerfomed;
    public String Field1321;
    public IActionListener Field1322;
    private static IActionListener Field1323 = new IActionListener() {
        ///////@Override
        public void actionPerformed(Object obj) {
            switch (((Command) ((Object[]) obj)[0]).cmdId) {
                case -2:
                case -1:
                    Screen.currentDialog.Method274();
                    return;
                default:
                    return;
            }
        }
    };
    public static final Command Field1324 = new Command(-1, T.gL(6), Field1323);
    public static final Command Field1325 = new Command(-2, T.gL(0), Field1323);

    public Command(String str, IActionListener class200) {
        this(-1, str, class200);
    }

    public Command(int i, String str, IActionListener class200) {
        this(-1, str, null, class200);
        this.cmdId = i;
    }

    public Command(int i, String str, Object obj, IActionListener class200) {
        this.objPerfomed = obj;
        this.cmdId = i;
        this.Field1321 = str;
        this.Field1322 = class200;
    }

    public final void actionPerformed(Object obj) {
        if (this.Field1322 != null) {
            this.Field1322.actionPerformed(obj);
        }
    }
}

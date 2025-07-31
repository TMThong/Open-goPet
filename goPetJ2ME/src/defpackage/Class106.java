package defpackage;

import vn.me.network.Cmd;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.InputDialog;
import vn.me.network.Message;
import vn.me.ui.Dialog;
import vn.me.ui.common.T;

public final class Class106 implements IActionListener {

    private InputDialog Field696;
    private InputDialog Field697;

    public final void Method0() {
        this.Field696 = Dialog.Method57(T.gL(T.RECIPIENT_STR), new Command(1, T.gL(T.CONTINUE_STR), this), GameController.Field464, 0);
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 1:
                this.Field696.Method274();
                this.Field697 = Dialog.Method57(T.gL(T.CONTENT_STR), new Command(2, T.gL(T.SEND_STR), this), GameController.Field464, 0);
                return;
            case 2:
                String Method327 = this.Field696.Method327(0);
                String Method3272 = this.Field697.Method327(0);
                this.Field697.Method274();
                GameController.waitDialog();
                Message message = new Message(Cmd.LETTER_COMMAND);
                message.putByte(Cmd.LETTER_COMMAND_SEND_LETTER);
                message.putString(Method327);
                message.putString(Method3272);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            default:
                return;
        }
    }
}

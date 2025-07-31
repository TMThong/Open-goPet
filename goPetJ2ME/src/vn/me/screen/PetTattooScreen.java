package vn.me.screen;

import vn.me.core.BaseCanvas;
import defpackage.Class105;
import defpackage.TattooUpgradeDialog;
import vn.me.ui.common.ResourceManager;
import defpackage.PetGameModel;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import vn.me.network.Message;
import defpackage.Tattoo;
import vn.me.ui.Widget;
import vn.me.screen.Screen;
import vn.me.ui.common.T;

/* renamed from: Class121  reason: default package */
/* loaded from: gopet_repackage.jar:Class121.class */
public final class PetTattooScreen extends Screen {
    private final Class105 Field771;
    private Tattoo[] Field772;
    private String[] Field773;
    private Tattoo Field774;
    private final Command Field775;
    private final Command Field776;
    private final Command Field777;
    private int Field778;
    private TattooUpgradeDialog Field779;

    public PetTattooScreen(PetGameModel class43) {
        super(true);
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.Field771 = new Class105(15, 110, BaseCanvas.w - 30, ((BaseCanvas.h - 110) - 10) - LAF.Field1293, 40);
        this.container.addWidget(this.Field771);
        this.cmdLeft = GameController.Field467;
        this.Field775 = new Command(1, T.gL(T.UPGRADE_STR), this);
        this.Field776 = new Command(2, T.gL(T.REMOVE), this);
        this.Field777 = new Command(3, T.gL(T.TATTOO), this);
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        LAF.Method413(10, 20, BaseCanvas.w - 20, 80);
        int i = 20 + this.Field778;
        int i2 = BaseCanvas.w >> 1;
        if (this.Field773 != null) {
            for (int i3 = 0; i3 < this.Field773.length; i3++) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field773[i3], i2, i, 17);
                i += ResourceManager.boldFont.getHeight() + 2;
            }
        }
        LAF.Method413(10, 100, BaseCanvas.w - 20, ((BaseCanvas.h - 100) - 5) - LAF.Field1293);
    }

    public final void Method170(Tattoo[] class120Arr) {
        this.Field772 = class120Arr;
        for (int i = 0; i < this.Field772.length; i++) {
            this.Field772[i].onFocusAction = this;
        }
        this.Field771.Method125(this.Field772);
        this.Field771.Method79(0);
    }

    /* JADX WARN: Can't fix incorrect switch cases order, some code will duplicate */
    ///////@Override // defpackage.Screen, defpackage.Class200
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        if (Command == null) {
            Widget class184 = (Widget) ((Object[]) obj)[1];
            this.Field773 = null;
            for (int i = 0; i < this.Field772.length; i++) {
                if (class184 == this.Field772[i]) {
                    Tattoo class120 = (Tattoo) class184;
                    if (class120.name != null) {
                        this.Field773 = ResourceManager.boldFont.wrap(class120.name, BaseCanvas.w - 30);
                    }
                    this.Field774 = class120;
                    this.Field778 = (80 - ((this.Field773.length * ResourceManager.boldFont.getHeight()) + ((this.Field773.length - 1) << 1))) >> 1;
                }
            }
            if (this.Field774 != null) {
                switch (this.Field774.id) {
                    case -1:
                        this.cmdCenter = null;
                        this.cmdRight = null;
                        return;
                    case 0:
                        this.cmdCenter = this.Field777;
                        this.cmdRight = null;
                        return;
                    default:
                        this.cmdCenter = this.Field775;
                        this.cmdRight = this.Field776;
                        return;
                }
            }
            return;
        }
        switch (Command.cmdId) {
            case 1:
                this.Field779 = new TattooUpgradeDialog();
                this.Field779.setTattooDetails(0, this.Field774.id, this.Field774.imgPathId, this.Field774.name, this.Field774.Field684);
                this.Field779.show(true);
                return;
            case 2:
                GameController.Method45(T.gL(T.DO_YOU_WANT_REMOVE_THIS_TATTOO), new Command(4, T.gL(T.REMOVE), this), GameController.Field464);
                return;
            case 3:
                GameController.waitDialog();
                Message message = new Message(81);
                message.putByte(90);
                message.putByte(2);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 4:
                int i2 = this.Field774.id;
                Message message2 = new Message(81);
                message2.putByte(90);
                message2.putByte(3);
                message2.putInt(i2);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                BaseCanvas.getCurrentScreen();
                Screen.hideDialog(Screen.currentDialog);
                GameController.waitDialog();
                break;
        }
        super.actionPerformed(obj);
    }

    public final void Method171(int i, String str, String str2) {
        if (this.Field779 != null) {
            this.Field779.setTattooDetails(this.Field779.selectedTattooIndex, i, str, str2, (byte) 0);
            this.Field779.show(true);
        }
    }
}

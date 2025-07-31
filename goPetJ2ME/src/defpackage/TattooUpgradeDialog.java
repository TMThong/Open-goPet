package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.common.T;

/* renamed from: Class124  reason: default package */
 /* loaded from: gopet_repackage.jar:Class124.class */
public final class TattooUpgradeDialog extends Dialog implements IActionListener {

    private Tattoo[] tattoos;
    public int selectedTattooIndex;

    public TattooUpgradeDialog() {
        int i = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        int i2 = 120 + (LAF.LOT_PADDING << 2) + 6;
        setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - LAF.Field1293) - i2) - this.padding, i, i2);
        this.tattoos = new Tattoo[3];
        for (int i3 = 0; i3 < this.tattoos.length; i3++) {
            this.tattoos[i3] = new Tattoo();
            addWidget(this.tattoos[i3], false);
            this.tattoos[i3].setPosition(20, LAF.LOT_PADDING + ((40 + LAF.LOT_PADDING) * i3));
            if (i3 != 0) {
                this.tattoos[i3].cmdCenter = new Command(0, T.gL(T.CHANGE_STR), new Integer(i3), this);
            }
        }
        this.cmdRight = GameController.Field464;
        this.cmdLeft = new Command(1, T.gL(T.UPGRADE_STR), this);
    }

    public final void setTattooDetails(int i, int i2, String str, String str2, byte b) {
        this.tattoos[i].id = i2;
        this.tattoos[i].imgPathId = str;
        this.tattoos[i].name = str2;
        this.tattoos[i].Method22(b);
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        super.paintBackground();
        for (int i = 0; i < this.tattoos.length; i++) {
            if (this.tattoos[i].name != null) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.tattoos[i].name, 70, LAF.LOT_PADDING + ((40 + LAF.LOT_PADDING) * i) + ((40 - ResourceManager.boldFont.getHeight()) / 2), 0);
            }
        }
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 0:
                GameController.waitDialog();
                Integer num = (Integer) Command.objPerfomed;
                this.selectedTattooIndex = num.intValue();
                byte byteValue = num.byteValue();
                Message message = new Message(81);
                message.putByte(90);
                message.putByte(4);
                message.putByte(byteValue);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 1:
                boolean z = true;
                for (int i = 0; i < this.tattoos.length; i++) {
                    if (this.tattoos[i].id == 0) {
                        z = false;
                    }
                }
                if (!z) {
                    GameController.startOKDlg(T.gL(T.NOT_ENGOUH_MATTERIAL_STR));
                    return;
                }
                GameController.show(true);
                int i2 = this.tattoos[0].id;
                int i3 = this.tattoos[1].id;
                int i4 = this.tattoos[2].id;
                Message message2 = new Message(81);
                message2.putByte(90);
                message2.putByte(5);
                message2.putInt(i2);
                message2.putInt(i3);
                message2.putInt(i4);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                return;
            default:
                return;
        }
    }
}

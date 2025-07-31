package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.common.T;

/* renamed from: Class122  reason: default package */
/* loaded from: gopet_repackage.jar:Class122.class */
public final class Class122 extends Dialog implements IActionListener {
    private PetItem[] Field780;
    public int Field781;
    public int[] Field782;

    public Class122(int i) {
        this.Field781 = i;
        int i2 = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        int i3 = 90 + (LAF.LOT_PADDING << 2) + 6;
        setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - LAF.Field1293) - i3) - this.padding, i2, i3);
        int i4 = 0;
        switch (i) {
            case 0:
            case 2:
                i4 = 3;
                break;
            case 1:
            case 3:
                i4 = 2;
                break;
        }
        this.Field780 = new PetItem[i4];
        for (int i5 = 0; i5 < this.Field780.length; i5++) {
            this.Field780[i5] = new PetItem();
            addWidget(this.Field780[i5], false);
            this.Field780[i5].setPosition(20, LAF.LOT_PADDING + ((30 + LAF.LOT_PADDING) * i5));
            if (i5 != 0) {
                this.Field780[i5].cmdCenter = new Command(0, "Đổi", new Integer(i5), this);
            }
        }
        this.cmdRight = GameController.Field464;
        if (i == 0 || i == 2) {
            this.cmdLeft = new Command(1, T.gL(T.ENCHANT_STR), this);
        } else {
            this.cmdLeft = new Command(1, T.gL(T.EVOLUTIONARY_STR), this);
        }
    }

    public final void Method168(int i, int i2, String str, String str2, byte b) {
        this.Field780[i].itemId = i2;
        this.Field780[i].imgPathId = str;
        this.Field780[i].name = str2;
        this.Field780[i].Method22(b);
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        super.paintBackground();
        for (int i = 0; i < this.Field780.length; i++) {
            if (this.Field780[i].name != null) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field780[i].name, 60, LAF.LOT_PADDING + ((30 + LAF.LOT_PADDING) * i) + ((30 - ResourceManager.boldFont.getHeight()) / 2), 0);
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
                switch (this.Field781) {
                    case 0:
                        MEService.Method29(this.Field780[0].itemId, num.intValue() + 6);
                        return;
                    case 1:
                        MEService.Method29(this.Field780[0].itemId, 123);
                        return;
                    case 2:
                        num.intValue();
                        int i = this.Field780[0].itemId;
                        int i2 = this.Field780[0].type;
                        Message message = new Message(81);
                        message.putByte(80);
                        message.putInt(i);
                        GlobalService.session.sendMessage(message);
                        message.cleanup();
                        return;
                    case 3:
                        int i3 = this.Field780[0].itemId;
                        Message message2 = new Message(81);
                        message2.putByte(81);
                        message2.putInt(i3);
                        GlobalService.session.sendMessage(message2);
                        message2.cleanup();
                        return;
                    default:
                        return;
                }
            case 1:
                boolean z = true;
                for (int i4 = 0; i4 < this.Field780.length; i4++) {
                    if (this.Field780[i4].itemId == 0) {
                        z = false;
                    }
                }
                if (!z) {
                    GameController.startOKDlg(T.gL(T.NOT_ENGOUH_MATTERIAL_STR));
                    return;
                }
                GameController.show(true);
                switch (this.Field781) {
                    case 0:
                        int i5 = this.Field780[0].itemId;
                        int i6 = this.Field780[1].itemId;
                        int i7 = this.Field780[2].itemId;
                        Message message3 = new Message(81);
                        message3.putByte(48);
                        message3.putInt(i5);
                        message3.putInt(i6);
                        message3.putInt(i7);
                        GlobalService.session.sendMessage(message3);
                        message3.cleanup();
                        this.Field782 = new int[3];
                        break;
                    case 1:
                        int i8 = this.Field780[0].itemId;
                        int i9 = this.Field780[1].itemId;
                        Message message4 = new Message(81);
                        message4.putByte(49);
                        message4.putInt(i8);
                        message4.putInt(i9);
                        GlobalService.session.sendMessage(message4);
                        message4.cleanup();
                        this.Field782 = new int[2];
                        break;
                    case 2:
                        int i10 = this.Field780[0].itemId;
                        int i11 = this.Field780[1].itemId;
                        int i12 = this.Field780[2].itemId;
                        Message message5 = new Message(81);
                        message5.putByte(76);
                        message5.putInt(i10);
                        message5.putInt(i11);
                        message5.putInt(i12);
                        GlobalService.session.sendMessage(message5);
                        message5.cleanup();
                        this.Field782 = new int[3];
                        break;
                    case 3:
                        int i13 = this.Field780[0].itemId;
                        int i14 = this.Field780[1].itemId;
                        Message message6 = new Message(81);
                        message6.putByte(79);
                        message6.putInt(i13);
                        message6.putInt(i14);
                        GlobalService.session.sendMessage(message6);
                        message6.cleanup();
                        this.Field782 = new int[2];
                        break;
                }
                for (int i15 = 0; i15 < this.Field780.length; i15++) {
                    this.Field782[i15] = this.Field780[i15].itemId;
                }
                return;
            default:
                return;
        }
    }
}

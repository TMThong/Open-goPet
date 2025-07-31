package defpackage;

import vn.me.ui.common.LAF;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Label;
import javax.microedition.lcdui.Image;

/* renamed from: Class104  reason: default package */
/* loaded from: gopet_repackage.jar:Class104.class */
public class PetItem extends Label {
    public int itemId;
    public int type;
    public String Field668;
    public String imgPathId;
    public String name;
    public int Field671;
    public int Field672;
    public int Field673;
    public int Field674;
    public int Field675;
    public int Field676;
    public int Field677;
    public int Field678;
    public int Field679;
    public int Field680;
    public int Field681;
    public int petEquipId;
    public byte Field683;
    public byte Field684;
    public String Field685;
    public byte typeGem;
    public long currentTime;
    public int Field688 = 0;
    public boolean Field689 = true;

    public PetItem() {
        this.w = 30;
        this.h = 30;
    }

    public final boolean Method126() {
        return this.typeGem == 2;
    }

    public final void Method22(byte b) {
        this.Field684 = b;
        this.Field685 = String.valueOf((int) b);
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(2, 2, this.w - 4, this.h - 4);
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        if (this.isFocused) {
            BaseCanvas.g.setColor(LAF.Field1283);
            BaseCanvas.g.drawRect(0, 0, this.w - 1, this.h - 1);
            BaseCanvas.g.drawRect(1, 1, this.w - 3, this.h - 3);
        }
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public final void paint() {
        Image Method455;
        if (this.imgPathId != null && (Method455 = PetGameModel.Field284.requestImg(this.imgPathId)) != null) {
            BaseCanvas.g.drawImage(Method455, this.w >> 1, this.h >> 1, 3);
        }
        if (!this.Field689) {
            BaseCanvas.g.setColor(16711680);
            BaseCanvas.g.drawRect(0, 0, this.w - 1, this.h - 1);
            BaseCanvas.g.drawRect(1, 1, this.w - 3, this.h - 3);
        }
        if (this.Field684 == 0 || this.Field685 == null) {
            return;
        }
        GameResourceManager.Method117().drawString(BaseCanvas.g, this.Field685, 0, 0, 0);
    }

    public final void Method127(PetItem class104) {
        this.itemId = class104.itemId;
        this.type = class104.type;
        this.Field668 = class104.Field668;
        this.imgPathId = class104.imgPathId;
        this.name = class104.name;
        this.Field671 = class104.Field671;
        this.Field672 = class104.Field672;
        this.Field673 = class104.Field673;
        this.Field674 = class104.Field674;
        this.Field675 = class104.Field675;
        this.Field676 = class104.Field676;
        this.Field677 = class104.Field677;
        this.Field678 = class104.Field678;
        this.Field679 = class104.Field679;
        this.Field680 = class104.Field680;
        this.Field681 = class104.Field681;
        this.petEquipId = class104.petEquipId;
        this.Field683 = class104.Field683;
        Method22(class104.Field684);
        this.typeGem = class104.typeGem;
        this.currentTime = class104.currentTime;
        this.Field688 = class104.Field688;
    }

    public final int Method128() {
        return (int) (this.Field688 - ((System.currentTimeMillis() - this.currentTime) / 1000));
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public final void update() {
        super.update();
        if (this.typeGem != 2 || Method128() >= 0) {
            return;
        }
        this.typeGem = (byte) 0;
        int i = this.itemId;
        Message message = new Message(81);
        message.putByte(82);
        message.putInt(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }
}

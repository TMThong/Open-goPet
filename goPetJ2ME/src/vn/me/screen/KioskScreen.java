package vn.me.screen;

import defpackage.Ulti;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import defpackage.PetGameModel;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import vn.me.screen.Screen;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

/* renamed from: Class77  reason: default package */
 /* loaded from: gopet_repackage.jar:Class77.class */
public final class KioskScreen extends Screen {

    private static int frameWidth = 40;
    private static int frameHeight = 80;
    private int Field492;
    private String Field493;
    private byte type;
    private String Field495;
    private String[] Field496;
    private String Field497;
    private Image petImg;
    private int Field499;
    private long Field500;
    private boolean hasItemUpload;
    private int frameNum = 2;

    public KioskScreen() {
        super(true);
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.cmdLeft = GameController.Field467;
    }

    public final void Method6() {
        this.petImg = null;
        this.hasItemUpload = false;
        this.cmdCenter = new Command(2, T.gL(T.SUBMISSION), this);
    }

    public final void setItem(int i, String str, String str2, String str3, int i2, byte fNum) {
        this.hasItemUpload = true;
        this.Field492 = i;
        this.Field493 = str;
        this.Field495 = str2;
        this.Field496 = ResourceManager.boldFont.wrap(str2, (((BaseCanvas.w - 15) - frameWidth) - 5) - 15);
        this.Field497 = str3;
        this.Field499 = i2;
        this.Field500 = System.currentTimeMillis();
        this.frameNum = fNum;
        this.cmdCenter = new Command(1,T.gL(T.OPTION), this);
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method447();
        LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
        GameResourceManager.Method116().drawString(BaseCanvas.g, T.gL(T.YOUR_STORE), BaseCanvas.Field157, 22, 17);
        int i = 20 + 22;
        LAF.Method413(10, 42, BaseCanvas.w - 20, (BaseCanvas.h - LAF.LOT_ITEM_HEIGHT) - 42);
        if (!this.hasItemUpload) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, T.gL(T.NO_CONSIGNMENT), 15, 47, 0);
            return;
        }
        int i2 = i + 10;
        if (this.petImg == null) {
            Image Method455 = PetGameModel.Field284.requestImg(this.Field493);
            if (Method455 != null) {
                this.petImg = Effects.show0(Method455, Method455.getHeight() << 1);
                frameWidth = this.petImg.getWidth();
                frameHeight = this.petImg.getHeight();
                if (this.type == 4) {
                    frameWidth /= frameNum;
                }
                this.Field496 = ResourceManager.boldFont.wrap(this.Field495, (((BaseCanvas.w - 15) - frameWidth) - 5) - 15);
            }
        } else if (this.type == 4) {
            PetGameModel.Field283.drawPetz(this.petImg, 20, 52, 0, frameNum);
        } else {
            BaseCanvas.g.setColor(0);
            BaseCanvas.g.fillRect(18, 50, frameWidth + 4, frameHeight + 4);
            BaseCanvas.g.drawImage(this.petImg, 20, 52, 0);
        }
        int Method332 = ResourceManager.boldFont.getHeight() + 2;
        for (int i3 = 0; i3 < this.Field496.length; i3++) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field496[i3], 20 + frameWidth + 10, i2, 0);
            i2 += Method332;
        }
        ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field497, 20 + frameWidth + 10, i2, 0);
        int currentTimeMillis = (int) (this.Field499 - ((System.currentTimeMillis() - this.Field500) / 1000));
        int i4 = i2 + (Method332 << 1);
        ResourceManager.boldFont.drawString(BaseCanvas.g, T.gL(T.STILL_VALID_IN), BaseCanvas.w >> 1, i4, 17);
        ResourceManager.boldFont.drawString(BaseCanvas.g, Ulti.Method375(currentTimeMillis), BaseCanvas.w >> 1, i4 + Method332, 17);
    }

    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 1:
                Vector vector = new Vector();
                vector.addElement(new Command(20, T.gL(T.CANCEL_SUBMISSION), this));
                showMenu(vector, 2);
                return;
            case 2:
                GameController.waitDialog();
                byte b = this.type;
                Message message = new Message(81);
                message.putByte(85);
                message.putByte(b);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 20:
                GameController.waitDialog();
                int i = this.Field492;
                Message message2 = new Message(81);
                message2.putByte(87);
                message2.putInt(i);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                return;
            case 21:
            default:
                return;
        }
    }

    public final void Method22(byte b) {
        this.type = b;
    }
}

package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.screen.Screen;
import java.io.IOException;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

public final class PetUprgadeScreen extends Screen {
    private int Field502;
    private String Field503;
    private String[] Field504;
    private int Field505;
    private String Field506;
    private String[] Field507;
    private String Field508;
    private String[] Field509;
    private Image Field510;
    private Image Field511;
    private Image Field512;
    private int Field513;
    private Class81 Field514;
    private Class81 Field515;
    private Image Field516;
    private int Field517;
    private int Field518;
    private int Field519;
    private int Field520;
    private int Field521;
    private int Field522;
    private int Field523;
    private int Field524;
    private int Field525;
    private int Field526;
    private int Field527;
    private long Field528;
    private int Field529;

    public final void Method23(int i, int i2, String str, String[] strArr) {
        switch (i) {
            case 1:
                this.Field502 = i2;
                this.Field503 = str;
                this.Field504 = strArr;
                this.Field510 = null;
                break;
            case 2:
                this.Field505 = i2;
                this.Field506 = str;
                this.Field507 = strArr;
                this.Field511 = null;
                break;
            case 3:
                this.Field508 = str;
                this.Field509 = strArr;
                this.Field512 = null;
                break;
        }
        Method7();
        if (i == 3 || this.Field502 == -1 || this.Field505 == -1) {
            return;
        }
        if (this.Field502 == this.Field505) {
            GameController.Method47(T.gL(T.TWO_PET_ARE_DUPLICATE));
            return;
        }
        GameController.waitDialog();
        int i3 = this.Field502;
        int i4 = this.Field505;
        Message message = new Message(81);
        message.putByte(70);
        message.putInt(i3);
        message.putInt(i4);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public PetUprgadeScreen() {
        super(true);
        this.Field518 = 42;
        this.Field527 = 0;
        try { 
            this.Field516 = Image.createImage("/unknow.png");
        } catch (IOException e) {
            e.printStackTrace();
        }
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        Method25();
        Method7();
    }

    private void Method7() {
        if (this.Field509 != null) {
            this.Field519 = 10 + (ResourceManager.boldFont.getHeight() * this.Field509.length) + ((this.Field509.length - 1) * 3);
        } else {
            this.Field519 = 10 + (ResourceManager.boldFont.getHeight() * 3) + 6;
        }
        this.Field520 = ((BaseCanvas.h - LAF.LOT_ITEM_HEIGHT) - this.Field519) - this.Field518;
        int i = (this.Field520 - 100) / 4;
        this.Field521 = i;
        this.Field522 = 50 + (i * 3);
        int i2 = 0;
        for (int i3 = 0; i3 < this.Field504.length; i3++) {
            int Method330 = ResourceManager.boldFont.getWidth(this.Field504[i3]);
            if (i2 < Method330) {
                i2 = Method330;
            }
        }
        for (int i4 = 0; i4 < this.Field507.length; i4++) {
            int Method3302 = ResourceManager.boldFont.getWidth(this.Field507[i4]);
            if (i2 < Method3302) {
                i2 = Method3302;
            }
        }
        this.Field523 = BaseCanvas.Field157 - ((i2 + 75) >> 1);
        this.Field514.setMetrics(this.Field523, this.Field521 + this.Field518, 50, 50);
        this.Field515.setMetrics(this.Field523, this.Field522 + this.Field518, 50, 50);
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method447();
        LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
        GameResourceManager.Method116().drawString(BaseCanvas.g, T.gL(T.EVOLUTIONARY_STR), BaseCanvas.Field157, 22, 17);
        LAF.Method413(10, this.Field518, BaseCanvas.w - 20, this.Field520);
        switch (this.Field513) {
            case 0:
                int i = this.Field518 + this.Field521;
                Method27(1, this.Field523 + 25, i + 25, 0, 3);
                for (int i2 = 0; i2 < this.Field504.length; i2++) {
                    ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field504[i2], this.Field523 + 70 + 5, i + ((ResourceManager.boldFont.getHeight() + 3) * i2), 0);
                }
                int i3 = this.Field518 + this.Field522;
                Method27(2, this.Field523 + 25, i3 + 25, 0, 3);
                for (int i4 = 0; i4 < this.Field507.length; i4++) {
                    ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field507[i4], this.Field523 + 70 + 5, i3 + ((ResourceManager.boldFont.getHeight() + 3) * i4), 0);
                }
                break;
            case 1:
                Method27(1, this.Field523 + 25, this.Field518 + this.Field524 + 25, 0, 3);
                Method27(2, this.Field523 + 25, this.Field518 + this.Field525 + 25, 0, 3);
                break;
            case 2:
                Method27(3, this.Field523 + 25, this.Field518 + this.Field526 + 25, 0, 3);
                break;
        }
        int i5 = this.Field518 + this.Field520;
        LAF.Method413(10, i5, BaseCanvas.w - 20, this.Field519);
        for (int i6 = 0; i6 < this.Field509.length; i6++) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field509[i6], BaseCanvas.Field157, i5 + 5 + ((ResourceManager.boldFont.getHeight() + 3) * i6), 17);
        }
    }

    public final void update() {
        super.update();
        if (this.Field513 == 1) {
            switch (this.Field527) {
                case 0:
                    boolean z = false;
                    boolean z2 = false;
                    if (this.Field524 < this.Field526) {
                        this.Field524 += 2;
                    }
                    if (this.Field524 >= this.Field526) {
                        this.Field524 = this.Field526;
                        z = true;
                    }
                    if (this.Field525 > this.Field526) {
                        this.Field525 -= 2;
                    }
                    if (this.Field525 <= this.Field526) {
                        this.Field525 = this.Field526;
                        z2 = true;
                    }
                    if (z && z2) {
                        this.Field527 = 1;
                        this.Field528 = System.currentTimeMillis();
                        AnimationEffect2222 class25 = new AnimationEffect2222(this.Field523 + 25, this.Field518 + this.Field526 + 25, false);
                        class25.Method337(2000);
                        Screen.animationEffects.addElement(class25);
                        break;
                    }
                    break;
                case 1:
                    if (System.currentTimeMillis() - this.Field528 >= 2000) {
                        this.Field527 = 0;
                        this.Field513 = 2;
                        this.cmdRight = null;
                        this.cmdCenter = new Command(3, T.gL(T.CONTINUE_STR), this);
                        break;
                    }
                    break;
            }
        }
        if (BaseCanvas.ticks % 2 == 0) {
            this.Field529++;
            if (this.Field529 == ResourceManager.Field1307.nFrame) {
                this.Field529 = 0;
            }
        }
    }

    ///////@Override // defpackage.Screen, defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 0:
                Message message = new Message(81);
                message.putByte(72);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                GameController.waitDialog();
                return;
            case 1:
                GameController.waitDialog();
                MEService.Method316(1);
                return;
            case 2:
                GameController.waitDialog();
                MEService.Method316(2);
                return;
            case 3:
                Method25();
                return;
            case 4:
                this.Field517 = 1;
                Method26();
                return;
            case 5:
                this.Field517 = 2;
                Method26();
                return;
            case 6:
                GameController class74 = GameController.instance;
                String[] Method67 = class74.Method67(Screen.currentDialog);
                if (Method67 == null || Method67.length == 0 || Method67[0].equals("")) {
                    class74.Method47(T.gL(T.MUST_SET_NEW_NAME_FOR_PET));
                    return;
                }
                String str = Method67[0];
                class74.waitDialog();
                int i = this.Field502;
                int i2 = this.Field505;
                int i3 = this.Field517;
                Message message2 = new Message(81);
                message2.putByte(71);
                message2.putInt(i);
                message2.putInt(i2);
                message2.putString(str);
                message2.putByte(i3);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                return;
            default:
                return;
        }
    }

    private void Method25() {
        this.cmdLeft = GameController.Field467;
        this.cmdRight = new Command(0, T.gL(T.EVOLUTIONARY_STR), this);
        this.Field502 = -1;
        this.Field505 = -1;
        this.Field510 = this.Field516;
        this.Field504 = new String[]{T.gL(T.SELECT_PET), T.gL(T.TO_EVOLVE)};
        this.Field511 = this.Field516;
        this.Field507 = new String[]{T.gL(T.SELECT_PET), T.gL(T.TO_EVOLVE)};
        this.Field512 = this.Field516;
        this.Field509 = new String[]{"", ""};
        this.Field513 = 0;
        if (this.Field514 == null) {
            this.Field514 = new Class81(this, (byte) 0);
            this.Field514.setMetrics(this.Field523, this.Field521 + this.Field518, 50, 50);
            this.Field514.cmdCenter = new Command(1, T.gL(T.SELECT_PET), this);
        }
        this.container.addWidget(this.Field514);
        if (this.Field515 == null) {
            this.Field515 = new Class81(this, (byte) 0);
            this.Field515.setMetrics(this.Field523, this.Field522 + this.Field518, 50, 50);
            this.Field515.cmdCenter = new Command(2, T.gL(T.SELECT_PET), this);
        }
        this.container.addWidget(this.Field515);
    }

    private void Method26() {
        GameController.Method64(122, 7, 0, T.gL(T.SET_NEW_PET_NAME), new String[]{""}, new byte[]{0}, new Command(6, T.gL(T.OK), this)).show(true);
    }

    private void Method27(int i, int i2, int i3, int i4, int i5) {
        /*switch (i) {
            case 1:
                if (this.Field510 != null) {
                    PetRenderer class47 = PetGameModel.Field283;
                    PetRenderer.drawPet(this.Field510, i2, i3, 0, 3);
                    return;
                }
                this.Field510 = PetGameModel.Field284.requestImg(this.Field503);
                ResourceManager.Field1307.drawFrame(BaseCanvas.g, this.Field529, i2, i3, 0, 3);
                return;
            case 2:
                if (this.Field511 != null) {
                    PetRenderer class472 = PetGameModel.Field283;
                    PetRenderer.drawPet(this.Field511, i2, i3, 0, 3);
                    return;
                }
                this.Field511 = PetGameModel.Field284.requestImg(this.Field506);
                ResourceManager.Field1307.drawFrame(BaseCanvas.g, this.Field529, i2, i3, 0, 3);
                return;
            case 3:
                if (this.Field512 != null) {
                    PetRenderer class473 = PetGameModel.Field283;
                    PetRenderer.drawPet(this.Field512, i2, i3, 0, 3);
                    return;
                }
                this.Field512 = PetGameModel.Field284.requestImg(this.Field508);
                ResourceManager.Field1307.drawFrame(BaseCanvas.g, this.Field529, i2, i3, 0, 3);
                return;
            default:
                return;
        }*/
    }

    public final void Method6() {
        Method309();
        this.Field513 = 1;
        this.Field527 = 0;
        this.Field524 = this.Field521;
        this.Field525 = this.Field522;
        this.Field526 = (this.Field521 + this.Field522) / 2;
        this.container.removeWidget(this.Field514);
        this.container.removeWidget(this.Field515);
    }

    public final void Method28(int i, int i2) {
        Dialog Method63 = GameController.Method63(0, T.gL(T.PRIEC_OF_EVOLUTIONARY), new int[]{1, 2}, new String[]{i > 0 ? new StringBuffer("1. ").append(i).append(" (vang)").toString() : "1. Miễn phí", i2 > 0 ? new StringBuffer("2. ").append(i2).append(" (ngoc)").toString() : "2. Miễn phí"}, new byte[]{1, 1}, new Command[]{new Command(4, "Chọn", this), new Command(5, "Chọn", this)});
        Method309();
        Method63.show(true);
    }
}

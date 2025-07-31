package vn.me.screen;

import vn.me.core.BaseCanvas;
import defpackage.PetUI;
import vn.me.ui.common.ResourceManager;
import defpackage.MEService;
import defpackage.PetGameModel;
import defpackage.PetInfo;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import vn.me.network.Message;
import vn.me.ui.WidgetGroup;
import vn.me.ui.common.T;

public final class TrScreen extends Screen {

    private WidgetGroup Field720;
    private PetGameModel Field721;
    PetInfo pet;
    private PetUI petUI;
    public int Field724;
    byte[] Field725;
    int[] Field726;
    int[] Field727;
    private String[] Field728;
    String[] Field729;
    public int Field730;
    private String Field731;

    public TrScreen(PetGameModel class43, int i) {
        super(true);
        this.Field720 = new WidgetGroup();
        this.Field730 = i;
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.Field721 = class43;
        this.cmdLeft = GameController.Field467;
        this.petUI = new PetUI(this.Field721);
        this.petUI.setPosition(10 + ((64 - this.petUI.w) / 2), 37);
        this.container.addWidget(this.petUI);
        this.container.addWidget(this.Field720);
        this.Field720.setMetrics(74, 42, (BaseCanvas.w - 64) - 20, 80);
        this.Field720.setPreferredSize(100, 100);
        this.Field720.isScrollableY = true;
        this.Field720.setViewMode(1);
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method448(this.pet.tiemnang);
        LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
        PetGameModel class43 = this.Field721;
        PetGameModel.Method461(this.pet.petName, this.pet.nClass, (BaseCanvas.w >> 1), 22);
        LAF.Method413(10, 42, 64, 80);
        LAF.Method413(74, 42, (BaseCanvas.w - 20) - 64, 80);
        LAF.Method413(10, 122, BaseCanvas.w - 20, ((BaseCanvas.h - 122) - 5) - LAF.Field1293);
        int i = 130;
        if (this.Field731 != null) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field731, 16, 130, 0);
            i = 130 + ResourceManager.boldFont.getHeight() + 4;
        }
        if (this.Field729 != null) {
            for (int i2 = 0; i2 < this.Field729.length; i2++) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field729[i2], 16, i, 0);
                i += ResourceManager.boldFont.getHeight();
            }
        }
    }

    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 10:
                int Method352 = this.Field720.getFocusedIndex();
                int i = this.pet.petId;
                Message message = new Message(81);
                message.putByte(19);
                message.putInt(i);
                message.putByte(Method352);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                GameController.waitDialog();
                return;
            case 11:
                int Method3522 = this.Field720.getFocusedIndex();
                if (Method3522 < this.pet.skillName.length) {
                    GameController.Method45(T.gL(T.DO_YOU_WANT_CHANGE_PET_SKILL), new Command(12, T.gL(T.LEARN), new int[]{Method3522}, this), GameController.Field464);
                    return;
                }
                GameController.waitDialog();
                MEService.Method146(-1);
                return;
            case 12:
                int i2 = ((int[]) Command.objPerfomed)[0];
                GameController.waitDialog();
                MEService.Method146(this.pet.skillId[i2]);
                return;
            default:
                super.actionPerformed(obj);
                return;
        }
    }

    public final void setTrainData(PetInfo class58, int[] iArr, int[] iArr2, byte[] bArr, String[] strArr, byte[] bArr2) {
        this.pet = class58;
        this.petUI.setPetInfo(class58);
        this.Field726 = iArr;
        this.Field727 = iArr2;
        this.Field725 = bArr2;
        this.Field728 = strArr;
        this.Field731 = new StringBuffer("(str)").append(this.pet.str).append(" (agi)").append(this.pet.agi).append(" (int)").append(this.pet._int).toString();
        this.Field720.removeAll();
        for (int i = 0; i < 3; i++) {
            TrainScreen class112 = new TrainScreen(this, i);
            class112.setSize((BaseCanvas.w - 85) - 17, 20);
            class112.setPosition(10, 5 + (i * 25));
            this.Field720.addWidget(class112);
            if (this.Field730 == 0) {
                this.cmdCenter = new Command(10, T.gL(T.OK), this);
            } else {
                this.cmdCenter = new Command(11, T.gL(T.LEARN), this);
            }
        }
        requestFocus(this.Field720.getWidgetAt(this.Field724));
    }

    public final void setInfo(PetInfo class58) {
        setTrainData(class58, null, null, null, null, null);
    }

    public final void onMessage(Message message) {
        try {
            if (message.reader().readInt() != this.pet.petId) {
                return;
            }
            this.pet.str = message.reader().readInt();
            this.pet.agi = message.reader().readInt();
            this.pet._int = message.reader().readInt();
            int[] iArr = new int[3];
            int[] iArr2 = new int[3];
            byte[] bArr = new byte[3];
            String[] strArr = new String[3];
            byte[] bArr2 = new byte[3];
            for (int i = 0; i < 3; i++) {
                iArr[i] = message.reader().readInt();
                iArr2[i] = message.reader().readInt();
                bArr[i] = message.reader().readByte();
                strArr[i] = message.reader().readUTF();
                bArr2[i] = message.reader().readByte();
            }
            setTrainData(this.pet, iArr, iArr2, bArr, strArr, bArr2);
            GameController.Method40(new StringBuffer( T.gL(T.GYM_UP_OK) +   " \n(str) ").append(this.pet.str).append("\n(agi) ").append(this.pet.agi).append("\n(int) ").append(this.pet._int).toString(), true);
            this.pet.tiemnang--;
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static String Method141(TrScreen class111) {
        return class111.Field728[class111.Field724];
    }
}

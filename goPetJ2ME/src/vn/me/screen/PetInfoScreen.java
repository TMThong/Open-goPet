package vn.me.screen;

import defpackage.Class105;
import defpackage.Class110;
import defpackage.PetUI;
import defpackage.Class119;
import defpackage.Class122;
import defpackage.Class123;
import defpackage.Ulti;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import defpackage.PetGameModel;
import defpackage.PetInfo;
import defpackage.PetItem;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Label;
import vn.me.ui.Widget;
import vn.me.screen.Screen;
import java.io.IOException;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

public final class PetInfoScreen extends Screen {

    private PetUI petUI;
    private PetInfo Field702;
    private Class110[] Field703;
    private int[] Field704;
    private String Field705;
    private Class105 Field706;
    private PetItem[] Field707;
    private String[] Field708;
    private boolean Field709;
    private PetItem Field710;
    private PetItem Field711;
    private int Field712;
    private Class119 Field713;
    private Class119 Field714;
    private int Field715;
    private int Field716;
    private int Field717;
    private Class122 Field718;

    public PetInfoScreen(PetGameModel class43, boolean z) {
        super(true);
        this.Field715 = 124;
        this.Field716 = ((BaseCanvas.w - 30) - 30) - 15;
        this.Field717 = (BaseCanvas.w - 30) - 15;
        this.Field709 = z;
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.petUI = new PetUI(class43);
        this.petUI.setPosition(15 + ((64 - this.petUI.w) / 2), 57);
        this.petUI.Field740 = false;
        this.petUI.isFocusable = false;
        this.container.addWidget(this.petUI);
        if (z) {
            this.Field706 = new Class105(15, 172, BaseCanvas.w - 30, ((BaseCanvas.h - 172) - 10) - LAF.Field1293, 30);
            this.Field706.cmdRight = new Command(4, T.gL(T.STOP), this);
            this.Field706.cmdCenter = new Command(5, T.gL(T.USE), this);
            this.Field706.cmdLeft = new Command(11, T.gL(T.OPTION), this);
        }
        this.cmdLeft = GameController.Field467;
        this.Field703 = new Class110[5];
        this.Field704 = new int[5];
        for (int i = 0; i < 5; i++) {
            this.Field703[i] = new Class110(this, (byte) 0);
        }
        this.Field704[0] = 3;
        this.Field704[1] = 2;
        this.Field704[2] = 1;
        this.Field704[3] = 104;
        this.Field704[4] = 105;
        int[] iArr = {37, 16, 57, 16, 57};
        int[] iArr2 = {50, 90, 90, 120, 120};
        for (int i2 = 0; i2 < this.Field703.length; i2++) {
            this.Field703[i2].setMetrics(iArr[i2], iArr2[i2], 25, 25);
            if (this.Field709) {
                this.Field703[i2].cmdCenter = new Command(0, T.gL(T.CHANGE), new Integer(i2), this);
            }
            this.container.addWidget(this.Field703[i2]);
            this.Field703[i2].onFocusAction = this;
        }
        this.Field703[0].requestFocus();
        this.container.removeWidget(this.Field706);
        this.Field715 = (BaseCanvas.h - 20) >> 1;
        Image iOException = null;
        Image iOException2 = null;
        try {
            iOException = Image.createImage("/pet/left.png");
            iOException2 = iOException;
        } catch (IOException e) {
            e.printStackTrace();
        }
        this.Field713 = new Class119(iOException2);
        this.Field713.setMetrics(this.Field716, this.Field715, 30, 20);
        this.Field713.Method279(new Command(17, "", this));
        Image iOException3 = null;
        Image iOException4 = null;
        try {
            iOException3 = Image.createImage("/pet/right.png");
            iOException4 = iOException3;
        } catch (IOException e2) {
            e2.printStackTrace();
        }
        this.Field714 = new Class119(iOException4);
        this.Field714.setMetrics(this.Field717, this.Field715, 30, 20);
        this.Field714.Method279(new Command(18, "", this));
    }

    private Class110 Method142(int i) {
        for (int i2 = 0; i2 < 5; i2++) {
            if (this.Field704[i2] == i) {
                return this.Field703[i2];
            }
        }
        return null;
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method448(this.Field702.tiemnang);
        LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
        PetGameModel.Method461(this.Field702.petName, this.Field702.nClass, BaseCanvas.w >> 1, 22);
        LAF.Method413(10, 42, 80, 110);
        LAF.Method413(90, 42, (BaseCanvas.w - 20) - 80, 110);
        int i = 47;
        if (this.Field708 != null) {
            for (int i2 = 0; i2 < this.Field708.length; i2++) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field708[i2], 95, i, 0);
                i += ResourceManager.boldFont.getHeight() + 2;
            }
            if (!this.Field711.Field689) {
                GameResourceManager.largeFont.drawString(BaseCanvas.g, "Pet khác đang mặc", 95, i, 0);
            }
        }
        if (this.Field711 != null && this.Field711.Method126()) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, "Đang tháo ngọc,", 95, i, 0);
            ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("còn ").append(Ulti.Method374(this.Field711.Method128())).toString(), 95, i + ResourceManager.boldFont.getHeight() + 2, 0);
        }
        int i3 = 25;
        LAF.Method413(10, 152, BaseCanvas.w - 20, ((BaseCanvas.h - 152) - 5) - LAF.Field1293);
        if (this.Field705 != null) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field705, 25, 157, 20);
            i3 = 25 + ResourceManager.boldFont.getWidth(this.Field705);
        }
        if (this.Field706 == null || this.Field706.Field691 <= 1) {
            return;
        }
        int i4 = i3 + 10;
        for (int i5 = 0; i5 < this.Field706.Field691; i5++) {
            if (i5 == this.Field706.Field692) {
                BaseCanvas.g.setColor(16777215);
            } else {
                BaseCanvas.g.setColor(10000536);
            }
            BaseCanvas.g.fillArc(i4, 162, 5, 5, 0, 360);
            i4 += 10;
        }
    }

    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        if (Command == null) {
            Widget class184 = (Widget) ((Object[]) obj)[1];
            boolean z = false;
            int i = 0;
            while (true) {
                if (i >= 5) {
                    break;
                } else if (class184 == this.Field703[i]) {
                    z = true;
                    break;
                } else {
                    i++;
                }
            }
            this.Field708 = null;
            this.Field711 = null;
            if (z) {
                Class110 class110 = (Class110) class184;
                if (class110.Field719 != null) {
                    if (class110.Field719.name != null) {
                        this.Field708 = ResourceManager.boldFont.wrap(class110.Field719.name, (BaseCanvas.w - 100) - 10);
                    }
                    this.Field711 = class110.Field719;
                    return;
                }
                return;
            }
            for (int i2 = 0; i2 < this.Field707.length; i2++) {
                if (class184 == this.Field707[i2]) {
                    PetItem class104 = (PetItem) class184;
                    if (class104.name != null) {
                        this.Field708 = ResourceManager.boldFont.wrap(class104.name, (BaseCanvas.w - 100) - 10);
                    }
                    this.Field711 = class104;
                }
            }
            return;
        }
        switch (Command.cmdId) {
            case 0:
                switch (((Integer) Command.objPerfomed).intValue()) {
                    case 0:
                        this.Field705 = "Nón";
                        Method144(3);
                        return;
                    case 1:
                        this.Field705 = "Giáp";
                        Method144(2);
                        return;
                    case 2:
                        this.Field705 = "Vũ khí";
                        Method144(1);
                        return;
                    case 3:
                        this.Field705 = "Giày";
                        Method144(104);
                        return;
                    case 4:
                        this.Field705 = "Bao tay";
                        Method144(105);
                        return;
                    default:
                        return;
                }
            case 1:
            case 2:
            case 3:
            default:
                super.actionPerformed(obj);
                return;
            case 4:
                this.container.removeWidget(this.Field713);
                this.container.removeWidget(this.Field714);
                this.Field703[0].requestFocus();
                this.container.removeWidget(this.Field706);
                return;
            case 5:
                Widget Method315 = this.Field706.getFocusedWidget(true);
                if (Method315 == null || Method315 == this.Field706) {
                    return;
                }
                int i3 = ((PetItem) this.Field706.getFocusedWidget(true)).itemId;
                Message message = new Message(81);
                message.putByte(29);
                message.putInt(i3);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                GameController.waitDialog();
                return;
            case 6:
                int intValue = ((Integer) Command.objPerfomed).intValue();
                Message message2 = new Message(81);
                message2.putByte(39);
                message2.putInt(intValue);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                GameController.waitDialog();
                return;
            case 7:
                Widget Method3152 = this.Field706.getFocusedWidget(true);
                if (Method3152 == null || Method3152 == this.Field706) {
                    return;
                }
                Method127((PetItem) this.Field706.getFocusedWidget(true));
                return;
            case 8:
                PetItem class1042 = (PetItem) Command.objPerfomed;
                Vector vector = new Vector();
                switch (class1042.typeGem) {
                    case 0:
                        vector.addElement(new Command(14, "Gắn ngọc", class1042, this));
                        break;
                    case 1:
                        vector.addElement(new Command(15, "Tháo ngọc", class1042, this));
                        break;
                    case 2:
                        vector.addElement(new Command(16, "Tháo ngọc nhanh", class1042, this));
                        break;
                }
                vector.addElement(new Command(6, "Tháo ra", new Integer(class1042.itemId), this));
                vector.addElement(new Command(9, T.gL(T.ENCHANT_STR), class1042, this));
                vector.addElement(new Command(10, T.gL(T.EVOLUTIONARY_STR), class1042, this));
                showMenu(vector, 0);
                return;
            case 9:
                Method127((PetItem) Command.objPerfomed);
                return;
            case 10:
                Method145((PetItem) Command.objPerfomed);
                return;
            case 11:
                Widget Method3153 = this.Field706.getFocusedWidget(true);
                if (Method3153 == null || Method3153 == this.Field706) {
                    return;
                }
                PetItem class1043 = (PetItem) this.Field706.getFocusedWidget(true);
                Vector vector2 = new Vector();
                switch (class1043.typeGem) {
                    case 0:
                        vector2.addElement(new Command(14, "Gắn ngọc", class1043, this));
                        break;
                    case 1:
                        vector2.addElement(new Command(15, "Tháo ngọc", class1043, this));
                        break;
                    case 2:
                        vector2.addElement(new Command(16, "Tháo ngọc nhanh", class1043, this));
                        break;
                }
                if (!class1043.Field689) {
                    vector2.addElement(new Command(6, "Tháo ra", new Integer(class1043.itemId), this));
                }
                vector2.addElement(new Command(7, T.gL(T.ENCHANT_STR), class1043, this));
                vector2.addElement(new Command(12, T.gL(T.EVOLUTIONARY_STR), class1043, this));
                vector2.addElement(new Command(13, T.gL(T.CANCEL), new Integer(class1043.itemId), this));
                showMenu(vector2, 0);
                return;
            case 12:
                Widget Method3154 = this.Field706.getFocusedWidget(true);
                if (Method3154 == null || Method3154 == this.Field706) { 
                    return;
                }
                Method145((PetItem) this.Field706.getFocusedWidget(true));
                return;
            case 13:
                GameController.waitDialog();
                int intValue2 = ((Integer) Command.objPerfomed).intValue();
                Message message3 = new Message(81);
                message3.putByte(56);
                message3.putInt(intValue2);
                GlobalService.session.sendMessage(message3);
                message3.cleanup();
                return;
            case 14:
                this.Field712 = ((PetItem) Command.objPerfomed).itemId;
                GameController.waitDialog();
                int i4 = this.Field712;
                Message message4 = new Message(81);
                message4.putByte(73);
                message4.putInt(i4);
                GlobalService.session.sendMessage(message4);
                message4.cleanup();
                return;
            case 15:
                this.Field712 = ((PetItem) Command.objPerfomed).itemId;
                GameController.waitDialog();
                int i5 = this.Field712;
                Message message5 = new Message(81);
                message5.putByte(75);
                message5.putInt(i5);
                GlobalService.session.sendMessage(message5);
                message5.cleanup();
                return;
            case 16:
                this.Field712 = ((PetItem) Command.objPerfomed).itemId;
                GameController.waitDialog();
                int i6 = this.Field712;
                Message message6 = new Message(81);
                message6.putByte(78);
                message6.putInt(i6);
                GlobalService.session.sendMessage(message6);
                message6.cleanup();
                return;
            case 17:
                this.Field706.Method79(this.Field706.Field692 == 0 ? 0 : this.Field706.Field692 - 1);
                return;
            case 18:
                this.Field706.Method79(this.Field706.Field692 == this.Field706.Field691 - 1 ? this.Field706.Field691 - 1 : this.Field706.Field692 + 1);
                return;
        }
    }

    private void Method6() {
        for (int i = 0; i < 5; i++) {
            this.Field703[i].cmdLeft = null;
            this.Field703[i].Field719 = null;
        }
        for (int i2 = 0; i2 < this.Field707.length; i2++) {
            if (this.Field707[i2].petEquipId == this.Field702.petId) {
                Class110 Method142 = Method142(this.Field707[i2].type);
                Method142.Field719 = this.Field707[i2];
                Method142.cmdLeft = new Command(8, T.gL(T.OPTION), this.Field707[i2], this);
            }
        }
    }

    public final void setItem1(PetInfo class58, PetItem[] class104Arr) {
        this.petUI.setPetInfo(class58);
        this.Field702 = class58;
        this.Field707 = class104Arr;
        for (int i = 0; i < this.Field707.length; i++) {
            this.Field707[i].onFocusAction = this;
        }
        Method6();
    }

    private void Method144(int i) {
        this.container.removeWidget(this.Field713);
        this.container.removeWidget(this.Field714);
        int i2 = 0;
        for (int i3 = 0; i3 < this.Field707.length; i3++) {
            if (Method2(this.Field707[i3].type, i) && this.Field707[i3].petEquipId != this.Field702.petId) {
                i2++;
            }
        }
        if (i2 != 0) {
            PetItem[] class104Arr = new PetItem[i2];
            int i4 = 0;
            for (int i5 = 0; i5 < this.Field707.length; i5++) {
                if (Method2(this.Field707[i5].type, i) && this.Field707[i5].petEquipId != this.Field702.petId) {
                    class104Arr[i4] = this.Field707[i5];
                    if (this.Field707[i5].petEquipId == 0 || this.Field707[i5].petEquipId == -1) {
                        class104Arr[i4].Field689 = true;
                    } else {
                        class104Arr[i4].Field689 = false;
                    }
                    i4++;
                }
            }
            this.Field706.Method125(class104Arr);
            this.Field706.Method79(0);
            if (this.Field706.Field691 > 1) {
                this.container.addWidget(this.Field713);
                this.container.addWidget(this.Field714);
            }
        } else {
            Label class173 = new Label("Không có item nào.", ResourceManager.boldFont);
            class173.isFocusable = false;
            this.Field706.removeAll();
            this.Field706.addWidget(class173);
        }
        this.container.addWidget(this.Field706);
    }

    public final void setUse(int i) {
        Screen.hideDialog(Screen.currentDialog);
        PetItem class104 = null;
        int i2 = 0;
        while (true) {
            if (i2 >= this.Field707.length) {
                break;
            } else if (this.Field707[i2].itemId == i) {
                class104 = this.Field707[i2];
                break;
            } else {
                i2++;
            }
        }
        if (class104 == null) {
            return;
        }
        class104.petEquipId = this.Field702.petId;
        int i3 = -1;
        Class110 Method142 = Method142(class104.type);
        if (Method142.Field719 != null) {
            i3 = Method142.Field719.itemId;
        }
        int i4 = 0;
        while (true) {
            if (i4 >= this.Field707.length) {
                break;
            } else if (i3 == this.Field707[i4].itemId) {
                this.Field707[i4].petEquipId = 0;
                break;
            } else {
                i4++;
            }
        }
        Method6();
        this.Field706.removeAll();
        this.Field703[0].requestFocus();
        this.container.removeWidget(this.Field706);
    }

    public final void Method104(int i) {
        Screen.hideDialog(Screen.currentDialog);
        PetItem class104 = null;
        int i2 = 0;
        while (true) {
            if (i2 >= this.Field707.length) {
                break;
            } else if (this.Field707[i2].itemId == i) {
                class104 = this.Field707[i2];
                break;
            } else {
                i2++;
            }
        }
        if (class104 == null) {
            return;
        }
        class104.petEquipId = 0;
        Method6();
        this.Field706.removeAll();
        this.Field703[0].requestFocus();
        this.container.removeWidget(this.Field706);
    }

    private void Method127(PetItem class104) {
        this.Field718 = new Class122(0);
        this.Field718.Method168(0, class104.itemId, class104.imgPathId, class104.name, class104.Field684);
        showDialog(this.Field718, false);
        this.Field710 = class104;
    }

    public final void Method92(Message message) {
        try {
            if (this.Field718 == null) {
                return;
            }
            int readInt = message.reader().readInt();
            String readUTF = message.reader().readUTF();
            String readUTF2 = message.reader().readUTF();
            int readInt2 = message.reader().readInt();
            if (this.Field718.Field781 == 0) {
                this.Field718.Method168(readInt2 - 6, readInt, readUTF, readUTF2, (byte) 0);
            } else {
                this.Field718.Method168(1, readInt, readUTF, readUTF2, (byte) 0);
            }
            showDialog(this.Field718, true);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public final void Method93(Message message) {
        try {
            byte readByte = message.reader().readByte();
            int readInt = message.reader().readInt();
            String readUTF = message.reader().readUTF();
            String readUTF2 = message.reader().readUTF();
            String readUTF3 = message.reader().readUTF();
            int readInt2 = message.reader().readInt();
            int readInt3 = message.reader().readInt();
            int readInt4 = message.reader().readInt();
            int readInt5 = message.reader().readInt();
            int readInt6 = message.reader().readInt();
            int readInt7 = message.reader().readInt();
            int readInt8 = message.reader().readInt();
            byte readByte2 = message.reader().readByte();
            byte readByte3 = message.reader().readByte();
            if (this.Field718.Field781 == 0) {
                if (readByte == -1) {
                    PetItem[] class104Arr = new PetItem[this.Field707.length - 1];
                    int i = 0;
                    for (int i2 = 0; i2 < this.Field707.length; i2++) {
                        if (this.Field707[i2].itemId != readInt) {
                            int i3 = i;
                            i++;
                            class104Arr[i3] = this.Field707[i2];
                        }
                    }
                    this.Field707 = new PetItem[class104Arr.length];
                    System.arraycopy(class104Arr, 0, this.Field707, 0, this.Field707.length);
                    this.Field703[0].requestFocus();
                    this.container.removeWidget(this.Field706);
                } else {
                    int i4 = 0;
                    while (true) {
                        if (i4 >= this.Field707.length) {
                            break;
                        } else if (this.Field707[i4].itemId == readInt) {
                            this.Field707[i4].name = readUTF2;
                            this.Field707[i4].Field668 = readUTF3;
                            this.Field707[i4].Field675 = readInt2;
                            this.Field707[i4].Field676 = readInt3;
                            this.Field707[i4].Field677 = readInt4;
                            this.Field707[i4].Field678 = readInt5;
                            this.Field707[i4].Field679 = readInt6;
                            this.Field707[i4].Field680 = readInt7;
                            this.Field707[i4].Field681 = readInt8;
                            this.Field707[i4].Field683 = readByte2;
                            this.Field707[i4].Method22(readByte3);
                            break;
                        } else {
                            i4++;
                        }
                    }
                }
                this.Field703[0].requestFocus();
            } else {
                PetItem class104 = null;
                PetItem class1042 = null;
                for (int i5 = 0; i5 < this.Field707.length; i5++) {
                    if (this.Field707[i5].itemId == this.Field718.Field782[0]) {
                        class104 = this.Field707[i5];
                    }
                    if (this.Field707[i5].itemId == this.Field718.Field782[1]) {
                        class1042 = this.Field707[i5];
                    }
                }
                for (int i6 = 0; i6 < 5; i6++) {
                    if ((class104 != null && this.Field703[i6].Field719 != null && this.Field703[i6].Field719.itemId == class104.itemId) || (class1042 != null && this.Field703[i6].Field719 != null && this.Field703[i6].Field719.itemId == class1042.itemId)) {
                        this.Field703[i6].Field719 = null;
                        this.Field703[i6].cmdLeft = null;
                    }
                }
                PetItem class1043 = new PetItem();
                class1043.Method127(this.Field710);
                class1043.itemId = readInt;
                class1043.imgPathId = readUTF;
                class1043.name = readUTF2;
                class1043.Field668 = readUTF3;
                class1043.Field675 = readInt2;
                class1043.Field676 = readInt3;
                class1043.Field677 = readInt4;
                class1043.Field678 = readInt5;
                class1043.Field679 = readInt6;
                class1043.Field680 = readInt7;
                class1043.Field681 = readInt8;
                class1043.Field683 = readByte2;
                class1043.petEquipId = 0;
                class1043.onFocusAction = this;
                class1043.Method22(readByte3);
                PetItem[] class104Arr2 = new PetItem[this.Field707.length - 1];
                int i7 = 0;
                for (int i8 = 0; i8 < this.Field707.length; i8++) {
                    if (this.Field707[i8].itemId != this.Field718.Field782[0] && this.Field707[i8].itemId != this.Field718.Field782[1]) {
                        int i9 = i7;
                        i7++;
                        class104Arr2[i9] = this.Field707[i8];
                    }
                }
                class104Arr2[i7] = class1043;
                this.Field707 = new PetItem[class104Arr2.length];
                System.arraycopy(class104Arr2, 0, this.Field707, 0, this.Field707.length);
                this.Field703[0].requestFocus();
                this.container.removeWidget(this.Field706);
            }
            Method309();
            Class123 class123 = new Class123(readByte == 1, 30);
            PetItem class1044 = new PetItem();
            class1044.itemId = readInt;
            class1044.imgPathId = readUTF;
            class1044.Method22(readByte3);
            class123.Method169(class1044, readUTF2);
            showDialog(class123, false);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void Method145(PetItem class104) {
        this.Field718 = new Class122(1);
        this.Field718.Method168(0, class104.itemId, class104.imgPathId, class104.name, class104.Field684);
        showDialog(this.Field718, false);
        this.Field710 = class104;
    }

    public final void Method146(int i) {
        Screen.hideDialog(Screen.currentDialog);
        boolean z = false;
        PetItem[] class104Arr = new PetItem[this.Field707.length];
        int i2 = 0;
        for (int i3 = 0; i3 < this.Field707.length; i3++) {
            if (this.Field707[i3].itemId != i) {
                int i4 = i2;
                i2++;
                class104Arr[i4] = this.Field707[i3];
            } else {
                z = true;
            }
        }
        if (z) {
            this.Field707 = new PetItem[class104Arr.length - 1];
            System.arraycopy(class104Arr, 0, this.Field707, 0, this.Field707.length);
        }
        this.Field703[0].requestFocus();
        this.container.removeWidget(this.Field706);
    }

    public final void Method94(Message message) {
        try {
            int readInt = message.reader().readInt();
            int i = 0;
            while (true) {
                if (i >= this.Field707.length) {
                    break;
                } else if (this.Field707[i].itemId == readInt) {
                    this.Field707[i].imgPathId = message.reader().readUTF();
                    this.Field707[i].Field668 = message.reader().readUTF();
                    this.Field707[i].name = message.reader().readUTF();
                    this.Field707[i].type = message.reader().readInt();
                    this.Field707[i].petEquipId = message.reader().readInt();
                    this.Field707[i].Field671 = message.reader().readInt();
                    this.Field707[i].Field672 = message.reader().readInt();
                    this.Field707[i].Field673 = message.reader().readInt();
                    this.Field707[i].Field674 = message.reader().readInt();
                    this.Field707[i].Field675 = message.reader().readInt();
                    this.Field707[i].Field676 = message.reader().readInt();
                    this.Field707[i].Field677 = message.reader().readInt();
                    this.Field707[i].Field678 = message.reader().readInt();
                    this.Field707[i].Field679 = message.reader().readInt();
                    this.Field707[i].Field680 = message.reader().readInt();
                    this.Field707[i].Field681 = message.reader().readInt();
                    this.Field707[i].Field683 = message.reader().readByte();
                    this.Field707[i].Method22(message.reader().readByte());
                    this.Field707[i].typeGem = (byte) 1;
                    break;
                } else {
                    i++;
                }
            }
            this.Field703[0].requestFocus();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void Method147(PetItem class104) {
        if (this.Field711 != class104 || this.Field711.name == null) {
            return;
        }
        this.Field708 = ResourceManager.boldFont.wrap(this.Field711.name, (BaseCanvas.w - 100) - 10);
    }

    public final void Method95(Message message) {
        try {
            int readInt = message.reader().readInt();
            int i = 0;
            while (true) {
                if (i >= this.Field707.length) {
                    break;
                } else if (this.Field707[i].itemId == readInt) {
                    this.Field707[i].imgPathId = message.reader().readUTF();
                    this.Field707[i].Field668 = message.reader().readUTF();
                    this.Field707[i].name = message.reader().readUTF();
                    this.Field707[i].type = message.reader().readInt();
                    this.Field707[i].petEquipId = message.reader().readInt();
                    this.Field707[i].Field671 = message.reader().readInt();
                    this.Field707[i].Field672 = message.reader().readInt();
                    this.Field707[i].Field673 = message.reader().readInt();
                    this.Field707[i].Field674 = message.reader().readInt();
                    this.Field707[i].Field675 = message.reader().readInt();
                    this.Field707[i].Field676 = message.reader().readInt();
                    this.Field707[i].Field677 = message.reader().readInt();
                    this.Field707[i].Field678 = message.reader().readInt();
                    this.Field707[i].Field679 = message.reader().readInt();
                    this.Field707[i].Field680 = message.reader().readInt();
                    this.Field707[i].Field681 = message.reader().readInt();
                    this.Field707[i].Field683 = message.reader().readByte();
                    this.Field707[i].Method22(message.reader().readByte());
                    this.Field707[i].typeGem = (byte) 2;
                    message.reader().readLong();
                    this.Field707[i].Field688 = message.reader().readInt();
                    this.Field707[i].currentTime = System.currentTimeMillis();
                    Method147(this.Field707[i]);
                    break;
                } else {
                    i++;
                }
            }
            this.Field703[0].requestFocus();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private static boolean Method2(int i, int i2) {
        return i == i2 || i == i2 + 100;
    }

    public final void Method148(Message message) {
        try {
            int readInt = message.reader().readInt();
            Screen.hideDialog(Screen.currentDialog);
            for (int i = 0; i < this.Field707.length; i++) {
                if (this.Field707[i].itemId == readInt) {
                    PetItem petItem = this.Field707[i];
                    petItem.imgPathId = message.reader().readUTF();
                    petItem.Field668 = message.reader().readUTF();
                    petItem.name = message.reader().readUTF();
                    petItem.type = message.reader().readInt();
                    petItem.petEquipId = message.reader().readInt();
                    petItem.Field671 = message.reader().readInt();
                    petItem.Field672 = message.reader().readInt();
                    petItem.Field673 = message.reader().readInt();
                    petItem.Field674 = message.reader().readInt();
                    petItem.Field675 = message.reader().readInt();
                    petItem.Field676 = message.reader().readInt();
                    petItem.Field677 = message.reader().readInt();
                    petItem.Field678 = message.reader().readInt();
                    petItem.Field679 = message.reader().readInt();
                    petItem.Field680 = message.reader().readInt();
                    petItem.Field681 = message.reader().readInt();
                    petItem.Field683 = message.reader().readByte();
                    petItem.Method22(message.reader().readByte());
                    if (message.reader().readBoolean()) {
                        long readLong = message.reader().readLong();
                        petItem.Field688 = message.reader().readInt();
                        if (readLong > 0) {
                            petItem.typeGem = (byte) 2;
                            petItem.currentTime = System.currentTimeMillis();
                        } else {
                            petItem.typeGem = (byte) 1;
                        }
                    } else {
                        petItem.typeGem = (byte) 0;
                    }
                    Method147(petItem);
                    return;
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

package vn.me.screen;

import vn.me.core.BaseCanvas;
import defpackage.PetItem;
import defpackage.Class105;
import defpackage.Class119;
import defpackage.Class122;
import defpackage.Class123;
import defpackage.PetGameModel;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import vn.me.ui.Label;
import vn.me.network.Message;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.Widget;
import java.io.IOException;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

public final class GemScreen extends Screen {

    private Class105 Field530;
    private PetItem[] Field531;
    private String[] Field532;
    private Class122 Field533;
    private PetItem Field534;
    private Class119 Field535;
    private Class119 Field536;
    private int Field537;
    private int Field538;
    private int Field539;

    public GemScreen() {
        super(true);
        this.Field537 = 96;
        this.Field538 = ((BaseCanvas.w - 30) - 30) - 15;
        this.Field539 = (BaseCanvas.w - 30) - 15;
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.cmdLeft = new Command(1, T.gL(T.CLOSE_STR), this);
        this.Field530 = new Class105(15, 114, BaseCanvas.w - 30, (((BaseCanvas.h - 100) - 15) - 9) - LAF.Field1293, 30);
        this.container.addWidget(this.Field530);
        this.cmdCenter = new Command(2, T.gL(T.SELECT_STR), this);
        Image iOException = null;
        Image iOException2 = null;
        try {
            iOException = Image.createImage("/pet/left.png");
            iOException2 = iOException;
        } catch (IOException e) {
            e.printStackTrace();
        }
        this.Field535 = new Class119(iOException2);
        this.Field535.setMetrics(this.Field538, this.Field537, 30, 20);
        this.Field535.Method279(new Command(17, "", this));
        Image iOException3 = null;
        Image iOException4 = null;
        try {
            iOException3 = Image.createImage("/pet/right.png");
            iOException4 = iOException3;
        } catch (IOException e2) {
            e2.printStackTrace();
        }
        this.Field536 = new Class119(iOException4);
        this.Field536.setMetrics(this.Field539, this.Field537, 30, 20);
        this.Field536.Method279(new Command(18, "", this));
    }

    public final void Method91(PetItem[] class104Arr) {
        this.container.removeWidget(this.Field535);
        this.container.removeWidget(this.Field536);
        if (class104Arr == null || class104Arr.length <= 0) {
            Label class173 = new Label(T.gL(T.NO_ITEMS_STR), ResourceManager.boldFont);
            class173.isFocusable = false;
            this.Field530.removeAll();
            this.Field530.addWidget(class173);
            return;
        }
        this.Field531 = class104Arr;
        for (int i = 0; i < this.Field531.length; i++) {
            this.Field531[i].onFocusAction = this;
        }
        this.Field530.Method125(class104Arr);
        this.Field530.Method79(0);
        if (this.Field530.Field691 > 1) {
            this.container.addWidget(this.Field535);
            this.container.addWidget(this.Field536);
        }
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method448(0);
        LAF.Method413(10, 20, BaseCanvas.w - 20, 80);
        if (this.Field532 != null) {
            int i = 25;
            for (int i2 = 0; i2 < this.Field532.length; i2++) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field532[i2], 20, i, 0);
                i += ResourceManager.boldFont.getHeight() + 2;
            }
        }
        LAF.Method413(10, 100, BaseCanvas.w - 20, ((BaseCanvas.h - 100) - 5) - LAF.Field1293);
        int i3 = 20;
        for (int i4 = 0; i4 < this.Field530.Field691; i4++) {
            if (i4 == this.Field530.Field692) {
                BaseCanvas.g.setColor(16777215);
            } else {
                BaseCanvas.g.setColor(10000536);
            }
            BaseCanvas.g.fillArc(i3, 107, 5, 5, 0, 360);
            i3 += 10;
        }
    }

    ///////@Override // defpackage.Screen, defpackage.Class200
    public final void actionPerformed(Object obj) {
        PetItem class104;
        PetItem class1042;
        PetItem class1043;
        Command Command = (Command) ((Object[]) obj)[0];
        if (Command == null) {
            Widget class184 = (Widget) ((Object[]) obj)[1];
            boolean z = false;
            for (int i = 0; i < this.Field531.length; i++) {
                if (class184 == this.Field531[i]) {
                    z = true;
                }
            }
            if (!z) {
                this.Field532 = null;
                return;
            }
            PetItem class1044 = (PetItem) class184;
            if (class1044.name != null) {
                this.Field532 = ResourceManager.boldFont.wrap(class1044.name, BaseCanvas.w - 40);
                return;
            }
            return;
        }
        switch (Command.cmdId) {
            case 1:
                Method297(null);
                return;
            case 2:
                Widget Method315 = this.Field530.getFocusedWidget(true);
                if (Method315 == null || Method315 == this.Field530) {
                    return;
                }
                this.Field530.getFocusedWidget(true);
                Vector vector = new Vector();
                vector.addElement(new Command(3, T.gL(T.ENCHANT_STR), this));
                vector.addElement(new Command(4, T.gL(T.EVOLUTIONARY_STR), this));
                vector.addElement(new Command(5, T.gL(T.CLOSE_STR), this));
                showMenu(vector, 2);
                return;
            case 3:
                Widget Method3152 = this.Field530.getFocusedWidget(true);
                if (Method3152 == null || Method3152 == this.Field530 || (class1043 = (PetItem) this.Field530.getFocusedWidget(true)) == null) {
                    return;
                }
                this.Field533 = new Class122(2);
                this.Field533.Method168(0, class1043.itemId, class1043.imgPathId, class1043.name, class1043.Field684);
                showDialog(this.Field533, false);
                this.Field534 = class1043;
                return;
            case 4:
                Widget Method3153 = this.Field530.getFocusedWidget(true);
                if (Method3153 == null || Method3153 == this.Field530 || (class1042 = (PetItem) this.Field530.getFocusedWidget(true)) == null) {
                    return;
                }
                this.Field533 = new Class122(3);
                this.Field533.Method168(0, class1042.itemId, class1042.imgPathId, class1042.name, class1042.Field684);
                showDialog(this.Field533, false);
                this.Field534 = class1042;
                return;
            case 5:
                Widget Method3154 = this.Field530.getFocusedWidget(true);
                if (Method3154 == null || Method3154 == this.Field530 || (class104 = (PetItem) this.Field530.getFocusedWidget(true)) == null) {
                    return;
                }
                GameController.waitDialog();
                int i2 = class104.itemId;
                Message message = new Message(81);
                message.putByte(83);
                message.putInt(i2);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            default:
                super.actionPerformed(obj);
                return;
            case 17:
                this.Field530.Method79(this.Field530.Field692 == 0 ? 0 : this.Field530.Field692 - 1);
                return;
            case 18:
                this.Field530.Method79(this.Field530.Field692 == this.Field530.Field691 - 1 ? this.Field530.Field691 - 1 : this.Field530.Field692 + 1);
                return;
        }
    }

    public final void Method92(Message message) {
        if (this.Field533 == null) {
            return;
        }
        try {
            int readInt = message.reader().readInt();
            String readUTF = message.reader().readUTF();
            String readUTF2 = message.reader().readUTF();
            int readInt2 = message.reader().readInt();
            switch (this.Field533.Field781) {
                case 2:
                    int i = 1;
                    if (readInt2 == 12) {
                        i = 2;
                    }
                    this.Field533.Method168(i, readInt, readUTF, readUTF2, (byte) 0);
                    break;
                case 3:
                    this.Field533.Method168(1, readInt, readUTF, readUTF2, (byte) message.reader().readInt());
                    break;
            }
            showDialog(this.Field533, true);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public final void Method93(Message message) {
        try {
            int readInt = message.reader().readInt();
            int readInt2 = message.reader().readInt();
            String readUTF = message.reader().readUTF();
            int readInt3 = message.reader().readInt();
            if (this.Field533.Field781 == 2) {
                for (int i = 0; i < this.Field531.length; i++) {
                    if (this.Field531[i].itemId == readInt) {
                        this.Field531[i].name = readUTF;
                        this.Field531[i].imgPathId = String.valueOf(readInt2);
                        this.Field531[i].Method22((byte) readInt3);
                        PetItem[] class104Arr = this.Field531;
                    }
                }
            } else {
                PetItem class104 = new PetItem();
                class104.Method127(this.Field534);
                class104.itemId = readInt;
                class104.imgPathId = String.valueOf(readInt2);
                class104.name = readUTF;
                class104.Method22((byte) readInt3);
                class104.onFocusAction = this;
                PetItem[] class104Arr2 = new PetItem[this.Field531.length - 1];
                int i2 = 0;
                for (int i3 = 0; i3 < this.Field531.length; i3++) {
                    if (this.Field531[i3].itemId != this.Field533.Field782[0] && this.Field531[i3].itemId != this.Field533.Field782[1]) {
                        int i4 = i2;
                        i2++;
                        class104Arr2[i4] = this.Field531[i3];
                    }
                }
                class104Arr2[i2] = class104;
                this.Field531 = new PetItem[class104Arr2.length];
                System.arraycopy(class104Arr2, 0, this.Field531, 0, this.Field531.length);
                this.container.removeWidget(this.Field530);
                this.Field530.Method125(this.Field531);
                this.Field530.Method79(0);
                this.container.addWidget(this.Field530);
            }
            Method309();
            Class123 class123 = new Class123(true, 30);
            PetItem class1042 = new PetItem();
            class1042.itemId = readInt;
            class1042.imgPathId = String.valueOf(readInt2);
            class1042.Method22((byte) readInt3);
            class123.Method169(class1042, readUTF);
            showDialog(class123, false);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public final void Method94(Message message) {
        try {
            int readInt = message.reader().readInt();
            PetItem[] class104Arr = new PetItem[this.Field531.length - 1];
            int i = 0;
            for (int i2 = 0; i2 < this.Field531.length; i2++) {
                if (this.Field531[i2].itemId != readInt) {
                    int i3 = i;
                    i++;
                    class104Arr[i3] = this.Field531[i2];
                }
            }
            this.Field531 = new PetItem[class104Arr.length];
            System.arraycopy(class104Arr, 0, this.Field531, 0, this.Field531.length);
            this.container.removeWidget(this.Field530);
            this.Field530.Method125(this.Field531);
            this.Field530.Method79(0);
            this.container.addWidget(this.Field530);
            Method309();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public final void Method95(Message message) {
        try {
            int readInt = message.reader().readInt();
            String readUTF = message.reader().readUTF();
            String readUTF2 = message.reader().readUTF();
            byte readByte = message.reader().readByte();
            for (int i = 0; i < this.Field531.length; i++) {
                if (this.Field531[i].itemId == readInt) {
                    this.Field531[i].name = readUTF2;
                    this.Field531[i].imgPathId = readUTF;
                    this.Field531[i].Method22(readByte);
                    if (this.Field530.getFocusedWidget(true) == this.Field531[i]) {
                        this.Field531[i].onFocusAction.actionPerformed(new Object[]{null, this});
                        return;
                    }
                    return;
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

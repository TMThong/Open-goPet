package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.Button;
import vn.me.ui.geom.Rectangle;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Label;
import vn.me.ui.Widget;
import java.io.DataInputStream;


public final class mMapObject extends mObject implements IActionListener {
    private SceneManage Field70;
    private int type;
    private int Field72;
    private int Field73;
    private int Field74;
    private int Field75;
    private int Field76;
    private int Field77 = 0;
    private int Field78 = 1;
    private long Field79 = System.currentTimeMillis();

    public mMapObject() {
    }

    public static mMapObject Method383(SceneManage class140, DataInputStream dataInputStream) {
        mMapObject class19;
        try {
            byte readByte = dataInputStream.readByte();
            byte readByte2 = dataInputStream.readByte();
            short readShort = dataInputStream.readShort();
            short readShort2 = dataInputStream.readShort();
            byte readByte3 = dataInputStream.readByte();
            byte readByte4 = dataInputStream.readByte();
            byte readByte5 = dataInputStream.readByte();
            byte readByte6 = dataInputStream.readByte();
            byte readByte7 = dataInputStream.readByte();
            byte b = -1;
            byte b2 = 0;
            String name = "";
            byte b3 = 0;
            if (readByte != 0) {
                readByte2 = -1;
                b = dataInputStream.readByte();
                b2 = dataInputStream.readByte();
                name = dataInputStream.readUTF();
                b3 = dataInputStream.readByte();
            }
            class19 = new mMapObject(class140, readShort, readShort2, readByte3, readByte4, readByte5, readByte6, readByte7, readByte2, b, b2, name, b3);
            return class19;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    private mMapObject(SceneManage class140, int i, int i2, int i3, int i4, int i5, int i6, int i7, byte type_, int i8, int i9, String name_, int i10) {
        mMapObject class19;
        String str2;
        this.Field70 = class140;
        this.type = type_;
        if (this.type != -1) {
            class19 = this;
            switch (this.type) {
                case 0:
                    str2 = "Nhà hẻm";
                    break;
                case 1:
                    str2 = "Nhà mặt tiền";
                    break;
                case 2:
                    str2 = "Biệt thự";
                    break;
                case 3:
                    str2 = "Dinh thự";
                    break;
                case 4:
                    str2 = "Nhà";
                    break;
                case 5:
                default:
                    str2 = "";
                    break;
                case 6:
                    str2 = "Thú cưng";
                    break;
                case 7:
                    str2 = "Vườn";
                    break;
                case 8:
                    str2 = "Phòng vé";
                    break;
                case 9:
                    str2 = "";
                    break;
                case 10:
                    str2 = "Cà phê";
                    break;
                case 11:
                    str2 = "Khu";
                    break;
                case 12:
                    str2 = "Hộp thư";
                    break;
                case 13:
                    str2 = "Caro";
                    break;
                case 14:
                    str2 = "Cờ tướng";
                    break;
                case 15:
                    str2 = "Tiến lên";
                    break;
                case 16:
                    str2 = "Phỏm";
                    break;
                case 17:
                    str2 = "Thời trang";
                    break;
                case 18:
                    str2 = "Nón";
                    break;
                case 19:
                    str2 = "Giày";
                    break;
                case 20:
                    str2 = "Mỹ viện";
                    break;
                case 21:
                    str2 = "Tóc";
                    break;
                case 22:
                    str2 = "Vật phẩm";
                    break;
                case 23:
                    str2 = "Gara";
                    break;
                case 24:
                    str2 = "Trò chơi trong nhà";
                    break;
                case 25:
                    str2 = "Pet shop";
                    break;
                case 26:
                    str2 = "Đấu trường";
                    break;
                case 27:
                    str2 = "";
                    break;
                case 28:
                    str2 = "";
                    break;
                case 29:
                    str2 = "";
                    break;
                case 30:
                    str2 = "";
                    break;
                case 31:
                    str2 = "";
                    break;
                case 32:
                    str2 = "";
                    break;
            }
        } else {
            this.Field872 = true;
            class19 = this;
            str2 = name_;
        }
        class19.name = str2;
        this.xChar = i;
        this.yChar = i2;
        this.Field76 = i3;
        this.Field72 = i6;
        this.Field73 = i7;
        this.Field74 = i8;
        this.Field75 = i9;
        setCollisionRec(new Rectangle(i4, i5, this.Field72, this.Field73));
        this.centerObjectCMD = new Command("Chọn", this);
    }

    public final void paintInMap(int i, int i2) {
        long currentTimeMillis = System.currentTimeMillis();
        if (currentTimeMillis - this.Field79 > 50) {
            this.Field79 = currentTimeMillis;
            this.Field77 += this.Field78;
            if (this.Field77 < -1 || this.Field77 > 2) {
                this.Field78 = -this.Field78;
            }
        }
        int height = ((((-i2) + this.yChar) - this.Field76) - GameResourceManager.Field601.getHeight()) - this.Field873;
        if (this.type == -1) {
            GameResourceManager.Method116().drawString(BaseCanvas.g, this.name, this.xChar - i, height + 20, 3);
        } else {
            GameResourceManager.Method116().drawString(BaseCanvas.g, this.name, this.xChar - i, height - 23, 3);
        }
    }

    public final void paintObj(int i, int i2) {
        long currentTimeMillis = System.currentTimeMillis();
        if (currentTimeMillis - this.Field79 > 50) {
            this.Field79 = currentTimeMillis;
            this.Field77 += this.Field78;
            if (this.Field77 < -1 || this.Field77 > 2) {
                this.Field78 = -this.Field78;
            }
        }
        if (this.type != -1) {
            BaseCanvas.g.drawImage(GameResourceManager.Field601, this.xChar - i, ((((-i2) + this.yChar) - this.Field76) - GameResourceManager.Field601.getHeight()) - this.Field873, 17);
        }
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (this.type) {
            case -1:
                SceneManage.warp(this.Field74, this.Field75, mMap.getMapVersion(this.Field74));
                return;
            case 0:
                this.Field70.actionPerformed(new Object[]{new Command(2, "", this.Field70)});
                return;
            case 1:
                this.Field70.actionPerformed(new Object[]{new Command(3, "", this.Field70)});
                return;
            case 2:
                this.Field70.actionPerformed(new Object[]{new Command(4, "", this.Field70)});
                return;
            case 3:
                this.Field70.actionPerformed(new Object[]{new Command(5, "", this.Field70)});
                return;
            case 4:
                this.Field70.actionPerformed(new Object[]{new Command(502, "", this.Field70)});
                return;
            case 5:
            case 6:
            case 24:
            case 25:
            default:
                return;
            case 7:
                this.Field70.actionPerformed(new Object[]{new Command(10, "", this.Field70)});
                return;
            case 8:
                SceneManage class140 = this.Field70;
                SceneManage.Method224(this, new Widget[]{new Label(ActorFactory.gL(574)), new Button(new Command(1202, ActorFactory.gL(134), this.Field70)), new Button(new Command(1203, ActorFactory.gL(471), this.Field70))});
                return;
            case 9:
                GameController.instance.actionPerformed(new Object[]{new Command(18, "", GameController.instance)});
                return;
            case 10:
                this.Field70.actionPerformed(new Object[]{new Command(2019, "", this.Field70)});
                return;
            case 11:
                this.Field70.actionPerformed(new Object[]{new Command(1103, "", this.Field70)});
                return;
            case 12:
                this.Field70.actionPerformed(new Object[]{new Command(2020, "", this.Field70)});
                return;
            case 13:
                this.Field70.actionPerformed(new Object[]{new Command(602, "", this.Field70)});
                return;
            case 14:
                this.Field70.actionPerformed(new Object[]{new Command(603, "", this.Field70)});
                return;
            case 15:
                this.Field70.actionPerformed(new Object[]{new Command(606, "", this.Field70)});
                return;
            case 16:
                this.Field70.actionPerformed(new Object[]{new Command(607, "", this.Field70)});
                return;
            case 17:
                GameController.waitDialog();
                Message message = new Message(81);
                message.putByte(60);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 18:
                this.Field70.actionPerformed(new Object[]{new Command(1003, "", this.Field70)});
                return;
            case 19:
                this.Field70.actionPerformed(new Object[]{new Command(1004, "", this.Field70)});
                return;
            case 20:
                this.Field70.actionPerformed(new Object[]{new Command(1005, "", this.Field70)});
                return;
            case 21:
                this.Field70.actionPerformed(new Object[]{new Command(1006, "", this.Field70)});
                return;
            case 22:
                this.Field70.actionPerformed(new Object[]{new Command(1009, "", this.Field70)});
                return;
            case 23:
                this.Field70.actionPerformed(new Object[]{new Command(1008, "", this.Field70)});
                return;
            case 26:
                GameController.waitDialog();
                Message message2 = new Message(81);
                message2.putByte(58);
                message2.putByte(0);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                return;
            case 27:
                GameController.waitDialog();
                MEService.switchToMe(1);
                return;
            case 28:
                GameController.waitDialog();
                MEService.switchToMe(2);
                return;
            case 29:
                GameController.waitDialog();
                MEService.switchToMe(3);
                return;
            case 30:
                GameController.waitDialog();
                MEService.switchToMe(4);
                return;
            case 31:
                GameController.waitDialog();
                Message message3 = new Message(81);
                message3.putByte(21);
                GlobalService.session.sendMessage(message3);
                message3.cleanup();
                return;
            case 32:
                PetGameModel class43 = (PetGameModel) GlobalMessageHandler.Method208().Field985;
                if (class43.myPet == null) {
                    GameController.startOKDlg("Bạn không dẫn theo pet");
                    return;
                }
                PetGameModel.Field285 = 1;
                GameController.waitDialog();
                MEService.Method104(class43.myPet.petTemplateId);
                return;
        }
    }
}

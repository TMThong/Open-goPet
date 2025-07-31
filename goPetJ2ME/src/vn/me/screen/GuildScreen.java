package vn.me.screen;

import defpackage.ActorFactory;
import defpackage.MEService;
import defpackage.Class70;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import defpackage.MenuItemInfo;
import defpackage.PetGameModel;
import defpackage.SceneManage;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.Button;
import vn.me.ui.InputDialog;
import vn.me.ui.common.ResourceManager;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.WidgetGroup;
import vn.me.ui.Widget;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

public final class GuildScreen extends MenuScreen {

    public int Field540;
    private int Field541;
    private String Field542;
    private boolean Field543;
    private String Field544;
    private byte Field545;
    private byte Field546;
    private Image Field547;
    private String Field548;
    private Object Field549;

    public GuildScreen() {
        this.screenId = "SCREEN_GUILD";
        this.title = T.gL(T.CLAN);
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.cmdLeft = GameController.Field467;
    }

    private Class70 Method96() {
        Widget Method315 = this.Field17.getFocusedWidget(true);
        if (Method315 != null) {
            return (Class70) Method315;
        }
        return null;
    }

    public final void paintBackground() {
        super.paintBackground();
        if ((this.Field540 & 2) != 0) {
            LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
            PetGameModel.Method461(T.gL(T.GENERAL_INFORMATION), 2, BaseCanvas.w >> 1, 22);
            int i = 20 + 30;
            LAF.Method413(10, 50, BaseCanvas.w - 20, (BaseCanvas.h - 50) - 25);
            String[] list = (String[]) this.Field549;
            for (int j = 0; j < list.length; j++) {
                 String str = list[j];
                 i += ResourceManager.boldFont.charHeight + 4;
                ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(str) ").append(str).toString(), 20, i, 0);
            }
        } else if ((this.Field540 & 32) != 0) {
            Object[] objArr = (Object[]) this.Field549;
            String obj = objArr[0].toString();
            String[] strArr = objArr[1] != null ? (String[]) objArr[1] : null;
            BaseCanvas.g.setColor(0);
            BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
            LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
            PetGameModel.Method461(obj, 2, BaseCanvas.w >> 1, 22);
            LAF.Method413(10, 42, 64, 80);
            LAF.Method413(74, 42, (BaseCanvas.w - 20) - 64, 80);
            ResourceManager.boldFont.drawString(BaseCanvas.g, strArr == null ? "(str)Điểm vinh dự: 0" : strArr[0], 90, 50, 0);
            int i2 = 50 + 18;
            ResourceManager.boldFont.drawString(BaseCanvas.g, strArr == null ? "(str)Đóng đóng góp: 0" : strArr[1], 90, 68, 0);
            LAF.Method413(10, 122, BaseCanvas.w - 20, ((BaseCanvas.h - 122) - 5) - LAF.Field1293);
            if (this.Field547 == null) {
                this.Field547 = GameResourceManager.loadResourceImg(this.Field548, (byte) 3);
            }
            int i3 = i2 + 50;
            if (this.Field547 != null) {
                BaseCanvas.g.drawImage(this.Field547, 42, 115, 33);
            }
            if (strArr == null) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, "(str) Chưa vào bang hội.", 20, 118 + ResourceManager.boldFont.charHeight + 4, 0);
                return;
            }
            for (int i4 = 2; i4 < strArr.length; i4++) {
                i3 += ResourceManager.boldFont.charHeight + 4;
                ResourceManager.boldFont.drawString(BaseCanvas.g, strArr[i4], 20, i3, 0);
            }
        }
    }

    ///////@Override // defpackage.Class6, defpackage.Screen, defpackage.Class200
    public final void actionPerformed(Object obj) {
        Class70 Method96;
        Class70 Method962;
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 1:
                Vector vector = new Vector();
                vector.addElement(new Command(3, T.gL(T.GENERAL_INFORMATION), this));
                vector.addElement(new Command(4, "Thông tin bang chúng", this));
                vector.addElement(new Command(5, "Top nộp quỹ", this));
                vector.addElement(new Command(6, "Top điểm phát triển", this));
                showMenu(vector, 0);
                return;
            case 2:
            default:
                if ((this.Field540 & 1) != 0) {
                    switch (Command.cmdId) {
                        case 2:
                            if (!this.Field543 || ((MenuItemInfo) Command.objPerfomed).title.trim().toUpperCase().equals(SceneManage.myCharacter.nameUpperCase) || (Method962 = Method96()) == null) {
                                return;
                            }
                            Command Command2 = new Command(9, T.gL(T.KICK_MEMBER), this);
                            Vector vector2 = new Vector();
                            Command2.objPerfomed = Method962.model;
                            vector2.addElement(Command2);
                            showMenu(vector2, 2);
                            return;
                        case 9:
                            if (Command.objPerfomed != null) {
                                Dialog.Method7();
                                int i = ((MenuItemInfo) Command.objPerfomed).menuId;
                                Message message = new Message(81);
                                message.putByte(91);
                                message.putByte(6);
                                message.putInt(i);
                                GlobalService.session.sendMessage(message);
                                message.cleanup();
                                return;
                            }
                            return;
                        case 13:
                            if (this.Field545 > 1) {
                                Dialog.Method7();
                                MEService.switchToMe(this.Field545 - 1, false);
                                return;
                            }
                            return;
                        case 14:
                            if (this.Field545 < this.Field546) {
                                Dialog.Method7();
                                MEService.switchToMe(this.Field545 + 1, false);
                                return;
                            }
                            return;
                        default:
                            return;
                    }
                } else if ((this.Field540 & 4) != 0) {
                    switch (Command.cmdId) {
                        case 2:
                            if (this.Field544 != null || (Method96 = Method96()) == null || Method96.model == null) {
                                return;
                            }
                            Command Command3 = new Command(7, T.gL(T.ACCEPT), this);
                            Command3.objPerfomed = Method96.model;
                            Dialog.Method45(new StringBuffer("Bạn muốn gia nhập: ").append(((MenuItemInfo) Method96.model).title).toString(), Command3, GameController.Field464);
                            return;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        default:
                            return;
                        case 7:
                            if (Command.objPerfomed == null) {
                                Dialog.Method40("Không tìm thấy bang hội", true);
                                return;
                            }
                            Dialog.Method7();
                            int i2 = ((MenuItemInfo) Command.objPerfomed).menuId;
                            Message message2 = new Message(81);
                            message2.putByte(91);
                            message2.putByte(2);
                            message2.putInt(i2);
                            GlobalService.session.sendMessage(message2);
                            message2.cleanup();
                            return;
                        case 13:
                            if (this.Field545 > 1) {
                                Dialog.Method7();
                                MEService.Method144(this.Field545 - 1);
                                return;
                            }
                            return;
                        case 14:
                            if (this.Field545 < this.Field546) {
                                Dialog.Method7();
                                MEService.Method144(this.Field545 + 1);
                                return;
                            }
                            return;
                        case 15:
                            Dialog.Method57(T.gL(T.FIND_CLAN), new Command(16, T.gL(T.OK), this), GameController.Field464, 0);
                            return;
                        case 16:
                            if (Screen.currentDialog == null || !(Screen.currentDialog instanceof InputDialog)) {
                                return;
                            }
                            String Method327 = ((InputDialog) Screen.currentDialog).Method327(0);
                            if (Method327.length() != 0) {
                                Message message3 = new Message(81);
                                message3.putByte(91);
                                message3.putByte(13);
                                message3.putString(Method327);
                                GlobalService.session.sendMessage(message3);
                                message3.cleanup();
                                return;
                            }
                            return;
                    }
                } else if ((this.Field540 & 8) != 0) {
                    switch (Command.cmdId) {
                        case 2:
                            Class70 Method963 = Method96();
                            if (Method963 != null) {
                                Vector vector3 = new Vector();
                                Command Command4 = new Command(7, ActorFactory.gL(ActorFactory.ACCEPT_STR), this);
                                Command Command5 = new Command(8, ActorFactory.gL(ActorFactory.REFUSE_STR), this);
                                Command4.objPerfomed = Method963.model;
                                Command5.objPerfomed = Method963.model;
                                vector3.addElement(Command4);
                                vector3.addElement(Command5);
                                showMenu(vector3, 2);
                                return;
                            }
                            return;
                        case 7:
                            if (Command.objPerfomed != null) {
                                Dialog.Method7();
                                int i3 = ((MenuItemInfo) Command.objPerfomed).menuId;
                                Message message4 = new Message(81);
                                message4.putByte(91);
                                message4.putByte(5);
                                message4.putInt(i3);
                                message4.putBoolean(true);
                                GlobalService.session.sendMessage(message4);
                                message4.cleanup();
                                return;
                            }
                            return;
                        case 8:
                            if (Command.objPerfomed != null) {
                                Dialog.Method7();
                                int i4 = ((MenuItemInfo) Command.objPerfomed).menuId;
                                Message message5 = new Message(81);
                                message5.putByte(91);
                                message5.putByte(5);
                                message5.putInt(i4);
                                message5.putBoolean(false);
                                GlobalService.session.sendMessage(message5);
                                message5.cleanup();
                                return;
                            }
                            return;
                        default:
                            return;
                    }
                } else if ((this.Field540 & 16) != 0) {
                    switch (Command.cmdId) {
                        case 2:
                            Class70 Method964 = Method96();
                            if (Method964 != null) {
                                MenuItemInfo class5 = (MenuItemInfo) Method964.model;
                                Command Command6 = new Command(8, T.gL(T.OK), this);
                                Command6.objPerfomed = class5;
                                Dialog.Method276(new StringBuffer(T.gL(T.DO_YOU_WANT_FUND_CLAN)).append(class5.title).toString(), null, Command6, GameController.Field464, true);
                                return;
                            }
                            return;
                        case 7:
                            Method297(null);
                            return;
                        case 8:
                            if (Command.objPerfomed != null) {
                                int i5 = ((MenuItemInfo) Command.objPerfomed).menuId;
                                Message message6 = new Message(81);
                                message6.putByte(91);
                                message6.putByte(10);
                                message6.putInt(i5);
                                GlobalService.session.sendMessage(message6);
                                message6.cleanup();
                                Method297(null);
                                return;
                            }
                            return;
                        default:
                            return;
                    }
                } else if ((this.Field540 & 64) != 0) {
                    switch (Command.cmdId) {
                        case 13:
                            if (this.Field545 > 1) {
                                Dialog.Method7();
                                MEService.Method160(this.Field541, this.Field545 - 1);
                                return;
                            }
                            return;
                        case 14:
                            if (this.Field545 < this.Field546) {
                                Dialog.Method7();
                                MEService.Method160(this.Field541, this.Field545 + 1);
                                return;
                            }
                            return;
                        default:
                            return;
                    }
                } else if ((this.Field540 & 128) != 0) {
                    switch (Command.cmdId) {
                        case 13:
                            if (this.Field545 > 1) {
                                Dialog.Method7();
                                MEService.Method162(this.Field541, this.Field545 - 1);
                                return;
                            }
                            return;
                        case 14:
                            if (this.Field545 < this.Field546) {
                                Dialog.Method7();
                                MEService.Method162(this.Field541, this.Field545 + 1);
                                return;
                            }
                            return;
                        default:
                            return;
                    }
                } else if ((this.Field540 & 256) != 0) {
                    switch (Command.cmdId) {
                        case 2:
                            if (this.Field543) {
                                Class70 Method965 = Method96();
                                Command Command7 = new Command(7, T.gL(T.OK), this);
                                MenuItemInfo class52 = (MenuItemInfo) Method965.model;
                                Command7.objPerfomed = class52;
                                Dialog.Method45(new StringBuffer("Bạn chắc chắn muốn nhường quyền bang chủ cho ").append(class52.title).toString(), Command7, GameController.Field464);
                                return;
                            }
                            return;
                        case 7:
                            if (Command.objPerfomed != null) {
                                MenuItemInfo class53 = (MenuItemInfo) Command.objPerfomed;
                                int i6 = this.Field541;
                                int i7 = class53.menuId;
                                Message message7 = new Message(81);
                                message7.putByte(91);
                                message7.putByte(12);
                                message7.putInt(i6);
                                message7.putInt(i7);
                                GlobalService.session.sendMessage(message7);
                                message7.cleanup();
                                return;
                            }
                            return;
                        case 13:
                            if (this.Field545 > 1) {
                                Dialog.Method7();
                                MEService.switchToMe(this.Field545 - 1, true);
                                return;
                            }
                            return;
                        case 14:
                            if (this.Field545 < this.Field546) {
                                Dialog.Method7();
                                MEService.switchToMe(this.Field545 + 1, true);
                                return;
                            }
                            return;
                        default:
                            return;
                    }
                } else {
                    return;
                }
            case 3:
                Dialog.Method7();
                MEService.Method0();
                return;
            case 4:
                Dialog.Method7();
                MEService.switchToMe(1, false);
                return;
            case 5:
                Dialog.Method7();
                MEService.Method162(this.Field541, 1);
                return;
            case 6:
                Dialog.Method7();
                MEService.Method160(this.Field541, 1);
                return;
        }
    }

    public final void Method97(byte b, byte b2, int i, boolean z, int i2, String str, int i3, int i4, Vector vector, boolean z2) {
        if (z2) {
            this.Field540 = 256;
        } else {
            this.Field540 = 1;
        }
        removeWidget(this.Field17);
        this.Field541 = i;
        this.Field543 = z;
        this.Field542 = str;
        this.Field544 = str;
        this.Field545 = b;
        this.Field546 = b2;
        setMenuItemList(vector);
        Method6();
        this.cmdRight = new Command(1, "menu", this);
        this.title = this.Field542;
        if (this.Field17.count() > 0) {
            this.Field17.getWidgetAt(0).requestFocus();
        }
    }

    public final void Method98(int i, String[] strArr) {
        this.Field540 = 2;
        this.Field541 = i;
        removeWidget(this.Field17);
        this.Field549 = strArr;
        this.title = null;
        this.cmdRight = new Command(1, "menu", this);
        this.cmdCenter = null;
        this.cmdLeft = GameController.Field467;
    }

    public final void Method59(Vector vector) {
        this.Field540 = 8;
        removeWidget(this.Field17);
        setMenuItemList(vector);
        if (this.Field17.count() > 0) {
            this.Field17.getWidgetAt(0).requestFocus();
        }
        this.cmdLeft = GameController.Field467;
        this.cmdRight = null;
    }

    public final void Method99(String str, Vector vector, byte b, byte b2) {
        this.Field540 = 4;
        removeWidget(this.Field17);
        this.Field544 = str;
        this.Field543 = false;
        this.Field542 = null;
        this.cmdRight = null;
        this.Field545 = b;
        this.Field546 = b2;
        setMenuItemList(vector);
        Method6();
        if (this.Field17.count() > 0) {
            this.Field17.getWidgetAt(0).requestFocus();
        }
        this.cmdRight = new Command(15, T.gL(T.FIND), this);
    }

    public final void Method100(byte b, byte b2, Vector vector) {
        this.Field540 = 64;
        removeWidget(this.Field17);
        this.title = "Top điểm phát triển";
        this.Field545 = b;
        this.Field546 = b2;
        setMenuItemList(vector);
        Method6();
        this.cmdRight = new Command(1, "menu", this);
        this.cmdCenter = null;
        this.cmdLeft = GameController.Field467;
        if (this.Field17.count() > 0) {
            this.Field17.getWidgetAt(0).requestFocus();
        }
    }

    public final void Method101(byte b, byte b2, Vector vector) {
        this.Field540 = 128;
        removeWidget(this.Field17);
        this.title = "Top nộp quỹ";
        this.Field545 = b;
        this.Field546 = b2;
        setMenuItemList(vector);
        Method6();
        this.cmdRight = new Command(1, "menu", this);
        this.cmdCenter = null;
        this.cmdLeft = GameController.Field467;
        if (this.Field17.count() > 0) {
            this.Field17.getWidgetAt(0).requestFocus();
        }
    }

    public final void Method102(Vector vector) {
        this.Field540 = 16;
        removeWidget(this.Field17);
        setMenuItemList(vector);
        this.cmdLeft = new Command(7, T.gL(T.STOP), this);
        this.cmdRight = null;
        this.cmdCenter = null;
        if (this.Field17.count() > 0) {
            this.Field17.getWidgetAt(0).requestFocus();
        }
    }

    public final void Method103(boolean z, int i, String str, String str2, Object obj) {
        this.Field540 = 32;
        removeWidget(this.Field17);
        this.title = null;
        this.Field548 = str2;
        this.Field549 = new Object[]{str, obj};
        if (z) {
            this.cmdRight = new Command("Mời gia nhập", new IActionListener() {
                ///////@Override
                public void actionPerformed(Object obj) {
                    int intValue = ((Integer) ((Command) ((Object[]) obj)[0]).objPerfomed).intValue();
                    Message message = new Message(81);
                    message.putByte(91);
                    message.putByte(18);
                    message.putInt(intValue);
                    GlobalService.session.sendMessage(message);
                    message.cleanup();
                    Dialog.Method158("Đã gửi lời mời gia nhập bang.");
                }
            });
            this.cmdRight.objPerfomed = new Integer(i);
        } else {
            this.cmdRight = null;
        }
        this.cmdLeft = GameController.Field467;
    }

    private void Method6() {
        WidgetGroup class185 = new WidgetGroup(0, 0, BaseCanvas.w, LAF.Field1293);
        if (this.Field545 > 1) {
            Button class165 = new Button("<<<");
            class165.align = 17;
            class165.setMetrics(LAF.LOT_PADDING, LAF.LOT_PADDING, 30, LAF.Field1293);
            class165.cmdCenter = new Command(13, T.gL(T.GO_BACK), this);
            class185.addWidget(class165);
        }
        if (this.Field545 < this.Field546) {
            Button class1652 = new Button(">>>");
            class1652.align = 17;
            class1652.setMetrics((BaseCanvas.w - 30) - LAF.LOT_PADDING, LAF.LOT_PADDING, 30, LAF.Field1293);
            class1652.cmdCenter = new Command(14, T.gL(T.NEXT), this);
            class185.addWidget(class1652);
        }
        this.Field17.addWidget(class185);
        this.Field17.setViewMode(1);
    }

    public final void Method104(int i) {
        int Method351 = this.Field17.count();
        while (true) {
            Method351--;
            if (Method351 < 0) {
                return;
            }
            Widget Method350 = this.Field17.getWidgetAt(Method351);
            if (Method350 != null && (Method350 instanceof Class70) && ((MenuItemInfo) ((Class70) Method350).model).menuId == i) {
                this.Field17.removeWidget(Method350);
                this.Field17.setViewMode(1);
                if (this.Field17.count() > 0) {
                    this.Field17.getWidgetAt(0).requestFocus();
                    return;
                }
                return;
            }
        }
    }
}

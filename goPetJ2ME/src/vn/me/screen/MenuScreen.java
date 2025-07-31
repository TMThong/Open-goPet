package vn.me.screen;

import defpackage.ActorFactory;
import vn.me.core.BaseCanvas;
import vn.me.ui.InputDialog;
import vn.me.ui.common.ResourceManager;
import defpackage.Class70;
import defpackage.Command;
import vn.me.ui.Dialog;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import defpackage.MEService;
import vn.me.ui.common.LAF;
import defpackage.MenuItemInfo;
import vn.me.network.Message;
import vn.me.ui.Widget;
import vn.me.ui.WidgetGroup;
import vn.me.screen.Screen;
import java.util.Vector;
import vn.me.ui.common.T;
import vn.me.ui.interfaces.IActionListener;

/* renamed from: Class6  reason: default package */
 /* loaded from: gopet_repackage.jar:Class6.class */
public class MenuScreen extends Screen implements IActionListener {

    public byte menuType;
    public int menuId;
    protected WidgetGroup Field17;
    private MenuItemInfo[] Field18;
    private InputDialog Field19;

    public MenuScreen() {
        super(true);
        this.menuType = (byte) -1;
        this.cmdLeft = new Command(1, ActorFactory.gL(64), this);
    }

    public final void setMenuItemList(Vector vector) {
        this.container.removeWidget(this.Field17);
        this.Field18 = new MenuItemInfo[vector.size()];
        if (this.Field17 == null) {
            this.Field17 = new WidgetGroup(0, LAF.Field1292, BaseCanvas.w, BaseCanvas.h - (2 * LAF.Field1292));
            this.Field17.isScrollableY = true;
            this.Field17.isLoop = true;
        }
        this.Field17.removeAll();
        int size = vector.size();
        for (int i = 0; i < size; i++) {
            MenuItemInfo class5 = (MenuItemInfo) vector.elementAt(i);
            this.Field18[i] = class5;
            Class70 class70 = new Class70(class5, 0, 0, this.Field17.w, LAF.Field1291 + (LAF.LOT_PADDING << 1));
            class70.descriptionFont = GameResourceManager.smallFont;
            class70.focusDescFont = GameResourceManager.smallFont;
            class70.normalFont = ResourceManager.boldFont;
            if (class5.canSelect) {
                class70.cmdCenter = new Command(2, ActorFactory.gL(419), class5, this);
            } else {
                class70.cmdCenter = null;
            }
            this.Field17.addWidget(class70, false);
        }
        this.Field17.spacing = 0;
        this.container.addWidget(this.Field17);
        this.Field17.setViewMode(1);
        this.Field17.setFocusWithParents(true);
    }

    ///////@Override // defpackage.Screen
    public void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    ///////@Override // defpackage.Screen, defpackage.Class200
    public void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 1:
                Method297(null);
                return;
            case 2:
                MenuItemInfo class5 = (MenuItemInfo) Command.objPerfomed;
                if (class5.Field7) {
                    GameController.Method69(class5.title, class5.Field8, null, new Command(3, class5.Field9, class5, this), GameController.Field464, true);
                    return;
                }
                GlobalService.Method267(this.menuId, class5.menuId);
                if (class5.Field11) {
                    Method297(null);
                    return;
                }
                return;
            case 3:
                Screen.hideDialog(Screen.currentDialog);
                MenuItemInfo class52 = (MenuItemInfo) Command.objPerfomed;
                switch (this.menuType) {
                    case 1:
                    case 3:
                        Command[] CommandArr = new Command[class52.Field12.length];
                        for (int i = 0; i < CommandArr.length; i++) {
                            if (class52.Field14[i] != 0) {
                                CommandArr[i] = new Command(4, T.gL(T.OK), new int[]{class52.menuId, class52.Field13[i]}, this);
                            }
                        }
                        GameController.Method63(0, T.gL(T.PAYMENT), class52.Field13, class52.Field12, class52.Field14, CommandArr).show(true);
                        return;
                    case 2:
                        GlobalService.Method267(this.menuId, class52.menuId);
                        if (class52.Field11) {
                            Method297(null);
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 4:
                Screen.hideDialog(Screen.currentDialog);
                GameController.waitDialog();
                int[] iArr = (int[]) Command.objPerfomed;
                int i2 = this.menuId;
                int i3 = iArr[0];
                int i4 = iArr[1];
                Message message = new Message(122);
                message.putByte(9);
                message.putInt(i2);
                message.putByte(2);
                message.putInt(i3);
                message.putInt(i4);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 5:
                Message message2 = new Message(81);
                message2.putByte(74);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                GameController.waitDialog();
                return;
            case 6:
                GameController.waitDialog();
                Message message3 = new Message(81);
                message3.putByte(92);
                message3.putByte(5);
                GlobalService.session.sendMessage(message3);
                message3.cleanup();
                return;
            case 7:
                Integer num = (Integer) Command.objPerfomed;
                Vector vector = new Vector();
                vector.addElement(new Command(8, T.gL(T.USE), num, this));
                vector.addElement(new Command(9, T.gL(T.UPGRADE_STR), num, this));
                showMenu(vector, 2);
                return;
            case 8:
                int i5 = this.Field18[((Integer) Command.objPerfomed).intValue()].menuId;
                Message message4 = new Message(81);
                message4.putByte(92);
                message4.putByte(4);
                message4.putInt(i5);
                GlobalService.session.sendMessage(message4);
                message4.cleanup();
                GameController.waitDialog();
                return;
            case 9:
                int i6 = this.Field18[((Integer) Command.objPerfomed).intValue()].menuId;
                Message message5 = new Message(81);
                message5.putByte(92);
                message5.putByte(6);
                message5.putInt(i6);
                GlobalService.session.sendMessage(message5);
                message5.cleanup();
                GameController.waitDialog();
                return;
            case 10:
                int Method352 = this.Field17.getFocusedIndex();
                if (Method352 != -1) {
                    int i7 = this.Field18[Method352].menuId;
                    Message message6 = new Message(81);
                    message6.putByte(99);
                    message6.putInt(i7);
                    GlobalService.session.sendMessage(message6);
                    message6.cleanup();
                    GameController.waitDialog();
                    return;
                }
                return;
            case 11:
                int Method3522 = this.Field17.getFocusedIndex();
                if (Method3522 != -1) {
                    GameController.Method45(T.gL(T.ASK_ADD_FRIEND), new Command(111, T.gL(T.YES), new Integer(Method3522), this), GameController.Field464);
                    return;
                }
                return;
            case 12:
                this.Field19 = Dialog.Method57(T.gL(T.SEND_LETTER), new Command(121, T.gL(T.SEND_STR), new Integer(this.Field18[((Integer) Command.objPerfomed).intValue()].menuId), this), GameController.Field464, 0);
                return;
            case 13:
                Vector vector2 = new Vector();
                vector2.addElement(new Command(131, T.gL(T.REFUSE), this));
                vector2.addElement(new Command(132, T.gL(T.BLOCK), this));
                showMenu(vector2, 0);
                return;
            case 14:
                MenuItemInfo class53 = this.Field18[this.Field17.getFocusedIndex()];
                GameController.waitDialog();
                int i8 = class53.menuId;
                Message message7 = new Message(121);
                message7.putByte(5);
                message7.putInt(i8);
                GlobalService.session.sendMessage(message7);
                message7.cleanup();
                return;
            case 15:
                MenuItemInfo class54 = this.Field18[this.Field17.getFocusedIndex()];
                GameController.waitDialog();
                int i9 = class54.menuId;
                Message message8 = new Message(121);
                message8.putByte(12);
                message8.putInt(i9);
                GlobalService.session.sendMessage(message8);
                message8.cleanup();
                return;
            case 16:
                this.Field19 = Dialog.Method57(T.gL(T.TYPING_NAME), new Command(161, T.gL(T.OK), this), GameController.Field464, 0);
                return;
            case 111:
                MenuItemInfo class55 = this.Field18[((Integer) Command.objPerfomed).intValue()];
                Method309();
                GameController.waitDialog();
                int i10 = class55.menuId;
                Message message9 = new Message(121);
                message9.putByte(7);
                message9.putInt(i10);
                GlobalService.session.sendMessage(message9);
                message9.cleanup();
                return;
            case 121:
                this.Field19.Method274();
                int intValue = ((Integer) Command.objPerfomed).intValue();
                GameController.waitDialog();
                String Method327 = this.Field19.Method327(0);
                Message message10 = new Message(121);
                message10.putByte(14);
                message10.putInt(intValue);
                message10.putString(Method327);
                GlobalService.session.sendMessage(message10);
                message10.cleanup();
                return;
            case 131:
                MenuItemInfo class56 = this.Field18[this.Field17.getFocusedIndex()];
                GameController.waitDialog();
                int i11 = class56.menuId;
                Message message11 = new Message(121);
                message11.putByte(6);
                message11.putInt(i11);
                GlobalService.session.sendMessage(message11);
                message11.cleanup();
                return;
            case 132:
                MenuItemInfo class57 = this.Field18[this.Field17.getFocusedIndex()];
                GameController.waitDialog();
                int i12 = class57.menuId;
                Message message12 = new Message(121);
                message12.putByte(9);
                message12.putInt(i12);
                GlobalService.session.sendMessage(message12);
                message12.cleanup();
                return;
            case 161:
                GameController.waitDialog();
                String trim = this.Field19.Method327(0).trim();
                Message message13 = new Message(121);
                message13.putByte(11);
                message13.putString(trim);
                GlobalService.session.sendMessage(message13);
                message13.cleanup();
                this.Field19.Method274();
                return;
            case 162: {
                MEService.AdminGetItem();
            }
            return;
            case 163: {
                MEService.AdminGiveItem();
            }
            return;
            default:
                return;
        }
    }

    public final void setMenuId(int i) {
        this.menuId = i;
        this.cmdRight = null;
        this.cmdCenter = null;
        switch (this.menuId) {
            case 1049:
                this.cmdRight = new Command(162, "Lấy", this);
                return;
            case 1050:
                this.cmdRight = new Command(163, "Đưa", this);
                return;
            case 81004:
                this.cmdRight = new Command(5, T.gL(T.JADE_MANAGEMENT), this);
                return;
            case 81028:
                this.cmdRight = new Command(10, T.gL(T.VIEW_TATTOOS), this);
                return;
            case 81040:
                this.cmdRight = new Command(6, T.gL(T.REMOVE_THE_WINGS), this);
                for (int i2 = 0; i2 < this.Field17.children.length; i2++) {
                    Widget class184 = this.Field17.children[i2];
                    Command Command = new Command(7, T.gL(T.SELECT_STR), new Integer(i2), this);
                    this.cmdCenter = Command;
                    class184.cmdCenter = Command;
                }
                return;
            case 81087:
                this.cmdRight = new Command(11, T.gL(T.UNFRIEND), this);
                for (int i3 = 0; i3 < this.Field17.children.length; i3++) {
                    Widget class1842 = this.Field17.children[i3];
                    Command Command2 = new Command(12, T.gL(T.SEND_LETTER), new Integer(i3), this);
                    this.cmdCenter = Command2;
                    class1842.cmdCenter = Command2;
                }
                return;
            case 81088:
                this.cmdRight = new Command(13, T.gL(T.OPTION), this);
                for (int i4 = 0; i4 < this.Field17.children.length; i4++) {
                    Widget class1843 = this.Field17.children[i4];
                    Command Command3 = new Command(14, T.gL(T.ADD_FRIEND), new Integer(i4), this);
                    this.cmdCenter = Command3;
                    class1843.cmdCenter = Command3;
                }
                return;
            case 81089:
                this.cmdRight = new Command(16, T.gL(T.ADD), this);
                for (int i5 = 0; i5 < this.Field17.children.length; i5++) {
                    Widget class1844 = this.Field17.children[i5];
                    Command Command4 = new Command(15, T.gL(T.UNBLOCK), new Integer(i5), this);
                    this.cmdCenter = Command4;
                    class1844.cmdCenter = Command4;
                }
                return;
            default:
                return;
        }
    }

    public final void close() {
        super.close();
        GameResourceManager.releaseImgCache();
    }
}

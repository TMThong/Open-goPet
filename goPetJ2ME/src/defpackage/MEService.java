package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.network.Message;
import vn.me.screen.Screen;

public class MEService implements IActionListener {

    /* JADX INFO: Access modifiers changed from: package-private */
    public MEService(PkBattle class30) {
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        Message message = new Message(81);
        message.putByte(101);
        message.putByte(1);
        GlobalService.session.sendMessage(message);
        message.cleanup();
        Screen.currentDialog.Method274();
        GameController.waitDialog();
    }

    public static void Method79(int i) {
        Message message = new Message(81);
        message.putByte(17);
        message.putByte(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method444(String str, int i) {
        Message message = new Message(81);
        message.putByte(9);
        message.putByte(i);
        message.putString(str);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method104(int i) {
        Message message = new Message(81);
        message.putByte(11);
        message.putInt(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method146(int i) {
        Message message = new Message(81);
        message.putByte(31);
        message.putInt(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void switchToMe(int i) {
        Message message = new Message(81);
        message.putByte(2);
        message.putByte(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void show(boolean z) {
        Message message = new Message(81);
        message.putByte(45);
        if (z) {
            message.putByte(1);
        } else {
            message.putByte(0);
        }
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method29(int i, int i2) {
        Message message = new Message(81);
        message.putByte(46);
        message.putInt(i);
        message.putInt(i2);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static void Method28(int i, int i2) {
        Message message = new Message(81);
        message.putByte(55);
        message.putByte(i2);
        message.putInt(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void chat(String str, String str2) {
        Message message = new Message(81);
        message.putByte(66);
        message.putString(str);
        message.putString(str2);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void chatGlobal(String str) {
        Message message = new Message(81);
        message.putByte(10);
        message.putString(str);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method316(int i) {
        Message message = new Message(81);
        message.putByte(68);
        message.putByte(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method144(int i) {
        Message message = new Message(81);
        message.putByte(91);
        message.putByte(1);
        message.putByte(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void switchToMe(int i, boolean z) {
        Message message = new Message(81);
        message.putByte(91);
        message.putByte(3);
        message.putByte(i);
        message.putBoolean(z);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method0() {
        Message message = new Message(81);
        message.putByte(91);
        message.putByte(14);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method160(int i, int i2) {
        Message message = new Message(81);
        message.putByte(91);
        message.putByte(15);
        message.putInt(i);
        message.putByte(i2);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method162(int i, int i2) {
        Message message = new Message(81);
        message.putByte(91);
        message.putByte(16);
        message.putInt(i);
        message.putByte(i2);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void Method445(int i) {
        Message message = new Message(81);
        message.putByte(91);
        message.putByte(26);
        message.putInt(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void showSkillClan(int i) {
        Message message = new Message(81);
        message.putByte(91);
        message.putByte(27);
        message.putInt(i);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void AdminGetItem() {
        Message message = new Message(81);
        message.putByte(6);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void AdminGiveItem() {
        Message message = new Message(81);
        message.putByte(6);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    public static void AutoAttack() {
        Message message = new Message(81);
        message.putByte(22);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }
}

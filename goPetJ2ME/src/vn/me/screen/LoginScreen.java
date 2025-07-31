package vn.me.screen;

import vn.me.core.BaseCanvas;
import defpackage.mMap;
import defpackage.Class14;
import defpackage.SceneManage;
import defpackage.Server;
import defpackage.GlobalService;
import vn.me.ui.Button;
import vn.me.ui.InputDialog;
import vn.me.ui.Label;
import vn.me.ui.common.Resource;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.geom.Dimension;
import defpackage.ActorFactory;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.Command;
import vn.me.ui.common.T;
import vn.me.ui.Dialog;
import vn.me.ui.EditField;
import vn.me.ui.common.LAF;
import vn.me.ui.WidgetGroup;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.util.Vector;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;
import thong.auto.AutoDectectSpeed;
import vn.me.ui.interfaces.IActionListener;

public final class LoginScreen extends Screen implements IActionListener {

    private String Field630;
    private String Field631;
    private String Field632;
    private String Field633;
    public static boolean Field634 = true;
    public static String Field635;
    public static String Field636;
    private EditField Field637;
    private EditField Field638;
    private EditField Field639;
    private Button Field640;
    private int Field641;
    private int Field642;
    private int Field643;
    private int Field644;
    private int Field645;
    private byte Field646;
    private Command Field647;
    private Command Field648;
    private Command Field649;
    private Command Field650;
    private Command Field651;
    private Command Field652;
    private Command Field653;
    private Command Field654;
    private Label Field655;
    private int Field656;
    private int Field657;
    private final mMap Field658;
    public long Field659;
    public boolean Field660;
    private Dialog serverDialog;
    private Dialog languageDialog;
    private Image Field662;
    private Image Field663;
    private int Field664;
    private int Field665;

    ///////@Override // defpackage.Screen
    public final void hide() {
        super.hide();
        Method25();
        GlobalService.Field1005 = GlobalService.Field1004;
        GlobalService.Field1006 = GlobalService.Field1003;
    }

    public LoginScreen() {
        super(true);
        this.Field631 = new StringBuffer().append(GameController.Method34()).append("beta").toString();
        this.Field632 = T.gL(T.AN_APPLICATION_OF_STR);
        this.Field633 = "mGO.";
        this.Field646 = (byte) 0;
        this.Field656 = 0;
        this.Field657 = 0;
        this.Field660 = true;
        this.Field665 = 2;
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.screenId = "LOGIN";
        this.Field664 = LAF.Field1295;
        this.Field637 = new EditField(this.Field641, (BaseCanvas.Field158 - LAF.Field1293) - LAF.LOT_PADDING, this.Field642, LAF.Field1293);
        this.Field638 = new EditField(this.Field641, BaseCanvas.Field158, this.Field642, LAF.Field1293);
        this.Field639 = new EditField(this.Field641, BaseCanvas.Field158 + LAF.Field1293 + LAF.LOT_PADDING, this.Field642, LAF.Field1293);
        this.Field640 = new Button(1);
        this.Field655 = new Label(ActorFactory.gL(331), ResourceManager.boldFont);
        Resource.Method402("/lg.dat");
        this.Field663 = Resource.createImage(0);
        Method79(0);
        this.Field658 = new mMap(11, new SceneManage(), 0, 0);
        SplashScreen.Method6();
        showLanguageModal();
        AutoDectectSpeed.isTest = false;
    }

    private void Method79(int i) {
        int i2;
        int Method330;
        String str;
        String Method486 = BaseCanvas.w > 240 ? ActorFactory.gL(331) : new StringBuffer().append(ActorFactory.gL(330)).append(" ").append(ActorFactory.gL(362)).toString();
        Label class173 = new Label("000000");
        int i3 = LAF.LOT_PADDING;
        while (true) {
            int Method3302 = class173.normalfont.getWidth(new StringBuffer().append(Method486).append(class173.text).toString()) + (LAF.LOT_PADDING << 1);
            i2 = Method3302;
            if (Method3302 < BaseCanvas.w) {
                class173.text = class173.text.substring(0, class173.text.length() - 1);
                if (i2 > BaseCanvas.w - (LAF.LOT_PADDING << 1)) {
                    if (class173.text.length() <= 0) {
                        break;
                    }
                } else {
                    break;
                }
            } else {
                i2 = BaseCanvas.w - (LAF.LOT_PADDING << 1);
                break;
            }
        }
        this.Field642 = i2 - (this.Field664 << 1);
        this.Field641 = ((BaseCanvas.w - i2) >> 1) + this.Field664;
        if (i == 0) {
            this.Field643 = (LAF.LOT_PADDING << 2) + (LAF.Field1293 << 1) + this.Field655.h + this.Field640.h;
        } else if (i == 1) {
            this.Field643 = (LAF.LOT_PADDING * 6) + (LAF.Field1293 * 3);
        }
        if (BaseCanvas.h >= 176) {
            this.Field645 = LAF.Field1293 + (LAF.Field1295 << 1);
        } else {
            this.Field645 = LAF.Field1293 + LAF.LOT_PADDING;
        }
        int i4 = (BaseCanvas.h - LAF.Field1293) - this.Field643;
        if (BaseCanvas.w <= 240 || BaseCanvas.h <= 240) {
            if ((i4 - LAF.Field1293) - LAF.LOT_PADDING > (this.Field663.getHeight() + this.Field645) - 20) {
                i4 -= (((((i4 - LAF.Field1293) - LAF.LOT_PADDING) - this.Field663.getHeight()) - this.Field645) + 20) >> 1;
            }
        } else if ((i4 - LAF.Field1293) - LAF.LOT_PADDING > this.Field663.getHeight() + this.Field645) {
            i4 -= ((((i4 - LAF.Field1293) - LAF.LOT_PADDING) - this.Field663.getHeight()) - this.Field645) >> 1;
        }
        if ((i4 - LAF.Field1293) - (LAF.LOT_PADDING * 3) < 0) {
            i4 = LAF.Field1293 + (LAF.LOT_PADDING * 3);
        }
        this.Field637.setMetrics(this.Field641, (i4 - LAF.Field1293) - LAF.LOT_PADDING, this.Field642, LAF.Field1293);
        this.Field637.Method158(ActorFactory.gL(298));
        this.Field637.Method79(3);
        this.Field638.setMetrics(this.Field641, i4, this.Field642, LAF.Field1293);
        this.Field639.setMetrics(this.Field641, i4 + LAF.Field1293 + LAF.LOT_PADDING, this.Field642, LAF.Field1293);
        this.Field638.Method79(2);
        this.Field639.Method79(2);
        this.Field640.Method158(ActorFactory.gL(488));
        this.Field640.Method324(ResourceManager.boldFont, ResourceManager.boldFont);
        this.Field640.setMetrics(((BaseCanvas.w >> 1) - (this.Field640.w >> 1)) - LAF.LOT_PADDING, i4 + LAF.Field1293 + LAF.LOT_PADDING, this.Field640.w + ResourceManager.Field1308.getWidth() + LAF.Field1295, this.Field640.h);
        this.Field640.setImage(ResourceManager.Field1308, new Dimension(ResourceManager.Field1308.getWidth(), ResourceManager.Field1308.getWidth()));
        if (BaseCanvas.w >= 240) {
            this.Field655.Method158(ActorFactory.gL(331));
            this.Field637.Method158(ActorFactory.gL(298));
            this.Field638.Method158(ActorFactory.gL(375));
            this.Field639.Method158(ActorFactory.gL(397));
            Method330 = ResourceManager.boldFont.getWidth(ActorFactory.gL(397)) + (LAF.LOT_PADDING * 5);
        } else {
            this.Field655.Method158(new StringBuffer().append(ActorFactory.gL(330)).append(" ").append(ActorFactory.gL(362)).toString());
            this.Field637.Method158(ActorFactory.gL(298));
            this.Field638.Method158(ActorFactory.gL(348));
            this.Field639.Method158(ActorFactory.gL(482));
            Method330 = ResourceManager.boldFont.getWidth(ActorFactory.gL(482)) + (LAF.LOT_PADDING * 5);
        }
        this.Field637.Method104(Method330);
        this.Field638.Method104(Method330);
        this.Field639.Method104(Method330);
        this.Field655.setMetrics(0, this.Field640.y + this.Field640.h, BaseCanvas.w, this.Field655.h);
        this.Field655.align = 17;
        this.container.addWidget(this.Field655);
        this.Field648 = new Command(1, ActorFactory.gL(385), this);
        String Method4862 = ActorFactory.gL(266);
        if (BaseCanvas.w <= 128) {
            String trim = Method4862.trim();
            str = trim.equals(ActorFactory.gL(266)) ? "Đ.nhập" : trim.equals(ActorFactory.gL(385)) ? "Đ.ký" : trim;
        } else {
            str = Method4862;
        }
        Command Command = new Command(2, str, this);
        this.Field647 = Command;
        this.cmdCenter = Command;
        this.cmdRight = new Command(3, ActorFactory.gL(275), this);
        this.Field649 = new Command(4, ActorFactory.gL(154), this);
        this.Field650 = new Command(5, ActorFactory.gL(337), this);
        this.Field651 = new Command(6, ActorFactory.gL(580), this);
        this.Field652 = new Command(7, ActorFactory.gL(300), this);
        this.Field653 = new Command(8, ActorFactory.gL(220), this);
        this.Field654 = new Command(9, ActorFactory.gL(139), this);
        this.container = new WidgetGroup(0, 0, BaseCanvas.w, BaseCanvas.h);
        Method25();
        this.container.isLoop = true;
        this.Field637.setText(ActorFactory.loadUTF("nick"));
        this.Field638.setText(ActorFactory.loadUTF("pass"));
        this.Field640.Field1048 = this.Field637.getText().length() > 0;
        this.Field640.cmdRight = this.Field647;
        Method312(this.Field639);
        this.Field630 = new StringBuffer().append(ActorFactory.gL(205)).append(": ").append(GameController.Method52()).toString();
        this.Field637.requestFocus();
        addWidget(this.Field637);
        addWidget(this.Field638);
        addWidget(this.Field639);
        addWidget(this.Field640);
        addWidget(this.Field655);
        this.serverDialog = new Dialog((BaseCanvas.w - i2) >> 1, 0, i2, 120);
        this.serverDialog.cmdLeft = new Command(151, ActorFactory.gL(548), this);
        this.serverDialog.cmdRight = new Command(17, ActorFactory.gL(41), this);
        this.serverDialog.y = -100;

        this.languageDialog = new Dialog((BaseCanvas.w - i2) >> 1, 0, i2, 120);
        this.languageDialog.y = -100;
    }

    private void Method6() {
        Method79(1);
        this.cmdCenter = this.Field648;
        this.Field646 = (byte) 1;
        this.Field637.isVisible = true;
        this.Field638.isVisible = true;
        this.Field639.isVisible = true;
        this.Field640.isVisible = false;
        this.Field655.isVisible = false;
        Method25();
    }

    private void Method7() {
        this.Field646 = (byte) 0;
        Method79(0);
        this.container.hideWidget(this.Field639);
        this.Field640.isVisible = true;
        this.Field655.isVisible = true;
        this.Field637.isVisible = true;
        this.Field638.isVisible = true;
        this.cmdCenter = this.Field647;
        Method25();
    }

    ///////@Override // defpackage.Screen
    public final void update() {
        super.update();
        if (this.Field645 != this.Field644) {
            this.Field644 += (this.Field645 - this.Field644) >> 1;
        }
        this.Field656 += this.Field665;
        if (this.Field656 < 0 || this.Field656 >= (this.Field658.mapWidth * 24) - BaseCanvas.w) {
            this.Field665 = -this.Field665;
        }
    }

    ///////@Override // defpackage.Screen
    public void paintBackground() {
        this.Field658.Method183(this.Field656, 0, true);
    }

    ///////@Override // defpackage.Screen
    public void paintChildren() {
        if (BaseCanvas.w < 240) {
            Method129(BaseCanvas.g, BaseCanvas.Field157, this.Field644 - 20, 17);
        } else if (BaseCanvas.h > 240) {
            Method129(BaseCanvas.g, BaseCanvas.Field157, this.Field644, 17);
        } else {
            Method129(BaseCanvas.g, BaseCanvas.Field157, this.Field644 - 20, 17);
        }
        if (this.Field631 != null) {
            int Method332 = ((BaseCanvas.h - LAF.Field1293) - GameResourceManager.Method116().getHeight()) - 1;
            if (BaseCanvas.h >= 240) {
                GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field632, 1, ((BaseCanvas.h - LAF.Field1293) - (2 * GameResourceManager.Method116().getHeight())) - 1, 20);
                GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field633, 1, ((BaseCanvas.h - LAF.Field1293) - GameResourceManager.Method116().getHeight()) - 1, 20);
            } else {
                GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field632, 1, ((BaseCanvas.h - LAF.Field1293) - (2 * GameResourceManager.Method116().getHeight())) + 5, 20);
                GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field633, 1, ((BaseCanvas.h - LAF.Field1293) - GameResourceManager.Method116().getHeight()) + 1, 20);
            }
            GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field631, BaseCanvas.w - 1, Method332, 24);
        }
        if (this.Field630 != null) {
            GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field630, BaseCanvas.w - 2, 1, 24);
        }
        super.paintChildren();
    }

    private void Method129(Graphics graphics, int i, int i2, int i3) {
        graphics.drawImage(this.Field663, i, i2, 17);
        if (this.Field660) {
            int i4 = ((BaseCanvas.w - this.Field642) - (this.Field664 << 1)) >> 1;
            int i5 = this.Field637.y - 5;
            int i6 = this.Field642 + (this.Field664 << 1);
            int i7 = this.Field643;
            BaseCanvas.g.translate(i4, i5);
            BaseCanvas.g.setColor(LAF.CLR_MENU_BAR_LIGHTER);
            BaseCanvas.g.fillRect(3, 3, i6 - 6, i7 - 6);
            LAF.Method412(0, 0, i6, i7);
            BaseCanvas.g.translate(-i4, -i5);
        }
    }

    private void Method25() {
        this.Field637.x = -this.Field641;
        this.Field638.x = BaseCanvas.w + this.Field641;
        this.Field639.x = -this.Field641;
        this.Field640.x = -this.Field641;
        this.Field644 = -this.Field663.getHeight();
    }

    ///////@Override // defpackage.Screen
    public final void Method130() {
        super.Method130();
        if (this.Field655.isVisible) {
            Method79(0);
            Method7();
            return;
        }
        Method79(1);
        Method6();
    }

    public static void Method131() {
        try {
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
            int size = GlobalService.serverList.size();
            dataOutputStream.writeInt(size);
            for (int i = 0; i < size; i++) {
                Server class15 = (Server) GlobalService.serverList.elementAt(i);
                dataOutputStream.writeUTF(class15.Field51);
                dataOutputStream.writeUTF(class15.Field52);
                dataOutputStream.writeInt(class15.Field53);
            }
            dataOutputStream.close();
            ActorFactory.deleteRecord("server_list");
            ActorFactory.saveBuffer("server_list", byteArrayOutputStream.toByteArray());
            ByteArrayOutputStream byteArrayOutputStream2 = new ByteArrayOutputStream();
            DataOutputStream dataOutputStream2 = new DataOutputStream(byteArrayOutputStream2);
            for (int i2 = 0; i2 < size; i2++) {
                Server class152 = (Server) GlobalService.serverList.elementAt(i2);
                dataOutputStream2.writeBoolean(class152.Field54);
                dataOutputStream2.writeBoolean(class152.Field55);
            }
            dataOutputStream2.close();
            ActorFactory.deleteRecord("server_list_log_reg");
            ActorFactory.saveBuffer("server_list_log_reg", byteArrayOutputStream2.toByteArray());
        } catch (Exception unused) {
            unused.printStackTrace();
        }
    }

    private static boolean Method132() {
        GlobalService.serverList.removeAllElements();
        byte[] Method488 = ActorFactory.loadBuffer("server_list", 1);
        if (Method488 == null) {
            /*Server class15 = new Server("Pet City", GlobalService.Field1004, GlobalService.Field1003, 3500, 0);
            class15.Field55 = false;*/
            Server class152 = new Server("Đại thời đại", "127.0.0.1", 19189, 3500, 0);
            class152.Field55 = true;
            Server class153 = new Server("LocalHost", "127.0.0.1", 19180, 3500, 0);
            //GlobalService.Field1012.addElement(class15);
            //GlobalService.serverList.addElement(class153);
            GlobalService.serverList.addElement(class152);
            return false;
        }
        try {
            DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(Method488));
            int readInt = dataInputStream.readInt();
            for (int i = 0; i < readInt; i++) {
                GlobalService.serverList.addElement(new Server(dataInputStream.readUTF(), dataInputStream.readUTF(), dataInputStream.readInt(), 0, 0));
            }
            dataInputStream.close();
            byte[] Method4882 = ActorFactory.loadBuffer("server_list_log_reg", 1);
            if (Method4882 != null) {
                DataInputStream dataInputStream2 = new DataInputStream(new ByteArrayInputStream(Method4882));
                for (int i2 = 0; i2 < readInt; i2++) {
                    Server class154 = (Server) GlobalService.serverList.elementAt(i2);
                    class154.Field54 = dataInputStream2.readBoolean();
                    class154.Field55 = dataInputStream2.readBoolean();
                }
                dataInputStream2.close();
                return true;
            }
            return true;
        } catch (IOException unused) {
            return false;
        }
    }

    public final void showSelectServerModal() {
        Method132();
        if (this.Field662 == null) {
            this.Field662 = Resource.setFileTable("/common.dat", 20);
        }
        this.serverDialog.removeAll();
        Label label = new Label(ActorFactory.gL(428), ResourceManager.boldFont);
        label.setMetrics(0, 0, this.serverDialog.w, label.h);
        label.align = 17;
        Class14 class14 = new Class14();
        class14.setMetrics(0, label.h, this.serverDialog.w, class14.h);
        int i = label.h + class14.h;
        WidgetGroup class185 = new WidgetGroup(0, 0, this.serverDialog.w, i);
        class185.addWidget(label);
        class185.addWidget(class14);
        WidgetGroup wg = new WidgetGroup(0, label.h + 2, this.serverDialog.w, (this.serverDialog.h - i) - 2);
        int size = GlobalService.serverList.size();
        int i2 = 0;
        for (int i3 = 0; i3 < size; i3++) {
            Server class15 = (Server) GlobalService.serverList.elementAt(i3);
            boolean z = false;
            switch (this.Field646) {
                case 0:
                    z = class15.Field54;
                    break;
                case 1:
                    z = class15.Field55;
                    break;
            }
            if (z) {
                Button class165 = new Button();
                class165.setImage(this.Field662);
                class165.Method324(ResourceManager.boldFont, ResourceManager.boldFont);
                class165.text = class15.Field51;
                class165.cmdCenter = new Command(16, ActorFactory.gL(337), this);
                class165.setMetrics(0, (i2 * LAF.Field1293) + 2, this.serverDialog.w - (this.serverDialog.border << 1), LAF.Field1293);
                wg.addWidget(class165);
                i2++;
            }
        }
        int Method351 = 30 + (21 * wg.count());
        int i4 = Method351;
        if (Method351 < 93) {
            i4 = 93;
        }
        this.serverDialog.setMetrics((BaseCanvas.w - this.serverDialog.w) >> 1, (BaseCanvas.h - i4) >> 1, this.serverDialog.w, this.serverDialog.h);
        this.serverDialog.h = i4;
        this.serverDialog.addWidget(class185);
        this.serverDialog.addWidget(wg);
        wg.setViewMode(0);
        this.serverDialog.setViewMode(0);
        wg.isScrollableY = true;
        this.serverDialog.show(true);
        if (wg.count() > 0) {
            wg.getWidgetAt(0).setFocusWithParents(true);
        }
    }

    public final void showLanguageModal() {
        try {
            Integer language = ActorFactory.loadInt(ActorFactory.languageRMS);
            if (language != null) {
                if (language.intValue() >= 0) {
                    return;
                }
            }

        } catch (Exception e) {
        }
        if (this.Field662 == null) {
            this.Field662 = Resource.setFileTable("/common.dat", 20);
        }
        this.languageDialog.removeAll();
        Label label = new Label(T.gL(T.SELECT_LANGUAGE), ResourceManager.boldFont);
        label.setMetrics(0, 0, this.languageDialog.w, label.h);
        label.align = 17;
        Class14 class14 = new Class14();
        class14.setMetrics(0, label.h, this.languageDialog.w, class14.h);
        int i = label.h + class14.h;
        WidgetGroup class185 = new WidgetGroup(0, 0, this.languageDialog.w, i);
        class185.addWidget(label);
        class185.addWidget(class14);
        WidgetGroup wg = new WidgetGroup(0, label.h + 2, this.languageDialog.w, (this.languageDialog.h - i) - 2);

        int i2 = 0;
        String[] strings = new String[]{T.gL(T.VIETNAMESE), T.gL(T.ENGLISH)};
        for (int i3 = 0; i3 < strings.length; i3++) {
            Button btn = new Button();
            btn.Method324(ResourceManager.boldFont, ResourceManager.boldFont);
            btn.text = strings[i3];
            final int langId = i3;
            btn.cmdCenter = new Command(153, ActorFactory.gL(337), new IActionListener() {
                public void actionPerformed(Object obj) {
                    Screen.hideDialog(Screen.currentDialog);
                    ActorFactory.langueCode = langId;
                    ActorFactory.saveInt(ActorFactory.languageRMS, langId);
                    new SplashScreen(0).switchToMe(0, true);
                }
            });
            btn.setMetrics(0, (i2 * LAF.Field1293) + 2, this.languageDialog.w - (this.languageDialog.border << 1), LAF.Field1293);
            wg.addWidget(btn);
            i2++;
        }
        int Method351 = 30 + (21 * wg.count());
        int i4 = Method351;
        if (Method351 < 93) {
            i4 = 93;
        }
        this.languageDialog.setMetrics((BaseCanvas.w - this.serverDialog.w) >> 1, (BaseCanvas.h - i4) >> 1, this.serverDialog.w, this.serverDialog.h);
        this.languageDialog.h = i4;
        this.languageDialog.addWidget(class185);
        this.languageDialog.addWidget(wg);
        wg.setViewMode(0);
        this.languageDialog.setViewMode(0);
        wg.isScrollableY = true;
        this.languageDialog.show(true);
        if (wg.count() > 0) {
            wg.getWidgetAt(0).setFocusWithParents(true);
        }
    }

    public void switchToMe(int i, boolean z) {
        super.switchToMe(i, z); //To change body of generated methods, choose Tools | Templates.
        showLanguageModal();
    }

    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 0:
            case 10:
                Screen.hideDialog(Screen.currentDialog);
                return;
            case 1:
                if (this.Field646 != 1) {
                    Method6();
                    return;
                }
                Field635 = this.Field637.getText().toLowerCase().trim();
                Field636 = this.Field638.getText();
                String Method33 = this.Field639.getText();
                if (Field635.equals("")) {
                    this.Field637.requestFocus();
                    GameController.startOKDlg(ActorFactory.gL(132));
                    return;
                } else if (Field636.equals("")) {
                    this.Field638.requestFocus();
                    GameController.startOKDlg(ActorFactory.gL(133));
                    return;
                } else if (Field636.equals(Method33)) {
                    showSelectServerModal();
                    return;
                } else {
                    this.Field639.requestFocus();
                    GameController.startOKDlg(ActorFactory.gL(322));
                    return;
                }
            case 2:
                if (this.Field646 != 0) {
                    Method7();
                    return;
                }
                Field635 = this.Field637.getText().toLowerCase().trim();
                Field636 = this.Field638.getText();
                if (Field635.equals("")) {
                    this.Field637.requestFocus();
                    GameController.startOKDlg(ActorFactory.gL(393));
                    return;
                } else if (!Field636.equals("")) {
                    showSelectServerModal();
                    //showLanguageModal();
                    return;
                } else {
                    this.Field638.requestFocus();
                    GameController.startOKDlg(ActorFactory.gL(394));
                    return;
                }
            case 3:
                Vector vector = new Vector();
                vector.addElement(this.Field646 == 0 ? this.Field648 : this.Field647);
                vector.addElement(GameController.Field470);
                vector.addElement(this.Field649);
                vector.addElement(new Command(18, T.gL(T.OTHER_FUNCTIONS_STR), this));
                vector.addElement(this.Field654);
                showMenu(vector, 0);
                return;
            case 4:
                showDialog(new InputDialog(new StringBuffer().append(ActorFactory.gL(519)).append(":").toString(), this.Field650, GameController.Field464, 0));
                return;
            case 5:
                String Method332 = ((InputDialog) Screen.currentDialog).txtInput.getText();
                GameController.Field462 = Method332;
                if (Method332 == null || GameController.Field462.length() == 0) {
                    GameController.Method40(ActorFactory.gL(178), true);
                    return;
                } else {
                    Dialog.Method276(ActorFactory.gL(356), this.Field651, null, this.Field652, true);
                    return;
                }
            case 6:
                GameController.show(true);
                GlobalService.Field1001 = new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        GlobalService.Method242(1);
                    }
                };
                if (GlobalService.Method233()) {
                    GlobalService.Field1001.actionPerformed(null);
                    return;
                }
                GameController.Method44(ActorFactory.gL(83), true);
                GlobalService.Method235();
                return;
            case 7:
                GameController.Method40(ActorFactory.gL(174), true);
                return;
            case 8:
                GameController.startOKDlg(new StringBuffer("\n").append(ActorFactory.gL(371)).append(": ").append(GameController.Method33()).append('\n').append(ActorFactory.gL(557)).append(": ").append(GameController.Method34()).append(ActorFactory.gL(353)).append("\n\n").toString());
                return;
            case 9:
                GameController.destroyApp();
                return;
            case 16:
                Server class15 = (Server) GlobalService.serverList.elementAt(((WidgetGroup) this.serverDialog.getWidgetAt(1)).getFocusedIndex());
                String str = class15.Field52;
                GlobalService.Field1005 = str;
                GlobalService.Field1004 = str;
                int i = class15.Field53;
                GlobalService.Field1006 = i;
                GlobalService.Field1003 = i;
                GlobalService.Method236();
                if (this.Field646 != 0) {
                    if (this.Field646 == 1) {
                        GlobalService.Field1001 = new IActionListener() {
                            public void actionPerformed(Object obj) {
                                GameController.Method44(ActorFactory.gL(386), true);
                                if (System.currentTimeMillis() - Field659 > 2000) {
                                    GlobalService.Method239(LoginScreen.Field635, LoginScreen.Field636);
                                    Field659 = System.currentTimeMillis();
                                }
                            }
                        };
                        if (GlobalService.Method233()) {
                            GlobalService.Field1001.actionPerformed(null);
                            return;
                        }
                        GameController.Method44(ActorFactory.gL(83), true);
                        GlobalService.Method235();
                        return;
                    }
                    return;
                }
                String str2 = Field635;
                String str3 = Field636;
                GameController.Field455 = this.Field640.Field1048;
                Field635 = str2;
                Field636 = str3;
                GlobalService.Field1001 = new IActionListener() {
                    public void actionPerformed(Object obj) {
                        GameController.Method44(ActorFactory.gL(265), true);
                        GlobalService.Method238(LoginScreen.Field635, LoginScreen.Field636, GameController.Method34());
                    }
                };
                if (GlobalService.Method233()) {
                    GlobalService.Field1001.actionPerformed(null);
                    return;
                }
                GameController.Method44(ActorFactory.gL(83), true);
                GlobalService.Method235();
                return;
            case 17:
                this.serverDialog.Method274();
                return;
            case 18:
                Vector vector2 = new Vector();
                vector2.addElement(GameController.Field465);
                vector2.addElement(GameController.Field466);
                vector2.addElement(GameController.Field468);
                vector2.addElement(GameController.Field469);
                vector2.addElement(this.Field653);
                showMenu(vector2, 0);
                return;
            case 19:
                return;
            case 151:
                GlobalService.Field1001 = new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        GameController.Method44(ActorFactory.gL(552), true);
                        GlobalService.Method265();
                        LoginScreen.Field634 = true;
                    }
                };
                if (GlobalService.Method233()) {
                    GlobalService.Field1001.actionPerformed(null);
                    return;
                }
                GameController.Method44(ActorFactory.gL(83), true);
                GlobalService.Method235();
                return;
            case 152:
                GlobalService.Method260(new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        showSelectServerModal();
                    }
                }, null);
                return;
            default:
                super.actionPerformed(obj);
                return;
        }
    }
}

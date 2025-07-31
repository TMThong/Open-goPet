package vn.me.screen;

import defpackage.ActorFactory;
import defpackage.Ulti;
import defpackage.Class68;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import vn.me.ui.ListItem;
import defpackage.PetGameModel;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.InputDialog;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.Label;
import vn.me.ui.EditField;
import vn.me.ui.WidgetGroup;
import vn.me.screen.Screen;
import java.util.Vector;
import javax.microedition.io.ConnectionNotFoundException;

/* renamed from: Class9  reason: default package */
 /* loaded from: gopet_repackage.jar:Class9.class */
public final class MoneyScreen extends Screen implements IActionListener {

    private Vector Field34;
    private WidgetGroup Field35;
    private Command Field36;
    private Command Field37;
    private Command Field38;
    Class68 Field39;
    private InputDialog Field40;

    public MoneyScreen() {
        super(true);
        this.title = ActorFactory.gL(24);
        this.cmdLeft = new Command(0, ActorFactory.gL(64), this);
        this.screenId = "MONEY";
    }

    private void Method6() {
        Command Command = new Command(111, ActorFactory.gL(337), this);
        ActorFactory.gL(25);
        GameController.Method58(new String[]{ActorFactory.gL(184), ActorFactory.gL(25)}, new int[]{1, 1}, Command, 0);
    }

    private void Method7() {
        Command Command = new Command(121, ActorFactory.gL(337), this);
        ActorFactory.gL(400);
        GameController.Method58(new String[]{ActorFactory.gL(184), ActorFactory.gL(400)}, new int[]{1, 1}, Command, 0);
    }

    private static void Method25() {
        GameController.Method40(new StringBuffer().append(ActorFactory.gL(184)).append(": ").append(Ulti.formatNumber(PetGameModel.mGold)).append("\nNgọc: ").append(Ulti.formatNumber(PetGameModel.mCoin)).toString(), true);
    }

    public final void Method19(Vector vector) {
        Class68 class68 = new Class68((byte) -1);
        class68.Field436 = new StringBuffer().append(ActorFactory.gL(55)).append(" ").append(ActorFactory.gL(184)).toString();
        Class68 class682 = new Class68((byte) -1);
        class682.Field436 = new StringBuffer().append(ActorFactory.gL(51)).append(" ").append(ActorFactory.gL(184)).toString();
        vector.insertElementAt(class68, 0);
        vector.addElement(class682);
        Class68 class683 = new Class68((byte) 9);
        class683.Field436 = new StringBuffer().append(ActorFactory.gL(184)).append(" > Ngọc").toString();
        Class68 class684 = new Class68((byte) 8);
        class684.Field436 = new StringBuffer().append(ActorFactory.gL(184)).append(" ").append(ActorFactory.gL(595)).toString();
        vector.addElement(class683);
        vector.addElement(class684);
        this.Field34 = vector;
        if (this.Field35 == null) {
            this.Field35 = new WidgetGroup(0, LAF.Field1292, BaseCanvas.w, BaseCanvas.h - (LAF.Field1292 << 1));
            if (GameController.myInfo != null) {
                this.Field35.cmdCenter = new Command(ActorFactory.gL(419), this);
            }
            this.Field35.isScrollableY = true;
            this.Field35.spacing = 0;
        }
        this.Field35.removeAll();
        int size = this.Field34.size();
        int Method332 = (GameResourceManager.smallFont.getHeight() << 1) + (LAF.LOT_PADDING << 1) + 2;
        for (int i = 0; i < size; i++) {
            Class68 class685 = (Class68) this.Field34.elementAt(i);
            final ListItem class174 = new ListItem(class685, 0, 0, BaseCanvas.w, Method332);
            if (class685.Field440 != -1) {
                class174.descriptionFont = GameResourceManager.smallFont;
                class174.focusDescFont = GameResourceManager.smallFont;
                //class174.onFocusAction = new Class11(this, class174);
                class174.onFocusAction = new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        MoneyScreen.Method382(MoneyScreen.this).cmdCenter = new Command(ActorFactory.gL(419), (MoneyScreen) BaseCanvas.getCurrentScreen());
                        class174.cmdCenter = MoneyScreen.Method382(MoneyScreen.this).cmdCenter;
                    }
                };
                this.Field35.addWidget(class174, false);
            } else {
                Label class173 = new Label(class685.Field436);
                if (i == 0) {
                    class173.isFocusable = true;
                }
                class173.onFocusAction = new IActionListener() {
                    ///////@Override
                    public void actionPerformed(Object obj) {
                        MoneyScreen.Method382(MoneyScreen.this).cmdCenter = null;
                    }
                };
                class173.Method324(ResourceManager.boldFont, ResourceManager.boldFont);
                class173.setMetrics(0, 0, BaseCanvas.w, ResourceManager.boldFont.getHeight() + 5);
                class173.align = 20;
                this.Field35.addWidget(class173, false);
            }
        }
        if (size > 0) {
            this.Field35.getWidgetAt(0).requestFocus();
        }
        this.container.addWidget(this.Field35);
        this.Field35.setFocusWithParents(true);
        this.Field35.setViewMode(1);
        this.Field35.isLoop = true;
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    ///////@Override // defpackage.Screen, defpackage.Class200
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        if (Command == null) {
            BaseCanvas.getCurrentScreen();
            Screen.hideDialog(Screen.currentDialog);
            String str = (String) ((Object[]) obj)[1];
            if ("smsOK".equals(str)) {
                GameController.Method40(ActorFactory.gL(440), true);
                GlobalService.Method241(new StringBuffer().append(this.Field39.Field437).append(GameController.myInfo.Field49).toString(), this.Field39.Field438);
            } else if ("smsFail".equals(str)) {
                GameController.Method40(ActorFactory.gL(439), true);
            }
        } else if (Command == this.Field35.cmdCenter) {
            try {
                this.Field39 = (Class68) ((ListItem) this.Field35.defaultFocusWidget).model;
                switch (this.Field39.Field440) {
                    case -1:
                        return;
                    case 0:
                        Dialog.Method276(this.Field39.Field439, new Command("Có", new IActionListener() {
                            ///////@Override
                            public void actionPerformed(Object obj) {
                                hideDialog();
                                GameController.Method41(ActorFactory.gL(435));
                                GlobalService.Method240(Field39.Field437, new StringBuffer("sms://").append(Field39.Field438).toString(), this, this);
                            }
                        }), null, GameController.Field464, true);
                        return;
                    case 1:
                        this.Field38 = new Command(ActorFactory.gL(337), this);
                        GameController.Method57(this.Field39.Field436, this.Field38, GameController.Field464, 0);
                        return;
                    case 2:
                        this.Field37 = new Command(ActorFactory.gL(337), this);
                        this.Field40 = GameController.Method56(this.Field39.Field436, new String[]{this.Field39.Field437, this.Field39.Field438}, new int[]{0, 0}, this.Field37, GameController.Field464);
                        return;
                    case 3:
                        return;
                    case 4:
                        this.Field36 = new Command(ActorFactory.gL(337), this);
                        Dialog.Method275(this.Field39.Field437, this.Field36, null, GameController.Field464);
                        return;
                    case 5:
                    default:
                        GameController.Method40(ActorFactory.gL(496), true);
                        return;
                    case 6:
                        Method6();
                        return;
                    case 7:
                        Method7();
                        return;
                    case 8:
                        Method25();
                        return;
                    case 9:
                        Command Command2 = new Command(122, ActorFactory.gL(337), this);
                        int i = GameController.Field454;
                        ActorFactory.gL(25);
                        GameController.Method58(new String[]{ActorFactory.gL(184), "Ngọc"}, new int[]{1, 1}, Command2, i);
                        return;
                }
            } catch (Exception unused) {
            }
        } else if (Command == this.Field36) {
            try {
                BaseCanvas.instance.midlet.platformRequest(new StringBuffer("tel:").append(this.Field39.Field438).toString());
            } catch (ConnectionNotFoundException unused2) {
            }
        } else if (Command == this.Field37) {
            BaseCanvas.getCurrentScreen();
            Screen.hideDialog(Screen.currentDialog);
            if (this.Field40 == null) {
                return;
            }
            GameController.Method41(ActorFactory.gL(352));
            GlobalService.Method258(this.Field39, this.Field40.Method327(0), this.Field40.Method327(1));
        } else if (Command == this.Field38) {
            InputDialog class172 = (InputDialog) Screen.currentDialog;
            GameController.Method41(ActorFactory.gL(352));
            GlobalService.Method258(this.Field39, class172.txtInput.getText(), null);
        } else if (Command == this.cmdRight) {
            Vector vector = new Vector(2);
            vector.addElement(new Command(1, new StringBuffer().append(ActorFactory.gL(51)).append(" ").append(ActorFactory.gL(184)).toString(), this));
            vector.addElement(new Command(2, new StringBuffer().append(ActorFactory.gL(184)).append(" ").append(ActorFactory.gL(595)).toString(), this));
            showMenu(vector, 0);
        } else {
            switch (Command.cmdId) {
                case 0:
                    BaseCanvas.getCurrentScreen().Method297(null);
                    return;
                case 1:
                    Vector vector2 = new Vector(5);
                    vector2.addElement(new Command(11, new StringBuffer().append(ActorFactory.gL(184)).append(" > ").append(ActorFactory.gL(25)).toString(), this));
                    vector2.addElement(new Command(12, new StringBuffer().append(ActorFactory.gL(184)).append(" > ").append(ActorFactory.gL(400)).toString(), this));
                    showMenu(vector2, 0);
                    return;
                case 2:
                    Method25();
                    return;
                case 11:
                    Method6();
                    return;
                case 12:
                    Method7();
                    return;
                case 111:
                    try {
                        int parseInt = Integer.parseInt(((EditField) ((InputDialog) Screen.currentDialog).getWidgetAt(2)).getText());
                        GameController.show(true);
                        GlobalService.Method247(79, 1, parseInt);
                        return;
                    } catch (Exception unused3) {
                        return;
                    }
                case 121:
                    try {
                        int parseInt2 = Integer.parseInt(((EditField) ((InputDialog) Screen.currentDialog).getWidgetAt(2)).getText());
                        GameController.show(true);
                        GlobalService.Method247(79, 2, parseInt2);
                        return;
                    } catch (Exception unused4) {
                        return;
                    }
                case 122:
                    try {
                        int parseInt3 = Integer.parseInt(((EditField) ((InputDialog) Screen.currentDialog).getWidgetAt(2)).getText());
                        GameController.show(true);
                        GlobalService.Method246(81, (byte) 44, parseInt3);
                        return;
                    } catch (Exception unused5) {
                        return;
                    }
                default:
                    return;
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static WidgetGroup Method382(MoneyScreen class9) {
        return class9.Field35;
    }
}

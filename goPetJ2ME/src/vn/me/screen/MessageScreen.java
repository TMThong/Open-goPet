package vn.me.screen;

import defpackage.ActorFactory;
import vn.me.core.BaseCanvas;
import defpackage.IwinMesssage;
import vn.me.ui.ListItem;
import defpackage.Class179;
import defpackage.Command;
import vn.me.ui.Dialog;
import defpackage.GameController;
import defpackage.GameResourceManager;
import vn.me.ui.common.LAF;
import vn.me.ui.Label;
import vn.me.ui.WidgetGroup;
import vn.me.screen.Screen;
import java.util.Vector;
import vn.me.ui.interfaces.IActionListener;

/* renamed from: Class7  reason: default package */
/* loaded from: gopet_repackage.jar:Class7.class */
public final class MessageScreen extends Screen implements IActionListener {
    public static Vector Field20 = new Vector();
    public static Vector Field21 = new Vector();
    public static Vector Field22 = new Vector();
    private WidgetGroup Field23;
    private WidgetGroup Field24;
    private WidgetGroup Field25;
    private Command Field26;
    private Command Field27;
    public Class179 Field28;
    private int Field29;
    private int Field30;
    private int Field31;
    private Command cmdOptions;

    public MessageScreen() {
        super(true);
        this.Field29 = -1;
        this.Field30 = -1;
        this.Field31 = -1;
        this.screenId = "MESSAGE";
        this.cmdLeft = GameController.Field467;
        this.cmdOptions = new Command(100, ActorFactory.gL(275), this);
        this.cmdRight = this.cmdOptions;
    }

    ///////@Override // defpackage.Screen
    public final void switchToMe(int i, boolean z) {
        super.switchToMe(i, z);
        int size = Field20.size();
        int size2 = Field21.size();
        int size3 = Field22.size();
        int i2 = -1;
        this.Field28 = new Class179(0, 0, this.container.w, BaseCanvas.h - LAF.Field1293);
        this.Field28.Field1194.isAutoFit = true;
        this.container.addWidget(this.Field28, false);
        if (this.Field23 == null) {
            this.Field23 = new WidgetGroup(0, LAF.Field1292, this.container.w, BaseCanvas.h - (2 * LAF.Field1292));
            this.Field23.isScrollableY = true;
        }
        if (Field20.isEmpty()) {
            Label class173 = new Label(ActorFactory.gL(307));
            class173.align = 17;
            this.Field23.addWidget(class173);
        } else {
            i2 = 0;
        }
        for (int i3 = 0; i3 < size; i3++) {
            ListItem class174 = new ListItem((IwinMesssage) Field20.elementAt(i3), 0, 0, BaseCanvas.w, LAF.Field1291);
            class174.cmdCenter = new Command(3, ActorFactory.gL(560), this);
            class174.descriptionFont = GameResourceManager.smallFont;
            class174.focusDescFont = GameResourceManager.smallFont;
            class174.destX = class174.x;
            this.Field23.addWidget(class174, false);
        }
        if (this.Field24 == null) {
            this.Field24 = new WidgetGroup(0, LAF.Field1292, this.container.w, BaseCanvas.h - (2 * LAF.Field1292));
            this.Field24.isScrollableY = true;
        }
        if (Field21.isEmpty()) {
            Label class1732 = new Label(ActorFactory.gL(307));
            class1732.align = 17;
            this.Field24.addWidget(class1732);
        } else if (i2 == -1) {
            i2 = 1;
        }
        for (int i4 = 0; i4 < size2; i4++) {
            ListItem class1742 = new ListItem((IwinMesssage) Field21.elementAt(i4), 0, 0, BaseCanvas.w, LAF.Field1291);
            class1742.cmdCenter = new Command(5, ActorFactory.gL(560), this);
            class1742.descriptionFont = GameResourceManager.smallFont;
            class1742.focusDescFont = GameResourceManager.smallFont;
            class1742.destX = class1742.x;
            this.Field24.addWidget(class1742, false);
        }
        if (this.Field25 == null) {
            this.Field25 = new WidgetGroup(0, LAF.Field1292, this.container.w, BaseCanvas.h - (2 * LAF.Field1292));
            this.Field25.isScrollableY = true;
        }
        if (Field22.isEmpty()) {
            Label class1733 = new Label(ActorFactory.gL(307));
            class1733.align = 17;
            this.Field25.addWidget(class1733);
        } else if (i2 == -1) {
            i2 = 2;
        }
        for (int i5 = 0; i5 < size3; i5++) {
            ListItem class1743 = new ListItem((IwinMesssage) Field22.elementAt(i5), 0, 0, BaseCanvas.w, LAF.Field1291);
            class1743.cmdCenter = new Command(8, ActorFactory.gL(560), this);
            class1743.descriptionFont = GameResourceManager.smallFont;
            class1743.focusDescFont = GameResourceManager.smallFont;
            class1743.destX = class1743.x;
            this.Field25.addWidget(class1743, false);
        }
        this.Field23.padding = LAF.LOT_PADDING;
        this.Field23.setViewMode(1);
        this.Field24.padding = LAF.LOT_PADDING;
        this.Field24.setViewMode(1);
        this.Field25.padding = LAF.LOT_PADDING;
        this.Field25.setViewMode(1);
        this.Field28.Method294(ActorFactory.gL(159), this.Field23);
        this.Field28.Method294("Admin", this.Field24);
        this.Field28.Method294(ActorFactory.gL(391), this.Field25);
        this.Field28.Field1193 = new IActionListener() {
            ///////@Override
            public void actionPerformed(Object obj) {
                
            }
        };
        if (i2 != -1) {
            this.Field28.Method104(i2);
        } else {
            this.Field28.Method79(0);
        }
    }

    private void Method6() {
        IwinMesssage class143;
        if (this.Field23.getFocusedIndex() != -1 && this.Field23.getFocusedIndex() < Field20.size()) {
            this.Field29 = this.Field23.getFocusedIndex();
            IwinMesssage class1432 = (IwinMesssage) Field20.elementAt(this.Field23.getFocusedIndex());
            class143 = class1432;
            class1432.Field983 = true;
        } else if (this.Field24.getFocusedIndex() == -1 || this.Field24.getFocusedIndex() >= Field21.size()) {
            if (this.Field25.getFocusedIndex() == -1 || this.Field25.getFocusedIndex() >= Field22.size()) {
                return;
            }
            this.Field31 = this.Field25.getFocusedIndex();
            IwinMesssage class1433 = (IwinMesssage) Field22.elementAt(this.Field25.getFocusedIndex());
            class143 = class1433;
            class1433.Field983 = true;
            switch (class143.Field982) {
                case 1:
                    Dialog.Method275(class143.Field981, null, null, GameController.Field464);
                    return;
                case 5:
                    Dialog.Method275(new StringBuffer().append(ActorFactory.gL(9)).append(" ").append(class143.Field980).append(" ").append(ActorFactory.gL(221)).toString(), new Command(101, ActorFactory.gL(580), new Integer(class143.Field979), this), null, GameController.Field464);
                    return;
            }
        } else {
            this.Field30 = this.Field24.getFocusedIndex();
            IwinMesssage class1434 = (IwinMesssage) Field21.elementAt(this.Field24.getFocusedIndex());
            class143 = class1434;
            class1434.Field983 = true;
        }
        Dialog.Method275(class143.Field981, null, null, GameController.Field464);
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 0:
                switch (this.Field28.Method128()) {
                    case 0:
                        if (this.Field29 < 0 || this.Field29 >= Field20.size()) {
                            return;
                        }
                        Field20.removeElementAt(this.Field29);
                        this.Field23.removeWidget(this.Field23.getWidgetAt(this.Field29));
                        return;
                    case 1:
                        if (this.Field30 < 0 || this.Field30 >= Field21.size()) {
                            return;
                        }
                        Field21.removeElementAt(this.Field30);
                        this.Field24.removeWidget(this.Field24.getWidgetAt(this.Field30));
                        return;
                    case 2:
                        if (this.Field31 < 0 || this.Field31 >= Field22.size()) {
                            return;
                        }
                        Field22.removeElementAt(this.Field31);
                        this.Field25.removeWidget(this.Field25.getWidgetAt(this.Field31));
                        return;
                    default:
                        return;
                }
            case 1:
                switch (this.Field28.Method128()) {
                    case 0:
                        Field20.removeAllElements();
                        this.Field23.removeAll();
                        return;
                    case 1:
                        Field21.removeAllElements();
                        this.Field24.removeAll();
                        return;
                    case 2:
                        Field22.removeAllElements();
                        this.Field25.removeAll();
                        return;
                    default:
                        return;
                }
            case 3:
                Method6();
                return;
            case 5:
                Method6();
                return;
            case 8:
                Method6();
                return;
            case 100:
                this.Field29 = this.Field23.getFocusedIndex();
                this.Field30 = this.Field24.getFocusedIndex();
                this.Field31 = this.Field25.getFocusedIndex();
                Vector vector = new Vector();
                this.Field26 = new Command(0, ActorFactory.gL(95), this);
                this.Field27 = new Command(1, ActorFactory.gL(94), this);
                vector.addElement(this.Field26);
                vector.addElement(this.Field27);
                showMenu(vector, 0);
                return;
            default:
                return;
        }
    }
}

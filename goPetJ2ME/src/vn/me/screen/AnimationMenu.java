package vn.me.screen;

import defpackage.Class63;
import defpackage.Class65;
import defpackage.Class67;
import defpackage.Class69;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import defpackage.PetGameModel;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.WidgetGroup;
import vn.me.screen.Screen;
import java.util.Hashtable;
import java.util.Vector;

/* renamed from: Class61  reason: default package */
/* loaded from: gopet_repackage.jar:Class61.class */
public final class AnimationMenu extends Screen {
    private final int Field410;
    private final Vector Field411;
    private final Hashtable Field412;
    private final String Field413;
    private WidgetGroup Field414;
    private int Field415;

    public AnimationMenu(int i, String str) {
        super(true);
        this.Field411 = new Vector();
        this.Field412 = new Hashtable();
        this.Field415 = 0;
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.Field410 = i;
        this.Field413 = str;
        Method6();
    }

    public final void Method6() {
        this.Field411.removeAllElements();
        this.Field412.clear();
        this.Field414 = new WidgetGroup();
        this.Field414.setMetrics(15, 45, BaseCanvas.w - 30, ((BaseCanvas.h - LAF.LOT_ITEM_HEIGHT) - 3) - 45);
        this.Field414.isScrollableY = true;
        this.Field414.isLoop = true;
        this.Field415 = 0;
    }

    public final void Method7() {
        this.Field414.spacing = 0;
        this.container.addWidget(this.Field414);
        this.Field414.setViewMode(1);
        this.Field414.setFocusWithParents(true);
        this.Field414.findNextFocus(true, 0, 1);
    }

    public final Class67 Method8(boolean z, String str, int i, boolean z2, int i2, int i3) {
        Class67 class67 = new Class67(this);
        class67.Field422 = (byte) 1;
        class67.Field427 = str;
        class67.Field423 = (byte) i;
        class67.Field429 = z2;
        this.Field411.addElement(class67);
        class67.setSize(BaseCanvas.w - 30, 4);
        this.Field414.addWidget(class67);
        class67.setPosition(0, this.Field415);
        this.Field415 += class67.h + 5;
        class67.isFocusable = z;
        class67.Field430 = i2;
        class67.Field431 = i3;
        return class67;
    }

    public final void Method9(boolean z, String str, int i, int i2) {
        Class69 class69 = new Class69(this);
        class69.Field422 = (byte) 0;
        class69.Field444 = (byte) i2;
        class69.Field423 = (byte) i;
        class69.Field442 = str;
        class69.Field443 = GameResourceManager.Method124(class69.Field444).wrap(str, BaseCanvas.w - 30);
        this.Field411.addElement(class69);
        class69.setSize(BaseCanvas.w - 30, (class69.Field443.length * GameResourceManager.Method124(class69.Field444).getHeight()) + 4);
        this.Field414.addWidget(class69);
        class69.setPosition(0, this.Field415);
        this.Field415 += class69.h + 5;
        class69.isFocusable = z;
    }

    public final void Method10(int i, int i2, String str, boolean z, boolean z2) {
        Class63 class63 = (Class63) this.Field412.get(new Integer(i));
        Class63 class632 = class63;
        if (class63 == null) {
            class632 = new Class63(this);
            this.Field412.put(new Integer(i), class632);
        }
        Class63.Method15(class632)[i2] = z;
        Class63.Method16(class632)[i2] = z2;
        if (i == -1) {
            switch (i2) {
                case 0:
                    this.cmdRight = new Command(0, str, new Integer(i), this);
                    return;
                case 1:
                    this.cmdCenter = new Command(1, str, new Integer(i), this);
                    return;
                case 2:
                    this.cmdLeft = new Command(2, str, new Integer(i), this);
                    return;
                default:
                    return;
            }
        }
        Class65 class65 = (Class65) this.Field411.elementAt(i);
        switch (i2) {
            case 0:
                class65.cmdLeft = new Command(0, str, new Integer(i), this);
                return;
            case 1:
                class65.cmdCenter = new Command(1, str, new Integer(i), this);
                return;
            case 2:
                class65.cmdRight = new Command(2, str, new Integer(i), this);
                return;
            default:
                return;
        }
    }

    ///////@Override // defpackage.Screen, defpackage.Class200
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        Integer num = (Integer) Command.objPerfomed;
        Class63 class63 = (Class63) this.Field412.get(num);
        boolean z = false;
        boolean z2 = false;
        int i = Command.cmdId;
        if (class63 != null) {
            z = Class63.Method15(class63)[i];
            z2 = Class63.Method16(class63)[i];
        }
        if (z) {
            for (int i2 = 0; i2 < this.Field411.size(); i2++) {
                Class65 class65 = (Class65) this.Field411.elementAt(i2);
                if (class65.Field422 == 1) {
                    PetGameModel.Field284.Method456(((Class67) class65).Field427);
                }
            }
            Method297(null);
        }
        if (z2) {
            int i3 = Command.cmdId;
            int i4 = this.Field410;
            int intValue = num.intValue();
            Message message = new Message(81);
            message.putByte(100);
            message.putInt(i4);
            message.putInt(intValue);
            message.putInt(i3);
            GlobalService.session.sendMessage(message);
            message.cleanup();
            GameController.waitDialog();
        }
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method447();
        LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
        GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field413, BaseCanvas.Field157, 22, 17);
        LAF.Method413(10, 42, BaseCanvas.w - 20, (BaseCanvas.h - LAF.LOT_ITEM_HEIGHT) - 42);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static WidgetGroup Method12(AnimationMenu class61) {
        return class61.Field414;
    }
}

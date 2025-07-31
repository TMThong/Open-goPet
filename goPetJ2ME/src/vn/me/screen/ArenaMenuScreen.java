package vn.me.screen;

import defpackage.Class142;
import defpackage.Class75;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.WidgetGroup;
import vn.me.ui.Font;
import vn.me.screen.Screen;
import java.util.Vector;
import vn.me.network.Message;
import vn.me.ui.common.T;

/* renamed from: Class71  reason: default package */
 /* loaded from: gopet_repackage.jar:Class71.class */
public final class ArenaMenuScreen extends Screen {

    private final Vector Field446;
    private final Font Field447;
    private int Field448;

    public ArenaMenuScreen(Vector vector, int i) {
        super(true);
        this.Field447 = ResourceManager.boldFont;
        this.title = T.gL(T.REGISTER_FOR_REWARDS_STR);
        this.Field446 = vector;
        WidgetGroup class185 = new WidgetGroup(0, LAF.Field1292, BaseCanvas.w, BaseCanvas.h - (2 * LAF.Field1292));
        class185.isScrollableY = true;
        class185.isLoop = true;
        int i2 = 0;
        for (int i3 = 0; i3 < this.Field446.size(); i3++) {
            Class142 class142 = (Class142) this.Field446.elementAt(i3);
            Class75 class75 = new Class75(this, class142.Field977, class142.Field978, i3);
            class75.setPosition(2, i2);
            class185.addWidget(class75);
            i2 += class75.h;
        }
        this.container.addWidget(class185);
        class185.setViewMode(1);
        class185.setFocusWithParents(true);
        this.cmdLeft = GameController.Field467;
        this.cmdCenter = new Command(0, T.gL(T.GET_STR), new IActionListener() {
            ///////@Override
            public void actionPerformed(Object obj) {
                Message message = new Message(81);
                message.putByte(89);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                Method297(null);
            }
        });
        this.Field448 = i;
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static Font Method31(ArenaMenuScreen class71) {
        return class71.Field447;
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static int Method32(ArenaMenuScreen class71) {
        return class71.Field448;
    }
}

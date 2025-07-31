package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.Label;

/* renamed from: Class100  reason: default package */
/* loaded from: gopet_repackage.jar:Class100.class */
public final class Class100 extends Dialog implements IActionListener {
    private int Field612 = 94;
    private int Field613 = 126;
    private int Field614 = 24;
    public static int Field615;
    private Class101 Field616;
    private Class101 Field617;
    private Label Field618;

    public Class100() {
        this.h = this.Field613;
        this.w = BaseCanvas.Field161;
        int i = (BaseCanvas.w - this.w) >> 1;
        this.x = i;
        this.destX = i;
        this.destY = ((BaseCanvas.h - this.h) >> 1) + 6;
        this.y = -this.h;
        Field615 = 0;
        this.Field618 = new Label(ActorFactory.gL(422));
        this.Field616 = new Class101(this, (byte) 0);
        this.Field616.Field619 = (byte) 1;
        this.Field616.cmdCenter = new Command(1, ActorFactory.gL(337), this);
        this.Field616.h = 60;
        this.Field616.w = 45;
        this.Field616.destX = ((this.w >> 1) - this.Field616.w) >> 1;
        this.Field616.destY = ((this.h + this.Field614) - this.Field616.h) >> 1;
        this.Field617 = new Class101(this, (byte) 0);
        this.Field617.Field619 = (byte) 0;
        this.Field617.cmdCenter = new Command(2, ActorFactory.gL(337), this);
        this.Field617.h = 60;
        this.Field617.w = 45;
        this.Field617.destX = (this.w >> 1) + (((this.w >> 1) - this.Field617.w) >> 1);
        this.Field617.destY = ((this.h + this.Field614) - this.Field617.h) >> 1;
        Class101 class101 = this.Field616;
        this.Field617.border = 1;
        class101.border = 1;
        this.Field616.x = 0;
        this.Field617.x = this.Field612;
        this.defaultFocusWidget = this.Field616;
        this.cmdLeft = new Command(3, ActorFactory.gL(139), this);
        Class14 class14 = new Class14();
        addWidget(this.Field618, false);
        addWidget(class14, false);
        addWidget(this.Field616, false);
        addWidget(this.Field617, false);
        this.columns = 2;
        setViewMode(0);
        this.Field618.setMetrics(0, 4, this.w, LAF.Field1293);
        this.Field618.align = 17;
        class14.setMetrics(0, this.Field618.h + this.Field618.y + LAF.LOT_PADDING, this.w, 4);
        this.cmdRight = new Command(4, ActorFactory.gL(267), this);
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 1:
                Field615 = 0;
                GameController.Method60(new StringBuffer().append(ActorFactory.gL(130)).append(":").toString());
                return;
            case 2:
                Field615 = 1;
                GameController.Method60(new StringBuffer().append(ActorFactory.gL(130)).append(":").toString());
                return;
            case 3:
                GameController.Method50();
                return;
            case 4:
                GlobalService.Method259();
                return;
            default:
                return;
        }
    }
}

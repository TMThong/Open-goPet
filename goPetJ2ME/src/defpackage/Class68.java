package defpackage;

import vn.me.ui.interfaces.IListModel;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

/* renamed from: Class68  reason: default package */
 /* loaded from: gopet_repackage.jar:Class68.class */
public class Class68 implements IListModel {

    public byte Field435 = -1;
    public String Field436;
    public String Field437;
    public String Field438;
    public String Field439;
    public byte Field440;
    private String Field441;

    public Class68(byte b) {
        this.Field440 = b;
    }

    ///////@Override // defpackage.Class201
    public final Image getIcon() {
        if (BaseCanvas.w <= 128) {
            return null;
        }
        return GameResourceManager.Field598;
    }

    ///////@Override // defpackage.Class201
    public final String getTitle() {
        return this.Field436;
    }

    ///////@Override // defpackage.Class201
    public final String getDescription() {
        if (this.Field441 == null) {
            switch (this.Field440) {
                case 0:
                    this.Field441 = new StringBuffer(ActorFactory.gL(344)).append(this.Field437).append(" ").append(GlobalService.Field1008).append(" ").append(BaseCanvas.instance.midlet.getAppProperty("RefCode")).append(ActorFactory.gL(441)).append(this.Field438).toString();
                    break;
                case 1:
                    this.Field441 = ActorFactory.gL(212);
                    break;
                case 2:
                    this.Field441 = ActorFactory.gL(213);
                    break;
                case 6:
                    this.Field441 = ActorFactory.gL(185);
                    break;
                case 7:
                    this.Field441 = ActorFactory.gL(186);
                    break;
                case 8:
                    this.Field441 = ActorFactory.gL(596);
                    break;
                case 9:
                    this.Field441 = T.gL(T.CHANGE_GOLD_TO_GEM);
                    break;
            }
        }
        return this.Field441;
    }
}

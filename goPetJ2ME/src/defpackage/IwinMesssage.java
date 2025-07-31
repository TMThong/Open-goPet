package defpackage;

import vn.me.ui.interfaces.IListModel;
import javax.microedition.lcdui.Image;

/* renamed from: Class143  reason: default package */
/* loaded from: gopet_repackage.jar:Class143.class */
public final class IwinMesssage implements IListModel {
    public int Field979;
    public String Field980;
    public String Field981;
    public byte Field982 = 0;
    public boolean Field983;

    ///////@Override // defpackage.Class201
    public final Image getIcon() {
        return GameResourceManager.Field584[this.Field983 ? (char) 1 : (char) 0];
    }

    ///////@Override // defpackage.Class201
    public final String getTitle() {
        return this.Field982 == 5 ? "Hệ thống" : this.Field980;
    }

    ///////@Override // defpackage.Class201
    public final String getDescription() {
        return this.Field981;
    }
}

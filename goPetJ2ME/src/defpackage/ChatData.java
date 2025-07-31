package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;

/* renamed from: Class117  reason: default package */
/* loaded from: gopet_repackage.jar:Class117.class */
public final class ChatData {
    public String Field762;
    public String[] Field763;
    public int Field764;
    public int Field765;

    public ChatData(int i, String str, String str2) {
        this.Field765 = i;
        this.Field762 = str;
        this.Field763 = ResourceManager.defaultFont.wrap(str2 != null ? new StringBuffer().append(this.Field762).append(":_").append(str2).toString() : new StringBuffer().append(this.Field762).append(":_").toString(), (BaseCanvas.w - 40) - LAF.LOT_PADDING);
        if (this.Field763[0].length() > this.Field762.length()) {
            this.Field763[0] = this.Field763[0].substring(this.Field762.length(), this.Field763[0].length());
        }
        this.Field764 = (this.Field763.length * ResourceManager.defaultFont.getHeight()) - 1;
    }
}

package defpackage;

import vn.me.ui.interfaces.IListModel;
import javax.microedition.lcdui.Image;

/* renamed from: Class5  reason: default package */
/* loaded from: gopet_repackage.jar:Class5.class */
public final class MenuItemInfo implements IListModel {
    public int menuId;
    private String iconPath;
    public String title;
    private String desc;
    public boolean canSelect;
    public boolean Field7;
    public String Field8;
    public String Field9;
    public byte saleStatus;
    public boolean Field11;
    public String[] Field12;
    public int[] Field13;
    public byte[] Field14;

    public MenuItemInfo(int i, String str, String str2, String str3, byte b) {
        this.menuId = i;
        this.title = new StringBuffer("  ").append(str2).toString();
        this.desc = new StringBuffer("  ").append(str3).toString();
        this.iconPath = str;
        if (b == 0) {
            this.canSelect = false;
        } else {
            this.canSelect = true;
        }
    }

    ///////@Override // defpackage.Class201
    public final Image getIcon() {
        if (this.iconPath == null) {
            return null;
        }
        return GameResourceManager.loadResourceImg(this.iconPath, (byte) 3) != null ? GameResourceManager.loadResourceImg(this.iconPath, (byte) 3) : GameResourceManager.Method120();
    }

    ///////@Override // defpackage.Class201
    public final String getTitle() {
        return this.title;
    }

    ///////@Override // defpackage.Class201
    public final String getDescription() {
        return this.desc;
    }
}

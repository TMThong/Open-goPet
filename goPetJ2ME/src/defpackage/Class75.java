package defpackage;

import vn.me.ui.common.LAF;
import vn.me.screen.ArenaMenuScreen;
import vn.me.ui.Button;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import java.util.Vector;

/* renamed from: Class75  reason: default package */
 /* loaded from: gopet_repackage.jar:Class75.class */
public class Class75 extends Button {

    private final String Field486;
    private final String[] Field487;
    private int Field488;
    private final ArenaMenuScreen Field489;

    public Class75(ArenaMenuScreen class71, String str, String[] strArr, int i) {
        this.Field489 = class71;
        this.Field486 = str;
        this.Field488 = i;
        Vector vector = new Vector();
        for (int j = 0; j < strArr.length; j++) {
            String str2 = strArr[j];
            String[] Method331 = ArenaMenuScreen.Method31(class71).wrap(str2, (BaseCanvas.w - 2) - 60);
            Method331[0] = new StringBuffer("- ").append(Method331[0]).toString();
            for (int k = 0; k < Method331.length; k++) {
                String str3 = Method331[k];
                vector.addElement(str3);
            }
            
        }

        this.Field487 = new String[vector.size()];
        for (int i2 = 0; i2 < vector.size(); i2++) {
            this.Field487[i2] = (String) vector.elementAt(i2);
        }
        setSize(BaseCanvas.w - 4, (this.Field487.length * (ArenaMenuScreen.Method31(class71).getHeight() + 4)) + 4);
        if (i == ArenaMenuScreen.Method32(class71)) {
            this.Field1136 = true;
        }
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void paint() {
        LAF.Method413(0, 0, 50, 20);
        ArenaMenuScreen.Method31(this.Field489).drawString(BaseCanvas.g, this.Field486, 2, 2, 0);
        int i = 0;
        for (int i2 = 0; i2 < this.Field487.length; i2++) {
            ArenaMenuScreen.Method31(this.Field489).drawString(BaseCanvas.g, this.Field487[i2], 60, i, 0);
            i += ArenaMenuScreen.Method31(this.Field489).getHeight() + 4;
        }
    }

    ///////@Override 
    public final void paintBackground() {
        if (this.Field488 == ArenaMenuScreen.Method32(this.Field489)) {
            Effects.show1(BaseCanvas.g, LAF.Field1290, LAF.Field1289, 1, 1, this.w - 2, this.h - 2, false);
            return;
        }
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, this.w, this.h);
    }
}

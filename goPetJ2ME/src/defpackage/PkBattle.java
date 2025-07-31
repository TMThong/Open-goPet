package defpackage;

import vn.me.ui.MessageDialog;
import vn.me.ui.common.T;

/* renamed from: Class30  reason: default package */
/* loaded from: gopet_repackage.jar:Class30.class */
public final class PkBattle extends PetBattle {
    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method407(FightResult class37) {
        byte b = class37.petPos;
        setTurn(0, 0);
        switch (class37.action) {
            case 1:
                this.turnList.addElement(new BattleInfo(this, 0, b, null));
                break;
            case 4:
                this.turnList.addElement(new BattleInfo(this, 5, b, new Integer(class37.skillManaCost)));
                break;
        }
        int length = class37.effectId.length;
        for (int i = 0; i < length; i++) {
            int i2 = class37.effectTo[i];
            int i3 = class37.effectId[i];
            int i4 = class37.hpDiff[i];
            int i5 = class37.mpDiff[i];
            if (i3 < 0) {
                this.turnList.addElement(new BattleInfo(this, 4, i2, new int[]{i4, i5}));
            } else if (i3 >= 0 && i3 <= 2) {
                this.turnList.addElement(new BattleInfo(this, 1, i2, new int[]{i3, i4}));
            } else if (i3 >= 101 && i3 < 125) {
                this.turnList.addElement(new BattleInfo(this, 6, i2, new int[]{(i3 - 101) + 8, i4, i5}));
            } else if (i3 >= 125) {
                this.turnList.addElement(new BattleInfo(this, 11, i2, new int[]{i3, i4, i5}));
            }
        }
    }

    public final void show(boolean z) {
        int i = z ? 1 : 0;
        this.turnList.addElement(new BattleInfo(this, 10, i, new MessageDialog(z ? T.gL(T.GET_GIFT_WIN) : T.gL(T.GET_GIFT_LOSE), null, new Command(T.gL(T.GET_STR), new MEService(this)), Command.Field1325, 0)));
        this.turnList.addElement(new BattleInfo(this, 7, i, null));
    }
}

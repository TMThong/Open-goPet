package defpackage;

import java.util.Hashtable;
import java.util.Vector;

/* renamed from: Class127  reason: default package */
/* loaded from: gopet_repackage.jar:Class127.class */
public final class Class127 {
    private Vector Field825 = new Vector();
    private Hashtable Field826 = new Hashtable();

    public final void Method153(mCharacter class126) {
        this.Field825.addElement(class126);
        this.Field826.put(new Integer(class126.Id), class126);
    }

    public final mCharacter getChar(int i) {
        return (mCharacter) this.Field826.get(new Integer(i));
    }

    public final mCharacter Method155(int i) {
        return (mCharacter) this.Field825.elementAt(i);
    }

    public final int Method156() {
        return this.Field825.size();
    }

    public final void Method157(mCharacter class126) {
        this.Field825.removeElement(class126);
        this.Field826.remove(new Integer(class126.Id));
    }
}

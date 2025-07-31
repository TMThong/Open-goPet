package defpackage;

import vn.me.network.MobileClient;
import java.io.IOException;

/* JADX INFO: Access modifiers changed from: package-private */
/* renamed from: Class161  reason: default package */
/* loaded from: gopet_repackage.jar:Class161.class */
public final class Class161 implements Runnable {
    private final String Field1039;
    private final int Field1040;
    private final MobileClient Field1041;

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class161(MobileClient class160, String str, int i) {
        this.Field1041 = class160;
        this.Field1039 = str;
        this.Field1040 = i;
    }

    /* JADX WARN: Multi-variable type inference failed */
    /* JADX WARN: Type inference failed for: r0v1, types: [Class160] */
    /* JADX WARN: Type inference failed for: r0v18, types: [Class158] */
    /* JADX WARN: Type inference failed for: r0v2, types: [java.io.IOException] */
    ///////@Override // java.lang.Runnable
    public final void run() {
       
        Field1041.close();
        try {
            MobileClient.connect(this.Field1041, this.Field1039, this.Field1040);
            if (this.Field1041.messageHandler != null) {
               
                this.Field1041.messageHandler.Method209();
            }
        } catch (Exception e) {
            e.printStackTrace();
            if (this.Field1041.messageHandler != null) {
                this.Field1041.close();
                this.Field1041.messageHandler.Method210();
            }
        }
    }
}

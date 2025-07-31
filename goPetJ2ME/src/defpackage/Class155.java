package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.core.BaseCanvas;
import java.io.IOException;
import javax.microedition.io.Connector;
import javax.wireless.messaging.MessageConnection;
import javax.wireless.messaging.TextMessage;

/* JADX INFO: Access modifiers changed from: package-private */
/* renamed from: Class155  reason: default package */
/* loaded from: gopet_repackage.jar:Class155.class */
public final class Class155 implements Runnable {
    private MessageConnection Field1013 = null;
    private final String Field1014;
    private final String Field1015;
    private final IActionListener Field1016;
    private final IActionListener Field1017;

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class155(String str, String str2, IActionListener class200, IActionListener class2002) {
        this.Field1014 = str;
        this.Field1015 = str2;
        this.Field1016 = class200;
        this.Field1017 = class2002;
    }

    ///////@Override // java.lang.Runnable
    public final void run() {
        try {
            try {
                this.Field1013 = (MessageConnection) Connector.open(this.Field1014);
                TextMessage newMessage = (TextMessage) this.Field1013.newMessage("text");
                newMessage.setAddress(this.Field1014);
                newMessage.setPayloadText(new StringBuffer().append(this.Field1015).append(" ").append(GlobalService.Field1008).append(" ").append(BaseCanvas.instance.midlet.getAppProperty("RefCode")).toString());
                this.Field1013.send(newMessage);
                this.Field1016.actionPerformed(new Object[]{null, "smsOK"});
                if (this.Field1013 != null) {
                    try {
                        this.Field1013.close();
                    } catch (IOException unused) {
                    }
                }
            } catch (Throwable th) {
                if (this.Field1013 != null) {
                    try {
                        this.Field1013.close();
                    } catch (IOException unused2) {
                    }
                }
                 th.printStackTrace();
            }
        } catch (Exception unused3) {
            this.Field1017.actionPerformed(new Object[]{null, "smsFail"});
            if (this.Field1013 != null) {
                try {
                    this.Field1013.close();
                } catch (IOException unused4) {
                }
            }
        }
    }
}

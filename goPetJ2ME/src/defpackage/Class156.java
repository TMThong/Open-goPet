package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.screen.LoginScreen;
import java.io.IOException;
import java.io.InputStream;
import javax.microedition.io.Connector;
import javax.microedition.io.HttpConnection;

/* JADX INFO: Access modifiers changed from: package-private */
/* renamed from: Class156  reason: default package */
/* loaded from: gopet_repackage.jar:Class156.class */
public final class Class156 implements Runnable {
    private final IActionListener Field1018;
    private final IActionListener Field1019;

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class156(IActionListener class200, IActionListener class2002) {
        this.Field1018 = class200;
        this.Field1019 = class2002;
    }

    ///////@Override // java.lang.Runnable
    public final void run() {
        try {
            HttpConnection open = (HttpConnection) Connector.open(new StringBuffer().append(GlobalService.Field1009).append("?").append(System.currentTimeMillis()).toString());
            open.setRequestMethod("GET");
            open.setRequestProperty("Content-Type", "//text plain");
            open.setRequestProperty("Connection", "close");
            if (open.getResponseCode() == 200) {
                String str = "";
                InputStream openInputStream = open.openInputStream();
                int length = (int) open.getLength();
                if (length != -1) {
                    byte[] bArr = new byte[length];
                    openInputStream.read(bArr);
                    str = new String(bArr);
                }
                GlobalService.Method261(str);
                LoginScreen.Method131();
                if (this.Field1018 != null) {
                    this.Field1018.actionPerformed(GlobalService.serverList);
                    return;
                }
                return;
            }
        } catch (IOException unused) {
        }
        GameController.Method47(ActorFactory.gL(553));
        if (this.Field1019 != null) {
            this.Field1019.actionPerformed(null);
        }
    }
}

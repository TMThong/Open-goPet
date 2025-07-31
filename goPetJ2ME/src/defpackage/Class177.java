package defpackage;

import vn.me.ui.common.T;
import vn.me.ui.interfaces.IActionListener;
import javax.microedition.io.Connector;
import javax.microedition.lcdui.Alert;
import javax.microedition.lcdui.AlertType;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.Displayable;
import javax.microedition.lcdui.Form;
import javax.microedition.lcdui.Image;
import javax.microedition.lcdui.TextField;
import javax.wireless.messaging.MessageConnection;
import javax.wireless.messaging.TextMessage;

/* renamed from: Class177  reason: default package */
/* loaded from: gopet_repackage.jar:Class177.class */
public final class Class177 extends Form implements CommandListener {
    private TextField Field1165;
    private TextField Field1166;
    private  javax.microedition.lcdui.Command Field1167;
    private  javax.microedition.lcdui.Command Field1168;
    private Display Field1169;
    private IActionListener Field1170;

    public Class177(String str, String str2, IActionListener class200, Display display) {
        super(str);
        this.Field1165 = new TextField(new StringBuffer().append(T.gL(14)).append(":").toString(), "", 15, 3);
        this.Field1166 = new TextField(new StringBuffer().append(T.gL(10)).append(":").toString(), str2, 600, 0);
        this.Field1167 = new  javax.microedition.lcdui.Command(T.gL(9), 4, 0);
        this.Field1168 = new  javax.microedition.lcdui.Command(T.gL(8), 7, 1);
        this.Field1170 = class200;
        append(this.Field1165);
        append(this.Field1166);
        addCommand(this.Field1167);
        addCommand(this.Field1168);
        this.Field1169 = display;
        setCommandListener(this);
    }

    public final void commandAction( javax.microedition.lcdui.Command command, Displayable displayable) {
        MessageConnection messageConnection = null;
        if (command == this.Field1168) {
            this.Field1170.actionPerformed(new Object[]{new Command(-1, "", this.Field1170), this});
        } else if (command == this.Field1167) {
            String string = this.Field1165.getString();
            String string2 = this.Field1166.getString();
            if (string.equals("")) {
                Alert alert = new Alert(T.gL(12));
                alert.setString(T.gL(15));
                alert.setTimeout(2000);
                this.Field1169.setCurrent(alert);
                return;
            }
            try {
                messageConnection = (MessageConnection) Connector.open(new StringBuffer("sms://").append(string).toString());
            } catch (Exception unused) {
                Alert alert2 = new Alert("Alert");
                alert2.setString(T.gL(11));
                alert2.setTimeout(2000);
                this.Field1169.setCurrent(alert2);
            }
            try {
                TextMessage newMessage = (TextMessage) messageConnection.newMessage("text");
                newMessage.setAddress(new StringBuffer("sms://").append(string).toString());
                newMessage.setPayloadText(string2);
                messageConnection.send(newMessage);
            } catch (Exception unused2) {
                Alert alert3 = new Alert(T.gL(12), "", (Image) null, AlertType.INFO);
                alert3.setTimeout(-2);
                alert3.setString(T.gL(13));
                this.Field1169.setCurrent(alert3);
            }
        }
    }
}

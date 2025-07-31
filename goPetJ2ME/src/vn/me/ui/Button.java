package vn.me.ui;

import defpackage.Command;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.LAF;
import vn.me.ui.common.T;
import vn.me.ui.common.ResourceManager;
import javax.microedition.lcdui.Image;
import thong.sdk.ISoundManagerSDK;

/* renamed from: Class165  reason: default package */
/* loaded from: gopet_repackage.jar:Class165.class */
public class Button extends Label implements IActionListener {
    public int typeBtn;
    public boolean Field1048;
    public int Field1049;
    public int Field1050;
    private int Field1051;
    private int Field1052;

    public Button() {
        this(0);
    }

    public Button(int i) {
        this("");
        this.typeBtn = i;
        if (i == 1 || i == 2) {
            this.cmdCenter = new Command(0, T.gL(7), this);
        }
    }

    public Button(Image image) {
        this(0);
        setImage(image);
    }

    public Button(String str, Font class171) {
        super(str, class171);
        this.typeBtn = 0;
        this.Field1048 = false;
        this.Field1049 = 16777215;
        this.Field1050 = 16777215;
        this.Field1051 = -1;
        this.Field1052 = -1;
        this.isFocusable = true;
    }

    public Button(String str) {
        super(str);
        this.typeBtn = 0;
        this.Field1048 = false;
        this.Field1049 = 16777215;
        this.Field1050 = 16777215;
        this.Field1051 = -1;
        this.Field1052 = -1;
        this.isFocusable = true;
    }

    private Button(Command Command, Font class171) {
        this(Command == null ? "" : Command.Field1321, class171);
        this.cmdCenter = Command;
    }

    public Button(Command Command) {
        this(Command, ResourceManager.boldFont);
    }

    public final void Method279(Command Command) {
        this.text = Command == null ? "" : Command.Field1321;
        this.cmdCenter = Command;
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public void paintBackground() {
        LAF.Method415(this);
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public void paint() {
        super.paint();
        LAF.Method417(this);
    }

    ///////@Override // defpackage.Class184
    public void paintBorder() {
        LAF.Method416(this);
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public void onFocused() {
        super.onFocused();
        if ((this.typeBtn == 1 || this.typeBtn == 2) && this.image != null) {
            this.frameIndex = 1;
            this.border = 1;
        }
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public final void onLostFocused() {
        super.onLostFocused();
        if ((this.typeBtn == 1 || this.typeBtn == 2) && this.image != null) {
            this.frameIndex = 0;
            this.border = 0;
        }
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        if (this.typeBtn == 1) {
            this.Field1048 = !this.Field1048;
        }
        if (this.typeBtn != 2 || this.Field1048) {
            return;
        }
        ((ButtonGroup) this.parent).setSelected(this);
    }
}

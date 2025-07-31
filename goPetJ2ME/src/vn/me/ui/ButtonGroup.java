package vn.me.ui;

import thong.sdk.ISoundManagerSDK;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.Button;
import vn.me.ui.WidgetGroup;

/* renamed from: Class166  reason: default package */
/* loaded from: gopet_repackage.jar:Class166.class */
public final class ButtonGroup extends WidgetGroup {
    public int selectedIndex;
    public IActionListener buttonSelectionChanged;

    public ButtonGroup(int i, int i2, int i3, int i4) {
        super(0, 0, i3, i4);
        this.selectedIndex = -1;
    }

    private void clearSelection() {
        if (this.selectedIndex != -1) {
            if (this.selectedIndex < count()) {
                ((Button) getWidgetAt(this.selectedIndex)).Field1048 = false;
                ((Button) getWidgetAt(this.selectedIndex)).isPressed = false;
                ((Button) getWidgetAt(this.selectedIndex)).isFocused = false;
            }
            this.selectedIndex = -1;
        }
    }

    public final void setSelected(Button class165) {
        if (class165 == null) {
            clearSelection();
            return;
        }
        int length = this.children.length;
        do {
            length--;
            if (length < 0) {
                return;
            }
        } while (class165 != this.children[length]);
        ButtonGroup.this.setSelected(length);
    }

    public final void setSelected(int i) {
        if (this.selectedIndex == i) {
            return;
        }
        clearSelection();
        ((Button) getWidgetAt(i)).Field1048 = true;
        this.selectedIndex = i;
        this.defaultFocusWidget = (Button) getWidgetAt(i);
        if (this.buttonSelectionChanged != null) {
            this.buttonSelectionChanged.actionPerformed(this);
        }
    }
}

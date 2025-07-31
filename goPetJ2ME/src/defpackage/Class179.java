package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.ButtonGroup;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Widget;
import vn.me.ui.WidgetGroup;
import vn.me.ui.Font;

/* renamed from: Class179  reason: default package */
 /* loaded from: gopet_repackage.jar:Class179.class */
public final class Class179 extends WidgetGroup {

    public IActionListener Field1193;
    public ButtonGroup Field1194;
    public WidgetGroup Field1195;

    public Class179(int i, int i2, int i3, int i4) {
        this(0, 0, i3, i4, LAF.Field1293);
    }

    private Class179(int i, int i2, int i3, int i4, int i5) {
        super(i, i2, i3, i4);
        this.isScrollableX = false;
        this.isScrollableY = false;
        this.Field1194 = new ButtonGroup(0, 0, i3, i5);
        this.Field1195 = new WidgetGroup(0, i5 + 2, i3, (i4 - i5) - 2);
        addWidget(this.Field1194, false);
        addWidget(this.Field1195, false);
        this.Field1194.buttonSelectionChanged = new IActionListener() {
            public void actionPerformed(Object obj) {
                Field1195.hideAll();
                Widget Method350 = Field1195.getWidgetAt(Field1194.selectedIndex);
                if (Method350 != null) {
                    Method350.isVisible = true;
                    if (Method350.isFocusable) {
                        Field1195.defaultFocusWidget = Method350;
                    } else {
                        Field1195.defaultFocusWidget = null;
                    }
                }
                if (Field1193 != null) {
                    Field1193.actionPerformed(new Object[]{new Command(-1, null, Field1193), this});
                }
            }
        };
        this.Field1194.isScrollableX = true;
    }

    public Class179() {
        this(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    public final void Method294(String str, Widget class184) {
        Font class171 = ResourceManager.defaultFont;
        int i = 0;
        if (this.Field1194.count() > 0) {
            Class182 class182 = (Class182) this.Field1194.children[this.Field1194.children.length - 1];
            i = class182.x + class182.w;
        }
        final Class182 class1822 = new Class182(str);
        class1822.normalfont = class171;
        class1822.setMetrics(i, 0, Math.max(class1822.normalfont.getWidth(str), class1822.selectedfont.getWidth(str)) + (class1822.padding << 1), LAF.Field1293);
        switchToMe(0);
        if (class184 != null) {
            this.Field1194.addWidget(class1822, false);
            class184.setMetrics(0, 0, this.Field1195.w - (this.Field1195.padding << 1), this.Field1195.h - (this.Field1195.padding << 1));
            this.Field1195.addWidget(class184, false);
            this.Field1194.columns = this.Field1194.count() + 1;
            this.Field1194.preferredSize.width += class1822.w;
            class184.isVisible = false;
            class1822.onFocusAction = new IActionListener() {
                public void actionPerformed(Object obj) {
                    Field1194.setSelected(class1822);
                }
            };
        }
        doLayout();
    }

    private void switchToMe(int i) {
        if (i < 0 || i > this.Field1195.count()) {
            throw new IndexOutOfBoundsException(new StringBuffer("Index: ").append(i).toString());
        }
    }

    public final int Method128() {
        if (this.Field1194 != null) {
            return this.Field1194.selectedIndex;
        }
        return -1;
    }

    public final void Method79(int i) {
        switchToMe(0);
        this.Field1194.setSelected(0);
    }

    public final void Method104(int i) {
        switchToMe(i);
        BaseCanvas.getCurrentScreen().requestFocus(this.Field1194.getWidgetAt(i));
    }

    public final void paintBorder() {
        BaseCanvas.g.setColor(6990585);
        BaseCanvas.g.drawRect(0, this.Field1194.h, this.Field1195.w - 1, 1);
    }

    public final void onFocused() {
        super.onFocused();
        BaseCanvas.getCurrentScreen().requestFocus(this.Field1194);
    }

    public final void doLayout() {
        if (this.Field1194.isAutoFit) {
            int i = 0;
            for (int i2 = 0; i2 < this.Field1194.children.length; i2++) {
                Widget class184 = this.Field1194.children[i2];
                int i3 = i;
                this.Field1194.children[i2].x = i3;
                class184.destX = i3;
                i += this.Field1194.children[i2].w;
            }
            return;
        }
        int i4 = 0;
        for (int i5 = 0; i5 < this.Field1194.children.length; i5++) {
            this.Field1194.children[i5].w = (this.Field1194.w - (2 * (this.border + this.padding))) / this.Field1194.children.length;
            Widget class1842 = this.Field1194.children[i5];
            int i6 = i4;
            this.Field1194.children[i5].x = i6;
            class1842.destX = i6;
            i4 += this.Field1194.children[i5].w;
        }
    }
}

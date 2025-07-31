package vn.me.ui;

import vn.me.core.BaseCanvas;
import defpackage.Command;
import thong.sdk.ISoundManagerSDK;

public class WidgetGroup extends Widget {

    public static final int VIEW_MODE_FREE = 0;
    public static final int VIEW_MODE_LIST = 1;
    public static final int VIEW_MODE_GRID = 2;
    protected int viewMode;
    public int columns;
    public Widget[] children;
    public boolean isLoop;
    public int spacing;
    public Widget defaultFocusWidget;
    public boolean isAutoFit;

    public WidgetGroup(int x, int y, int w, int h) {
        this(x, y, w, h, 0);
    }

    public WidgetGroup(int x, int y, int w, int h, int layout) {
        super(x, y, w, h);
        this.columns = 1;
        this.isLoop = false;
        this.spacing = 1;
        this.isAutoFit = false;
        this.viewMode = layout;
        this.initialize();
    }

    public WidgetGroup() {
        this(0, 0, 1, 1, 0);
    }

    public WidgetGroup(int layout) {
        this(0, 0, 1, 1, layout);
    }

    private void initialize() {
        this.children = new Widget[0];
    }

    public void addWidget(Widget widget, boolean isLayout) {
        Widget[] tmpChildren = new Widget[this.children.length + 1];
        System.arraycopy(this.children, 0, tmpChildren, 0, this.children.length);
        tmpChildren[tmpChildren.length - 1] = widget;
        this.children = tmpChildren;
        widget.parent = this;
        if (this.defaultFocusWidget == null && widget.isFocusable) {
            this.defaultFocusWidget = widget;
        }

        if (isLayout) {
            this.doLayout();
        }

    }

    public void addWidget(Widget wid) {
        this.addWidget(wid, false);
    }

    public void removeWidget(Widget widget) {
        if (widget != null && this.children.length != 0) {
            if (widget == this.defaultFocusWidget) {
                this.defaultFocusWidget = null;
            }

            Widget[] tmpChildren = new Widget[this.children.length - 1];
            boolean isFound = false;

            for (int i = 0; i < tmpChildren.length; ++i) {
                if (this.children[i] == widget) {
                    isFound = true;
                }

                tmpChildren[i] = this.children[isFound ? i + 1 : i];
            }

            if (isFound || widget == this.children[this.children.length - 1]) {
                this.children = tmpChildren;
            }

            this.doLayout();
        }
    }

    public void removeAll() {
        this.defaultFocusWidget = null;
        this.children = new Widget[0];
    }

    private void findOtherFocus() {
        int index = this.getFocusedIndex();
        if (this.isLoop) {
            this.findNextFocus(true, index, 1);
        } else {
            this.isLoop = true;
            this.findNextFocus(true, index, 1);
            this.isLoop = false;
        }

    }

    public void hideWidget(Widget widget) {
        if (this.isVisible) {
            if (widget.isFocused && this.children.length > 1) {
                this.findOtherFocus();
            } else {
                widget.setFocused(false);
            }

            widget.isVisible = false;
        }

    }

    public Widget getWidgetAt(int index) {
        return this.children.length != 0 && index >= 0 && index < this.children.length ? this.children[index] : null;
    }

    public void setViewMode(int mode) {
        this.viewMode = mode;
        this.doLayout();
    }

    public void doLayout() {
        switch (this.viewMode) {
            case 1:
                this.doListLayout();
                break;
            case 2:
                this.doGridLayout();
        }

    }

    private void doGridLayout() {
        if (this.children.length != 0) {
            int spacingW = 0;
            int maxW = 0;
            int maxH = 0;
            int numberOfWidget = this.children.length;
            int rows = numberOfWidget;

            while (true) {
                --rows;
                if (rows < 0) {
                    if (this.columns > 0) {
                        if (this.isAutoFit && this.w > 0) {
                            maxW = (this.w - (this.padding + this.border << 1)) / this.columns;
                            this.preferredSize.width = this.columns * maxW + (this.columns + 1) * this.spacing;
                            this.w = this.preferredSize.width + 2 * this.padding;
                        }

                        this.preferredSize.width = this.columns * maxW + (this.columns + 1) * this.spacing;
                    } else {
                        rows = this.w - 2 * this.padding;
                        this.columns = Math.max(rows / maxW, 1);
                        spacingW = (rows - maxW * this.columns) / (this.columns + 1);
                    }

                    rows = numberOfWidget / this.columns;
                    if (numberOfWidget % this.columns != 0) {
                        ++rows;
                    }

                    this.preferredSize.height = rows * maxH + (rows - 1) * this.spacing;

                    for (int i = 0; i < numberOfWidget; ++i) {
                        int xP = i % this.columns;
                        int yP = i / this.columns;
                        int wx = xP * maxW;
                        int wy = yP * maxH;
                        Widget item = this.getWidgetAt(i);
                        item.setMetrics(wx + (xP + 1) * spacingW, wy + yP * this.spacing, maxW, maxH);
                    }

                    if (this.isAutoFit) {
                        this.w = this.preferredSize.width + 2 * this.padding;
                        this.h = this.preferredSize.height + 2 * this.padding;
                    }

                    return;
                }

                Widget wd = this.getWidgetAt(rows);
                maxW = Math.max(maxW, wd.w);
                maxH = Math.max(maxH, wd.h);
            }
        }
    }

    protected void doListLayout() {
        this.preferredSize.height = 0;
        if (this.children.length != 0) {
            this.columns = 1;
            int amount = this.children.length;
            int maxW = 0;
            int wy;
            if (this.isAutoFit) {
                for (wy = 0; wy < amount; ++wy) {
                    if (maxW < this.children[wy].w) {
                        maxW = this.children[wy].w;
                    }
                }

                this.w = this.preferredSize.width = maxW + (this.padding + this.border << 1);
            } else if (this.w > 0) {
                maxW = this.w - (this.padding + this.border << 1);
            }

            wy = 0;
            int wx = 0;

            for (int i = 0; i < amount; ++i) {
                Widget wd = this.getWidgetAt(i);
                wd.setMetrics(wx, wy, maxW, wd.h);
                if (wd instanceof WidgetGroup) {
                    ((WidgetGroup) wd).doLayout();
                }

                wy = wd.y + wd.h + this.spacing;
            }

            this.preferredSize.height = wy - this.spacing;
            if (this.isAutoFit) {
                Widget var10000;
                if (this.parent != null && this.parent instanceof WidgetGroup && ((WidgetGroup) this.parent).isAutoFit) {
                    var10000 = this.parent;
                    var10000.h -= this.h;
                }

                this.h = this.preferredSize.height + (this.padding + this.border << 1);
                if (this.parent != null && this.parent instanceof WidgetGroup && ((WidgetGroup) this.parent).isAutoFit) {
                    var10000 = this.parent;
                    var10000.h += this.h;
                }
            } else if (this.h == 0) {
                this.h = this.preferredSize.height + (this.padding + this.border << 1);
            }

        }
    }

    public int count() {
        return this.children == null ? 0 : this.children.length;
    }

    public Widget getFocusedWidget(boolean isAtom) {
        if (this.children == null) {
            return this;
        } else {
            int count = this.children.length;
            int i = count;

            Widget widget;
            do {
                --i;
                if (i < 0) {
                    return this.isFocusable ? this : null;
                }

                widget = this.children[i];
            } while (!widget.isFocused);

            if (isAtom && widget instanceof WidgetGroup) {
                return ((WidgetGroup) widget).getFocusedWidget(true);
            } else {
                return widget;
            }
        }
    }

    public int getFocusedIndex() {
        if (this.children != null) {
            int i = this.children.length;

            while (true) {
                --i;
                if (i < 0) {
                    break;
                }

                Widget widget = this.children[i];
                if (widget.isFocused) {
                    return i;
                }
            }
        }

        return -1;
    }

    ///////@Override
    public void update() {
        super.update();
        if (this.children != null) {
            int i = this.children.length;

            while (true) {
                --i;
                if (i < 0) {
                    break;
                }

                if (this.children[i].isVisible) {
                    this.children[i].update();
                }
            }
        }

    }

    ///////@Override
    public void paint() {
        BaseCanvas.g.translate(-this.scrollX, -this.scrollY);
        if (this.children != null) {
            Widget focusWid = this.getFocusedWidget(false);

            for (int i = 0; i < this.children.length; ++i) {
                if (this.children[i].isVisible && focusWid != this.children[i] && !(this.children[i] instanceof Menu)) {
                    this.children[i].paintComponent();
                }
            }

            if (focusWid != this && focusWid != null && !(focusWid instanceof Menu)) {
                focusWid.paintComponent();
            }
        }
        BaseCanvas.g.translate(this.scrollX, this.scrollY);
    }

    ///////@Override
    public void paintBackground() {
        super.paintBackground();
    }

    public boolean findNextFocus(boolean isForward, int startIndex, int step) {
        if (this.children != null && this.children.length != 0) {
            if (!this.hasFocusableChildren()) {
                return false;
            } else {
                int index = startIndex + (isForward ? step : -step);
                if (index < 0) {
                    if (this.isLoop) {
                        index = this.children.length - 1;
                    } else {
                        index = startIndex;
                    }
                } else if (index >= this.children.length) {
                    if (this.isLoop) {
                        index = 0;
                    } else {
                        index = startIndex;
                    }
                }

                if (startIndex == index || startIndex > 0 && this.children[startIndex] == this.children[index]) {
                    return false;
                } else {
                    Widget widget = this.children[index];
                    if (widget.isVisible && widget.isFocusable) {
                        if (widget instanceof WidgetGroup) {
                            Widget focW = ((WidgetGroup) widget).findDefaultfocusableWidget();
                            focW.requestFocus();
                        } else {
                            widget.requestFocus();
                        }

                        return true;
                    } else {
                        return this.findNextFocus(isForward, index, step);
                    }
                }
            }
        } else {
            return false;
        }
    }

    private boolean hasFocusableChildren() {
        for (int i = 0; i < this.children.length; ++i) {
            if (this.children[i].isFocusable) {
                return true;
            }
        }
        return false;
    }

    public Widget findDefaultfocusableWidget() {
        if (this.defaultFocusWidget != null && this.defaultFocusWidget.isVisible && this.defaultFocusWidget.isFocusable) {
            return this.defaultFocusWidget instanceof WidgetGroup ? ((WidgetGroup) this.defaultFocusWidget).findDefaultfocusableWidget() : this.defaultFocusWidget;
        } else {
            return this;
        }
    }

    public boolean checkKeys(int type, int keyCode) {
        boolean isChangeFocused = false;
        boolean isHandle = false;
        Widget focusedWidget = this.getFocusedWidget(false);
        if (focusedWidget != this && focusedWidget != null) {
            isHandle = focusedWidget.checkKeys(type, keyCode);
            if (isHandle) {
                return true;
            }
        }

        if (type == 0 && keyCode == -3 && this.viewMode != 1) {
            isChangeFocused = this.findNextFocus(false, this.getFocusedIndex(), 1);
        } else if (type == 0 && keyCode == -4 && this.viewMode != 1) {
            isChangeFocused = this.findNextFocus(true, this.getFocusedIndex(), 1);
        } else if (type == 0 && keyCode == -2) {
            isChangeFocused = this.findNextFocus(true, this.getFocusedIndex(), this.columns);
        } else if (type == 0 && keyCode == -1) {
            isChangeFocused = this.findNextFocus(false, this.getFocusedIndex(), this.columns);
        }

        return isChangeFocused;
    }

    public boolean pointerPressed(int x, int y) {
        
        Widget widget = this.getFocusableWidgetAt(x, y);
        if (widget == this) {
            ISoundManagerSDK.playSoundEffect("s_button");
            return super.pointerPressed(x, y);
        } else {
            return widget != null ? widget.pointerPressed(x, y) : false;
        }
        
    }

    public Widget getFocusableWidgetAt(int x, int y) {
        int count = this.children.length;

        for (int i = count - 1; i >= 0; --i) {
            Widget cmp = this.getWidgetAt(i);
            if (cmp.isVisible && cmp.contains(x, y)) {
                if (cmp instanceof WidgetGroup) {
                    return ((WidgetGroup) cmp).getFocusableWidgetAt(x, y);
                }

                if (cmp.isFocusable) {
                    return cmp;
                }
            }
        }

        if (this.isFocusable && this.contains(x, y)) {
            return this;
        } else {
            return null;
        }
    }

    public boolean isScrollableY() {
        return this.isScrollableY;
    }

    public boolean isScrollableX() {
        return this.isScrollableX;
    }

    public void hideAll() {
        int count = this.count();
        int i = count;

        while (true) {
            --i;
            if (i < 0) {
                this.defaultFocusWidget = null;
                return;
            }

            Widget wg = this.getWidgetAt(i);
            wg.isVisible = false;
            wg.isFocused = false;
        }
    }

    public void setChildrenCommand(Command cmd) {
        int count = this.count();
        if (this.children != null) {
            int i = count;

            while (true) {
                --i;
                if (i < 0) {
                    break;
                }

                this.getWidgetAt(i).cmdCenter = cmd;
            }
        }

    }

    public void scrollComponentToVisible(Widget c) {
        this.scrollTo(c.x, c.y, c.w, c.h);
    }

    public void setMetrics(int x, int y, int w, int h) {
        super.setMetrics(x, y, w, h);
        this.doLayout();
    }

    public int getMaxContentWidth() {
        return this.w - (this.padding + this.border << 1);
    }

    public boolean containWidget(Widget childWidget) {
        for (int i = this.children.length - 1; i >= 0; --i) {
            if (this.children[i] == childWidget) {
                return true;
            }
        }

        return false;
    }

    public int[] getMinSize() {
        return new int[]{this.w, this.h};
    }
}

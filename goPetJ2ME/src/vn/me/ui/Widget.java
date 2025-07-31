package vn.me.ui;

import vn.me.core.BaseCanvas;
import defpackage.Command;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.LAF;
import vn.me.screen.Screen;
import vn.me.ui.geom.Dimension;

/* renamed from: Class184  reason: default package */
 /* loaded from: gopet_repackage.jar:Class184.class */
public class Widget {

    public static Command cmdNull = new Command(-1, "", null);
    public int x;
    public int y;
    public int w;
    public int h;
    public boolean isFocused;
    public boolean isDragActivated;
    public boolean isVisible;
    public Command cmdLeft;
    public Command cmdCenter;
    public Command cmdRight;
    public int destX;
    public int destY;
    public int border;
    public int padding;
    public boolean isFocusable;
    public Widget parent;
    public int id;
    private int lastScrollX;
    public int scrollX;
    public int scrollY;
    public int destScrollX;
    public int destScrollY;
    public int scrollVy;
    public int scrollDy;
    public int scrollVx;
    public int scrollDx;
    public boolean isScrollableX;
    public boolean isScrollableY;
    public Dimension preferredSize;
    public boolean isPressed;
    public IActionListener onFocusAction;
    public int speed;
    public int scrollBarH;
    public int scrollBarY;
    private int dy;
    private int vy;
    private int dx;
    private int vx;
    private long lastDragTicker;
    private long lastDragY;
    private long lastDragX;
    private int lastScrollY;

    public Widget() {
        this.isFocused = false;
        this.isDragActivated = false;
        this.isVisible = true;
        this.border = 0;
        this.padding = 0;
        this.isFocusable = true;
        this.speed = 3;
        this.lastDragTicker = 0L;
        this.lastDragY = 0L;
        this.preferredSize = new Dimension();
    }

    public Widget(int i, int i2, int i3, int i4) {
        this.isFocused = false;
        this.isDragActivated = false;
        this.isVisible = true;
        this.border = 0;
        this.padding = 0;
        this.isFocusable = true;
        this.speed = 3;
        this.lastDragTicker = 0L;
        this.lastDragY = 0L;
        this.x = i;
        this.y = i2;
        this.w = i3;
        this.h = i4;
        this.destX = i;
        this.destY = i2;
        this.preferredSize = new Dimension(i3, i4);
    }

    public void paint() {
    }

    public void paintBackground() {
    }

    public void paintComponent() {
        int clipWidth = BaseCanvas.g.getClipWidth();
        int clipHeight = BaseCanvas.g.getClipHeight();
        int clipX = BaseCanvas.g.getClipX();
        int clipY = BaseCanvas.g.getClipY();
        if (this.w <= 0 || this.h <= 0 || clipX + clipWidth >= this.x && clipX <= this.x + this.w && clipY + clipHeight >= this.y && clipY <= this.y + this.h) {
            BaseCanvas.g.translate(this.x, this.y);
            if (this.w > 0 && this.h > 0) {
                BaseCanvas.g.clipRect(0, 0, this.w, this.h);
            }
            this.paintBackground();
            int borderClipW = BaseCanvas.g.getClipWidth();
            int borderClipH = BaseCanvas.g.getClipHeight();
            int borderClipX = BaseCanvas.g.getClipX();
            int borderClipY = BaseCanvas.g.getClipY();
            int totalPadding = this.padding + this.border;
            BaseCanvas.g.clipRect(totalPadding, totalPadding, this.w - (totalPadding << 1), this.h - (totalPadding << 1));
            BaseCanvas.g.translate(totalPadding, totalPadding);
            this.paint();
            BaseCanvas.g.translate(-totalPadding, -totalPadding);
            BaseCanvas.g.setClip(borderClipX, borderClipY, borderClipW, borderClipH);
            this.paintBorder();
            BaseCanvas.g.translate(-this.x, -this.y);
            BaseCanvas.g.setClip(clipX, clipY, clipWidth, clipHeight);
        }
    }

    public void moveCamera() {
        if (this.scrollY != this.destScrollY) {
            this.scrollVy = this.destScrollY - this.scrollY << 2;
            this.scrollDy += this.scrollVy;
            this.scrollY += this.scrollDy >> 4;
            this.scrollDy &= 15;
            this.calcScrollSize();
        }

        if (this.scrollX != this.destScrollX) {
            this.scrollVx = this.destScrollX - this.scrollX << 2;
            this.scrollDx += this.scrollVx;
            this.scrollX += this.scrollDx >> 4;
            this.scrollDx &= 15;
        }

    }

    public void update() {
        if (this.destY != this.y) {
            this.vy = this.destY - this.y << this.speed;
            this.dy += this.vy;
            this.y += this.dy >> 4;
            this.dy &= 15;
        }

        if (this.destX != this.x) {
            this.vx = this.destX - this.x << this.speed;
            this.dx += this.vx;
            this.x += this.dx >> 4;
            this.dx &= 15;
        }

        if (this.isScrollable()) {
            this.moveCamera();
        }

    }

    public boolean checkKeys(int type, int keyCode) {
        if (keyCode == -5) {
            if (type == 1) {
                this.isPressed = false;
                if (this.cmdCenter != null) {
                    this.cmdCenter.actionPerformed(new Object[]{this.cmdCenter, this});
                    return true;
                }
            } else {
                this.isPressed = true;
            }
        } else if (type == 1 && keyCode == -6) {
            if (this.cmdLeft != null) {
                this.cmdLeft.actionPerformed(new Object[]{this.cmdLeft, this});
                return true;
            }
        } else if (type == 1 && keyCode == -7 && this.cmdRight != null) {
            this.cmdRight.actionPerformed(new Object[]{this.cmdRight, this});
            return true;
        }

        return false;
    }

    public boolean pointerPressed(int x, int y) {
        this.lastDragTicker = System.currentTimeMillis();
        this.lastDragY = (long) y;
        this.lastDragX = (long) x;
        this.clearDrag();
        this.isPressed = true;
        if (this.isFocusable) {
            this.requestFocus();
        }

        return true;
    }

    public boolean pointerReleased(int x, int y) {
        if (this.isDragActivated) {
            this.isPressed = false;
            this.isDragActivated = false;
            boolean shouldScrollX = this.chooseScrollXOrY(x, y);
            int s;
            if (!shouldScrollX) {
                s = (int) ((long) y - this.lastDragY);
                long deltaTime = System.currentTimeMillis() - this.lastDragTicker + 1L;
                int viewH;
                if (deltaTime != 1L) {
                    viewH = (int) ((long) (s << 11) / (deltaTime * deltaTime));
                    int vT = (int) ((long) viewH * deltaTime);
                    long time = (long) (vT / ((viewH << 1) + 1));
                    s = (int) ((long) vT * time - ((long) viewH * time * time >> 1) >> 8);
                } else {
                    s = (int) (((long) y - this.lastDragY + 1L) * 250L / Math.abs((long) y - this.lastDragY + 1L));
                }

                if (this.scrollY < 0) {
                    this.destScrollY = 0;
                    return true;
                }

                viewH = this.h - (this.padding + this.border << 1);
                if (this.scrollY > this.preferredSize.height - viewH) {
                    this.destScrollY = this.preferredSize.height - viewH;
                } else {
                    this.destScrollY -= s;
                }

                if (this.destScrollY > this.preferredSize.height - viewH) {
                    this.destScrollY = this.preferredSize.height - viewH;
                } else if (this.destScrollY < 0) {
                    this.destScrollY = 0;
                }

                return true;
            }

            if (this.scrollX < 0) {
                this.destScrollX = 0;
                return true;
            }

            s = this.w - 2 * this.padding;
            if (this.scrollX > this.preferredSize.width - s) {
                this.destScrollX = this.preferredSize.width - s;
                if (this.destScrollX < 0) {
                    this.destScrollX = 0;
                }

                return true;
            }
        } else {
            if (this.isPressed) {
                this.isPressed = false;
                if (this.cmdCenter != null) {
                    this.cmdCenter.actionPerformed(new Object[]{this.cmdCenter, this});
                    return true;
                }
            }

            this.isPressed = false;
        }

        return false;
    }

    public boolean isPressed() {
        return this.isPressed;
    }

    public boolean pointerDragged(int x, int y) {
        if (this.isScrollable()) {
            long currentTime = System.currentTimeMillis();
            if (currentTime - this.lastDragTicker > 500L) {
                this.lastDragTicker = currentTime;
                this.lastDragY = (long) y;
                this.lastDragX = (long) x;
            }

            if (!this.isDragActivated) {
                this.lastScrollY = y;
                this.lastScrollX = x;
                this.isDragActivated = true;
                ((Screen) BaseCanvas.getCurrentScreen()).draggedWidget = this;
                return true;
            } else {
                int scroll;
                if (this.isScrollableY()) {
                    scroll = this.scrollY + (this.lastScrollY - y);
                    this.destScrollY = this.scrollY = scroll;
                    this.calcScrollSize();
                }

                if (this.isScrollableX()) {
                    scroll = this.scrollX + (this.lastScrollX - x);
                    this.destScrollX = this.scrollX = scroll;
                }

                this.lastScrollY = y;
                this.lastScrollX = x;
                return true;
            }
        } else {
            if (this.parent != null) {
                this.isPressed = false;
                this.parent.pointerDragged(x, y);
            }

            return false;
        }
    }

    public void setFocused(boolean focused) {
        this.isFocused = focused;
    }

    public void setFocusWithParents(boolean focused) {
        this.setFocused(focused);
        if (this.parent != null && this.parent.isFocusable) {
            this.parent.setFocusWithParents(focused);
        }

    }

    public void clearFocus() {
        this.isFocused = false;
        if (this.parent != null) {
            this.parent.isFocused = false;
        }

    }

    public static boolean containPoint(Widget wid, int x, int y) {
        return wid.x < x && wid.y < y && wid.x + wid.w > x && wid.y + wid.h > y;
    }

    public void onFocused() {
        if (this.onFocusAction != null) {
            this.onFocusAction.actionPerformed(new Object[]{null, this});
        }

    }

    public boolean contains(int x, int y) {
        int absX = this.getAbsoluteX() + this.scrollX;
        int absY = this.getAbsoluteY() + this.scrollY;
        return x >= absX && x < absX + this.w && y >= absY && y < absY + this.h;
    }

    public int getAbsoluteX() {
        int xx = this.x - this.scrollX + this.border + this.padding;
        if (this.parent != null) {
            xx += this.parent.getAbsoluteX();
        }

        return xx;
    }

    public int getAbsoluteY() {
        int yy = this.y - this.scrollY + this.border + this.padding;
        if (this.parent != null) {
            yy += this.parent.getAbsoluteY();
        }

        return yy;
    }

    public void onLostFocused() {
    }

    public void clearDrag() {
        this.isDragActivated = false;
        if (this.parent != null) {
            this.parent.clearDrag();
        }

    }

    public boolean isScrollable() {
        return this.isScrollableX() || this.isScrollableY();
    }

    public boolean isScrollableX() {
        return this.isScrollableX;
    }

    public boolean isScrollableY() {
        return this.isScrollableY;
    }

    public boolean chooseScrollXOrY(int x, int y) {
        boolean ix = this.isScrollableX();
        boolean iy = this.isScrollableY();
        if (ix && iy) {
            return Math.abs(BaseCanvas.instance.initialPressX - x) > Math.abs(BaseCanvas.instance.initialPressY - y);
        } else {
            return ix;
        }
    }

    public Widget getDragWidget() {
        if (this.isDragActivated) {
            return this;
        } else {
            if (this instanceof WidgetGroup) {
                WidgetGroup wg = (WidgetGroup) this;
                int count = wg.count();
                int i = count - 1;
                if (i >= 0) {
                    return wg.getWidgetAt(i).getDragWidget();
                }
            }

            return null;
        }
    }

    protected Widget getRootWidget() {
        return this.parent == null ? this : this.parent.getRootWidget();
    }

    public Command getLeftCommand() {
        if (this.cmdLeft != null) {
            return this.cmdLeft;
        } else {
            return this.parent != null ? this.parent.getLeftCommand() : null;
        }
    }

    public Command getRightCommand() {
        if (this.cmdRight != null) {
            return this.cmdRight;
        } else {
            return this.parent != null ? this.parent.getRightCommand() : null;
        }
    }

    public Command getCenterCommand() {
        if (this.cmdCenter != null) {
            return this.cmdCenter;
        } else {
            return this.parent != null ? this.parent.getCenterCommand() : null;
        }
    }

    public void scrollTo(int sx, int sy, int sw, int sh) {
        int viewW;
        if (this.isScrollableY()) {
            viewW = this.h - (this.padding + this.border << 1);
            if (sy < this.destScrollY) {
                this.destScrollY = sy;
            } else if (sy + sh > this.destScrollY + viewW) {
                this.destScrollY = sy + sh - viewW;
                if (this.destScrollY >= this.preferredSize.height - viewW) {
                    this.destScrollY = this.preferredSize.height - viewW;
                }
            }

            if (this.destScrollY < 0) {
                this.destScrollY = 0;
            }
        }

        if (this.isScrollableX()) {
            viewW = this.w - (this.padding + this.border << 1);
            if (sx < this.destScrollX) {
                this.destScrollX = sx;
            } else if (sx + sw > this.destScrollX + viewW) {
                this.destScrollX = sx + sw - viewW;
                if (this.destScrollX > this.preferredSize.width - viewW) {
                    this.destScrollX = this.preferredSize.width - viewW;
                }
            }

            if (this.destScrollX < 0) {
                this.destScrollX = 0;
            }
        }

    }

    public void scrollToEnd() {
        int viewH = this.h - (this.padding << 1);
        this.destScrollY = this.preferredSize.height - viewH;
        if (this.destScrollY < 0) {
            this.destScrollY = 0;
        }

    }

    public void requestFocus() {
        ((Screen) BaseCanvas.getCurrentScreen()).requestFocus(this);
    }

    public void setMetrics(int x, int y, int w, int h) {
        this.w = w;
        this.h = h;
        this.setPosition(x, y);
    }

    public void setPosition(int x, int y) {
        this.x = x;
        this.y = y;
        this.destX = x;
        this.destY = y;
    }

    public void setSize(int w, int h) {
        this.w = w;
        this.h = h;
    }

    private void calcScrollSize() {
        if (this.preferredSize.height != 0) {
            this.scrollBarY = this.scrollY * (this.h - (this.border << 1)) / this.preferredSize.height + this.border;
            this.scrollBarH = (this.h - (this.border << 1)) * (this.h - (this.border << 1)) / this.preferredSize.height;
        }
    }

    protected void paintBorder() {
        LAF.paintScrollBar(this);
    }

    public void setPreferredSize(int width, int height) {
        this.preferredSize = new Dimension(width, height);
    }
}

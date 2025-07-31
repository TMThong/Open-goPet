package vn.me.screen;

import defpackage.AnimationEffect;
import vn.me.core.BaseCanvas;
import vn.me.ui.EditField;
import vn.me.ui.Font;
import vn.me.ui.Label;
import vn.me.ui.common.ResourceManager;
import defpackage.Command;
import vn.me.ui.Dialog;
import vn.me.ui.common.Effects;
import vn.me.ui.EmotionDialog;
import vn.me.ui.common.LAF;
import vn.me.ui.Menu;
import vn.me.ui.common.T;
import vn.me.ui.Widget;
import vn.me.ui.WidgetGroup;
import java.util.Stack;
import java.util.Vector;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;
import vn.me.ui.interfaces.IActionListener;

/* renamed from: Screen  reason: default package */
 /* loaded from: gopet_repackage.jar:Screen.class */
public class Screen implements IActionListener {

    public boolean Field1171;
    public Stack listScreen;
    public String screenId;
    public Command cmdRight;
    public Command cmdCenter;
    public Command cmdLeft;
    private Widget btnLeft;
    private Widget btnCenter;
    private Widget btnRight;
    public static Image imgSoft;
    public EditField chatEditField;
    public Menu currentMenu;
    public WidgetGroup container;
    public boolean Field1184;
    public Font Field1185;
    public Widget draggedWidget;
    public static Dialog currentDialog;
    public static Label Field1189;
    private boolean Field1191;
    public String title;
    public static Vector dialogs = new Vector();
    public static Vector animationEffects = new Vector();

    public Screen() {
        this(false);
    }

    public Screen(boolean isUseMUI) {
        this.Field1171 = false;
        this.listScreen = new Stack();
        this.Field1184 = false;
        this.Field1191 = false;
        if (isUseMUI) {
            this.container = new WidgetGroup(0, 0, BaseCanvas.w, BaseCanvas.h);
            this.Field1191 = true;
            createSoftBarImage();
            this.btnLeft = new Widget();
            this.btnLeft.setMetrics(0, BaseCanvas.h - LAF.Field1293, BaseCanvas.w / 3, LAF.Field1293);
            this.btnCenter = new Widget();
            this.btnCenter.setMetrics(BaseCanvas.w / 3, BaseCanvas.h - LAF.Field1293, BaseCanvas.w / 3, LAF.Field1293);
            this.btnRight = new Widget();
            this.btnRight.setMetrics((BaseCanvas.w << 1) / 3, BaseCanvas.h - LAF.Field1293, BaseCanvas.w / 3, LAF.Field1293);
            this.btnLeft.cmdCenter = new Command(0, "", this);
            this.btnCenter.cmdCenter = new Command(1, "", this);
            this.btnRight.cmdCenter = new Command(2, "", this);
            this.btnLeft.isFocusable = false;
            this.btnCenter.isFocusable = false;
            this.btnRight.isFocusable = false;
        }
        this.Field1185 = ResourceManager.boldFont;
    }

    public final void Method296() {
        for (int i = 0; i < dialogs.size(); i++) {
            Dialog Dialog = (Dialog) dialogs.elementAt(i);
            Dialog.update();
            if (Dialog.Field1056) {
                dialogs.removeElement(Dialog);
                if (dialogs.isEmpty()) {
                    currentDialog = null;
                    if (this.container != null) {
                        this.container.findDefaultfocusableWidget().requestFocus();
                    }
                } else {
                    Dialog Dialog2 = (Dialog) dialogs.lastElement();
                    currentDialog = Dialog2;
                    Dialog2.findDefaultfocusableWidget().requestFocus();
                }
            }
        }
        if (Field1189 != null) {
            Field1189.update();
        }
        int i2 = 0;
        while (i2 < animationEffects.size()) {
            AnimationEffect class193 = (AnimationEffect) animationEffects.elementAt(i2);
            if (class193 != null && class193.isInEffect) {
                class193.update(System.currentTimeMillis());
            }
            if (class193.isInEffect) {
                i2++;
            } else {
                animationEffects.removeElement(class193);
            }
        }
    }

    public void Method130() {
        if (this.container != null) {
            this.container.w = BaseCanvas.w;
            this.container.h = BaseCanvas.h;
            if (this.Field1191) {
                Widget class184 = this.btnLeft;
                Widget class1842 = this.btnCenter;
                Widget class1843 = this.btnRight;
                int i = BaseCanvas.Field159;
                class1843.w = i;
                class1842.w = i;
                class184.w = i;
                Widget class1844 = this.btnLeft;
                Widget class1845 = this.btnCenter;
                Widget class1846 = this.btnRight;
                int i2 = BaseCanvas.Field159;
                class1846.h = i2;
                class1845.h = i2;
                class1844.h = i2;
                this.btnCenter.x = BaseCanvas.Field159;
                this.btnRight.x = BaseCanvas.Field160;
                Widget class1847 = this.btnLeft;
                Widget class1848 = this.btnCenter;
                Widget class1849 = this.btnRight;
                int i3 = BaseCanvas.h - LAF.Field1293;
                class1849.y = i3;
                class1848.y = i3;
                class1847.y = i3;
            }
        }
        imgSoft = null;
        createSoftBarImage();
    }

    public void switchToMe(int i) {
        switchToMe(i, false);
    }

    public void switchToMe(int i, boolean z) {
        if (BaseCanvas.getCurrentScreen() != this) {
            switch (i) {
                case -1:
                    if (this.container != null) {
                        this.container.x = -BaseCanvas.w;
                        break;
                    }
                    break;
                case 1:
                    this.container.x = BaseCanvas.w;
                    break;
            }
            if (z && (this.listScreen.empty() || this.listScreen.peek() != BaseCanvas.getCurrentScreen() || ((Screen) this.listScreen.peek()).screenId.equals(BaseCanvas.getCurrentScreen().screenId))) {
                if (BaseCanvas.getCurrentScreen().currentMenu != null) {
                    BaseCanvas.getCurrentScreen().hideMenu();
                }
                this.listScreen.push(BaseCanvas.getCurrentScreen());
            }
        }
        Effects.clearCache();
        BaseCanvas.setCurrentScreen(this);
        if (dialogs.isEmpty()) {
            return;
        }
        int size = dialogs.size();
        while (true) {
            size--;
            if (size < 0) {
                return;
            }
            if (!((Dialog) dialogs.elementAt(size)).Field1058) {
                Screen.this.hideDialog((Dialog) dialogs.elementAt(size));
                ((Dialog) dialogs.elementAt(size)).Field1056 = true;
            }
        }
        
    }

    public final void Method297(Screen Screen) {
        if (this.listScreen.empty()) {
            return;
        }
        ((Screen) this.listScreen.pop()).switchToMe(-1, false);
    }

    private static void createSoftBarImage() {
        if (imgSoft == null) {
            Image createImage = Image.createImage(BaseCanvas.w, LAF.LOT_ITEM_HEIGHT);
            imgSoft = createImage;
            Graphics graphics = createImage.getGraphics();
            Effects.isRadialGradientCache = false;
            Effects.show2(graphics, LAF.CLR_MENU_BAR_LIGHTER, LAF.CLR_MENU_BAR_DARKER, 0, 0, BaseCanvas.w, LAF.LOT_ITEM_HEIGHT, 0, -10, BaseCanvas.w, 10 + LAF.LOT_ITEM_HEIGHT);
            Effects.isRadialGradientCache = true;
            graphics.setColor(0);
            graphics.drawLine(0, 0, BaseCanvas.w, 0);
            graphics.setColor(LAF.Field1282);
            graphics.drawLine(0, 1, BaseCanvas.w, 1);
        }
    }

    public void paint() {
        BaseCanvas.g.translate(-BaseCanvas.g.getTranslateX(), -BaseCanvas.g.getTranslateY());
        BaseCanvas.g.setClip(0, 0, BaseCanvas.w, BaseCanvas.h);
        paintBackground();
        paintChildren();
        if (this.title != null) {
            LAF.Method409(this.title);
        }
        if (Field1189 != null) {
            Field1189.paintComponent();
        }
        if (this.Field1191) {
            LAF.paintScreenCommandBar(this);
        }
        for (int i = 0; i < animationEffects.size(); i++) {
            AnimationEffect e = (AnimationEffect) animationEffects.elementAt(i);
            if (e != null && e.isInEffect) {
                if (e.overCommandBar) {
                    BaseCanvas.g.setClip(0, 0, BaseCanvas.w, BaseCanvas.h);
                } else {
                    BaseCanvas.g.setClip(0, 0, BaseCanvas.w, BaseCanvas.h - LAF.Field1293);
                }
                e.paint();
            }
        }
        BaseCanvas.g.setClip(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    public void paintChildren() {
        if (this.container != null) {
            this.container.paintComponent();
        }
        if (!dialogs.isEmpty()) {
            int size = dialogs.size();
            for (int i = 0; i < size; i++) {
                if (i < dialogs.size()) {
                    ((Dialog) dialogs.elementAt(i)).paintComponent();
                }
            }
        }
        if (this.currentMenu != null) {
            this.currentMenu.paintComponent();
        }
    }

    public void update() {
        if (this.container != null) {
            this.container.update();
        }
    }

    public boolean checkKeys(int i, int i2) {
        if (Field1189 != null && Field1189.cmdCenter != null && i2 == ((Integer) Field1189.cmdCenter.objPerfomed).intValue()) {
            if (i == 1) {
                Field1189.cmdCenter.actionPerformed(new Command[]{Field1189.cmdCenter});
                return true;
            }
            return true;
        } else if (i == 1 && i2 == -5 && this.currentMenu == null) {
            commandCenterActionPerform(getCurrentRoot());
            return true;
        } else if (i2 == -6) {
            if (i == 0) {
                commandLeftActionPerform(getCurrentRoot());
                return true;
            }
            return true;
        } else if (i2 == -7) {
            if (i == 0) {
                commandRightActionPerform(getCurrentRoot());
                return true;
            }
            return true;
        } else {
            WidgetGroup Method314 = getCurrentRoot();
            if (Method314 != null) {
                return Method314.checkKeys(i, i2);
            }
            return false;
        }
    }

    public void paintBackground() {
        BaseCanvas.g.setColor(LAF.CLR_MENU_BAR_DARKER);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    public final void setTitle(String tl) {
        this.title = tl;
    }

    public void pointerPressed(int i, int i2) {
        if (this.btnLeft != null && this.btnLeft.contains(i, i2)) {
            this.btnLeft.isPressed = true;
        } else if (this.btnCenter != null && this.btnCenter.contains(i, i2)) {
            this.btnCenter.isPressed = true;
        } else if (this.btnRight != null && this.btnRight.contains(i, i2)) {
            this.btnRight.isPressed = true;
        } else {
            WidgetGroup Method314 = getCurrentRoot();
            if (Method314 != null) {
                Method314.pointerPressed(i, i2);
                if (this.currentMenu == null || this.currentMenu.contains(i, i2)) {
                    return;
                }
                Menu class175 = this.currentMenu;
                Menu.hide();
            }
        }
    }

    public void pointerDragged(int i, int i2) {
        if (this.draggedWidget != null) {
            this.draggedWidget.pointerDragged(i, i2);
            return;
        }
        Widget wg = null;
        if (currentDialog != null) {
            wg = currentDialog.getFocusedWidget(true);
        } else if (this.currentMenu != null) {
            wg = this.currentMenu.getFocusedWidget(true);
        } else if (this.container != null) {
            wg = this.container.getFocusedWidget(true);
        }
        if (wg != null) {
            wg.pointerDragged(i, i2);
        }
    }

    public void pointerReleased(int i, int i2) {
        if (this.btnLeft != null && this.btnLeft.isPressed && this.btnLeft.contains(i, i2)) {
            this.btnLeft.isPressed = false;
            commandLeftActionPerform(getCurrentRoot());
        } else if (this.btnCenter != null && this.btnCenter.isPressed && this.btnCenter.contains(i, i2)) {
            commandCenterActionPerform(getCurrentRoot());
        } else if (this.btnRight != null && this.btnRight.isPressed && this.btnRight.contains(i, i2)) {
            this.btnRight.isPressed = false;
            commandRightActionPerform(getCurrentRoot());
        } else if (this.draggedWidget == null) {
            getCurrentRoot().getFocusedWidget(true).pointerReleased(i, i2);
        } else {
            this.draggedWidget.pointerReleased(i, i2);
            this.draggedWidget = null;
        }
    }

    private void commandLeftActionPerform(WidgetGroup class185) {
        if (class185 != null) {
            Widget Method315 = class185.getFocusedWidget(true);
            if (Method315 != null) {
                Method315.isPressed = false;
                Command Method363 = Method315.getLeftCommand();
                if (Method363 != null) {
                    Method363.actionPerformed(new Object[]{Method363, Method315});
                    return;
                }
            } else if (class185.cmdLeft != null) {
                class185.cmdLeft.actionPerformed(new Object[]{class185.cmdLeft, class185});
                return;
            }
        }
        if (currentDialog == null && this.currentMenu == null && this.cmdRight != null) {
            this.cmdRight.actionPerformed(new Object[]{this.cmdRight, this.btnLeft});
        }
    }

    private void commandCenterActionPerform(WidgetGroup class185) {
        if (class185 != null) {
            Widget Method315 = class185.getFocusedWidget(true);
            if (Method315 != null) {
                Method315.isPressed = false;
                Command Method365 = Method315.getCenterCommand();
                if (Method365 != null) {
                    if (this.currentMenu != null) {
                        System.out.println(new StringBuffer().append(toString()).append("Hide menu khi focus").toString());
                        hideMenu();
                    }
                    Method365.actionPerformed(new Object[]{Method365, Method315});
                    return;
                }
            } else if (class185.cmdCenter != null) {
                if (this.currentMenu != null) {
                    hideMenu();
                }
                class185.cmdCenter.actionPerformed(new Object[]{class185.cmdCenter, class185});
                return;
            }
        }
        if (currentDialog != null || this.cmdCenter == null) {
            return;
        }
        this.cmdCenter.actionPerformed(new Object[]{this.cmdCenter, this.btnCenter});
    }

    private void commandRightActionPerform(WidgetGroup class185) {
        if (class185 != null) {
            Widget Method315 = class185.getFocusedWidget(true);
            if (Method315 != null) {
                Method315.isPressed = false;
                Command Method364 = Method315.getRightCommand();
                if (Method364 != null) {
                    Method364.actionPerformed(new Object[]{Method364, Method315});
                    return;
                }
            } else if (class185.cmdRight != null) {
                class185.cmdRight.actionPerformed(new Object[]{class185.cmdRight, class185});
                return;
            }
        }
        if (currentDialog != null || this.cmdLeft == null) {
            return;
        }
        this.cmdLeft.actionPerformed(new Object[]{this.cmdLeft, this.btnRight});
    }

    public void showMenu(Vector menu, int pos) {
        if (menu == null || menu.isEmpty()) {
            return;
        }
        if (this.currentMenu != null) {
            this.currentMenu.showNewMenu(menu, pos);
            this.currentMenu.isShowNextMenu = true;
            return;
        }
        this.currentMenu = new Menu();
        this.currentMenu.focusWid = this.container.getFocusedWidget(true);
        this.currentMenu.showNewMenu(menu, pos);
        this.container.addWidget(this.currentMenu);
        this.currentMenu.requestFocus();
    }

    public final void hideMenu() {
        this.container.removeWidget(this.currentMenu);
        if (this.currentMenu != null && this.currentMenu.focusWid != null && this.currentMenu.focusWid.isVisible && currentDialog == null) {
            this.currentMenu.focusWid.requestFocus();
        }
        this.currentMenu = null;
    }

    private void Method304(Widget Widget0) {
        for (Widget undefineField = Widget0.parent; undefineField != null; undefineField = undefineField.parent) {
            if (undefineField.isScrollable()) {
                if (undefineField instanceof WidgetGroup) {
                    ((WidgetGroup) undefineField).scrollComponentToVisible(Widget0);
                }
                Screen screen = this;
                Widget0 = undefineField;
                break;
            }
            Widget0 = undefineField;
        }
    }

    public final void requestFocus(Widget wid) {
        if (wid == null) {
            return;
        }
        WidgetGroup class185 = currentDialog == null ? this.container : currentDialog;
        WidgetGroup class1852 = class185;
        if (class185 != null) {
            Widget Method315 = class1852.getFocusedWidget(true);
            if (Method315 != class1852) {
                Method315.setFocusWithParents(false);
            }
            if (wid != null) {
                wid.setFocusWithParents(true);
                Widget class1842 = wid.parent;
                while (true) {
                    Widget class1843 = class1842;
                    if (class1843 == null) {
                        break;
                    }
                    if (class1843 instanceof WidgetGroup) {
                        ((WidgetGroup) class1843).defaultFocusWidget = wid;
                    }
                    class1842 = class1843.parent;
                }
                if (Method315 != null) {
                    Method315.onLostFocused();
                }
                if (wid != null) {
                    Method304(wid);
                    wid.onFocused();
                }
            }
        }
    }

    public final void showDialog(Dialog dialog, boolean z) {
        dialog.Field1056 = false;
        dialog.Field1055 = z;
        if (z) {
            dialog.x = -dialog.w;
            dialog.destX = (BaseCanvas.w - dialog.w) >> 1;
        }
        dialog.Field1057 = this.container.getFocusedWidget(true);
        if (dialog.Field1057 != this.container) {
            dialog.Field1057.setFocusWithParents(false);
        }
        dialogs.addElement(dialog);
        currentDialog = dialog;
        if (dialog.defaultFocusWidget != null) {
            dialog.defaultFocusWidget.requestFocus();
        } else if (dialog.children.length > 0) {
            dialog.children[0].requestFocus();
        } else {
            dialog.requestFocus();
        }
    }

    public void showDialog(Dialog dialog) {
        Screen.this.showDialog(dialog, true);
    }

    public final void hideDialog() {
        Screen.this.hideDialog(currentDialog);
    }

    public static void hideDialog(Dialog dialog) {
        if (dialog == null || dialogs.isEmpty()) {
            return;
        }
        dialog.Method26();
    }

    public final void Method309() {
        if (this.container == null || dialogs.isEmpty()) {
            return;
        }
        for (int i = 0; i < dialogs.size(); i++) {
            ((Dialog) dialogs.elementAt(i)).Method26();
        }
        dialogs.removeAllElements();
        currentDialog = null;
        this.container.findDefaultfocusableWidget().requestFocus();
    }

    public void close() {
    }

    public final boolean Method272(int i) {
        if (currentDialog != null) {
            Widget Method315 = currentDialog.getFocusedWidget(true);
            if (Method315 instanceof EditField) {
                return Method315.checkKeys(0, i);
            }
            return false;
        } else if (this.container != null) {
            Widget Method3152 = this.container.getFocusedWidget(true);
            if (Method3152 instanceof EditField) {
                return Method3152.checkKeys(0, i);
            }
            if (this.chatEditField == null || currentDialog != null || this.currentMenu != null || i <= 0) {
                return false;
            }
            return this.chatEditField.checkKeys(0, i);
        } else {
            return false;
        }
    }

    public static boolean Method126() {
        return false;
    }

    public void hide() {
        if (currentDialog != null) {
            currentDialog.isFocused = false;
            currentDialog.requestFocus();
        }
    }

    public final void addWidget(Widget class184) {
        if (this.container != null) {
            this.container.addWidget(class184);
        }
    }

    public final void removeWidget(Widget class184) {
        if (this.container != null) {
            this.container.removeWidget(class184);
        }
    }

    public final void Method312(Widget class184) {
        this.container.hideWidget(class184);
    }

    public static void Method313(String str) {
        if (LAF.Field1298 == 0) {
            Field1189 = new Label(str);
        } else {
            Field1189 = new Label(str, ResourceManager.boldFont);
        }
        Field1189.padding = LAF.LOT_PADDING;
        Field1189.scrollType = (byte) 1;
        Field1189.speed = 1;
        Field1189.setMetrics(0, 0, BaseCanvas.w, LAF.Field1292);
        Field1189.y = -Field1189.h;
        Field1189.startTicker(1000L);
    }

    public void onChat(String str) {
    }

    ///////@Override // defpackage.Class200
    public void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case -6:
                if (this.chatEditField.getText().length() == 0 && this.container.containWidget(this.chatEditField)) {
                    this.chatEditField.isVisible = false;
                    removeWidget(this.chatEditField);
                    return;
                } else if (this.chatEditField.getText().length() <= 0 || this.container.containWidget(this.chatEditField)) {
                    return;
                } else {
                    this.chatEditField.isVisible = true;
                    addWidget(this.chatEditField);
                    this.chatEditField.requestFocus();
                    if ("*".equals(this.chatEditField.text)) {
                        new EmotionDialog(this.chatEditField).show(false);
                        this.chatEditField.clear();
                        return;
                    }
                    return;
                }
            case -5:
                new EmotionDialog(this.chatEditField).show(false);
                return;
            case -4:
                break;
            case -3:
                Vector vector = new Vector(2);
                vector.addElement(new Command(-5, T.gL(17), this));
                vector.addElement(new Command(-4, T.gL(2), this));
                showMenu(vector, 0);
                return;
            case -2:
                String trim = this.chatEditField.getText().trim();
                if (trim.length() > 0) {
                    onChat(trim);
                    break;
                }
                break;
            case -1:
            default:
                return;
            case 0:
                checkKeys(1, -6);
                return;
            case 1:
                checkKeys(1, -5);
                return;
            case 2:
                checkKeys(1, -7);
                return;
        }
        removeWidget(this.chatEditField);
        this.chatEditField.setText("");
        this.chatEditField.isVisible = false;
    }

    public final WidgetGroup getCurrentRoot() {
        Menu menu = null;
        if (this.currentMenu != null) {
            menu = this.currentMenu;
        } else if (currentDialog != null) {
            return currentDialog;
        } else if (this.container != null) {
            return this.container;
        }
        return menu;
    }

    public final Widget Method315(boolean z) {
        return this.container.getFocusedWidget(true);
    }

    public static void Method316(int i) {
        for (int i2 = 0; i2 < animationEffects.size(); i2++) {
            AnimationEffect class193 = (AnimationEffect) animationEffects.elementAt(i2);
            if (class193.Field1299 == 1) {
                animationEffects.removeElement(class193);
                return;
            }
        }
    }
}

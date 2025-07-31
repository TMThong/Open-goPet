package vn.me.ui.common;

import defpackage.Command;
import vn.me.ui.ListItem;
import vn.me.ui.Button;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.EditField;
import vn.me.ui.Menu;
import vn.me.ui.Widget;
import vn.me.ui.Font;
import vn.me.screen.Screen;

public final class LAF {
    public static int CLR_MENU_BAR_DARKER = 345451;
    public static int CLR_MENU_BAR_LIGHTER = 551328;
    public static int Field1282 = 16777215;
    public static int Field1283 = 7564651;
    public static int Field1284 = 16711680;
    private static int Field1285 = 6710886;
    public static int Field1286 = 0;
    public static int Field1287 = 5378829;
    public static int Field1288 = 16777215;
    public static int Field1289 = 16748544;
    public static int Field1290 = 16433664;
    public static int Field1291 = 40;
    public static int Field1292 = 20;
    public static int Field1293 = 20;
    public static int LOT_ITEM_HEIGHT = 20;
    public static int Field1295 = 8;
    public static int LOT_PADDING = 3;
    public static int Field1297 = 6;
    public static byte Field1298;

    public static void Method408(byte b) {
        Field1298 = (byte) 1;
        Field1283 = 1160191;
        Field1286 = 16777215;
        Field1287 = 8955067;
        Field1288 = 0;
    }

    public static void Method409(String str) {
        BaseCanvas.g.translate(-BaseCanvas.g.getTranslateX(), -BaseCanvas.g.getTranslateY());
        BaseCanvas.g.setClip(0, 0, BaseCanvas.w, Field1292);
        Effects.show1(BaseCanvas.g, CLR_MENU_BAR_DARKER, CLR_MENU_BAR_LIGHTER, 0, 0, BaseCanvas.w, Field1292, false);
        BaseCanvas.g.setColor(Field1282);
        BaseCanvas.g.drawLine(0, Field1292 - 1, BaseCanvas.w, Field1292 - 1);
        ResourceManager.boldFont.drawString(BaseCanvas.g, str, BaseCanvas.Field157, (Field1292 - ResourceManager.boldFont.getHeight()) >> 1, 17);
    }

    public static void paintScreenCommandBar(Screen sc) {
        BaseCanvas.g.translate(-BaseCanvas.g.getTranslateX(), -BaseCanvas.g.getTranslateY());
        BaseCanvas.g.setClip(0, 0, BaseCanvas.w, BaseCanvas.h);
        if (Screen.imgSoft != null && !sc.Field1184) {
            BaseCanvas.g.drawImage(Screen.imgSoft, 0, (BaseCanvas.h - LOT_ITEM_HEIGHT) + 1, 20);
        }
        if (sc.currentMenu != null) {
            paintCommandBarText(sc.Field1185, null, sc.currentMenu.cmdCenter, sc.currentMenu.cmdRight);
        } else if (Screen.currentDialog != null) {
            Widget Method315 = Screen.currentDialog.getFocusedWidget(true);
            if (Method315 != null) {
                paintCommandBarText(sc.Field1185, Method315.getLeftCommand(), Method315.getCenterCommand(), Method315.getRightCommand());
            } else {
                paintCommandBarText(sc.Field1185, Screen.currentDialog.cmdLeft, Screen.currentDialog.cmdCenter, Screen.currentDialog.cmdRight);
            }
        } else if (sc.getCurrentRoot() == null) {
            paintCommandBarText(sc.Field1185, sc.cmdRight, sc.cmdCenter, sc.cmdLeft);
        } else {
            Widget Method3152 = sc.Method315(true);
            Command Method363 = Method3152.getLeftCommand();
            Command Method365 = Method3152.getCenterCommand();
            Command Method364 = Method3152.getRightCommand();
            paintCommandBarText(sc.Field1185, (sc.currentMenu == null && Method363 == null) ? sc.cmdRight : Method363, (sc.currentMenu == null && Method365 == null) ? sc.cmdCenter : Method365, (sc.currentMenu == null && Method364 == null) ? sc.cmdLeft : Method364);
        }
    }

    private static void paintCommandBarText(Font font, Command Command, Command Command2, Command Command3) {
        if (Command != null) {
            font.drawString(BaseCanvas.g, Command.Field1321, LOT_PADDING, (((LOT_ITEM_HEIGHT - ResourceManager.boldFont.getHeight()) >> 1) + BaseCanvas.h) - LOT_ITEM_HEIGHT, 20);
        }
        if (Command2 != null) {
            font.drawString(BaseCanvas.g, Command2.Field1321, BaseCanvas.Field157, (((LOT_ITEM_HEIGHT - ResourceManager.boldFont.getHeight()) >> 1) + BaseCanvas.h) - LOT_ITEM_HEIGHT, 17);
        }
        if (Command3 != null) {
            font.drawString(BaseCanvas.g, Command3.Field1321, BaseCanvas.w - LOT_PADDING, (((LOT_ITEM_HEIGHT - ResourceManager.boldFont.getHeight()) >> 1) + BaseCanvas.h) - LOT_ITEM_HEIGHT, 24);
        }
    }

    public static void Method412(int i, int i2, int i3, int i4) {
        BaseCanvas.g.translate(0, 0);
        BaseCanvas.g.setColor(16777215);
        BaseCanvas.g.fillRect(2, 1, i3 - 4, 1);
        BaseCanvas.g.fillRect(2, i4 - 2, i3 - 4, 1);
        BaseCanvas.g.fillRect(1, 2, 1, i4 - 4);
        BaseCanvas.g.fillRect(i3 - 2, 2, 1, i4 - 4);
        BaseCanvas.g.drawRect(2, 2, i3 - 5, i4 - 5);
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.drawRoundRect(0, 0, i3 - 1, i4 - 1, 10, 10);
        BaseCanvas.g.drawRect(3, 3, i3 - 7, i4 - 7);
        BaseCanvas.g.translate(0, 0);
    }

    public static void Method413(int i, int i2, int i3, int i4) {
        BaseCanvas.g.translate(i, i2);
        BaseCanvas.g.setColor(CLR_MENU_BAR_DARKER);
        BaseCanvas.g.fillRect(3, 3, i3 - 6, i4 - 6);
        Method412(0, 0, i3, i4);
        BaseCanvas.g.translate(-i, -i2);
    }

    public static void Method414(Dialog Dialog) {
        Effects.show1(BaseCanvas.g, CLR_MENU_BAR_DARKER, CLR_MENU_BAR_LIGHTER, 3, 3, Dialog.w - 6, Dialog.h - 2, false);
    }

    public static void Method415(Button btn) {
        if (btn.isPressed()) {
            if ((btn.Field1050 >>> 24) == 0) {
                Effects.show1(BaseCanvas.g, Field1290, Field1289, 1, 1, btn.w - 2, btn.h - 2, false);
                return;
            }
            BaseCanvas.g.setColor(btn.Field1050);
            BaseCanvas.g.fillRoundRect(0, 0, btn.w - 1, btn.h - 1, 6, 6);
        } else if (!btn.isFocused || btn.typeBtn != 0) {
            if ((btn.Field1049 >>> 24) != 0) {
                BaseCanvas.g.setColor(btn.Field1049);
                BaseCanvas.g.fillRoundRect(0, 0, btn.w - 1, btn.h - 1, 6, 6);
            }
        } else if ((btn.Field1050 >>> 24) == 0) {
            Effects.show1(BaseCanvas.g, CLR_MENU_BAR_LIGHTER, CLR_MENU_BAR_DARKER, 1, 1, btn.w - 2, btn.h - 2, false);
        } else {
            BaseCanvas.g.setColor(btn.Field1050);
            BaseCanvas.g.fillRoundRect(0, 0, btn.w - 1, btn.h - 1, 6, 6);
        }
    }

    public static void Method416(Button btn) {
        if (btn.isFocused) {
            BaseCanvas.g.setColor(Field1283);
            BaseCanvas.g.drawRoundRect(1, 1, btn.w - 3, btn.h - 3, 6, 6);
        }
    }

    public static void Method417(Button btn) {
        if ((btn.typeBtn == 1 || btn.typeBtn == 2) && btn.Field1048 && btn.image != null) {
            btn.image.drawFrame(BaseCanvas.g, 2, 1, (btn.preferredSize.height - btn.image.frameHeight) >> 1, 0, 20);
        }
    }

    public static void Method418(Button btn) {
        if (Field1298 == 0) {
            if (btn.Field1048 || btn.isFocused) {
                if (btn.isFocused) {
                    BaseCanvas.g.setColor(btn.Field1050);
                } else {
                    BaseCanvas.g.setColor(Field1285);
                }
                BaseCanvas.g.fillRect(2, 2, btn.w - 4, btn.h - 2);
                BaseCanvas.g.setColor(3289650);
                BaseCanvas.g.fillRect(2, 0, btn.w - 4, 1);
                BaseCanvas.g.fillRect(0, 2, 1, btn.h - 1);
                BaseCanvas.g.fillRect(btn.w - 1, 2, 1, btn.h - 1);
                BaseCanvas.g.fillRect(1, 1, 1, 1);
                BaseCanvas.g.fillRect(btn.w - 2, 1, 1, 1);
            }
        } else if (!btn.Field1048 && !btn.isFocused) {
            BaseCanvas.g.setColor(btn.Field1049);
            BaseCanvas.g.fillRect(1, 3, btn.w - 2, btn.h - 2);
            BaseCanvas.g.setColor(6515815);
            BaseCanvas.g.fillRect(1, 2, btn.w - 2, 1);
            BaseCanvas.g.fillRect(0, 3, 1, btn.h - 2);
            BaseCanvas.g.fillRect(1, 3, 1, 1);
            BaseCanvas.g.fillRect(btn.w - 2, 3, 1, 1);
            BaseCanvas.g.setColor(3289650);
            BaseCanvas.g.fillRect(btn.w - 1, 3, 1, btn.h - 2);
        } else {
            if (btn.isFocused) {
                BaseCanvas.g.setColor(btn.Field1050);
            } else {
                BaseCanvas.g.setColor(14478591);
            }
            BaseCanvas.g.fillRect(2, 2, btn.w - 4, btn.h - 2);
            BaseCanvas.g.setColor(3289650);
            BaseCanvas.g.fillRect(2, 0, btn.w - 4, 1);
            BaseCanvas.g.fillRect(0, 2, 1, btn.h - 1);
            BaseCanvas.g.fillRect(btn.w - 1, 2, 1, btn.h - 1);
            BaseCanvas.g.fillRect(1, 1, 1, 1);
            BaseCanvas.g.fillRect(btn.w - 2, 1, 1, 1);
            BaseCanvas.g.setColor(16777215);
            BaseCanvas.g.fillRect(2, 1, btn.w - 4, 1);
            BaseCanvas.g.fillRect(1, 2, 1, btn.h - 1);
            BaseCanvas.g.fillRect(btn.w - 2, 2, 1, btn.h - 1);
            BaseCanvas.g.fillRect(2, 2, 1, 1);
            BaseCanvas.g.fillRect(btn.w - 3, 2, 1, 1);
        }
    }

    public static void Method419(EditField editField) {
        if (editField.isFocused) {
            BaseCanvas.g.setColor(Field1283);
        } else {
            BaseCanvas.g.setColor(Field1282);
        }
        if (editField.Field1082 == 0) {
            BaseCanvas.g.drawRoundRect(0, 0, editField.w - 1, editField.h - 1, Field1297, Field1297);
        } else {
            BaseCanvas.g.drawRoundRect(0 + editField.Field1082, 0, (editField.w - 1) - editField.Field1082, editField.h - 1, Field1297, Field1297);
        }
    }

    public static void paintListItemBG(ListItem list) {
        if (list.isPressed()) {
            Effects.show1(BaseCanvas.g, Field1290, Field1289, 0, 0, list.w - 1, list.h - 1, false);
        } else if (list.isFocused) {
            Effects.show1(BaseCanvas.g, CLR_MENU_BAR_DARKER, CLR_MENU_BAR_LIGHTER, 0, 0, list.w - 1, list.h - 1, false);
        }
    }

    public static void paintListItemBorder(ListItem list) {
        BaseCanvas.g.setColor(5592405);
        BaseCanvas.g.drawLine(0, 0, list.w - 1, 0);
        if (list.isFocused) {
            BaseCanvas.g.setColor(Field1283);
            BaseCanvas.g.drawRoundRect(0, 0, list.w - 1, list.h - 1, Field1297, Field1297);
        }
    }

    public static void Method422(Menu menu) {
        BaseCanvas.g.setColor(16777215);
        BaseCanvas.g.fillRect(2, 1, menu.w - 4, 1);
        BaseCanvas.g.fillRect(2, menu.h - 2, menu.w - 4, 1);
        BaseCanvas.g.fillRect(1, 2, 1, menu.h - 4);
        BaseCanvas.g.fillRect(menu.w - 2, 2, 1, menu.h - 4);
        BaseCanvas.g.drawRect(2, 2, menu.w - 5, menu.h - 5);
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.drawRoundRect(0, 0, menu.w - 1, menu.h - 1, 10, 10);
        BaseCanvas.g.drawRect(3, 3, menu.w - 7, menu.h - 7);
    }

    public static void Method423(Menu menu) {
        BaseCanvas.g.setColor(345451);
        BaseCanvas.g.fillRect(3, 3, menu.w - 6, menu.h - 6);
    }

    public static void paintScrollBar(Widget widget) {
        if (widget.scrollY != widget.destScrollY || widget.isDragActivated) {
            BaseCanvas.g.setClip(BaseCanvas.g.getClipX() + widget.border, BaseCanvas.g.getClipY() + widget.border, BaseCanvas.g.getClipWidth() - (widget.border << 1), BaseCanvas.g.getClipHeight() - (widget.border << 1));
            BaseCanvas.g.setColor(201059015);
            BaseCanvas.g.fillRoundRect((widget.w - 4) - widget.border, widget.scrollBarY, 4, widget.scrollBarH, 4, 4);
            BaseCanvas.g.setColor(14582307);
            BaseCanvas.g.fillRect((widget.w - 3) - widget.border, widget.scrollBarY + 2, 2, widget.scrollBarH - 4);
            BaseCanvas.g.setClip(BaseCanvas.g.getClipX() - widget.border, BaseCanvas.g.getClipY() - widget.border, BaseCanvas.g.getClipWidth() + (widget.border << 1), BaseCanvas.g.getClipHeight() + (widget.border << 1));
        }
    }
}

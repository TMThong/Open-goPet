package vn.me.screen;

import defpackage.ActorFactory;
import vn.me.core.BaseCanvas;
import defpackage.Class106;
import defpackage.Class179;
import defpackage.Letter;
import defpackage.LetterWidget;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GlobalService;
import java.io.IOException;
import vn.me.ui.common.LAF;
import vn.me.ui.Label;
import vn.me.network.Message;
import vn.me.ui.WidgetGroup;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.network.Cmd;
import vn.me.ui.common.T;

public final class MGOMessageScreen extends Screen {

    private static final Vector[] letters = new Vector[]{new Vector(), new Vector(), new Vector()};
    private Image nonIsMarkImg;
    private Image isMarkImg;
    private WidgetGroup[] Field573;
    private Class179 Field574;

    public MGOMessageScreen() {
        super(true);
        this.screenId = "MESSAGE";
        this.cmdLeft = GameController.Field467;
        this.cmdRight = new Command(1, T.gL(T.OPTION), this);
        this.Field573 = new WidgetGroup[3];
        try {
            nonIsMarkImg = Image.createImage("/pet/thuchuadoc.png");
            isMarkImg = Image.createImage("/pet/thudocroi.png");
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    public final void clear() {
        this.container.removeAll();
        for (int i = 0; i < 3; i++) {
            letters[i].removeAllElements();
            this.Field573[i] = null;
        }
        this.Field574 = null;
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    public final void switchToMe(int i, boolean z) {
        super.switchToMe(i, z);
        int i2 = -1;
        int i3 = 0;
        while (true) {
            if (i3 >= 3) {
                break;
            } else if (letters[i3].size() > 0) {
                i2 = i3;
                break;
            } else {
                i3++;
            }
        }
        this.Field574 = new Class179(0, 0, this.container.w, BaseCanvas.h - LAF.Field1293);
        this.Field574.Field1194.isAutoFit = true;
        this.container.addWidget(this.Field574, false);
        for (int i4 = 0; i4 < 3; i4++) {
            this.Field573[i4] = new WidgetGroup(0, LAF.Field1292, this.container.w, BaseCanvas.h - (2 * LAF.Field1292));
            this.Field573[i4].isScrollableY = true;
            WidgetGroup class185 = this.Field573[i4];
            Vector vector = letters[i4];
            WidgetGroup class1852 = class185;
            if (class1852 == null) {
                WidgetGroup class1853 = new WidgetGroup(0, LAF.Field1292, this.container.w, BaseCanvas.h - (2 * LAF.Field1292));
                class1852 = class1853;
                class1853.isScrollableY = true;
            }
            if (vector.isEmpty()) {
                Label class173 = new Label(ActorFactory.gL(307));
                class173.align = 17;
                class1852.addWidget(class173);
            }
            int size = vector.size();
            for (int i5 = 0; i5 < size; i5++) {
                Letter class97 = (Letter) vector.elementAt(i5);
                LetterWidget class99 = new LetterWidget(this, class97);
                class99.cmdCenter = new Command(2, T.gL(T.VIEW), class97, this);
                class1852.addWidget(class99, false);
            }
            this.Field573[i4].padding = LAF.LOT_PADDING;
            this.Field573[i4].setViewMode(1);
        }
        this.Field574.Method294(T.gL(T.FRIEND), this.Field573[0]);
        this.Field574.Method294(T.gL(T.EVENTS), this.Field573[1]);
        this.Field574.Method294(T.gL(T.ADMIN), this.Field573[2]);
        if (i2 != -1) {
            this.Field574.Method104(i2);
        } else {
            this.Field574.Method79(0);
        }
    }

    public final void setLetter0(int i, String str, String str2, String str3, boolean z) {
        letters[0].addElement(new Letter(this, i, str, str2, str3, z));
    }

    public final void setLetter2(int i, String str, String str2, String str3, boolean z) {
        letters[2].addElement(new Letter(this, i, str, str2, str3, z));
    }

    public final void setLetter1(int i, String str, String str2, String str3, boolean z) {
        letters[1].addElement(new Letter(this, i, str, str2, str3, z));
    }

    private void Method79(int id) {
        int i2 = -1;
        int i3 = -1;
        int i4 = 0;
        while (true) {
            if (i4 >= 3) {
                break;
            }
            int i5 = 0;
            while (true) {
                if (i5 >= letters[i4].size()) {
                    break;
                } else if (((Letter) letters[i4].elementAt(i5)).letterId == id) {
                    i2 = i5;
                    break;
                } else {
                    i5++;
                }
            }
            if (i2 != -1) {
                i3 = i4;
                break;
            }
            i4++;
        }
        if (i2 != -1) {
            Letter class97 = (Letter) letters[i3].elementAt(i2);
            letters[i3].removeElementAt(i2);
            WidgetGroup class185 = this.Field573[i3];
            for (int i6 = 0; i6 < class185.children.length; i6++) {
                LetterWidget class99 = (LetterWidget) class185.children[i6];
                if (class99.letter == class97) {
                    System.out.println("removed");
                    class185.removeWidget(class99);
                    return;
                }
            }
        }
    }

    public final void actionPerformed(Object obj) {
        int Method352;
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 1:
                Vector vector = new Vector();
                vector.addElement(new Command(11, T.gL(T.REMOVE), this));
                vector.addElement(new Command(12, T.gL(T.SEND_LETTER), this));
                showMenu(vector, 0);
                return;
            case 2:
                Letter class97 = (Letter) Command.objPerfomed;
                GameController.Method69(class97.Field578, class97.Field580, null, null, GameController.Field464, true);
                int i = class97.letterId;
                Message message = new Message(Cmd.LETTER_COMMAND);
                message.putByte(Cmd.LETTER_COMMAND_SET_MARK);
                message.putInt(i);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                class97.isMark = true;
                return;
            case 11:
                int Method128 = this.Field574.Method128();
                if (Method128 == -1 || (Method352 = this.Field573[Method128].getFocusedIndex()) == -1) {
                    return;
                }
                Letter class972 = (Letter) letters[Method128].elementAt(Method352);
                int i2 = class972.letterId;
                Message message2 = new Message(Cmd.LETTER_COMMAND);
                message2.putByte(Cmd.LETTER_COMMAND_REMOVE_LETTER);
                message2.putInt(i2);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                Method79(class972.letterId);
                return;
            case 12:
                new Class106().Method0();
                return;
        }
    }

    public static Image getMarkImg(MGOMessageScreen class95) {
        return class95.isMarkImg;
    }

    public static Image getNonIsMarkImg(MGOMessageScreen class95) {
        return class95.nonIsMarkImg;
    }
}

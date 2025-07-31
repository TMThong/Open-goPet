package vn.me.ui;

import vn.me.core.BaseCanvas;
import defpackage.Class169;
import defpackage.Command;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.common.T;
import vn.me.ui.Widget;
import vn.me.ui.Font;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.TextBox;

/* renamed from: Class168  reason: default package */
/* loaded from: gopet_repackage.jar:Class168.class */
public final class EditField extends Widget implements IActionListener {
    public String text;
    private String Field1066;
    private String Field1067;
    private int Field1068;
    private int Field1069;
    public int Field1070;
    private int Field1071;
    private int Field1072;
    private int Field1073;
    private int Field1074;
    private int Field1075;
    public int Field1076;
    private static boolean Field1077;
    private boolean Field1078;
    private int Field1079;
    private boolean Field1080;
    private String Field1081;
    public int Field1082;
    private int Field1083;
    private Font Field1087;
    private Font Field1088;
    private int Field1089;
    private Command Field1090;
    public static Command Field1091;
    private int Field1092;
    private int Field1093;
    private int Field1094;
    private int Field1095;
    private String Field1096;
    public IActionListener Field1097;
    private int Field1098;
    private long Field1099;
    private static int Field1060 = 0;
    private static final int[] Field1061 = {18, 14, 11, 9, 6, 4, 2};
    private static int Field1062 = 0;
    private static String[] Field1063 = {" 0", ".,@?!_1\"/$-():*+<=>;%&~#%^&*{}[];'/1", "abc2áàảãạâấầẩẫậăắằẳẵặ2", "def3đéèẻẽẹêếềểễệ3", "ghi4íìỉĩị4", "jkl5", "mno6óòỏõọôốồổỗộơớờởỡợ6", "pqrs7", "tuv8úùủũụưứừửữự8", "wxyz9ýỳỷỹỵ9", "*", "#"};
    private static String[] Field1064 = {"0", "1", "abc2", "def3", "ghi4", "jkl5", "mno6", "pqrs7", "tuv8", "wxyz9", "0", "0"};
    private static String[] Field1084 = {"abc", "Abc", "ABC", "123"};
    private static int Field1085 = 35;
    private static int Field1086 = 42;

    public EditField() {
        this(0, 0, 0, 0, "");
    }

    public EditField(int i, int i2, int i3, int i4) {
        this(i, i2, i3, i4, null);
    }

    private EditField(int i, int i2, int i3, int i4, String str) {
        this(i, i2, i3, i4, str, ResourceManager.defaultFont, ResourceManager.defaultFont, ResourceManager.boldFont);
    }

    private EditField(int i, int i2, int i3, int i4, String str, Font class171, Font class1712, Font class1713) {
        super(i, i2, i3, i4);
        this.text = "";
        this.Field1066 = "";
        this.Field1067 = "";
        this.Field1068 = 0;
        this.Field1069 = 0;
        this.Field1070 = 500;
        this.Field1071 = 0;
        this.Field1072 = -1982;
        this.Field1073 = 0;
        this.Field1074 = 0;
        this.Field1075 = 10;
        this.Field1076 = 0;
        this.Field1078 = true;
        this.Field1080 = false;
        this.Field1082 = 0;
        this.Field1083 = 0;
        this.Field1087 = ResourceManager.defaultFont;
        Font class1714 = ResourceManager.defaultFont;
        this.Field1088 = ResourceManager.defaultFont;
        int i5 = LAF.Field1283;
        this.Field1089 = 0;
        this.Field1096 = "";
        this.Field1098 = 0;
        this.Field1099 = 0L;
        this.Field1087 = class171;
        this.Field1088 = class1713;
        this.Field1082 = str == null ? 0 : class171.getWidth(str) + (2 * LAF.LOT_PADDING);
        this.Field1081 = str;
        this.Field1080 = false;
        this.text = "";
        Field1062 = this.Field1087.getHeight() - 1;
        Command Command = new Command(0, T.gL(3), this);
        this.Field1090 = Command;
        this.cmdRight = Command;
        this.Field1079 = this.Field1088.getWidth("ABC") + 5;
        this.border = 1;
        Method7();
    }

    ///////@Override // defpackage.Class184
    public final void setMetrics(int i, int i2, int i3, int i4) {
        super.setMetrics(i, i2, i3, i4);
        if (this.Field1082 == 0) {
            return;
        }
        String str = this.Field1081;
        this.Field1082 = this.Field1087.getWidth(str) + this.padding;
        while (this.Field1082 > (this.w << 1) / 3) {
            str = str.substring(0, str.length() - 1);
            this.Field1082 = this.Field1087.getWidth(str) + this.padding;
        }
        this.Field1081 = str;
        Method7();
    }

    public final void Method79(int i) {
        this.Field1076 = i;
        switch (i) {
            case 0:
            case 2:
            case 3:
                this.Field1083 = 0;
                break;
            case 1:
                this.Field1083 = 3;
                break;
        }
        this.Field1079 = 0;
        if (this.Field1078) {
            this.Field1079 = this.Field1088.getWidth("ABC") + 5;
        }
    }

    public final void Method158(String str) {
        Method104(ResourceManager.boldFont.getWidth(str) + (this.padding << 1));
        this.Field1081 = str;
    }

    public final void clear() {
        if (this.Field1068 <= 0 || this.text.length() <= 0) {
            return;
        }
        this.text = new StringBuffer().append(this.text.substring(0, this.Field1068 - 1)).append(this.text.substring(this.Field1068, this.text.length())).toString();
        this.Field1068--;
        Method7();
        Method25();
    }

    private void Method7() {
        if (this.text.length() == 0) {
            this.Field1067 = this.Field1096;
            this.Field1067 = this.Field1066;
        } else {
            this.Field1067 = this.text;
        }
        this.Field1092 = this.Field1082;
        this.Field1093 = 0;
        this.Field1094 = (this.w - 1) - this.Field1082;
        this.Field1095 = this.h - 1;
        if (this.Field1071 < 0 && this.Field1087.getWidth(this.Field1067) + this.Field1071 < ((this.Field1094 - this.padding) - 13) - this.Field1079) {
            this.Field1071 = ((this.Field1094 - 10) - this.Field1079) - this.Field1087.getWidth(this.Field1067);
        }
        if (this.Field1071 + this.Field1087.getWidth(this.Field1067.substring(0, this.Field1068)) <= 0) {
            this.Field1071 = -this.Field1087.getWidth(this.Field1067.substring(0, this.Field1068));
            this.Field1071 += 40;
        } else if (this.Field1071 + this.Field1087.getWidth(this.Field1067.substring(0, this.Field1068)) >= (this.Field1094 - 12) - this.Field1079) {
            this.Field1071 = (((this.Field1094 - 10) - this.Field1079) - this.Field1087.getWidth(this.Field1067.substring(0, this.Field1068))) - (this.padding << 1);
        }
        if (this.Field1071 > 0) {
            this.Field1071 = 0;
        }
        if (this.Field1097 != null) {
            this.Field1097.actionPerformed(new Object[]{new Command(-6, null, null), this});
        }
    }

    private boolean Method272(int i) {
        if (this.Field1076 == 3) {
            if ((i < 48 || i > 57) && ((i < 65 || i > 90) && (i < 97 || i > 122))) {
                return false;
            }
        } else if (this.Field1076 == 1 && (i < 48 || i > 57)) {
            return false;
        }
        if (this.text.length() < this.Field1070) {
            String stringBuffer = new StringBuffer().append(this.text.substring(0, this.Field1068)).append((char) i).toString();
            if (this.Field1068 < this.text.length()) {
                stringBuffer = new StringBuffer().append(stringBuffer).append(this.text.substring(this.Field1068, this.text.length())).toString();
            }
            this.text = stringBuffer;
            this.Field1068++;
            Method25();
            Method7();
            return true;
        }
        return true;
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        if (LAF.Field1298 == 0) {
            LAF.Method419(this);
            return;
        }
        if (this.isFocused) {
            BaseCanvas.g.setColor(1655180);
        } else {
            BaseCanvas.g.setColor(3033945);
        }
        BaseCanvas.g.drawRect(this.Field1092, 0, this.Field1094, this.Field1095);
        if (this.isFocused) {
            BaseCanvas.g.setColor(LAF.Field1283);
            BaseCanvas.g.drawRect(this.Field1092 + 1, 1, this.Field1094 - 2, this.Field1095 - 2);
        }
    }

    ///////@Override // defpackage.Class184
    public final void paintBackground() {
        if (LAF.Field1298 == 1) {
            if (this.isFocused || this.border > 0) {
                BaseCanvas.g.setColor(16777215);
                BaseCanvas.g.fillRect(this.Field1092 + 1, 1, this.Field1094 - 1, this.h - 2);
            }
            if (Field1077 || this.Field1076 == 1) {
                return;
            }
            BaseCanvas.g.setColor(8955067);
            BaseCanvas.g.fillRect((this.w - this.Field1079) - 3, 3, this.Field1079, this.Field1095 - 5);
            this.Field1088.drawString(BaseCanvas.g, Field1084[this.Field1083], this.w - 3, (this.Field1095 - this.Field1088.getHeight()) >> 1, 24);
            return;
        }
        if (this.isFocused) {
            BaseCanvas.g.setColor(LAF.Field1286);
            if (this.Field1082 == 0) {
                BaseCanvas.g.fillRoundRect(0, 0, this.w - 1, this.h - 1, LAF.Field1297, LAF.Field1297);
            } else {
                BaseCanvas.g.fillRoundRect(0 + this.Field1082, 0, (this.w - 1) - this.Field1082, this.h - 1, LAF.Field1297, LAF.Field1297);
            }
        }
        if (Field1077 || !this.Field1078) {
            return;
        }
        BaseCanvas.g.setColor(LAF.Field1287);
        BaseCanvas.g.fillRect((this.w - this.Field1079) - 3, 3, this.Field1079, this.Field1095 - 4);
        BaseCanvas.g.fillRect(this.w - 3, 4, 1, this.Field1095 - 6);
        this.Field1088.drawString(BaseCanvas.g, Field1084[this.Field1083], this.w - 4, (this.Field1095 - this.Field1088.getHeight()) >> 1, 24);
    }

    ///////@Override // defpackage.Class184
    public final void paint() {
        if (this.text.length() == 0) {
            this.Field1067 = this.Field1096;
        } else if (this.Field1076 == 2) {
            this.Field1067 = this.Field1066;
        } else {
            this.Field1067 = this.text;
        }
        if (this.Field1082 == 0) {
            this.Field1087.drawString(BaseCanvas.g, this.Field1067, LAF.LOT_PADDING + this.Field1071, (((this.h - this.Field1087.getHeight()) - this.padding) - this.border) >> 1, 20);
        } else {
            BaseCanvas.g.clipRect(0, 0, ((this.Field1092 + this.Field1094) - this.Field1079) - 6, this.Field1095);
            if (this.isFocused) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field1081, -this.Field1098, (this.h - this.Field1087.getHeight()) >> 1, 20);
            } else {
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field1081, 0, (this.h - this.Field1087.getHeight()) >> 1, 20);
            }
            BaseCanvas.g.setClip(this.Field1092 + 3, 0, (this.Field1094 - this.Field1079) - 6, this.Field1095);
            this.Field1087.drawString(BaseCanvas.g, this.Field1067, LAF.LOT_PADDING + this.Field1071 + this.Field1082, (this.Field1095 - this.Field1087.getHeight()) >> 1, 20);
        }
        if (this.isFocused && this.Field1073 == 0) {
            if (this.Field1075 > 0 || (this.Field1069 / 5) % 2 == 0) {
                BaseCanvas.g.setColor(LAF.Field1288);
                if (this.Field1082 == 0) {
                    BaseCanvas.g.fillRect(LAF.LOT_PADDING + this.Field1071 + this.Field1087.getWidth(this.Field1067.substring(0, this.Field1068)) + 1, ((this.h - Field1062) >> 1) + 1, 1, Field1062);
                } else {
                    BaseCanvas.g.fillRect(LAF.LOT_PADDING + this.Field1071 + this.Field1082 + this.Field1087.getWidth(this.Field1067.substring(0, this.Field1068)) + 1, ((this.h - Field1062) >> 1) + 1, 1, Field1062);
                }
            }
        }
    }

    private void Method25() {
        if (this.Field1076 == 2) {
            this.Field1066 = "";
            for (int i = 0; i < this.text.length(); i++) {
                this.Field1066 = new StringBuffer().append(this.Field1066).append("*").toString();
            }
            if (this.Field1073 <= 0 || this.Field1068 <= 0) {
                return;
            }
            this.Field1066 = new StringBuffer().append(this.Field1066.substring(0, this.Field1068 - 1)).append(this.text.charAt(this.Field1068 - 1)).append(this.Field1066.substring(this.Field1068, this.Field1066.length())).toString();
        }
    }

    ///////@Override // defpackage.Class184
    public final void update() {
        super.update();
        this.Field1069++;
        if (this.Field1073 > 0) {
            this.Field1073--;
            if (this.Field1073 == 0) {
                this.Field1074 = 0;
                if (this.Field1083 == 1 && this.Field1072 != Field1085) {
                    this.Field1083 = 0;
                }
                this.Field1072 = -1982;
                Method25();
            }
        }
        if (this.Field1075 > 0) {
            this.Field1075--;
        }
        if (System.currentTimeMillis() <= 100 || this.Field1081 == null || this.Field1082 >= this.Field1087.getWidth(this.Field1081)) {
            return;
        }
        int i = this.Field1098 + 1;
        this.Field1098 = i;
        this.Field1098 = i >= this.Field1087.getWidth(this.Field1081) ? -this.Field1082 : this.Field1098;
    }

    public final String getText() {
        return this.text;
    }

    public final void setText(String str) {
        if (str == null) {
            return;
        }
        this.Field1072 = -1982;
        this.Field1073 = 0;
        this.Field1074 = 0;
        this.text = str;
        this.Field1067 = str;
        Method25();
        this.Field1068 = str.length();
        Method7();
    }

    ///////@Override // defpackage.Class184
    public final boolean pointerReleased(int i, int i2) {
        if (this.isPressed) {
            this.isPressed = false;
            TextBox textBox = new TextBox("", "", 500, 0);
            textBox.addCommand(new  javax.microedition.lcdui.Command(T.gL(6), 4, 0));
            textBox.addCommand(new  javax.microedition.lcdui.Command(T.gL(0), 2, 0));
            textBox.setCommandListener(new Class169(this, textBox));
            if (this.Field1076 == 2) {
                textBox.setConstraints(65536);
            } else if (this.Field1076 == 1) {
                textBox.setConstraints(2);
            } else {
                textBox.setConstraints(0);
            }
            textBox.setString(this.text);
            textBox.setMaxSize(this.Field1070);
            Display.getDisplay(BaseCanvas.instance.midlet).setCurrent(textBox);
            return true;
        }
        return false;
    }

    ///////@Override // defpackage.Class184
    public final boolean checkKeys(int i, int i2) {
        if (i == 1) {
            return false;
        }
        if (i2 == -8) {
            clear();
            return true;
        }
        if (i2 >= 65 && i2 <= 122) {
            Field1077 = true;
            this.Field1079 = 0;
        }
        if (Field1077) {
            if (i2 == 45) {
                if (i2 == this.Field1072 && this.Field1073 < Field1061[0] && this.Field1076 != 1) {
                    this.text = new StringBuffer().append(this.text.substring(0, this.Field1068 == 0 ? 0 : this.Field1068 - 1)).append('_').toString();
                    this.Field1067 = this.text;
                    Method25();
                    Method7();
                    this.Field1072 = -1982;
                    return true;
                }
                this.Field1072 = 45;
            }
            if (this.isVisible && Font.isEmotion && i2 == Field1086 && this.Field1076 == 0 && Field1091 != null) {
                Field1091.actionPerformed(new Object[]{Field1091, this});
                return true;
            } else if (this.Field1076 == 1) {
                if (i2 < 48 || i2 > 57) {
                    return false;
                }
                return Method272(i2);
            } else if (i2 >= 32) {
                return Method272(i2);
            }
        }
        if (i2 == Field1085) {
            if (this.Field1076 != 1) {
                int i3 = this.Field1083 + 1;
                this.Field1083 = i3;
                this.Field1083 = i3 % 4;
            }
            this.Field1073 = 1;
            this.Field1072 = i2;
            return true;
        } else if (this.isVisible && Font.isEmotion && i2 == Field1086 && this.Field1076 == 0 && Field1091 != null) {
            Field1091.actionPerformed(new Object[]{Field1091, this});
            return true;
        } else {
            if (i2 == 42) {
                i2 = 58;
            }
            if (i2 == 35) {
                i2 = 59;
            }
            if (i2 < 48 || i2 > 59) {
                this.Field1074 = 0;
                this.Field1072 = -1982;
                if (i2 == -3) {
                    if (this.Field1068 > 0) {
                        this.Field1068--;
                        Method7();
                        this.Field1075 = 10;
                        return true;
                    }
                    return false;
                } else if (i2 != -4) {
                    this.Field1072 = i2;
                    return false;
                } else if (this.Field1068 < this.text.length()) {
                    this.Field1068++;
                    Method7();
                    this.Field1075 = 10;
                    return true;
                } else {
                    return false;
                }
            } else if (this.Field1076 != 0 && this.Field1076 != 2 && this.Field1076 != 3) {
                if (this.Field1076 == 1) {
                    Method272(i2);
                    this.Field1073 = 1;
                    return true;
                }
                return true;
            } else {
                int i4 = i2;
                String[] strArr = this.Field1076 == 3 ? Field1064 : Field1063;
                if (i4 == this.Field1072) {
                    this.Field1074 = (this.Field1074 + 1) % strArr[i4 - 48].length();
                    char charAt = strArr[i4 - 48].charAt(this.Field1074);
                    String stringBuffer = new StringBuffer().append(this.text.substring(0, this.Field1068 > 0 ? this.Field1068 - 1 : 0)).append(this.Field1083 == 0 ? Character.toLowerCase(charAt) : this.Field1083 == 1 ? Character.toUpperCase(charAt) : this.Field1083 == 2 ? Character.toUpperCase(charAt) : strArr[i4 - 48].charAt(strArr[i4 - 48].length() - 1)).toString();
                    if (this.Field1068 < this.text.length()) {
                        stringBuffer = new StringBuffer().append(stringBuffer).append(this.text.substring(this.Field1068, this.text.length())).toString();
                    }
                    this.text = stringBuffer;
                    this.Field1073 = Field1061[0];
                    Method25();
                } else if (this.text.length() < this.Field1070) {
                    if (this.Field1083 == 1 && this.Field1072 != -1982) {
                        this.Field1083 = 0;
                    }
                    this.Field1074 = 0;
                    char charAt2 = strArr[i4 - 48].charAt(this.Field1074);
                    String stringBuffer2 = new StringBuffer().append(this.text.substring(0, this.Field1068)).append(this.Field1083 == 0 ? Character.toLowerCase(charAt2) : this.Field1083 == 1 ? Character.toUpperCase(charAt2) : this.Field1083 == 2 ? Character.toUpperCase(charAt2) : strArr[i4 - 48].charAt(strArr[i4 - 48].length() - 1)).toString();
                    if (this.Field1068 < this.text.length()) {
                        stringBuffer2 = new StringBuffer().append(stringBuffer2).append(this.text.substring(this.Field1068, this.text.length())).toString();
                    }
                    this.text = stringBuffer2;
                    this.Field1073 = Field1061[0];
                    this.Field1068++;
                    Method25();
                    Method7();
                }
                this.Field1072 = i4;
                return true;
            }
        }
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        if (((Command) ((Object[]) obj)[0]) == this.Field1090) {
            clear();
        }
    }

    public final void Method104(int i) {
        if (i > this.w - 60) {
            i = this.w - 60;
        }
        this.Field1082 = i;
        Method7();
    }
}

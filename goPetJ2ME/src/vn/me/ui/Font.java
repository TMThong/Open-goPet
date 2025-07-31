package vn.me.ui;

import vn.me.ui.common.Effects;
import java.util.Vector;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;

public final class Font {

    public static int emoSize;
    public static String[] icons;
    public static Image iconImage;
    private Image imgFont;
    private String charsets;
    private byte[] charWidths;
    public int charHeight;
    private int charSpace;
    private boolean type = true;
    public static String[] emotions = {":D", ":P", ":)", ":@", "(c)", "/--", "(w)", "(b)", ":(", "(d)", "(s)", "8|", "(y)", "(n)", ":*", "U-", "(l)", ":S", "(?)", ":zZ", "(B)", "(h)", "(u)", "@^", "@-"};
    public static Image emotionsImage = null;
    public static boolean isEmotion = true;

    public Font(Font font, int i) {
        this.charsets = font.charsets;
        this.charWidths = font.charWidths;
        this.charHeight = font.charHeight;
        this.charSpace = font.charSpace;
        this.imgFont = Effects.changeColor(font.imgFont, -16777216);
    }

    public Font(Font font, int i, int i2) {
        this.charsets = font.charsets;
        this.charWidths = font.charWidths;
        this.charHeight = font.charHeight;
        this.charSpace = font.charSpace;
        this.imgFont = Effects.changeColor(font.imgFont, i, i2);
    }

    public Font(String str, byte[] bArr, int i, Image image, int i2) {
        this.charsets = str;
        this.charWidths = bArr;
        this.charHeight = i;
        this.charSpace = i2;
        this.imgFont = image;
    }

    public final void drawString(Graphics g, String st, int x, int y, int align) {
        int x1 = x;
        int len = st.length();
        if ((align & 8) > 0) {
            x1 = x - getWidth(st);
        } else if ((align & 1) > 0) {
            x1 = x - (getWidth(st) >> 1);
        }
        if (isEmotion && emotionsImage != null) {
            for (int i = 0; i < emotions.length; i++) {
                String emotion = emotions[i];
                int indexEmotion = st.indexOf(emotion);
                if (indexEmotion > -1) {
                    String before = st.substring(0, indexEmotion);
                    String after = st.substring(indexEmotion + emotion.length(), st.length());
                    drawString(g, before, x1, y, 20);
                    int beforeWidth = getWidth(before);
                    g.drawRegion(emotionsImage, i * emoSize, 0, emoSize, emoSize, 0, x1 + beforeWidth, y + (this.charHeight >> 1), 6);
                    drawString(g, after, emoSize + x1 + beforeWidth, y, 20);
                    return;
                }
            }
        }
        if (icons != null && iconImage != null) {
            for (int i = 0; i < icons.length; i++) {
                String emotion = icons[i];
                int indexEmotion = st.indexOf(emotion);
                if (indexEmotion > -1) {
                    String before = st.substring(0, indexEmotion);
                    String after = st.substring(indexEmotion + emotion.length(), st.length());
                    drawString(g, before, x1, y, 20);
                    int beforeWidth = getWidth(before);
                    g.drawRegion(iconImage, i * 15, 0, 15, 15, 0, x1 + beforeWidth, y + (this.charHeight >> 1), 6);
                    drawString(g, after, emoSize + x1 + beforeWidth, y, 20);
                    return;
                }
            }
        }
        int i2 = 0;
        while (i2 < len) {
            int pos = this.charsets.indexOf(st.charAt(i2));
            if (pos == -1) {
                pos = 0;
            }
            if (pos > -1) {
                if (this.type) {
                    g.drawRegion(this.imgFont, 0, pos * this.charHeight, this.charWidths[pos], this.charHeight, 0, x1, y, 20);
                }
            }
            x1 += this.charWidths[pos] + (i2 < len - 1 ? this.charSpace : 0);
            i2++;
        }
    }

    public final void drawString(Graphics graphics, String str, int i, int i2, int i3, int i4) {
        Font.this.drawString(graphics, str, i, i2, i3);
    }

    public final int getWidth(String st) {
        if (isEmotion && emotionsImage != null && emotions != null) {
            for (int i = 0; i < emotions.length; i++) {
                String emotion = emotions[i];
                int indexEmotion = st.indexOf(emotion);
                if (indexEmotion > -1) {
                    String before = st.substring(0, indexEmotion);
                    String after = st.substring(indexEmotion + emotion.length(), st.length());
                    int returnValue = 0 + getWidth(before);
                    return returnValue + emoSize + getWidth(after);
                }
            }
        }
        
        if (icons != null && iconImage != null) {
            for (int i = 0; i < icons.length; i++) {
                String emotion = icons[i];
                int indexEmotion = st.indexOf(emotion);
                if (indexEmotion > -1) {
                    String before = st.substring(0, indexEmotion);
                    String after = st.substring(indexEmotion + emotion.length(), st.length());
                    int returnValue = 0 + getWidth(before);
                    return returnValue + emoSize + getWidth(after);
                }
            }
        }
        int len = 0;
        int size = st.length();
        int i2 = 0;
        while (i2 < size) {
            int pos = this.charsets.indexOf(st.charAt(i2));
            if (pos == -1) {
                pos = 0;
            }
            len += this.charWidths[pos] + (i2 < size - 1 ? this.charSpace : 0);
            i2++;
        }
        return len;
    }

    public final String[] wrap(String text, int width) {
        Vector list = new Vector();
        int position = 0;
        int length = text.length();

        for (int start = 0; position < length; start = position) {
            int i = position;

            int lastBreak;
            for (lastBreak = -1; i < length; ++i) {
                int subW = this.getWidth(text.substring(position, i + 1));
                if (subW > width) {
                    if (lastBreak == -1) {
                        lastBreak = i;
                    }
                    break;
                }

                if (text.charAt(i) == ' ') {
                    lastBreak = i;
                } else if (text.charAt(i) == '\n') {
                    lastBreak = i;
                    break;
                }
            }

            if (i != length && lastBreak > position) {
                position = lastBreak;
            } else {
                position = i;
            }

            list.addElement(text.substring(start, position).trim());
            if (position >= 0 && position < length && text.charAt(position) == '\n') {
                ++position;
            }
        }

        String[] strs = new String[list.size()];
        list.copyInto(strs);
        return strs;
    }

    public final int getHeight() {
        return this.charHeight;
    }
}

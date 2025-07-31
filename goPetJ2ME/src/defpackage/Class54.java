package defpackage;

import javax.microedition.lcdui.Image;

/* renamed from: Class54  reason: default package */
/* loaded from: gopet_repackage.jar:Class54.class */
public final class Class54 implements Class62 {
    private static Class54 Field358;

    private Class54() {
    }

    public static Class54 Method496() {
        if (Field358 == null) {
            Field358 = new Class54();
        }
        return Field358;
    }

    ///////@Override // defpackage.Class62
    public final Image[] Method17(String str) {
        Image[] imageArr = new Image[1];
        if (str.equals("/pet/battle/skills/125.anu")) {
            imageArr[0] = Method497("/pet/battle/skills/125.png");
        } else if (str.equals("/pet/battle/skills/126.anu")) {
            imageArr[0] = Method497("/pet/battle/skills/126.png");
        } else if (str.equals("/pet/battle/skills/127.anu")) {
            imageArr[0] = Method497("/pet/battle/skills/127.png");
        } else if (str.equals("/pet/battle/skills/128.anu")) {
            imageArr[0] = Method497("/pet/battle/skills/128.png");
        } else if (str.equals("/pet/battle/skills/129.anu")) {
            imageArr[0] = Method497("/pet/battle/skills/129.png");
        } else if (str.equals("/pet/battle/skills/130.anu")) {
            imageArr[0] = Method497("/pet/battle/skills/130.png");
        }
        return imageArr;
    }

    private static Image Method497(String str) {
        try {
            return Image.createImage(str);
        } catch (Exception unused) {
            System.out.println(new StringBuffer("Error loading Image ").append(str).toString());
            return null;
        }
    }
}

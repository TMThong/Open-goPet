package defpackage;

import vn.me.ui.common.ResourceManager;
import java.io.DataInputStream;
import java.io.IOException;
import java.util.Hashtable;
import javax.microedition.lcdui.Image;

/* renamed from: Class48  reason: default package */
 /* loaded from: gopet_repackage.jar:Class48.class */
public final class Class48 {

    public Hashtable Field307 = new Hashtable();
    private Image Field308 = Image.createImage(1, 1);
    private Hashtable Field309 = new Hashtable();

    public final void Method454(String str, byte[] bArr) {
        Image createImage = Image.createImage(bArr, 0, bArr.length);
        if (createImage != null) {
            this.Field307.put(str, createImage);
        }
    }

    public final Image requestImg(String str) {
        Image image = (Image) this.Field307.get(str);
        if (image == null) {
            MEService.Method444(str, 1);
            this.Field307.put(str, this.Field308);
            return null;
        } else if (image != this.Field308) {
            return image;
        } else {
            return null;
        }
    }

    public final void Method456(String str) {
        this.Field307.remove(str);
    }

    public static SkillEffect[] loadSKill(String str) {
        Image image = null;
        Image image2 = null;
        try {
            image = Image.createImage(new StringBuffer().append(str).append(".png").toString());
            image2 = image;
        } catch (IOException e) {
            e.printStackTrace();
        }
        SkillAnimation class188 = new SkillAnimation();
        class188.read(new DataInputStream(ResourceManager.getResource(str)));
        for (int i = 0; i < class188.Field1263.length; i++) {
            class188.Field1263[i].imgSkill = image2;
        }
        SkillEffect[] skillEffects = new SkillEffect[class188.Field1263.length];
        for (int i2 = 0; i2 < skillEffects.length; i2++) {
            skillEffects[i2] = new SkillEffect(class188.Field1263[i2]);
        }
        return skillEffects;
    }

    public final void Method458(String str, byte[] bArr) {
        Image createImage = Image.createImage(bArr, 0, bArr.length);
        if (createImage != null) {
            this.Field309.put(str, createImage);
        }
    }

    public final Image Method459(String str) {
        Image image = (Image) this.Field309.get(str);
        if (image == null) {
            MEService.Method444(str, 2);
            this.Field309.put(str, this.Field308);
            return null;
        } else if (image != this.Field308) {
            return image;
        } else {
            return null;
        }
    }
}

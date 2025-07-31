package defpackage;

import vn.me.ui.common.ResourceManager;
import vn.me.screen.GameScene;
import vn.me.screen.PetGameScreen;
import java.io.DataInputStream;
import java.io.IOException;
import javax.microedition.lcdui.Image;

/* renamed from: Class53  reason: default package */
/* loaded from: gopet_repackage.jar:Class53.class */
public final class Class53 extends Class135 {
    private Class52 Field354;
    private int Field355;
    private int Field356;
    private final PetGameScreen Field357;

    public Class53(PetGameScreen class49, GameScene class134, int i, mCharacter class126) {
        super(class49, class134, (byte) i, class126, null);
        this.Field357 = class49;
        switch (i) {
            case 0:
                if (class49.Field328 == null) {
                    Image image = null;
                    Image image2 = null;
                    try {
                        image = Image.createImage("/pet/petInteract/kiss.png");
                        image2 = image;
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                    SkillAnimation class188 = new SkillAnimation();
                    class188.read(new DataInputStream(ResourceManager.getResource("/pet/petInteract/kiss")));
                    for (int i2 = 0; i2 < class188.Field1263.length; i2++) {
                        class188.Field1263[i2].imgSkill = image2;
                    }
                    class49.Field328 = new SkillEffect(class188.Field1263[0]);
                }
                this.Field918 = System.currentTimeMillis();
                return;
            case 1:
                if (class49.Field329 == null) {
                    Image image3 = null;
                    Image image4 = null;
                    try {
                        image3 = Image.createImage("/pet/petInteract/play.png");
                        image4 = image3;
                    } catch (IOException e2) {
                        e2.printStackTrace();
                    }
                    SkillAnimation class1882 = new SkillAnimation();
                    class1882.read(new DataInputStream(ResourceManager.getResource("/pet/petInteract/play")));
                    for (int i3 = 0; i3 < class1882.Field1263.length; i3++) {
                        class1882.Field1263[i3].imgSkill = image4;
                    }
                    class49.Field329 = new SkillEffect(class1882.Field1263[0]);
                }
                this.Field918 = System.currentTimeMillis();
                return;
            case 2:
                if (class49.Field330 == null) {
                    Image image5 = null;
                    Image image6 = null;
                    try {
                        image5 = Image.createImage("/pet/petInteract/poke.png");
                        image6 = image5;
                    } catch (IOException e3) {
                        e3.printStackTrace();
                    }
                    SkillAnimation class1883 = new SkillAnimation();
                    class1883.read(new DataInputStream(ResourceManager.getResource("/pet/petInteract/poke")));
                    for (int i4 = 0; i4 < class1883.Field1263.length; i4++) {
                        class1883.Field1263[i4].imgSkill = image6;
                    }
                    class49.Field330 = new SkillEffect(class1883.Field1263[0]);
                }
                this.Field918 = System.currentTimeMillis();
                return;
            default:
                return;
        }
    }

    ///////@Override // defpackage.Class135
    public final void Method188() {
        switch (this.Field915) {
            case 0:
                switch (this.Field917) {
                    case 0:
                        this.Field354 = new Class52(this.Field357, 0);
                        Pet class39 = ((MyCharacter) this.Field916).ownerPet;
                        this.Field354.xChar = class39.xChar + 5;
                        this.Field354.yChar = class39.yChar + 1;
                        this.Field357.addObject(this.Field354);
                        this.Field357.Method200(this.Field354);
                        this.Field917 = (byte) 1;
                        if (this.Field916.Id == SceneManage.myCharacter.Id) {
                            this.Field357.canMove = false;
                            return;
                        }
                        return;
                    case 1:
                        if (System.currentTimeMillis() - this.Field918 >= 2000) {
                            this.Field357.removeGameObject(this.Field354);
                            this.Field914 = true;
                            if (this.Field916.Id == SceneManage.myCharacter.Id) {
                                this.Field357.canMove = true;
                                return;
                            }
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 1:
                switch (this.Field917) {
                    case 0:
                        this.Field354 = new Class52(this.Field357, 1);
                        Pet class392 = ((MyCharacter) this.Field916).ownerPet;
                        if (class392 == null) {
                            return;
                        }
                        this.Field355 = class392.xChar;
                        this.Field356 = class392.yChar;
                        class392.yChar = this.Field916.yChar;
                        if (class392.xChar < this.Field916.xChar) {
                            class392.xChar = this.Field916.xChar - 40;
                            class392.Field248 = 2;
                            this.Field916.Field807 = 0;
                        } else {
                            class392.xChar = this.Field916.xChar - 40;
                            class392.Field248 = 0;
                            this.Field916.Field807 = 1;
                        }
                        this.Field354.xChar = (class392.xChar + this.Field916.xChar) >> 1;
                        this.Field354.yChar = class392.yChar + 1;
                        this.Field357.addObject(this.Field354);
                        this.Field357.Method200(this.Field354);
                        this.Field917 = (byte) 1;
                        if (this.Field916.Id == SceneManage.myCharacter.Id) {
                            this.Field357.canMove = false;
                            return;
                        }
                        return;
                    case 1:
                        if (System.currentTimeMillis() - this.Field918 >= 8000) {
                            this.Field357.removeGameObject(this.Field354);
                            this.Field914 = true;
                            if (this.Field916.Id == SceneManage.myCharacter.Id) {
                                this.Field357.canMove = true;
                            }
                            Pet class393 = ((MyCharacter) this.Field916).ownerPet;
                            class393.xChar = this.Field355;
                            class393.yChar = this.Field356;
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 2:
                switch (this.Field917) {
                    case 0:
                        this.Field354 = new Class52(this.Field357, 2);
                        Pet class394 = ((MyCharacter) this.Field916).ownerPet;
                        this.Field354.xChar = class394.xChar + 5;
                        this.Field354.yChar = class394.yChar + 1;
                        this.Field357.addObject(this.Field354);
                        this.Field357.Method200(this.Field354);
                        this.Field917 = (byte) 1;
                        if (this.Field916.Id == SceneManage.myCharacter.Id) {
                            this.Field357.canMove = false;
                            return;
                        }
                        return;
                    case 1:
                        if (System.currentTimeMillis() - this.Field918 >= 2000) {
                            this.Field357.removeGameObject(this.Field354);
                            this.Field914 = true;
                            if (this.Field916.Id == SceneManage.myCharacter.Id) {
                                this.Field357.canMove = true;
                                return;
                            }
                            return;
                        }
                        return;
                    default:
                        return;
                }
            default:
                return;
        }
    }
}

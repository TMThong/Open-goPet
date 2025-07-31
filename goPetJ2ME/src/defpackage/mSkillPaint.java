package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.core.BaseCanvas;
import java.util.Hashtable;
import java.util.Vector;
import javax.microedition.lcdui.Image;

/* renamed from: Class34  reason: default package */
 /* loaded from: gopet_repackage.jar:Class34.class */
public final class mSkillPaint extends mObject {

    boolean isPlayer;
    long curTime;
    int turnTime;
    int timeMax;
    public int Field198;
    private Image petImg;
    public int Field200;
    public int Field201;
    public int Field202;
    boolean Field203;
    private Class36 Field205;
    boolean paintSkill;
    boolean Field207;
    int skillId;
    int Field209;
    int Field210;
    boolean Field211;
    String Field212;
    int Field213;
    int Field214;
    Class38 Field216;
    public PetInfo petInfo;
    private Class36 Field218;
    private Class35 Field219;
    private Class36 Field220;
    private Class35 Field221;
    private Class35 Field222;
    private Class35 Field223;
    private SkillEffect oldSkill;
    private Class57 mnewSkill;
    private Vector skillEffects = new Vector();
    public int Field215 = 0;
    private final Hashtable Field226 = new Hashtable(6);

    public mSkillPaint(Class38 class38, int i, int i2, PetInfo petInf, int i3) {
        this.Field216 = class38;
        this.petInfo = petInf;
        setCollisionRec(new Rectangle(10, -30, 21, 30));
        this.Field200 = i3;
        this.xChar = i;
        this.yChar = i2;
        this.Field218 = new Class36(this, new Class35[]{new Class35(this, 5, null)});
        this.Field219 = new Class35(this, 7, null);
        this.Field220 = new Class36(this, new Class35[]{this.Field219});
        int i4 = i3 == 2 ? 1 : -1;
        this.Field221 = new Class35(this, 1, new Integer(i4 * 20));
        this.Field222 = new Class35(this, 1, new Integer(i4 * (-20)));
        this.Field223 = new Class35(this, 4, new int[]{6, i + (i4 * 20), i2 - 10});
    }

    private void Method30() {
        if (this.skillEffects.isEmpty()) {
            this.Field205 = null;
            return;
        }
        Class36 class36 = (Class36) this.skillEffects.firstElement();
        this.skillEffects.removeElementAt(0);
        this.Field205 = class36;
    }

    public final void paintInMap(int i, int i2) {
        int i3;
        Class57 newSkill;
        SkillEffect class187;
        BaseCanvas.g.translate(-i, -i2);
        if (this.petImg == null) {
            this.petImg = PetGameModel.Field284.requestImg(this.petInfo.petImgPath);
        } else {
            BaseCanvas.g.drawImage(GameResourceManager.Field603, this.xChar - 11, this.yChar - 6, 0);
            if (this.Field215 == 0) {
                PetGameModel.Field283.drawPetza(this.petImg, this.xChar - ((this.petImg.getWidth() / this.petInfo.frameNum) / 2), this.yChar - this.petImg.getHeight(), this.Field200, this.petInfo.frameNum);
            } else {
                PetRenderer class47 = PetGameModel.Field283;
                Image image = this.petImg;
                int width = this.xChar - (this.petImg.getWidth() / this.petInfo.frameNum);
                int height = this.yChar - this.petImg.getHeight();
                int i4 = this.Field200;
                int i5 = this.Field215;
                int width2 = image.getWidth() / this.petInfo.frameNum;
                int height2 = image.getHeight() / 4;
                for (int i6 = 0; i6 < height2; i6++) {
                    i4 = i6 << 2;
                    BaseCanvas.g.drawRegion(image, PetRenderer.ticks * width2, i4, width2, 4 - i5, i4, width, height + (i6 << 2) + this.petInfo.vY, 0);
                }
                long currentTimeMillis = System.currentTimeMillis();
                if (currentTimeMillis - PetRenderer.lastTime >= 200) {
                    PetRenderer.lastTime = currentTimeMillis;
                    PetRenderer.ticks = (PetRenderer.ticks + 1) % this.petInfo.frameNum;
                }
            }
            if (this.paintSkill && (class187 = this.oldSkill) != null) {
                switch (this.skillId) {
                    case 6:
                        class187.Method345(BaseCanvas.g, this.Field209, this.Field210, (this.Field200 + 2) % 4);
                        break;
                    case 7:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 16:
                    case 17:
                    case 19:
                    default:
                        class187.Method345(BaseCanvas.g, this.Field209, this.Field210, 0);
                        break;
                    case 8:
                    case 15:
                    case 18:
                    case 20:
                    case 21:
                        class187.Method345(BaseCanvas.g, this.Field209, this.Field210, this.Field200);
                        break;
                }
            }
            if (this.Field207 && (newSkill = this.mnewSkill) != null) {
                newSkill.paint(BaseCanvas.g);
                newSkill.Method475();
            }
            if (this.Field211) {
                switch (this.Field213) {
                    case 0:
                        GameResourceManager.largeFont.drawString(BaseCanvas.g, this.Field212, this.xChar, (this.yChar - this.Field214) - 20, 17);
                        break;
                    case 1:
                        GameResourceManager.regularFont.drawString(BaseCanvas.g, this.Field212, this.xChar, (this.yChar - this.Field214) - 20, 17);
                        break;
                }
            }
            if (this.Field203) {
                int i7 = this.Field201;
                int i8 = this.petInfo.maxHp;
                i3 = this.Field202;
                PetGameModel.Method466(20, i7, i8, i3, this.petInfo.maxMp, this.xChar - 10, (this.yChar - this.petImg.getHeight()) - 8);
            } else {
                int i9 = this.petInfo.hp;
                int i10 = this.petInfo.maxHp;
                i3 = this.petInfo.mp;
                PetGameModel.Method466(20, i9, i10, i3, this.petInfo.maxMp, this.xChar - 10, (this.yChar - this.petImg.getHeight()) - 8);
            }
            if (this.isPlayer && this.Field198 == 0 && this.timeMax > 0) {
                int currentTimeMillis2 = this.turnTime - ((int) (System.currentTimeMillis() - this.curTime));
                int i11 = currentTimeMillis2;
                if (currentTimeMillis2 < 0) {
                    i11 = 0;
                }
                BaseCanvas.g.setColor(16711680);
                BaseCanvas.g.fillArc(this.xChar - 5, this.yChar - 50, 10, 10, 0, (i11 * 360) / this.timeMax);
            }
        }
        BaseCanvas.g.translate(i, i2);
    }

    ///////@Override // defpackage.Class133
    public final void update(long j) {
        SkillEffect class187;
        super.update(j);
        if (this.Field205 != null) {
            boolean z = true;
            for (int i = 0; i < this.Field205.Field235.length; i++) {
                this.Field205.Field235[i].update();
                if (!this.Field205.Field235[i].Field227) {
                    z = false;
                }
            }
            if (z) {
                Method30();
            }
        } else {
            Method30();
        }
        if (!this.paintSkill || (class187 = this.oldSkill) == null) {
            return;
        }
        class187.update(j);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method0() {
        this.Field198 = 1;
        this.Field221.Method392();
        this.skillEffects.addElement(new Class36(this, new Class35[]{this.Field221}));
        this.Field223.Method392();
        this.skillEffects.addElement(new Class36(this, new Class35[]{this.Field223}));
        this.Field222.Method392();
        this.skillEffects.addElement(new Class36(this, new Class35[]{this.Field222}));
        this.skillEffects.addElement(this.Field218);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method160(int i, int i2) {
        this.Field198 = 1;
        if (i != 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 6, new Object[]{String.valueOf(i), null})}));
        }
        if (i2 != 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 6, new Object[]{String.valueOf(i2), new Integer(1)})}));
        }
        this.Field219.Method392();
        this.Field219.Field229 = new int[]{this.petInfo.hp, this.petInfo.hp + i, this.petInfo.mp, this.petInfo.mp + i2};
        this.skillEffects.addElement(this.Field220);
        this.skillEffects.addElement(this.Field218);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method79(int i) {
        this.Field198 = 1;
        this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 4, new int[]{5, this.xChar, this.yChar - 10}), new Class35(this, 3, new Integer(500)), new Class35(this, 6, new Object[]{String.valueOf(i), null})}));
        this.Field219.Method392();
        this.Field219.Field229 = new int[]{this.petInfo.hp, this.petInfo.hp + i, this.petInfo.mp, this.petInfo.mp};
        this.skillEffects.addElement(this.Field220);
        this.skillEffects.addElement(this.Field218);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method18() {
        this.Field198 = 1;
        int i = this.Field200 == 2 ? -1 : 1;
        this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 1, new Integer(i * 20)), new Class35(this, 4, new int[]{5, this.xChar, this.yChar - 10})}));
        this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 1, new Integer(i * (-20)))}));
        this.skillEffects.addElement(this.Field218);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method104(int i) {
        this.Field198 = 1;
        this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 4, new int[]{7, this.xChar, this.yChar - 15})}));
        if (this.petInfo.mp + i >= 0) {
            this.Field219.Method392();
            this.Field219.Field229 = new int[]{this.petInfo.hp, this.petInfo.hp, this.petInfo.mp, this.petInfo.mp + i};
            this.skillEffects.addElement(this.Field220);
        }
        this.skillEffects.addElement(this.Field218);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method146(int i) {
        this.Field198 = 1;
        this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 4, new int[]{4, this.xChar, this.yChar - 10}), new Class35(this, 3, new Integer(500)), new Class35(this, 6, new Object[]{String.valueOf(i), null})}));
        this.Field219.Method392();
        this.Field219.Field229 = new int[]{this.petInfo.hp, this.petInfo.hp + i, this.petInfo.mp, this.petInfo.mp};
        this.skillEffects.addElement(this.Field220);
        this.skillEffects.addElement(this.Field218);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method384(int[] iArr) {
        int i = iArr[0];
        int i2 = iArr[1];
        int i3 = iArr[2];
        this.Field198 = 1;
        Class35 class35 = new Class35(this, 4, new int[]{i, this.xChar, this.yChar - 15});
        if (i2 < 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{class35, new Class35(this, 3, new Integer(500))}));
        } else {
            this.skillEffects.addElement(new Class36(this, new Class35[]{class35}));
        }
        if (i2 != 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 6, new Object[]{String.valueOf(i2), null})}));
        }
        if (i3 != 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 6, new Object[]{String.valueOf(i3), new Integer(1)})}));
        }
        this.Field219.Method392();
        this.Field219.Field229 = new int[]{this.petInfo.hp, this.petInfo.hp + i2, this.petInfo.mp, this.petInfo.mp + i3};
        this.skillEffects.addElement(this.Field220);
        this.skillEffects.addElement(this.Field218);
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method385(int[] iArr) {
        int i = iArr[0];
        int i2 = iArr[1];
        int i3 = iArr[2];
        this.Field198 = 2;
        Class35 class35 = new Class35(this, 9, new int[]{i, this.xChar, this.yChar});
        if (i2 < 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{class35, new Class35(this, 3, new Integer(500))}));
        } else {
            this.skillEffects.addElement(new Class36(this, new Class35[]{class35}));
        }
        if (i2 != 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 6, new Object[]{String.valueOf(i2), null})}));
        }
        if (i3 != 0) {
            this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 6, new Object[]{String.valueOf(i3), new Integer(1)})}));
        }
        this.Field219.Method392();
        this.Field219.Field229 = new int[]{this.petInfo.hp, this.petInfo.hp + i2, this.petInfo.mp, this.petInfo.mp + i3};
        this.skillEffects.addElement(this.Field220);
        this.skillEffects.addElement(this.Field218);
    }

    public final void Method14() {
        this.Field198 = 1;
        this.skillEffects.addElement(new Class36(this, new Class35[]{new Class35(this, 8, null)}));
        this.skillEffects.addElement(this.Field218);
    }

    public final Class57 Method386() {
        Integer num = new Integer(this.skillId);
        Class57 class57 = (Class57) this.Field226.get(num);
        Class57 class572 = class57;
        if (class57 == null) {
            ActorFactory class59 = null;
            String stringBuffer = new StringBuffer("/pet/battle/skills/").append(this.skillId).append(".anu").toString();
            System.out.println(stringBuffer);
            try {
                class59 = ActorFactory.Method485(stringBuffer, false, Class54.Method496());
            } catch (Exception e) {
                e.printStackTrace();
            }
            Class57 class573 = new Class57(class59, this.Field209, this.Field210);
            this.Field226.put(num, class573);
            class572 = class573;
        }
        class572.Method472(0);
        class572.Method474(-1);
        return class572;
    }

    public static Class57 setNewSkill(mSkillPaint class34, Class57 nSkill) {
        class34.mnewSkill = nSkill;
        return nSkill;
    }

    public static Class57 getNewSkill(mSkillPaint class34) {
        return class34.mnewSkill;
    }

    public static SkillEffect setOldSkill(mSkillPaint class34, SkillEffect skill) {
        class34.oldSkill = skill;
        return skill;
    }

    public static SkillEffect getOldSkill(mSkillPaint class34) {
        return class34.oldSkill;
    }
}

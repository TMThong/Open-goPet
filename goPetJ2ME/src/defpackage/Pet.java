package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.screen.PetGameScreen;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.PetEffectAnimation;

public final class Pet extends mObject {

    public MyCharacter Field243;
    private Image petImg;
    private String petImgPath;
    private int frameTicks;
    private long Field247;
    public int Field248;
    public int petTemplateId;
    public int petLvl;
    private int frameWidth;
    private int frameHeight;
    private Vector Field253 = new Vector();
    public boolean isHealing;
    private int healTicks;
    private int numFrame = 2;
    private int vY = 0;
    private PetEffectAnimation[] petEffectAnimations = new PetEffectAnimation[0];

    public Pet(MyCharacter character, int i, String str, int fNum, int vY_) {
        setCollisionRec(new Rectangle(-10, -30, 20, 30));
        this.Field243 = character;
        this.petTemplateId = i;
        this.petImgPath = str;
        this.xChar = character.xChar - 10;
        this.yChar = character.yChar;
        this.numFrame = fNum;
        this.vY = vY_;
    }

    public final void paintInMap(int i, int i2) {
        BaseCanvas.g.translate(-i, -i2);
        BaseCanvas.g.drawImage(GameResourceManager.Field603, this.xChar - 11, this.yChar - 6, 0);
        if (this.petImg == null) {
            this.petImg = PetGameModel.Field284.requestImg(this.petImgPath);
            if (this.petImg != null) {
                this.frameWidth = this.petImg.getWidth() / numFrame;
                this.frameHeight = this.petImg.getHeight();
            }
        } else {
            paintEff(this.xChar, this.yChar + vY, true);
            BaseCanvas.g.drawRegion(this.petImg, this.frameTicks * this.frameWidth, 0, this.frameWidth, this.frameHeight, this.Field248, this.xChar, this.yChar - this.petImg.getHeight() + vY, 17);
            long currentTimeMillis = System.currentTimeMillis();
            if (currentTimeMillis - this.Field247 >= 200) {
                this.Field247 = currentTimeMillis;
                this.frameTicks = (this.frameTicks + 1) % numFrame;
            }
            if (this.isHealing) {
                this.healTicks++;
                if (this.healTicks > 10) {
                    this.healTicks = 0;
                }
                BaseCanvas.g.drawImage(PetGameScreen.healImage, this.xChar, (this.yChar - this.healTicks) - this.petImg.getHeight(), 3);
            }
           paintEff(this.xChar, this.yChar + vY, false);
        }
        long currentTimeMillis2 = System.currentTimeMillis();
        int i3 = 0;
        while (i3 < this.Field253.size()) {
            PetTextPopup popup = (PetTextPopup) this.Field253.elementAt(i3);
            if (popup.ticks <= currentTimeMillis2 && popup.ticks + 1000 > currentTimeMillis2) {
                switch (popup.type) {
                    case 0:
                        GameResourceManager.largeFont.drawString(BaseCanvas.g, popup.text, this.xChar, (this.yChar - popup.Field258) - 30, 17);
                        break;
                    case 1:
                        GameResourceManager.regularFont.drawString(BaseCanvas.g, popup.text, this.xChar, (this.yChar - popup.Field258) - 30, 17);
                        break;
                    case 2:
                        ResourceManager.defaultFont.drawString(BaseCanvas.g, popup.text, this.xChar, (this.yChar - popup.Field258) - 30, 17);
                        break;
                    case 3:
                        GameResourceManager.Method117().drawString(BaseCanvas.g, popup.text, this.xChar, (this.yChar - popup.Field258) - 30, 17);
                        break;
                }
                popup.Field258++;
            } else if (popup.ticks + 1000 <= currentTimeMillis2) {
                this.Field253.removeElement(popup);
            }
            i3++;
        }
        BaseCanvas.g.translate(i, i2);
    }

    public final void addEff(String str, int i, long j) {
        PetTextPopup popup = new PetTextPopup(this, (byte) 0);
        popup.ticks = j;
        popup.Field258 = 0;
        popup.text = str;
        popup.type = i;
        this.Field253.addElement(popup);
    }

    public void setPetEffect(PetEffectAnimation[] animations) {
        this.petEffectAnimations = animations;
    }

    public final void Method0() {
        this.isHealing = false;
    }

    public void update(long j) {
        super.update(j);
        for (int i = 0; i < petEffectAnimations.length; i++) {
            PetEffectAnimation petEffectAnimation = petEffectAnimations[i];
            petEffectAnimation.update();
        }
    }

    private void paintEff(int offsetX, int offsetY, boolean isDrawBefore) {
        for (int i = 0; i < petEffectAnimations.length; i++) {
            PetEffectAnimation petEffectAnimation = petEffectAnimations[i];
            if (petEffectAnimation.isDrawBeforeDrawPet == isDrawBefore) {
                petEffectAnimation.paint(BaseCanvas.g, offsetX, offsetY);
            }
        }
    }
}

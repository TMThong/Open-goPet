/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vn.me.ui.common;

import defpackage.GameResourceManager;
import defpackage.PetGameModel;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;
import vn.me.core.BaseCanvas;

/**
 *
 * @author TMThong
 */
public class PetEffectAnimation {

    public int numFrame;
    public String frameImgPath;
    public int vX;
    public int vY;
    public boolean isDrawBeforeDrawPet;
    public byte type;
    public Image image;
    public long timeFrame;
    private long lastTimeUpdate = System.currentTimeMillis();
    private int currentFrame = 0;

    public PetEffectAnimation(int numFrame, String frameImgPath, int vX, int vY, boolean isDrawBeforeDrawPet, byte type, long timeFrame) {
        this.numFrame = numFrame;
        this.frameImgPath = frameImgPath;
        this.vX = vX;
        this.vY = vY;
        this.isDrawBeforeDrawPet = isDrawBeforeDrawPet;
        this.type = type;
        this.timeFrame = timeFrame;
    }

    public void update() {
        long deltaTime = System.currentTimeMillis() - lastTimeUpdate;
        this.currentFrame = (int)((deltaTime / timeFrame) % this.numFrame);
    }

    public void paint(Graphics g, int offsetX, int offsetY) {
        Image spriteSheet = PetGameModel.Field284.requestImg(this.frameImgPath);
        if (spriteSheet != null) {
            int frameWidth = spriteSheet.getWidth() / numFrame;
            int frameHeight = spriteSheet.getHeight();
            g.drawRegion(
                    spriteSheet,
                    frameWidth * currentFrame, 
                    0,
                    frameWidth,
                    spriteSheet.getHeight(), 
                    0, 
                    offsetX + vX, 
                    offsetY + vY,
                    17);
        }
    }
}

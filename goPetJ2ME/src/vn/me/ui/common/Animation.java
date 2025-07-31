/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vn.me.ui.common;

import javax.microedition.lcdui.Image;

/**
 *
 * @author Admin
 */
public class Animation {

    public final int numFrame;
    public final String frameImgPath;
    public final int vX;
    public final int vY;
    public final boolean isDrawEnd;
    public final boolean mirrorWithChar;
    public final byte type;
    public Image image;
    public int ticks = 0;
    public static final byte TYPE_ARCHIVENMENT = 0;
    
    public Animation(int numFrame, String frameImgPath, int vX, int vY, boolean isDrawEnd, boolean mirrorWithChar, byte type) {
        this.numFrame = numFrame;
        this.frameImgPath = frameImgPath;
        this.vX = vX;
        this.vY = vY;
        this.isDrawEnd = isDrawEnd;
        this.mirrorWithChar = mirrorWithChar;
        this.type = type;
    }
}

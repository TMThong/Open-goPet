package defpackage;

import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;

public final class PetRenderer {

    public static int Field302;
    public static long Field303;
    static int ticks;
    static long lastTime;
    public PetGameModel Field306;

    public PetRenderer(PetGameModel class43) {
        this.Field306 = class43;
    }

    public final void Method447() {
        BaseCanvas.g.drawRegion(GameResourceManager.Field597, Field302 * 14, 0, 14, 14, 0, 2, 1, 0);
        GameResourceManager.Method117().drawString(BaseCanvas.g, PetGameModel.mGoldStr, 17, 4, 0);
        BaseCanvas.g.drawImage(this.Field306.gemImg, BaseCanvas.Field159, 1, 0);
        GameResourceManager.Method117().drawString(BaseCanvas.g, PetGameModel.mCoinStr, BaseCanvas.Field159 + 15, 4, 0);
        long currentTimeMillis = System.currentTimeMillis();
        if (currentTimeMillis - Field303 > 100) {
            Field302 = (Field302 + 1) % 5;
            Field303 = currentTimeMillis;
        }
    }

    public final void Method448(int i) {
        Method447();
        BaseCanvas.g.drawImage(this.Field306.tiemnangImg, BaseCanvas.Field159 << 1, 1, 0);
        GameResourceManager.Method117().drawString(BaseCanvas.g, String.valueOf(i), (BaseCanvas.Field159 << 1) + 15, 4, 0);
    }

    public static void drawPet(Image image, int i, int i2, int i3, int i4, int frameNum) {
        drawPetWithFrame(image, i, i2, i3, i4, frameNum);
    }

    public static void drawPetWithFrame(Image image, int i, int i2, int i3, int i4, int frameNum) {
        try {
            int width = image.getWidth() / frameNum;
            BaseCanvas.g.drawRegion(image, ticks * width, 0, width, image.getHeight(), i3, i, i2, i4);
            long currentTimeMillis = System.currentTimeMillis();
            if (currentTimeMillis - lastTime >= 200) {
                lastTime = currentTimeMillis;
                ticks = (ticks + 1) % frameNum;
            }
        } catch (Exception e) {
            System.out.println("defpackage.PetRenderer.drawPetWithFrame() " + frameNum);
            //e.printStackTrace();
        }
    }

    public final void drawPetza(Image image, int i, int i2, int i3, int frameNum) {
        drawPet(image, i, i2, i3, 0, frameNum);
    }

    public final void drawPetz(Image image, int i, int i2, int i3, int frameNum) {
        drawPetWithFrame(image, i, i2, i3, 0, frameNum);
    }
}

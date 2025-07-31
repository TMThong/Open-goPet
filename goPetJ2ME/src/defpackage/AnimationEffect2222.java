package defpackage;

import vn.me.core.BaseCanvas;


public final class AnimationEffect2222 extends AnimationEffect {
    private int Field116;
    private int Field117;
    private int Field118;
    private int Field119;
    private int Field120;
    private int Field121;
    private long Field122;
    private boolean Field123;
    private int Field124;

    public AnimationEffect2222(int i, int i2, boolean z) {
        this.Field1299 = 1;
        this.Field120 = i;
        this.Field121 = i2;
        this.overCommandBar = true;
        this.Field123 = z;
    }

    public final void Method337(int i) {
        super.start();
        this.Field124 = 2000;
        this.Field122 = System.currentTimeMillis();
    }

    ///////@Override // defpackage.Class193
    public final void start() {
        Method337(2000);
    }

    ///////@Override // defpackage.Class193
    public final void paint() {
        int i;
        int i2;
        BaseCanvas.g.setColor(16777215);
        int translateX = BaseCanvas.g.getTranslateX();
        int translateY = BaseCanvas.g.getTranslateY();
        if (this.Field123) {
            i = SceneManage.currentScene.Field888.Field57;
            i2 = SceneManage.currentScene.Field889.Field57;
        } else {
            i = 0;
            i2 = 0;
        }
        BaseCanvas.g.translate((-i) + this.Field120, (-i2) + this.Field121);
        BaseCanvas.g.fillTriangle(0, 0, 0 + this.Field116, 0 + this.Field117, 0 + this.Field116 + this.Field118, 0 + this.Field117 + this.Field119);
        BaseCanvas.g.translate(-translateX, -translateY);
    }

    ///////@Override // defpackage.Class193
    public final void update(long j) {
        if (j - this.Field122 >= this.Field124) {
            this.isInEffect = false;
            return;
        }
        this.Field116 = Ulti.Method370(400) - 100;
        this.Field117 = Ulti.Method370(400) - 100;
        this.Field118 = Ulti.Method370(60) - 30;
        this.Field119 = Ulti.Method370(60) - 30;
    }
}

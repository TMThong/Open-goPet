package defpackage;

/* renamed from: Class193  reason: default package */
/* loaded from: gopet_repackage.jar:Class193.class */
public abstract class AnimationEffect {
    public boolean isInEffect;
    public int Field1299 = -1;
    public boolean overCommandBar = false;

    public void start() {
        this.isInEffect = true;
    }

    public abstract void paint();

    public abstract void update(long j);
}

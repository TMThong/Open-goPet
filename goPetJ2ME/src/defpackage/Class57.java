package defpackage;

/* renamed from: Class57  reason: default package */
/* loaded from: gopet_repackage.jar:Class57.class */
public final class Class57 extends ActorRenderer {
    private int Field370;
    private int Field371;
    private byte Field372;
    private boolean Field373;

    public Class57(ActorFactory class59, int i, int i2) {
        super(class59);
        this.Field370 = i;
        this.Field371 = i2;
    }

    ///////@Override // defpackage.Class55
    public final void Method481() {
        this.Field373 = true;
    }

    ///////@Override // defpackage.Class55
    public final void Method482() {
        this.Field373 = false;
    }

    public final boolean Method483() {
        return this.Field373;
    }

    public final void Method484(byte b) {
        this.Field372 = b;
    }

    ///////@Override // defpackage.Class55
    public final byte Method480() {
        return this.Field372;
    }

    ///////@Override // defpackage.Class55
    public final int Method477() {
        return this.Field370;
    }

    ///////@Override // defpackage.Class55
    public final int Method478() {
        return this.Field371;
    }

    ///////@Override // defpackage.Class55
    public final void Method479(int i, int i2) {
        this.Field370 += i;
        this.Field371 += i2;
    }
}

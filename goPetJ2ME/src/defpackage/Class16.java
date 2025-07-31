package defpackage;

/* renamed from: Class16  reason: default package */
/* loaded from: gopet_repackage.jar:Class16.class */
public final class Class16 {
    public int Field56;
    public int Field57;
    private int Field58;
    private int Field59;

    public final void Method293() {
        if (this.Field57 != this.Field56) {
            this.Field59 = (this.Field56 - this.Field57) << 2;
            this.Field58 += this.Field59;
            this.Field57 += this.Field58 >> 4;
            this.Field58 &= 15;
        }
    }
}

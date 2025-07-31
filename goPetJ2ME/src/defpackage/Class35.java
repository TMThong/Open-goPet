package defpackage;

import thong.sdk.ISoundManagerSDK;

public final class Class35 {

    public boolean Field227;
    private long Field228;
    Object Field229;
    private int Field230;
    private int Field231;
    private byte Field232;
    private byte Field233;
    private final mSkillPaint Field234;

    public Class35(mSkillPaint class34, int i, Object obj) {
        this.Field234 = class34;
        this.Field232 = (byte) i;
        this.Field229 = obj;
    }

    public final void Method392() {
        this.Field227 = false;
        this.Field228 = 0L;
        this.Field230 = 0;
        this.Field231 = 0;
        this.Field233 = (byte) 0;
    }

    public final void update() {
        SkillEffect class187 = null;
        switch (this.Field232) {
            case 1:
                switch (this.Field233) {
                    case 0:
                        this.Field230 = this.Field234.xChar + ((Integer) this.Field229).intValue();
                        this.Field231 = this.Field234.xChar;
                        this.Field233 = (byte) 1;
                        return;
                    case 1:
                        int i = this.Field230 - this.Field231;
                        int i2 = this.Field230 - this.Field234.xChar;
                        if (this.Field230 > this.Field234.xChar) {
                            this.Field234.xChar += 4;
                        } else {
                            this.Field234.xChar -= 4;
                        }
                        if (i * i2 <= 0) {
                            this.Field234.xChar = this.Field230;
                            this.Field227 = true;
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 2:
                switch (this.Field233) {
                    case 0:
                        this.Field228 = System.currentTimeMillis();
                        this.Field230 = ((Integer) this.Field229).intValue();
                        this.Field233 = (byte) 1;
                        return;
                    case 1:
                        if (System.currentTimeMillis() - this.Field228 > this.Field230) {
                            this.Field227 = true;
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 3:
                switch (this.Field233) {
                    case 0:
                        this.Field228 = System.currentTimeMillis();
                        this.Field230 = ((Integer) this.Field229).intValue();
                        this.Field231 = this.Field234.xChar;
                        this.Field233 = (byte) 1;
                        return;
                    case 1:
                        if (System.currentTimeMillis() - this.Field228 > this.Field230) {
                            this.Field227 = true;
                        }
                        if (this.Field234.xChar == this.Field231) {
                            this.Field234.xChar--;
                            return;
                        } else if (this.Field234.xChar >= this.Field231) {
                            this.Field234.xChar = this.Field231;
                            return;
                        } else {
                            this.Field234.xChar += 2;
                            return;
                        }
                    default:
                        return;
                }
            case 4:
                switch (this.Field233) {
                    case 0:
                        this.Field228 = System.currentTimeMillis();
                        this.Field233 = (byte) 1;
                        int[] iArr = (int[]) this.Field229;
                        this.Field234.skillId = iArr[0];
                        this.Field234.Field209 = iArr[1];
                        this.Field234.Field210 = iArr[2];
                        this.Field234.paintSkill = true;
                        mSkillPaint.setOldSkill(this.Field234, null);
                        mSkillPaint class34 = this.Field234;
                        mSkillPaint class342 = this.Field234;
                        if (class342.skillId >= 4) {
                            if (class342.skillId < 6) {
                                class187 = PetGameModel.skill.SlashEffect[class342.skillId - 4];
                            } else if (class342.skillId == 6) {
                                class187 = PetGameModel.skill.attackEffect;
                                ISoundManagerSDK.playSoundEffect("s_hit");
                            } else if (class342.skillId == 7) {
                                class187 = PetGameModel.skill.gong;
                            } else if (class342.skillId >= 8) {
                                class187 = PetGameModel.skill.loadCustomSkill(class342.skillId - 8);
                                ISoundManagerSDK.playSoundEffect("s_hit");
                            }
                            mSkillPaint.setOldSkill(class34, class187);
                            if (mSkillPaint.getOldSkill(this.Field234) != null) {
                                mSkillPaint.getOldSkill(this.Field234).Method344();
                                return;
                            }
                            this.Field227 = true;
                            this.Field234.paintSkill = false;
                            return;
                        }
                        class187 = null;
                        mSkillPaint.setOldSkill(class34, class187);
                        if (mSkillPaint.getOldSkill(this.Field234) == null) {
                        }
                    case 1:
                        if (mSkillPaint.getOldSkill(this.Field234) != null) {
                            SkillEffect Method390 = mSkillPaint.getOldSkill(this.Field234);
                            if (Method390.Field1259 == Method390.Field1258.Field1253 - 1) {
                                this.Field227 = true;
                                this.Field234.paintSkill = false;
                                return;
                            }
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 5:
                this.Field234.Field198 = 0;
                this.Field227 = true;
                if (this.Field234.Field216 != null) {
                    this.Field234.Field216.setDelayBattle();
                    return;
                }
                return;
            case 6:
                switch (this.Field233) {
                    case 0:
                        this.Field228 = System.currentTimeMillis();
                        this.Field233 = (byte) 1;
                        this.Field234.Field214 = 0;
                        this.Field234.Field211 = true;
                        Object[] objArr = (Object[]) this.Field229;
                        this.Field234.Field212 = (String) objArr[0];
                        Integer num = (Integer) objArr[1];
                        if (num == null) {
                            this.Field234.Field213 = 0;
                            return;
                        } else {
                            this.Field234.Field213 = num.intValue();
                            return;
                        }
                    case 1:
                        if (System.currentTimeMillis() - this.Field228 > 1000) {
                            this.Field227 = true;
                            this.Field234.Field211 = false;
                            this.Field234.Field213 = 0;
                        }
                        this.Field234.Field214++;
                        return;
                    default:
                        return;
                }
            case 7:
                switch (this.Field233) {
                    case 0:
                        int[] iArr2 = (int[]) this.Field229;
                        int i3 = iArr2[0];
                        int i4 = iArr2[1];
                        int i5 = iArr2[2];
                        int i6 = iArr2[3];
                        this.Field234.Field201 = i3;
                        this.Field234.Field202 = i5;
                        this.Field230 = i4;
                        this.Field231 = i6;
                        this.Field233 = (byte) 1;
                        this.Field234.Field203 = true;
                        return;
                    case 1:
                        boolean z = Ulti.Method369(this.Field230 - this.Field234.Field201) < 2;
                        boolean z2 = z;
                        if (z) {
                            this.Field234.petInfo.hp = this.Field230;
                            z2 = true;
                        } else {
                            this.Field234.Field201 += (this.Field230 - this.Field234.Field201) >> 1;
                        }
                        boolean z3 = Ulti.Method369(this.Field231 - this.Field234.Field202) < 2;
                        boolean z4 = z3;
                        if (z3) {
                            this.Field234.petInfo.mp = this.Field231;
                            z4 = true;
                        } else {
                            this.Field234.Field202 += (this.Field231 - this.Field234.Field202) >> 1;
                        }
                        if (z2 && z4) {
                            this.Field227 = true;
                            this.Field234.Field203 = false;
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 8:
                switch (this.Field233) {
                    case 0:
                        this.Field234.Field215 = 1;
                        this.Field233 = (byte) 1;
                        return;
                    case 1:
                        this.Field234.Field215++;
                        if (this.Field234.Field215 == 4) {
                            this.Field227 = true;
                            return;
                        }
                        return;
                    default:
                        return;
                }
            case 9:
                switch (this.Field233) {
                    case 0:
                        this.Field228 = System.currentTimeMillis();
                        this.Field233 = (byte) 1;
                        int[] iArr3 = (int[]) this.Field229;
                        this.Field234.skillId = iArr3[0];
                        this.Field234.Field209 = iArr3[1];
                        this.Field234.Field210 = iArr3[2];
                        this.Field234.Field207 = true;
                        mSkillPaint.setNewSkill(this.Field234, null);
                        mSkillPaint.setNewSkill(this.Field234, this.Field234.Method386());
                        mSkillPaint.getNewSkill(this.Field234).Method484((byte) (this.Field234.Field200 == 0 ? 0 : 1));
                        if (mSkillPaint.getNewSkill(this.Field234) != null) {
                            mSkillPaint.getNewSkill(this.Field234).Method473(0);
                            ISoundManagerSDK.playSoundEffect("s_attack_crit");
                            return;
                        }
                        this.Field227 = true;
                        this.Field234.Field207 = false;
                        return;
                    case 1:
                        if (mSkillPaint.getNewSkill(this.Field234) == null || mSkillPaint.getNewSkill(this.Field234).Method483()) {
                            return;
                        }
                        this.Field227 = true;
                        this.Field234.Field207 = false;
                        return;
                    default:
                        return;
                }
            default:
                return;
        }

    }
}

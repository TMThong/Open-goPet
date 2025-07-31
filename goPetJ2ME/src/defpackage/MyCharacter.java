package defpackage;

import vn.me.screen.GameScene;
import vn.me.screen.PetGameScreen;

/* renamed from: Class42  reason: default package */
 /* loaded from: gopet_repackage.jar:Class42.class */
public final class MyCharacter extends mCharacter {

    public Pet ownerPet;

    public MyCharacter(int i, byte b, SceneManage class140) {
        super(i, b, class140);
    }

    public final void update(long j) {
        int i = this.xChar;
        int i2 = this.yChar;
        super.update(j);
        int i3 = this.xChar - i;
        int i4 = this.yChar - i2;
        if (this.ownerPet != null) {
            if (i3 == 0 && i4 == 0) {
                return;
            }
            Pet pet = this.ownerPet;
            int i5 = pet.Field243.xChar - pet.xChar;
            if (i5 < -30) {
                pet.xChar = pet.Field243.xChar + 30;
            } else if (i5 > 30) {
                pet.xChar = pet.Field243.xChar - 30;
            }
            int i6 = pet.Field243.yChar - pet.yChar;
            if (i6 < -15) {
                pet.yChar = pet.Field243.yChar + 15;
            } else if (i6 > 15) {
                pet.yChar = pet.Field243.yChar - 15;
            }
            if (i5 < 0) {
                pet.Field248 = 0;
            } else {
                pet.Field248 = 2;
            }
            if (i3 != 0) {
                if (pet.yChar < pet.Field243.yChar) {
                    pet.yChar += 2;
                    if (pet.yChar > pet.Field243.yChar) {
                        pet.yChar = pet.Field243.yChar;
                    }
                } else if (pet.yChar > pet.Field243.yChar) {
                    pet.yChar -= 2;
                    if (pet.yChar < pet.Field243.yChar) {
                        pet.yChar = pet.Field243.yChar;
                    }
                }
            }
        }
    }

    public final void paintInMap(int i, int i2) {
        super.paintInMap(i, i2);
    }

    public final void Method166(GameScene class134) {
        class134.removeGameObject(this.ownerPet);
        PetBattle battle = ((PetGameScreen) SceneManage.currentScene).Method451(this.Field874);
        if (battle != null) {
            battle.fastRemovePet();
        }
    }

    public final void setPet(Pet class39) {
        if (this.ownerPet != null) {
            SceneManage.currentScene.removeGameObject(this.ownerPet);
        }
        this.ownerPet = class39;
        SceneManage.currentScene.addObject(this.ownerPet);
        SceneManage.currentScene.Method200(this.ownerPet);
    }

    public final void unfollowPet(int petId) {
        if (this.ownerPet != null && this.ownerPet.petTemplateId == petId) {
            SceneManage.currentScene.removeGameObject(this.ownerPet);
        }
        this.ownerPet = null;
    }
}

package defpackage;

import vn.me.ui.Dialog;
import vn.me.screen.PetGameScreen;
import vn.me.screen.Screen;

/* renamed from: Class33  reason: default package */
 /* loaded from: gopet_repackage.jar:Class33.class */
public final class BattleInfo {

    private byte option;
    private int Field191;
    private Object Field192;
    private final PetBattle battle;

    public BattleInfo(PetBattle bt, int i, int i2, Object obj) {
        this.battle = bt;
        this.option = (byte) i;
        this.Field191 = i2;
        this.Field192 = obj;
    }

    public final void updateBattleOption() {
        switch (this.option) {
            case 0:
                this.battle.obj[this.Field191].Method0();
                return;
            case 1:
                int[] iArr = (int[]) this.Field192;
                switch (iArr[0]) {
                    case 0:
                        this.battle.obj[this.Field191].Method79(iArr[1]);
                        return;
                    case 1:
                        this.battle.obj[this.Field191].Method18();
                        return;
                    case 2:
                        this.battle.obj[this.Field191].Method146(iArr[1]);
                        return;
                    default:
                        return;
                }
            case 2:
                this.battle.isRemove = true;
                return;
            case 3:
            default:
                return;
            case 4:
                int[] iArr2 = (int[]) this.Field192;
                this.battle.obj[this.Field191].Method160(iArr2[0], iArr2[1]);
                return;
            case 5:
                this.battle.obj[this.Field191].Method104(((Integer) this.Field192).intValue());
                return;
            case 6:
                this.battle.obj[this.Field191].Method384((int[]) this.Field192);
                return;
            case 7:
                this.battle.obj[this.Field191].Method14();
                return;
            case 8:
                SceneManage.currentScene.canMove = true;
                PetGameModel.Field296 = false;
                MyCharacter class42 = (MyCharacter) SceneManage.currentScene.listChar.getChar(this.battle.objId[0]);
                if (PetBattle.Method426(this.battle) > 0) {
                    class42.ownerPet.addEff(new StringBuffer().append(String.valueOf(PetBattle.Method426(this.battle))).append(" (ngoc)").toString(), 3, System.currentTimeMillis());
                }
                if (PetBattle.Method427(this.battle) > 0) {
                    class42.ownerPet.addEff(new StringBuffer().append(String.valueOf(PetBattle.Method427(this.battle))).append(" EXP").toString(), 3, System.currentTimeMillis() + 1000);
                }
                if (PetBattle.Method428(this.battle) != null && PetBattle.Method428(this.battle).length > 0) {
                    Popup class27 = new Popup(new StringBuffer("Nhận được ").append(PetBattle.Method428(this.battle)[0].Field668).toString());
                    class27.start();
                    Screen.animationEffects.addElement(class27);
                }
                if (this.battle.Field185) {
                    Popup class272 = new Popup(new StringBuffer().append(SceneManage.currentScene.listChar.getChar(PetBattle.Method429(this.battle)).name).append(" thắng!").toString());
                    class272.start();
                    Screen.animationEffects.addElement(class272);
                } else if (PetBattle.Method429(this.battle) != this.battle.objId[0] && SceneManage.currentScene.Field879.mGOMapId != 12) {
                    GameController.Method45("Thua rồi, bạn có muốn về thành phố để điều trị?", new Command(5, ActorFactory.gL(337), this.battle), GameController.Field464);
                }
                this.battle.delayBattle = false;
                return;
            case 9:
                for (int i = 0; i < this.battle.obj.length; i++) {
                    SceneManage.currentScene.removeGameObject(this.battle.obj[i]);
                }
                for (int i2 = 0; i2 < this.battle.objId.length; i2++) {
                    MyCharacter myPet = (MyCharacter) SceneManage.currentScene.listChar.getChar(this.battle.objId[i2]);
                    if (myPet != null && myPet.ownerPet != null) {
                        myPet.ownerPet.Field864 = true;
                    }
                }
                if (!this.battle.Field185 && PetBattle.Method429(this.battle) != this.battle.objId[0]) {
                    if (this.battle.doithu.isBoss) {
                        return;
                    }
                    ((PetGameScreen) SceneManage.currentScene).addObject(this.battle.doithu);
                    ((PetGameScreen) SceneManage.currentScene).Method200(this.battle.doithu);
                }
                this.battle.delayBattle = false;
                return;
            case 10:
                ((Dialog) this.Field192).show(true);
                this.battle.delayBattle = false;
                return;
            case 11:
                this.battle.obj[this.Field191].Method385((int[]) this.Field192);
                return;

            case 12:
                for (int i = 0; i < this.battle.obj.length; i++) {
                    SceneManage.currentScene.removeGameObject(this.battle.obj[i]);
                }
                for (int i2 = 0; i2 < this.battle.objId.length; i2++) {
                    MyCharacter myPet = (MyCharacter) SceneManage.currentScene.listChar.getChar(this.battle.objId[i2]);
                    if (myPet != null && myPet.ownerPet != null) {
                        myPet.ownerPet.Field864 = true;
                    }
                }
                this.battle.delayBattle = false;
                return;
        }
    }
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package thong.auto;

import defpackage.Command;
import defpackage.GameController;
import defpackage.MEService;
import defpackage.Mob;
import defpackage.MyCharacter;
import defpackage.SceneManage;
import vn.me.screen.PetGameScreen;

/**
 *
 * @author TMThong
 */
public class AutoAttack implements IAutoBase {

    public final static AutoAttack instance = new AutoAttack();

    public int[] skills_coolDown = new int[]{0, 0, 0};
    public int[] skills_Id = new int[]{0, 0, 0};

    public void update() {
        if (SceneManage.myCharacter != null) {
            PetGameScreen gameSrc = (PetGameScreen) SceneManage.currentScene;
            if (!gameSrc.listBattle.containsKey(new Integer(SceneManage.myCharacter.Id))) {
                updateFindMob(gameSrc);
            } else {
                updateAttack(gameSrc);
            }
        }
    }

    private void updateFindMob(PetGameScreen gameSrc) {
        MEService.AutoAttack();
        timeDelayFirstAttack = System.currentTimeMillis() + 1000;
    }

    public void userUseTurn(int id) {
        if (SceneManage.myCharacter != null && SceneManage.myCharacter.Id == id) {
            for (int i = 0; i < skills_coolDown.length; i++) {
                skills_coolDown[i]--;
            }
        }
    }

    private void updateAttack(PetGameScreen gameSrc) {
        normalAttak(gameSrc);
    }

    private long timeDelay = System.currentTimeMillis();
    private long timeDelayFirstAttack = System.currentTimeMillis();

    private void normalAttak(PetGameScreen gameSrc) {
        if (gameSrc.hasAttackType == 1 || timeDelay > System.currentTimeMillis() || timeDelayFirstAttack > System.currentTimeMillis()) {
            return;
        }
        timeDelay = System.currentTimeMillis() + 4000;
        gameSrc.actionPerformed(new Object[]{new Command(318, null, gameSrc)});
    }

    public boolean isEnable() {
        if (SceneManage.myCharacter != null) {
            MyCharacter myCharacter = (MyCharacter) SceneManage.myCharacter;
            if (myCharacter.ownerPet == null) {
                return false;
            }
        }
        if (GameController.myInfo != null) {
            if (GameController.myInfo.mGoMapId == 12) {
                return false;
            }
        }
        return AutoManager.isAutoAttack;
    }
}

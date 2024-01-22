/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package place;

import data.map.GopetMap;
import data.mob.Boss;
import data.mob.Mob;
import data.mob.MobLocation;
import data.mob.MobLvInfo;
import data.mob.MobLvlMap;
import data.pet.PetTemplate;
import manager.GopetManager;
import static server.GameController.messagePetSerive;
import server.GopetCMD;
import server.Player;
import server.io.Message;
import util.Utilities;

/**
 *
 * @author MINH THONG
 */
public class ChallengePlace extends GopetPlace {

    public long placeTime = System.currentTimeMillis();
    public const long TIME_WAIT = 60 * 1000;
    public const long TIME_ATTACK = 60 * 3 * 1000;
    public const int MAX_PLAYER_JOIN = 4;
    public const int MAX_TURN = 20;
    public bool isWait = true;
    public bool isFinish = false;
    private bool isWaitForNewTurn = false;
    private int turn = 0;
    public const int[][] MOB_XY = new int[][]{
        new int[]{240, 161},
        new int[]{341, 97},
        new int[]{393, 229},
        new int[]{95, 323},
        new int[]{35, 181},
        new int[]{79, 59},
        new int[]{347, 377},
        new int[]{402, 261}
    };

    public ChallengePlace(GopetMap m, int ID)   {
        base(m, ID);
        placeTime = System.currentTimeMillis() + TIME_WAIT;
        maxPlayer = MAX_PLAYER_JOIN;
        this.numMobDieNeed = int.MAX_VALUE;
    }

    @Override
    public void add(Player player)   {
        base.add(player); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
        player.controller.sendPlaceTime(Math.round(placeTime - System.currentTimeMillis()) / 1000);
        player.controller.showBigTextEff("PHÒNG CHỜ");
    }

    @Override
    public bool canAdd(Player player)   {
        return base.canAdd(player) && isWait; // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
    }

    @Override
    public void update()   {
        base.update(); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
        if (isWait) {
            if (placeTime < System.currentTimeMillis()) {
                isWait = false;
                nextTurn();
            }
        } else if (mobs.size() == 0) {
            if (isWaitForNewTurn && System.currentTimeMillis() > placeTime) {
                nextTurn();
            } else if (!isWaitForNewTurn) {
                isWaitForNewTurn = true;
                placeTime = System.currentTimeMillis() + 15000;
                this.sendTimePlace();
            }
        }
    }

    @Override
    public bool needRemove() {
        return !isWait && (numPlayer == 0 || placeTime < System.currentTimeMillis());
    }

    @Override
    public void removeAllPlayer()   {
        for (Player player : players) {
            player.controller.LoadMap();
        }
    }

    private void nextTurn()   {
        turn++;
        isWaitForNewTurn = false;
        if (turn <= MAX_TURN) {
            PetTemplate[] templates = GopetManager.petEnable.toArray(new PetTemplate[0]);
            bool isBossTurn = turn % 5 == 0;
            if (!isBossTurn) {
                for (int i = 0; i < getNumMob(); i++) {
                    int[] XY = MOB_XY[i];
                    PetTemplate petTemplate = Utilities.randomArray(templates);
                    MobLocation mobLocation = new MobLocation(this.map.mapID, XY[0], XY[1]);
                    MobLvlMap mobLvlMap = new MobLvlMap(map.mapID, turn, turn, petTemplate.getPetId());
                    MobLvInfo mobLvInfo = GopetManager.MOBLVLINFO_CHALLENGE.get(turn);
                    Mob mob = new Mob(petTemplate, this, mobLvlMap, mobLocation, mobLvInfo);
                    mob.setElite(false);
                    mob.setMobId(-(i + 1));
                    this.addNewMob(mob);
                }
            } else {
                int indexBoss = (turn / 5) - 1;
                for (int i = 0; i < numBoss(); i++) {
                    int[] XY = MOB_XY[i];
                    MobLocation mobLocation = new MobLocation(this.map.mapID, XY[0], XY[1]);
                    Boss b = new Boss(GopetManager.ID_BOSS_CHALLENGE[indexBoss], mobLocation);
                    b.setMobId(-(i + 1));
                    this.addNewMob(b);
                }
            }

            for (Player player : players) {
                player.controller.getTaskCalculator().onNextChellengePlace(this.turn);
            }
            placeTime = System.currentTimeMillis() + TIME_ATTACK;
            this.sendMob();
            this.sendTimePlace();
            this.showBigTextEff(String.format("LƯỢT", turn));
        } else {
            isFinish = true;
        }
    }

    private int getNumMob()   {
        if (this.numPlayer <= 2) {
            return 4;
        } else {
            return 4 + (numPlayer - 2) * 2;
        }
    }

    private int numBoss()   {
        if (numPlayer >= 4) {
            return 2;
        }
        return 1;
    }

    @Override
    public void mobDie(Mob gopetMob) {
        this.mobs.remove(gopetMob);
    }

    public void sendTimePlace()   {
        Message message = messagePetSerive(GopetCMD.TIME_PLACE);
        message.putInt(Math.round(placeTime - System.currentTimeMillis()) / 1000);
        message.cleanup();
        sendMessage(message);
    }
}

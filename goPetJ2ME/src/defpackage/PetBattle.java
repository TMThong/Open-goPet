package defpackage;

import vn.me.ui.interfaces.IActionListener;
import vn.me.network.Message;
import vn.me.screen.PetGameScreen;
import java.util.Vector;
import thong.auto.AutoAttack;

/* renamed from: Class32  reason: default package */
 /* loaded from: gopet_repackage.jar:Class32.class */
public class PetBattle implements Class38, IActionListener {

    public static final int MAX_PET = 2;

    public boolean delayBattle;
    public Mob doithu;
    public boolean isRemove;
    private int winId;
    private int Field187;
    private int Field188;
    private PetItem[] Field189;
    public mSkillPaint[] obj = new mSkillPaint[MAX_PET];
    public int[] objId = new int[MAX_PET];
    int[] xPos = new int[MAX_PET];
    int[] yPos = new int[MAX_PET];
    Vector turnList = new Vector();
    public boolean Field185 = false;
    public boolean isAttackBoss = false;

    public PetBattle() {
        for (int i = 0; i < MAX_PET; i++) {
            obj[i] = null;
            objId[i] = -1;
            xPos[i] = -1;
            yPos[i] = -1;
        }
    }

    private int getId(int i) {
        for (int i2 = 0; i2 < this.objId.length; i2++) {
            if (i == this.objId[i2]) {
                return i2;
            }
        }
        return 1;
    }

    public final void onMessage(Message msg) {
        try {
            int mainTurnId = getId(msg.reader().readInt());
            setTurn(msg.reader().readInt(), msg.reader().readInt());
            switch (msg.reader().readByte()) {
                case 1:
                    this.turnList.addElement(new BattleInfo(this, 0, mainTurnId, null));
                    break;
                case 4:
                    msg.reader().readInt();
                    msg.reader().readUTF();
                    this.turnList.addElement(new BattleInfo(this, 5, mainTurnId, new Integer(msg.reader().readInt())));
                    break;
            }
            int num_eff = msg.reader().readInt();
            for (int i = 0; i < num_eff; i++) {
                int Method3432 = getId(msg.reader().readInt());
                int readInt2 = msg.reader().readInt();
                msg.reader().readUTF();
                msg.reader().readInt();
                msg.reader().readInt();
                msg.reader().readInt();
                int readInt3 = msg.reader().readInt();
                int readInt4 = msg.reader().readInt();
                msg.reader().readInt();
                msg.reader().readInt();
                if (readInt2 < 0) {
                    this.turnList.addElement(new BattleInfo(this, 4, Method3432, new int[]{readInt3, readInt4}));
                } else if (readInt2 >= 0 && readInt2 <= 2) {
                    this.turnList.addElement(new BattleInfo(this, 1, Method3432, new int[]{readInt2, readInt3}));
                } else if (readInt2 >= 101 && readInt2 < 125) {
                    this.turnList.addElement(new BattleInfo(this, 6, Method3432, new int[]{(readInt2 - 101) + 8, readInt3, readInt4}));
                } else if (readInt2 >= 125) {
                    this.turnList.addElement(new BattleInfo(this, 11, Method3432, new int[]{readInt2, readInt3, readInt4}));
                }
            }

            AutoAttack.instance.userUseTurn(mainTurnId);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public final void updatePetBattle() {
        if (this.turnList.isEmpty() || this.delayBattle) {
            return;
        }
        this.delayBattle = true;
        ((BattleInfo) this.turnList.elementAt(0)).updateBattleOption();
        this.turnList.removeElementAt(0);
    }

    public final void onLostMassage(Message msg, boolean z) {
        try {
            if (z) {
                this.winId = msg.reader().readInt();
                msg.reader().readByte();
                this.Field188 = msg.reader().readInt();
                this.Field187 = msg.reader().readInt();
                int readByte = msg.reader().readByte();
                this.Field189 = new PetItem[readByte];
                for (int i = 0; i < readByte; i++) {
                    String readUTF = msg.reader().readUTF();
                    String readUTF2 = msg.reader().readUTF();
                    this.Field189[i] = new PetItem();
                    this.Field189[i].itemId = 0;
                    this.Field189[i].Field668 = readUTF;
                    this.Field189[i].imgPathId = readUTF2;
                }
                if (this.objId[0] == SceneManage.myCharacter.Id) {
                    ((PetGameScreen) SceneManage.currentScene).Method6();
                }
                this.turnList.addElement(new BattleInfo(this, 7, (getId(this.winId) + 1) % 2, null));
                this.turnList.addElement(new BattleInfo(this, 8, 0, null));
            } else {
                this.winId = msg.reader().readInt();
                msg.reader().readByte();
                this.turnList.addElement(new BattleInfo(this, 7, (getId(this.winId) + 1) % 2, null));
            }
            this.turnList.addElement(new BattleInfo(this, 9, 0, null));
            this.turnList.addElement(new BattleInfo(this, 2, 0, null));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public final void actionPerformed(Object obj_) {
        switch (((Command) ((Object[]) obj_)[0]).cmdId) {
            case 5:
                SceneManage.warp(11, 0, mMap.getMapVersion(11));
                return;
            default:
                return;
        }
    }

    public final void setTurn(int i, int i2) {
        for (int i3 = 0; i3 < this.obj.length; i3++) {
            if (this.obj[i3] == null) {
                return;
            }
            if (this.obj[i3].isPlayer) {
                this.obj[i3].timeMax = i2;
                this.obj[i3].turnTime = i;
                this.obj[i3].curTime = System.currentTimeMillis();
            }
        }
    }

    public final void setDelayBattle() {
        this.delayBattle = false;
    }

    public final void fastRemovePet() {
        this.turnList.addElement(new BattleInfo(this, 9, 0, this.obj));
        this.turnList.addElement(new BattleInfo(this, 2, 0, null));
    }

    public final void fastRemoveMob() {
        this.turnList.addElement(new BattleInfo(this, 12, 0, this.obj));
        this.turnList.addElement(new BattleInfo(this, 2, 0, null));
    }

    public static int Method426(PetBattle class32) {
        return class32.Field188;
    }

    public static int Method427(PetBattle class32) {
        return class32.Field187;
    }

    public static PetItem[] Method428(PetBattle class32) {
        return class32.Field189;
    }

    public static int Method429(PetBattle class32) {
        return class32.winId;
    }
}

package defpackage;

import vn.me.screen.MgScreen;
import vn.me.screen.PetInfoScreen;
import vn.me.screen.ArenaMenuScreen;
import vn.me.screen.AnimationMenu;
import vn.me.screen.GuildScreen;
import vn.me.ui.interfaces.IActionListener;
import vn.me.screen.KioskScreen;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.Font;
import vn.me.screen.GemScreen;
import vn.me.screen.TrScreen;
import vn.me.screen.GameScene;
import vn.me.screen.PetTattooScreen;
import vn.me.screen.MenuScreen;
import vn.me.screen.PetGameScreen;
import vn.me.screen.Screen;
import java.io.IOException;
import java.util.Enumeration;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import thong.auto.AutoAttack;
import thong.auto.AutoDectectSpeed;
import thong.sdk.ISoundManagerSDK;
import vn.me.network.Cmd;
import vn.me.ui.InputDialog;
import vn.me.ui.common.Animation;
import vn.me.ui.common.PetEffectAnimation;
import vn.me.ui.common.Resource;
import vn.me.ui.common.T;
import vn.thong.shared.data.MoneyDisplay;

public final class PetGameModel implements Class29, Class157, IActionListener {

    private boolean Field271 = false;
    public Image gemImg;
    public Pet myPet;
    public Image tiemnangImg;
    public Image levelImg;
    public Image levelImg_red;
    public static Image arrow2Img;
    public static PetRenderer Field283;
    public static Class48 Field284;
    public static int Field285;
    public static Skill skill;
    public static int oldMyPetHp;
    public static int oldMyPetMp;
    public static boolean hasPetInfo;
    public static int Field292;
    public static int Field293;
    private static int myPetHp;
    private static int myPetMp;
    public static boolean Field296;
    public long Field297;
    public long Field298;
    public boolean Field299;
    public String Field300;
    public static long mGold = -1;
    public static long mCoin = -1;
    public static long mLua = -1;
    private static long Field274 = 0;
    public static String mGoldStr = "";
    public static String mCoinStr = "";
    public static String mLuaStr = "";
    public static String Field277 = "";
    public static int myPetMaxHp = 100;
    public static int myPetMaxMp = 100;
    public Image luaImg;

    public PetGameModel() {
        Field283 = new PetRenderer(this);
        Field284 = new Class48();
        try {
            this.gemImg = Image.createImage("/pet/Gem.png");
            arrow2Img = Image.createImage("/pet/arrow2.png");
            this.tiemnangImg = Image.createImage("/pet/tiemnang.png");
            this.luaImg = Resource.setFileTable("/common.dat", 25);
            this.levelImg = Image.createImage("/pet/level.png");
            this.levelImg_red = Image.createImage("/pet/level_red.png");
        } catch (IOException e) {
            e.printStackTrace();
        }

        Font.icons = new String[]{"(ngoc)", "(dau)", "(thoc)", "(vang)", "(str)", "(agi)", "(int)", "(atk)", "(def)", "(hp)", "(mp)", "(water)", "(thunder)", "(rock)", "(fire)", "(dark)", "(tree)", "(light)", "(sao)", "(chien)", "(bthu)", "(codo)", "(coxanh)", "(nha)", "(nguoi)", "(saoden)", "(chienluc)", "(nluong)", "(diem)", "(lua)"};
        try {
            Font.iconImage = Image.createImage("/pet/icons.png");
        } catch (IOException e2) {
            e2.printStackTrace();
        }
        skill = new Skill();
        skill.SlashEffect = Class48.loadSKill("/pet/battle/SlashEffect");
        skill.gong = Class48.loadSKill("/pet/battle/gong")[0];
        skill.attackEffect = Class48.loadSKill("/pet/battle/attackEffect")[0];
    }

    private static void setPetInfo(int hp, int maxHp, int mp, int maxMp) {
        myPetMaxHp = maxHp;
        myPetMaxMp = maxMp;
        Pet myPet = ((MyCharacter) SceneManage.myCharacter).ownerPet;
        if (hp != oldMyPetHp) {
            hasPetInfo = true;
            Field292 = oldMyPetHp;
            myPetHp = hp;
            if (myPet != null) {
                myPet.addEff(String.valueOf(hp - oldMyPetHp), 0, System.currentTimeMillis() + 100);
            }
        }
        if (mp != oldMyPetMp) {
            hasPetInfo = true;
            Field293 = oldMyPetMp;
            myPetMp = mp;
            if (myPet != null) {
                myPet.addEff(String.valueOf(mp - oldMyPetMp), 1, System.currentTimeMillis() + 600);
            }
        }
    }

    private static void setMoney(long gold, long coin, long lua) {
        mGold = gold;
        mGoldStr = Ulti.formatNumber(gold);
        mCoin = coin;
        mCoinStr = Ulti.formatNumber(coin);
        mLua = lua;
        mLuaStr = Ulti.formatNumber(lua);
    }

    public static void Method461(String str, int i, int i2, int i3) {
        String str2 = null;
        switch (i) {
            case 0:
            case 3:
                str2 = "(str) ";
                break;
            case 1:
            case 4:
                str2 = "(agi) ";
                break;
            case 2:
            case 5:
                str2 = "(int) ";
                break;
        }
        String text = (str2 + str).trim();
        GameResourceManager.Method116().drawString(BaseCanvas.g, text, i2, 22, 17);
    }

    public static Animation readAnimation(Message msg) throws IOException {
        Animation animation = new Animation(msg.reader().readByte(), msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readByte());
        return animation;
    }

    public static Animation[] readAnimations(Message msg) throws IOException {
        int count = msg.reader().readInt();
        Animation[] animations = new Animation[count];
        for (int i = 0; i < count; i++) {
            animations[i] = readAnimation(msg);
        }
        return animations;
    }

    public final void onMssage(Message msg) {
        IOException printStackTrace;
        try {
            switch (msg.id) {
                case Cmd.PET_SERVICE:
                    switch (msg.reader().readByte()) {
                        case Cmd.PET_UNFOLLOW: {
                            int charId = msg.reader().readInt();
                            int petId = msg.reader().readInt();
                            mCharacter obj = SceneManage.currentScene.listChar.getChar(charId);
                            if (obj != null) {
                                ((MyCharacter) obj).unfollowPet(petId);
                                return;
                            }
                        }
                        return;
                        case Cmd.SEND_ANIMATION_CHARACTER: {
                            int charId = msg.reader().readInt();
                            mCharacter obj = SceneManage.currentScene.listChar.getChar(charId);
                            if (obj != null) {
                                ((MyCharacter) obj).setAnimation(readAnimations(msg));
                                return;
                            }
                        }
                        break;
                        case Cmd.SEND_LIST_ANIMATION_CHARACTER: {
                            while (msg.reader().available() > 0) {
                                int charId = msg.reader().readInt();
                                mCharacter obj = SceneManage.currentScene.listChar.getChar(charId);
                                if (obj != null) {
                                    ((MyCharacter) obj).setAnimation(readAnimations(msg));
                                    return;
                                }
                            }
                        }
                        break;
                        case 10: {
                            ChatPlace.setGlobalChat(msg.reader().readUTF(), msg.reader().readUTF());
                        }
                        break;
                        case 12: {
                            break;
                        }
                        case 13:
                        case 14:
                        case 15:
                        case 20:
                        case 23:
                        case 24:
                        case 27:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 38:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 51:
                        case 52:
                        case 54:
                        case 55:
                        case 58:
                        case 60:
                        case 62:
                        case 68:
                        case 77:
                        case 78:

                        default:
                            return;
                        case 89: {
                            int mobId = msg.reader().readInt();
                            int hp = msg.reader().readInt();
                            if (SceneManage.currentScene instanceof PetGameScreen) {
                                PetGameScreen gameScreen = (PetGameScreen) (SceneManage.currentScene);
                                Enumeration elements = gameScreen.listBattle.elements();
                                while (elements.hasMoreElements()) {
                                    PetBattle battle = (PetBattle) elements.nextElement();
                                    if (battle.doithu != null) {
                                        if (battle.doithu.mobId == mobId) {
                                            battle.obj[1].petInfo.hpBoss = hp;
                                            battle.obj[1].petInfo.isBoss = true;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                        case 96: {
                            int mobId = msg.reader().readInt();
                            if (SceneManage.currentScene instanceof PetGameScreen) {
                                PetGameScreen gameScreen = (PetGameScreen) (SceneManage.currentScene);
                                Enumeration elements = gameScreen.listBattle.elements();
                                while (elements.hasMoreElements()) {
                                    PetBattle battle = (PetBattle) elements.nextElement();
                                    if (battle.doithu != null) {
                                        if (battle.doithu.mobId == mobId) {
                                            gameScreen.listBattle.remove(new Integer(battle.objId[0]));
                                            for (int i = 0; i < battle.obj.length; i++) {
                                                SceneManage.currentScene.removeGameObject(battle.obj[i]);
                                            }
                                            MyCharacter myPet = (MyCharacter) SceneManage.currentScene.listChar.getChar(battle.objId[0]);
                                            if (myPet != null && myPet.ownerPet != null) {
                                                myPet.ownerPet.Field864 = true;
                                            }
                                            if (battle.objId[0] == SceneManage.myCharacter.Id) {
                                                SceneManage.currentScene.canMove = true;
                                                PetGameModel.Field296 = false;
                                                gameScreen.Method6();
                                            }
                                            SceneManage.currentScene.removeGameObject(battle.doithu);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                        case Cmd.FAST_REMOVE_MOB: {
                            int battleId = msg.reader().readInt();
                            PetGameScreen class493 = (PetGameScreen) SceneManage.currentScene;
                            PetBattle battle = class493.Method451(battleId);
                            if (battle != null) {
                                battle.fastRemoveMob();
                                return;
                            }
                        }
                        break;
                        case 8:
                            int readByte = msg.reader().readByte();
                            for (int i = 0; i < readByte; i++) {
                                int readInt3 = msg.reader().readInt();
                                int readInt4 = msg.reader().readInt();
                                String readUTF = msg.reader().readUTF();
                                String readUTF2 = msg.reader().readUTF();
                                int readInt5 = msg.reader().readInt();
                                mCharacter Method1542 = SceneManage.currentScene.listChar.getChar(readInt3);
                                if (Method1542 == null) {
                                    return;
                                }
                                MyCharacter class42 = (MyCharacter) Method1542;
                                Pet myPet1 = new Pet(class42, readInt4, readUTF, msg.reader().readByte(), msg.reader().readShort());
                                myPet1.petLvl = readInt5;
                                myPet1.name = readUTF2;
                                myPet1.setPetEffect(readPetEffAnimation(msg));
                                class42.setPet(myPet1);
                                if (readInt3 == SceneManage.myCharacter.Id) {
                                    if (this.myPet != null) {
                                        int i2 = this.myPet.petTemplateId;
                                        int i3 = myPet1.petTemplateId;
                                    }
                                    this.myPet = myPet1;
                                }
                            }
                            return;
                        case 9:
                            byte readByte2 = msg.reader().readByte();
                            String readUTF3 = msg.reader().readUTF();
                            byte[] Method214 = GlobalMessageHandler.Method214(msg);
                            if (Method214 != null) {
                                switch (readByte2) {
                                    case 1:
                                        Field284.Method454(readUTF3, Method214);
                                        return;
                                    case 2:
                                        Field284.Method458(readUTF3, Method214);
                                        break;
                                }
                                return;
                            }
                            return;
                        case 11:
                            int readInt6 = msg.reader().readInt();
                            PetInfo petInfo = new PetInfo();
                            petInfo.petId = msg.reader().readInt();
                            petInfo.element = msg.reader().readByte();
                            petInfo.petImgPath = msg.reader().readUTF();
                            petInfo.petName = msg.reader().readUTF();
                            petInfo.nClass = msg.reader().readByte();
                            petInfo.petGymLvl = msg.reader().readInt();
                            petInfo.currentExp = msg.reader().readLong();
                            petInfo.maxExp = msg.reader().readLong();
                            petInfo.subExp = msg.reader().readLong();
                            petInfo.str = msg.reader().readInt();
                            petInfo.agi = msg.reader().readInt();
                            petInfo._int = msg.reader().readInt();
                            petInfo.atk = msg.reader().readInt();
                            petInfo.def = msg.reader().readInt();
                            petInfo.hp = msg.reader().readInt();
                            petInfo.mp = msg.reader().readInt();
                            petInfo.maxHp = msg.reader().readInt();
                            petInfo.maxMp = msg.reader().readInt();
                            int readByte3 = msg.reader().readByte();
                            petInfo.skillName = new String[readByte3];
                            petInfo.skillDesc = new String[readByte3];
                            petInfo.skillId = new int[readByte3];
                            petInfo.mpLost = new int[readByte3];
                            for (int i4 = 0; i4 < readByte3; i4++) {
                                petInfo.skillId[i4] = msg.reader().readInt();
                                petInfo.skillName[i4] = msg.reader().readUTF();
                                petInfo.skillDesc[i4] = msg.reader().readUTF();
                                petInfo.mpLost[i4] = msg.reader().readInt();
                            }
                            petInfo.tiemnang = msg.reader().readInt();
                            petInfo.Field395 = new String[msg.reader().readInt()];
                            for (int i5 = 0; i5 < petInfo.Field395.length; i5++) {
                                msg.reader().readInt();
                                petInfo.Field395[i5] = msg.reader().readUTF();
                                msg.reader().readByte();
                                msg.reader().readUTF();
                            }
                            petInfo.frameNum = msg.reader().readByte();
                            if (Field285 == 0) {
                                MgScreen mg = new MgScreen(this, readInt6 == SceneManage.myCharacter.Id);
                                mg.setInfo(petInfo);
                                mg.switchToMe(1, true);
                                return;
                            } else if (BaseCanvas.currentScreen instanceof MenuScreen) {
                                ((MenuScreen) BaseCanvas.currentScreen).Method297(null);
                                ((TrScreen) BaseCanvas.currentScreen).setInfo(petInfo);
                                return;
                            } else {
                                TrScreen trScr = new TrScreen(this, 1);
                                trScr.setInfo(petInfo);
                                trScr.switchToMe(2, true);
                                return;
                            }
                        case 16:
                            PetGameScreen class49 = (PetGameScreen) SceneManage.currentScene;
                            int readInt7 = msg.reader().readInt();
                            PetBattle Method451 = class49.Method451(readInt7);
                            if (Method451 != null) {
                                Method451.onLostMassage(msg, readInt7 == SceneManage.myCharacter.Id);
                                return;
                            }
                            return;
                        case 17:
                            ((PetGameScreen) SceneManage.currentScene).Method92(msg);
                            return;
                        case 18:
                            msg.reader().readInt();
                            msg.reader().readInt();
                            int lvl = msg.reader().readInt();
                            StarAnimation starAni = new StarAnimation();
                            starAni.start();
                            Screen.animationEffects.addElement(starAni);
                            this.myPet.petLvl++;
                            ISoundManagerSDK.playSoundEffect("s_pet_level_up");
                            GameController.startOKDlg(new StringBuffer(T.gL(T.PET_LEVEL_UP_MESSGAE)).append(lvl).toString());
                            return;
                        case 19:
                            if (BaseCanvas.currentScreen instanceof TrScreen) {
                                ((TrScreen) BaseCanvas.currentScreen).onMessage(msg);
                                return;
                            }
                            return;
                        case 21:
                            PetInfo petInfo1 = new PetInfo();
                            petInfo1.petId = msg.reader().readInt();
                            petInfo1.petImgPath = msg.reader().readUTF();
                            petInfo1.petName = msg.reader().readUTF();
                            petInfo1.nClass = msg.reader().readByte();
                            petInfo1.petGymLvl = msg.reader().readInt();
                            petInfo1.currentExp = msg.reader().readLong();
                            petInfo1.maxExp = msg.reader().readLong();
                            petInfo1.subExp = msg.reader().readLong();
                            petInfo1.str = msg.reader().readInt();
                            petInfo1.agi = msg.reader().readInt();
                            petInfo1._int = msg.reader().readInt();
                            petInfo1.tiemnang = msg.reader().readByte();
                            int[] iArr = new int[3];
                            int[] iArr2 = new int[3];
                            byte[] bArr = new byte[3];
                            String[] strArr = new String[3];
                            byte[] bArr2 = new byte[3];
                            for (int i6 = 0; i6 < 3; i6++) {
                                iArr[i6] = msg.reader().readInt();
                                iArr2[i6] = msg.reader().readInt();
                                bArr[i6] = msg.reader().readByte();
                                strArr[i6] = msg.reader().readUTF();
                                bArr2[i6] = msg.reader().readByte();
                            }
                            petInfo1.frameNum = msg.reader().readByte();
                            TrScreen gameSrc1 = new TrScreen(this, 0);
                            gameSrc1.setTrainData(petInfo1, iArr, iArr2, bArr, strArr, bArr2);
                            gameSrc1.switchToMe(1, true);
                            return;
                        case 22: {
                            int mobCount = msg.reader().readInt();
                            for (int i7 = 0; i7 < mobCount; i7++) {
                                Mob mob = new Mob(
                                        msg.reader().readInt(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readInt(),
                                        msg.reader().readInt(),
                                        msg.reader().readInt(),
                                        msg.reader().readByte(),
                                        msg.reader().readByte(),
                                        msg.reader().readShort(),
                                        msg.reader().readBoolean());
                                ((PetGameScreen) SceneManage.currentScene).Field331.addElement(mob);
                                SceneManage.currentScene.addObject(mob);
                                SceneManage.currentScene.Method200(mob);
                                mob.centerObjectCMD = new Command(0, T.gL(T.FIGHT), mob, this);
                            }
                            break;
                        }
                        case 41: {
                            int mobCount = msg.reader().readInt();
                            for (int i7 = 0; i7 < mobCount; i7++) {
                                Mob mob = new Mob(
                                        msg.reader().readInt(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readInt(),
                                        msg.reader().readInt(),
                                        msg.reader().readInt(),
                                        msg.reader().readByte(),
                                        msg.reader().readByte(),
                                        msg.reader().readShort(),
                                        false);
                                ((PetGameScreen) SceneManage.currentScene).Field331.addElement(mob);
                                SceneManage.currentScene.addObject(mob);
                                SceneManage.currentScene.Method200(mob);
                                mob.centerObjectCMD = new Command(0, T.gL(T.FIGHT), mob, this);
                            }
                        }
                        return;
                        case Cmd.MONEY_INFO:
                            msg.reader().readInt();
                            long gold = msg.reader().readLong();
                            long coin = msg.reader().readLong();
                            long lua = msg.reader().readLong();
                            if (this.Field271 && !Field296) {
                                Class23 class23 = new Class23();
                                int i8 = BaseCanvas.Field157;
                                int i9 = BaseCanvas.Field158;
                                if (SceneManage.myCharacter != null && SceneManage.currentScene != null) {
                                    i8 = SceneManage.myCharacter.xChar - SceneManage.currentScene.Field888.Field57;
                                    i9 = ((SceneManage.myCharacter.yChar - SceneManage.currentScene.Field889.Field57) - 54) - 2;
                                }
                                class23.Method333(this, i8, i9, gold - mGold, coin - mCoin);
                                Screen.animationEffects.addElement(class23);
                            }
                            setMoney(gold, coin, lua);
                            if (msg.reader().available() > 0) {
                                MoneyDisplay[] moneyDisplays = new MoneyDisplay[msg.reader().readInt()];
                                for (int i = 0; i < moneyDisplays.length; i++) {
                                    moneyDisplays[i] = new MoneyDisplay(msg.reader().readUTF(), msg.reader().readLong());
                                }
                                PetGameScreen.moneyDisplays = moneyDisplays;
                            }
                            this.Field271 = true;
                            return;
                        case 26:
                            return;
                        case 28:
                            int readInt10 = msg.reader().readInt();
                            PetInfo petInfo2 = new PetInfo();
                            petInfo2.petId = msg.reader().readInt();
                            petInfo2.petImgPath = msg.reader().readUTF();
                            petInfo2.petName = msg.reader().readUTF();
                            petInfo2.petGymLvl = msg.reader().readInt();
                            petInfo2.str = msg.reader().readInt();
                            petInfo2.agi = msg.reader().readInt();
                            petInfo2._int = msg.reader().readInt();
                            int readInt11 = msg.reader().readInt();
                            PetItem[] petItems1 = new PetItem[readInt11];
                            for (int i10 = 0; i10 < readInt11; i10++) {
                                petItems1[i10] = new PetItem();
                                petItems1[i10].itemId = msg.reader().readInt();
                                petItems1[i10].imgPathId = msg.reader().readUTF();
                                petItems1[i10].Field668 = msg.reader().readUTF();
                                petItems1[i10].name = msg.reader().readUTF();
                                petItems1[i10].type = msg.reader().readInt();
                                petItems1[i10].petEquipId = msg.reader().readInt();
                                petItems1[i10].Field671 = msg.reader().readInt();
                                petItems1[i10].Field672 = msg.reader().readInt();
                                petItems1[i10].Field673 = msg.reader().readInt();
                                petItems1[i10].Field674 = msg.reader().readInt();
                                petItems1[i10].Field675 = msg.reader().readInt();
                                petItems1[i10].Field676 = msg.reader().readInt();
                                petItems1[i10].Field677 = msg.reader().readInt();
                                petItems1[i10].Field678 = msg.reader().readInt();
                                petItems1[i10].Field679 = msg.reader().readInt();
                                petItems1[i10].Field680 = msg.reader().readInt();
                                petItems1[i10].Field681 = msg.reader().readInt();
                                petItems1[i10].Field683 = msg.reader().readByte();
                                petItems1[i10].Method22(msg.reader().readByte());
                                if (msg.reader().readBoolean()) {
                                    long readLong3 = msg.reader().readLong();
                                    petItems1[i10].Field688 = msg.reader().readInt();
                                    if (readLong3 > 0) {
                                        petItems1[i10].typeGem = (byte) 2;
                                        petItems1[i10].currentTime = System.currentTimeMillis();
                                    } else {
                                        petItems1[i10].typeGem = (byte) 1;
                                    }
                                    System.out.println(petItems1[i10].Field688);
                                } else {
                                    petItems1[i10].typeGem = (byte) 0;
                                }
                            }
                            petInfo2.frameNum = msg.reader().readByte();
                            PetInfoScreen class109 = new PetInfoScreen(this, readInt10 == SceneManage.myCharacter.Id);
                            class109.setItem1(petInfo2, petItems1);
                            class109.switchToMe(2, true);
                            return;
                        case 29:
                            if (msg.reader().readByte() == 1 && (BaseCanvas.currentScreen instanceof PetInfoScreen)) {
                                ((PetInfoScreen) BaseCanvas.currentScreen).setUse(msg.reader().readInt());
                                return;
                            }
                            return;
                        case 36: {
                            int readInt12 = msg.reader().readInt();
                            int readInt13 = msg.reader().readInt();
                            int userId = msg.reader().readInt();
                            if (userId == SceneManage.myCharacter.Id) {
                                Screen.Method316(1);
                            }
                            PetInfo Method465 = readPet1(msg);
                            int mobId = msg.reader().readInt();
                            PetInfo Method464 = readPet2(msg);
                            PetBattle battle = new PetBattle();
                            battle.doithu = ((PetGameScreen) SceneManage.currentScene).findMob(Method464.petId);
                            battle.objId[0] = userId;
                            battle.objId[1] = mobId;
                            Mob mob = battle.doithu;
                            if (!mob.isBoss) {
                                SceneManage.currentScene.removeGameObject(battle.doithu);
                            }
                            mSkillPaint class34 = new mSkillPaint(battle, battle.doithu.xChar, battle.doithu.yChar, Method464, 2);
                            class34.isPlayer = false;
                            SceneManage.currentScene.addObject(class34);
                            SceneManage.currentScene.Method200(class34);
                            mSkillPaint mPlayerSkillpaint = new mSkillPaint(battle, battle.doithu.xChar + 40, battle.doithu.yChar, Method465, 0);
                            mPlayerSkillpaint.isPlayer = true;
                            SceneManage.currentScene.addObject(mPlayerSkillpaint);
                            SceneManage.currentScene.Method200(mPlayerSkillpaint);
                            MyCharacter curChar = (MyCharacter) SceneManage.currentScene.listChar.getChar(userId);
                            battle.xPos[0] = curChar.xChar;
                            battle.yPos[0] = curChar.yChar;
                            curChar.xChar = battle.doithu.xChar + 20;
                            curChar.yChar = battle.doithu.yChar - 20;
                            curChar.ownerPet.Field864 = false;
                            curChar.ownerPet.xChar = battle.doithu.xChar + 40;
                            curChar.ownerPet.yChar = battle.doithu.yChar;
                            battle.obj[0] = mPlayerSkillpaint;
                            battle.obj[1] = class34;
                            battle.setTurn(readInt12, readInt13);
                            PetGameScreen gameSrc = (PetGameScreen) SceneManage.currentScene;
                            gameSrc.listBattle.put(new Integer(userId), battle);
                            if (SceneManage.myCharacter.Id == userId) {
                                gameSrc.Method202();
                                gameSrc.showAttackUI();
                                SceneManage.currentScene.canMove = false;
                                Field296 = true;
                                AutoAttack.instance.skills_coolDown = new int[mPlayerSkillpaint.petInfo.skillId.length];
                                AutoAttack.instance.skills_Id = new int[mPlayerSkillpaint.petInfo.skillId.length];
                                for (int i = 0; i < mPlayerSkillpaint.petInfo.skillId.length; i++) {
                                    AutoAttack.instance.skills_coolDown[i] = 0;
                                    AutoAttack.instance.skills_Id[i] = mPlayerSkillpaint.petInfo.skillId[i];
                                }
                            }
                            return;
                        }
                        case 37:
                            PetGameScreen class493 = (PetGameScreen) SceneManage.currentScene;
                            class493.hasAttackType = -1;
                            class493.Field340 = -1;
                            PetBattle Method4512 = class493.Method451(msg.reader().readInt());
                            if (Method4512 != null) {
                                Method4512.onMessage(msg);
                                return;
                            }
                            return;
                        case 39:
                            if (msg.reader().readByte() == 1 && (BaseCanvas.currentScreen instanceof PetInfoScreen)) {
                                ((PetInfoScreen) BaseCanvas.currentScreen).Method104(msg.reader().readInt());
                                return;
                            }
                            return;
                        case 40:
                            setPetInfo(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readInt());
                            return;
                        case 42:
                            int readInt16 = msg.reader().readInt();
                            Vector vector = ((PetGameScreen) SceneManage.currentScene).Field331;
                            for (int i11 = 0; i11 < vector.size(); i11++) {
                                Mob class412 = (Mob) vector.elementAt(i11);
                                if (class412.mobId == readInt16) {
                                    vector.removeElement(class412);
                                    SceneManage.currentScene.removeGameObject(class412);
                                    return;
                                }
                            }
                            return;
                        case 47:
                            Screen Screen = BaseCanvas.currentScreen;
                            if (Screen instanceof PetInfoScreen) {
                                ((PetInfoScreen) Screen).Method92(msg);
                                return;
                            }
                            return;
                        case 48:
                        case 49:
                            Screen Screen2 = BaseCanvas.currentScreen;
                            if (Screen2 instanceof PetInfoScreen) {
                                ((PetInfoScreen) Screen2).Method93(msg);
                                return;
                            }
                            return;
                        case 50:
                            int readInt17 = msg.reader().readInt();
                            int readInt18 = msg.reader().readInt();
                            String readUTF4 = msg.reader().readUTF();
                            String readUTF5 = msg.reader().readUTF();
                            int readInt19 = msg.reader().readInt();
                            Screen Screen3 = BaseCanvas.currentScreen;
                            if (Screen3 instanceof MgScreen) {
                                ((MgScreen) Screen3).Method139(readInt17, readInt18, readUTF4, readUTF5, readInt19);
                                return;
                            }
                            return;
                        case 53:
                            String readUTF6 = msg.reader().readUTF();
                            if (SceneManage.myCharacter != null) {
                                MyCharacter class423 = (MyCharacter) SceneManage.myCharacter;
                                if (class423.ownerPet != null) {
                                    class423.ownerPet.addEff(readUTF6, 3, System.currentTimeMillis() + 1000);
                                    return;
                                }
                                return;
                            }
                            return;
                        case 56:
                            int readInt20 = msg.reader().readInt();
                            Screen Screen4 = BaseCanvas.currentScreen;
                            if (Screen4 instanceof PetInfoScreen) {
                                ((PetInfoScreen) Screen4).Method146(readInt20);
                                return;
                            }
                            return;
                        case 57:
                            PetGameScreen Method463 = Method463();
                            if (Method463 != null) {
                                int readInt21 = msg.reader().readInt();
                                for (int i12 = 0; i12 < readInt21; i12++) {
                                    Method463.listChar.getChar(msg.reader().readInt()).Field818 = msg.reader().readInt();
                                }
                                return;
                            }
                            return;
                        case 59:
                            int readInt22 = msg.reader().readInt();
                            int readInt23 = msg.reader().readInt();
                            int readInt24 = msg.reader().readInt();
                            byte readByte4 = msg.reader().readByte();
                            PetInfo Method4652 = readPet1(msg);
                            int readInt25 = msg.reader().readInt();
                            PetInfo Method4642 = readPet2(msg);
                            PetBattle class322 = new PetBattle();
                            class322.Field185 = true;
                            msg.reader().readBoolean();
                            class322.objId[0] = readInt24;
                            class322.objId[1] = readInt25;
                            MyCharacter class424 = (MyCharacter) SceneManage.currentScene.listChar.getChar(readInt24);
                            class322.xPos[0] = class424.xChar;
                            class322.yPos[0] = class424.yChar;
                            MyCharacter class425 = (MyCharacter) SceneManage.currentScene.listChar.getChar(readInt25);
                            class322.xPos[1] = class425.xChar;
                            class322.yPos[1] = class425.yChar;
                            int i13 = (class424.xChar + class425.xChar) >> 1;
                            int i14 = (class424.yChar + class425.yChar) >> 1;
                            int i15 = i13 - 30;
                            int i16 = i13 + 30;
                            mSkillPaint class343 = new mSkillPaint(class322, readByte4 == 0 ? i16 : i15, i14, Method4642, readByte4 == 0 ? 0 : 2);
                            class343.isPlayer = false;
                            SceneManage.currentScene.addObject(class343);
                            SceneManage.currentScene.Method200(class343);
                            mSkillPaint class344 = new mSkillPaint(class322, readByte4 == 0 ? i15 : i16, i14, Method4652, readByte4 == 1 ? 0 : 2);
                            class344.isPlayer = true;
                            SceneManage.currentScene.addObject(class344);
                            SceneManage.currentScene.Method200(class344);
                            class424.xChar = readByte4 == 0 ? i15 : i16;
                            class424.yChar = i14 - 20;
                            class424.ownerPet.Field864 = false;
                            class424.ownerPet.xChar = readByte4 == 0 ? i15 : i16;
                            class424.ownerPet.yChar = i14;
                            class424.Field807 = readByte4 == 0 ? 1 : 0;
                            class425.xChar = readByte4 == 0 ? i16 : i15;
                            class425.yChar = i14 - 20;
                            class425.ownerPet.Field864 = false;
                            class425.ownerPet.xChar = readByte4 == 0 ? i16 : i15;
                            class425.ownerPet.yChar = i14;
                            class425.Field807 = readByte4 == 0 ? 0 : 1;
                            class322.obj[0] = class344;
                            class322.obj[1] = class343;
                            class322.setTurn(readInt22, readInt23);
                            PetGameScreen class494 = (PetGameScreen) SceneManage.currentScene;
                            class494.listBattle.put(new Integer(readInt24), class322);
                            if (SceneManage.myCharacter.Id == readInt24) {
                                class494.Method202();
                                class494.showAttackUI();
                                SceneManage.currentScene.canMove = false;
                                Field296 = true;
                            }
                            return;
                        case 61:
                            PetGameScreen Method4632 = Method463();
                            if (Method4632 != null) {
                                int readInt26 = msg.reader().readInt();
                                for (int i17 = 0; i17 < readInt26; i17++) {
                                    Method4632.listChar.getChar(msg.reader().readInt()).Method158(msg.reader().readUTF());
                                }
                                return;
                            }
                            return;
                        case 63:
                            BigTextEffect eff = new BigTextEffect(msg.reader().readUTF());
                            eff.start();
                            GameScene.animationEffects.addElement(eff);
                            return;
                        case 64:
                            int readInt27 = msg.reader().readInt();
                            if (SceneManage.currentScene instanceof PetGameScreen) {
                                ((PetGameScreen) SceneManage.currentScene).Method79(readInt27);
                                return;
                            }
                            return;
                        case 65:
                            vn.me.screen.Screen.animationEffects.removeAllElements();
                            Class17 class17 = new Class17();
                            int readInt28 = msg.reader().readInt();
                            int readInt29 = msg.reader().readInt();
                            class17.Method271(readInt28, readInt29, msg.reader().readInt(), msg.reader().readInt());
                            for (int i18 = 0; i18 < 9; i18++) {
                                byte[] bArr3 = new byte[msg.reader().readInt()];
                                msg.reader().read(bArr3);
                                class17.Field60[i18] = Image.createImage(bArr3, 0, bArr3.length);
                                if (class17.Field60[i18] == null) {
                                    class17.Method50();
                                    if (readInt29 != -1) {
                                        class17.switchToMe(0, true);
                                        return;
                                    } else {
                                        class17.switchToMe(0);
                                        return;
                                    }
                                }
                            }
                            class17.Method50();
                            if (readInt29 != -1) {
                                class17.switchToMe(0, true);
                                return;
                            }
                        case 66:
                            if (ChatPlace.Field743) {
                                int readByte5 = msg.reader().readByte();
                                for (int i19 = 0; i19 < readByte5; i19++) {
                                    ChatPlace.setNormalChat(msg.reader().readUTF(), msg.reader().readUTF());
                                }
                                return;
                            }
                            return;
                        case 67:
                            new PetUprgadeScreen().switchToMe(1, true);
                            return;
                        case 69:
                            if (BaseCanvas.currentScreen instanceof PetUprgadeScreen) {
                                byte readByte6 = msg.reader().readByte();
                                int readInt30 = msg.reader().readInt();
                                String readUTF7 = msg.reader().readUTF();
                                int readByte7 = msg.reader().readByte();
                                String[] strArr2 = new String[readByte7];
                                for (int i20 = 0; i20 < readByte7; i20++) {
                                    strArr2[i20] = msg.reader().readUTF();
                                }
                                ((PetUprgadeScreen) BaseCanvas.currentScreen).Method23(readByte6, readInt30, readUTF7, strArr2);
                                return;
                            }
                            return;
                        case 70:
                            Dialog.Method25();
                            if (BaseCanvas.currentScreen instanceof PetUprgadeScreen) {
                                String readUTF8 = msg.reader().readUTF();
                                int readByte8 = msg.reader().readByte();
                                String[] strArr3 = new String[readByte8];
                                for (int i21 = 0; i21 < readByte8; i21++) {
                                    strArr3[i21] = msg.reader().readUTF();
                                }
                                ((PetUprgadeScreen) BaseCanvas.currentScreen).Method23(3, 0, readUTF8, strArr3);
                                return;
                            }
                            return;
                        case 71:
                            if (BaseCanvas.currentScreen instanceof PetUprgadeScreen) {
                                ((PetUprgadeScreen) BaseCanvas.currentScreen).Method6();
                                return;
                            }
                            return;
                        case 72:
                            if (BaseCanvas.currentScreen instanceof PetUprgadeScreen) {
                                ((PetUprgadeScreen) BaseCanvas.currentScreen).Method28(msg.reader().readInt(), msg.reader().readInt());
                                return;
                            }
                            return;
                        case 73:
                            Screen Screen5 = BaseCanvas.currentScreen;
                            if (Screen5 instanceof PetInfoScreen) {
                                ((PetInfoScreen) Screen5).Method94(msg);
                                return;
                            }
                            return;
                        case 74:
                            int readInt31 = msg.reader().readInt();
                            PetItem[] class104Arr2 = new PetItem[readInt31];
                            for (int i22 = 0; i22 < readInt31; i22++) {
                                class104Arr2[i22] = new PetItem();
                                class104Arr2[i22].itemId = msg.reader().readInt();
                                class104Arr2[i22].imgPathId = String.valueOf(msg.reader().readInt());
                                class104Arr2[i22].name = msg.reader().readUTF();
                                class104Arr2[i22].Method22((byte) msg.reader().readInt());
                            }
                            GemScreen class83 = new GemScreen();
                            class83.Method91(class104Arr2);
                            class83.switchToMe(1, true);
                            return;
                        case 75:
                            Screen Screen6 = BaseCanvas.currentScreen;
                            if (Screen6 instanceof PetInfoScreen) {
                                ((PetInfoScreen) Screen6).Method95(msg);
                                return;
                            }
                            return;
                        case 76:
                        case 79:
                            Screen Screen7 = BaseCanvas.currentScreen;
                            if (Screen7 instanceof GemScreen) {
                                ((GemScreen) Screen7).Method93(msg);
                                return;
                            }
                            return;
                        case 80:
                        case 81:
                            Screen Screen8 = BaseCanvas.currentScreen;
                            if (Screen8 instanceof GemScreen) {
                                ((GemScreen) Screen8).Method92(msg);
                                return;
                            }
                            return;
                        case 82:
                            Screen Screen9 = BaseCanvas.currentScreen;
                            if (Screen9 instanceof PetInfoScreen) {
                                ((PetInfoScreen) Screen9).Method148(msg);
                                break;
                            }
                            break;
                        case 83:
                            break;
                        case 84:
                            Screen Screen10 = BaseCanvas.currentScreen;
                            if (Screen10 instanceof GemScreen) {
                                ((GemScreen) Screen10).Method95(msg);
                                return;
                            }
                            return;
                        case 86:
                            KioskScreen screen = null;
                            Screen getCurrentScreen = BaseCanvas.getCurrentScreen();
                            getCurrentScreen.Method309();
                            if (getCurrentScreen instanceof KioskScreen) {
                                screen = (KioskScreen) getCurrentScreen;
                            } else {
                                int i23 = 0;
                                while (i23 < getCurrentScreen.listScreen.size()) {
                                    Screen Screen11 = (Screen) getCurrentScreen.listScreen.elementAt(i23);
                                    if (Screen11 instanceof KioskScreen) {
                                        screen = (KioskScreen) Screen11;
                                    } else {
                                        i23++;
                                    }
                                }
                            }
                            boolean z = false;
                            if (screen == null) {
                                screen = new KioskScreen();
                                z = true;
                            }
                            screen.Method22(msg.reader().readByte());
                            if (msg.reader().readInt() == 0) {
                                screen.Method6();
                            } else {
                                screen.setItem(msg.reader().readInt(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readInt(), msg.reader().readByte());
                            }
                            if (z) {
                                screen.switchToMe(1, true);
                                return;
                            }
                            return;
                        case 88:
                            int readInt32 = msg.reader().readInt();
                            int readInt33 = msg.reader().readInt();
                            Vector vector2 = new Vector();
                            for (int i24 = 0; i24 < readInt32; i24++) {
                                String readUTF9 = msg.reader().readUTF();
                                int readByte9 = msg.reader().readByte();
                                String[] strArr4 = new String[readByte9];
                                for (int i25 = 0; i25 < readByte9; i25++) {
                                    strArr4[i25] = msg.reader().readUTF();
                                }
                                vector2.addElement(new Class142(readUTF9, strArr4));
                            }
                            new ArenaMenuScreen(vector2, readInt33).switchToMe(2, true);
                            return;
                        case 90:
                            switch (msg.reader().readByte()) {
                                case 1:
                                    int readInt34 = msg.reader().readInt();
                                    Tattoo[] tattoos = new Tattoo[readInt34];
                                    for (int i26 = 0; i26 < readInt34; i26++) {
                                        Tattoo tattoo = new Tattoo();
                                        tattoo.id = msg.reader().readInt();
                                        tattoo.name = msg.reader().readUTF();
                                        tattoo.Method22(msg.reader().readByte());
                                        tattoo.imgPathId = msg.reader().readUTF();
                                        tattoos[i26] = tattoo;
                                    }
                                    Screen Screen12 = BaseCanvas.currentScreen;
                                    if (Screen12 instanceof PetTattooScreen) {
                                        ((PetTattooScreen) Screen12).Method170(tattoos);
                                        return;
                                    }
                                    PetTattooScreen class121 = new PetTattooScreen(this);
                                    class121.Method170(tattoos);
                                    class121.switchToMe(2, true);
                                    return;
                                case 7:
                                    int readInt35 = msg.reader().readInt();
                                    String readUTF10 = msg.reader().readUTF();
                                    String readUTF11 = msg.reader().readUTF();
                                    Screen Screen13 = BaseCanvas.currentScreen;
                                    if (Screen13 instanceof PetTattooScreen) {
                                        ((PetTattooScreen) Screen13).Method171(readInt35, readUTF10, readUTF11);
                                        break;
                                    }
                                    break;
                            }
                            return;
                        case 91:
                            clan(msg.reader().readByte(), msg);
                            return;
                        case 92:
                            switch (msg.reader().readByte()) {
                                case 3:
                                    PetGameScreen Method4633 = Method463();
                                    if (Method4633 != null) {
                                        int readInt36 = msg.reader().readInt();
                                        for (int i27 = 0; i27 < readInt36; i27++) {
                                            Method4633.listChar.getChar(msg.reader().readInt()).Method167(msg.reader().readUTF(), msg.reader().readByte());
                                        }
                                        break;
                                    }
                                    break;
                            }
                            return;
                        case 93:
                            msg.reader().readByte();
                            return;
                        case 94:
                            int readInt37 = msg.reader().readInt();
                            Field277 = Ulti.formatNumber(readInt37);
                            Class24 class24 = new Class24();
                            int i28 = BaseCanvas.Field157;
                            int i29 = BaseCanvas.Field158;
                            if (SceneManage.myCharacter != null && SceneManage.currentScene != null) {
                                i28 = SceneManage.myCharacter.xChar - SceneManage.currentScene.Field888.Field57;
                                i29 = ((SceneManage.myCharacter.yChar - SceneManage.currentScene.Field889.Field57) - 54) - 2;
                            }
                            class24.Method338(i28, i29, readInt37 - Field274);
                            vn.me.screen.Screen.animationEffects.addElement(class24);
                            Field274 = readInt37;
                            return;
                        case 95:
                            this.Field300 = msg.reader().readUTF();
                            this.Field298 = msg.reader().readInt();
                            msg.reader().readInt();
                            this.Field297 = msg.reader().readLong();
                            this.Field299 = true;
                            return;
                        case 98:
                            PetGameScreen Method4634 = Method463();
                            if (Method4634 != null) {
                                int readInt38 = msg.reader().readInt();
                                for (int i30 = 0; i30 < readInt38; i30++) {
                                    Method4634.listChar.getChar(msg.reader().readInt()).Method22(msg.reader().readByte());
                                }
                                return;
                            }
                            return;
                        case 100:
                            byte readByte10 = msg.reader().readByte();
                            AnimationMenu Method462 = Method462();
                            if (readByte10 == 0) {
                                int readInt39 = msg.reader().readInt();
                                String readUTF12 = msg.reader().readUTF();
                                boolean z2 = true;
                                if (Method462 == null) {
                                    Method462 = new AnimationMenu(readInt39, readUTF12);
                                    z2 = false;
                                }
                                Method462.Method6();
                                int readInt40 = msg.reader().readInt();
                                for (int i31 = 0; i31 < readInt40; i31++) {
                                    if (msg.reader().readByte() == 1) {
                                        Method462.Method8(msg.reader().readBoolean(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readBoolean(), msg.reader().readInt(), msg.reader().readInt());
                                    } else {
                                        Method462.Method9(msg.reader().readBoolean(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte());
                                    }
                                }
                                int readInt41 = msg.reader().readInt();
                                for (int i32 = 0; i32 < readInt41; i32++) {
                                    Method462.Method10(msg.reader().readInt(), msg.reader().readByte(), msg.reader().readUTF(), msg.reader().readBoolean(), msg.reader().readBoolean());
                                }
                                if (!z2) {
                                    Method462.switchToMe(0, true);
                                }
                                Method462.Method7();
                                return;
                            }
                            return;
                        case 101:
                            SceneManage class140 = GlobalMessageHandler.Method208().sceneManager;
                            SceneManage.Method0();
                            Method18();
                            mMap class137 = new mMap(20, GlobalMessageHandler.Method208().sceneManager, 200, 100);
                            PetGameScreen class495 = new PetGameScreen(GlobalMessageHandler.Method208().sceneManager);
                            class495.cmdRight = null;
                            class495.cmdLeft = null;
                            GlobalMessageHandler.Method208().sceneManager.Method223(class495, class137, GameController.myInfo.mGoMapId, 200, 100);
                            SceneManage.currentScene = class495;
                            class495.switchToMe(0);
                            GameScene.updateMapIndicator();
                            PkBattle class30 = new PkBattle();
                            class30.Field185 = true;
                            PetInfo class584 = new PetInfo();
                            class584.petId = msg.reader().readInt();
                            class584.petImgPath = msg.reader().readUTF();
                            class584.maxHp = msg.reader().readInt();
                            class584.hp = class584.maxHp;
                            class584.maxMp = msg.reader().readInt();
                            class584.mp = class584.maxMp;
                            mSkillPaint class345 = new mSkillPaint(class30, 180, 209, class584, 2);
                            class345.isPlayer = true;
                            SceneManage.currentScene.addObject(class345);
                            SceneManage.currentScene.Method200(class345);
                            PetInfo class585 = new PetInfo();
                            class585.petId = msg.reader().readInt();
                            class585.petImgPath = msg.reader().readUTF();
                            class585.maxHp = msg.reader().readInt();
                            class585.hp = class585.maxHp;
                            class585.maxMp = msg.reader().readInt();
                            class585.mp = class585.maxMp;
                            mSkillPaint class346 = new mSkillPaint(class30, 260, 209, class585, 0);
                            class346.isPlayer = false;
                            SceneManage.currentScene.addObject(class346);
                            SceneManage.currentScene.Method200(class346);
                            class30.obj[0] = class345;
                            class30.obj[1] = class346;
                            ((PetGameScreen) SceneManage.currentScene).listBattle.put(new Integer(0), class30);
                            int readInt42 = msg.reader().readInt();
                            for (int i33 = 0; i33 < readInt42; i33++) {
                                FightResult class37 = new FightResult();
                                class37.petPos = msg.reader().readByte();
                                class37.action = msg.reader().readByte();
                                if (class37.action == 4) {
                                    class37.skillManaCost = msg.reader().readInt();
                                }
                                int readInt43 = msg.reader().readInt();
                                class37.effectId = new int[readInt43];
                                class37.effectTo = new int[readInt43];
                                class37.hpDiff = new int[readInt43];
                                class37.mpDiff = new int[readInt43];
                                for (int i34 = 0; i34 < readInt43; i34++) {
                                    class37.effectTo[i34] = msg.reader().readInt();
                                    System.out.println(class37.effectTo[i34]);
                                    class37.effectId[i34] = msg.reader().readInt();
                                    class37.hpDiff[i34] = msg.reader().readInt();
                                    class37.mpDiff[i34] = msg.reader().readInt();
                                }
                                class30.Method407(class37);
                            }
                            class30.show(msg.reader().readBoolean());
                            final PetGameScreen Method4635 = Method463();
                            Method4635.show(false);
                            Method4635.Method205(false);
                            //Method4635.Field907 = new Class44(this, Method4635);
                            Method4635.Field907 = new IActionListener() {
                                ///////@Override
                                public void actionPerformed(Object obj) {
                                    Method4635.show(false);
                                    Method4635.Method205(false);
                                }
                            };
                            return;
                        case 102:
                            int readInt44 = msg.reader().readInt();
                            int readInt45 = msg.reader().readInt();
                            int readInt46 = msg.reader().readInt();
                            int readInt47 = msg.reader().readInt();
                            PetGameScreen Method4636 = Method463();
                            if (Method4636 != null) {
                                Method4636.Method271(readInt44, readInt45, readInt46, readInt47);
                                return;
                            }
                            return;
                        case 103: {
                            AutoDectectSpeed.TimeSend = System.currentTimeMillis() + msg.reader().readInt();
                            AutoDectectSpeed.isTest = true;
                            break;
                        }
                    }
                    Screen Screen14 = BaseCanvas.currentScreen;
                    if (Screen14 instanceof GemScreen) {
                        ((GemScreen) Screen14).Method94(msg);
                        return;
                    }
                    return;
                default:
                    return;
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private static AnimationMenu Method462() {
        AnimationMenu class61 = null;
        Screen getCurrentScreen = BaseCanvas.getCurrentScreen();
        if (!(getCurrentScreen instanceof AnimationMenu)) {
            int i = 0;
            while (true) {
                if (i >= getCurrentScreen.listScreen.size()) {
                    break;
                }
                Screen Screen = (Screen) getCurrentScreen.listScreen.elementAt(i);
                if (Screen instanceof AnimationMenu) {
                    class61 = (AnimationMenu) Screen;
                    break;
                }
                i++;
            }
        } else {
            class61 = (AnimationMenu) getCurrentScreen;
        }
        return class61;
    }

    private static PetGameScreen Method463() {
        PetGameScreen class49 = null;
        Screen getCurrentScreen = BaseCanvas.getCurrentScreen();
        if (!(getCurrentScreen instanceof PetGameScreen)) {
            int i = 0;
            while (true) {
                if (i >= getCurrentScreen.listScreen.size()) {
                    break;
                }
                Screen Screen = (Screen) getCurrentScreen.listScreen.elementAt(i);
                if (Screen instanceof PetGameScreen) {
                    class49 = (PetGameScreen) Screen;
                    break;
                }
                i++;
            }
        } else {
            class49 = (PetGameScreen) getCurrentScreen;
        }
        return class49;
    }

    private static PetInfo readPet2(Message message) {
        PetInfo class58 = new PetInfo();
        try {
            class58.petId = message.reader().readInt();
            class58.petImgPath = message.reader().readUTF();
            class58.frameNum = message.reader().readByte();
            class58.vY = message.reader().readShort();
            class58.petName = message.reader().readUTF();
            class58.petGymLvl = message.reader().readInt();
            class58.hp = message.reader().readInt();
            class58.mp = message.reader().readInt();
            class58.maxHp = message.reader().readInt();
            class58.maxMp = message.reader().readInt();
            int readByte = message.reader().readByte();
            class58.skillId = new int[readByte];
            class58.skillName = new String[readByte];
            for (int i = 0; i < readByte; i++) {
                class58.skillId[i] = message.reader().readInt();
                class58.skillName[i] = message.reader().readUTF();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return class58;
    }

    private static PetInfo readPet1(Message message) {
        PetInfo class58 = new PetInfo();
        try {
            class58.petId = message.reader().readInt();
            class58.petImgPath = message.reader().readUTF();
            class58.frameNum = message.reader().readByte();
            class58.vY = message.reader().readShort();
            class58.petName = message.reader().readUTF();
            class58.petGymLvl = message.reader().readInt();
            class58.str = message.reader().readInt();
            class58.agi = message.reader().readInt();
            class58._int = message.reader().readInt();
            class58.atk = message.reader().readInt();
            class58.def = message.reader().readInt();
            class58.hp = message.reader().readInt();
            class58.mp = message.reader().readInt();
            class58.maxHp = message.reader().readInt();
            class58.maxMp = message.reader().readInt();
            int readByte = message.reader().readByte();
            class58.skillDesc = new String[readByte];
            class58.skillName = new String[readByte];
            class58.skillId = new int[readByte];
            class58.mpLost = new int[readByte];
            for (int i = 0; i < readByte; i++) {
                class58.skillId[i] = message.reader().readInt();
                class58.skillName[i] = message.reader().readUTF();
                class58.skillDesc[i] = message.reader().readUTF();
                class58.mpLost[i] = message.reader().readInt();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return class58;
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        Object[] list_p = (Object[]) obj;
        Command Command = (Command) list_p[0];
        switch (Command.cmdId) {
            case 0:
                Mob class41 = (Mob) Command.objPerfomed;
                Boolean hasEff = Boolean.TRUE;
                if (list_p.length > 2) {
                    hasEff = (Boolean) list_p[2];
                }
                if (hasEff.equals(Boolean.TRUE)) {
                    AnimationEffect2222 class25 = new AnimationEffect2222(class41.xChar, class41.yChar - 20, true);
                    class25.start();
                    Screen.animationEffects.addElement(class25);
                }
                int i = class41.mobId;
                Message message = new Message(81);
                message.putByte(36);
                message.putInt(i);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            default:
                return;
        }
    }

    public static void Method466(int i, int i2, int i3, int i4, int i5, int i6, int i7) {
        Method467(20, i2, i3, i4, i5, i6, i7, false);
    }

    public static void Method467(int i, int i2, int i3, int i4, int i5, int i6, int i7, boolean z) {
        BaseCanvas.g.translate(i6, i7);
        if (z) {
            GameResourceManager.Method117().drawString(BaseCanvas.g, new StringBuffer().append(Ulti.formatNumber(i2)).append("/").append(Ulti.formatNumber(i3)).toString(), 0, 0, 0);
            BaseCanvas.g.translate(0, 9);
        }
        BaseCanvas.g.setColor(3691038);
        BaseCanvas.g.fillRect(0, 0, i, 4);
        int i8 = (i2 * (i - 2)) / i3;
        BaseCanvas.g.setColor(9830022);
        BaseCanvas.g.fillRect(1, 1, i8, 1);
        BaseCanvas.g.setColor(2021890);
        BaseCanvas.g.fillRect(1, 2, i8, 1);
        BaseCanvas.g.translate(0, 5);
        if (z) {
            GameResourceManager.Method117().drawString(BaseCanvas.g, new StringBuffer().append(Ulti.formatNumber(i4)).append("/").append(Ulti.formatNumber(i5)).toString(), 0, 0, 0);
            BaseCanvas.g.translate(0, 9);
        }
        BaseCanvas.g.setColor(75607);
        BaseCanvas.g.fillRect(0, 0, i, 4);
        int i9 = (i4 * (i - 2)) / i5;
        BaseCanvas.g.setColor(6455253);
        BaseCanvas.g.fillRect(1, 1, i9, 1);
        BaseCanvas.g.setColor(2836897);
        BaseCanvas.g.fillRect(1, 2, i9, 1);
        if (z) {
            BaseCanvas.g.translate(-i6, (-i7) - 23);
        } else {
            BaseCanvas.g.translate(-i6, (-i7) - 5);
        }
    }

    ///////@Override // defpackage.Class29
    public final void Method0() {
        if (hasPetInfo) {
            boolean z = Ulti.Method369(myPetHp - Field292) < 2;
            boolean z2 = z;
            if (z) {
                oldMyPetHp = myPetHp;
                z2 = true;
            } else {
                Field292 += (myPetHp - Field292) >> 1;
            }
            boolean z3 = Ulti.Method369(myPetMp - Field293) < 2;
            boolean z4 = z3;
            if (z3) {
                oldMyPetMp = myPetMp;
                z4 = true;
            } else {
                Field293 += (myPetMp - Field293) >> 1;
            }
            if (z2 && z4) {
                hasPetInfo = false;
            }
        }
    }

    private void clan(int i, Message message) {
        GuildScreen class85;
        GuildScreen class852;
        GuildScreen class853;
        GuildScreen class854;
        GuildScreen class855;
        GuildScreen class856;
        GuildScreen class857;
        try {
            switch (i) {
                case 1:
                    String readUTF = message.reader().readUTF();
                    byte readByte = message.reader().readByte();
                    byte readByte2 = message.reader().readByte();
                    int readInt = message.reader().readInt();
                    Vector vector = new Vector();
                    byte b = readUTF.length() == 0 ? (byte) 1 : (byte) 0;
                    for (int i2 = 0; i2 < readInt; i2++) {
                        int readInt2 = message.reader().readInt();
                        message.reader().readInt();
                        vector.addElement(new MenuItemInfo(readInt2, null, message.reader().readUTF(), message.reader().readUTF(), b));
                    }
                    boolean z = false;
                    if (BaseCanvas.currentScreen == null || !"SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        class857 = new GuildScreen();
                        z = true;
                    } else {
                        class857 = (GuildScreen) BaseCanvas.currentScreen;
                    }
                    class857.Method99(readUTF.length() == 0 ? null : readUTF, vector, readByte, readByte2);
                    class857.switchToMe(0, z);
                    return;
                case 2:
                    return;
                case 3:
                    int readInt3 = message.reader().readInt();
                    boolean readBoolean = message.reader().readBoolean();
                    int readInt4 = message.reader().readInt();
                    String readUTF2 = message.reader().readUTF();
                    int readInt5 = message.reader().readInt();
                    int readInt6 = message.reader().readInt();
                    byte readByte3 = message.reader().readByte();
                    byte readByte4 = message.reader().readByte();
                    int readByte5 = message.reader().readByte();
                    Vector vector2 = new Vector();
                    byte b2 = readBoolean ? (byte) 1 : (byte) 0;
                    for (int i3 = 0; i3 < readByte5; i3++) {
                        int readInt7 = message.reader().readInt();
                        String readUTF3 = message.reader().readUTF();
                        vector2.addElement(new MenuItemInfo(readInt7, readUTF3.length() == 0 ? null : readUTF3, message.reader().readUTF(), message.reader().readUTF(), b2));
                    }
                    boolean readBoolean2 = message.reader().readBoolean();
                    boolean z2 = false;
                    if (BaseCanvas.currentScreen == null || !"SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        class856 = new GuildScreen();
                        z2 = true;
                    } else {
                        class856 = (GuildScreen) BaseCanvas.currentScreen;
                    }
                    class856.Method97(readByte3, readByte4, readInt3, readBoolean, readInt4, readUTF2, readInt5, readInt6, vector2, readBoolean2);
                    class856.switchToMe(0, z2);
                    return;
                case 4:
                    int readByte6 = message.reader().readByte();
                    Vector vector3 = new Vector();
                    for (int i4 = 0; i4 < readByte6; i4++) {
                        int readInt8 = message.reader().readInt();
                        String readUTF4 = message.reader().readUTF();
                        vector3.addElement(new MenuItemInfo(readInt8, readUTF4.length() == 0 ? null : readUTF4, message.reader().readUTF(), message.reader().readUTF(), (byte) 1));
                    }
                    boolean z3 = false;
                    if (BaseCanvas.currentScreen == null || !"SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        class855 = new GuildScreen();
                        z3 = true;
                    } else {
                        class855 = (GuildScreen) BaseCanvas.currentScreen;
                    }
                    class855.Method59(vector3);
                    class855.switchToMe(0, z3);
                    return;
                case 5:
                    int readInt9 = message.reader().readInt();
                    boolean readBoolean3 = message.reader().readBoolean();
                    Dialog.Method25();
                    if (readBoolean3 && BaseCanvas.currentScreen != null && "SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        ((GuildScreen) BaseCanvas.currentScreen).Method104(readInt9);
                        return;
                    }
                    return;
                case 6:
                    int readInt10 = message.reader().readInt();
                    boolean readBoolean4 = message.reader().readBoolean();
                    Dialog.Method25();
                    if (readBoolean4 && BaseCanvas.currentScreen != null && "SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        ((GuildScreen) BaseCanvas.currentScreen).Method104(readInt10);
                        return;
                    }
                    return;
                case 7:
                    return;
                case 8:
                    return;
                case 9:
                    int readInt11 = message.reader().readInt();
                    Vector vector4 = new Vector();
                    for (int i5 = 0; i5 < readInt11; i5++) {
                        vector4.addElement(new MenuItemInfo(message.reader().readInt(), null, message.reader().readUTF(), "", (byte) 1));
                    }
                    if (BaseCanvas.currentScreen != null && "SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId) && ((GuildScreen) BaseCanvas.currentScreen).Field540 == 16) {
                        return;
                    }
                    GuildScreen class858 = new GuildScreen();
                    class858.Method102(vector4);
                    class858.switchToMe(0, true);
                    return;
                case 10:
                case 11:
                case 13:
                case 18:
                case 21:
                default:
                    return;
                case 12:
                    return;
                case 14:
                    int readInt12 = message.reader().readInt();
                    int readByte7 = message.reader().readByte();
                    String[] strArr = new String[readByte7];
                    for (int i6 = 0; i6 < readByte7; i6++) {
                        strArr[i6] = message.reader().readUTF();
                    }
                    boolean z4 = false;
                    if (BaseCanvas.currentScreen == null || !"SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        class854 = new GuildScreen();
                        z4 = true;
                    } else {
                        class854 = (GuildScreen) BaseCanvas.currentScreen;
                    }
                    class854.Method98(readInt12, strArr);
                    class854.switchToMe(1, z4);
                    return;
                case 15:
                    byte readByte8 = message.reader().readByte();
                    byte readByte9 = message.reader().readByte();
                    int readByte10 = message.reader().readByte();
                    Vector vector5 = new Vector();
                    for (int i7 = 0; i7 < readByte10; i7++) {
                        int readInt13 = message.reader().readInt();
                        String readUTF5 = message.reader().readUTF();
                        vector5.addElement(new MenuItemInfo(readInt13, readUTF5.length() == 0 ? null : readUTF5, message.reader().readUTF(), message.reader().readUTF(), (byte) 0));
                    }
                    boolean z5 = false;
                    if (BaseCanvas.currentScreen == null || !"SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        class853 = new GuildScreen();
                        z5 = true;
                    } else {
                        class853 = (GuildScreen) BaseCanvas.currentScreen;
                    }
                    class853.Method100(readByte8, readByte9, vector5);
                    class853.switchToMe(0, z5);
                    return;
                case 16:
                    byte readByte11 = message.reader().readByte();
                    byte readByte12 = message.reader().readByte();
                    int readByte13 = message.reader().readByte();
                    Vector vector6 = new Vector();
                    for (int i8 = 0; i8 < readByte13; i8++) {
                        int readInt14 = message.reader().readInt();
                        String readUTF6 = message.reader().readUTF();
                        vector6.addElement(new MenuItemInfo(readInt14, readUTF6.length() == 0 ? null : readUTF6, message.reader().readUTF(), message.reader().readUTF(), (byte) 0));
                    }
                    boolean z6 = false;
                    if (BaseCanvas.currentScreen == null || !"SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        class852 = new GuildScreen();
                        z6 = true;
                    } else {
                        class852 = (GuildScreen) BaseCanvas.currentScreen;
                    }
                    class852.Method101(readByte11, readByte12, vector6);
                    class852.switchToMe(0, z6);
                    return;
                case 17:
                    String readUTF7 = message.reader().readUTF();
                    int readInt15 = message.reader().readInt();
                    String readUTF8 = message.reader().readUTF();
                    boolean readBoolean5 = message.reader().readBoolean();
                    String[] strArr2 = null;
                    if (message.reader().readBoolean()) {
                        String[] strArr3 = new String[7];
                        strArr2 = strArr3;
                        strArr3[0] = message.reader().readUTF();
                        strArr2[1] = message.reader().readUTF();
                        strArr2[2] = message.reader().readUTF();
                        strArr2[3] = message.reader().readUTF();
                        strArr2[4] = message.reader().readUTF();
                        strArr2[5] = message.reader().readUTF();
                        strArr2[6] = message.reader().readUTF();
                    }
                    boolean z7 = false;
                    if (BaseCanvas.currentScreen == null || !"SCREEN_GUILD".equals(BaseCanvas.currentScreen.screenId)) {
                        class85 = new GuildScreen();
                        z7 = true;
                    } else {
                        class85 = (GuildScreen) BaseCanvas.currentScreen;
                    }
                    class85.Method103(readBoolean5, readInt15, readUTF7, readUTF8, strArr2);
                    class85.switchToMe(1, z7);
                    return;
                case 19:
                    boolean readBoolean6 = message.reader().readBoolean();
                    Dialog.Method45(message.reader().readUTF(), readBoolean6 ? new Command("sửa", new IActionListener() {
                        ///////@Override
                        public void actionPerformed(Object obj) {
                            if (Screen.currentDialog != null) {
                                Screen.currentDialog.Method274();
                            }
                            Dialog.Method57("Nhập thông báo", new Command("Gửi", new IActionListener() {
                                ///////@Override
                                public void actionPerformed(Object obj) {
                                    if (Screen.currentDialog == null || !(Screen.currentDialog instanceof InputDialog)) {
                                        return;
                                    }
                                    InputDialog class172 = (InputDialog) Screen.currentDialog;
                                    String Method327 = class172.Method327(0);
                                    if (Method327.length() == 0) {
                                        Dialog.Method41("Thông báo không hợp lệ");
                                    } else if (Method327.length() > 500) {
                                        Dialog.Method41("Thông báo quá dài. Tối đa 500 ký tự");
                                    } else {
                                        class172.Method274();
                                        Message message = new Message(81);
                                        message.putByte(91);
                                        message.putByte(19);
                                        message.putString(Method327);
                                        GlobalService.session.sendMessage(message);
                                        message.cleanup();
                                    }
                                }
                            }), Command.Field1325, 0);
                        }
                    }) : null, Command.Field1325);
                    return;
                case 20:
                    BaseCanvas.getCurrentScreen().hideDialog();
                    int readInt16 = message.reader().readInt();
                    message.reader().readUTF();
                    int readByte14 = message.reader().readByte();
                    ChatPlace class115 = new ChatPlace();
                    for (int i9 = 0; i9 < readByte14; i9++) {
                        ChatPlace.setNormalChat(message.reader().readUTF(), message.reader().readUTF());
                    }
                    class115.Method79(readInt16);
                    class115.addWidget();
                    return;
                case 22:
                    if (ChatPlace.Field743) {
                        ChatPlace.setNormalChat(message.reader().readUTF(), message.reader().readUTF());
                        return;
                    }
                    Popup class27 = new Popup(new StringBuffer().append(message.reader().readUTF()).append(": ").append(message.reader().readUTF()).toString());
                    class27.start();
                    Screen.animationEffects.addElement(class27);
                    return;
                case 23:
                    PetGameScreen Method463 = Method463();
                    if (Method463 != null) {
                        int readInt17 = message.reader().readInt();
                        for (int i10 = 0; i10 < readInt17; i10++) {
                            Method463.listChar.getChar(message.reader().readInt()).Field794 = message.reader().readUTF();
                        }
                        return;
                    }
                    return;
                case 24:
                    BaseCanvas.currentScreen.Method309();
                    boolean z8 = message.reader().readByte() == 1;
                    int readInt18 = message.reader().readInt();
                    int[] iArr = new int[3];
                    int[] iArr2 = new int[3];
                    String[] strArr4 = new String[3];
                    String[] strArr5 = new String[3];
                    String[] strArr6 = new String[3];
                    for (int i11 = 0; i11 < 3; i11++) {
                        iArr[i11] = message.reader().readInt();
                        iArr2[i11] = message.reader().readInt();
                        strArr4[i11] = message.reader().readUTF();
                        strArr5[i11] = message.reader().readUTF();
                        strArr6[i11] = message.reader().readUTF();
                    }
                    Screen Screen = BaseCanvas.currentScreen;
                    Class89 class89 = null;
                    if (Screen instanceof Class89) {
                        class89 = (Class89) Screen;
                    } else {
                        int i12 = 0;
                        while (true) {
                            if (i12 < Screen.listScreen.size()) {
                                Screen Screen2 = (Screen) Screen.listScreen.elementAt(i12);
                                if (Screen2 instanceof Class89) {
                                    class89 = (Class89) Screen2;
                                } else {
                                    i12++;
                                }
                            } else {
                                break;
                            }
                        }
                    }
                    boolean z9 = class89 == null;
                    if (class89 == null) {
                        class89 = new Class89(z8 ? 0 : 1);
                    }
                    class89.Method79(readInt18);
                    class89.Method78(iArr, iArr2, strArr4, strArr6, strArr5);
                    class89.switchToMe(0, z9);
                    return;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void Method18() {
        if (skill != null) {
            skill.clear();
        }
    }

    public static PetEffectAnimation[] readPetEffAnimation(Message msg) throws IOException {
        PetEffectAnimation[] effectAnimations = new PetEffectAnimation[msg.reader().readInt()];
        for (int i = 0; i < effectAnimations.length; i++) {
            effectAnimations[i] = new PetEffectAnimation(
                    msg.reader().readInt(), 
                    msg.reader().readUTF(), 
                    msg.reader().readShort(), 
                    msg.reader().readShort(), 
                    msg.reader().readBoolean(), 
                    msg.reader().readByte(), 
                    msg.reader().readInt());
        }
        return effectAnimations;
    }
}

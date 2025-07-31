package vn.me.screen;

import defpackage.ActorFactory;
import vn.me.core.BaseCanvas;
import defpackage.Class106;
import vn.me.ui.BattleButton;
import defpackage.Class108;
import defpackage.ChatPlace;
import defpackage.mCharacter;
import vn.me.ui.ImageButton;
import defpackage.SceneManage;
import vn.me.ui.InputDialog;
import defpackage.SkillEffect;
import defpackage.Ulti;
import defpackage.MEService;
import defpackage.PetBattle;
import defpackage.Pet;
import defpackage.Mob;
import defpackage.MyCharacter;
import defpackage.PetGameModel;
import defpackage.PetRenderer;
import defpackage.Class53;
import defpackage.PetInfo;
import defpackage.Class60;
import defpackage.Command;
import vn.me.ui.Dialog;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalMessageHandler;
import defpackage.GlobalService;
import defpackage.mSkillPaint;
import vn.me.ui.interfaces.IActionListener;
import vn.me.network.Message;
import vn.me.ui.WidgetGroup;
import java.io.IOException;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Vector;
import javax.microedition.lcdui.Image;
import thong.auto.AutoManager;
import thong.sdk.ISoundManagerSDK;
import vn.me.network.Cmd;
import vn.me.ui.common.T;
import vn.me.ui.geom.Rectangle;
import vn.thong.shared.data.MoneyDisplay;

public final class PetGameScreen extends GameScene {

    private PetGameModel Field310;
    private ImageButton interactCMD;
    private ImageButton tattoCMD;
    private ImageButton Field313;
    private ImageButton playBtn;
    private ImageButton kissBtn;
    private ImageButton pukeBtn;
    private ImageButton petinfor2CMD;
    private ImageButton equipCMD;
    private ImageButton inventoryCMD;
    private ImageButton Field320;
    private ImageButton Field321;
    private ImageButton Field322;
    private ImageButton Field323;
    private ImageButton Field324;
    private ImageButton guildSkillCMD;
    private ImageButton Field326;
    private ImageButton Field327;
    public SkillEffect Field328;
    public SkillEffect Field329;
    public SkillEffect Field330;
    public Vector Field331;
    public Hashtable listBattle;
    private Class108 attackBtn;
    private Class108 skillBtn;
    private Class108 useItemBtn;
    private WidgetGroup Field336;
    private Command Field337;
    private Command Field338;
    public int hasAttackType;
    public int Field340;
    public static Image healImage;
    private boolean Field342;
    private int Field343;
    private long Field344;
    private InputDialog Field345;
    public static boolean hasLetter = false;
    private int Field347;
    private int Field348;
    private int Field349;
    private int Field350;
    private long Field351;
    public static MoneyDisplay[] moneyDisplays = new MoneyDisplay[0];
    private static Rectangle autoEnbaleBounds = new Rectangle(15, 15, 30, 30);
    public static final int SETTING_CMD = 3281;
    public static final int SOUND_BG_SETTING_CMD = 3282;
    public static final int SOUND_EFF_SETTING_CMD = 3283;
    public static final int SELECT_LANGUAGE_SETTING_CMD = 3284;
    public static final int AUTO_ATTACK_SETTING_CMD = 3285;
    public static final int CHAT_GLOBAL_CMD = 3286;

    public PetGameScreen(SceneManage class140) {
        super(class140);
        this.Field331 = new Vector();
        this.Field342 = false;
        this.Field1171 = true;
        this.Field310 = (PetGameModel) GlobalMessageHandler.Method208().Field985;
        this.listBattle = new Hashtable();
        if (healImage == null) {
            try {
                healImage = Image.createImage("/pet/button/heal.png");
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    public final void Method271(int i, int i2, int i3, int i4) {
        this.Field347 = i;
        this.Field348 = i2;
        this.Field349 = i4;
        this.Field350 = i3;
        this.Field351 = System.currentTimeMillis();
    }

    public final PetBattle Method451(int i) {
        return (PetBattle) this.listBattle.get(new Integer(i));
    }

    public final Mob findMob(int i) {
        for (int i2 = 0; i2 < this.Field331.size(); i2++) {
            Mob mob = (Mob) this.Field331.elementAt(i2);
            if (mob.mobId == i) {
                return mob;
            }
        }
        return null;
    }

    public final void update() {
        super.update();
        Enumeration elements = this.listBattle.elements();
        while (elements.hasMoreElements()) {
            PetBattle class32 = (PetBattle) elements.nextElement();
            class32.updatePetBattle();
            if (class32.isRemove) {
                this.listBattle.remove(new Integer(class32.objId[0]));
            }
        }
        long currentTimeMillis = System.currentTimeMillis();
        if (!this.Field342 || currentTimeMillis - this.Field344 < this.Field343 * 1000) {
            return;
        }
        this.Field342 = false;
    }

    public final void Method6() {
        this.container.removeWidget(this.Field336);
        this.cmdRight = this.Field337;
        this.cmdLeft = this.Field338;
    }

    public void pointerPressed(int x, int y) {
        if (AutoManager.isAutoAttack) {
            if (autoEnbaleBounds.contains(x, y)) {
                AutoManager.isAutoAttack = false;
                return;
            }
        }
        super.pointerPressed(x, y);
    }

    protected final void Method7() {
        Vector vector = new Vector();
        vector.addElement(new Command(340, T.gL(T.ADD_FRIEND), this));
        vector.addElement(new Command(341, T.gL(T.LETTER_BOX), this));
        vector.addElement(new Command(335, T.gL(T.MY_WINGS), this));
        vector.addElement(new Command(336, T.gL(T.GUILD_CHAT), this));
        vector.addElement(new Command(331, T.gL(T.COMMUNITY_CHAT), this));
        vector.addElement(new Command(CHAT_GLOBAL_CMD, T.gL(T.CHAT_GLOBAL), this));
        vector.addElement(new Command(330, T.gL(T.SKIN_INVENTORY), this));
        vector.addElement(new Command(317, T.gL(T.SELECT_PET), this));
        vector.addElement(new Command(327, T.gL(T.CHANGE_PASSWORD), this));
        vector.addElement(new Command(0, ActorFactory.gL(267), this));
        vector.addElement(new Command(SETTING_CMD, T.gL(T.SETTING), this));
        vector.addElement(new Command(10000, ActorFactory.gL(139), this));
        showMenu(vector, 0);
    }

    ///////@Override 
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 305:
                if (this.hasAttackType == 0) {
                    try {
                        this.playBtn = new ImageButton(this, Image.createImage("/pet/button/play.png"), new Command(307, T.gL(T.PLAY_WITH_PET), this));
                        this.kissBtn = new ImageButton(this, Image.createImage("/pet/button/kiss.png"), new Command(308, T.gL(T.KISS_PET), this));
                        this.pukeBtn = new ImageButton(this, Image.createImage("/pet/button/puke.png"), new Command(309, T.gL(T.RUBBING_THE_PET_HEAD), this));
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
                ImageButton[] class136Arr = {this.playBtn, this.kissBtn, this.pukeBtn};
                Method202();
                Method203(class136Arr);
                return;
            case 307:
                MEService.Method79(1);
                mCharacter class126 = SceneManage.myCharacter;
                if (((MyCharacter) SceneManage.myCharacter).ownerPet != null) {
                    this.Field881.addElement(new Class53(this, this, 1, SceneManage.myCharacter));
                }
                Method202();
                return;
            case 308:
                MEService.Method79(0);
                mCharacter class1262 = SceneManage.myCharacter;
                if (((MyCharacter) SceneManage.myCharacter).ownerPet != null) {
                    this.Field881.addElement(new Class53(this, this, 0, SceneManage.myCharacter));
                }
                Method202();
                return;
            case 309:
                MEService.Method79(2);
                mCharacter class1263 = SceneManage.myCharacter;
                if (((MyCharacter) SceneManage.myCharacter).ownerPet != null) {
                    this.Field881.addElement(new Class53(this, this, 2, SceneManage.myCharacter));
                }
                Method202();
                return;
            case 310:
                PetGameModel.Field285 = 0;
                Method202();
                MEService.Method104(1);
                GameController.waitDialog();
                return;
            case 311:
                Method202();
                GameController.waitDialog();
                Message message = new Message(81);
                message.putByte(28);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 312:
                Method202();
                Message message2 = new Message(81);
                message2.putByte(30);
                GlobalService.session.sendMessage(message2);
                message2.cleanup();
                return;
            case 313:
                Method202();
                if (this.Field884 != null) {
                    Pet class39 = ((MyCharacter) this.Field884).ownerPet;
                    if (class39 == null) {
                        GameController.startOKDlg(T.gL(T.THIS_PLAYER_IS_NOT_CARRYING_PETS));
                        return;
                    } else {
                        GameController.Method45(new StringBuffer(T.gL(T.YOU_WANT_TO_CHALLENGE)).append(class39.name).append(T.gL(T.YOU_WANT_TO_CHALLENGE__LEVEL)).append(class39.petLvl).append("?").toString(), new Command(314, T.gL(T.YES), new Integer(this.Field884.Id), this), new Command(315, T.gL(T.NO), this));
                        return;
                    }
                }
                return;
            case 314:
                int intValue = ((Integer) Command.objPerfomed).intValue();
                Message message3 = new Message(81);
                message3.putByte(12);
                message3.putInt(intValue);
                GlobalService.session.sendMessage(message3);
                message3.cleanup();
                break;
            case 315:
                break;
            case 317:
                GameController.waitDialog();
                Message message4 = new Message(81);
                message4.putByte(5);
                GlobalService.session.sendMessage(message4);
                message4.cleanup();
                return;
            case 318:
                if (this.hasAttackType != 1) {
                    this.hasAttackType = 1;
                    Message message5 = new Message(81);
                    message5.putByte(37);
                    message5.putByte(1);
                    GlobalService.session.sendMessage(message5);
                    message5.cleanup();
                    return;
                }
                return;
            case 319:
                if (this.hasAttackType != 2) {
                    this.hasAttackType = 2;
                    Message message6 = new Message(81);
                    message6.putByte(37);
                    message6.putByte(2);
                    GlobalService.session.sendMessage(message6);
                    message6.cleanup();
                    return;
                }
                return;
            case 320:
                showUseSkillDialog();
                return;
            case 321:
                if (this.hasAttackType != 3) {
                    this.hasAttackType = 3;
                    Message message7 = new Message(81);
                    message7.putByte(37);
                    message7.putByte(3);
                    message7.putInt(0);
                    GlobalService.session.sendMessage(message7);
                    message7.cleanup();
                    return;
                }
                return;
            case 322:
                int intValue2 = ((Integer) Command.objPerfomed).intValue();
                if (this.hasAttackType != 4 && this.Field340 != intValue2) {
                    this.hasAttackType = 4;
                    this.Field340 = intValue2;
                    Message message8 = new Message(81);
                    message8.putByte(37);
                    message8.putByte(4);
                    message8.putInt(intValue2);
                    GlobalService.session.sendMessage(message8);
                    message8.cleanup();
                }
                Method309();
                return;
            case 323:
                Method202();
                ((MyCharacter) SceneManage.myCharacter).ownerPet.isHealing = true;
                MEService.show(true);
                SceneManage.myCharacter.Field815 = true;
                return;
            case 324:
                GameController.waitDialog();
                Message message9 = new Message(81);
                message9.putByte(54);
                GlobalService.session.sendMessage(message9);
                message9.cleanup();
                return;
            case 325:
                PetGameModel.Field285 = 0;
                Method202();
                if (this.Field884 != null) {
                    MEService.Method28(this.Field884.Id, 0);
                    GameController.waitDialog();
                    return;
                }
                return;
            case 326:
                Method202();
                if (this.Field884 != null) {
                    GameController.waitDialog();
                    MEService.Method28(this.Field884.Id, 1);
                    return;
                }
                return;
            case 327:
                GameController.Method56(T.gL(T.CHANGE_PASSWORD), new String[]{T.gL(T.CURRENT_PASSWORD), T.gL(T.NEW_PASSWORD), T.gL(T.RETYPE)}, new int[]{2, 2, 2}, new Command(328, T.gL(T.CHANGE), this), GameController.Field464);
                return;
            case 328:
                InputDialog class172 = (InputDialog) Screen.currentDialog;
                String Method327 = class172.Method327(0);
                String Method3272 = class172.Method327(1);
                if (!class172.Method327(2).equals(Method3272) || "".equals(Method3272)) {
                    GameController.startOKDlg(T.gL(T.WRONG_PASSWORD));
                    return;
                }
                Method309();
                GameController.waitDialog();
                GlobalService.Method252(Method327, Method3272);
                return;
            case 330:
                GameController.waitDialog();
                Message message10 = new Message(81);
                message10.putByte(62);
                GlobalService.session.sendMessage(message10);
                message10.cleanup();
                return;
            case 331:
                new ChatPlace().addWidget();
                return;
            case CHAT_GLOBAL_CMD:
                new ChatPlace().addWidget();
                ChatPlace.typeChat = ChatPlace.TYPE_CHAT_GLBAL;
                return;
            case 332:
                GameController.waitDialog();
                Message message11 = new Message(81);
                message11.putByte(90);
                message11.putByte(1);
                GlobalService.session.sendMessage(message11);
                message11.cleanup();
                Method202();
                return;
            case 333:
                Method202();
                if (this.Field884 != null) {
                    GameController.waitDialog();
                    int i = this.Field884.Id;
                    Message message12 = new Message(81);
                    message12.putByte(91);
                    message12.putByte(17);
                    message12.putInt(i);
                    GlobalService.session.sendMessage(message12);
                    message12.cleanup();
                    return;
                }
                return;
            case 334:
                GameController.waitDialog();
                Message message13 = new Message(81);
                message13.putByte(92);
                message13.putByte(1);
                GlobalService.session.sendMessage(message13);
                message13.cleanup();
                return;
            case 335:
                GameController.waitDialog();
                Message message14 = new Message(81);
                message14.putByte(92);
                message14.putByte(2);
                GlobalService.session.sendMessage(message14);
                message14.cleanup();
                return;
            case 336:
                GameController.waitDialog();
                Message message15 = new Message(81);
                message15.putByte(91);
                message15.putByte(20);
                GlobalService.session.sendMessage(message15);
                message15.cleanup();
                return;
            case 337:
                Method202();
                if (this.Field884 != null) {
                    GameController.waitDialog();
                    int i2 = this.Field884.Id;
                    Message message16 = new Message(81);
                    message16.putByte(96);
                    message16.putInt(i2);
                    GlobalService.session.sendMessage(message16);
                    message16.cleanup();
                    return;
                }
                return;
            case 338:
                Method202();
                GameController.waitDialog();
                MEService.showSkillClan(GameController.myInfo.Field48);
                return;
            case 339:
                Method202();
                if (this.Field884 != null) {
                    GameController.waitDialog();
                    MEService.showSkillClan(this.Field884.Id);
                    return;
                }
                return;
            case SETTING_CMD: {
                showSetting();
            }
            break;

            case 340:
                Vector vector = new Vector();
                vector.addElement(new Command(3401, T.gL(T.LIST_FRIEND), this));
                vector.addElement(new Command(3402, T.gL(T.ADD_FRIEND), this));
                vector.addElement(new Command(3403, T.gL(T.PENDING_FRIEND_REQUESTS), this));
                vector.addElement(new Command(3404, T.gL(T.BLOCK_LIST), this));
                showMenu(vector, 0);
                return;
            case 341:
                Vector vector2 = new Vector();
                vector2.addElement(new Command(3411, T.gL(T.INBOX), this));
                vector2.addElement(new Command(3412, T.gL(T.LETTER_BOX), this));
                showMenu(vector2, 0);
                return;
            case 342:
                GameController.waitDialog();
                int i3 = this.Field884.Id;
                Message message17 = new Message(Cmd.LETTER_COMMAND);
                message17.putByte(3);
                message17.putInt(i3);
                GlobalService.session.sendMessage(message17);
                message17.cleanup();
                return;
            case 3401:
                GameController.waitDialog();
                Message message18 = new Message(Cmd.LETTER_COMMAND);
                message18.putByte(1);
                GlobalService.session.sendMessage(message18);
                message18.cleanup();
                return;
            case 3402:
                this.Field345 = Dialog.Method57(T.gL(T.ADD_FRIEND), new Command(34021, T.gL(T.OK), this), GameController.Field464, 0);
                return;
            case 3403:
                GameController.waitDialog();
                Message message19 = new Message(Cmd.LETTER_COMMAND);
                message19.putByte(2);
                GlobalService.session.sendMessage(message19);
                message19.cleanup();
                return;
            case 3404:
                GameController.waitDialog();
                Message message20 = new Message(Cmd.LETTER_COMMAND);
                message20.putByte(10);
                GlobalService.session.sendMessage(message20);
                message20.cleanup();
                return;
            case 3411:
                GameController.waitDialog();
                Message message21 = new Message(Cmd.LETTER_COMMAND);
                message21.putByte(Cmd.LETTER_BOX);
                GlobalService.session.sendMessage(message21);
                message21.cleanup();
                return;
            case 3412:
                new Class106().Method0();
                return;
            case 34021:
                GameController.waitDialog();
                String Method3273 = this.Field345.Method327(0);
                this.Field345.Method274();
                Message message22 = new Message(Cmd.LETTER_COMMAND);
                message22.putByte(Cmd.LETTER_COMMAND_REQUEST_ADD_FRIEND_WITH_NAME);
                message22.putString(Method3273);
                GlobalService.session.sendMessage(message22);
                message22.cleanup();
                return;
            case SOUND_BG_SETTING_CMD:
                ISoundManagerSDK.hasPermissionPlayBgSound = !ISoundManagerSDK.hasPermissionPlayBgSound;
                if (ISoundManagerSDK.currentSoundBg != null && !ISoundManagerSDK.hasPermissionPlayBgSound) {
                    ISoundManagerSDK.currentSoundBg.stop();
                } else if (ISoundManagerSDK.currentSoundBg != null && ISoundManagerSDK.hasPermissionPlayBgSound) {
                    ISoundManagerSDK.currentSoundBg.start();
                }
                ISoundManagerSDK.saveMusicState();
                showSetting();
                break;
            case SOUND_EFF_SETTING_CMD:
                ISoundManagerSDK.hasPermissionPlayEffSound = !ISoundManagerSDK.hasPermissionPlayEffSound;
                ISoundManagerSDK.saveMusicState();
                showSetting();
                break;
            case SELECT_LANGUAGE_SETTING_CMD:
                ActorFactory.saveInt(ActorFactory.languageRMS, -99999);
                try {
                    GlobalService.session.close();
                } catch (Exception e) {
                }
                new SplashScreen(0).switchToMe(0, true);
                return;
            case AUTO_ATTACK_SETTING_CMD:
                AutoManager.isAutoAttack = !AutoManager.isAutoAttack;
                break;
            default:
                super.actionPerformed(obj);
                return;
        }
        Screen.hideDialog(Screen.currentDialog);
    }

    private void showSetting() {
        Vector vector = new Vector();
        vector.addElement(new Command(SOUND_BG_SETTING_CMD, T.gL(T.SOUND_BG_SETTING) + (ISoundManagerSDK.hasPermissionPlayBgSound ? T.gL(T.ENABLE) : T.gL(T.DISENABLE)), this));
        vector.addElement(new Command(SOUND_EFF_SETTING_CMD, T.gL(T.SOUND_EFF_SETTING) + (ISoundManagerSDK.hasPermissionPlayEffSound ? T.gL(T.ENABLE) : T.gL(T.DISENABLE)), this));
        vector.addElement(new Command(SELECT_LANGUAGE_SETTING_CMD, T.gL(T.SELECT_LANGUAGE), this));
        vector.addElement(new Command(AUTO_ATTACK_SETTING_CMD, T.gL(T.TURN_ON_OR_OFF_AUTO_ATTACK), this));
        showMenu(vector, 0);
    }

    private void showUseSkillDialog() {
        PetInfo petInfo = Method451(SceneManage.myCharacter.Id).obj[0].petInfo;
        if (petInfo.skillDesc.length == 0) {
            GameController.Method47(T.gL(T.NO_SKILL));
            return;
        }
        Class60 class60 = new Class60();
        Command[] CommandArr = new Command[petInfo.skillDesc.length];
        for (int i = 0; i < petInfo.skillName.length; i++) {
            CommandArr[i] = new Command(322, T.gL(T.OK), new Integer(petInfo.skillId[i]), this);
        }
        class60.Method13(petInfo.skillId, petInfo.skillName, petInfo.skillDesc, petInfo.mpLost, CommandArr);
        class60.show(true);
    }

    protected final void drawGUI() {
        if (hasLetter && BaseCanvas.ticks % 50 > 10) {
            BaseCanvas.g.drawImage(GameResourceManager.Field596, 1, 49, 0);
            GameResourceManager.Field596.getWidth();
        }
        PetRenderer class47 = PetGameModel.Field283;
        GameResourceManager.Method116().drawString(BaseCanvas.g, PetGameModel.mGoldStr, 20, 2, 0);
        BaseCanvas.g.drawRegion(GameResourceManager.Field597, PetRenderer.Field302 * 14, 0, 14, 14, 0, 2, 2, 0);
        GameResourceManager.Method116().drawString(BaseCanvas.g, PetGameModel.mCoinStr, 20, 16, 0);
        BaseCanvas.g.drawImage(class47.Field306.gemImg, 2, 18, 0);
        int yDraw = (15 * 3) + 10 + 20;
        for (int i = 0; i < moneyDisplays.length; i++) {
            MoneyDisplay moneyDisplay = moneyDisplays[i];
            if (moneyDisplay.image == null) {
                moneyDisplay.image = PetGameModel.Field284.requestImg(moneyDisplay.imgPath);
                continue;
            }
            BaseCanvas.g.drawImage(moneyDisplay.image, 2, yDraw, 0);
            GameResourceManager.Method116().drawString(BaseCanvas.g, moneyDisplay.valueStr, moneyDisplay.image.getWidth() + 4, yDraw + ((moneyDisplay.image.getHeight() / 2) - (15 / 2)), 0);
            yDraw += moneyDisplay.image.getHeight() + 5;
        }
        long currentTimeMillis = System.currentTimeMillis();
        if (currentTimeMillis - PetRenderer.Field303 > 100) {
            PetRenderer.Field302 = (PetRenderer.Field302 + 1) % 5;
            PetRenderer.Field303 = currentTimeMillis;
        }
        long currentTimeMillis2 = (System.currentTimeMillis() - this.Field351) / 1000;
        int i = this.Field347;
        if (this.Field350 != 0 && currentTimeMillis2 > this.Field348) {
            int i2 = (int) (i + 1 + ((currentTimeMillis2 - this.Field348) / this.Field350));
            i = i2;
            if (i2 > this.Field349) {
                i = this.Field349;
            }
        }
        int i3 = 2;
        GameResourceManager.Method116().drawString(BaseCanvas.g, new StringBuffer("(nluong)").append(i).toString(), 2, 34, 0);
        if (this.Field342) {
            BaseCanvas.g.drawImage(GameResourceManager.Field605, 2, 52, 0);
            i3 = 19;
            GameResourceManager.Method116().drawString(BaseCanvas.g, Ulti.Method374(((this.Field343 * 1000) - (System.currentTimeMillis() - this.Field344)) / 1000), 19, 52, 0);
        }
        Method194();
        if (this.Field310.myPet != null && this.Field908) {
            int maxValue = PetGameModel.myPetMaxMp > PetGameModel.myPetMaxHp ? PetGameModel.myPetMaxMp : PetGameModel.myPetMaxHp;
            int vX = -(GameResourceManager.Method117().getWidth(Ulti.formatNumber(maxValue)) * 2);
            BaseCanvas.g.drawImage(this.Field310.levelImg, BaseCanvas.w - 63 + vX, 15, 17);
            GameResourceManager.Method116().drawString(BaseCanvas.g, String.valueOf(this.Field310.myPet.petLvl), BaseCanvas.w - 63 + vX, 22, 17);
            if (PetGameModel.hasPetInfo) {
                int i4 = PetGameModel.Field292;
                int i5 = PetGameModel.myPetMaxHp;
                i3 = PetGameModel.Field293;
                PetGameModel.Method467(44, i4, i5, i3, PetGameModel.myPetMaxMp, BaseCanvas.w - 48 + vX, 20, true);
            } else {
                int i6 = PetGameModel.oldMyPetHp;
                int i7 = PetGameModel.myPetMaxHp;
                i3 = PetGameModel.oldMyPetMp;
                PetGameModel.Method467(44, i6, i7, i3, PetGameModel.myPetMaxMp, BaseCanvas.w - 48 + vX, 20, true);
            }
        }

        if (SceneManage.myCharacter != null && BaseCanvas.w > 200) {
            Object obj = listBattle.get(new Integer(SceneManage.myCharacter.Id));
            if (obj != null) {
                PetBattle petBattle = (PetBattle) obj;
                if (petBattle.objId[0] == SceneManage.myCharacter.Id) {
                    mSkillPaint skillPaint = petBattle.obj[1];
                    PetInfo petInfo = skillPaint.petInfo;
                    int maxValue = petInfo.maxMp > petInfo.maxHp ? petInfo.maxMp : petInfo.maxHp;
                    int vX = -((GameResourceManager.Method117().getWidth(Ulti.formatNumber(maxValue)) * 2) * 2) - 60;
                    BaseCanvas.g.drawImage(this.Field310.levelImg_red, BaseCanvas.w - 63 + vX, 15, 17);
                    GameResourceManager.Method116().drawString(BaseCanvas.g, String.valueOf(0), BaseCanvas.w - 63 + vX, 22, 17);
                    int i4 = petInfo.hp;
                    if (petInfo.isBoss) {
                        i4 = petInfo.hpBoss;
                    }
                    int i5 = petInfo.maxHp;
                    i3 = petInfo.mp;
                    PetGameModel.Method467(44, i4, i5, i3, petInfo.maxMp, BaseCanvas.w - 48 + vX, 20, true);
                }
            }
        }

        if (this.Field310.Field299) {
            int i8 = BaseCanvas.w - 44;
            Image Method455 = PetGameModel.Field284.requestImg(this.Field310.Field300);
            if (Method455 != null) {
                long currentTimeMillis3 = System.currentTimeMillis();
                int i9 = i3;
                if (this.Field310.Field298 - ((currentTimeMillis3 - this.Field310.Field297) / 1000) >= 10 || currentTimeMillis3 % 10 < 5) {
                    BaseCanvas.g.drawImage(Method455, i8, 50, 0);
                }
                if (i9 <= 0) {
                    this.Field310.Field299 = false;
                }
            }
        }
        Method189();
        if (AutoManager.isAutoAttack) {
            BaseCanvas.g.drawRect(autoEnbaleBounds.x, autoEnbaleBounds.y, autoEnbaleBounds.size.width, autoEnbaleBounds.size.height);
        }
    }

    public final void Method26() {
        Method309();
        int i = GameController.myInfo.Field48;
        String str = GameController.myInfo.Field50;
        this.Field899 = true;
        if (this.interactCMD == null) {
            try {
                this.Field313 = new ImageButton(this, healImage, new Command(323, T.gL(T.HEAL), this));
                this.interactCMD = new ImageButton(this, Image.createImage("/pet/button/interact.png"), new Command(305, T.gL(T.INTERACT_PET), this));
                this.petinfor2CMD = new ImageButton(this, Image.createImage("/pet/button/petinfor2.png"), new Command(310, T.gL(T.INFOMATION), this));
                this.equipCMD = new ImageButton(this, Image.createImage("/pet/button/equip.png"), new Command(311, T.gL(T.EQUIP), this));
                this.inventoryCMD = new ImageButton(this, Image.createImage("/pet/button/inventory.png"), new Command(312, T.gL(T.INVENTORY), this));
                this.tattoCMD = new ImageButton(this, Image.createImage("/pet/button/xam.png"), new Command(332, T.gL(T.TATTOO), this));
                this.guildSkillCMD = new ImageButton(this, Image.createImage("/pet/button/guildSkill.png"), new Command(338, T.gL(T.CLAN_SKILL), this));
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        Method203(new ImageButton[]{this.Field313, this.tattoCMD, this.inventoryCMD, this.petinfor2CMD, this.equipCMD, this.guildSkillCMD, this.interactCMD});
    }

    public final void Method49() {
        Method309();
        this.Field899 = true;
        if (this.Field320 == null) {
            this.Field320 = new ImageButton(this, this.Field905.Field971, new Command(313, ActorFactory.gL(667), this));
            Image image = null;
            Image image2 = null;
            Image image3 = null;
            Image image4 = null;
            Image iOException = null;
            Image iOException2 = null;
            try {
                image = Image.createImage("/pet/button/petinfor2.png");
                image2 = Image.createImage("/pet/button/equip.png");
                image3 = Image.createImage("/pet/button/guild.png");
                image4 = Image.createImage("/pet/button/guildSkill.png");
                iOException = Image.createImage("/pet/button/ketban.png");
                iOException2 = iOException;
            } catch (IOException e) {
                e.printStackTrace();
            }
            this.Field322 = new ImageButton(this, image, new Command(325, T.gL(T.MY_INFO), this));
            this.Field323 = new ImageButton(this, image2, new Command(326, T.gL(T.MY_EQUIP), this));
            this.Field324 = new ImageButton(this, image3, new Command(333, T.gL(T.CLAN), this));
            this.Field321 = new ImageButton(this, this.Field905.Field971, new Command(337, T.gL(T.PK), this));
            this.Field326 = new ImageButton(this, image4, new Command(339, T.gL(T.CLAN_SKILL), this));
            this.Field327 = new ImageButton(this, iOException2, new Command(342, T.gL(T.ADD_FRIEND), this));
        }
        Method203(new ImageButton[]{this.Field327, this.Field321, this.Field320, this.Field322, this.Field323, this.Field324, this.Field326});
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public final void Method92(Message message) {
        try {
            int readInt = message.reader().readInt();
            byte readByte = message.reader().readByte();
            MyCharacter class42 = (MyCharacter) this.listChar.getChar(readInt);
            if (class42 == null || class42.ownerPet == null) {
                return;
            }
            this.Field881.addElement(new Class53(this, this, readByte, class42));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public final void showAttackUI() {
        if (this.attackBtn == null) {
            Image image = null;
            Image image2 = null;
            Image iOException = null;
            Image iOException2 = null;
            try {
                image = Image.createImage("/pet/battle/attack.png");
                image2 = Image.createImage("/pet/battle/skill.png");
                iOException = Image.createImage("/pet/battle/potion.png");
                iOException2 = iOException;
            } catch (IOException e) {
                e.printStackTrace();
            }
            this.attackBtn = new BattleButton(image);
            this.skillBtn = new BattleButton(image2);
            this.useItemBtn = new BattleButton(iOException2);
            int width = image.getWidth();
            int i = (BaseCanvas.w - 60) - 2;
            int i2 = (i - (width << 2)) / 8;
            this.attackBtn.setMetrics(20, 0, 16, 16);
            int i3 = 20 + width + i2;
            this.skillBtn.setMetrics(i3, 0, 16, 16);
            this.useItemBtn.setMetrics(i3 + width + i2, 0, 16, 16);
            this.attackBtn.cmdCenter = new Command(318, T.gL(T.FIGHT), this);
            this.skillBtn.cmdCenter = new Command(320, T.gL(T.USE_SKILL), this);
            this.useItemBtn.cmdCenter = new Command(321, T.gL(T.USE_ITEM), this);
            this.Field336 = new WidgetGroup(60, BaseCanvas.h - 37, i, 16);
            this.Field336.columns = 4;
            this.Field336.isScrollableX = true;
            this.Field336.isLoop = true;
            this.Field336.setViewMode(2);
            this.Field336.addWidget(this.attackBtn);
            this.Field336.addWidget(this.skillBtn);
            this.Field336.addWidget(this.useItemBtn);
        }
        this.skillBtn.setFocused(false);
        this.useItemBtn.setFocused(false);
        this.attackBtn.requestFocus();
        this.container.addWidget(this.Field336);
        this.Field337 = this.cmdRight;
        this.Field338 = this.cmdLeft;
        this.cmdRight = null;
        this.cmdLeft = null;
        this.hasAttackType = -1;
        this.Field340 = -1;
    }

    protected final void Method190() {
        Vector vector = new Vector();
        if (this.Field879.mGOMapId != 12) {
            vector.addElement(new Command(T.gL(T.CLAN_INFO), new IActionListener() {
                ///////@Override
                public void actionPerformed(Object obj) {
                    Dialog.Method7();
                    MEService.Method0();
                }
            }));
            vector.addElement(new Command(T.gL(T.SUBMIT_GUILD_FUNDS), new IActionListener() {
                ///////@Override
                public void actionPerformed(Object obj) {
                    Dialog.Method7();
                    Message message = new Message(81);
                    message.putByte(91);
                    message.putByte(9);
                    GlobalService.session.sendMessage(message);
                    message.cleanup();
                }
            }));
            vector.addElement(GameController.Field479);
            vector.addElement(GameController.Field476);
            vector.addElement(new Command(324, T.gL(T.CHECK_THE_TASK), this));
            vector.addElement(GameController.letterCmd);
            vector.addElement(this.Field897);
        } else {
            vector.addElement(new Command(41, T.gL(T.GIVE_UP), GameController.instance));
            vector.addElement(GameController.Field476);
            vector.addElement(GameController.letterCmd);
            vector.addElement(this.Field897);
        }
        showMenu(vector, 1);
    }

    public final void Method79(int i) {
        Image image = GameResourceManager.Field605;
        if (image == null) {
            try {
                image = Image.createImage("/clock.png");
                GameResourceManager.Field605 = image;
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        this.Field343 = i;
        this.Field344 = System.currentTimeMillis();
        this.Field342 = true;
    }
}

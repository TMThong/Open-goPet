package vn.me.network;

import java.io.IOException;
import java.util.Vector;
import javax.microedition.lcdui.Image;
//import vn.me.common.Board;
import vn.me.common.ChargeMoneyInfo;
import vn.me.common.GameController;
import vn.me.common.GameResourceManager;
import vn.me.common.GuiderDialog;
import vn.me.common.L;
import vn.me.common.LoginScreen;
import vn.me.common.MenuItemInfo;
import vn.me.common.MenuScreen;
import vn.me.common.MessageScreen;
import vn.me.common.Player;
import vn.me.common.Server;
import vn.me.common.Util;
import vn.me.common.effects.Popup;
import vn.me.common.effects.BigTextEffect;
import vn.me.core.ILiveObject;
import vn.me.game.mgo.Actor;
import vn.me.game.mgo.AvatarItem;
import vn.me.game.mgo.GameScene;
//import vn.me.game.mgo.Map;
import vn.me.game.mgo.Npc;
import vn.me.game.mgo.SceneManager;
import vn.me.core.BaseCanvas;
import vn.me.game.mPet.PetGameScene;
import vn.me.game.mgo.ActorFactory;
import vn.me.game.mgo.Map;
import vn.me.game.mPet.PetGameModel;
import vn.me.game.mPet.gui.ImageDialog;
import vn.me.game.mgo.Avatar;
import vn.me.game.model.Board;
import vn.me.game.model.IwinMessage;
import vn.me.ui.Dialog;
import vn.me.ui.Screen;
import vn.me.ui.geom.Rectangle;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.model.Command;

public class GlobalMessageHandler implements IMessageListener {

    public GlobalLogicHandler globalLogicHandler = new GlobalLogicHandler();
    public IGameMessageHandler gameMessageHandler;
    protected static GlobalMessageHandler instance;
    public static int serviceID = 0;
    public SceneManager sceneManager;
    
    public IMessageNotify messagenotify;

    public static GlobalMessageHandler getInstance() {
        if (instance == null) {
            instance = new GlobalMessageHandler();
        }
        return instance;
    }

    public void onConnectOK() {
        //#if DefaultConfiguration
//@	System.out.println(  "Connected to : " + GlobalService.session.currentIp + ":" + GlobalService.session.currentPort);
        //#endif
        globalLogicHandler.onConnectOK();
    }

    public void onConnectionFail() {
        //#if DefaultConfiguration
//@	System.out.println(  "onConnectionFail");
        //#endif
//        if (!XScreen.isLoadingDone) {
//            return;
//        }
        if (!GlobalService.isChangeSession) {
            globalLogicHandler.onConnectFail();
        } else {
            GlobalService.connectOKAction = new IActionListener() {

                public void actionPerformed(Object o) {
                    GlobalService.requestCheckSessionForNewService();
                }
            };
            GlobalService.IP = GlobalService.LOGIN_IP;
            GlobalService.PORT = GlobalService.LOGIN_PORT;
            GlobalService.isChangeSession = false;
//            GlobalService.closeNetwork();
            //#if DefaultConfiguration
//@	    System.out.println(  "Reconnect to master server: " + GlobalService.IP + ":" + GlobalService.PORT);
            //#endif
            GlobalService.connect();
        }
    }

    public void onDisconnected() {
        //#if DefaultConfiguration
//@	System.out.println(  "onDisconnect");
        //#endif
        globalLogicHandler.onDisconnect();
    }

    public void onMessage(Message msg) {
        //#if DefaultConfiguration
//@	System.out.println(  "SERVER Message: " + msg.id);
        //#endif
        try {
            switch (msg.id) {
                case MGOProtocol.JOINT_MAP_AUTO_SELECT_CHANNEL:
                    String text = msg.reader().readUTF();
                    GameController.startErrorDlg(text, true);
                    break;
                case MGOProtocol.SHOUT_MESSAGE:
                    if (sceneManager == null) {
                        break;
                    }
                    int id = msg.reader().readInt();
                    String message = msg.reader().readUTF();
                    if (message.length() > 0) {
                        sceneManager.onSomeShout(id, message);
                    }
                    break;
                case MGOProtocol.CLEAR_RMS_REQUEST:
                    globalLogicHandler.onClearRMSRequest();
                    break;
                case MGOProtocol.ON_OTHER_USER_LEAVE:
                    if (sceneManager == null) {
                        break;
                    }
                    id = msg.reader().readInt();
                    byte faceDir = msg.reader().readByte();
                    int size = msg.reader().readInt();
                    if (size <= 0) {
                        sceneManager.onActorLeave(id, faceDir, null);
                    } else {
                        int[] waypoint = new int[size];
                        for (int i = 0; i < size; i++) {
                            waypoint[i] = msg.reader().readInt();
                        }
                        sceneManager.onActorLeave(id, faceDir, waypoint);
                    }

                    break;
                case MGOProtocol.ON_OTHER_USER_MOVE:
                    if (sceneManager == null) {
                        break;
                    }
                    id = msg.reader().readInt();
                    faceDir = msg.reader().readByte();
                    int sceneId = msg.reader().readInt();
                    size = msg.reader().readInt();
                    if (size <= 0) {
                        break;
                    }
                    int[] waypoint = new int[size];
                    for (int i = 0; i < size; i++) {
                        waypoint[i] = msg.reader().readInt();
                    }
                    sceneManager.onActorMove(id, sceneId, faceDir, waypoint);
                    break;
                case MGOProtocol.UPDATE_MAP_DATA_FROM_SERVER:
                    msg.reader().readByte();
                    GameController.myInfo.mGoMapId = msg.reader().readInt();
                    int mapVersion = msg.reader().readInt();
                    Map.saveMapVersion(GameController.myInfo.mGoMapId, mapVersion);
                    processMapData(msg, GameController.myInfo.mGoMapId);
                    break;
                case MGOProtocol.ON_UPDATE_PLAYER_IN_MAP:
                    if (sceneManager == null) {
                        break;
                    }
                    GameController.myInfo.mGoMapId = msg.reader().readInt();
                    GameController.myInfo.mGoMapChanel = msg.reader().readInt();
                    byte entranceIndex = msg.reader().readByte();
                    int myX = msg.reader().readInt();
                    int myY = msg.reader().readInt();

                    sceneManager.leaveCurrentScene();
                    Map map2 = new Map(GameController.myInfo.mGoMapId, getInstance().sceneManager, myX, myY);

                    GameScene s = null;
                    s = new PetGameScene(sceneManager);

                    if (entranceIndex >= 0) {
                        for (int i = 0; i < map2.entranceList.length; i++) {
                            if (map2.entranceList[i].index == entranceIndex) {
                                myX = map2.entranceList[i].x;
                                myY = map2.entranceList[i].y;
                                break;
                            }
                        }
                    }
                    sceneManager.creatAndAddObjectToScene(s, map2, GameController.myInfo.mGoMapId, myX, myY);
//                    GameScene s = sceneManager.creatAndAddObjectToScene(GameController.myInfo.mGoMapId, myX, myY);
                    SceneManager.currentScene = s;

                    while (msg.reader().available() > 0) {
                        readPlayerInfor(false, msg);
                    }

                    s.switchToMe(0);

                    if (GameController.myInfo.mGoMapId != Map.SURVIVAL) {
                        String greeting = L.gL(666) + ' ' + String.valueOf(GameController.myInfo.mGoMapChanel);
                        if (BaseCanvas.w - 20 < GameResourceManager.getBigFont().getWidth(greeting)) {
                            greeting = L.gL(666) + ' ' + String.valueOf(GameController.myInfo.mGoMapChanel);
                        }
                        BigTextEffect effect = new BigTextEffect(greeting);
                        effect.start();
                        GameScene.overlays.addElement(effect);
                    }

                    GameScene.updateMapIndicator();
                    // request hình effect
//                    GameResourceManager.getGuiderImg(GameResourceManager.imageEffectSendGift, (byte) 3);
                    break;
                case MGOProtocol.ON_PLAYER_ENTER_MAP:
                    if (GameController.myInfo == null) {
                        GameController.myInfo = new Player();
                    }
                    readPlayerInfor(true, msg);
                    break;
                case MGOProtocol.INIT_PLAYER:
                    if (GameController.myInfo == null) {
                        GameController.myInfo = new Player();
                    }
                    GameController.myInfo.IDDB = msg.reader().readInt();
                    GameController.myInfo.characterName = msg.reader().readUTF();

                    GameController.myInfo.mGoGender = msg.reader().readInt();

                    SceneManager.myCharacter = ActorFactory.createActor(GameController.myInfo.IDDB, (byte) GameController.myInfo.mGoGender, sceneManager);
                    SceneManager.myCharacter.setName(GameController.myInfo.characterName);
                    // khoi tao player trong nha
                    SceneManager.myCharacter.relation = msg.reader().readByte();
                    GameController.exchangeRate = msg.reader().readInt();
                    break;
                case MGOProtocol.SET_CLIENT_INFO:
                    byte isOK = msg.reader().readByte();
                    if (isOK == 1) {
                        globalLogicHandler.onSetClientInfoOK();
                        //#if DefaultConfiguration
//@			System.out.println(  "SERVER: SET_CLIENT_INFO: OK");
                        //#endif
                    } else {
                        globalLogicHandler.onConnectFail();
                        //#if DefaultConfiguration
//@			System.out.println(  "SERVER: SET_CLIENT_INFO: FAIL");
                        //#endif
                    }
                    break;
                case MGOProtocol.LOGIN_FAIL:
                    //#if DefaultConfiguration
//@		    System.out.println(  "SERVER LOGIN_FAIL");
                    //#endif
                    globalLogicHandler.onLoginFail(msg.reader().readUTF());
                    break;
                case MGOProtocol.LOGIN_SUCESS:
                    //#if DefaultConfiguration
//@		    System.out.println(  "SERVER LOGIN_SUCESS");
                    //#endif
                    GameController.myInfo = new Player();
                    GameController.myInfo.IDDB = msg.reader().readInt();
                    GameController.myInfo.userName = msg.reader().readUTF();
                    GlobalService.sessionKey = msg.reader().readUTF();
                    globalLogicHandler.onLoginSuccess();

                    if (sceneManager == null) {
                        sceneManager = new SceneManager();
                        sceneManager.loadIconButtonRes();
                    }

                    GlobalMessageHandler.getInstance().gameMessageHandler = new PetGameModel();
                    BaseCanvas.instance.liveObject = (ILiveObject) GlobalMessageHandler.getInstance().gameMessageHandler;
                    break;
                case MGOProtocol.REGISTER_INFO:
                    //#if DefaultConfiguration
//@		    System.out.println(  "SERVER REGISTER_INFO");
                    //#endif
                    String username = msg.reader().readUTF();
                    boolean available = msg.reader().readBoolean();
                    String smsPrefix = msg.reader().readUTF();
                    String sendTo = msg.reader().readUTF();

                    //#if DefaultConfiguration
//@		    System.out.println(  "username: " + username);
//@		    System.out.println(  "available:" + available);
//@		    System.out.println(  "smsPrefix: " + smsPrefix);
//@		    System.out.println(  "sendTo: " + sendTo);
                    //#endif
                    globalLogicHandler.onRegisterInfo(username, available, smsPrefix, sendTo);
                    break;
                case MGOProtocol.ERROR:
//                    GameController.closeWaitDialog();
                    ((Screen) BaseCanvas.currentScreen).hideAllDialog();
                    String error = msg.reader().readUTF();
                    //#if DefaultConfiguration
//@		    System.out.println(  "ERROR: " + error);
                    //#endif
                    GameController.startErrorDlg(error);
                    //#if Android || mGo_BigScreen || mGo_BigScreen_Ovi
//@                     if(BaseCanvas.currentScreen instanceof LoginScreen){
//@                         LoginScreen loginScreen = (LoginScreen)BaseCanvas.currentScreen;
//@                         loginScreen.showControl();
//@                     }
                    //#endif
                    break;
                case MGOProtocol.GET_SMS_SYNTAX:
                    //#if DefaultConfiguration
//@		    System.out.println(  "SERVER GET_SMS_SYNTAX");
                    //#endif
                    byte subId = msg.reader().readByte();
                    String smsData = msg.reader().readUTF();
                    String address = msg.reader().readUTF();
                    //#if DefaultConfiguration
//@		    System.out.println(  "subId: " + subId);
//@		    System.out.println(  "smsData:" + smsData);
//@		    System.out.println(  "address: " + address);
                    //#endif
                    globalLogicHandler.onGetSMSSyntax(subId, smsData, address);
                    break;
                case MGOProtocol.GUIDER:
                    subId = msg.reader().readByte();
                    switch (subId) {
                        case 1: {
                            if (sceneManager == null) {
                                break;
                            }
                            if (GameController.nShowGuide == 0) {
                                int guiId = msg.reader().readInt();

                                boolean isShowingThisGuide = false;
                                Vector dialogs = Screen.dialogs;
                                for (int i = 0; i < dialogs.size(); i++) {
                                    Dialog d1 = (Dialog) dialogs.elementAt(i);
                                    if (d1 instanceof GuiderDialog) {
                                        if (((GuiderDialog) d1).guiId == guiId) {
                                            isShowingThisGuide = true;
                                            break;
                                        }
                                    }
                                }

                                if (isShowingThisGuide) {
                                    break;
                                }

                                String guidTittle = msg.reader().readUTF();
                                int len = msg.reader().readInt();
                                String[] guides = new String[len];
                                for (int i = 0; i < len; i++) {
                                    guides[i] = msg.reader().readUTF();
                                }
                                String imgName = msg.reader().readUTF();

//                                GameController.showGui(imgName);
                                int NPCid = msg.reader().readInt();

                                GuiderDialog d = new GuiderDialog(guiId, imgName, guides,
                                        SceneManager.myCharacter.y - Avatar.HEIGHT);
                                d.show(false);
                                if (SceneManager.currentScene != null) {
                                    SceneManager.currentScene.clearKeyPressed();
                                }
                            }
                            break;
                        }

                        case 2: {
                            if (sceneManager == null) {
                                break;
                            }
                            int npcId = msg.reader().readInt();
                            if (npcId >= 0) {
                                break;
                            }

                            Npc npc = (Npc) sceneManager.getActor(npcId);
                            if (npc == null) {
                                break;
                            }

                            int guiNum = msg.reader().readInt();
                            npc.clearGui();
                            for (int i = 0; i < guiNum; i++) {
                                int guiID = msg.reader().readInt();
                                String guiCap = msg.reader().readUTF();
                                int messageNum = msg.reader().readInt();
                                String[] guidMessage = new String[messageNum];
                                for (int j = 0; j < messageNum; j++) {
                                    guidMessage[j] = msg.reader().readUTF();
                                }

                                String imageId1 = msg.reader().readUTF();
                                GuiderDialog d = new GuiderDialog(guiID, imageId1, guidMessage, npc.y - npc.frameH);

                                npc.addGuide(guiCap, d);
                            }
                            npc.showGuiMenu();
                            break;
                        }

                        case 3: {
                            int listID = msg.reader().readInt();

                            if (msg.reader().readInt() == 0) {
                                GameController.startErrorDlg(msg.reader().readUTF());
                            } else {
                                String title = msg.reader().readUTF();
                                message = msg.reader().readUTF();
                                int optionNum = msg.reader().readInt();
                                int[] optionId = new int[optionNum];
                                String[] optionText = new String[optionNum];
                                byte[] optionStatus = new byte[optionNum];
                                Command[] cmds = new Command[optionNum];
                                for (int i = 0; i < optionNum; i++) {
                                    optionId[i] = msg.reader().readInt();
                                    optionText[i] = msg.reader().readUTF();
                                    optionStatus[i] = msg.reader().readByte();
                                    if (optionStatus[i] != 0) {
                                        int[] param = new int[2];
                                        param[0] = listID;
                                        param[1] = optionId[i];
                                        cmds[i] = new Command(43, L.gL(419), param, GameController.instance);
                                    }
                                }

                                 GameController.showOptionDialog(
                                        listID, title, optionId, optionText, optionStatus, cmds).show(true);
                            }
                            break;
                        }
                        case 5: {
                            if (sceneManager == null) {
                                break;
                            }

                            int npcId = msg.reader().readInt();
                            if (npcId >= 0) {
                                break;
                            }

                            Npc npc = (Npc) sceneManager.getActor(npcId);
                            if (npc == null) {
                                break;
                            }

                            int optionNum = msg.reader().readInt();
                            int[] optionId = new int[optionNum];
                            String[] optionText = new String[optionNum];
                            for (int i = 0; i < optionNum; i++) {
                                optionId[i] = msg.reader().readInt();
                                optionText[i] = msg.reader().readUTF();
                            }

                            npc.showOptionPopup(optionText, optionId);
                            break;
                        }
                        case 6: { // hiển thị danh sách btnMenu top
                            int listID = msg.reader().readInt();

                            String title = msg.reader().readUTF();
                            //message = msg.reader().readUTF();
                            int itemNum = msg.reader().readInt();
                            Vector menuItemList = new Vector();
                            for (int i = 0; i < itemNum; i++) {
                                MenuItemInfo mnInfo = new MenuItemInfo(msg.reader().readInt(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readByte());
                                menuItemList.addElement(mnInfo);
                            }
                            
                            onMenuScreenDataReceived(listID, menuItemList, (byte)-1, title);
                            break;
                        }
                        case 8: {
                            int listID = msg.reader().readInt();
                            byte menuType = msg.reader().readByte();
                            String title = msg.reader().readUTF();
                            //message = msg.reader().readUTF();
                            int itemNum = msg.reader().readInt();
                            Vector menuItemList = new Vector();
                            for (int i = 0; i < itemNum; i++) {
                                MenuItemInfo mnInfo = new MenuItemInfo(msg.reader().readInt(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readByte());
                                menuItemList.addElement(mnInfo);

                                mnInfo.showDialog = msg.reader().readBoolean();
                                if (mnInfo.showDialog) {
                                    mnInfo.dialogText = msg.reader().readUTF();
                                    mnInfo.leftCmdText = msg.reader().readUTF();
                                    mnInfo.rightCmdText = msg.reader().readUTF();
                                }
                                mnInfo.saleStatus = msg.reader().readByte();
                                mnInfo.closeScreenAfterClick = msg.reader().readBoolean();

                                int moneyOptionNum = msg.reader().readInt();
                                mnInfo.paymentOptionsId = new int[moneyOptionNum];
                                mnInfo.moneyText = new String[moneyOptionNum];
                                mnInfo.isPaymentEnable = new byte[moneyOptionNum];
                                for (int j = 0; j < moneyOptionNum; j++) {
                                    mnInfo.paymentOptionsId[j] = msg.reader().readInt();
                                    mnInfo.moneyText[j] = msg.reader().readUTF();
                                    mnInfo.isPaymentEnable[j] = msg.reader().readByte();
                                }
                            }
                            
                            onMenuScreenDataReceived(listID, menuItemList, menuType, title);
                        }
                        break;
                        // shop avatar use this list
                        case 10: {
                            int listID = msg.reader().readInt();
                            byte menuType = msg.reader().readByte();
                            String title = msg.reader().readUTF();
                            //message = msg.reader().readUTF();
                            int itemNum = msg.reader().readInt();
                            Vector menuItemList = new Vector();
                            for (int i = 0; i < itemNum; i++) {
                                MenuItemInfo mnInfo = new MenuItemInfo(msg.reader().readInt(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readUTF(),
                                        msg.reader().readByte());
                                menuItemList.addElement(mnInfo);

                                mnInfo.showDialog = msg.reader().readBoolean();
                                if (mnInfo.showDialog) {
                                    mnInfo.dialogText = msg.reader().readUTF();
                                    mnInfo.leftCmdText = msg.reader().readUTF();
                                    mnInfo.rightCmdText = msg.reader().readUTF();
                                }
                                mnInfo.closeScreenAfterClick = msg.reader().readBoolean();
                            }
                            
                            onMenuScreenDataReceived(listID, menuItemList, menuType, title);
                        }
                        break;
                        case 11:
                            id = msg.reader().readInt();
                            int w = msg.reader().readInt();
                            int h = msg.reader().readInt();
                            String image = msg.reader().readUTF();
                            int frameNum = msg.reader().readInt();
                            int frameDelay = msg.reader().readInt();
                            ImageDialog d = new ImageDialog(id, image, w, h, frameNum, frameDelay);
                            d.show(false);
                            break;
                        case 7: // input -> confirm
                            int dialogId = msg.reader().readInt();
                            String dialogTitle = msg.reader().readUTF();
                            int countOption = msg.reader().readInt();
                            String[] optionText = new String[countOption];
                            byte[] optionTextType = new byte[countOption];
                            for (int i = countOption - 1; i >= 0; i--) {
                                optionText[i] = msg.reader().readUTF();
                                optionTextType[i] = msg.reader().readByte();
                            }
                            GameController.showOptionDialog(MGOProtocol.GUIDER, 7, dialogId, dialogTitle, optionText, optionTextType);
                            break;
                    }

                    break;
                case MGOProtocol.GET_INFO:
                    String info;
                    subId = msg.reader().readByte();
                    if (subId == -1) {
                        subId = msg.reader().readByte();
                        info = null;
                    } else {
                        info = msg.reader().readUTF();
                    }
                    globalLogicHandler.onGetInfo(subId, info);
                    break;

                case MGOProtocol.SERVER_MESSAGE:
                    //globalLogicHandler.onServerMessage(msg.reader().readUTF());
                    subId = msg.reader().readByte();
                    switch (subId) {
                        case 0:
                            String a1 = msg.reader().readUTF();
                            GameController.startOKDlg(a1, true);
                            break;
                        case 1:
                            String a2 = msg.reader().readUTF();
                            ((Screen) BaseCanvas.getCurrentScreen()).showBanner(a2);
                            break;
                        case 2:
                            info = msg.reader().readUTF();
                            String url = msg.reader().readUTF();
                            globalLogicHandler.onVersion(info, url);
                            break;
                        case 4:
                            final int qid = msg.reader().readInt();
                            String question = msg.reader().readUTF();
                            final Screen scr = (Screen) BaseCanvas.getCurrentScreen();

                            Command cmdYes = new Command(L.gL(580), new IActionListener() {

                                public void actionPerformed(Object o) {
                                    GlobalService.sendAnswer(qid, true);
                                    scr.hideDialog();
                                }
                            });

                            Command cmdNo = new Command(L.gL(300), new IActionListener() {

                                public void actionPerformed(Object o) {
                                    GlobalService.sendAnswer(qid, false);
                                    scr.hideDialog();
                                }
                            });

                            Dialog.showMessageDialog(question, cmdYes, null, cmdNo, true);

                            break;
                        case 5:
                            Screen scr2 = (Screen) BaseCanvas.getCurrentScreen();
                            if (scr2 == null) {
                                break;
                            }
                            message = msg.reader().readUTF();
                            Popup p = new Popup(message);
                            p.start();
                            Screen.overlays.addElement(p);
                            break;
                    }
                    break;
                case MGOProtocol.SET_GAME_TYPE:
                    int service = msg.reader().readInt();
                    // read money
                    msg.reader().readLong();
                    msg.reader().readLong();
                    msg.reader().readLong();
                    //#if DefaultConfiguration
//@		    System.out.print(  "\nSet Game Type = " + serviceID);
//@		    System.out.print(  "\nUpdate money :");
//@		    System.out.print(  "\n- Gold = " + GameController.myInfo.mGoGoldMoney);
//@//		    System.out.print(  "\n- mGo Money = " + GameController.myInfo.mGoVFarmMoney);
//@//		    System.out.print(  "\n- Game Money = " + GameController.myInfo.mGoGameMoney);
                    //#endif
                    globalLogicHandler.onSetGameTypeSuccess(service);
                    break;
                case MGOProtocol.BOARD_LIST:
                    Vector boardList = new Vector();
                    while (msg.reader().available() > 0) {
                        Board boardInfo = new Board();
                        boardInfo.id = msg.reader().readInt();
                        boardInfo.nPlayer = msg.reader().readInt();
                        boardInfo.isPass = msg.reader().readBoolean();
                        boardInfo.money = msg.reader().readInt();
                        boardInfo.strMoney = Util.formatNumber(boardInfo.money);
                        boardList.addElement(boardInfo);
                    }
                    GameController.showBoardListScreen(boardList);
                    break;
                case MGOProtocol.MESSAGE:
                    // public static final byte NOTICE_FRIEND = 1;
                    // public static final byte NOTICE_INBOX = 2; // thong bao chung chung
                    // public static final byte NOTICE_GIFT = 3;
                    // npublic static final byte NOTICE_ACTIVITY = 4;
                    if (MessageScreen.notifyShow == null) {
                        MessageScreen.notifyShow = new boolean[MessageScreen.TAB_NUM];
                    }
//                    subId = msg.reader().readByte();
                    IwinMessage m;
                    int length = msg.reader().readByte();
                    for (int i = 0; i < length; i++) {
                        m = new IwinMessage();
                        m.fromID = msg.reader().readInt();
                        m.fromName = msg.reader().readUTF();
                        m.message = msg.reader().readUTF();
                        m.type = msg.reader().readByte();
                        switch (m.type) {
                            case 0: // admin
                                if (!MessageScreen.notifyShow[1]) {
                                    MessageScreen.notifyShow[1] = true;
                                }
                                MessageScreen.listAdmin.addElement(m);
                                break;
                            case 2: // message from friend
                            case 3: // gift from friend
                                if (!MessageScreen.notifyShow[0]) {
                                    MessageScreen.notifyShow[0] = true;
                                }
                                MessageScreen.listFriendMessage.addElement(m);
                                break;
//                            case 1: // add friend request
//                            case 4: // farm activity 
//                            case 5: // suggest friend
                            default:
                                if (!MessageScreen.notifyShow[2]) {
                                    MessageScreen.notifyShow[2] = true;
                                }
                                MessageScreen.listNotification.addElement(m);
                                break;
                        }
                    }
                    if (length != 0) {
                        ((Screen) BaseCanvas.getCurrentScreen()).showBanner(L.gL(203));
                    }
                    break;

                case MGOProtocol.CHARGE_MONEY_INFO:
                    GameController.closeWaitDialog();
                    Vector mni = new Vector();
                    while (msg.reader().available() > 0) {
                        byte type = msg.reader().readByte();
                        ChargeMoneyInfo info1 = new ChargeMoneyInfo(type);
                        if (type == ChargeMoneyInfo.TYPE_CHARGE_2_PIN || type == ChargeMoneyInfo.TYPE_BANKING) {
                            info1.subId = msg.reader().readByte();
                        }
                        info1.info = msg.reader().readUTF();
                        info1.smsContent = msg.reader().readUTF();
                        info1.smsTo = msg.reader().readUTF();
                        mni.addElement(info1);
                    }
                    GameController.showMoneyScreen(mni);
                    break;
                case 21:
//                    processCreateCharater(msg);
                    byte cmdId = msg.reader().readByte();
                    if (cmdId == 0) {
                        int idImg1 = msg.reader().readInt();
                        int idImg2 = msg.reader().readInt();
                        globalLogicHandler.onAddUserInfor(idImg1, idImg2);
                    } else if (cmdId == 1) {
                        ((Screen) BaseCanvas.getCurrentScreen()).hideDialog();
                        GameController.startOKDlg(L.gL(491));
                    } else if (cmdId == 2) {
                        String str = msg.reader().readUTF();
                        globalLogicHandler.onAddNameUser(str);
//                        GameController.startErrorDlg(str, false);
                    }
                case MGOProtocol.COMMAND_IMAGE:
                    processImage(msg);
                    break;
                case MGOProtocol.MGO_COMMAND:
                    byte sub = msg.reader().readByte();
                    switch (sub) {
                        case 12: //
                            int num = msg.reader().readByte();
                            GameController.switchMapNames = new String[num];
                            GameController.switchMapShortNames = new String[num];
                            GameController.switchMapEntrances = new byte[num];
                            GameController.switchMapIds = new byte[num];

                            for (int i = 0; i < num; i++) {
                                GameController.switchMapIds[i] = msg.reader().readByte();
                                GameController.switchMapNames[i] = msg.reader().readUTF();
                                GameController.switchMapShortNames[i] = msg.reader().readUTF();
                                GameController.switchMapEntrances[i] = msg.reader().readByte();
                            }
                            break;
                    }
                    break;
                //<- command cua INVENTORY

                case MGOProtocol.VERSION:
                    globalLogicHandler.onVersion(msg.reader().readUTF(), msg.reader().readUTF());
                    break;
                case MGOProtocol.GAME_OBJECT:
                    while (msg.reader().available() > 0) {
                        byte gameObjectType = msg.reader().readByte();
                        switch (gameObjectType) {
                            case 0:
                                if (sceneManager == null) {
                                    break;
                                }

                                int x = msg.reader().readInt();
                                int y = msg.reader().readInt();
                                int w = msg.reader().readInt();
                                int h = msg.reader().readInt();

                                id = msg.reader().readInt();

//                                System.out.println(  "collision rect for npc " + id);
//                                System.out.println(x + " " + y + " " + w + " " + h);

                                String imageName = msg.reader().readUTF();
                                if (  "".equals(imageName)) {
                                    imageName = null;
                                }
                                
                                int wayNum = msg.reader().readInt();

                                waypoint = new int[wayNum];

                                for (int i = 0; i < wayNum; i++) {
                                    waypoint[i] = msg.reader().readInt();
                                }

                                int delay = msg.reader().readInt();
                                int textNum = msg.reader().readInt();

                                String[] texts = null;
                                if (textNum != 0) {
                                    texts = new String[textNum];
                                    for (int i = 0; i < textNum; i++) {
                                        texts[i] = msg.reader().readUTF();
                                    }
                                }


                                Npc npc = new Npc(id, sceneManager);
                                npc.setName(msg.reader().readUTF());
                                npc.setType(msg.reader().readByte());
                                npc.setCollisionRec(new Rectangle(x, y, w, h));
                                npc.imageId = imageName;

                                if (waypoint.length > 2) {
                                    npc.autoMove((byte) 0, waypoint);
                                    npc.frameNum = 10;
                                } else {
                                    npc.stand(-1);
                                }

                                if (texts != null) {
                                    npc.setTexts(texts, delay);
                                }

                                if (SceneManager.currentScene != null) {

                                    SceneManager.currentScene.addActor(npc, waypoint[0], waypoint[1], false);
                                    if (!npc.isHuman) {
                                        npc.useCollisionRectToDetectCollision = true;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                // Tat ca cac command khac vui long dat tren dong nay
                case MGOProtocol.COUPON_MONEY:
                    ((Screen) BaseCanvas.getCurrentScreen()).hideDialog();

//                    int subID = msg.reader().readByte();
                    int typeMsg = msg.reader().readByte();
                    String msgStr = msg.reader().readUTF();
                    GameController.startOKDlg(msgStr);
                    break;
                case MGOProtocol.SERVER_LIST:
                    int serverNum = msg.reader().readInt();
                    Vector list = new Vector();
                    for (int i = 0; i < serverNum; i++) {
                        Server server = new Server(
                                (msg.reader().readUTF()).trim(),
                                (msg.reader().readUTF()).trim(),
                                msg.reader().readInt(),
                                msg.reader().readInt(),
                                msg.reader().readInt());
                        list.addElement(server);
                    }
                    GlobalService.serverList = list;
//                    System.out.println(  "write server list to rms");
                  //#ifdef mGo
                   LoginScreen.saveServerListToRMS();
                  //#elif gopet2
                   //@ LoginScreen.saveServerListToRMS();
                  //#endif
                    Screen scr = BaseCanvas.getCurrentScreen();
                    if (scr instanceof LoginScreen) {
                        if (LoginScreen.needDisplayServerList) {
                            ((LoginScreen) scr).showServerListDialog();
                        }
                    }

//                    for (int i = 0; i < GlobalService.serverList.size(); i++) {
//                        Server server = (Server) GlobalService.serverList.elementAt(i);
//                        System.out.println(server.name);
//                    }
                    break;
                case MGOProtocol.QUEST:
                    byte type = msg.reader().readByte();
                    switch (type) {
                        case 1:
                            int QuestId = msg.reader().readInt();
                            String QuestName = msg.reader().readUTF();
                            String QuestDest = msg.reader().readUTF();
                            byte questType = msg.reader().readByte();
                            GameController.showQuestDialog(QuestId, QuestName, QuestDest, questType);
                            break;
                        default:
                    }
                    break;
                case 74:
                    // send sms
                    String syntax = msg.reader().readUTF();
                    String add = msg.reader().readUTF();
                    final String messageToShow = msg.reader().readUTF();
                    GlobalService.sendSMS(syntax, "sms://" + add, new IActionListener() {

                        public void actionPerformed(Object o) {
                            GameController.startOKDlg(messageToShow);
                        }
                    }, new IActionListener() {

                        public void actionPerformed(Object o) {
                        }
                    });
                    break;
                case 65:// in app purchase
                    //#if Ovi || mGo_BigScreen_Ovi
//@                     byte subComand = msg.reader().readByte();
//@ //                    System.out.println(  "sub command: " + subComand);                           
//@                     Vector productIDs = new Vector();
//@ //                    String str[] = new String[3];
//@                     while (msg.reader().available() > 0) {
//@                         String str = msg.reader().readUTF();
//@                         productIDs.addElement(str);                        
//@ //                        System.out.println(  "id product: " + str);
//@                     }
//@                 
//@ //                    System.out.println(  "vao day sub command: ");
//@                     MoneyScreen moneyScr = null;
//@                     if (  "MONEY".equals(((Screen) BaseCanvas.getCurrentScreen()).screenId)) {
//@                         moneyScr = (MoneyScreen) BaseCanvas.getCurrentScreen();
//@ //                        moneyScr.setProductIds(productIDs);
//@                         moneyScr.hideDialog();
//@                     }
//@                     else {
//@ //                        Screen.backScreens.push(BaseCanvas.getCurrentScreen());
//@                         moneyScr = new MoneyScreen();
//@                         moneyScr.setProductIds(productIDs);
//@ //                        moneyScr.switchToMe(1);
//@                     }
                    //#endif
                    break;
                case 83:
                	if (messagenotify != null) {
                		String url = msg.reader().readUTF();
                		messagenotify.onDownloadUrlReceive(url);
                	}
                	break;
                // dis me iwin    
                default:
                    if (gameMessageHandler != null) {
                        gameMessageHandler.onMessage(msg);
                    }
                    break;
            }
        } catch (Exception e) {
        }
    }
    

    private void readPlayerInfor(boolean readEntrance, Message msg) throws IOException {
        int id = msg.reader().readInt();
        String name = msg.reader().readUTF();
        byte gender = msg.reader().readByte();
        byte relation = msg.reader().readByte();

        byte speed = msg.reader().readByte();
        ///////////////// SUA CHO NAY LAI->
        byte faceDir = msg.reader().readByte();

        byte entranceIndex = -1;
        if (readEntrance) {
            entranceIndex = msg.reader().readByte();
        }

        int x = msg.reader().readInt();
        int y = msg.reader().readInt();

        if (entranceIndex >= 0 && SceneManager.currentScene != null) {
            Map map = SceneManager.currentScene.map;
            for (int i = 0; i < map.entranceList.length; i++) {
                if (map.entranceList[i].index == entranceIndex) {
                    x = map.entranceList[i].x;
                    y = map.entranceList[i].y;
                    break;
                }
            }
        }

        Actor a = sceneManager.onActorEnterMap(id, name, gender, relation, x, y, faceDir);
        if (a != null) {
            a.setSpeed(speed);
        }
        ///////////////// <-SUA CHO NAY LAI
    }

    private void processImage(Message msg) {
        try {
            byte game = msg.reader().readByte();
            byte imageType = msg.reader().readByte();
            switch (game) {
                case MGOProtocol.PROFILE_IMAGE:
                    processPrfileImage(msg, imageType);
                case 0:// mGO
                    if (imageType == 10 || imageType == 11) { // hình/ani của map object trong map động0
//                        processImageAndAnimationMapObject(msg, imageType);
                    } else {
                        String imgName = msg.reader().readUTF();
                        int len = msg.reader().readInt();
                        if (len != 0) {
                            byte[] data = new byte[len];
                            msg.reader().read(data);
                            Image itemImage = Image.createImage(data, 0, data.length);
                            if (imageType == 4) {
//                                GameResourceManager.addShopItemImage(Integer.parseInt(imgName), itemImage);
                            } else {
                                GameResourceManager.addGuiderImg(imgName, itemImage, imageType);
                            }
                        }
                    }
                    break;
            }
        } catch (IOException ex) {
//            ex.printStackTrace();
        }
    }

    public static byte[] readImageByteArray(Message msg) throws IOException {
        int size = msg.reader().readInt();
        if (size <= 0) {
            return null;
        }
        byte[] data = new byte[size];
        msg.reader().read(data);
        return data;
    }

    private void processPrfileImage(Message msg, byte imageType) {
        try {
            switch (imageType) {
                case 7:

                    int itemId = msg.reader().readInt();
                    int itemType = msg.reader().readByte();
                    int itemXOffset = msg.reader().readInt();
                    int itemYOffset = msg.reader().readInt();
                    int itemZIndex = msg.reader().readInt();
                    //
                    byte[] imageData = readImageByteArray(msg);
                    Image img = Image.createImage(imageData, 0, imageData.length);

                    AvatarItem item = sceneManager.getAvatarItem(itemId);
                    if (item == null) {
                        break;
                    }

                    if (!item.isCar() && !item.isPatin()) {
                        if (item.isUpper()) {
//                            if (img.getHeight() % 2 != 0) {
//                                break;
//                            }
                        } else if (item.type == 12) {
                            if (img.getHeight() % 2 != 0) {
                                break;
                            }
                        }
                    }
                    item.setImage(img);

                    break;
            }
        } catch (IOException ex) {
            //#ifndef mGo
//@            System.out.println(  "error" + ex.getMessage());
//@            ex.printStackTrace();
            //#endif
        }
    }

    private static void processMapData(Message m, int mapId) throws IOException {
        int mapDataLength = m.reader().readInt();
        byte[] mapData = new byte[mapDataLength];
        m.reader().read(mapData);
        Map.saveMapData(mapData, mapId);
    }

    public static void releaseResource() {
        SceneManager.clearResource();
        GlobalMessageHandler.getInstance().sceneManager = null;
        GlobalMessageHandler.getInstance().gameMessageHandler = null;
    }

    private boolean onMenuScreenDataReceived(int listID, Vector menuItemList, byte menuType, String title) {
        Screen current = BaseCanvas.getCurrentScreen();
        if (current instanceof MenuScreen) {
            MenuScreen menuScr = (MenuScreen) current;
            if (menuScr.menuID == listID) {
                menuScr.setMenuItemList(menuItemList);
                return true;
            }
        }
        MenuScreen menuScreen = new MenuScreen();
        menuScreen.setMenuId(listID);
        menuScreen.menuType = menuType;
        menuScreen.setMenuItemList(menuItemList);
        menuScreen.setTitle(title);
        menuScreen.switchToMe(1, true);
        return false;
    }
}


using Gopet.App;
using Gopet.Battle;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.Dialog;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySql.Data.MySqlClient;
using static MenuController;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Collections.Generic;
using System.Xml.Linq;
using Gopet.Data.dialog;
using Gopet.Data.Clan;

[NonController]
public class GameController
{

    private Player player;
    private PetBattle petBattle;
    private PetUpgradeInfo petUpgradeInfo;
    public HashMap<int, dynamic> objectPerformed = new();
    private long changePlaceDelay = Utilities.CurrentTimeMillis;
    private ClanMember _clanMember;


    public TaskCalculator taskCalculator { get; set; }


    public long lastTimeKillMob { get; set; } = 0;

    private long lastTimeTypeGiftCode = 0L;

    public long delayTimeHealPet = 0;

    public bool isBuffEnchent { get; private set; } = false;
    public bool IsBuffEnchantTatto { get; set; } = false;

    public bool isHasBattleAndShowDialog()
    {
        if (petBattle != null)
        {
            player.redDialog("Đang đánh nhau không thể thao tác");
            return true;
        }
        return false;
    }

    public bool canTypeGiftCode()
    {
        bool can = lastTimeTypeGiftCode < Utilities.CurrentTimeMillis;
        if (can)
        {
            lastTimeTypeGiftCode = Utilities.CurrentTimeMillis + 5000L;
            return true;
        }
        return false;
    }

    public ClanMember getClan()
    {
        if (player.playerData.clanId > 0)
        {
            if (_clanMember != null)
            {
                if (_clanMember.getClan().getClanId() != player.playerData.clanId)
                {
                    _clanMember = null;
                }
            }
            if (_clanMember == null)
            {
                Clan clan = ClanManager.clanHashMap.get(player.playerData.clanId);
                if (clan != null)
                {
                    _clanMember = clan.getMemberByUserId(player.user.user_id);
                    if (_clanMember == null)
                    {
                        player.playerData.clanId = -1;
                    }
                }
            }
            return _clanMember;
        }
        return null;
    }

    public PetUpgradeInfo getPetUpgradeInfo()
    {
        return petUpgradeInfo;
    }

    public void setPetUpgradeInfo(PetUpgradeInfo petUpgradeInfo)
    {
        this.petUpgradeInfo = petUpgradeInfo;
    }

    public PetBattle getPetBattle()
    {
        return petBattle;
    }

    public void setPetBattle(PetBattle petBattle)
    {
        this.petBattle = petBattle;
    }

    public GameController(Player player, ISession session_)
    {
        this.player = player;
    }

    public Player getPlayer()
    {
        return player;
    }

    public void setPlayer(Player player)
    {
        this.player = player;
    }

    public void LoadMap()
    {
        if (player.playerData != null)
        {
            player.playerData.waypointIndex = 0;
            MapManager.maps.get(11).addRandom(player);
        }
    }

    public int killMob = 0;

    public void randomCaptcha()
    {
        /*
        killMob++;
        if (killMob > GopetManager.MOB_NEED_CAPTCHA || Utilities.NextFloatPer() < 1f && player.playerData.captcha == null)
        {
            player.playerData.captcha = new GopetCaptcha();
            killMob = 0;
            showCaptchaDialog();
        }*/
    }

    public String captchaPath = "img/captcha.png";

    public void showCaptchaDialog()
    {
        GopetCaptcha captcha = player.playerData.captcha;
        if (captcha != null)
        {
            if (captcha.numShow >= GopetManager.MAX_TIMES_SHOW_CAPTCHA)
            {
                player.playerData.captcha = new GopetCaptcha();
                return;
            }
            captchaPath = "img/captcha.png" + Utilities.nextInt(0, 2000000000) + Utilities.nextInt(0, 2000000000) + Utilities.nextInt(0, 2000000000) + Utilities.nextInt(0, 2000000000) + Utilities.nextInt(0, 2000000000) + Utilities.nextInt(0, 2000000000);
            showImageDialog(MenuController.IMGDIALOG_CAPTCHA, 160, 80, captchaPath, 1, 0);
            captcha.numShow++;
        }
    }

    private long timeMove = Utilities.CurrentTimeMillis;
    private int hackMoveCounter = 0;
    public const long TIME_MOVE_SEND = 2000;

    public void onMessage(Message message)
    {

        switch (message.id)
        {
            case GopetCMD.ON_OTHER_USER_MOVE:
                {
                    int i1 = message.reader().readInt();
                    sbyte b1 = message.reader().readsbyte();
                    int b2 = message.reader().readInt();
                    int[] points = new int[message.reader().readInt()];
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i] = message.reader().readInt();
                    }
                    // System.out.println("server.GameController.onMessage() point  " + JsonManager.ToJson(points));
                    GopetPlace place = (GopetPlace)player.getPlace();
                    if (place != null)
                    {
                        player.playerData.x = points[points.Length - 2];
                        player.playerData.y = points[points.Length - 1];
                        //GopetManager.ServerMonitor.LogInfo(player.playerData.x + "|" + player.playerData.y + "|" + place.map.mapID);
                        place.sendMove(player.user.user_id, b1, points);
                    }

                    //                if (Math.abs(points[0] - points[points.Length - 2]) > 300) {
                    //                    hackMoveCounter++;
                    //                }
                    //
                    //                if (Math.abs(points[1] - points[points.Length - 1]) > 300) {
                    //                    hackMoveCounter++;
                    //                }
                    if (points.Length > 30 && !player.playerData.isAdmin)
                    {
                        hackMoveCounter++;
                    }

                    if (!(Utilities.CurrentTimeMillis - timeMove > TIME_MOVE_SEND) && !player.playerData.isAdmin)
                    {
                        hackMoveCounter++;
                    }
                    timeMove = Utilities.CurrentTimeMillis;

                    if (hackMoveCounter > 200)
                    {
                        //player.user.ban(UserData.BAN_TIME, "Hack speed", Utilities.CurrentTimeMillis + (1000l * 60 * 60 * 24));
                        player.session.Close();
                    }
                }
                break;
            case GopetCMD.ON_PLACE_CHAT:
                {
                    GopetPlace place = (GopetPlace)player.getPlace();
                    if (place != null)
                    {
                        String text = message.reader().readUTF();
                        switch (text)
                        {
                            case "kiss":
                                {
                                    place.petInteract(GopetCMD.ON_PET_INTERACT_KISS, player.playerData.user_id);
                                    return;
                                }
                            case "play":
                                {
                                    place.petInteract(GopetCMD.ON_PET_INTERACT_PLAY, player.playerData.user_id);
                                    return;
                                }
                            case "poke":
                                {
                                    place.petInteract(GopetCMD.ON_PET_INTERACT_POKE, player.playerData.user_id);
                                    return;
                                }
                            default:
                                break;
                        }
                        place.chat(player, text);
                        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Chat khu vực nội dung là :%s", text)));
                    }
                }
                break;
            case GopetCMD.ON_PLAYER_WARPING:
                {
                    if (getPetBattle() == null)
                    {
                        int mapId = message.reader().readInt();
                        if (!CheckSky(mapId))
                        {
                            return;
                        }


                        if (player.getPet()?.TimeDieZ > Utilities.CurrentTimeMillis)
                        {
                            player.redDialog($"Bạn đã kiệt sức vui lòng không rời khỏi vùng an toàn !!! Còn {Utilities.FormatNumber(((player.getPet().TimeDieZ - Utilities.CurrentTimeMillis) / 1000))} giây nữa là hồi phục!");
                            return;
                        }

                        int index = message.reader().readInt();
                        int mapVersion = message.reader().readInt();
                        player.playerData.waypointIndex = (sbyte)index;
                        player.playerData.x = player.playerData.y = 360;
                        MapManager.maps.get(mapId).addRandom(player);
                    }
                    else
                    {
                        player.redDialog("Đang đánh không thể rời");
                    }
                }
                break;
            case GopetCMD.ON_PLAYER_GET_CHANNEL_INFO:
                getChannelInfo();
                break;
            case GopetCMD.ON_PLAYER_CHANGE_CHANNEL:
                int[] changeInfo = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    changeInfo[i] = message.reader().readInt();
                }
                changeChannel(changeInfo);
                break;
            case GopetCMD.MGO_COMMAND:
                sbyte sub = message.reader().readsbyte();
                switch (sub)
                {
                    case GopetCMD.TELE_MENU:
                        mapTeleMenu();
                        break;
                }
                break;
            case GopetCMD.PET_SERVICE:
                processPet(message.reader().readsbyte(), message);
                break;
            case GopetCMD.CHANGE_NEW_PASSWORD:
                sbyte subCmd = message.reader().readsbyte();
                sbyte subCmd2 = message.reader().readsbyte();
                if (subCmd != 2 && subCmd2 != 7)
                {
                    throw new UnsupportedOperationException();
                }
                else
                {
                    requestChangePass(player.user.user_id, message.reader().readUTF(), message.reader().readUTF());
                }
                break;
            case GopetCMD.COMMAND_IMAGE:
                requestImg(message.reader().readsbyte(), message.reader().readsbyte(), message.reader().readUTF());
                break;
            case GopetCMD.COMMAND_GUIDER:
                guider(message.reader().readsbyte(), message);
                break;
            case GopetCMD.CREATE_CHAR:
                onClienSendCharInfo(message.reader().readUTF(), message.reader().readsbyte());
                break;
            case GopetCMD.SERVER_MESSAGE:
                {
                    sbyte subCmdServerMsg = message.reader().readsbyte();
                    serverMessage(subCmdServerMsg, message);
                    break;
                }
            case GopetCMD.CHARGE_MONEY_INFO:
                requestBank();
                break;
            case GopetCMD.PLAYER_CHALLENGE:
                inviteChallenge(message.readInt());
                break;

        }
        message.Close();
    }

    public void magic(int user_id, bool isMyPet)
    {

        if (isHasBattleAndShowDialog())
        {
            return;
        }

        Pet pet = null;
        if (isMyPet)
        {
            pet = player.getPet();
        }
        else
        {
            Player otherPlayer = PlayerManager.get(user_id);
            if (otherPlayer != null)
            {
                pet = otherPlayer.getPet();
            }
        }
        if (pet != null)
        {
            Message message = new Message(GopetCMD.PET_SERVICE);
            message.putsbyte(GopetCMD.MAGIC);
            message.putInt(user_id);
            message.putInt(pet.getPetIdTemplate());
            message.putsbyte(pet.getPetTemplate().element);
            message.putUTF(pet.getPetTemplate().frameImg);
            message.putUTF(pet.getNameWithStar());
            message.putsbyte(pet.getPetTemplate().nclass);
            message.putInt(pet.lvl);
            message.putlong(pet.exp);
            if (GopetManager.PetExp.ContainsKey(pet.lvl))
            {
                message.putlong(GopetManager.PetExp.get(pet.lvl));
            }
            else
            {
                message.putlong(long.MaxValue);
            }
            message.putlong(0);
            message.putInt(pet.getStr());
            message.putInt(pet.getAgi());
            message.putInt(pet.getInt());
            message.putInt(pet.getAtk());
            message.putInt(pet.getDef());
            message.putInt(pet.hp);
            message.putInt(pet.mp);
            message.putInt(pet.maxHp);
            message.putInt(pet.maxMp);
            message.putsbyte(pet.skill.Length);
            for (int i = 0; i < pet.skill.Length; i++)
            {
                int skillId = pet.skill[i][0];
                int skilllvl = pet.skill[i][1];
                PetSkill petSkill = GopetManager.PETSKILL_HASH_MAP.get(skillId);
                PetSkillLv petSkillLv = petSkill.skillLv.get(skilllvl);
                message.putInt(skillId);
                message.putUTF(petSkill.name + " " + skilllvl);
                message.putUTF(petSkill.getDescription(petSkillLv));
                message.putInt(petSkillLv.mpLost);
            }
            message.putInt(pet.tiemnang_point);
            CopyOnWriteArrayList<PetTatto> petTattos = (CopyOnWriteArrayList<PetTatto>)pet.tatto.clone();
            message.putInt(petTattos.Count);

            foreach (PetTatto petTatto in petTattos)
            {
                message.putInt(1);
                message.putUTF(petTatto.getName());
                message.putsbyte(1);
                message.putUTF("");
            }

            message.cleanup();
            player.session.sendMessage(message);
            HistoryManager.addHistory(new History(player).setLog("Xem magic của pet " + pet.getNameWithoutStar()).setObj(pet));
        }
        else
        {
            player.petNotFollow();
        }
    }

    private void serverMessage(sbyte subtype, Message message)
    {
        switch (subtype)
        {
            case GopetCMD.SEND_YES_NO:
                MenuController.answerYesNo(message.reader().readInt(), message.reader().readbool(), player);
                break;
        }
    }

    private void onClienSendCharInfo(String name, sbyte gender)
    {
        if (player.playerData == null)
        {
            if (!Utilities.CheckString(name, "^[a-z0-9]+$")
                    || (name.Length > 20 || name.Length < 5))
            {
                player.redDialog(
                        "Tên nhân vật phải có số lượng kí tự lớn hơn 5 và bé hơn 20 cũng như không chứa các kí tự đặc biệt");
                player.loginOK();
                return;
            }
            using (var conn = MYSQLManager.create())
            {
                var playerData = conn.QueryFirstOrDefault("select user_id from player where name = @name", new { name = name });
                if (playerData != null)
                {
                    player.redDialog("Tên nhân vật đã tồn tại");
                    Thread.Sleep(1000);
                    player.session.Close();
                    return;
                }

            }
            PlayerData.create(player.user.user_id, name, gender);
            player.login(player.user.username, player.user.password, "");
        }
    }

    private void guider(sbyte subCMD, Message message)
    {
        switch (subCMD)
        {
            case GopetCMD.GUIDER_IMGDIALOG:
                MenuController.selectImgDialog(message.readInt(), player);
                break;
            case GopetCMD.NPC_GUIDER:
                int npcId = message.reader().readInt();
                GopetPlace place = (GopetPlace)player.getPlace();
                foreach (int npcIdTemp in place.map.mapTemplate.npc)
                {
                    if (npcIdTemp == npcId)
                    {
                        getTaskCalculator().onMeetNpc(npcId);
                        MenuController.showNpcOption(npcId, player);
                        break;
                    }
                }
                break;
            case GopetCMD.SELECT_OPTION:
                {
                    selectOption(message.reader().readInt(), message.reader().readInt());
                }
                break;

            case GopetCMD.SELECT_MENU_ELEMENT:
                {
                    int listId = message.reader().readInt();
                    MenuController.selectMenu(listId, message.reader().readInt(), 0, player);
                    break;
                }
            case GopetCMD.GUIDER_TYPE_PAY:
                int menuId = message.readInt();
                switch (message.readsbyte())
                {
                    case 2:
                        int menuElementIndex = message.readInt();
                        int paymentIndex = message.readInt();
                        MenuController.selectMenu(menuId, menuElementIndex, paymentIndex, player);
                        break;
                }
                break;
            case GopetCMD.TYPE_DIALOG_INPUT:
                int dialogInputId = message.readInt();
                String[] texts = new String[message.readInt()];
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i] = message.readUTF();
                }
                try
                {
                    MenuController.inputDialog(dialogInputId, new InputReader(MenuController.getTypeInput(dialogInputId), texts), player);
                }
                catch (Exception e)
                {
                    player.redDialog("Nhập sai");
                    e.printStackTrace();
                }
                break;
        }
    }

    private void selectOption(int npcId, int option)
    {
        GopetPlace gopetPlace = (GopetPlace)player.getPlace();
        if (gopetPlace != null)
        {
            foreach (int npcIdTemp in gopetPlace.map.mapTemplate.npc)
            {
                if (npcIdTemp == npcId)
                {
                    MenuController.selectNpcOption(option, player);
                    break;
                }
            }
        }
    }

    public void showMenuItem(int listID, sbyte type, String title, JArrayList<MenuItemInfo> menuItemInfos)
    {
        Message message = new Message(GopetCMD.COMMAND_GUIDER);
        message.putsbyte(GopetCMD.SHOW_MENU_ITEM);
        message.putInt(listID);
        message.putsbyte(type);
        message.putUTF(title);
        message.putInt(menuItemInfos.Count);
        for (int i = 0; i < menuItemInfos.Count; i++)
        {
            MenuItemInfo menuItemInfo = menuItemInfos.get(i);
            if (menuItemInfo.isHasId())
            {
                message.putInt(menuItemInfo.getItemId());
            }
            else
            {
                message.putInt(i);
            }
            message.putUTF(menuItemInfo.getImgPath());
            message.putUTF(menuItemInfo.getTitleMenu());
            message.putUTF(menuItemInfo.getDesc());
            message.putsbyte(menuItemInfo.isCanSelect() ? 1 : 0);
            message.putbool(menuItemInfo.isShowDialog());
            if (menuItemInfo.isShowDialog())
            {
                message.putUTF(menuItemInfo.getDialogText());
                message.putUTF(menuItemInfo.getLeftCmdText());
                message.putUTF(menuItemInfo.getRightCmdText());
            }
            message.putsbyte(menuItemInfo.getSaleStatus());
            message.putbool(menuItemInfo.isCloseScreenAfterClick());
            MenuItemInfo.PaymentOption[] paymentOptions = menuItemInfo.getPaymentOptions();
            message.putInt(paymentOptions.Length);
            for (int j = 0; j < paymentOptions.Length; j++)
            {
                MenuItemInfo.PaymentOption paymentOption = paymentOptions[j];
                message.putInt(paymentOption.getPaymentOptionsId());
                message.putUTF(paymentOption.getMoneyText());
                message.putsbyte(paymentOption.getIsPaymentEnable());
            }
        }
        message.cleanup();
        player.session.sendMessage(message);
    }

    private void requestImg(sbyte gameType, sbyte type, String path)
    {
#if DEBUG
        GopetManager.ServerMonitor.LogWarning($"CLIENT REQUEST IMAGE: {path}");
#endif
        String originPath = path;
        if (path.Equals(GopetManager.EMPTY_IMG_PATH))
        {
            return;
        }

        if (!PlatformHelper.hasAssets(path))
        {
            try
            {
                int idAsset = int.Parse(path);
                path = GopetManager.itemAssetsIcon[idAsset];
            }
            catch (Exception e)
            {

            }
        }

        switch (gameType)
        {
            case 0:
                if (type != 10 && type != 11)
                {
                    if (path.Length == 0)
                    {
                        return;
                    }
                    try
                    {
                        if (PlatformHelper.hasAssets(path) || path.Equals(captchaPath))
                        {
                            sbyte[] buffer = null;
                            if (path.Equals(captchaPath))
                            {
                                if (player.playerData.captcha != null)
                                {
                                    buffer = player.playerData.captcha.getBufferImg();
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                buffer = PlatformHelper.loadAssets(path);
                            }

                            Message ms = new Message(GopetCMD.COMMAND_IMAGE);
                            ms.putsbyte(gameType);
                            ms.putsbyte(type);
                            ms.putUTF(originPath);
                            ms.putInt(buffer.Length);
                            ms.writer().write(buffer);
                            ms.cleanup();
                            player.session.sendMessage(ms);
                        }
                    }
                    catch (Exception e)
                    {

                        e.printStackTrace();
                    }
                }
                break;
        }
    }

    public void requestChangePass(int id, String oldPass, String newPass)
    {
        player.requestChangePass(id, oldPass, newPass);
    }

    private void processPet(sbyte subCmd, Message message)
    {
#if DEBUG
        GopetManager.ServerMonitor.LogInfo($" MESSAGE SERVICE: {subCmd}");
#endif
        switch (subCmd)
        {
            case GopetCMD.CHAT_PUBLIC:
                {
                    String name = message.reader().readUTF();
                    String text = message.reader().readUTF();

                    Place p = player.getPlace();
                    if (p != null)
                    {
                        p.chat(player.user.user_id, name, text);
                    }
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Chat khu vực nội dung : \'%s\' và người nhận là \'%s\'", text, name)));
                    break;
                }
            case GopetCMD.PET_INVENTORY:
                requestPetInventory();
                break;
            case GopetCMD.SHOW_GEM_INVENTORY:
                showGemInvenstory();
                break;
            case GopetCMD.REQUEST_PET_IMG:
                {
                    requestPetImg(message.reader().readsbyte(), message.reader().readUTF());
                    break;
                }
            case GopetCMD.ATTACK_MOB:
                {
                    if (player.playerData.captcha != null)
                    {
                        showCaptchaDialog();
                        return;
                    }
                    attackMob(message.reader().readInt());
                    break;
                }
            case GopetCMD.PET_BATTLE:
                {
                    PetBattle petBattle = getPetBattle();
                    if (petBattle != null)
                    {
                        petBattle.onMessage(message, player);
                    }
                    break;
                }
            case GopetCMD.PET_RECOVERY_HP:
                setRecovery(message.readsbyte() == 1);
                break;
            case GopetCMD.MAGIC:
                magic(player.user.user_id, true);
                break;
            case GopetCMD.MAGIC_LEARN_SKILL:
                int skillId = message.readInt();
                learnSkill(skillId);
                break;
            case GopetCMD.GYM:
                gym();
                break;
            case GopetCMD.UP_TIEM_NANG:
                upTiemNang(message.readInt(), message.readsbyte());
                break;
            case GopetCMD.EQUIP_INFO:
                equipInfo(player.user.user_id);
                break;
            case GopetCMD.GET_PLAYER_INFO:
                getInfo(message.readsbyte(), message.readInt());
                break;
            case GopetCMD.TATTOO:
                tatto(message.readsbyte(), message);
                break;
            case GopetCMD.USE_EQUIP_ITEM:
                useEquipItem(message.readInt());
                break;
            case GopetCMD.REQUEST_SHOP:
                requestShop(message.readsbyte());
                break;
            case GopetCMD.UNEQUIP_ITEM:
                unEquipItem(message.readInt());
                break;
            case GopetCMD.CLAN:
                clan(message.readsbyte(), message);
                break;
            case GopetCMD.SKIN_INVENTORY:
                MenuController.sendMenu(MenuController.MENU_SKIN_INVENTORY, player);
                break;
            case GopetCMD.WING:
                {
                    sbyte type = message.readsbyte();
                    switch (type)
                    {
                        case GopetCMD.WING_TYPE_INVENTORY:
                            MenuController.sendMenu(MenuController.MENU_WING_INVENTORY, player);
                            break;
                        case GopetCMD.WING_TYPE_ENCHANT:
                            {
                                int index = message.readInt();
                                if (index == -1)
                                {
                                    player.redDialog("Vui lòng tháo cánh ra");
                                    return;
                                }
                                var wingInventory = player.playerData[GopetManager.WING_INVENTORY];
                                if (wingInventory.Count > 0 && wingInventory.Count > index && index >= -1)
                                {
                                    this.objectPerformed[MenuController.OBJKEY_INDEX_WING_WANT_ENCHANT] = index;
                                    MenuController.sendMenu(MENU_SELECT_MONEY_TO_PAY_FOR_ENCHANT_WING, player);
                                }
                                else
                                {
                                    player.redDialog("Xảy ra lỗi ở nâng cấp cánh");
                                }
                            }
                            break;
                        case GopetCMD.WING_TYPE_USE:
                            MenuController.selectMenu(MenuController.MENU_WING_INVENTORY, message.readInt(), 0, player);
                            MenuController.sendMenu(MenuController.MENU_WING_INVENTORY, player);
                            break;
                        case GopetCMD.WING_TYPE_UNEQUIP:
                            {
                                Pet p = player.getPet();
                                GopetPlace place_Lc = (GopetPlace)player.getPlace();
                                Item it = player.playerData.wing;
                                if (it != null)
                                {
                                    player.playerData.wing = null;
                                    player.addItemToInventory(it);
                                    place_Lc.sendUnEquipWing(player);
                                    if (p != null)
                                    {
                                        p.applyInfo(player);
                                    }
                                    player.okDialog("Thao tác thành công");
                                    MenuController.sendMenu(MenuController.MENU_WING_INVENTORY, player);
                                    HistoryManager.addHistory(new History(player).setLog("Tháo cánh " + it.getName()).setObj(it));
                                }
                                else
                                {
                                    player.redDialog("Hiện tại bạn không có mang bất kỳ cánh nào!");
                                }
                            }
                            break;
                    }
                }
                break;
            case GopetCMD.REMOVE_ITEM_EQUIP:
                confirmRemoveItemEquip(message.readInt());
                break;
            case GopetCMD.SELECT_PET_UPGRADE:
                if (petUpgradeInfo != null)
                {
                    selectPet(message.readsbyte());
                }
                break;
            case GopetCMD.REQUEST_SHOP_SKIN:
                MenuController.showShop(MenuController.SHOP_SKIN, player);
                break;
            case GopetCMD.SELECT_METERIAL_ENCHANT:
                selectMaterialEnchantItem(message.readInt(), message.readInt());
                break;
            case GopetCMD.ENCHANT_ITEM:
                confirmEnchantItem(message.readInt(), message.readInt(), message.readInt(), false);
                break;
            case GopetCMD.NORMAL_INVENTORY:
                MenuController.showInventory(player, GopetManager.NORMAL_INVENTORY, MenuController.MENU_NORMAL_INVENTORY, "Rương đồ");
                break;
            case GopetCMD.UP_TIER_ITEM:
                upTierItem(message.readInt(), message.readInt(), false);
                break;
            case GopetCMD.INFO_UP_TIER_PET:
                showInfoPetUpTier(message.readInt(), message.readInt());
                break;
            case GopetCMD.PRICE_UPGRADE_PET:
                setPricePetUpgrade(int.MaxValue, GopetManager.PRICE_UP_TIER_PET);
                break;
            case GopetCMD.PET_UP_TIER:
                petUpTier(message.readInt(), message.readInt(), message.readUTF(), message.readsbyte());
                break;
            case GopetCMD.SELECT_KIOSK_ITEM:
                selectKioskItem(message.readsbyte());
                break;
            case GopetCMD.REMOVE_SELL_ITEM:
                removeSellItem(message.readInt());
                break;
            case GopetCMD.PLAYER_PK:
                pk(message.readInt());
                break;
            case GopetCMD.GEM_INVENTORY:
                selectGem(message.readInt());
                break;
            case GopetCMD.SELECT_GEM_ENCHANT:
                selectItemEnchantGem(message.readInt());
                break;
            case GopetCMD.REMOVE_GEM_ITEM:
                askRemoveGemItem(message.readInt());
                break;
            case GopetCMD.ENCHANT_GEM_ITEM:
                confirmEnchantItem(message.readInt(), message.readInt(), message.readInt(), true);
                break;
            case GopetCMD.SELECT_GEM_UP_TIER:
                MenuController.sendMenu(MenuController.MENU_SELECT_GEM_UP_TIER, player);
                break;
            case GopetCMD.UP_TIER_GEM_ITEM:
                upTierItem(message.readInt(), message.readInt(), true);
                break;
            case GopetCMD.PET_UNEQUIP_GEM_ITEM_INFO:
                unequipGem(message.readInt());
                break;
            case GopetCMD.ON_UNQUIP_GEM:
                onUnEquipGem(message.readInt());
                break;
            case GopetCMD.FAST_UNQUIP_GEM:
                fastUnequipGem(message.readInt());
                break;
            case GopetCMD.SHOW_LIST_TASK:
                showListTask();
                break;
            case GopetCMD.INVITE_MATCH:
                inviteMatch(message.readInt());
                break;
            case GopetCMD.SHOW_TATTO_PET_IN_KIOSK:
                {
                    int IdMenuItem = message.readInt();
                    var kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_PET);
                    var sellItems = kiosk.kioskItems.Where(p => p.itemId == IdMenuItem);
                    if (sellItems.Any())
                    {
                        var sellItem = sellItems.First();
                        showPetTattoUI(sellItem.pet);
                    }
                    else
                    {
                        player.redDialog("Pet đã được bán hoặc người bán đã hủy kí gửi");
                    }
                }
                break;
        }
    }

    private void showInfoPetUpTier(int petId1, int petId2)
    {
        Pet petActive = selectPetByItemId(petId1);
        Pet petPassive = selectPetByItemId(petId2);
        if (petActive.Expire != null || petPassive.Expire != null)
        {
            showDescPetUpTierUI("Không thể tiến hóa với pet dùng thử", null);
            return;
        }
        PetTier petTier = GopetManager.petTier.get(petActive.petIdTemplate);
        if (petTier == null || petTier.petTemplateIdNeed != petPassive.Template.petId)
        {
            showDescPetUpTierUI("2 pet này không thể kết hợp tiến hóa", null);
        }
        else
        {
            int gym_add = 0;
            int gym_up_level = 0;
            if (petActive.star + petPassive.star >= 10)
            {
                gym_up_level += 5;
            }
            else if (petActive.star + petPassive.star >= 8)
            {
                gym_up_level += 4;
            }
            else
            {
                gym_up_level += 3;
            }
            gym_add += Utilities.round((petActive.lvl + petPassive.lvl) / 2);
            Pet oldPet = petActive;
            petActive = new Pet(petTier.getPetTemplateId2());
            petActive._int = oldPet._int + 10;
            petActive.agi = oldPet.agi + 10;
            petActive.str = oldPet.str + 10;
            petActive.tiemnang_point = gym_add;
            petActive.pointTiemNangLvl = gym_up_level;
            showDescPetUpTierUI(petActive.Template.name, new string[] { petActive.Template.name, $"{petActive.str}(str) {petActive.agi}(agi) {petActive._int}(int)", $"Điểm tiềm năng {petActive.pointTiemNangLvl}" });
        }
    }


    public void showDescPetUpTierUI(string text, string[] line_desc)
    {
        if (line_desc == null)
            line_desc = new string[] { text };

        if (line_desc.Length >= sbyte.MaxValue) throw new UnsupportedOperationException("Chi duoc dung 7bit thoi");

        Message m = messagePetSerive(GopetCMD.INFO_UP_TIER_PET);
        m.putUTF(text);
        m.putsbyte(line_desc.Length);
        for (int i = 0; i < line_desc.Length; i++)
        {
            m.putUTF(line_desc[i]);
        }
        player.session.sendMessage(m);
    }


    public Item findWingItemWantEnchant()
    {
        int indexWing = objectPerformed[OBJKEY_INDEX_WING_WANT_ENCHANT];
        Item wingItem = null;
        if (indexWing == -1) wingItem = player.playerData.wing;
        else
        {
            var wingInventory = player.playerData[GopetManager.WING_INVENTORY];
            if (indexWing <= 0 && wingInventory.Count > indexWing)
            {
                wingItem = wingInventory[indexWing];
            }
        }

        return wingItem;
    }

    private void learnSkill(int skillId)
    {
        if (isHasBattleAndShowDialog())
        {
            return;
        }
        player.skillId_learn = skillId;
        MenuController.sendMenu(MenuController.MENU_LEARN_NEW_SKILL, player);
    }

    private void attackMob(int mobId)
    {
        GopetPlace gopetPlace = (GopetPlace)player.getPlace();
        if (gopetPlace != null)
        {
            gopetPlace.startFightMob(mobId, player);
        }
    }

    public void sendMyPetInfo()
    {

        if (player.playerData.petSelected != null)
        {
            Message message = new Message(GopetCMD.PET_SERVICE);
            message.putsbyte(GopetCMD.MY_PET_INFO);
            //hp
            message.putInt(player.playerData.petSelected.hp);
            //mp
            message.putInt(player.playerData.petSelected.mp);
            //max Hp
            message.putInt(player.playerData.petSelected.maxHp);
            //max Mp
            message.putInt(player.playerData.petSelected.maxMp);

            message.cleanup();
            player.session.sendMessage(message);
        }
    }

    private void requestPetImg(sbyte type, String path)
    {
#if DEBUG
        GopetManager.ServerMonitor.LogWarning($"CLIENT REQUEST IMAGE: {type} {path}");
#endif
        String originPath = path;
        if (path.Equals(GopetManager.EMPTY_IMG_PATH))
        {
            return;
        }
        if (!PlatformHelper.hasAssets(path))
        {
            try
            {
                int idAsset = int.Parse(path);
                path = GopetManager.itemAssetsIcon[idAsset];
            }
            catch (Exception e)
            {
            }
        }
        // System.err.println("requestPetImg: " + path + "|" + type);
        switch (type)
        {
            case 1:
                {
                    if (PlatformHelper.hasAssets(path) || path.Equals(captchaPath))
                    {
                        sbyte[] buffer = null;
                        if (path.Equals(captchaPath))
                        {
                            if (player.playerData.captcha != null)
                            {
                                buffer = player.playerData.captcha.getBufferImg();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            buffer = PlatformHelper.loadAssets(path);
                        }
                        Message message = new Message(GopetCMD.PET_SERVICE);
                        message.putsbyte(GopetCMD.REQUEST_PET_IMG);
                        message.putsbyte(type);
                        message.putUTF(originPath);
                        message.putInt(buffer.Length);
                        message.writer().write(buffer);
                        message.cleanup();
                        player.session.sendMessage(message);
                    }
                    break;
                }
            case 2:
                if (PlatformHelper.hasAssets(path) || path.Equals(captchaPath))
                {
                    sbyte[] buffer = null;
                    if (path.Equals(captchaPath))
                    {
                        if (player.playerData.captcha != null)
                        {
                            buffer = player.playerData.captcha.getBufferImg();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        buffer = PlatformHelper.loadAssets(path);
                    }
                    Message message = new Message(GopetCMD.PET_SERVICE);
                    message.putsbyte(GopetCMD.REQUEST_PET_IMG);
                    message.putsbyte(type);
                    message.putUTF(originPath);
                    message.putInt(buffer.Length);
                    message.writer().write(buffer);
                    message.cleanup();
                    player.session.sendMessage(message);
                }
                break;
        }
    }

    public void createChar()
    {
        if (isHasBattleAndShowDialog() || player.playerData != null)
        {
            return;
        }
        Message ms = new Message(GopetCMD.CREATE_CHAR);
        ms.putsbyte(0);
        ms.putInt(0);
        ms.putInt(0);
        ms.writer().flush();
        ms.cleanup();
        player.session.sendMessage(ms);
    }

    public void requestPetInventory()
    {
        MenuController.sendMenu(MenuController.MENU_PET_INVENTORY, player);
        HistoryManager.addHistory(new History(player).setLog("Mở hành trang pet").setObj(player.playerData.pets));
    }

    private void mapTeleMenu()
    {
        Message ms = new Message(GopetCMD.MGO_COMMAND);
        ms.putsbyte(GopetCMD.TELE_MENU);
        ms.putsbyte((sbyte)GopetManager.TeleMapId.Length);
        for (int i = 0; i < GopetManager.TeleMapId.Length; i++)
        {
            int j = GopetManager.TeleMapId[i];
            GopetMap mapData = MapManager.maps.get(j);
            ms.putsbyte((sbyte)j);
            ms.putUTF(mapData.mapTemplate.name);
            ms.putUTF(mapData.mapTemplate.name);
            ms.putsbyte(0);
        }
        ms.writer().flush();
        ms.cleanup();
        player.session.sendMessage(ms);
        HistoryManager.addHistory(new History(player).setLog("Lấy danh sách map có thể dịch chuyển được"));
    }

    private void changeChannel(int[] info)
    {
        if (!(player.getPlace() is ClanPlace))
        {
            if (getPetBattle() == null)
            {
                if (changePlaceDelay < Utilities.CurrentTimeMillis)
                {
                    int mapId = info[0];
                    if (!CheckSky(mapId))
                    {
                        return;
                    }
                    int placeId = info[1];
                    if (MapManager.maps.get(mapId) != null)
                    {
                        GopetMap mapData = MapManager.maps.get(mapId);
                        if (!mapData.CanChangeZone)
                        {
                            player.Popup("Không thể đổi khu");
                            return;
                        }
                        if (placeId < mapData.numPlace && placeId >= 0)
                        {
                            mapData.places.get(placeId).add(player);
                        }
                        else
                        {
                            player.Popup("Không tồn tại khu này");
                        }
                    }
                    changePlaceDelay = Utilities.CurrentTimeMillis + GopetManager.CHANGE_CHANNEL_DELAY;
                }
                else
                {
                    int second = Utilities.round((changePlaceDelay - Utilities.CurrentTimeMillis) / 1000);
                    player.redDialog(Utilities.Format("Vui lòng chờ %s giây nữa", second));
                }
            }
            else
            {
                player.redDialog("Không cho phép đánh chuyển khu cảm ơn");
            }
        }
        else
        {
            player.redDialog("Không thể đổi khu");
        }
    }

    private void getChannelInfo()
    {
        Place p = player.getPlace();
        if (p != null)
        {
            GopetMap mapData = p.map;
            Message message = new Message(GopetCMD.ON_PLAYER_GET_CHANNEL_INFO);
            foreach (Place place in mapData.places)
            {
                message.putInt(place.zoneID);
                message.putInt(place.players.Count);
                message.putbool(false);
                message.putInt(0);
            }
            message.cleanup();
            player.session.sendMessage(message);
            HistoryManager.addHistory(new History(player).setLog("Lấy danh sách khu vực"));
        }
    }

    public void loginOK()
    {
        if (player.playerData != null)
        {
            daily();
            Message ms = new Message(GopetCMD.LOGIN_SUCCES);
            ms.putInt(player.user.user_id);
            ms.putString(player.playerData.name);
            ms.putString(player.playerData.name);
            ms.putString(Utilities.ServerIP());
            ms.putInt(Main.PORT_SERVER);
            ms.writer().flush();
            player.session.sendMessage(ms);
            HistoryManager.addHistory(new History(player).setLog("Đăng nhập vào trò chơi và tải nhân vật thành công"));
            checkBugEquipItem();
            showExp();
        }
    }

    private void daily()
    {

        var serverDate = Utilities.GetCurrentDate();
        if (player.playerData.loginDate.Day != serverDate.Day || player.playerData.loginDate.Month != serverDate.Month || player.playerData.loginDate.Year != serverDate.Year)
        {
            this.player.playerData.numUseEnergy.Clear();
            this.player.playerData.star = GopetManager.DAILY_STAR;
        }
        player.playerData.loginDate = DateTime.Now;
        HistoryManager.addHistory(new History(player).setLog("Vào game và kiểm tra có phải qua ngày mới nếu qua ngày mới thì nhận 20 năng lượng và năng lượng hiện tại là:" + player.playerData.star).setObj(player.playerData));
    }

    public void updateUserInfo()
    {
        if (player.playerData == null)
        {
            return;
        }
        Message message = messagePetSerive(GopetCMD.STAR_INFO);
        //star
        message.putInt(player.playerData.star);
        message.cleanup();
        player.session.sendMessage(message);

        message = messagePetSerive(GopetCMD.MONEY_INFO);
        //star
        message.putInt(player.playerData.star);
        message.putlong(player.playerData.gold);
        message.putlong(player.playerData.coin);
        message.cleanup();
        player.session.sendMessage(message);
    }

    public void updatePetSelected(bool isRemove)
    {
        if (!isRemove)
        {
            GopetPlace place = (GopetPlace)player.getPlace();
            if (place != null)
            {
                place.sendListPet(player);
            }
        }
    }

    private void setRecovery(bool b)
    {
        if (b)
        {
            if (player.playerData.petSelected != null)
            {
                if (player.controller.getPetBattle() == null)
                {
                    player.isPetRecovery = true;
                }
            }
        }
        else
        {
            player.isPetRecovery = b;
        }

        HistoryManager.addHistory(new History(player).setLog("Bật/Tắt hồi máu cho pet đi theo"));
    }

    public void updatePetLvl()
    {

        Pet myPet = player.getPet();

        if (GopetManager.PetExp.ContainsKey(myPet.lvl))
        {
            int expUp = GopetManager.PetExp.get(myPet.lvl);
            if (myPet.exp >= expUp)
            {
                myPet.exp -= expUp;
                myPet.lvlUP();
                Message message = new Message(GopetCMD.PET_SERVICE);
                message.putsbyte(GopetCMD.UPDATE_PET_LVL);
                //old version
                message.putInt(0);
                message.putInt(0);
                //old version

                message.putInt(myPet.lvl);
                message.cleanup();
                player.session.sendMessage(message);

                this.taskCalculator.onPetUpLevel(myPet);
            }
        }
    }

    private void gym()
    {
        if (isHasBattleAndShowDialog())
        {
            return;
        }
        Pet pet = player.getPet();
        if (pet != null)
        {
            Message message = messagePetSerive(GopetCMD.GYM);
            message.putInt(pet.getPetIdTemplate());
            message.putUTF(pet.getPetTemplate().frameImg);
            message.putUTF(pet.getNameWithStar());
            message.putsbyte(pet.getNClassIcon());
            message.putInt(pet.lvl);
            message.putlong(pet.exp);
            if (GopetManager.PetExp.ContainsKey(pet.lvl))
            {
                message.putlong(GopetManager.PetExp.get(pet.lvl));
            }
            else
            {
                message.putlong(long.MaxValue);
            }
            message.putlong(0);
            message.putInt(pet.getStr());
            message.putInt(pet.getAgi());
            message.putInt(pet.getInt());
            message.putsbyte(pet.tiemnang_point);
            for (int i = 0; i < 3; i++)
            {
                message.putInt(0);
                message.putInt(0);
                message.putsbyte(0);
                message.putUTF("");
                message.putsbyte(1);
            }
            message.cleanup();
            player.session.sendMessage(message);
            HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Xem gym pet %s", pet.getNameWithoutStar())).setObj(pet));
        }
        else
        {
            player.petNotFollow();
        }
    }

    private void upTiemNang(int num, sbyte index)
    {
        if (isHasBattleAndShowDialog())
        {
            return;
        }
        if (index >= 0 && index < MenuController.gym_options.Length)
        {
            Pet pet = player.getPet();
            if (pet == null)
            {
                player.petNotFollow();
            }
            else
            {
                if (pet.tiemnang_point > 0)
                {
                    pet.tiemnang_point--;
                    pet.tiemnang[index]++;
                    pet.applyInfo(player);
                    updateTiemnang();
                    getTaskCalculator().onPlusGymPoint();
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cộng tìm năng cho pet %s [num =%s,index=%s]", pet.getNameWithoutStar(), num, index)).setObj(pet));
                }
                else
                {
                    player.redDialog("Pet không có đủ tiềm năng");
                }
            }
        }
    }

    private void updateTiemnang()
    {
        Pet pet = player.getPet();
        if (pet != null)
        {
            Message message = messagePetSerive(GopetCMD.UP_TIEM_NANG);
            message.putInt(pet.getPetIdTemplate());
            message.putInt(pet.getStr());
            message.putInt(pet.getAgi());
            message.putInt(pet.getInt());
            for (int i = 0; i < 3; i++)
            {
                message.putInt(0);
                message.putInt(0);
                message.putsbyte(0);
                message.putUTF("");
                message.putsbyte(1);
            }
            message.cleanup();
            player.session.sendMessage(message);
        }
    }

    private void equipInfo(int user_id)
    {
        if (isHasBattleAndShowDialog())
        {
            return;
        }
        Player oPlayer = PlayerManager.get(user_id);
        if (oPlayer != null)
        {
            Pet pet = oPlayer.getPet();
            if (oPlayer == player)
            {
                if (pet != null)
                {
                    pet.applyInfo(player);
                }
            }
            if (pet != null)
            {
                Message message = messagePetSerive(GopetCMD.EQUIP_INFO);
                message.putInt(user_id);
                message.putInt(pet.petId);
                message.putUTF(pet.getPetTemplate().frameImg);
                message.putUTF(pet.getNameWithStar());
                message.putInt(pet.lvl);
                message.putInt(pet.getStr());
                message.putInt(pet.getAgi());
                message.putInt(pet.getInt());
                CopyOnWriteArrayList<Item> petEquipItem = oPlayer.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY);
                writeListItemEquip(petEquipItem, message, false);
                message.cleanup();
                player.session.sendMessage(message);
                if (oPlayer != player)
                {
                    oPlayer.Popup(Utilities.Format("Người chơi %s đang xem trang bị của thú cưng bạn", player.playerData.name));
                }
                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Xem trang bị pet của người chơi %s", oPlayer.playerData.name)).setObj(oPlayer.playerData));
            }
            else
            {
                player.petNotFollow();
            }
        }
        else
        {
            player.redDialog("Người chơi đã offline");
        }
    }

    private void writeListItemEquip(CopyOnWriteArrayList<Item> petEquipItem, Message message, bool isReSend)
    {
        message.putInt(petEquipItem.Count);
        for (int i = 0; i < petEquipItem.Count; i++)
        {
            Item item = petEquipItem.get(i);
            writeItemEquip(item, message, isReSend);
        }
    }

    private void writeItemEquip(Item item, Message message, bool isReSend)
    {
        ItemTemplate template = item.getTemp();
        message.putInt(item.itemId);
        message.putUTF(template.getFrameImgPath());
        message.putUTF("???");
        message.putUTF(item.getEquipName());
        message.putInt(template.getType());
        message.putInt(item.petEuipId);
        for (int j = 0; j < 11; j++)
        {
            message.putInt(j + 1);
        }
        message.putsbyte(0);
        message.putsbyte(item.lvl);
        if (!isReSend)
        {
            bool hasGem = item.gemInfo != null;
            message.putbool(hasGem);
            if (hasGem)
            {
                message.putlong(item.gemInfo.timeUnequip);
                message.putInt(Utilities.round((item.gemInfo.timeUnequip - Utilities.CurrentTimeMillis) / 1000L));
            }
        }
        else
        {
            bool hasGem = item.gemInfo != null;
            message.putbool(hasGem);
            if (hasGem)
            {
                message.putlong(item.gemInfo.timeUnequip);
                message.putInt(Utilities.round((item.gemInfo.timeUnequip - Utilities.CurrentTimeMillis) / 1000L));
            }
            else
            {
                message.putlong(-1);
                message.putInt(-1);
            }
        }
    }

    public void resendPetEquipInfo(Item item)
    {
        Message m = messagePetSerive(GopetCMD.ON_UNQUIP_GEM);
        writeItemEquip(item, m, true);
        m.cleanup();
        player.session.sendMessage(m);
    }

    private void getInfo(sbyte type, int user_id)
    {
        switch (type)
        {
            case GopetCMD.GET_PET_PLAYER_INFO:
                magic(user_id, false);
                break;
            case GopetCMD.GET_PET_EQUIP_PLAYER_INFO:
                equipInfo(user_id);
                break;
        }
    }

    public void notEnoughCoin()
    {
        player.redDialog("Không đủ (ngoc)");
    }

    public void notEnoughGold()
    {
        player.redDialog("Không đủ (vang)");
    }

    public void notEnoughSilverBar()
    {
        player.redDialog("Không đủ thỏi bạc");
    }

    public void notEnoughGoldBar()
    {
        player.redDialog("Không đủ thỏi vàng");
    }

    public void notEnoughBloodGem()
    {
        player.redDialog("Không đủ huyết ngọc");
    }

    public void notEnoughCrystal ()
    {
        player.redDialog("Không đủ tinh thạch");
    }

    public bool checkGoldBar(int count)
    {
        return checkCount(GopetManager.GOLD_BAR_ID, count, GopetManager.MONEY_INVENTORY);
    }

    public bool checkSilverBar(int count)
    {
        return checkCount(GopetManager.SILVER_BAR_ID, count, GopetManager.MONEY_INVENTORY);
    }

    public bool checkBloodGem(int count)
    {
        return checkCount(GopetManager.BLOOD_GEM_ID, count, GopetManager.MONEY_INVENTORY);
    }

    public bool checkCrystal (int count)
    {
        return checkCount(GopetManager.CRYSTAL_ID, count, GopetManager.MONEY_INVENTORY);
    }

    public bool checkCount(int tempId, int count, sbyte inventory)
    {
        Item itemSelect = selectItemsbytemp(tempId, inventory);
        return checkCountItem(itemSelect, count);
    }
    public bool checkCountItem(Item itemSelect, int count)
    {
        if (itemSelect != null)
        {
            return itemSelect.count >= count;
        }
        else
        {
            return false;
        }
    }

    public void addGoldBar(int gold)
    {
        Item item = new Item(GopetManager.GOLD_BAR_ID);
        item.count = gold;
        player.addItemToInventory(item, GopetManager.MONEY_INVENTORY);
    }

    public void addSilverBar(int silver)
    {
        Item item = new Item(GopetManager.SILVER_BAR_ID);
        item.count = silver;
        player.addItemToInventory(item, GopetManager.MONEY_INVENTORY);
    }

    public void addBloodGem(int blood)
    {
        Item item = new Item(GopetManager.BLOOD_GEM_ID);
        item.count = blood;
        player.addItemToInventory(item, GopetManager.MONEY_INVENTORY);
    }

    public void addCrystal(int crystal)
    {
        Item item = new Item(GopetManager.CRYSTAL_ID);
        item.count = crystal;
        player.addItemToInventory(item, GopetManager.MONEY_INVENTORY);
    }

    public void mineGoldBar(int gold)
    {
        subCountItem(selectItemsbytemp(GopetManager.GOLD_BAR_ID, GopetManager.MONEY_INVENTORY), gold, GopetManager.MONEY_INVENTORY);
    }

    public void mineSilverBar(int silver)
    {
        subCountItem(selectItemsbytemp(GopetManager.SILVER_BAR_ID, GopetManager.MONEY_INVENTORY), silver, GopetManager.MONEY_INVENTORY);
    }

    public void mineBloodGem(int blood)
    {
        subCountItem(selectItemsbytemp(GopetManager.BLOOD_GEM_ID, GopetManager.MONEY_INVENTORY), blood, GopetManager.MONEY_INVENTORY);
    }

    public void mineCrystal(int crystal)
    {
        subCountItem(selectItemsbytemp(GopetManager.CRYSTAL_ID, GopetManager.MONEY_INVENTORY), crystal, GopetManager.MONEY_INVENTORY);
    }

    private void tatto(sbyte type, Message message)
    {
        // System.out.println("server.GameController.tatto() " + type);
        Pet pet = player.getPet();
        if (pet != null)
        {
            switch (type)
            {
                case GopetCMD.TATTOO_INIT_SCREEN:
                    showPetTattoUI();
                    break;
                case GopetCMD.SELECT_ITEM_GEM_TATTO:
                    {
                        MenuController.sendMenu(MenuController.MENU_SELECT_ITEM_GEN_TATTO, player);
                    }
                    break;
                case GopetCMD.SELECT_ITEM_REMOVE_TATOO:
                    {
                        objectPerformed.put(MenuController.OBJKEY_TATTO_ID_REMOVE, message.readInt());
                        MenuController.sendMenu(MenuController.MENU_SELECT_ITEM_REMOVE_TATTO, player);
                    }
                    break;
                case GopetCMD.TATTOO_ENCHANT_SELECT_MATERIAL:
                    {
                        selectTattoMaterialToEnchant(message.readsbyte());
                    }
                    break;
                case GopetCMD.TATTOO_ENCHANT:
                    {
                        sendConfirmEnchantTatto(message.readInt(), message.readInt(), message.readInt());
                    }
                    break;
            }
        }
        else
        {
            player.petNotFollow();
        }
    }


    public void enchantTatto()
    {

        if (!objectPerformed.ContainsKeyZ(OBJKEY_ID_TATTO_TO_ENCHANT, OBJKEY_ID_MATERIAL1_TATTO_TO_ENCHANT, OBJKEY_ID_MATERIAL2_TATTO_TO_ENCHANT, OBJKEY_TYPE_PRICE_TATTO_TO_ENCHANT))
        {
            player.redDialog("Thao tác quá nhanh!!!");
            return;
        }

        Pet p = player.getPet();
        if (p != null)
        {
            PetTatto first = objectPerformed[OBJKEY_ID_TATTO_TO_ENCHANT];
            int typeMoney = objectPerformed[OBJKEY_TYPE_PRICE_TATTO_TO_ENCHANT];
            Item item1 = selectItemByItemId(objectPerformed[OBJKEY_ID_MATERIAL1_TATTO_TO_ENCHANT], GopetManager.NORMAL_INVENTORY);
            Item item2 = selectItemByItemId(objectPerformed[OBJKEY_ID_MATERIAL2_TATTO_TO_ENCHANT], GopetManager.NORMAL_INVENTORY);
            if (item1 != null && item2 != null)
            {
                if (!checkMoney((sbyte)typeMoney, typeMoney == 1 ? GopetManager.PRICE_COIN_ENCHANT_TATTO : GopetManager.PRICE_GOLD_ENCHANT_TATTO, player))
                {
                    NotEngouhMoney((sbyte)typeMoney, typeMoney == 1 ? GopetManager.PRICE_COIN_ENCHANT_TATTO : GopetManager.PRICE_GOLD_ENCHANT_TATTO, player);
                    return;
                }

                if (!(checkCount(item1, 1) && checkCount(item2, 1)))
                {
                    player.redDialog("Không đủ nguyên liệu");
                    return;
                }
                if (checkType(GopetManager.ITEM_MATERIAL_ENCHANT_TATOO, item1) && checkType(GopetManager.MATERIAL_ENCHANT_ITEM, item2))
                {
                    if (first.lvl < 10)
                    {
                        if (typeMoney == 1) player.mineCoin(GopetManager.PRICE_COIN_ENCHANT_TATTO);
                        else player.mineGold(GopetManager.PRICE_GOLD_ENCHANT_TATTO);

                        subCountItem(item1, 1, GopetManager.NORMAL_INVENTORY);
                        subCountItem(item2, 1, GopetManager.NORMAL_INVENTORY);

                        bool isSucces = Utilities.NextFloatPer() < GopetManager.PERCENT_OF_ENCHANT_TATOO[first.lvl] || IsBuffEnchantTatto;

                        if (isSucces)
                        {
                            first.lvl++;
                            p.applyInfo(player);
                            showPetTattoUI();
                            player.okDialog("Cường hóa thành công");
                        }
                        else
                        {
                            int numDrop = GopetManager.NUM_LVL_DROP_ENCHANT_TATTO_FAILED[first.lvl];
                            first.lvl -= numDrop;
                            p.applyInfo(player);
                            showPetTattoUI();
                            player.redDialog($"Cường hóa thất bại bị giảm {numDrop} cấp!");
                        }
                    }
                    else
                    {
                        player.redDialog("Xăm đã đạt cấp tối đa");
                    }
                }
                else
                {
                    InvailIitemType();
                }
            }
            else
            {
                player.redDialog("Thao tác quá nhanh");
            }
        }
        else
        {
            player.petNotFollow();
        }
    }

    private void sendConfirmEnchantTatto(int tattotId, int itemId1, int itemId2)
    {
        Pet p = player.getPet();
        if (p != null)
        {
            var tatooList = p.tatto.Where(p => p.tattoId == tattotId);
            if (tatooList.Any())
            {
                PetTatto first = tatooList.First();

                Item item1 = selectItemByItemId(itemId1, GopetManager.NORMAL_INVENTORY);
                Item item2 = selectItemByItemId(itemId2, GopetManager.NORMAL_INVENTORY);
                if (item1 != null && item2 != null)
                {
                    if (checkType(GopetManager.ITEM_MATERIAL_ENCHANT_TATOO, item1) && checkType(GopetManager.MATERIAL_ENCHANT_ITEM, item2))
                    {
                        if (first.lvl < 10)
                        {
                            objectPerformed[OBJKEY_ID_TATTO_TO_ENCHANT] = first;
                            objectPerformed[OBJKEY_ID_MATERIAL1_TATTO_TO_ENCHANT] = itemId1;
                            objectPerformed[OBJKEY_ID_MATERIAL2_TATTO_TO_ENCHANT] = itemId2;
                            sendMenu(MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO, player);
                        }
                        else
                        {
                            player.redDialog("Xăm đã đạt cấp tối đa");
                        }
                    }
                    else
                    {
                        InvailIitemType();
                    }
                }
                else
                {
                    player.redDialog("Thao tác quá nhanh");
                }
            }
            else
            {
                player.redDialog("Tính bug xăm?");
            }
        }
        else
        {
            player.petNotFollow();
        }
    }


    public void sendItemSelectTattoMaterialToEnchant(int id, string icon, string name)
    {
        Message m = messagePetSerive(GopetCMD.TATTOO);
        m.putsbyte(7);
        m.putInt(id);
        m.putUTF(icon);
        m.putUTF(name);
        player.session.sendMessage(m);
    }

    public void selectTattoMaterialToEnchant(sbyte type)
    {
        switch (type)
        {
            case GopetCMD.TATTOO_ENCHANT_SELECT_MATERIAL1:
                {
                    sendMenu(MENU_SELECT_MATERIAL1_TO_ENCHANT_TATOO, player);
                    break;
                }
            case GopetCMD.TATTOO_ENCHANT_SELECT_MATERIAL2:
                {
                    sendMenu(MENU_SELECT_MATERIAL2_TO_ENCHANT_TATOO, player);
                    break;
                }
        }
    }

    private void useEquipItem(int itemId)
    {
        if (isHasBattleAndShowDialog())
        {
            return;
        }
        Pet pet = player.getPet();
        if (pet != null)
        {
            CopyOnWriteArrayList<Item> inventory = player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY);
            Item item = selectItemEquipByItemId(itemId);
            if (Pet.canEuip(item))
            {
                if (pet.getAgi() >= item.getTemp().getRequireAgi() && pet.getInt() >= item.getTemp().getRequireInt() && pet.getStr() >= item.getTemp().getRequireStr())
                {
                    if (item.gemInfo != null && pet.Template.element != GopetManager.DARK_ELEMENT && pet.Template.element != GopetManager.LIGHT_ELEMENT)
                    {
                        ItemGem itemGem = item.gemInfo;

                        ItemTemplate itemTemplate = GopetManager.itemTemplate[itemGem.itemTemplateId];

                        if (itemTemplate.element != GopetManager.DARK_ELEMENT && itemTemplate.element != GopetManager.LIGHT_ELEMENT)
                        {
                            if (pet.Template.element != itemTemplate.element)
                            {
                                player.redDialog($"Chỉ có pet hệ {GopetManager.GetElementDisplay(itemTemplate.element)} mới mang được");
                                return;
                            }
                        }
                    }


                    if (item.petEuipId > 0)
                    {
                        Pet searchPet = selectPetByItemId(item.petEuipId);
                        if (searchPet == null)
                        {
                            item.petEuipId = -1;
                        }
                    }
                    if (item.petEuipId <= 0)
                    {
                        foreach (var next in pet.equip.ToArray())
                        {

                            Item it = selectItemEquipByItemId(next);
                            if (it == null)
                            {
                                pet.equip.remove(next);
                                continue;
                            }
                            if (it.getTemp().getType() == item.getTemp().getType())
                            {
                                it.petEuipId = -1;
                                pet.equip.remove(next);
                                break;
                            }
                        }
                        pet.equip.add(itemId);
                        item.petEuipId = pet.petId;
                        pet.applyInfo(player);
                        Message message = messagePetSerive(GopetCMD.USE_EQUIP_ITEM);
                        message.putsbyte(1);
                        message.putInt(itemId);
                        message.cleanup();
                        player.session.sendMessage(message);
                        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Mặc trang bị %s cho pet", item.getName())).setObj(item));

                    }
                    else
                    {
                        player.redDialog("Vật phẩm này đã trang bị cho pet rồi!");
                    }
                }
                else
                {
                    player.redDialog("Không đủ chỉ số.\n" + Utilities.Format("Cần %s(str) %s(agi) và %s(int)", item.getTemp().getRequireStr(), item.getTemp().getRequireAgi(), item.getTemp().getRequireInt()));
                }

            }
            else
            {
                player.redDialog("Vật phẩm này không phải trang bị");
            }

        }
        else
        {
            player.petNotFollow();
        }
    }

    public static Message messagePetSerive(sbyte subCmd)
    {
        Message message = new Message(GopetCMD.PET_SERVICE);
        message.putsbyte(subCmd);
        return message;
    }

    private void requestShop(sbyte shopId)
    {
        HistoryManager.addHistory(new History(player).setLog("Bấm hiển thĩ cửa hàng id là " + shopId));
        switch (shopId)
        {
            case MenuController.SHOP_ARMOUR:
            case MenuController.SHOP_SKIN:
            case MenuController.SHOP_WEAPON:
            case MenuController.SHOP_HAT:
            case MenuController.SHOP_FOOD:
                MenuController.sendMenu(shopId, player);
                break;
            default:
                player.redDialog("Cửa hàng này đang được xây dựng");
                break;
        }
    }

    private void requestBank()
    {

    }

    private void onVersion(String readUTF, String readUTF0)
    {

    }

    private void unEquipItem(int itemId)
    {
        if (isHasBattleAndShowDialog())
        {
            return;
        }
        Pet pet = player.getPet();
        if (pet != null)
        {
            CopyOnWriteArrayList<Item> inventory = player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY);
            Item item = selectItemEquipByItemId(itemId);
            if (Pet.canEuip(item))
            {
                if (item.petEuipId <= 0)
                {
                    player.redDialog("Vật phẩm này chưa có pet nào đeo");
                }
                else
                {
                    item.petEuipId = -1;
                    pet.equip.remove((Object)item.itemId);
                    pet.applyInfo(player);
                    Message message = messagePetSerive(GopetCMD.UNEQUIP_ITEM);
                    message.putsbyte(1);
                    message.putInt(itemId);
                    message.cleanup();
                    player.session.sendMessage(message);
                    resendPetEquipInfo(item);
                    HistoryManager.addHistory(new History(player).setLog("Tháo vật phẩm " + item.getName()).setObj(item));
                }
            }
            else
            {
                player.redDialog("Vật phẩm này không phải trang bị");
            }
        }
        else
        {
            player.petNotFollow();
        }
    }

    public void sendPlaceTime(int time)
    {
        Message message = messagePetSerive(GopetCMD.TIME_PLACE);
        message.putInt(time);
        message.cleanup();
        player.session.sendMessage(message);
    }

    public void showBigTextEff(String text)
    {
        Message message = messagePetSerive(GopetCMD.SHOW_BIG_TEXT_EFF);
        message.putUTF(text);
        message.cleanup();
        player.session.sendMessage(message);
    }

    public static Message clanMessage(sbyte subCmd)
    {
        Message m = messagePetSerive(GopetCMD.CLAN);
        m.putsbyte(subCmd);
        return m;
    }

    public void clan(sbyte subCMD, Message message)
    {
#if DEBUG
        GopetManager.ServerMonitor.LogWarning("Bang hội MSG: " + subCMD);
#endif
        switch (subCMD)
        {
            case GopetCMD.CLAN_INFO:
                clanInfo();
                break;
            case GopetCMD.DONATE_CLAN:
                donateClan();
                break;
            case GopetCMD.CLAN_INFO_MEMBER:
                clanInfoMember(message.readsbyte(), message.reader().readbool());
                break;
            case GopetCMD.SEARCH_GUILD:
                searchClan(message.readUTF());
                break;
            case GopetCMD.PLAYER_DONATE_CLAN:
                donateClan(message.readInt());
                break;

            case GopetCMD.GUILD_TOP_FUND:
                showTopFund();
                break;

            case GopetCMD.GUILD_TOP_GROWTH_POINT:
                showTopFund();
                break;
            case GopetCMD.GUILD_REQUEST_JOIN:
                requestJoinClanById(message.readInt());
                break;
            case GopetCMD.GUILD_CHAT:
                showGuidChat();
                break;
            case GopetCMD.GUILD_PLAYER_CHAT:
                playerChatInClan(message.readInt(), message.readUTF());
                break;
            case GopetCMD.GUILD_SHOW_OHTER_PLAYER_CLAN_SKILL:
                showSkillClan(message.readInt());
                break;
            case GopetCMD.GUILD_CLAN_UNLOCK_SKILL:
                unlockSlotSkillClan();
                break;
            case GopetCMD.GUILD_CLAN_RENT_SKILL:
                rentSkill(message.readInt());
                break;
            case GopetCMD.GUILD_KICK_MEMBER:
                kickClanMem(message.readInt());
                break;
        }
    }

    private void clanInfo()
    {
        ClanMember clanMember = getClan();
        if (clanMember != null)
        {
            Clan clan = clanMember.getClan();
            JArrayList<String> listInfoClan = new();
            listInfoClan.add("Tên bang hội: " + clan.name);
            listInfoClan.add("Cấp: " + clan.lvl);
            listInfoClan.add("Bang chủ: " + clan.getMemberByUserId(clan.getLeaderId()).name);
            listInfoClan.add(Utilities.Format("Thành viên: %s/%s", clan.curMember, clan.maxMember));
            listInfoClan.add("Khẩu hiệu: " + clan.slogan);
            listInfoClan.add(Utilities.Format("Quỹ: %s", Utilities.FormatNumber(clan.getFund())));
            Message message = clanMessage(GopetCMD.CLAN_INFO);
            message.putInt(clan.getClanId());
            message.putsbyte(listInfoClan.Count);
            for (int i = 0; i < listInfoClan.Count; i++)
            {
                message.putUTF(listInfoClan.get(i));
            }
            message.cleanup();
            player.session.sendMessage(message);
        }
        else
        {
            showListClan();
        }
    }

    private void donateClan()
    {
        ClanMember clanMember = getClan();
        if (clanMember != null)
        {
            CopyOnWriteArrayList<ClanMemberDonateInfo> donateInfos = GopetManager.clanMemberDonateInfos;
            Message message = clanMessage(GopetCMD.DONATE_CLAN);
            message.putInt(donateInfos.Count);
            for (int i = 0; i < donateInfos.Count; i++)
            {
                ClanMemberDonateInfo get = donateInfos.get(i);
                message.putInt(i);
                message.putUTF("Mốc " + (i + 1) + Utilities.Format(": Quyên góp %s để nhận %s điểm cống hiến", MenuController.getMoneyText(get.getPriceType(), get.getPrice()), Utilities.FormatNumber(get.getFund())));
            }
            message.cleanup();
            player.session.sendMessage(message);
        }
        else
        {
            showListClan();
        }
    }

    private void donateClan(int menuId)
    {
        ClanMember clanMember = getClan();
        if (clanMember != null)
        {
            CopyOnWriteArrayList<ClanMemberDonateInfo> donateInfos = GopetManager.clanMemberDonateInfos;
            if (menuId >= 0 && menuId < donateInfos.Count)
            {
                ClanMemberDonateInfo clanMemberDonateInfo = donateInfos.get(menuId);
                if (MenuController.checkMoney(clanMemberDonateInfo.getPriceType(), clanMemberDonateInfo.getPrice(), player))
                {
                    MenuController.addMoney(clanMemberDonateInfo.getPriceType(), -(int)clanMemberDonateInfo.getPrice(), player);
                    clanMember.getClan().addFund(clanMemberDonateInfo.getFund(), clanMember);
                    player.okDialog("Quyên góp thành công");
                }
                else
                {
                    player.redDialog("Không đủ tiền");
                }
            }
            else
            {
                player.redDialog("Đã có lỗi");
            }
        }
        else
        {
            showListClan();
        }
    }

    private void clanInfoMember(sbyte page, bool adsfgasf)
    {
        ClanMember clanMember = getClan();
        if (clanMember != null)
        {
            Clan clan = clanMember.getClan();
            Message m = clanMessage(GopetCMD.CLAN_INFO_MEMBER);
            m.putInt(clan.getClanId());
            m.putbool(clanMember.duty == Clan.TYPE_DEPUTY_LEADER || clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_SENIOR);
            m.putInt(0);
            m.putUTF("Thông tin bang chúng");
            m.putInt(0);
            m.putInt(0);
            m.putsbyte(0);
            m.putsbyte(0);
            m.putsbyte(clan.getMembers().Count);
            foreach (ClanMember member in clan.getMembers())
            {
                m.putInt(member.user_id);
                m.putUTF(member.getAvatar());
                m.putUTF(member.name + Utilities.Format(" (Chức vụ: %s)", member.getDutyName()));
                m.putUTF(Utilities.Format("Đóng góp quỹ: %s", Utilities.FormatNumber(member.fundDonate)));
            }
            m.putbool(false);
            player.session.sendMessage(m);
        }
        else
        {
            showListClan();
        }
    }

    public void showUpgradePet()
    {
        Message message = messagePetSerive(GopetCMD.SHOW_UPGRADE_PET);
        message.cleanup();
        player.session.sendMessage(message);
    }

    private void confirmUpgradePet()
    {

    }

    public void removeItemEquip(int itemId)
    {
        Item item = selectItemEquipByItemId(itemId);
        if (item != null)
        {
            if (item.petEuipId >= 0)
            {
                player.redDialog("Tháo trang bị mới hủy vật phẩm này được");
            }
            else
            {
                player.playerData.removeItem(GopetManager.EQUIP_PET_INVENTORY, item);
                Message message = messagePetSerive(GopetCMD.REMOVE_ITEM_EQUIP);
                message.putInt(itemId);
                message.cleanup();
                player.session.sendMessage(message);
                HistoryManager.addHistory(new History(player).setLog("Hủy vật phẩm " + item.getTemp().getName()).setObj(item));
            }
        }
    }

    private void confirmRemoveItemEquip(int itemid)
    {
        Item item = selectItemByItemId(itemid, GopetManager.EQUIP_PET_INVENTORY);
        if (item != null)
        {
            if (Pet.canEuip(item))
            {
                if (item.petEuipId < 0)
                {
                    objectPerformed.put(MenuController.OBJKEY_REMOVE_ITEM_EQUIP, itemid);
                    MenuController.showYNDialog(MenuController.DIALOG_CONFIRM_REMOVE_ITEM_EQUIP, Utilities.Format("Bạn có chắc muốn hủy vật phẩm", item.getTemp().getName()), player);
                }
                else
                {
                    player.redDialog("Vật phẩm này đã trang bị cho pet rồi");
                }
            }
            else
            {
                player.redDialog("Vật phẩm này không phải trang bị");
            }
        }
        else
        {
            throw new IndexOutOfRangeException("Chọn vật phẩm không có trong danh sách");
        }
    }

    public Item selectItemEquipByItemId(int itemId)
    {
        return selectItemByItemId(itemId, GopetManager.EQUIP_PET_INVENTORY);
    }

    public Item selectItem(int itemindex, sbyte inventoryItem)
    {
        CopyOnWriteArrayList<Item> inventory = player.playerData.getInventoryOrCreate(inventoryItem);
        if (itemindex >= 0 && itemindex < inventory.Count)
        {
            Item item = inventory.get(itemindex);
            return item;
        }
        else
        {
            throw new IndexOutOfRangeException("Chọn vật phẩm không có trong danh sách");
        }
    }

    public Item selectItemByItemId(int itemId, sbyte inventoryItem)
    {
        CopyOnWriteArrayList<Item> inventory = (CopyOnWriteArrayList<Item>)player.playerData.getInventoryOrCreate(inventoryItem);
        int left = 0;
        int right = inventory.Count - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            Item midItem = inventory.get(mid);
            if (midItem.itemId == itemId)
            {
                return midItem;
            }
            if (midItem.itemId < itemId)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return null;
    }

    public Item selectItemsbytemp(int templateId, sbyte inventoryItem)
    {
        CopyOnWriteArrayList<Item> inventory = player.playerData.getInventoryOrCreate(inventoryItem);
        foreach (Item item in inventory)
        {
            if (item.getTemp().getItemId() == templateId)
            {
                return item;
            }
        }
        return null;
    }

    public Pet selectPetByItemId(int petId)
    {
        CopyOnWriteArrayList<Pet> inventory = player.playerData.pets;
        int left = 0;
        int right = inventory.Count - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            Pet midPet = inventory.get(mid);
            if (midPet.petId == petId)
            {
                return midPet;
            }
            if (midPet.petId < petId)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return null;
    }

    private void selectPet(sbyte typeSelect)
    {
        switch (typeSelect)
        {
            case GopetCMD.PET_UPGRADE_ACTIVE:
                MenuController.sendMenu(MenuController.MENU_SELECT_PET_UPGRADE_ACTIVE, player);
                break;
            case GopetCMD.PET_UPGRADE_PASSIVE:
                MenuController.sendMenu(MenuController.MENU_SELECT_PET_UPGRADE_PASSIVE, player);
                break;
        }
    }

    public void addPetUpgrade(Pet pet, sbyte typePetUpgrade, int petindex)
    {
        Message m = messagePetSerive(GopetCMD.PET_UPGRADE_PET_INFO);
        m.putsbyte(typePetUpgrade);
        m.putInt(petindex);
        m.putUTF(pet.getPetTemplate().frameImg);
        m.putsbyte(0);
        m.cleanup();
        player.session.sendMessage(m);
    }

    private void setPricePetUpgrade(int coin, int gold)
    {
        Message m = messagePetSerive(GopetCMD.PRICE_UPGRADE_PET);
        m.putInt(gold);
        m.putInt(coin);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void showKiosk(sbyte typeKiosk)
    {
        objectPerformed.put(MenuController.OBJKEY_TYPE_SHOW_KIOSK, typeKiosk);
        MarketPlace marketPlace = (MarketPlace)player.getPlace();
        Kiosk kiosk = MarketPlace.getKiosk(typeKiosk);
        SellItem sellItem = kiosk.getItemByUserId(player.user.user_id);
        Message m = messagePetSerive(GopetCMD.KIOSK);
        m.putsbyte(typeKiosk);
        m.putInt(sellItem == null ? 0 : 1);
        if (sellItem != null)
        {
            m.putInt(sellItem.itemId);
            m.putUTF(sellItem.getFrameImgPath());
            m.putUTF(sellItem.getName());
            m.putUTF(sellItem.getDescription());
            m.putInt(Utilities.round((sellItem.expireTime - Utilities.CurrentTimeMillis) / 1000l));
        }
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void removeSellItem(int itemId)
    {
        sbyte typeKiosk = (sbyte)objectPerformed.get(MenuController.OBJKEY_TYPE_SHOW_KIOSK);
        MarketPlace marketPlace = (MarketPlace)player.getPlace();
        Kiosk kiosk = MarketPlace.getKiosk(typeKiosk);
        SellItem sellItem = kiosk.searchItem(itemId);
        if (sellItem != null)
        {
            lock (sellItem)
            {
                if (sellItem.hasSell)
                {
                    player.redDialog("Người khác mua vật phẩm này rồi");
                }
                else if (sellItem.hasRemoved)
                {
                    kiosk.kioskItems.remove(sellItem);
                }
                else
                {
                    kiosk.kioskItems.remove(sellItem);
                    if (sellItem.pet != null)
                    {
                        player.playerData.pets.add(sellItem.pet);
                    }
                    else
                    {
                        player.addItemToInventory(sellItem.ItemSell);
                    }
                    player.okDialog(Utilities.Format("Gỡ vật phẩm về túi thành công", sellItem.getName()));
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Gỡ vật phẩm về túi thành công", sellItem.getName())).setObj(sellItem));
                    sellItem.hasRemoved = true;
                }
            }
        }
        else
        {
            player.redDialog("Người khác mua vật phẩm này rồi");
        }
    }

    public void checkExpire()
    {
        Item skinItem = player.playerData.skin;
        if (skinItem != null)
        {
            if (skinItem.expire < Utilities.CurrentTimeMillis)
            {
                player.playerData.skin = null;
            }
        }
        foreach (var entry in player.playerData.items)
        {
            CopyOnWriteArrayList<Item> val = entry.Value;
            foreach (Item item in val)
            {
                skinItem = item;
                if (skinItem != null)
                {
                    if (skinItem.expire > 0)
                    {
                        if (skinItem.expire < Utilities.CurrentTimeMillis)
                        {
                            val.remove(item);
                        }
                    }
                }
                else
                {
                    val.remove(item);
                }
            }
        }
    }

    public void updateSkin()
    {
        updateAvatar();
        GopetPlace place = (GopetPlace)player.getPlace();
        if (place != null)
        {
            place.sendMySkin(player);
        }
    }

    public void updateWing()
    {
        GopetPlace place = (GopetPlace)player.getPlace();
        if (place != null)
        {
            place.sendMyWing(player);
        }
    }

    private void selectMaterialEnchantItem(int itemEnchantId, int itemSelectType)
    {
        Item echanItem = selectItemEquipByItemId(itemEnchantId);
        //System.out.println("server.GameController.selectMaterialEnchantItem() " + itemEnchantId + " " + itemSelectType);
        switch (itemSelectType)
        {
            case GopetManager.TYPE_SELECT_ENCHANT_MATERIAL1:
                MenuController.sendMenu(MenuController.MENU_SELECT_ENCHANT_MATERIAL1, player);
                break;
            case GopetManager.TYPE_SELECT_ENCHANT_MATERIAL2:
                MenuController.sendMenu(MenuController.MENU_SELECT_ENCHANT_MATERIAL2, player);
                break;
            case GopetManager.TYPE_SELECT_ITEM_UP_TIER:
                MenuController.showInventory(player, GopetManager.EQUIP_PET_INVENTORY, MenuController.MENU_SELECT_EQUIP_PET_TIER, "Vật phẩm");
                break;
            case GopetManager.TYPE_SELECT_ITEM_UP_SKILL:
                MenuController.sendMenu(MenuController.MENU_SELECT_ITEM_UP_SKILL, player);
                objectPerformed.put(MenuController.OBJKEY_SKILL_UP_ID, itemEnchantId);
                break;
        }
    }

    public void selectMaterialEnchant(int index, String iconPath, String name, int indexElemnt)
    {
        writeSelectItemEnchant(index, iconPath, name, indexElemnt, GopetCMD.SELECT_METERIAL_ENCHANT_PET_INFO);
    }

    public void selectMaterialGemEnchant(int index, String iconPath, String name, int indexElemnt)
    {
        writeSelectItemEnchant(index, iconPath, name, indexElemnt - 6, GopetCMD.SELECT_GEM_ENCHANT);
    }

    private void writeSelectItemEnchant(int index, String iconPath, String name, int indexElemnt, sbyte cmd)
    {
        Message m = messagePetSerive(cmd);
        m.putInt(index);
        m.putUTF(iconPath);
        m.putUTF(name);
        m.putInt(indexElemnt + 6);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void selectGemUpTier(int index, String iconPath, String name, int indexElemnt, int lvl)
    {
        Message m = messagePetSerive(GopetCMD.SELECT_GEM_UP_TIER);
        m.putInt(index);
        m.putUTF(iconPath);
        m.putUTF(name);
        m.putInt(indexElemnt);
        m.putInt(lvl);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void enchantItem()
    {
        Item itemEuip = (Item)objectPerformed.get(MenuController.OBJKEY_EQUIP_ITEM_ENCHANT);
        Item materialItem = (Item)objectPerformed.get(MenuController.OBJKEY_EQUIP_ITEM_MATERIAL_ENCHANT);
        Item materialCrystal = (Item)objectPerformed.get(MenuController.OBJKEY_EQUIP_ITEM_MATERIAL_CRYSTAL_ENCHANT);
        bool isGem = (bool)objectPerformed.get(MenuController.OBJKEY_IS_ENCHANT_GEM);
        if (itemEuip != null && materialItem != null && materialCrystal != null)
        {
            if (GopetManager.PRICE_ENCHANT.Length <= itemEuip.lvl)
            {
                player.redDialog("Trang bị đạt cấp tối đa rồi");
                return;
            }

            if (isGem)
            {
                if (!checkGemElementVsPet(itemEuip.Template.element))
                {
                    return;
                }
            }

            if (player.checkCoin(GopetManager.PRICE_ENCHANT[itemEuip.lvl]))
            {
                if (checkCount(materialCrystal, 1) && checkCount(materialItem, 1))
                {
                    if (itemEuip.lvl >= 10)
                    {
                        player.redDialog("Vật phẩm này đã đạt cấp tối đa , không thể nâng thêm nữa");
                    }
                    else
                    {
                        player.mineCoin(GopetManager.PRICE_ENCHANT[itemEuip.lvl]);
                        bool isSuccec = (materialCrystal.getTemp().getOptionValue()[0] + (isGem ? GopetManager.PERCENT_OF_ENCHANT_GEM[itemEuip.lvl] : GopetManager.PERCENT_ENCHANT[itemEuip.lvl]) > Utilities.NextFloatPer()) || isBuffEnchent;
                        int levelDrop = 0;
                        bool destroyItem = !isSuccec && isGem ? (itemEuip.lvl > 8) : (itemEuip.lvl == 8 || itemEuip.lvl == 9);
                        if (isSuccec)
                        {
                            if (!isGem)
                            {
                                itemEuip.AddEnchantInfo();
                            }
                            itemEuip.lvl++;
                            if (isGem)
                            {
                                itemEuip.updateGemOption();
                                sendGemItemInfo(itemEuip);
                            }
                            else
                            {
                                this.taskCalculator.onItemEnchant(itemEuip);
                            }
                        }
                        else
                        {
                            if (isGem)
                            {
                                if (itemEuip.lvl >= 1 && itemEuip.lvl <= 7)
                                {
                                    levelDrop = 1;
                                }
                                else if (itemEuip.lvl == 8)
                                {
                                    levelDrop = 2;
                                }
                            }
                            else
                            {
                                if (itemEuip.lvl == 7)
                                {
                                    levelDrop = 2;
                                }
                                else if (itemEuip.lvl >= 3 && itemEuip.lvl <= 6)
                                {
                                    levelDrop = 1;
                                }
                            }

                            for (int i = 0; i < levelDrop; i++)
                            {
                                itemEuip.lvl--;
                            }

                            if (destroyItem)
                            {
                                if (!isGem)
                                {
                                    unEquipItem(itemEuip.itemId);
                                    removeItemEquip(itemEuip.itemId);
                                }
                                else
                                {
                                    removeGem(itemEuip.itemId);
                                }
                            }
                            else
                            {
                                if (isGem)
                                {
                                    itemEuip.updateGemOption();
                                    sendGemItemInfo(itemEuip);
                                }
                            }
                        }
                        if (!isGem)
                        {
                            resendPetEquipInfo(itemEuip);
                            Pet pet = player.getPet();
                            if (pet != null)
                            {
                                pet.applyInfo(player);
                            }
                        }
                        subCountItem(materialItem, 1, GopetManager.NORMAL_INVENTORY);
                        subCountItem(materialCrystal, 1, GopetManager.NORMAL_INVENTORY);
                        if (isSuccec)
                        {
                            player.okDialog(Utilities.Format("Chức mưng bạn đã cường hóa thành công %s lên +%s", itemEuip.getName(), itemEuip.lvl));
                            if (itemEuip.lvl >= 7)
                            {
                                PlayerManager.showBanner(Utilities.Format("Chúc mừng người chơi %s đã cường hoá trang bị %s lên +%s,lực chiến tăng mạnh!!!", player.playerData.name, itemEuip.getTemp().getName(), itemEuip.lvl));
                            }
                            HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s lên +%s", itemEuip.getTemp().getName(), itemEuip.lvl)).setObj(itemEuip));
                        }
                        else
                        {
                            if (destroyItem)
                            {
                                player.redDialog("Thật không may, trong lúc cường hóa thì trang bị của bạn đã bị vỡ");
                                PlayerManager.showBanner(Utilities.Format("Thật đáng tiếc người chơi %s đã cường hoá %s thất bại,hư hỏng vĩnh viễn!!!", player.playerData.name, itemEuip.getTemp().getName()));
                                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s thất bại bị vỡ", itemEuip.getTemp().getName())).setObj(itemEuip));
                            }
                            else
                            {
                                if (levelDrop > 0)
                                {
                                    player.redDialog(Utilities.Format("Cường hóa thất bại trang bị giảm %s cấp", levelDrop));
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s thất bại bị giảm %s cấp", itemEuip.getTemp().getName(), levelDrop)).setObj(itemEuip));
                                }
                                else
                                {
                                    player.redDialog("Cường hóa trang bị thất bại");
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s thất bại bị giảm %s cấp", itemEuip.getTemp().getName(), levelDrop)).setObj(itemEuip));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                notEnoughCoin();
            }
        }
    }

    private void confirmEnchantItem(int equipItemId, int materialTemp, int materialTempCrystal, bool isGem)
    {
        Item itemEuip = isGem ? selectItemByItemId(equipItemId, GopetManager.GEM_INVENTORY) : selectItemEquipByItemId(equipItemId);
        Item materialItem = selectItemsbytemp(materialTemp, GopetManager.NORMAL_INVENTORY);
        Item materialCrystal = selectItemsbytemp(materialTempCrystal, GopetManager.NORMAL_INVENTORY);
        if (itemEuip == null || materialItem == null || materialCrystal == null)
        {
            player.redDialog("Thao tác quá nhanh");
            return;
        }

        if (itemEuip.lvl < 10)
        {
            if (itemEuip != null && materialItem != null && materialCrystal != null)
            {

                if (!isGem && itemEuip.gemInfo != null)
                {
                    player.redDialog("Vui lòng tháo ngọc");
                    return;
                }

                objectPerformed.put(MenuController.OBJKEY_EQUIP_ITEM_ENCHANT, itemEuip);
                objectPerformed.put(MenuController.OBJKEY_EQUIP_ITEM_MATERIAL_ENCHANT, materialItem);
                objectPerformed.put(MenuController.OBJKEY_EQUIP_ITEM_MATERIAL_CRYSTAL_ENCHANT, materialCrystal);
                objectPerformed.put(MenuController.OBJKEY_IS_ENCHANT_GEM, isGem);
                int levelDrop = 0;
                bool canDest = isGem ? (itemEuip.lvl > 8) : (itemEuip.lvl == 8 || itemEuip.lvl == 9);
                if (isGem)
                {
                    if (itemEuip.lvl >= 1 && itemEuip.lvl <= 7)
                    {
                        levelDrop = 1;
                    }
                    else if (itemEuip.lvl == 8)
                    {
                        levelDrop = 2;
                    }
                }
                else
                {
                    if (itemEuip.lvl == 7)
                    {
                        levelDrop = 2;
                    }
                    else if (itemEuip.lvl >= 3 && itemEuip.lvl <= 6)
                    {
                        levelDrop = 1;
                    }
                }
                String dropStr = "";
                String destStr = "";
                if (levelDrop > 0)
                {
                    dropStr = Utilities.Format("\n Nếu thất bại trang bị của bạn sẽ giảm %s cấp.", levelDrop);
                }
                if (canDest)
                {
                    destStr = "\n Nếu thất bại trang bị của bạn sẽ mất";
                }
                MenuController.showYNDialog(MenuController.DIALOG_ENCHANT, Utilities.Format("Bạn có chắc muốn cường hóa %s với tỉ lệ (%s /) + %s/ với giá %s (ngoc) không?", itemEuip.getTemp().getName(), isGem ? GopetManager.PERCENT_OF_ENCHANT_GEM[itemEuip.lvl] : GopetManager.DISPLAY_PERCENT_ENCHANT[itemEuip.lvl], materialCrystal.getTemp().getOptionValue()[0], GopetManager.PRICE_ENCHANT[itemEuip.lvl]).Replace('/', '%') + dropStr + destStr, player);
            }
        }
        else
        {
            player.redDialog("Trang bị đã đạt cấp tối đa");
        }
    }

    public void subCountItem(Item item, int count, sbyte typeInventory)
    {
        if (item.getTemp().isStackable)
        {
            item.count -= count;
            if (item.count <= 0)
            {
                player.playerData.getInventoryOrCreate(typeInventory).remove(item);
            }
        }
        else
        {
            throw new Exception("Có phải item dạng stack đâu");
        }
    }

    public static bool checkCount(Item item, int count)
    {
        return item.count - count >= 0;
    }

    private void upTierItem(int equipItemId, int equipItemId2, bool isGem)
    {
        Item itemEuipActive = isGem ? selectItemByItemId(equipItemId, GopetManager.GEM_INVENTORY) : selectItemEquipByItemId(equipItemId);
        Item itemEuipPassive = isGem ? selectItemByItemId(equipItemId2, GopetManager.GEM_INVENTORY) : selectItemEquipByItemId(equipItemId2);

        if (itemEuipActive == itemEuipPassive || itemEuipActive == null || itemEuipPassive == null)
        {
            player.redDialog("Thao tác nhanh");
            return;
        }
        objectPerformed.put(MenuController.OBJKEY_ITEM_UP_TIER_ACTIVE, itemEuipActive);
        objectPerformed.put(MenuController.OBJKEY_ITEM_UP_TIER_PASSIVE, itemEuipPassive);
        objectPerformed.put(MenuController.OBJKEY_IS_ENCHANT_GEM, isGem);
        if (isGem)
        {
            MenuController.showYNDialog(MenuController.DIALOG_ASK_KEEP_GEM, Utilities.Format("Bạn có muốn dùng %s (vang) để giữ ngọc không bị vỡ không?", GopetManager.PRICE_KEEP_GEM), player);
            objectPerformed.put(MenuController.OBJKEY_ASK_UP_TIER_GEM_STR, Utilities.Format("Bạn có chắc muốn tiến hóa %s với giá %s (ngoc) không?", itemEuipActive.getName(), GopetManager.PRICE_UP_TIER_ITEM));
        }
        else
        {
            MenuController.showYNDialog(MenuController.DIALOG_UP_TIER_ITEM, Utilities.Format("Bạn có chắc muốn tiến hóa %s với giá %s (ngoc) không?", itemEuipActive.getName(), GopetManager.PRICE_UP_TIER_ITEM), player);
        }
    }

    private bool checkGemElementVsPet(sbyte elementItem)
    {
        Pet pet = player.getPet();
        if (pet != null)
        {
            if (!(pet.Template.element == GopetManager.DARK_ELEMENT || pet.Template.element == GopetManager.LIGHT_ELEMENT || pet.Template.element == elementItem))
            {
                player.redDialog($"Bạn cần thú cưng hệ {GopetManager.GetElementDisplay(GopetManager.LIGHT_ELEMENT)} hoặc {GopetManager.GetElementDisplay(GopetManager.DARK_ELEMENT)} để thao tác với tất cả các hệ.\n Còn lại bạn phải có thú cưng cùng hệ với vật phẩm!!!");
                return false;
            }
        }
        else
        {
            player.petNotFollow();
            return false;
        }
        return true;
    }

    public void upTierItem()
    {
        Item itemEuipActive = (Item)objectPerformed.get(MenuController.OBJKEY_ITEM_UP_TIER_ACTIVE);
        objectPerformed.remove(MenuController.OBJKEY_ITEM_UP_TIER_ACTIVE);
        Item itemEuipPassive = (Item)objectPerformed.get(MenuController.OBJKEY_ITEM_UP_TIER_PASSIVE);
        objectPerformed.remove(MenuController.OBJKEY_ITEM_UP_TIER_PASSIVE);
        bool isGem = (bool)objectPerformed.get(MenuController.OBJKEY_IS_ENCHANT_GEM);
        bool isKeepGem = false;
        if (objectPerformed.ContainsKey(MenuController.OBJKEY_IS_KEEP_GOLD))
        {
            isKeepGem = (bool)objectPerformed.get(MenuController.OBJKEY_IS_KEEP_GOLD);
        }

        if (itemEuipActive != itemEuipPassive && itemEuipActive != null)
        {
            if (!isGem)
            {
                if (itemEuipActive.gemInfo != null || itemEuipPassive.gemInfo != null)
                {
                    player.redDialog("Vui lòng tháo ngọc");
                    return;
                }
            }
            if (itemEuipActive.getTemp() == itemEuipPassive.getTemp())
            {
                if (player.checkCoin(GopetManager.PRICE_UP_TIER_ITEM))
                {
                    TierItem tierItem = GopetManager.tierItem.get(itemEuipActive.itemTemplateId);
                    if (tierItem != null)
                    {
                        if (isGem)
                        {
                            if (!checkGemElementVsPet(tierItem.ItemTemplateTwo.element))
                            {
                                return;
                            }


                            player.mineCoin(GopetManager.PRICE_UP_TIER_ITEM);
                            if (isKeepGem)
                            {
                                if (player.checkGold(GopetManager.PRICE_KEEP_GEM))
                                {
                                    player.mineGold(GopetManager.PRICE_KEEP_GEM);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            bool isSucces = Utilities.NextFloatPer() <= tierItem.percent;
                            if (isSucces)
                            {
                                itemEuipActive.updateGemOption();
                                itemEuipPassive.updateGemOption();
                                int[] optionValue = new int[itemEuipActive.optionValue.Length];
                                for (int i = 0; i < itemEuipActive.gemOptionValue.Length; i++)
                                {
                                    float f = itemEuipActive.gemOptionValue[i] + itemEuipPassive.gemOptionValue[i];
                                    optionValue[i] = Utilities.round(Utilities.GetValueFromPercent(f * 100, GopetManager.PERCENT_ITEM_TIER_INFO));
                                }
                                itemEuipActive.lvl = 0;
                                itemEuipActive.itemTemplateId = tierItem.itemTemplateIdTier2;
                                itemEuipActive.optionValue = optionValue;
                                itemEuipActive.updateGemOption();
                                removeGem(itemEuipPassive.itemId);
                                sendGemItemInfo(itemEuipActive);
                            }
                            else
                            {
                                if (!isKeepGem)
                                {
                                    bool isActive = Utilities.nextInt(2) == 1;
                                    removeGem(isActive ? itemEuipActive.itemId : itemEuipPassive.itemId);
                                    PlayerManager.showBanner(Utilities.Format("Người chơi %s cường hóa %s thất bại làm bể viên ngọc", player.playerData.name, (itemEuipActive.getTemp().getName())));
                                }
                            }
                        }
                        else
                        {
                            if (itemEuipActive.petEuipId < 0 && itemEuipPassive.petEuipId < 0)
                            {
                                removeItemEquip(itemEuipPassive.itemId);
                                player.mineCoin(GopetManager.PRICE_UP_TIER_ITEM);
                                itemEuipActive.EnchantInfo.Clear();
                                itemEuipActive.hp = Utilities.round(Utilities.GetValueFromPercent(itemEuipActive.getHp() + itemEuipPassive.getHp(), GopetManager.PERCENT_ITEM_TIER_INFO));
                                itemEuipActive.mp = (Utilities.round(Utilities.GetValueFromPercent(itemEuipActive.getMp() + itemEuipPassive.getMp(), GopetManager.PERCENT_ITEM_TIER_INFO)));
                                itemEuipActive.atk = (Utilities.round(Utilities.GetValueFromPercent(itemEuipActive.getAtk() + itemEuipPassive.getAtk(), GopetManager.PERCENT_ITEM_TIER_INFO)));
                                itemEuipActive.def = (Utilities.round(Utilities.GetValueFromPercent(itemEuipActive.getDef() + itemEuipPassive.getDef(), GopetManager.PERCENT_ITEM_TIER_INFO)));
                                itemEuipActive.lvl = 0;
                                itemEuipActive.itemTemplateId = tierItem.itemTemplateIdTier2;
                                resendPetEquipInfo(itemEuipActive);
                                Pet p = player.getPet();
                                if (p != null)
                                {
                                    p.applyInfo(player);
                                }
                                if (GopetManager.tierItemHashMap.ContainsKey(itemEuipActive.itemTemplateId))
                                {
                                    getTaskCalculator().onUpTierItem(GopetManager.tierItemHashMap.get(itemEuipActive.itemTemplateId));
                                }
                            }
                            else
                            {
                                player.redDialog("Vui lòng chọn vật phẩm thứ  mà thứ cưng không có đeo");
                            }
                        }
                    }
                    else
                    {
                        player.redDialog("Vật phẩm này không thể lên đời");
                    }
                }
                else
                {
                    player.controller.notEnoughCoin();
                }
            }
            else
            {
                player.redDialog("Vật phẩm không cùng loại");
            }
        }
        else
        {
            player.redDialog("Vật phẩm trùng");
        }
    }

    private void petUpTier(int petId1, int petId2, String name, sbyte moneyType)
    {
        Pet petActive = selectPetByItemId(petId1);
        Pet petPassive = selectPetByItemId(petId2);
        if (petActive.Expire != null || petPassive.Expire != null)
        {
            player.redDialog("Không thể tiến hóa với pet dùng thử");
            return;
        }
        PetTier petTier = GopetManager.petTier.get(petActive.petIdTemplate);
        if (name.Length < 30 && name.Length >= 5)
        {
            if (petActive.equip.isEmpty() && petPassive.equip.isEmpty())
            {
                if (petActive.lvl >= GopetManager.LVL_PET_REQUIER_UP_TIER && petPassive.lvl >= GopetManager.LVL_PET_PASSIVE_REQUIER_UP_TIER)
                {
                    if (!(name.Length > 20 || name.Length < 6))
                    {
                        if (petTier != null)
                        {
                            if (petPassive.petIdTemplate == petTier.getPetTemplateIdNeed())
                            {
                                switch (moneyType)
                                {
                                    case 1:
                                        if (player.checkGold(GopetManager.PRICE_UP_TIER_PET))
                                        {
                                            player.mineGold(GopetManager.PRICE_UP_TIER_PET);
                                        }
                                        else
                                        {
                                            notEnoughCoin();
                                            return;
                                        }
                                        break;
                                    default:
                                        player.redDialog("Không dùng loại này");
                                        return;
                                }
                                player.playerData.pets.remove(petPassive);
                                player.playerData.pets.remove(petActive);
                                int gym_add = 0;
                                int gym_up_level = 0;
                                if (petActive.star + petPassive.star >= 10)
                                {
                                    gym_up_level += 5;
                                }
                                else if (petActive.star + petPassive.star >= 8)
                                {
                                    gym_up_level += 4;
                                }
                                else
                                {
                                    gym_up_level += 3;
                                }
                                gym_add += Utilities.round((petActive.lvl + petPassive.lvl) / 2);
                                Pet oldPet = petActive;
                                petActive = new Pet(petTier.getPetTemplateId2());
                                petActive._int = oldPet._int + 10;
                                petActive.agi = oldPet.agi + 10;
                                petActive.str = oldPet.str + 10;
                                petActive.name = name;
                                petActive.lvl = 1;
                                petActive.exp = 0;
                                petActive.tiemnang_point = gym_add;
                                petActive.isUpTier = true;
                                petActive.wasSell = oldPet.wasSell;
                                petActive.pointTiemNangLvl = gym_up_level;
                                player.playerData.pets.add(petActive);
                                player.okDialog(Utilities.Format("Chức mừng bạn đã tiến hóa thành công %s và thú cưng của bạn được cộng %s điểm gym", petActive.getNameWithStar(), gym_add));
                                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Tiến hóa pet %s thành công", petActive.getNameWithoutStar())).setObj(petActive));
                                Message message = messagePetSerive(GopetCMD.PET_UP_TIER);
                                message.cleanup();
                                player.session.sendMessage(message);
                                this.taskCalculator.onUpTierPet();
                            }
                            else
                            {
                                player.redDialog("Vui lòng chọn pet thứ 2 chính xác");
                            }
                        }
                        else
                        {
                            player.redDialog("Hiện tại thứ cưng này không thể tiến hóa");
                        }
                    }
                    else
                    {
                        player.redDialog("Tên pet phải lớn hơn 20 ký tự hoặc bé hơn 6 ký tự");
                    }
                }
                else
                {
                    player.redDialog(Utilities.Format("Bạn cần thú cưng đạt cấp %s mới có thể tiến hóa và ảo ảnh cần cấp %s", GopetManager.LVL_PET_REQUIER_UP_TIER, GopetManager.LVL_PET_PASSIVE_REQUIER_UP_TIER));
                }
            }
            else
            {
                player.redDialog("Bạn phải tháo trang bị cho cả 2 pet");
            }
        }
        else
        {
            player.redDialog("Tên pet không dài quá 30 ký tự và phải lớn hơn 6 ký tự");
        }
    }

    public void upSkill()
    {
        int skillId = (int)objectPerformed.get(OBJKEY_SKILL_UP_ID);
        Pet pet = player.getPet();
        int skillIndex = pet.getSkillIndex(skillId);
        PetSkill petSkill = GopetManager.PETSKILL_HASH_MAP.get(skillId);
        Item itemSelect = (Item)objectPerformed.get(OBJKEY_ITEM_UP_SKILL);
        if (itemSelect.count > 0)
        {
            if (pet.skill[skillIndex][1] < 11)
            {
                subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                bool succes = GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]] + itemSelect.getTemp().getOptionValue()[0] > Utilities.NextFloatPer();
                if (succes)
                {
                    pet.skill[skillIndex][1]++;
                    this.taskCalculator.onUpdateSkillPet(pet, pet.skill[skillIndex][1]);
                    player.okDialog(Utilities.Format("Chức mừng bạn đã nâng cấp %s lên cấp %s !", petSkill.name, pet.skill[skillIndex][1]));
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Bạn đã nâng cấp %s lên cấp %s  cho pet %s!", petSkill.name, pet.skill[skillIndex][1], pet.getNameWithoutStar())).setObj(pet));
                }
                else
                {
                    player.redDialog("Nâng cấp thất bại");
                }
            }
            else
            {
                player.redDialog("Kỹ năng đạt cấp tối đa rồi");
            }
        }
    }

    private void selectKioskItem(sbyte type)
    {
        switch (type)
        {
            case GopetManager.KIOSK_HAT:
                MenuController.sendMenu(MenuController.MENU_KIOSK_HAT_SELECT, player);
                break;
            case GopetManager.KIOSK_AMOUR:
                MenuController.sendMenu(MenuController.MENU_KIOSK_AMOUR_SELECT, player);
                break;
            case GopetManager.KIOSK_WEAPON:
                MenuController.sendMenu(MenuController.MENU_KIOSK_WEAPON_SELECT, player);
                break;
            case GopetManager.KIOSK_OTHER:
                MenuController.sendMenu(MenuController.MENU_KIOSK_OHTER_SELECT, player);
                break;
            case GopetManager.KIOSK_PET:
                MenuController.sendMenu(MenuController.MENU_KIOSK_PET_SELECT, player);
                break;

            case GopetManager.KIOSK_GEM:
                MenuController.sendMenu(MenuController.MENU_KIOSK_GEM_SELECT, player);
                break;
        }
    }

    public void showInputDialog(int dialogId, String dialogTitle, String[] optionText)
    {
        this.showInputDialog(dialogId, dialogTitle, optionText, null);
    }

    public void showInputDialog(int dialogId, String dialogTitle, String[] optionText, sbyte[] optionTextType)
    {
        Message m = new Message(GopetCMD.COMMAND_GUIDER);
        m.putsbyte(GopetCMD.TYPE_DIALOG_INPUT);
        m.putInt(dialogId);
        m.putUTF(dialogTitle);
        m.putInt(optionText.Length);
        for (int i = 0; i < optionText.Length; i++)
        {
            m.putUTF(optionText[i]);
            if (optionTextType == null)
            {
                m.putsbyte(0);
            }
            else
            {
                m.putsbyte(optionTextType[i]);
            }
        }
        m.cleanup();
        player.session.sendMessage(m);
    }

    private long timeInviteDelay = Utilities.CurrentTimeMillis;

    private void inviteChallenge(int user_id)
    {
        Player playerChallenge = PlayerManager.get(user_id);
        if (playerChallenge != player)
        {
            if (playerChallenge != null)
            {
                Pet ownPet = playerChallenge.getPet();
                if (ownPet != null)
                {
                    if (playerChallenge.controller.getPetBattle() == null)
                    {
                        MenuController.sendMenu(MenuController.MENU_INTIVE_CHALLENGE, player);
                        objectPerformed.put(MenuController.OBJKEY_INVITE_CHALLENGE_PLAYER, playerChallenge);
                    }
                    else
                    {
                        player.redDialog("Người chơi này đang có một cuộc chiến");
                    }
                }
                else
                {
                    player.petNotFollow();
                }
            }
            else
            {
                player.redDialog("Người chơi đã thoát");
            }
        }
    }

    public void sendChallenge(Player playerChallenge, int price)
    {
        if (timeInviteDelay < Utilities.CurrentTimeMillis)
        {
            timeInviteDelay = Utilities.CurrentTimeMillis + GopetManager.DELAY_INVITE_PLAYER_CHALLENGE;
            playerChallenge.controller.showChallenge(player, price);
        }
        else
        {
            player.redDialog("Vui lòng chờ");
        }
    }

    private void pk(int user_id)
    {
        Place place = player.getPlace();
        GopetMap map = place.map;
        if (map.mapID != 12 && map.mapID != 11 && map.mapID != 22)
        {
            if (!(player.playerData.pkPoint >= GopetManager.MAX_PK_POINT))
            {
                objectPerformed.put(MenuController.OBJKEY_USER_ID_PK, user_id);
                MenuController.sendMenu(MenuController.MENU_SELECT_ITEM_PK, player);
            }
            else
            {
                player.redDialog("Điểm pk của bạn quá cao");
            }
        }
        else
        {
            player.redDialog("Những map này không cho phép PK");
        }
    }

    public void confirmpk()
    {
        //        if (true) {
        //            player.redDialog("Chức năng này bị tắt do vi phạm nguyên tác cho người chơi mới");
        //            return;
        //        }
        int user_id = (int)objectPerformed.get(MenuController.OBJKEY_USER_ID_PK);
        Item cardPK = (Item)objectPerformed.get(MenuController.OBJKEY_ITEM_PK);
        objectPerformed.remove(MenuController.OBJKEY_ITEM_PK);
        objectPerformed.remove(MenuController.OBJKEY_USER_ID_PK);
        Player playerPassive = PlayerManager.get(user_id);
        ClanMember passiveClanMember = playerPassive.controller.getClan();
        if (playerPassive != player && playerPassive != null)
        {
            if (!playerPassive.playerData.isAdmin)
            {
                if (playerPassive != null)
                {
                    Pet ownPet = playerPassive.getPet();
                    if (ownPet != null)
                    {
                        if (playerPassive.controller.getPetBattle() == null)
                        {
                            if (!ownPet.petDieByPK)
                            {
                                if (checkCount(cardPK, 1))
                                {
                                    subCountItem(cardPK, 1, GopetManager.NORMAL_INVENTORY);
                                    player.playerData.pkPoint++;
                                    GopetPlace place = (GopetPlace)player.getPlace();
                                    place.startFightPlayer(user_id, player, true, 0);
                                    HistoryManager.addHistory(new History(playerPassive).setLog(Utilities.Format("Bị người chơi %s PK", player.playerData.name)));
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("PK người chơi %s", playerPassive.playerData.name)));
                                }
                            }
                            else
                            {
                                player.redDialog("Thú cưng của người này vừa bị PK đã chết rồi");
                            }
                        }
                        else
                        {
                            player.redDialog("Người chơi này đang có một cuộc chiến");
                        }
                    }
                    else
                    {
                        player.petNotFollow();
                    }
                }
                else
                {
                    player.redDialog("Người chơi đã thoát");
                }
            }
            else
            {
                player.redDialog("Người chơi này thuộc BQT");
            }
        }
    }

    private void showChallenge(Player playerInvite, int price)
    {
        if (playerInvite.controller.getPetBattle() == null)
        {
            MenuController.showYNDialog(MenuController.DIALOG_INVITE_CHALLENGE, Utilities.Format("Người chơi %s muốn thách đấu bạn với mức cược %s (ngoc)\n Bạn có đồng ý lời mời này không?", playerInvite.playerData.name, Utilities.FormatNumber(price)), player);
            objectPerformed.put(MenuController.OBJKEY_INVITE_CHALLENGE_PLAYER, playerInvite);
            objectPerformed.put(MenuController.OBJKEY_PRICE_BET_CHALLENGE, price);
        }
    }

    public void startChallenge()
    {
        Player playerInvite = (Player)objectPerformed.get(MenuController.OBJKEY_INVITE_CHALLENGE_PLAYER);
        int coinBet = (int)objectPerformed.get(MenuController.OBJKEY_PRICE_BET_CHALLENGE);
        if (playerInvite != player && playerInvite != null)
        {
            if (playerInvite.checkCoin(coinBet) && player.checkCoin(coinBet))
            {
                if (playerInvite.controller.getPetBattle() == null && playerInvite.controller.getPetBattle() == null)
                {
                    GopetPlace place = (GopetPlace)player.getPlace();
                    if (place.players.Contains(playerInvite))
                    {
                        if (playerInvite.getPet().petDieByPK || player.getPet().petDieByPK)
                        {
                            String redDl = "Trong 2 thú cưng có thú cưng vừa bị chết do PK";
                            player.redDialog(redDl);
                            playerInvite.redDialog(redDl);
                        }
                        else
                        {
                            playerInvite.mineCoin(coinBet);
                            player.mineCoin(coinBet);
                            place.startFightPlayer(playerInvite.user.user_id, player, false, coinBet);
                            HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Tiến hành thách đấu với người chơi %s tiền cược là %s ngọc", playerInvite.playerData.name, Utilities.FormatNumber(coinBet))));
                            HistoryManager.addHistory(new History(playerInvite).setLog(Utilities.Format("Tiến hành thách đấu với người chơi %s tiền cược là %s ngọc", player.playerData.name, Utilities.FormatNumber(coinBet))));
                        }
                    }
                    else
                    {
                        player.redDialog("Người chơi mới bạn không có nằm trong khu này");
                    }
                }
            }
            else
            {
                playerInvite.controller.notEnoughCoin();
                notEnoughCoin();
            }
        }
    }

    public void upStarPet(Item itemSelect)
    {
        Pet mypet = player.getPet();
        if (mypet != null)
        {
            if (mypet.star < 5)
            {
                if (mypet.getPetIdTemplate() == itemSelect.getTemp().getOptionValue()[0])
                {
                    int star = mypet.star;
                    int countNeed = (star + 1) * 10;
                    if (checkCount(itemSelect, countNeed))
                    {
                        subCountItem(itemSelect, countNeed, GopetManager.NORMAL_INVENTORY);
                        mypet.tiemnang_point += mypet.star;
                        mypet.star++;
                        player.okDialog(Utilities.Format("Chúc mừng bạn đã nâng sao thành công cho\n %s", mypet.getNameWithStar()));
                        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Nâng sao thú cưng %s lên %s sao", mypet.getPetTemplate().name, mypet.star)));
                    }
                    else
                    {
                        player.redDialog("Số mảnh không đủ\n Cần " + countNeed + " mảnh!");
                    }
                }
                else
                {
                    player.redDialog("Đây không phải là mảnh pet đúng với pet bạn dẫn theo");
                }
            }
            else
            {
                player.redDialog("Pet của bạn đã đạt sao tới đa");
            }
        }
        else
        {
            player.petNotFollow();
        }
    }

    private int getIndexOfPetCanTatto()
    {
        int index = 0;
        Pet pet = player.getPet();
        if (pet == null) return -1;
        for (int i = 0; i < GopetManager.LVL_REQUIRE_PET_TATTO.Length; i++)
        {
            int j = GopetManager.LVL_REQUIRE_PET_TATTO[i];
            if (pet.lvl >= j)
            {
                index++;
                if (pet.tatto.Count < index)
                {
                    return index;
                }
            }
        }
        return index;
    }

    private int getIndexOfPetCanUnlockTatto()
    {
        int index = 0;
        Pet pet = player.getPet();
        for (int i = 0; i < GopetManager.LVL_REQUIRE_PET_TATTO.Length; i++)
        {
            int j = GopetManager.LVL_REQUIRE_PET_TATTO[i];
            if (pet.lvl >= j)
            {
                index++;
            }
        }
        return index;
    }


    public void showPetTattoUI()
    {
        showPetTattoUI(player.getPet());
    }

    public void showPetTattoUI(Pet pet)
    {
        if (pet != null)
        {
            int indexTatto = getIndexOfPetCanTatto();
            if (indexTatto < 0)
            {
                player.petNotFollow();
                return;
            }
            int indexUnlock = getIndexOfPetCanUnlockTatto();
            Message m = messagePetSerive(GopetCMD.TATTOO);
            m.putsbyte(GopetCMD.TATTOO_INIT_SCREEN);
            m.putInt(GopetManager.LVL_REQUIRE_PET_TATTO.Length);
            for (int i = 0; i < GopetManager.LVL_REQUIRE_PET_TATTO.Length; i++)
            {
                if (i >= pet.tatto.Count)
                {
                    if (indexUnlock > i)
                    {
                        m.putInt(0);
                        m.putUTF("Chưa xăm");
                        m.putsbyte(i + 1);
                        m.putUTF("tatoos/0.png");
                    }
                    else
                    {
                        m.putInt(0);
                        m.putUTF(Utilities.Format("Mốc cấp %s", GopetManager.LVL_REQUIRE_PET_TATTO[i]));
                        m.putsbyte(i + 1);
                        m.putUTF("tatoos/-1.png");
                    }
                }
                else
                {
                    PetTatto petTatto = pet.tatto.get(i);
                    PetTattoTemplate petTattoTemplate = petTatto.Template;
                    m.putInt(petTatto.tattoId);
                    m.putUTF(petTatto.getName());
                    m.putsbyte(i + 1);
                    m.putUTF(petTattoTemplate.getIconPath());
                }
            }
            m.cleanup();
            player.session.sendMessage(m);
        }
    }

    public void genTatto(Item itemSelect)
    {
        if (checkCount(itemSelect, 1))
        {
            Pet pet = player.getPet();
            if (pet != null)
            {
                int indexTatto = getIndexOfPetCanTatto();
                if (indexTatto >= 0 && pet.tatto.Count < indexTatto)
                {
                    int randTatto = randTattoo(itemSelect.getTemp().getOptionValue());
                    PetTatto petTatto = new PetTatto(randTatto);
                    pet.addTatto(petTatto);
                    player.okDialog(Utilities.Format("Chức mừng bạn xăm thành công %s", petTatto.getName()));
                    pet.applyInfo(player);
                    subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                    showPetTattoUI();
                }
                else
                {
                    player.redDialog("Bạn chưa mở mốc này");
                }
            }
        }
    }

    public void setTaskCalculator(TaskCalculator taskCalculator)
    {
        this.taskCalculator = taskCalculator;
    }

    internal TaskCalculator getTaskCalculator()
    {
        return this.taskCalculator;
    }

    public static int randTattoo(int[] listTempId)
    {
        if (listTempId.Length == 1) return listTempId[0];

        while (true)
        {
            int randTatto = Utilities.RandomArray(listTempId);
            PetTattoTemplate petTattoTemplate = GopetManager.tattos.get(randTatto);
            if (petTattoTemplate.getPercent() > Utilities.NextFloatPer())
            {
                return randTatto;
            }
        }
    }

    public void removeTatto(Item itemSelect, int idTatto)
    {
        Pet p = player.getPet();
        if (p != null)
        {
            PetTatto tatto = p.selectTattoById(idTatto);
            if (tatto != null)
            {
                if (checkCount(itemSelect, 1))
                {
                    p.tatto.remove(tatto);
                    showPetTattoUI();
                    player.okDialog(Utilities.Format("Xóa thành công %s", tatto.Template.getName()));
                    subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                    objectPerformed.remove(MenuController.OBJKEY_TATTO_ID_REMOVE);
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Xóa xăm %s cho pet %s", tatto.Template.getName(), p.getNameWithoutStar())));
                }
            }
        }
    }

    public JArrayList<Popup> onReiceiveGift(int[][] gift)
    {
        JArrayList<Popup> popups = new();
        bool flagDrop = false;
        for (int i = 0; i < gift.Length; i++)
        {
            int[] giftInfo = gift[i];
            switch (giftInfo[0])
            {
                case GopetManager.GIFT_GOLD:
                    player.addGold(giftInfo[1]);
                    popups.add(new Popup(Utilities.Format("%s (vang)", Utilities.FormatNumber(giftInfo[1]))));
                    break;

                case GopetManager.GIFT_COIN:
                    player.addCoin(giftInfo[1]);
                    popups.add(new Popup(Utilities.Format("%s (ngoc)", Utilities.FormatNumber(giftInfo[1]))));
                    break;
                case GopetManager.GIFT_ITEM:
                    {
                        int itemId = giftInfo[1];
                        int count = giftInfo[2];
                        Item item = new Item(itemId);
                        if (giftInfo.Length >= 4)
                        {
                            item.canTrade = giftInfo[3] == 1;
                        }
                        if (!item.getTemp().isStackable)
                        {
                            for (int j = 0; j < count; j++)
                            {
                                player.addItemToInventory(new Item(itemId));
                            }
                            popups.add(new Popup(item.getName() + " x" + count));
                        }
                        else
                        {
                            item.count = count;
                            player.addItemToInventory(item);
                            popups.add(new Popup(item.getName()));
                        }
                    }
                    break;


                case GopetManager.GIFT_ITEM_PERCENT_NO_DROP_MORE:
                    {
                        if (!flagDrop)
                        {
                            int itemId = giftInfo[1];
                            bool nextBool = Utilities.NextFloatPer() < giftInfo[2] / 100f;
                            if (nextBool)
                            {
                                Item item = new Item(itemId);
                                item.count = giftInfo[3];
                                player.addItemToInventory(item);
                                popups.add(new Popup(item.getName()));
                                flagDrop = true;
                            }
                        }
                    }
                    break;


                case GopetManager.GIFT_EXP:
                    {
                        Pet myPet = player.getPet();
                        if (myPet != null)
                        {
                            myPet.addExp(giftInfo[1]);
                            popups.add(new Popup(Utilities.FormatNumber(giftInfo[1]) + " exp cho thú cưng đang đi theo"));
                        }
                    }
                    break;

                case GopetManager.GIFT_ENERGY:
                    {
                        player.addStar(giftInfo[1]);
                        popups.add(new Popup(Utilities.FormatNumber(giftInfo[1]) + " năng lượng"));
                    }
                    break;
                case GopetManager.GIFT_RANDOM_ITEM:
                    {
                        int numGift = giftInfo[1];
                        List<int[]> listGiftRandom = new();
                        for (int xxx = 2; xxx < giftInfo.Length; xxx += 2)
                        {
                            listGiftRandom.Add(new int[] { giftInfo[xxx], giftInfo[xxx + 1] });
                        }
                        if (listGiftRandom.Count > 0)
                        {
                            for (int t = 0; t < numGift; t++)
                            {
                                bool flag = false;
                                int[] rand = Utilities.RandomArray(listGiftRandom);
                                int itemId = rand[1];
                                int count = rand[0];
                                switch (itemId)
                                {
                                    case -123:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_SILVER);
                                        break;
                                    case -124:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PET_TIER_ONE);
                                        break;
                                    case -125:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PET_TIER_TOW);
                                        break;
                                    case -126:
                                        player.playerData.AccumulatedPoint += count;
                                        popups.add(new Popup($"{count} điểm tích lũy"));
                                        flag = true;
                                        break;
                                }
                                if (flag) break;
                                Item item = new Item(itemId);
                                if (!item.getTemp().isStackable)
                                {
                                    for (int j = 0; j < count; j++)
                                    {
                                        player.addItemToInventory(new Item(itemId));
                                    }
                                    popups.add(new Popup(item.getName() + " x" + count));
                                }
                                else
                                {
                                    item.count = count;
                                    player.addItemToInventory(item);
                                    popups.add(new Popup(item.getName()));
                                }
                            }
                        }
                    }
                    break;
                case GopetManager.GIFT_ITEM_MAX_OPTION:
                    {
                        int itemId = giftInfo[1];
                        int count = giftInfo[2];
                        for (int j = 0; j < count; j++)
                        {
                            Item item = new Item(itemId);
                            item.atk = Item.GetMaxOption(item.Template.atkRange);
                            item.def = Item.GetMaxOption(item.Template.defRange);
                            item.hp = Item.GetMaxOption(item.Template.hpRange);
                            item.mp = Item.GetMaxOption(item.Template.mpRange);
                            player.addItemToInventory(item);
                            popups.add(new Popup(item.getName()));
                        }
                    }
                    break;
                case GopetManager.GIFT_EVENT_POINT:
                    {
                        player.playerData.EventPoint += giftInfo[1];
                        popups.add(new Popup($"{giftInfo[1]} điểm sự kiện"));
                        break;
                    }
                case GopetManager.GIFT_FUND_CLAN:
                    {
                        ClanMember clanMember = getClan();
                        if(clanMember != null)
                        {
                            clanMember.clan.addFund(giftInfo[1], clanMember);
                            player.okDialog($"Bang hội của bạng được cộng {Utilities.FormatNumber(giftInfo[1])} quỹ bang");
                            popups.add(new Popup($"{Utilities.FormatNumber(giftInfo[1])} quỹ bang"));
                        }
                        break;
                    }
            }
        }
        return popups;
    }



    public void showImageDialog(
            int id,
            int w,
            int h,
            String image,
            int frameNum,
            int frameDelay)
    {
        Message m = new Message(GopetCMD.COMMAND_GUIDER);
        m.putsbyte(11);
        m.putInt(id);
        m.putInt(w);
        m.putInt(h);
        m.putString(image);
        m.putInt(frameNum);
        m.putInt(frameDelay);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void selectGem(int itemId)
    {
        objectPerformed.put(MenuController.OBJKEY_EQUIP_INLAY_GEM_ID, itemId);
        MenuController.sendMenu(MenuController.MENU_SELECT_GEM_TO_INLAY, player);
    }

    public bool selectGemM1 = false;

    public void showGemInvenstory()
    {
        selectGemM1 = false;
        CopyOnWriteArrayList<Item> items = player.playerData.getInventoryOrCreate(GopetManager.GEM_INVENTORY);
        Message m = messagePetSerive(GopetCMD.SHOW_GEM_INVENTORY);
        m.putInt(items.Count);
        foreach (Item item in items)
        {
            m.putInt(item.itemId);
            m.putInt(item.getTemp().getIconId());
            m.putUTF(item.getEquipName());
            m.putInt(item.lvl);
        }
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void sendMail()
    {
        Message m = new Message(GopetCMD.GL_MAIL);
        m.putsbyte(1);
        m.putInt(Utilities.nextInt(200000000));
        m.putUTF("Lời chào của Game");
        m.putUTF("Game gopet là một game nuôi thú ảo rất phổ biến trên điện thoại di động vào khoảng năm 2010-2015. Game này cho phép người chơi chọn một con thú ảo, chăm sóc, huấn luyện và thi đấu với các con thú khác. Game gopet có nhiều loại thú đa dạng, từ những con vật quen thuộc như mèo, chó, gấu, khỉ... cho đến những con thú kỳ lạ như rồng, phượng hoàng, kỳ lân... Người chơi có thể tùy biến ngoại hình, kỹ năng và trang bị cho con thú của mình. Game gopet cũng có nhiều hoạt động hấp dẫn như săn bắt, khám phá, trồng trọt, kết bạn, kết hôn... Game gopet đã thu hút hàng triệu người chơi trên khắp Việt Nam và trở thành một phần của tuổi thơ của nhiều người.");
        m.putsbyte(0);
        m.cleanup();
        player.session.sendMessage(m);
    }

    private void selectItemEnchantGem(int num)
    {
        if (selectGemM1)
        {
            MenuController.sendMenu(MenuController.MENU_SELECT_GEM_ENCHANT_MATERIAL2, player);
        }
        else
        {
            MenuController.sendMenu(MenuController.MENU_SELECT_GEM_ENCHANT_MATERIAL1, player);
        }
    }

    private void askRemoveGemItem(int itemId)
    {
        objectPerformed.put(MenuController.OBJKEY_ID_GEM_REMOVE, itemId);
        MenuController.showYNDialog(MenuController.DIALOG_CONFIRM_REMOVE_GEM, "Bạn chắc muốn xóa ngọc này?", player);
    }

    public void removeGem(int itemId)
    {
        Item item = selectItemByItemId(itemId, GopetManager.GEM_INVENTORY);
        if (item != null)
        {
            player.playerData.getInventoryOrCreate(GopetManager.GEM_INVENTORY).remove(item);
            sendRemoveGemItem(itemId);
        }
        else
        {
            player.redDialog("Đã xóa vật phẩm này");
        }
    }

    private void sendRemoveGemItem(int itemId)
    {
        Message m = messagePetSerive(GopetCMD.REMOVE_GEM_ITEM);
        m.putInt(itemId);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void sendGemItemInfo(Item item)
    {
        Message m = messagePetSerive(GopetCMD.SEND_GEM_INFo);
        m.putInt(item.itemId);
        m.putUTF(item.getTemp().getIconPath());
        m.putUTF(item.getEquipName());
        m.putsbyte(item.lvl);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void inlayGem(Item gem, int itemId)
    {
        Item equipItem = selectItemEquipByItemId(itemId);
        if (gem != null)
        {
            if (!checkGemElementVsPet(gem.Template.element))
            {
                return;
            }

            if (equipItem.petEuipId > -1 && equipItem.petEuipId != player.playerData.petSelected?.petId)
            {
                player.redDialog("Gắn ngọc thất bại do pet hiện tại không mang trang bị này");
                return;
            }

            if (equipItem != null)
            {
                if (equipItem.gemInfo == null)
                {
                    player.playerData.getInventoryOrCreate(GopetManager.GEM_INVENTORY).remove(gem);
                    ItemGem itemGem = new ItemGem();
                    itemGem.element = (gem.getTemp().getElement());
                    itemGem.itemTemplateId = (gem.itemTemplateId);
                    itemGem.lvl = (gem.lvl);
                    itemGem.option = (gem.option);
                    itemGem.optionValue = (gem.optionValue);
                    itemGem.name = (gem.getTemp().getName());
                    equipItem.gemInfo = itemGem;
                    equipItem.updateGemOption();
                    Pet p = player.getPet();
                    if (p != null)
                    {
                        p.applyInfo(player);
                    }
                    resendPetEquipInfo(equipItem);
                }
                else
                {
                    player.redDialog("Vật phẩm này đã khảm ngọc rồi");
                }
            }
            else
            {
                player.redDialog("Trang bị không tồn tại");
            }
        }
        else
        {
            player.redDialog("Xảy ra lỗi, có vẻ như thao tác quá nhanh hoặc mạng yếu");
        }
    }

    public void unequipGem(int itemId)
    {
        objectPerformed.put(MenuController.OBJKEY_ID_ITEM_REMOVE_GEM, itemId);
        MenuController.showYNDialog(MenuController.DIALOG_ASK_REMOVE_GEM, "Bạn có chắc là muốn tháo ngọc ra không?", player);
    }

    public void confirmUnequipGem(int itemId)
    {
        Item item = selectItemEquipByItemId(itemId);
        if (item != null)
        {
            if (item.gemInfo == null)
            {
                player.redDialog("Vật phẩm không có gắn ngọc");
            }
            else
            {
                if (item.gemInfo.timeUnequip > 0)
                {
                    player.redDialog("Vật phẩm đang tháo");
                }
                else
                {
                    item.gemInfo.timeUnequip = (Utilities.CurrentTimeMillis + GopetManager.TIME_UNEQUIP_GEM);
                    resendPetEquipInfo(item);
                }
            }
        }
        else
        {
            player.redDialog("Vật phẩm không tồn tại");
        }
    }

    public void checkUnequipGem(Item item)
    {
        if (item != null)
        {
            if (item.gemInfo != null)
            {
                if (item.gemInfo.timeUnequip > 0)
                {
                    if (item.gemInfo.timeUnequip < Utilities.CurrentTimeMillis)
                    {
                        ItemGem itemGem = item.gemInfo;
                        item.gemInfo = null;
                        item.updateGemOption();
                        Pet p = player.getPet();
                        if (p != null)
                        {
                            p.applyInfo(player);
                        }
                        resendPetEquipInfo(item);
                        Item recoveryGem = new Item(itemGem.itemTemplateId);
                        recoveryGem.lvl = itemGem.lvl;
                        recoveryGem.option = itemGem.option;
                        recoveryGem.optionValue = itemGem.optionValue;
                        recoveryGem.updateGemOption();
                        player.playerData.addItem(GopetManager.GEM_INVENTORY, recoveryGem);
                    }
                }
            }
        }
    }

    public void onUnEquipGem(int itemId)
    {
        Item item = selectItemEquipByItemId(itemId);
        if (item != null)
        {
            checkUnequipGem(item);
            resendPetEquipInfo(item);
        }
    }

    public void confirmUnequipFastGem(int itemId)
    {
        Item item = selectItemEquipByItemId(itemId);
        if (item != null)
        {
            if (item.gemInfo != null)
            {
                if (player.checkGold(GopetManager.PRICE_UNEQUIP_GEM))
                {
                    player.mineGold(GopetManager.PRICE_UNEQUIP_GEM);
                    item.gemInfo.timeUnequip = (1);
                    onUnEquipGem(itemId);
                }
                else
                {
                    notEnoughGold();
                }
            }
        }
    }

    private void fastUnequipGem(int itemId)
    {
        objectPerformed.put(MenuController.OBJKEY_ID_ITEM_FAST_REMOVE_GEM, itemId);
        MenuController.showYNDialog(MenuController.DIALOG_ASK_FAST_REMOVE_GEM, Utilities.Format("Bạn có muốn dùng %s (vang) để tháo ngọc nhanh không?", Utilities.FormatNumber(GopetManager.PRICE_UNEQUIP_GEM)), player);
    }

    private void showListClan()
    {
        Message m = clanMessage(GopetCMD.GUILD_LIST);
        m.putUTF("");
        m.putsbyte(0);
        m.putsbyte(0);
        m.putInt(ClanManager.clans.Count);
        foreach (Clan clan in ClanManager.clans)
        {
            m.putInt(clan.getClanId());
            m.putInt(-1);
            m.putUTF("Bang " + clan.getName() + Utilities.Format(" (Bang chủ: %s)", clan.getMemberByUserId(clan.getLeaderId()).name));
            m.putUTF(clan.getClanDesc());
        }
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void searchClan(String clanName)
    {
        ClanMember clanMember = getClan();
        if (clanMember == null)
        {
            if (clanName.Length > 5 && clanName.Length < 21 && Utilities.CheckString(clanName, "^[a-z0-9]+$"))
            {
                if (ClanManager.clanHashMapName.ContainsKey(clanName))
                {
                    objectPerformed.put(MenuController.OBJKEY_CLAN_NAME_REQUEST, clanName);
                    MenuController.showYNDialog(MenuController.DIALOG_ASK_REQUEST_JOIN_CLAN, Utilities.Format("Bạn có muốn xin vào bang hội %s không?", clanName), player);
                }
                else
                {
                    player.redDialog("Không tồn tại bang hội này");
                }
            }
            else
            {
                player.redDialog("Vui lòng không nhập ký tự lạ \n Hoặc số lượng ký tự không phù hợp");
            }
        }
        else
        {
            player.redDialog("Bạn đã có bang rồi");
        }
    }

    sealed class ClanMemberComparerViaFund : IComparer<ClanMember>
    {
        public int Compare(ClanMember? o1, ClanMember? o2)
        {
            return Utilities.round(o2.fundDonate - o1.fundDonate);
        }
    }

    private void showTopFund()
    {
        ClanMember clanMember = getClan();
        if (clanMember != null)
        {
            Clan clan = clanMember.getClan();
            CopyOnWriteArrayList<ClanMember> listMember = (CopyOnWriteArrayList<ClanMember>)clan.getMembers().clone();
            listMember.Sort(new ClanMemberComparerViaFund());
            Message m = clanMessage(GopetCMD.GUILD_TOP_FUND);
            m.putsbyte(0);
            m.putsbyte(0);
            m.putsbyte(listMember.Count);
            foreach (ClanMember clanMember1 in listMember)
            {
                m.putInt(clanMember1.user_id);
                m.putUTF(clanMember1.getAvatar());
                m.putUTF(clanMember1.name + Utilities.Format(" (Chức vụ: %s)", clanMember1.getDutyName()));
                m.putUTF(Utilities.Format("Đóng góp quỹ: %s", Utilities.FormatNumber(clanMember1.fundDonate)));
            }
            m.cleanup();
            player.session.sendMessage(m);
        }
        else
        {
            showListClan();
        }
    }


    public void requestJoinClan(String clanname)
    {
        Clan clan = ClanManager.getClanByName(clanname);
        if (clan != null)
        {
            ClanRequestJoin requestJoin = clan.getJoinRequestByUserId(player.user.user_id);
            if (requestJoin != null)
            {
                player.redDialog("Bạn đã xin vào bang này rồi, vui lòng chờ xét duyệt.");
            }
            else
            {
                if (clan.getBannedJoinRequestId().Contains(player.user.user_id))
                {
                    player.redDialog("Bang hội này không cho phép bạn gửi đơn xin vào bang");
                }
                else if (clan.curMember >= clan.maxMember)
                {
                    player.redDialog("Bang hội đã đủ thành viên.");
                }
                else
                {
                    clan.addJoinRequest(player.user.user_id, player.playerData.name, player.playerData.avatarPath);
                    player.okDialog("Xin gia nhập bang hội thành công");
                }
            }
        }
    }

    public void notClan()
    {
        player.redDialog("Bạn chưa vào bang hội");
    }

    public void sendListOption(int listId, String title, String message, JArrayList<Option> options)
    {
        Message m = new Message(GopetCMD.COMMAND_GUIDER);
        m.putsbyte(GopetCMD.GUIDER_LIST_OPTION);
        m.putInt(listId);
        m.putInt(listId);
        m.putUTF(title);
        m.putUTF(message);
        m.putInt(options.Count);
        foreach (Option option in options)
        {
            m.putInt(option.getOptionId());
            m.putUTF(option.getOptionText());
            m.putsbyte(option.getOptionStatus());
        }
        m.cleanup();
        player.session.sendMessage(m);
    }

    private void requestJoinClanById(int clanId)
    {
        Clan cl = ClanManager.getClanById(clanId);
        if (cl != null)
        {
            requestJoinClan(cl.getName());
        }
    }

    private void showGuidChat()
    {
        ClanMember clanMember = getClan();
        if (clanMember != null)
        {
            CopyOnWriteArrayList<ClanChat> clanChats = (CopyOnWriteArrayList<ClanChat>)clanMember.getClan().getClanChats().clone();
            Message m = clanMessage(GopetCMD.GUILD_CHAT);
            m.putInt(clanMember.getClan().getClanId());
            m.putUTF("");
            m.putsbyte(clanChats.Count);
            foreach (ClanChat clanChat in clanChats)
            {
                m.putUTF(clanChat.getWho());
                m.putUTF(clanChat.getText());
            }
            m.cleanup();
            player.session.sendMessage(m);
        }
        else
        {
            notClan();
        }
    }

    private void playerChatInClan(int clanId, String text)
    {
        ClanMember clanMember = getClan();
        if (clanMember != null)
        {
            Message m = clanMessage(GopetCMD.GUILD_ON_PLAYER_CHAT);
            m.putUTF(player.playerData.name);
            m.putUTF(text);
            m.cleanup();
            clanMember.getClan().sendMessage(m);
            clanMember.getClan().addChat(new ClanChat(player.playerData.name, text));
        }
        else
        {
            notClan();
        }

    }

    public void showSkillClan(int user_id)
    {
        Player olPlayer = PlayerManager.get(user_id);
        if (olPlayer != null)
        {
            ClanMember clanMember = olPlayer.controller.getClan();
            if (clanMember == null) return;
            bool canEdit = clanMember.duty == Clan.TYPE_LEADER && user_id == player.user.user_id;
            if (clanMember != null)
            {
                Clan clan = clanMember.getClan();
                Message m = clanMessage(GopetCMD.GUILD_CLAN_SKILL);
                m.putsbyte(canEdit ? 0 : 1);
                m.putInt(clanMember.clan.potentialSkill);
                for (int i = 0; i < 3; i++)
                {
                    bool hasSlot = clan.getLvl() >= GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[i] && clan.slotSkill > i;
                    if (hasSlot)
                    {
                        var clanBuffs = clan.SkillRent;
                        bool slotHasSkill = clanBuffs.Count > i;
                        if (slotHasSkill)
                        {
                            ClanSkill clanBuff = clanBuffs.get(i);
                            m.putInt(GopetCMD.SKILL_CLAN_CHANGE);
                            m.putInt(i);
                            m.putUTF(clanBuff.SkillTemplate.description);
                            m.putUTF(clanBuff.SkillTemplate.description);
                            m.putUTF(clanBuff.SkillTemplate.description);
                        }
                        else
                        {
                            m.putInt(GopetCMD.SKILL_CLAN_RENT);
                            m.putInt(i);
                            m.putUTF("Chưa có kỹ năng");
                            m.putUTF("Chưa có kỹ năng");
                            m.putUTF("Chưa có kỹ năng");
                        }
                    }
                    else
                    {
                        m.putInt(GopetCMD.SKILL_CLAN_LOCK);
                        m.putInt(i);
                        m.putUTF("");
                        m.putUTF("");
                        m.putUTF("");
                    }
                }
                m.cleanup();
                player.session.sendMessage(m);
            }
            else
            {
                if (olPlayer == player)
                {
                    notClan();
                }
                else
                {
                    player.redDialog(Utilities.Format("Người chơi %s chưa có bang hội", olPlayer.playerData.name));
                }
            }
        }
        else
        {
            player.redDialog("Người chơi offline");
        }
    }

    public void updateAvatar()
    {
        if (player.playerData != null)
        {
            Item skin = player.playerData.skin;
            if (skin != null)
            {
                player.playerData.avatarPath = skin.getTemp().getIconPath();
            }
            else
            {
                player.playerData.avatarPath = player.playerData.gender == 0 ? "anim_characters/34_icon.png" : "anim_characters/33_icon.png";
            }

            ClanMember clanMember = getClan();
            if (clanMember != null)
            {
                clanMember.avatarPath = player.playerData.avatarPath;
            }
        }
    }

    private void unlockSlotSkillClan()
    {
        ClanMember clanMember = getClan();
        bool canEdit = clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_LEADER;
        if (clanMember != null)
        {
            Clan clan = clanMember.getClan();
            if(clan.slotSkill >= GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN.Length)
            {
                player.redDialog("Mở khóa hết các ô rồi");
            }
            else
            {
                if(clan.lvl >= GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[clan.slotSkill] && clan.fund >= GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill])
                {
                    MenuController.showYNDialog(MenuController.DIALOG_ASK_UNLOCK_SLOT_SKILL_CLAN, $"Bạn có chắc muốn mở khóa ô kỹ năng bang hội tiếp theo cho bang với giá {Utilities.FormatNumber(GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill])} quỹ", player);
                }
                else
                {
                    player.redDialog($"Không đủ điều kiện mở ô\n Bang cần đạt cấp {GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[clan.slotSkill]} và có {Utilities.FormatNumber(GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill])} quỹ");
                }
            }
        }
        else
        {
            notClan();
        }
    }

    private void rentSkill(int index)
    {
        if (index < 3)
        {
            ClanMember clanMember = getClan();
            bool canEdit = clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_LEADER;
            if (clanMember != null)
            {
                Clan clan = clanMember.getClan();
                bool canRent = false;
                if (clan.getLvl() >= GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL.Length - 1])
                {
                    canRent = true;
                }
                else
                {
                    if (clan.getLvl() >= GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[index])
                    {
                        canRent = true;
                    }
                    else
                    {
                        canRent = false;
                        player.redDialog("Ô này chưa mở!");
                        return;
                    }
                }
                if (canRent)
                {
                    if (canEdit)
                    {
                        objectPerformed.put(MenuController.OBJKEY_INDEX_SLOT_SKILL_RENT, index);
                        MenuController.sendMenu(MenuController.MENU_SELECT_SKILL_CLAN_TO_RENT, player);
                    }
                    else
                    {
                        player.redDialog("Bạn không đủ quyền");
                    }
                }
            }
            else
            {
                notClan();
            }
        }
        else
        {
            player.redDialog("Bạn đang có bug ?");
        }
    }

    public void notEnoughFundClan()
    {
        player.redDialog("Điểm góp quỹ cá nhân không đủ");
    }

    public void notEnoughGrowthPointClan()
    {
        player.redDialog("Điểm cống hiến cá nhân không đủ");
    }

    private void kickClanMem(int memberId)
    {
        ClanMember mem = getClan();
        if (mem != null)
        {
            ClanMember memberIsKicked = mem.getClan().getMemberByUserId(memberId);
            if (memberIsKicked == mem)
            {
                player.redDialog("Không thể thao tác trên chính bản thân");
            }
            else if (memberIsKicked == null)
            {
                player.redDialog("Người chơi không còn trong bang hội nữa");
            }
            else
            {
                if (mem.duty == Clan.TYPE_LEADER || mem.duty == Clan.TYPE_DEPUTY_LEADER || mem.duty == Clan.TYPE_SENIOR)
                {
                    if (mem.duty != Clan.TYPE_NORMAL)
                    {
                        if (mem.duty < memberIsKicked.duty)
                        {
                            mem.getClan().kick(memberIsKicked.user_id);
                            player.okDialog("Đuổi thành công");
                        }
                        else
                        {
                            player.redDialog("Bạn không thể đuổi người có chức vụ cao hơn");
                        }
                    }
                }
                else
                {
                    player.redDialog("Bạn không có quyền này!");
                }
            }
        }
        else
        {
            notClan();
        }
    }

    private void showListTask()
    {
        MenuController.sendMenu(MenuController.MENU_SHOW_MY_LIST_TASK, player);
    }

    private void inviteMatch(int user_id)
    {
        Player player___ = PlayerManager.get(user_id);
        if (player___ != null)
        {
            objectPerformed.put(MenuController.OBJKEY_INVITE_CHALLENGE_PLAYER, player___);
            showInputDialog(MenuController.INPUT_DIALOG_CHALLENGE_INVITE, "Giá cược", new String[] { "Số ngọc:  " });
        }
        else
        {
            player.redDialog("Người chơi đã offline rồi!");
        }
    }

    public void unfollowPet(Pet pet)
    {
        Place p = player.getPlace();
        if (p != null)
        {
            Message m = messagePetSerive(GopetCMD.PET_UNFOLLOW);
            m.putInt(player.user.user_id);
            m.putInt(pet.getPetIdTemplate());
            m.cleanup();
            p.sendMessage(m);
        }
    }

    private void checkBugEquipItem()
    {
        int num = 0;
        foreach (Item item in player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY))
        {
            if (item.petEuipId == 0)
            {
                player.playerData.removeItem(GopetManager.EQUIP_PET_INVENTORY, item);
                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Xóa vật phẩm bug (%s)", item.getName())).setObj(item));
                num++;
            }
        }

        if (num > 0)
        {
            player.redDialog(Utilities.Format("Nhân vật của bạn vừa bị xóa %s vật phẩm do BUG mà có!", num));
        }
    }

    internal void setLastTimeKillMob(long lastTimeKillMob)
    {
        this.lastTimeKillMob = lastTimeKillMob;
    }

    internal long getLastTimeKillMob()
    {
        return this.lastTimeKillMob;
    }

    internal void setBuffEnchent(bool v)
    {
        this.isBuffEnchent = v;
    }

    public void notEnoughItem(Item itemSelect, int count)
    {
        player.redDialog($"Không đủ vật phẩm {itemSelect.Template.name} cần số lượng : {count}");
    }


    public bool CheckSky(int mapId)
    {
        if (mapId >= 26 && !player.playerData.isOnSky)
        {
            player.redDialog("Để lên thượng giới bạn cần 1 con thú cưng có cánh.\n Có nghĩa là bạn cần có 1 con pet trùng sinh!!!");
            return false;
        }
        return true;
    }

    public bool checkType(sbyte type, Item item)
    {
        if (item.Template.type == type)
        {
            return true;
        }
        return false;
    }

    public void InvailIitemType()
    {
        player.redDialog("Sai loại vật phẩm");
    }

    public void showExp()
    {
        if (player.playerData.buffExp != null)
        {
            if (player.playerData.buffExp.buffExpTime > 0 && player.playerData.buffExp.Template != null)
            {
                Message m = messagePetSerive(GopetCMD.SHOW_EXP);
                m.putUTF(player.playerData.buffExp.Template.iconPath);
                m.putInt((int)((player.playerData.buffExp.buffExpTime + Utilities.CurrentTimeMillisJava) / 1000));
                m.putInt(0);
                m.putlong(0);
                player.session.sendMessage(m);
            }
        }
    }

    public void showAnimationMenu(int menuId, AnimationMenu animationMenu)
    {
        Message m = messagePetSerive(GopetCMD.ANIMATION_MENU);
        m.putsbyte(0);
        m.putInt(menuId);
        m.putUTF(animationMenu.Title);
        m.putInt(animationMenu.Elements.Count);
        for (int i = 0; i < animationMenu.Elements.Count; i++)
        {
            var element = animationMenu.Elements[i];
            m.putsbyte(element is AnimationMenu.Animation ? 1 : 0);
            m.putbool(element.CanSelect);
            if (element is AnimationMenu.Animation animation)
            {
                m.putUTF(animation.ImagePath);
                m.putsbyte(animation.Type);
                m.putbool(true);
                m.putInt(animation.NumFrame);
                m.putInt(2);
            }
            else if (element is AnimationMenu.Label label)
            {
                m.putUTF(label.Text);
                m.putsbyte(label.Type);
                m.putsbyte((int)label.Style);
            }
            else
            {
                throw new UnsupportedOperationException($"This type is null or is " + element.GetType().FullName);
            }
        }
        m.putInt(animationMenu.Commands.Count);
        for (int i = 0; i < animationMenu.Commands.Count; i++)
        {
            var cmd = animationMenu.Commands[i];
            m.putInt(cmd.Id);
            m.putsbyte(cmd.Type);
            m.putUTF(cmd.Name);
            m.putbool(cmd.IsCloseScreen);
            m.putbool(cmd.IsRelpyServer);
        }

        player.session.sendMessage(m);
    }

    public void testMsg100()
    {
        Message m = messagePetSerive(100);
        m.putsbyte(0);
        m.putInt(11);
        m.putUTF("Giao diện gì đây?");
        m.putInt(20);
        for (int i = 0; i < 20; i++)
        {
            m.putsbyte(1);
            // hide
            m.putbool(true);
            m.putUTF($"achievement/dsfg.png");
            m.putsbyte(0);
            if (1 == 1)
            {
                m.putbool(false);
                m.putInt(2);
                m.putInt(2);
            }
            else
            {
                m.putsbyte(i);
            }
        }
        m.putInt(3);
        for (int i = 0; i < 3; i++)
        {
            m.putInt(-1);
            m.putsbyte(i);
            m.putUTF("Này là cl gì " + i);
            m.putbool(true);
            m.putbool(true);
        }
        player.session.sendMessage(m);
    }

    public void removePetTrial()
    {
        foreach (var pet in player.playerData.pets.Where(p => p.Expire != null))
        {
            if (pet.Expire < DateTime.Now)
            {
                player.playerData.pets.Remove(pet);
            }
        }
    }
}


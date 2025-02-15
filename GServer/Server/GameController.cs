
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
using MySqlConnector;
using static MenuController;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Collections.Generic;
using System.Xml.Linq;
using Gopet.Data.dialog;
using Gopet.Data.Clan;
using Gopet.Data.user;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using Gopet.Data.pet;

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


    public MergeData mergeData { get; } = new MergeData();


    public PlayerData MergePlayerData { get; set; }

    public CopyOnWriteArrayList<AdminSelectItemData> adminSelectItemDatas { get; } = new CopyOnWriteArrayList<AdminSelectItemData>();

    public long lastTimeKillMob { get; set; } = 0;

    private long lastTimeTypeGiftCode = 0L;

    public long delayTimeHealPet = 0;

    public bool isBuffEnchent { get; private set; } = false;
    public bool IsBuffEnchantTatto { get; set; } = false;

    public Animation[] Animations
    {
        get
        {
            List<Animation> list = new List<Animation>();
            if (player.playerData.CurrentAchievementId > 0)
            {
                Achievement achievement = player.controller.FindSeach(player.playerData.CurrentAchievementId);
                if (achievement != null)
                {
                    list.Add(new Animation(achievement.Template.FrameNum, achievement.Template.FramePath, achievement.Template.vX, achievement.Template.vY, false, false, Animation.TYPE_ARCHIVENMENT));
                }
            }
            return list.ToArray();
        }
    }

    public Achievement FindSeach(int Id)
    {
        return Utilities.BinarySearch(player.playerData.achievements, Id);
    }

    public bool isHasBattleAndShowDialog()
    {
        if (petBattle != null)
        {
            player.redDialog(player.Language.BanManipulateWhenHaveBattle);
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
                    GopetPlace place = (GopetPlace)player.getPlace();
                    if (place != null)
                    {
                        player.playerData.x = points[points.Length - 2];
                        player.playerData.y = points[points.Length - 1];
                        place.sendMove(player.user.user_id, b1, points);
                    }
                    /*if (points.Length > 30 && !player.playerData.isAdmin)
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
                        player.session.Close();
                    }*/
                }
                break;
            case GopetCMD.ON_PLACE_CHAT:
                {
                    GopetPlace place = (GopetPlace)player.getPlace();
                    if (place != null)
                    {
                        string text = message.reader().readUTF();
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
                            player.redDialog(string.Format(player.Language.WhenPlayerWasDieByPk, Utilities.FormatNumber(((player.getPet().TimeDieZ - Utilities.CurrentTimeMillis) / 1000))));
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
                        player.redDialog(player.Language.FightingCannotOutMap);
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
            case GopetCMD.LETTER_COMMAND:
                letter(message.readsbyte(), message);
                break;

        }
        message.Close();
    }

    public void updateLetterTime()
    {
        foreach (var delayTimeSendLetter in player.playerData.LettersSendTime.Where(k => k.Value.AddMinutes(5) < DateTime.Now))
        {
            player.playerData.LettersSendTime.Remove(delayTimeSendLetter.Key);
        }
    }

    public void sendLetter(string name, string title, string shortContent, string content, sbyte type)
    {
        if (name == player.playerData.name)
        {
            player.redDialog(player.Language.BugWarning);
            return;
        }

        Player playerRequest = PlayerManager.get(name);
        if (playerRequest == null)
        {
            int userId = -1;
            using (var conn = MYSQLManager.create())
            {
                var userData = conn.QueryFirstOrDefault("SELECT `user_id` FROM `player` WHERE `name` = @name;", new { name = name });
                if (userData == null)
                {
                    player.redDialog(player.Language.PlayerNotFound);
                    return;
                }
                else userId = userData.user_id;
            }
            sendLetter(userId, title, shortContent, content, type);
        }
        else
        {
            sendLetter(playerRequest.user.user_id, title, shortContent, content, type);
        }
    }

    public void sendLetter(int userId, string title, string shortContent, string content, sbyte type)
    {
        if (userId == player.playerData.user_id)
        {
            player.redDialog(player.Language.BugWarning);
            return;
        }
        if (player.playerData.LettersSendTime.ContainsKey(userId))
        {
            if (player.playerData.LettersSendTime[userId] > DateTime.Now)
            {
                player.redDialog(player.Language.WaitTimeForSendLetter);
                return;
            }
            else
                player.playerData.LettersSendTime[userId] = DateTime.Now.AddSeconds(30);
        }
        updateLetterTime();
        Letter letter = new Letter(type, title, shortContent, content);
        letter.time = DateTime.Now;
        letter.targetId = userId;
        letter.userId = player.user.user_id;
        Player playerRequest = PlayerManager.get(userId);
        if (playerRequest == null)
        {
            using (var conn = MYSQLManager.create())
            {
                var friendData = conn.QueryFirstOrDefault<PlayerData>("SELECT `BlockFriendLists` FROM `player` WHERE `player`.`user_id` = @user_id LIMIT 1;", new { user_id = userId });
                if (friendData == null)
                    player.redDialog(player.Language.PlayerNotFound);
                else
                {
                    if (friendData.BlockFriendLists.Contains(player.user.user_id))
                        player.redDialog(player.Language.SendLetterFailByBlock);
                    else
                    {
                        conn.Execute("INSERT INTO `letter`(`userId`, `targetId`, `time`, `Type`, `Title`, `ShortContent`, `Content`) VALUES (@userId,@targetId,@time,@Type,@Title,@ShortContent,@Content)", letter);
                        player.okDialog(player.Language.SendLetterOK);
                    }
                }
                friendData = null;
            }
        }
        else
        {
            playerRequest.playerData.addLetter(letter);
            playerRequest.controller.sendHasLetter();
            player.okDialog(player.Language.SendLetterOK);
        }
    }

    public static Message letterMessage(sbyte cmd)
    {
        Message message = new Message(GopetCMD.LETTER_COMMAND);
        message.putInt(cmd);
        return message;
    }

    public void letter(sbyte subCmd, Message msg)
    {
#if DEBUG_LOG
        GopetManager.ServerMonitor.LogWarning($"Hộp thư: {subCmd}");
#endif
        switch (subCmd)
        {
            case GopetCMD.LETTER_COMMAND_LIST_FRIEND:
                MenuController.sendMenu(MenuController.MENU_LIST_FRIEND, player);
                break;
            case GopetCMD.LETTER_BOX:
                showLetterBox();
                //testMsg65();
                break;
            case GopetCMD.LETTER_COMMAND_REQUEST_ADD_FRIEND:
                requestAddFriend(msg.readInt());
                break;
            case GopetCMD.LETTER_COMMAND_REQUEST_ADD_FRIEND_WITH_NAME:
                requestAddFriend(msg.readUTF());
                break;
            case GopetCMD.LETTER_COMMAND_LIST_REQUEST_ADD_FRIEND:
                MenuController.sendMenu(MenuController.MENU_LIST_REQUEST_ADD_FRIEND, player);
                break;
            case GopetCMD.LETTER_COMMAND_LIST_BLOCK_FRIEND:
                MenuController.sendMenu(MenuController.MENU_LIST_BLOCK_FRIEND, player);
                break;
            case GopetCMD.LETTER_COMMAND_SEND_LETTER:
                sendLetter(msg.readUTF(), $"{player.playerData.name}", $"Thư của {player.playerData.name} (Ngày: {Utilities.ToDateString(DateTime.Now)})", msg.readUTF(), Letter.FRIEND);
                break;
            case GopetCMD.LETTER_COMMAND_SET_MARK:
                {
                    var letter = player.playerData.FindLetter(msg.readInt());
                    if (letter != null)
                    {
                        letter.IsMark = true;
                        sendHasLetter();
                    }
                }
                break;
            case GopetCMD.LETTER_COMMAND_REMOVE_LETTER:
                {
                    var letter = player.playerData.FindLetter(msg.readInt());
                    if (letter != null)
                    {
                        player.playerData.letters.Remove(letter);
                    }
                }
                break;
        }
    }

    private void requestAddFriend(string name)
    {
        Player playerRequest = PlayerManager.get(name);
        if (playerRequest == null)
        {
            int userId = -1;
            using (var conn = MYSQLManager.create())
            {
                var userData = conn.QueryFirstOrDefault("SELECT `user_id` FROM `player` WHERE `name` = @name;", new { name = name });
                if (userData == null)
                {
                    player.redDialog(player.Language.PlayerNotFound);
                    return;
                }
                else userId = userData.user_id;
            }
            requestAddFriend(userId);
        }
        else
        {
            requestAddFriend(playerRequest.user.user_id);
        }
    }

    private void requestAddFriend(int userId)
    {
        if (userId == player.user.user_id)
        {
            player.redDialog(player.Language.BugWarning);
            return;
        }
        Player playerRequest = PlayerManager.get(userId);
        if (playerRequest == null)
        {
            using (var conn = MYSQLManager.create())
            {
                var friendData = conn.QueryFirstOrDefault<PlayerData>("SELECT * FROM `player` WHERE `player`.`user_id` = @user_id LIMIT 1;", new { user_id = userId });
                if (friendData == null)
                    player.redDialog(player.Language.PlayerNotFound);
                else
                {
                    if (friendData.RequestAddFriends.Contains(player.user.user_id) || friendData.ListFriends.Contains(player.user.user_id))
                        player.redDialog(player.Language.RequestAddFriendExist);
                    else if (friendData.BlockFriendLists.Contains(player.user.user_id))
                        player.redDialog(player.Language.RequestAddFriendBlock);
                    else
                    {
                        var findHasRequest = conn.Query<FriendRequest>("SELECT `userId`, `targetId` FROM `request_add_friend` WHERE `userId` =  @userId  AND `targetId` = @targetId;", new FriendRequest(player.user.user_id, userId, DateTime.Now));
                        if (findHasRequest.Any())
                        {
                            player.redDialog(player.Language.RequestAddFriendExist);
                            return;
                        }
                        conn.Execute("INSERT INTO `request_add_friend`(`userId`, `targetId`) VALUES (@userId, @targetId);", new FriendRequest(player.user.user_id, userId, DateTime.Now));
                        player.okDialog(player.Language.RequestAddFriendOK);
                    }
                }
                friendData = null;
            }
        }
        else
        {
            if (playerRequest.playerData.RequestAddFriends.Contains(player.user.user_id) || playerRequest.playerData.ListFriends.Contains(player.user.user_id))
                player.redDialog(player.Language.RequestAddFriendExist);
            else if (playerRequest.playerData.BlockFriendLists.Contains(player.user.user_id))
                player.redDialog(player.Language.RequestAddFriendBlock);
            else
            {
                playerRequest.playerData.RequestAddFriends.Add(player.user.user_id);
                player.okDialog(player.Language.RequestAddFriendOK);
            }
        }
    }


    public void showLetterBox()
    {
        var cloneLetter = player.playerData.letters.clone();
        Message message = letterMessage(GopetCMD.LETTER_BOX);
        message.putInt(cloneLetter.Count);
        foreach (var letter in cloneLetter)
        {
            message.putInt(letter.LetterId);
            message.putsbyte(letter.Type);
            message.putUTF(letter.Title);
            message.putUTF(letter.ShortContent);
            message.putUTF(letter.Content);
            message.putbool(letter.IsMark);
        }
        message.cleanup();
        player.session.sendMessage(message);
    }

    public void sendHasLetter()
    {
        if (player.playerData != null)
        {
            Message message = letterMessage(GopetCMD.LETTER_COMMAND_HAS_LETTER);
            message.putsbyte(player.playerData.letters.Where(p => !p.IsMark).Count() <= 0 ? 0 : 1);
            message.cleanup();
            player.session.sendMessage(message);
        }
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
            message.putUTF(pet.getNameWithStar(player));
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
                PetSkillLv petSkillLv = petSkill.skillLv.get(skilllvl - 1);
                message.putInt(skillId);
                message.putUTF(petSkill.getName(player) + " " + skilllvl);
                message.putUTF(petSkill.getDescription(petSkillLv, player));
                message.putInt(petSkillLv.mpLost);
            }
            message.putInt(pet.tiemnang_point);
            CopyOnWriteArrayList<PetTatto> petTattos = (CopyOnWriteArrayList<PetTatto>)pet.tatto.clone();
            message.putInt(petTattos.Count);

            foreach (PetTatto petTatto in petTattos)
            {
                message.putInt(1);
                message.putUTF(petTatto.getName(player));
                message.putsbyte(1);
                message.putUTF("");
            }
            message.putsbyte(pet.Template.frameNum);
            message.cleanup();
            player.session.sendMessage(message);
            HistoryManager.addHistory(new History(player).setLog("Xem magic của pet " + pet.Template.name).setObj(pet));
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
                player.redDialog(player.Language.CreateCharLaw);
                player.loginOK();
                return;
            }
            using (var conn = MYSQLManager.create())
            {
                var playerData = conn.QueryFirstOrDefault("select user_id from player where name = @name", new { name = name });
                if (playerData != null)
                {
                    player.redDialog(player.Language.DuplicateNameChar);
                    Thread.Sleep(1000);
                    player.session.Close();
                    return;
                }

            }
            PlayerData.create(player.user.user_id, name, gender);
            UserData user = player.user;
            player.user = null;
            player.session.Close();
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
                    player.redDialog(player.Language.IncorrectTyping);
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
#if DEBUG_LOG
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
#if DEBUG_LOG
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
#if DEBUG_LOG
                    GopetManager.ServerMonitor.LogWarning($"Message cánh: {type}");
#endif
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
                                    player.redDialog(player.Language.PleaseUnequipWing);
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
                                    player.redDialog(player.Language.ErrorWhenUpgradeWing);
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
                                    player.okDialog(player.Language.ManipulateOK);
                                    MenuController.sendMenu(MenuController.MENU_WING_INVENTORY, player);
                                    HistoryManager.addHistory(new History(player).setLog("Tháo cánh " + it.getName(player)).setObj(it));
                                }
                                else
                                {
                                    player.redDialog(player.Language.YouNotEquipWing);
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
                objectPerformed[MenuController.OBJKEY_ITEM_KIOSK_CANCEL] = message.readInt();
                MenuController.sendMenu(MenuController.MENU_OPTION_KIOSK_CANCEL_ITEM, player);
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
                        player.redDialog(player.Language.KioskCancelPet);
                    }
                }
                break;
            case GopetCMD.CHECK_SPEED:
                player.onClientSpeedRespose();
                break;
            case GopetCMD.ADMIN_GET_ITEM:
                MenuController.selectMenu(MenuController.MENU_OPTION_ADMIN_GET_ITEM, 2, 0, this.player);
                break;
            case GopetCMD.ADMIN_GIVE_ITEM:
                MenuController.selectMenu(MenuController.MENU_OPTION_ADMIN_GIVE_ITEM, 2, 0, this.player);
                break;
            case GopetCMD.CHAT_GLOBAL:
                sendGlobalChat(message.readUTF());
                break;
            case GopetCMD.AUTO_ATTACK_SUPPORT:
                {
                    if (this.getPetBattle() == null)
                    {
                        GopetPlace gopetPlace = this.player.getPlace();
                        if (gopetPlace != null)
                        {
                            foreach (var item in gopetPlace.mobs.Where(x => x.getPetBattle(player) == null))
                            {
                                gopetPlace.startFightMob(item.getMobId(), player);
                                if (this.getPetBattle() != null)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                break;
        }
    }

    private DateTime LastTimeChat = DateTime.Now;

    private void GlobalChat(string Text)
    {
        Message message = GameController.messagePetService(GopetCMD.CHAT_GLOBAL);
        message.putString(player.playerData.name);
        message.putString(Text);
        player.session.sendMessage(message);
    }

    public void sendGlobalChat(string Text)
    {
        if (this.player.checkGold(GopetManager.GOLD_NEED_CHAT_GLOBAL))
        {
            if (Text.Length <= 40)
            {
                if (LastTimeChat < DateTime.Now)
                {
                    this.player.mineGold(GopetManager.GOLD_NEED_CHAT_GLOBAL);
                    PlayerManager.chatGlobal(player.playerData.name, Text);
                    LastTimeChat = DateTime.Now.AddSeconds(5);
                }
                else
                {
                    player.Popup("Sau khi chat vui lòng chờ 15 giây");
                }
            }
            else
            {
                player.Popup("Số kí tự chat tối đa 40 kí tự");
            }
        }
        else
        {
            GlobalChat($"Cần {GopetManager.GOLD_NEED_CHAT_GLOBAL} (vang) để chat kênh thế giới");
        }
    }

    private void showInfoPetUpTier(int petId1, int petId2)
    {
        Pet petActive = selectPetByItemId(petId1);
        Pet petPassive = selectPetByItemId(petId2);
        if (petActive.Expire != null || petPassive.Expire != null)
        {
            showDescPetUpTierUI(player.Language.CannotUpTierWithTryPet, null);
            return;
        }
        PetTier petTier = GopetManager.petTier.get(petActive.petIdTemplate);
        if (petTier == null || petTier.petTemplateIdNeed != petPassive.Template.petId)
        {
            showDescPetUpTierUI(player.Language.CannotUpTier, null);
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
            showDescPetUpTierUI(petActive.Template.getName(player), new string[] { petActive.Template.getName(player), $"{petActive.str}(str) {petActive.agi}(agi) {petActive._int}(int)", string.Format(player.Language.PotentialScore, petActive.pointTiemNangLvl) });
        }
    }


    public void showDescPetUpTierUI(string text, string[] line_desc)
    {
        if (line_desc == null)
            line_desc = new string[] { text };
        if (line_desc.Length >= sbyte.MaxValue) throw new UnsupportedOperationException("Chi duoc dung 7bit thoi");
        Message m = messagePetService(GopetCMD.INFO_UP_TIER_PET);
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
            var wingInventory = player.playerData.getInventoryOrCreate(GopetManager.WING_INVENTORY);
            if (indexWing >= 0 && wingInventory.Count > indexWing)
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
            //max Hp
            message.putInt(player.playerData.petSelected.maxHp);
            //mp
            message.putInt(player.playerData.petSelected.mp);
            //max Mp
            message.putInt(player.playerData.petSelected.maxMp);
            message.cleanup();
            player.session.sendMessage(message);
        }
    }

    private void requestPetImg(sbyte type, String path)
    {
#if DEBUG_LOG
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
            ms.putUTF(mapData.mapTemplate.getName(player));
            ms.putUTF(mapData.mapTemplate.getName(player));
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
                            player.Popup(player.Language.CannotChangeZone);
                            return;
                        }
                        if (placeId < mapData.numPlace && placeId >= 0)
                        {
                            mapData.places.get(placeId).add(player);
                        }
                        else
                        {
                            player.Popup(player.Language.CannotFoundZone);
                        }
                    }
                    changePlaceDelay = Utilities.CurrentTimeMillis + GopetManager.CHANGE_CHANNEL_DELAY;
                }
                else
                {
                    int second = Utilities.round((changePlaceDelay - Utilities.CurrentTimeMillis) / 1000);
                    player.redDialog(string.Format(player.Language.WaitingForSeconds, second));
                }
            }
            else
            {
                player.redDialog(player.Language.CannotChangeZoneWhenBattle);
            }
        }
        else
        {
            player.redDialog(player.Language.CannotChangeZone);
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
            sendHasLetter();
        }
    }

    private void daily()
    {
        var serverDate = Utilities.GetCurrentDate();
        if (player.playerData.loginDate.Day != serverDate.Day || player.playerData.loginDate.Month != serverDate.Month || player.playerData.loginDate.Year != serverDate.Year)
        {
            this.player.playerData.numUseEnergy.Clear();
            this.player.playerData.star = GopetManager.DAILY_STAR;
            this.player.playerData.ClanTasked.Clear();
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
        Message message = messagePetService(GopetCMD.STAR_INFO);
        //star
        message.putInt(player.playerData.star);
        message.cleanup();
        player.session.sendMessage(message);
        message = messagePetService(GopetCMD.MONEY_INFO);
        //star
        message.putInt(player.playerData.star);
        message.putlong(player.playerData.gold);
        message.putlong(player.playerData.coin);
        message.putlong(player.playerData.lua);
        message.putInt(player.playerData.MoneyDisplays.Count);
        foreach (var itemId in player.playerData.MoneyDisplays)
        {
            Item item = player.controller.selectItemsbytemp(itemId, GopetManager.MONEY_INVENTORY);
            if (item == null)
            {
                message.putUTF(GopetManager.itemTemplate[itemId].iconPath);
                message.putlong(0);
            }
            else
            {
                message.putUTF(GopetManager.itemTemplate[itemId].iconPath);
                message.putlong(item.count);
            }
        }
        message.cleanup();
        player.session.sendMessage(message);


        Message messageEnergy = messagePetService(GopetCMD.ENERGY_INFO);
        messageEnergy.putInt(player.playerData.star);
        messageEnergy.putInt(0);
        messageEnergy.putInt(0);
        messageEnergy.putInt(0);
        messageEnergy.cleanup();
        player.session.sendMessage(messageEnergy);
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
            Message message = messagePetService(GopetCMD.GYM);
            message.putInt(pet.getPetIdTemplate());
            message.putUTF(pet.getPetTemplate().frameImg);
            message.putUTF(pet.getNameWithStar(player));
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
                message.putInt(10 + i);
                message.putInt(20 + i);
                message.putsbyte(0);
                message.putUTF("");
                message.putsbyte(1);
            }
            message.putsbyte(pet.Template.frameNum);
            message.cleanup();
            player.session.sendMessage(message);
        }
        else
        {
            player.petNotFollow();
        }
    }

    public string[] gym_options
    {
        get
        {
            return new string[] { player.Language.GymOptionStr, player.Language.GymOptionAgi, player.Language.GymOptionInt };
        }
    }

    private void upTiemNang(int num, sbyte index)
    {
        if (isHasBattleAndShowDialog())
        {
            return;
        }
        if (index >= 0 && index < gym_options.Length)
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
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cộng tìm năng cho pet %s [num =%s,index=%s]", pet.Template.name, num, index)).setObj(pet));
                }
                else
                {
                    player.redDialog(player.Language.NotEnoughtPotential);
                }
            }
        }
    }

    private void updateTiemnang()
    {
        Pet pet = player.getPet();
        if (pet != null)
        {
            Message message = messagePetService(GopetCMD.UP_TIEM_NANG);
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
                Message message = messagePetService(GopetCMD.EQUIP_INFO);
                message.putInt(user_id);
                message.putInt(pet.petId);
                message.putUTF(pet.getPetTemplate().frameImg);
                message.putUTF(pet.getNameWithStar(player));
                message.putInt(pet.lvl);
                message.putInt(pet.getStr());
                message.putInt(pet.getAgi());
                message.putInt(pet.getInt());
                CopyOnWriteArrayList<Item> petEquipItem = oPlayer.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY);
                writeListItemEquip(petEquipItem, message, false);
                message.putsbyte(pet.Template.frameNum);
                message.cleanup();
                player.session.sendMessage(message);
                if (oPlayer != player)
                {
                    oPlayer.Popup(string.Format(player.Language.OthersPlayerLeakPetInfo, player.playerData.name));
                }
            }
            else
            {
                player.petNotFollow();
            }
        }
        else
        {
            player.redDialog(player.Language.PlayerOffline);
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
        message.putUTF(item.getEquipName(player));
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
        Message m = messagePetService(GopetCMD.ON_UNQUIP_GEM);
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
        player.redDialog(player.Language.NotEnoughCoin);
    }

    public void notEnoughGold()
    {
        player.redDialog(player.Language.NotEnoughGold);
    }

    public void notEnoughSilverBar()
    {
        player.redDialog(player.Language.NotEnoughSilverBar);
    }

    public void notEnoughGoldBar()
    {
        player.redDialog(player.Language.NotEnoughGoldBar);
    }

    public void notEnoughBloodGem()
    {
        player.redDialog(player.Language.NotEnoughBloodGem);
    }

    public void notEnoughCrystal()
    {
        player.redDialog(player.Language.NotEnoughCrystal);
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

    public bool checkCrystal(int count)
    {
        return checkCount(GopetManager.CRYSTAL_ID, count, GopetManager.MONEY_INVENTORY);
    }

    public bool checkCount(int tempId, long count, sbyte inventory)
    {
        Item itemSelect = selectItemsbytemp(tempId, inventory);
        return checkCountItem(itemSelect, count);
    }
    public bool checkCountItem(Item itemSelect, long count)
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
            player.fastAction();
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
                    player.redDialog(player.Language.NotEnoughMaterial);
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
                            player.okDialog(player.Language.EnchantOK);
                        }
                        else
                        {
                            int numDrop = GopetManager.NUM_LVL_DROP_ENCHANT_TATTO_FAILED[first.lvl];
                            first.lvl -= numDrop;
                            p.applyInfo(player);
                            showPetTattoUI();
                            player.redDialog(string.Format(player.Language.EnchantFailDrop, numDrop));
                        }
                    }
                    else
                    {
                        player.redDialog(player.Language.TattoMaxLevel);
                    }
                }
                else
                {
                    InvailIitemType();
                }
            }
            else
            {
                player.fastAction();
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
                            player.redDialog(player.Language.TattoMaxLevel);
                        }
                    }
                    else
                    {
                        InvailIitemType();
                    }
                }
                else
                {
                    player.fastAction();
                }
            }
            else
            {
                player.redDialog(player.Language.BugWarning);
            }
        }
        else
        {
            player.petNotFollow();
        }
    }


    public void sendItemSelectTattoMaterialToEnchant(int id, string icon, string name)
    {
        Message m = messagePetService(GopetCMD.TATTOO);
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
                                player.redDialog(string.Format(player.Language.OnlyPetElementWearing, GopetManager.GetElementDisplay(itemTemplate.element, player)));
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
                        Message message = messagePetService(GopetCMD.USE_EQUIP_ITEM);
                        message.putsbyte(1);
                        message.putInt(itemId);
                        message.cleanup();
                        player.session.sendMessage(message);
                        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Mặc trang bị %s cho pet", item.getName(player))).setObj(item));

                    }
                    else
                    {
                        player.redDialog(player.Language.ItemHasPetEquip);
                    }
                }
                else
                {
                    player.redDialog(player.Language.NotEnoughMetrics + ".\n" + player.Language.Need + Utilities.Format(" %s(str) %s(agi) và %s(int)", item.getTemp().getRequireStr(), item.getTemp().getRequireAgi(), item.getTemp().getRequireInt()));
                }

            }
            else
            {
                player.redDialog(player.Language.ItemIsNotEquip);
            }

        }
        else
        {
            player.petNotFollow();
        }
    }

    public static Message messagePetService(sbyte subCmd)
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
                if (shopId == MenuController.SHOP_FOOD && player.getPlace()?.map.mapTemplate.mapId == 19)
                {
                    MenuController.sendMenu(MenuController.SHOP_SKIN, player);
                    return;
                }
                MenuController.sendMenu(shopId, player);
                break;
            default:
                player.redDialog(player.Language.KioskIsBuiding);
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
                    player.redDialog(player.Language.ItemHasntPetEquip);
                }
                else
                {
                    item.petEuipId = -1;
                    pet.equip.remove((Object)item.itemId);
                    pet.applyInfo(player);
                    Message message = messagePetService(GopetCMD.UNEQUIP_ITEM);
                    message.putsbyte(1);
                    message.putInt(itemId);
                    message.cleanup();
                    player.session.sendMessage(message);
                    resendPetEquipInfo(item);
                    HistoryManager.addHistory(new History(player).setLog("Tháo vật phẩm " + item.getName(player)).setObj(item));
                }
            }
            else
            {
                player.redDialog(player.Language.ItemIsNotEquip);
            }
        }
        else
        {
            player.petNotFollow();
        }
    }

    public void sendPlaceTime(int time)
    {
        Message message = messagePetService(GopetCMD.TIME_PLACE);
        message.putInt(time);
        message.cleanup();
        player.session.sendMessage(message);
    }

    public void showBigTextEff(String text)
    {
        Message message = messagePetService(GopetCMD.SHOW_BIG_TEXT_EFF);
        message.putUTF(text);
        message.cleanup();
        player.session.sendMessage(message);
    }

    public static Message clanMessage(sbyte subCmd)
    {
        Message m = messagePetService(GopetCMD.CLAN);
        m.putsbyte(subCmd);
        return m;
    }

    public void clan(sbyte subCMD, Message message)
    {
#if DEBUG_LOG
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
            listInfoClan.add(player.Language.ClanNameDescription + clan.name);
            listInfoClan.add(player.Language.ClanLvlDescription + clan.lvl);
            listInfoClan.add(player.Language.ClanLeaderDescription + clan.getMemberByUserId(clan.getLeaderId()).name);
            listInfoClan.add(string.Format(player.Language.ClanMemberDescription, clan.curMember, clan.maxMember));
            listInfoClan.add(player.Language.ClanSloganDescription + clan.slogan);
            listInfoClan.add(string.Format(player.Language.ClanFundDescription, Utilities.FormatNumber(clan.getFund())));
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
                message.putUTF("Mốc " + (i + 1) + Utilities.Format(": Quyên góp %s để nhận %s điểm cống hiến", MenuController.getMoneyText(get.getPriceType(), get.getPrice(), player), Utilities.FormatNumber(get.getFund())));
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
                    player.controller.getTaskCalculator().onDonateFund();
                    player.okDialog(player.Language.DonateFundOK);
                }
                else
                {
                    player.redDialog(player.Language.NotEnoughMoney);
                }
            }
            else
            {
                player.redDialog(player.Language.BugWarning);
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
            m.putUTF(player.Language.InfoClanMember);
            m.putInt(0);
            m.putInt(0);
            m.putsbyte(0);
            m.putsbyte(0);
            m.putsbyte(clan.getMembers().Count);
            foreach (ClanMember member in clan.getMembers())
            {
                m.putInt(member.user_id);
                m.putUTF(member.getAvatar());
                m.putUTF(member.name + string.Format(player.Language.ClanDutyDescription, member.getDutyName(player)));
                m.putUTF(string.Format(player.Language.ClanFundDescription, Utilities.FormatNumber(member.fundDonate)));
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
        Message message = messagePetService(GopetCMD.SHOW_UPGRADE_PET);
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
                player.redDialog(player.Language.NeedUnequipItemIfWantDestroyItem);
            }
            else
            {
                player.playerData.removeItem(GopetManager.EQUIP_PET_INVENTORY, item);
                Message message = messagePetService(GopetCMD.REMOVE_ITEM_EQUIP);
                message.putInt(itemId);
                message.cleanup();
                player.session.sendMessage(message);
                HistoryManager.addHistory(new History(player).setLog("Hủy vật phẩm " + item.getTemp().getName(player)).setObj(item));
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
                    MenuController.showYNDialog(MenuController.DIALOG_CONFIRM_REMOVE_ITEM_EQUIP, player.Language.DoYouWantDestroyItem, player);
                }
                else
                {
                    player.redDialog(player.Language.ItemHasPetEquip);
                }
            }
            else
            {
                player.redDialog(player.Language.ItemIsNotEquip);
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
        return inventory.BinarySearch(itemId);
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
        return inventory.BinarySearch(petId);
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
        Message m = messagePetService(GopetCMD.PET_UPGRADE_PET_INFO);
        m.putsbyte(typePetUpgrade);
        m.putInt(petindex);
        m.putUTF(pet.getPetTemplate().frameImg);
        m.putsbyte(0);
        m.cleanup();
        player.session.sendMessage(m);
    }

    private void setPricePetUpgrade(int coin, int gold)
    {
        Message m = messagePetService(GopetCMD.PRICE_UPGRADE_PET);
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
        Message m = messagePetService(GopetCMD.KIOSK);
        m.putsbyte(typeKiosk);
        m.putInt(sellItem == null ? 0 : 1);
        if (sellItem != null)
        {
            m.putInt(sellItem.itemId);
            m.putUTF(sellItem.getFrameImgPath());
            m.putUTF(sellItem.getName(player));
            m.putUTF(sellItem.getDescription(player));
            m.putInt(Utilities.round((sellItem.expireTime - Utilities.CurrentTimeMillis) / 1000l));
            if (sellItem.pet != null)
            {
                m.putsbyte(sellItem.pet.Template.frameNum);
            }
            else
            {
                m.putsbyte(2);
            }
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
                    player.redDialog(player.Language.ItemWasSell);
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
                        player.playerData.pets.Add(sellItem.pet);
                    }
                    else
                    {
                        player.addItemToInventory(sellItem.ItemSell);
                    }
                    if (sellItem.sumVal > 0)
                    {
                        player.addCoin(Utilities.round(Utilities.GetValueFromPercent(sellItem.sumVal, 100f - GopetManager.KIOSK_PER_SELL)));
                    }
                    player.okDialog(player.Language.CancelItemKiosk);
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Gỡ vật phẩm về túi thành công", sellItem.getName(player))).setObj(sellItem));
                    sellItem.hasRemoved = true;
                    showKiosk(typeKiosk);
                }
            }
        }
        else
        {
            player.redDialog(player.Language.ItemWasSell);
        }
    }

    public void checkExpire()
    {
        Item skinItem = player.playerData.skin;
        if (skinItem != null)
        {
            if (skinItem.expire < Utilities.CurrentTimeMillis && skinItem.expire >= 0)
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

        foreach (var item in player.playerData.achievements)
        {
            if (item.Expire < DateTime.Now)
            {
                player.playerData.achievements.remove(item);
                if (item.Id == player.playerData.CurrentAchievementId)
                {
                    player.playerData.CurrentAchievementId = -1;
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
        this.player.UpdateAntiPK();
        GopetPlace place = (GopetPlace)player.getPlace();
        if (place != null)
        {
            place.sendMyWing(player);
        }
    }

    private void selectMaterialEnchantItem(int itemEnchantId, int itemSelectType)
    {
        Item echanItem = selectItemEquipByItemId(itemEnchantId);
        switch (itemSelectType)
        {
            case GopetManager.TYPE_SELECT_ENCHANT_MATERIAL1:
                MenuController.sendMenu(MenuController.MENU_SELECT_ENCHANT_MATERIAL1, player);
                break;
            case GopetManager.TYPE_SELECT_ENCHANT_MATERIAL2:
                MenuController.sendMenu(MenuController.MENU_SELECT_ENCHANT_MATERIAL2, player);
                break;
            case GopetManager.TYPE_SELECT_ITEM_UP_TIER:
                MenuController.showInventory(player, GopetManager.EQUIP_PET_INVENTORY, MenuController.MENU_SELECT_EQUIP_PET_TIER, player.Language.Item);
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
        Message m = messagePetService(cmd);
        m.putInt(index);
        m.putUTF(iconPath);
        m.putUTF(name);
        m.putInt(indexElemnt + 6);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void selectGemUpTier(int index, String iconPath, String name, int indexElemnt, int lvl)
    {
        Message m = messagePetService(GopetCMD.SELECT_GEM_UP_TIER);
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
                player.redDialog(player.Language.ItemHasMaxLevel);
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
                        player.redDialog(player.Language.ItemHasMaxLevel);
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
                            player.okDialog(string.Format(player.Language.EnchantOKWithLvlItem, itemEuip.getName(player), itemEuip.lvl));
                            if (itemEuip.lvl >= 7)
                            {
                                PlayerManager.showBanner((Language) => string.Format(Language.EnchantOKWithLvlItemBanner, player.playerData.name, itemEuip.getTemp().getName(player), itemEuip.lvl));
                            }
                            HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s lên +%s", itemEuip.getTemp().getName(player), itemEuip.lvl)).setObj(itemEuip));
                        }
                        else
                        {
                            if (destroyItem)
                            {
                                player.redDialog(player.Language.EnchantFailAndDestroy);
                                PlayerManager.showBanner((Language) => string.Format(Language.EnchantFailAndDestroyBanner, player.playerData.name, itemEuip.getTemp().getName(player)));
                                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s thất bại bị vỡ", itemEuip.getTemp().getName(player))).setObj(itemEuip));
                            }
                            else
                            {
                                if (levelDrop > 0)
                                {
                                    player.redDialog(string.Format(player.Language.EnchantFailDrop, levelDrop));
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s thất bại bị giảm %s cấp", itemEuip.getTemp().getName(player), levelDrop)).setObj(itemEuip));
                                }
                                else
                                {
                                    player.redDialog(player.Language.EnchantFail);
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Cường hóa %s thất bại bị giảm %s cấp", itemEuip.getTemp().getName(player), levelDrop)).setObj(itemEuip));
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
            player.fastAction();
            return;
        }

        if (itemEuip.lvl < 10)
        {
            if (itemEuip != null && materialItem != null && materialCrystal != null)
            {

                if (!isGem && itemEuip.gemInfo != null)
                {
                    player.redDialog(player.Language.PleaseUnequipWing);
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
                    dropStr = string.Format(player.Language.IfEnchantFailItemWillDrop, levelDrop);
                }
                if (canDest)
                {
                    destStr = player.Language.IfEnchantFailItemWillDestroy;
                }
                MenuController.showYNDialog(MenuController.DIALOG_ENCHANT, string.Format(player.Language.AskEnchantItem, itemEuip.getTemp().getName(player), isGem ? GopetManager.PERCENT_OF_ENCHANT_GEM[itemEuip.lvl] : GopetManager.DISPLAY_PERCENT_ENCHANT[itemEuip.lvl], materialCrystal.getTemp().getOptionValue()[0], GopetManager.PRICE_ENCHANT[itemEuip.lvl]).Replace('/', '%') + dropStr + destStr, player);
            }
        }
        else
        {
            player.redDialog(player.Language.ItemHasMaxLevel);
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
            player.playerData.getInventoryOrCreate(typeInventory).remove(item);
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
            player.fastAction();
            return;
        }
        objectPerformed.put(MenuController.OBJKEY_ITEM_UP_TIER_ACTIVE, itemEuipActive);
        objectPerformed.put(MenuController.OBJKEY_ITEM_UP_TIER_PASSIVE, itemEuipPassive);
        objectPerformed.put(MenuController.OBJKEY_IS_ENCHANT_GEM, isGem);
        if (isGem)
        {
            MenuController.showYNDialog(MenuController.DIALOG_ASK_KEEP_GEM, string.Format(player.Language.KeepGem, GopetManager.PRICE_KEEP_GEM), player);
            objectPerformed.put(MenuController.OBJKEY_ASK_UP_TIER_GEM_STR, string.Format(player.Language.DoYouWantUpTierGem, itemEuipActive.getName(player), GopetManager.PRICE_UP_TIER_ITEM));
        }
        else
        {
            MenuController.showYNDialog(MenuController.DIALOG_UP_TIER_ITEM, string.Format(player.Language.DoYouWantUpTierGem, itemEuipActive.getName(player), GopetManager.PRICE_UP_TIER_ITEM), player);
        }
    }

    private bool checkGemElementVsPet(sbyte elementItem)
    {
        /*Pet pet = player.getPet();
        if (pet != null)
        {
            if (!(pet.Template.element == GopetManager.DARK_ELEMENT || pet.Template.element == GopetManager.LIGHT_ELEMENT || pet.Template.element == elementItem))
            {
                player.redDialog(player.Language.ItemGemElementLaw, GopetManager.GetElementDisplay(GopetManager.LIGHT_ELEMENT, player), GopetManager.GetElementDisplay(GopetManager.DARK_ELEMENT, player));
                return false;
            }
        }
        else
        {
            player.petNotFollow();
            return false;
        }*/
        return true;
    }

    public void upTierItem()
    {
        Item itemEuipActive = (Item)objectPerformed.get(MenuController.OBJKEY_ITEM_UP_TIER_ACTIVE);
        objectPerformed.Remove(MenuController.OBJKEY_ITEM_UP_TIER_ACTIVE);
        Item itemEuipPassive = (Item)objectPerformed.get(MenuController.OBJKEY_ITEM_UP_TIER_PASSIVE);
        objectPerformed.Remove(MenuController.OBJKEY_ITEM_UP_TIER_PASSIVE);
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
                    player.redDialog(player.Language.PleaseUnequipGem);
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
                                    PlayerManager.showBanner((Language) => string.Format(Language.EnchantGemFailAndDestroy, player.playerData.name, (itemEuipActive.getTemp().getName(player))));
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
                                itemEuipActive.NumFusion = 0;
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
                                player.redDialog(player.Language.ItemHasPetEquip);
                            }
                        }
                    }
                    else
                    {
                        player.redDialog(player.Language.ItemCannotUpTier);
                    }
                }
                else
                {
                    player.controller.notEnoughCoin();
                }
            }
            else
            {
                player.redDialog(player.Language.ItemNotEqualType);
            }
        }
        else
        {
            player.redDialog(player.Language.DuplicateItem);
        }
    }

    private void petUpTier(int petId1, int petId2, String name, sbyte moneyType)
    {
        Pet petActive = selectPetByItemId(petId1);
        Pet petPassive = selectPetByItemId(petId2);
        if (petActive.Expire != null || petPassive.Expire != null)
        {
            player.redDialog(player.Language.CannotUpTierWithTryPet);
            return;
        }
        PetTier petTier = GopetManager.petTier.get(petActive.petIdTemplate);
        if (name.Length < 30 && name.Length >= 5)
        {
            if (petActive.equip.isEmpty() && petPassive.equip.isEmpty())
            {
                if (petActive.lvl >= GopetManager.LVL_PET_REQUIER_UP_TIER && petPassive.lvl >= GopetManager.LVL_PET_PASSIVE_REQUIER_UP_TIER)
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
                                    player.redDialog(player.Language.MoneyNotEqualType);
                                    return;
                            }
                            player.playerData.pets.remove(petPassive);
                            player.playerData.pets.remove(petActive);
                            int gym_add = 0;
                            int gym_up_level = 0;
                            if (petActive.star + petPassive.star >= 10)
                            {
                                gym_up_level += 50;
                            }
                            else if (petActive.star + petPassive.star >= 8)
                            {
                                gym_up_level += 40;
                            }
                            else
                            {
                                gym_up_level += 30;
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
                            player.playerData.pets.Add(petActive);
                            player.okDialog(string.Format(player.Language.UpTierPetOK, petActive.getNameWithStar(player), gym_add));
                            HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Tiến hóa pet %s thành công", petActive.getNameWithoutStar(player))).setObj(petActive));
                            Message message = messagePetService(GopetCMD.PET_UP_TIER);
                            message.cleanup();
                            player.session.sendMessage(message);
                            this.taskCalculator.onUpTierPet();
                        }
                        else
                        {
                            player.redDialog(player.Language.WrongPet2UpTier);
                        }
                    }
                    else
                    {
                        player.redDialog(player.Language.PetCannotUpTier);
                    }
                }
                else
                {
                    player.redDialog(string.Format(player.Language.UpTierLaw, GopetManager.LVL_PET_REQUIER_UP_TIER, GopetManager.LVL_PET_PASSIVE_REQUIER_UP_TIER));
                }
            }
            else
            {
                player.redDialog(player.Language.RemoveEquipWhenUpTier);
            }
        }
        else
        {
            player.redDialog(player.Language.SetPetNameLaw);
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
            if ((petSkill.skillID < 116 && pet.skill[skillIndex][1] < 10) || (petSkill.skillID >= 116 && pet.skill[skillIndex][1] < 37))
            {
                subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                bool succes = (petSkill.skillID >= 116 ? GopetManager.PERCENT_UP_SKILL_SKY[pet.skill[skillIndex][1]] : GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]]) + itemSelect.getTemp().getOptionValue()[0] > Utilities.NextFloatPer();
                if (succes)
                {
                    pet.skill[skillIndex][1]++;
                    this.taskCalculator.onUpdateSkillPet(pet, pet.skill[skillIndex][1]);
                    player.okDialog(string.Format(player.Language.UpTierPetOK, petSkill.getName(player), pet.skill[skillIndex][1]));
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Bạn đã nâng cấp %s lên cấp %s  cho pet %s!", petSkill.name, pet.skill[skillIndex][1], pet.getNameWithoutStar(player))).setObj(pet));
                }
                else
                {
                    player.redDialog(player.Language.UpgradeFail);
                }
            }
            else
            {
                player.redDialog(player.Language.SkillIsMaxLevel);
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

    public void showInputDialog(int dialogId, String dialogTitle, params string[] optionText)
    {
        this.showInputDialog(dialogId, dialogTitle, optionText, null);
    }

    public void showInputRevertDialog(int dialogId, String dialogTitle, params string[] optionText)
    {
        this.showInputDialog(dialogId, dialogTitle, optionText.Reverse().ToArray(), null);
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
                        player.redDialog(player.Language.PlayerHasABatte);
                    }
                }
                else
                {
                    player.petNotFollow();
                }
            }
            else
            {
                player.redDialog(player.Language.PlayerQuit);
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
            player.redDialog(player.Language.PleaseWait);
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
                player.redDialog(player.Language.PkPoinHigher);
            }
        }
        else
        {
            player.redDialog(player.Language.BanPkInMap);
        }
    }

    public void confirmpk()
    {
        if (PlayerManager.Instance.waitUserPKs.Any(x => x.Active.user.user_id == this.player.user.user_id))
        {
            player.redDialog("Bạn đang chờ người chơi khác PK với bạn, vui lòng chờ đợi");
            return;
        }
        int user_id = (int)objectPerformed.get(MenuController.OBJKEY_USER_ID_PK);
        Item cardPK = (Item)objectPerformed.get(MenuController.OBJKEY_ITEM_PK);
        objectPerformed.Remove(MenuController.OBJKEY_ITEM_PK);
        objectPerformed.Remove(MenuController.OBJKEY_USER_ID_PK);
        Player playerPassive = PlayerManager.get(user_id);
        ClanMember passiveClanMember = playerPassive.controller.getClan();
        if (playerPassive != player && playerPassive != null)
        {
            if (playerPassive.AntiPK)
            {
                player.redDialog(player.Language.PlayerAntiPK);
                return;
            }
            if (!playerPassive.playerData.isAdmin)
            {
                if (playerPassive != null)
                {
                    Pet ownPet = playerPassive.getPet();
                    if (ownPet != null)
                    {
                        if (playerPassive.controller.getPetBattle() == null)
                        {
                            if (checkCount(cardPK, 1))
                            {
                                if (ownPet.hp <= 0)
                                {
                                    ownPet.hp = 1;
                                }
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
                            WaitUserPK waitUserPK = new WaitUserPK((GopetPlace)player.getPlace(), this.player, playerPassive, cardPK.itemTemplateId);
                            PlayerManager.Instance.waitUserPKs.Add(waitUserPK);
                        }
                    }
                    else
                    {
                        player.petNotFollow();
                    }
                }
                else
                {
                    player.redDialog(player.Language.PlayerQuit);
                }
            }
            else
            {
                player.redDialog(player.Language.PlayerIsAdmin);
            }
        }
    }

    private void showChallenge(Player playerInvite, int price)
    {
        if (playerInvite.controller.getPetBattle() == null)
        {
            MenuController.showYNDialog(MenuController.DIALOG_INVITE_CHALLENGE, string.Format(playerInvite.Language.InviteChanllenge, playerInvite.playerData.name, Utilities.FormatNumber(price)), player);
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
                            player.redDialog(player.Language.In2PetHavePetDie);
                            playerInvite.redDialog(playerInvite.Language.In2PetHavePetDie);
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
                        player.redDialog(player.Language.PlayerIsNotInThisZone);
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
                        player.okDialog(string.Format(player.Language.UpStarPetOK, mypet.getNameWithStar(player)));
                        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Nâng sao thú cưng %s lên %s sao", mypet.getPetTemplate().getName(player), mypet.star)));
                    }
                    else
                    {
                        player.redDialog(player.Language.ItemPartNeedToUpStar, countNeed);
                    }
                }
                else
                {
                    player.redDialog(player.Language.ItemPartNeedToUpStarWrong);
                }
            }
            else
            {
                player.redDialog(player.Language.PetIsMaxStar);
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
            Message m = messagePetService(GopetCMD.TATTOO);
            m.putsbyte(GopetCMD.TATTOO_INIT_SCREEN);
            m.putInt(GopetManager.LVL_REQUIRE_PET_TATTO.Length);
            for (int i = 0; i < GopetManager.LVL_REQUIRE_PET_TATTO.Length; i++)
            {
                if (i >= pet.tatto.Count)
                {
                    if (indexUnlock > i)
                    {
                        m.putInt(0);
                        m.putUTF(player.Language.NonTatto);
                        m.putsbyte(i + 1);
                        m.putUTF("tatoos/0.png");
                    }
                    else
                    {
                        m.putInt(0);
                        m.putUTF(string.Format(player.Language.PetTattoMilestones, GopetManager.LVL_REQUIRE_PET_TATTO[i]));
                        m.putsbyte(i + 1);
                        m.putUTF("tatoos/-1.png");
                    }
                }
                else
                {
                    PetTatto petTatto = pet.tatto.get(i);
                    PetTattoTemplate petTattoTemplate = petTatto.Template;
                    m.putInt(petTatto.tattoId);
                    m.putUTF(petTatto.getName(player));
                    m.putsbyte(i + 1);
                    m.putUTF(petTattoTemplate.iconPath);
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
                    player.okDialog(string.Format(player.Language.TattoOK, petTatto.getName(player)));
                    pet.applyInfo(player);
                    subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                    showPetTattoUI();
                }
                else
                {
                    player.redDialog(player.Language.PetNonUnlockTattoMilestones);
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
            if (petTattoTemplate.percent > Utilities.NextFloatPer())
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
                    player.okDialog(string.Format(player.Language.RemoveTattoOK, tatto.Template.getName(player)));
                    subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                    objectPerformed.Remove(MenuController.OBJKEY_TATTO_ID_REMOVE);
                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Xóa xăm %s cho pet %s", tatto.Template.name, p.getNameWithoutStar(player))));
                }
            }
        }
    }

    public JArrayList<Popup> onReiceiveGift(int[][]? gift)
    {
        JArrayList<Popup> popups = new();
        if (gift == null)
        {
            return popups;
        }
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
                        item.SourcesItem.Add(Gopet.Data.item.ItemSource.TỪ_GIFT_CODE);
                        if (giftInfo.Length >= 4)
                        {
                            item.canTrade = giftInfo[3] == 1;
                        }
                        if (!item.getTemp().isStackable)
                        {
                            for (int j = 0; j < count; j++)
                            {
                                player.addItemToInventory(new Item(itemId) { SourcesItem = new CopyOnWriteArrayList<Gopet.Data.item.ItemSource>(Gopet.Data.item.ItemSource.TỪ_GIFT_CODE) });
                            }
                            popups.add(new Popup(item.getName(player) + " x" + count));
                        }
                        else
                        {
                            item.count = count;
                            player.addItemToInventory(item);
                            popups.add(new Popup(item.getName(player)));
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
                                item.SourcesItem.Add(Gopet.Data.item.ItemSource.TỪ_GIFT_CODE);
                                item.count = giftInfo[3];
                                player.addItemToInventory(item);
                                popups.add(new Popup(item.getName(player)));
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
                            popups.add(new Popup(Utilities.FormatNumber(giftInfo[1]) + player.Language.AddExpForPetFollow));
                        }
                    }
                    break;

                case GopetManager.GIFT_ENERGY:
                    {
                        player.addStar(giftInfo[1]);
                        popups.add(new Popup(Utilities.FormatNumber(giftInfo[1]) + player.Language.Energy));
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
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PET_TIER_TWO);
                                        break;
                                    case -126:
                                        player.playerData.AccumulatedPoint += count;
                                        popups.add(new Popup(string.Format(player.Language.GiftAccumulatedPoint, count)));
                                        flag = true;
                                        break;
                                    case -127:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_SILVER2);
                                        break;
                                    case -128:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_PET_TIER_THREE);
                                        break;
                                    case -129:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PET_TIER_ONE);
                                        break;
                                    case -130:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_WING_TIER_1);
                                        break;
                                    case -131:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_WING_TIER_2);
                                        break;
                                    case -132:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_WING_TIER_3);
                                        break;
                                    case -133:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_HAI_TAC);
                                        break;
                                    case -134:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_TINH_VAN);
                                        break;
                                    case -135:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_HOANG_KIM);
                                        break;
                                    case -136:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_PET_TIER_FOUR);
                                        break;
                                    case -137:
                                        itemId = Utilities.RandomArray(GopetManager.ID_ITEM_PART_PET_TIER_FIVE);
                                        break;
                                }
                                if (flag) break;
                                Item item = new Item(itemId);
                                item.SourcesItem.Add(Gopet.Data.item.ItemSource.TỪ_GIFT_CODE);
                                if (!item.getTemp().isStackable)
                                {
                                    for (int j = 0; j < count; j++)
                                    {
                                        player.addItemToInventory(new Item(itemId) { SourcesItem = new CopyOnWriteArrayList<Gopet.Data.item.ItemSource>(Gopet.Data.item.ItemSource.TỪ_GIFT_CODE) });
                                    }
                                    popups.add(new Popup(item.getName(player) + " x" + count));
                                }
                                else
                                {
                                    item.count = count;
                                    player.addItemToInventory(item);
                                    popups.add(new Popup(item.getName(player)));
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
                            popups.add(new Popup(item.getName(player)));
                        }
                    }
                    break;
                case GopetManager.GIFT_EVENT_POINT:
                    {
                        player.playerData.EventPoint += giftInfo[1];
                        popups.add(new Popup(string.Format(player.Language.GiftEventPoint, giftInfo[1])));
                        break;
                    }
                case GopetManager.GIFT_FUND_CLAN:
                    {
                        ClanMember clanMember = getClan();
                        if (clanMember != null)
                        {
                            clanMember.clan.addFund(giftInfo[1], clanMember);
                            popups.add(new Popup(string.Format(player.Language.GiftFundClan, Utilities.FormatNumber(giftInfo[1]))));
                        }
                        break;
                    }
                case GopetManager.GIFT_TITLE:
                    {
                        int titleId = giftInfo[1];
                        bool isInfinity = giftInfo[2] == 1;
                        if (GopetManager.AchievementMAP.ContainsKey(titleId))
                        {
                            Achievement achievement = new Achievement(titleId);
                            if (isInfinity)
                            {
                                var achsDuplicate = player.playerData.achievements.Where(p => p.IdTemplate == achievement.IdTemplate && !p.Expire.HasValue);
                                if (achsDuplicate.Any())
                                {
                                    continue;
                                }
                                else
                                    goto ADD_TITLE;
                            }
                            else
                            {
                                int min = giftInfo[3];
                                int hours = giftInfo.Length > 4 ? giftInfo[4] : 0;
                                int day = giftInfo.Length > 5 ? giftInfo[5] : 0;
                                int month = giftInfo.Length > 6 ? giftInfo[6] : 0;
                                int year = giftInfo.Length > 7 ? giftInfo[7] : 0;
                                achievement.Expire = DateTime.Now.AddMinutes(min).AddHours(hours).AddDays(day).AddMonths(month).AddYears(year);
                                goto ADD_TITLE;
                            }
                        ADD_TITLE:
                            player.playerData.addAchivement(achievement, player);
                            popups.add(new Popup(achievement.Template.getName(player)));
                        }
                    }
                    break;
                case GopetManager.GIFT_SKIN:
                    {
                        int itemId = giftInfo[1];
                        bool isInfinity = giftInfo[2] == 1;
                        if (!GopetManager.itemTemplate.ContainsKey(itemId))
                            continue;
                        Item item = new Item(itemId);
                        item.count = 1;
                        item.SourcesItem.Add(Gopet.Data.item.ItemSource.TỪ_GIFT_CODE);
                        if (isInfinity)
                        {
                            player.addItemToInventory(item, GopetManager.SKIN_INVENTORY);
                            popups.add(new Popup(item.getName(player)));
                            continue;
                        }

                        int min = giftInfo[3];
                        int hours = giftInfo.Length > 4 ? giftInfo[4] : 0;
                        int day = giftInfo.Length > 5 ? giftInfo[5] : 0;
                        item.expire = Utilities.CurrentTimeMillis + (min * 60 * 1000) + (hours * 60 * 60 * 1000) + (day * 24 * 60 * 60 * 1000);
                        player.addItemToInventory(item, GopetManager.SKIN_INVENTORY);
                        popups.add(new Popup(item.getName(player)));
                    }
                    break;
                case GopetManager.GIFT_PET_TRIAL:
                    {
                        int petId = giftInfo[1];
                        Pet pet = new Pet(petId);
                        bool isInfinity = giftInfo[2] == 1;
                        if (!isInfinity)
                        {
                            int min = giftInfo[3];
                            int hours = giftInfo.Length > 4 ? giftInfo[4] : 0;
                            int day = giftInfo.Length > 5 ? giftInfo[5] : 0;
                            int month = giftInfo.Length > 6 ? giftInfo[6] : 0;
                            int year = giftInfo.Length > 7 ? giftInfo[7] : 0;
                            pet.Expire = DateTime.Now.AddMinutes(min).AddHours(hours).AddDays(day).AddMonths(month).AddYears(year);
                        }
                        player.playerData.addPet(pet, player);
                        popups.add(new Popup(pet.getNameWithoutStar(player)));
                    }
                    break;
            }
        }
        return popups;
    }

    public void showImageDialog(int id, int w, int h, String image, int frameNum, int frameDelay)
    {
        Message m = new Message(GopetCMD.COMMAND_GUIDER);
        m.putsbyte(GopetCMD.GUIDER_IMGDIALOG);
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
        Message m = messagePetService(GopetCMD.SHOW_GEM_INVENTORY);
        m.putInt(items.Count);
        foreach (Item item in items)
        {
            m.putInt(item.itemId);
            m.putInt(item.Template.iconId);
            m.putUTF(item.getEquipName(player));
            m.putInt(item.lvl);
        }
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
        MenuController.showYNDialog(MenuController.DIALOG_CONFIRM_REMOVE_GEM, player.Language.AskRemoveGem, player);
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
            player.redDialog(player.Language.WasDeleteItem);
        }
    }

    private void sendRemoveGemItem(int itemId)
    {
        Message m = messagePetService(GopetCMD.REMOVE_GEM_ITEM);
        m.putInt(itemId);
        m.cleanup();
        player.session.sendMessage(m);
    }

    public void sendGemItemInfo(Item item)
    {
        Message m = messagePetService(GopetCMD.SEND_GEM_INFo);
        m.putInt(item.itemId);
        m.putUTF(item.getTemp().getIconPath());
        m.putUTF(item.getEquipName(player));
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
                player.redDialog(player.Language.EquipGemFailByWrongItem);
                return;
            }

            if (equipItem != null)
            {
                if (equipItem.gemInfo == null)
                {
                    player.playerData.getInventoryOrCreate(GopetManager.GEM_INVENTORY).remove(gem);
                    ItemGem itemGem = new ItemGem();
                    itemGem.element = gem.Template.element;
                    itemGem.itemTemplateId = (gem.itemTemplateId);
                    itemGem.lvl = (gem.lvl);
                    itemGem.option = (gem.option);
                    itemGem.optionValue = (gem.optionValue);
                    itemGem.name = (gem.getTemp().getName(player));
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
                    player.redDialog(player.Language.ItemHasGem);
                }
            }
            else
            {
                player.redDialog(player.Language.CannotFoundEquip);
            }
        }
        else
        {
            player.fastAction();
        }
    }

    public void unequipGem(int itemId)
    {
        objectPerformed.put(MenuController.OBJKEY_ID_ITEM_REMOVE_GEM, itemId);
        MenuController.showYNDialog(MenuController.DIALOG_ASK_REMOVE_GEM, player.Language.AskUnequipGem, player);
    }

    public void confirmUnequipGem(int itemId)
    {
        Item item = selectItemEquipByItemId(itemId);
        if (item != null)
        {
            if (item.gemInfo == null)
            {
                player.redDialog(player.Language.ItemUnequipGem);
            }
            else
            {
                if (item.gemInfo.timeUnequip > 0)
                {
                    player.redDialog(player.Language.GemIsRemoving);
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
            player.redDialog(player.Language.ItemNotFound);
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
                        recoveryGem.SourcesItem.Add(Gopet.Data.item.ItemSource.TỪ_VẬT_PHẨM_KHÁC_SINH_RA);
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
        MenuController.showYNDialog(MenuController.DIALOG_ASK_FAST_REMOVE_GEM, string.Format(player.Language.AskWantUseFastRemoveGem, Utilities.FormatNumber(GopetManager.PRICE_UNEQUIP_GEM)), player);
    }

    private void showListClan()
    {
        var listClan = ClanManager.clans.ToArray().Where(p => p != null && p.getMemberByUserId(p.getLeaderId()) != null).ToArray();
        Message m = clanMessage(GopetCMD.GUILD_LIST);
        m.putUTF("");
        m.putsbyte(0);
        m.putsbyte(0);
        m.putInt(listClan.Length);
        foreach (Clan clan in listClan)
        {
            m.putInt(clan.getClanId());
            m.putInt(-1);
            m.putUTF(player.Language.Clan + clan.getName() + string.Format(player.Language.ClanLeaderDescription + " {0} ", clan.getMemberByUserId(clan.getLeaderId()).name));
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
                    MenuController.showYNDialog(MenuController.DIALOG_ASK_REQUEST_JOIN_CLAN, string.Format(player.Language.AskWantJoinClan, clanName), player);
                }
                else
                {
                    player.redDialog(player.Language.ClanNotFound);
                }
            }
            else
            {
                player.redDialog(player.Language.ClanSearchLaw);
            }
        }
        else
        {
            player.redDialog(player.Language.YouHaveClan);
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
                m.putUTF(clanMember1.name + string.Format(player.Language.ClanDutyDescription, clanMember1.getDutyName(player)));
                m.putUTF(Utilities.Format(player.Language.ClanFundDescription, Utilities.FormatNumber(clanMember1.fundDonate)));
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
                player.redDialog(player.Language.HaveJoinClanRequest);
            }
            else
            {
                if (clan.getBannedJoinRequestId().Contains(player.user.user_id))
                {
                    player.redDialog(player.Language.ClanBlockYouJoinRequest);
                }
                else if (clan.curMember >= clan.maxMember)
                {
                    player.redDialog(player.Language.ClanHaveMaxMember);
                }
                else
                {
                    clan.addJoinRequest(player.user.user_id, player.playerData.name, player.playerData.avatarPath);
                    player.okDialog(player.Language.SendClanJoinRequestOK);
                }
            }
        }
    }

    public void notClan()
    {
        player.redDialog(player.Language.YouNotHaveClan);
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
                            m.putUTF(player.Language.NotHaveSkill);
                            m.putUTF(player.Language.NotHaveSkill);
                            m.putUTF(player.Language.NotHaveSkill);
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
                    player.redDialog(string.Format(player.Language.OtherPlayerNotHaveAClan, olPlayer.playerData.name));
                }
            }
        }
        else
        {
            player.redDialog(player.Language.PlayerOffline);
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
            if (clan.slotSkill >= GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN.Length)
            {
                player.redDialog(player.Language.PlayerWasUnlockedAllSlotSkillClan);
            }
            else
            {
                if (clan.lvl >= GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[clan.slotSkill] && clan.fund >= GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill])
                {
                    MenuController.showYNDialog(MenuController.DIALOG_ASK_UNLOCK_SLOT_SKILL_CLAN, string.Format(player.Language.AskUnlockSlotSkillClan, Utilities.FormatNumber(GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill])), player);
                }
                else
                {
                    player.redDialog(player.Language.UnlockSkillSlotLaw, GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[clan.slotSkill], Utilities.FormatNumber(GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill]));
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
                        player.redDialog(player.Language.SlotSkillClanIsNotUnlock);
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
                        player.redDialog(player.Language.YouEnoughPermission);
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
            player.redDialog(player.Language.BugWarning);
        }
    }

    public void notEnoughFundClan()
    {
        player.redDialog(player.Language.NotEnoughFundClan);
    }

    public void notEnoughGrowthPointClan()
    {
        player.redDialog(player.Language.NotEnoughGrowthPoint);
    }

    public void notEnoughLua()
    {
        player.redDialog(player.Language.NotEnoughLua + " (lua)");
    }

    private void kickClanMem(int memberId)
    {
        ClanMember mem = getClan();
        if (mem != null)
        {
            ClanMember memberIsKicked = mem.getClan().getMemberByUserId(memberId);
            if (memberIsKicked == mem)
            {
                player.redDialog(player.Language.YouCannotManipulateYourself);
            }
            else if (memberIsKicked == null)
            {
                player.redDialog(player.Language.OtherPlayerNotHaveAClan);
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
                            player.okDialog(player.Language.ClanKickOK);
                        }
                        else
                        {
                            player.redDialog(player.Language.YouCanNotKickLeader);
                        }
                    }
                }
                else
                {
                    player.redDialog(player.Language.YouEnoughPermission);
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
            showInputDialog(MenuController.INPUT_DIALOG_CHALLENGE_INVITE, player.Language.PriceBet, new String[] { player.Language.PriceGemBetMatch });
        }
        else
        {
            player.redDialog(player.Language.PlayerOffline);
        }
    }

    public void unfollowPet(Pet pet)
    {
        Place p = player.getPlace();
        if (p != null)
        {
            Message m = messagePetService(GopetCMD.PET_UNFOLLOW);
            m.putInt(player.user.user_id);
            m.putInt(pet.getPetIdTemplate());
            m.cleanup();
            p.sendMessage(m);
        }

    }

    private void checkBugEquipItem()
    {
        /*
        int num = 0;
        foreach (Item item in player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY))
        {
            if (item.petEuipId == 0)
            {
                player.playerData.removeItem(GopetManager.EQUIP_PET_INVENTORY, item);
                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Xóa vật phẩm bug (%s)", item.getName(player))).setObj(item));
                num++;
            }
        }

        if (num > 0)
        {
            player.redDialog(string.Format(player.Language.DuplicateItemRemove, num));
        }
        */
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
        player.redDialog(player.Language.NotEnoughItemWithCount, itemSelect.Template.getName(player), count);
    }


    public bool CheckSky(int mapId)
    {
        if (mapId >= 26 && !player.playerData.isOnSky)
        {
            player.redDialog(player.Language.LawToUnlockSkyPlace);
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
        player.redDialog(player.Language.WrongItemType);
    }

    public void showExp()
    {
        if (player.playerData.buffExp != null)
        {
            if (player.playerData.buffExp.buffExpTime > 0 && player.playerData.buffExp.Template != null)
            {
                Message m = messagePetService(GopetCMD.SHOW_EXP);
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
        Message m = messagePetService(GopetCMD.ANIMATION_MENU);
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
        Message m = messagePetService(100);
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

    public void testMsg65()
    {
        Message m = messagePetService(65);
        m.putInt(0);
        m.putInt(0);
        m.putInt(0);
        m.putInt(0);
        for (int i = 0; i < 9; i++)
        {
            byte[] bytes = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/assets/tatoos/1.png");
            sbyte[] m_buffer = bytes.sbytes();
            m.putInt(m_buffer.Length);
            m.writer().write(m_buffer);
        }
        player.session.sendMessage(m);
    }

    public void sellItem(int count, Item itemSell)
    {
        if (player.controller.checkCountItem(itemSell, count) || !itemSell.Template.isStackable)
        {
            itemSell.SourcesItem.Add(Gopet.Data.item.ItemSource.BÁN_SHOP);
            var inventory = player.playerData.items.Where(p => p.Value.Any(c => c == itemSell));
            if (!inventory.Any())
            {
                player.redDialog(player.Language.BugWarning);
                return;
            }
            if (!itemSell.Template.isStackable)
            {
                count = 1;
                player.playerData.TrashItemBackup[DateTime.Now] = itemSell;
            }
            else
            {
                player.playerData.TrashItemBackup[DateTime.Now] = new Item(itemSell.Template.itemId, count);
            }
            sbyte inentoryType = inventory.First().Key;
            int price = count * itemSell.Template.price;
            player.controller.subCountItem(itemSell, count, inentoryType);
            player.addCoin(price);
            player.okDialog(string.Format(player.Language.SellTrashItemOK, itemSell.Template.getName(player), Utilities.FormatNumber(price)) + "(ngoc)");
        }
        else
        {
            player.redDialog(player.Language.EnoughCountOfItem);
        }
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

    public void noelDaily()
    {
        DateTime dateTime = DateTime.Now;
        DateTime timeDaily = player.playerData.DailyNoelTime;
        if (dateTime.Day != timeDaily.Day || dateTime.Month != timeDaily.Month || dateTime.Year != timeDaily.Year)
        {
            var index = player.playerData.DailyNoelIndex;
            if (index < GopetManager.NOEL_DAILYS.Length)
            {
                JArrayList<Popup> popups = player.controller.onReiceiveGift(GopetManager.NOEL_DAILYS[index].Item1);
                JArrayList<String> textInfo = new();
                foreach (Popup popup in popups)
                {
                    textInfo.add(popup.getText());
                }
                player.okDialog(string.Format(player.Language.GetGiftCodeOK, String.Join(",", textInfo)));
                player.playerData.DailyNoelTime = dateTime;
                player.playerData.DailyNoelIndex++;
                HistoryManager.addHistory(new History(player).setLog($"Nhận quà điểm danh ngày {dateTime}").setObj(new { Index = index }));
            }
            else player.redDialog(player.Language.DailyNoelMax, GopetManager.NOEL_DAILYS.Length);
        }
        else player.redDialog(player.Language.DailyNoelFail);
    }

    public bool TryUseCardSkill(int skillId, int indexSlot, out Pet myPet)
    {
        myPet = player.playerData.petSelected;
        if (myPet != null)
        {
            if (myPet.skill.Any(x => x[0] == skillId))
            {
                return false;
            }

            if (indexSlot < 0 && myPet.skill.Length < 3)
            {
                if (myPet.skillPoint > 0)
                {
                    myPet.skillPoint--;
                    myPet.addSkill(skillId, 1);
                    return true;
                }
            }

            if (indexSlot >= 0 && indexSlot < 3 && indexSlot < myPet.skill.Length)
            {
                myPet.skill[indexSlot][0] = skillId;
                myPet.skill[indexSlot][1] = 1;
                return true;
            }
        }
        return false;
    }

    public static void WritePetEffect(Message message, IEnumerable<PetEffectTemplate> petEffects)
    {
        message.putInt(petEffects.Count());
        foreach (var petEffect in petEffects)
        {
            message.putInt(petEffect.FrameNum);
            message.putString(petEffect.FramePath);
            message.putShort(petEffect.vX);
            message.putShort(petEffect.vY);
            message.putbool(petEffect.IsDrawBefore);
            message.putsbyte(petEffect.Type);
            message.putInt(petEffect.FrameTime);
        }
    }
}


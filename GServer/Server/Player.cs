

using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySqlConnector;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.Net.WebSockets;
using static Gopet.Util.Utilities;
using Gopet.Data.user;
using Gopet.Language;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using Gopet.Shared.Helper;

public class Player : IHandleMessage
{
    public static readonly string[] BANNAME = new string[] { "admin", "test", "banquantri", "gofarm" };
    public ISession session { get; }
    public sbyte CLIENT_TYPE;
    public int PROVIDER;
    public Version ApplicationVersion;
    public string info;
    public int displayWidth, displayHeight;
    public string _languageCode;
    public string Refcode;
    public UserData user;
    public PlayerData playerData;
    public GameController controller;
    private GopetPlace place_;
    public long timeSaveDelta = Utilities.CurrentTimeMillis + TIME_SAVE_DATA;
    public long petHpRecovery = Utilities.CurrentTimeMillis + TIME_PET_RECOVERY;
    public const long TIME_SAVE_DATA = 1000l * 60 * 15;
    public const long TIME_PET_RECOVERY = 1000l * 3;
    public bool isPetRecovery = false;
    public int skillId_learn = -1;
    public bool IsLogin2FAOK = false;
    public bool AntiPK { get; set; } = false;
    /// <summary>
    /// Có bang hội
    /// </summary>
    public bool HaveClan
    {
        get
        {
            return this.playerData.clanId > 0;
        }
    }

    public LanguageData Language
    {
        get
        {
            return GopetManager.Language[_languageCode];
        }
    }

    public Player(ISession session_)
    {
        session = session_;
        if (session_ == null)
        {
            throw new NullReferenceException();
        }
        controller = new GameController(this, session_);
        controller.setTaskCalculator(new TaskCalculator(this));
    }

    public void setPlace(GopetPlace place)
    {
        place_ = place;
    }

    public GopetPlace getPlace()
    {
        return place_;
    }


    public virtual void onMessage(Message ms)
    {
#if DEBUG_LOG
        Console.WriteLine("PLAYER:  " + ms.id);
#endif
        try
        {
            try
            {
                if (!session.clientOK && ms.id != GopetCMD.CLIENT_INFO)
                {
                    session.Close();
                    return;
                }

                switch (ms.id)
                {
                    case GopetCMD.CLIENT_INFO:

                        CLIENT_TYPE = ms.reader().readsbyte();
                        PROVIDER = ms.reader().readInt();
                        ApplicationVersion = Version.Parse(ms.reader().readUTF());
                        info = ms.reader().readUTF();
                        displayWidth = ms.reader().readInt();
                        displayHeight = ms.reader().readInt();
                        _languageCode = ms.reader().readUTF();
                        if (!GopetManager.Language.ContainsKey(_languageCode))
                        {
                            session.setClientOK(false);
                            session.Close();
                            break;
                        }
                        Refcode = ms.reader().readUTF();
                        if (ApplicationVersion >= GopetManager.VERSION_142)
                        {
                            session.setClientOK(true);
                        }
                        else
                        {
                            redDialog(Language.OldVersionNotify);
                            session.setClientOK(false);
                            session.Close();
                        }
                        break;

                    case GopetCMD.LOGIN:
                        {
                            login(ms.reader().readUTF(), ms.reader().readUTF(), ms.reader().readUTF());
                            break;
                        }
                    case GopetCMD.CHANGE_PASSWORD:
                        requestChangePass(ms.reader().readInt(), ms.reader().readUTF(), ms.reader().readUTF());
                        break;
                    case GopetCMD.REGISTER:
                        doRegister(ms.reader().readUTF(), ms.reader().readUTF());
                        break;
                    case GopetCMD.CHARGE_MONEY_INFO:
                        MenuController.sendMenu(MenuController.MENU_ATM, this);
                        break;
                    case GopetCMD.SERVER_LIST:
                        showNormalServer();
                        break;
                    default:
                        controller.onMessage(ms);
                        break;
                }
            }
            catch (Exception e)
            {
                e.printStackTrace();

            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
#if DEBUG_LOG
            throw e;
#elif !DEBUG
Thread.Sleep(1000);
#endif
        }
    }

    private void doRegister(String username, String password)
    {
        if (ServerSetting.instance.isShowMessageWhenLogin)
        {
            okDialog(ServerSetting.instance.messageWhenLogin);
            return;
        }
        if (true)
        {
            redDialog("Chức năng này bị khóa. Để đăng ký tài khoản vui lòng vào trang web\n gopettae.com vào mục diễn đàn.");
            return;
        }
        if (CheckString(username, "^[a-z0-9]+$"))
        {
            if (username.Length >= 6 && password.Length >= 6 && username.Length < 25 && password.Length < 60)
            {
                foreach (var str in BANNAME)
                {
                    if (username.Contains(str))
                    {
                        redDialog(Language.UsernameAndPasswordCanotHaveBanName + String.Join(",", BANNAME));
                        return;
                    }
                }
                using (MySqlConnection conn = MYSQLManager.createWebMySqlConnection())
                {
                    var user = conn.QueryFirstOrDefault("SELECT * FROM `user` WHERE username = @username;", new { username = username });
                    if (user != null)
                    {
                        redDialog(Language.DuplicateNameChar);
                    }
                    else
                    {
                        conn.Execute("INSERT INTO `user`(`user_id`, `username`, `password` , `ipv4Create` , `dayCreate`, `avatar`) VALUES (NULL,@username,@password, @ipv4Create, @dayCreate, NULL)",
                            new
                            {
                                username = username,
                                password = password,
                                ipv4Create = ((IPEndPoint)session.CSocket.RemoteEndPoint).Address.ToString(),
                                dayCreate = Utilities.CurrentTimeMillis
                            });
                        okDialog(Language.RegisterOK);
                    }
                }
            }
            else
            {
                redDialog(Language.RegisterLaw);
            }
        }
        else
        {
            redDialog(Language.HaveSpecialChar);
        }
    }

    public virtual void requestChangePass(int id, string oldPass, string newPass)
    {
        if (GopetHashHelper.VerifyHash(user.password, oldPass))
        {
            if (newPass.Length < 5)
            {
                Message mW = new Message(GopetCMD.CHANGE_PASSWORD);
                mW.putUTF(Language.ChangePasswordLaw);
                mW.writer().flush();
                session.sendMessage(mW);
                return;
            }
            user.password = GopetHashHelper.ComputeHash(newPass);
            using (var conn = MYSQLManager.createWebMySqlConnection())
            {
                conn.Execute("UPDATE `user` set password = @password where user_id = @user_id", new { password = GopetHashHelper.ComputeHash(newPass), user_id = user.user_id });
            }
            okDialog(Language.ChangePasswordOK);
        }
        else
        {
            redDialog(Language.IncorrectPassword);
        }
    }

    public static void listServer(Session session_)
    {

    }

    public const long TIME_GEN = 1000 * 60 * 60 * 6;
    public virtual void update()
    {
        checkSpeed();
        UpdateHP_MP();

        if (playerData != null)
        {
            if (playerData.buffExp == null)
                playerData.buffExp = new BuffExp();

            playerData.buffExp.update();
            updatePkPoint();
        }
    }

    private Mutex m_SpeedMutex = new Mutex();
    private Stopwatch s_SpeedStopWatch = new Stopwatch();
    private TimeSpan m_SpeedTime = TimeSpan.Zero;
    private void checkSpeed()
    {
        m_SpeedMutex.WaitOne();
        try
        {
            if (s_SpeedStopWatch.IsRunning)
            {
                if (s_SpeedStopWatch.Elapsed > m_SpeedTime * 3)
                {
                    s_SpeedStopWatch.Stop();
                    session.Close();
                    return;
                }
            }
            else
            {
                m_SpeedTime = TimeSpan.FromSeconds(Utilities.nextInt(15, 30));
                Message message = GameController.messagePetService(GopetCMD.CHECK_SPEED);
                message.putInt((int)m_SpeedTime.TotalMilliseconds);
                message.cleanup();
                session.sendMessage(message);
                s_SpeedStopWatch.Restart();
                s_SpeedStopWatch.Start();
            }
        }
        finally
        {
            m_SpeedMutex.ReleaseMutex();
        }
    }

    public void onClientSpeedRespose()
    {
#if DEBUG_LOG
        GopetManager.ServerMonitor.LogInfo("onClientSpeedRespose");
#endif
        m_SpeedMutex.WaitOne();
        try
        {
            if (s_SpeedStopWatch.IsRunning)
            {
                s_SpeedStopWatch.Stop();
                if (s_SpeedStopWatch.Elapsed + TimeSpan.FromSeconds(2) < m_SpeedTime)
                {
                    //user.ban(UserData.BAN_TIME, "HackSpeed", Utilities.CurrentTimeMillis + (1000 * 60 * 60));
                    //session.Close();
                    HistoryManager.Instance.add(new History(this).setLog($"Hệ thống ban acc do người dùng speed. Trạng thái TimeSpeedCheck={m_SpeedTime}, TimeSpeedCheck + 2 giây={m_SpeedTime + TimeSpan.FromSeconds(2)}, Đồng hồ đo = {s_SpeedStopWatch.Elapsed} "));
                    return;
                }
            }
            else
            {
                session.Close();
                return;
            }
        }
        finally
        {
            m_SpeedMutex.ReleaseMutex();
        }
    }

    private long currentTimeUpdateHP_MP = Utilities.CurrentTimeMillis;

    private void UpdateHP_MP()
    {
        if (playerData.petSelected == null) return;

        if (playerData.petSelected.Expire != null)
        {
            if (playerData.petSelected.Expire < DateTime.Now)
            {
                controller.unfollowPet(playerData.petSelected);
                playerData.petSelected = null;
                Popup(Language.PetExpire);
                return;
            }
        }

        if (playerData.petSelected.petDieByPK && Utilities.CurrentTimeMillis > playerData.petSelected.TimeDieZ)
        {
            playerData.petSelected.petDieByPK = false;
        }

        if (isPetRecovery && this.controller.getPetBattle() == null && petHpRecovery < Utilities.CurrentTimeMillis && !playerData.petSelected.petDieByPK && Utilities.CurrentTimeMillis > controller.delayTimeHealPet && Utilities.CurrentTimeMillis > playerData.petSelected.TimeDieZ)
        {
            if (playerData.petSelected.hp < 0)
            {
                playerData.petSelected.hp = 0;
                playerData.petSelected.mp = 0;
            }
            playerData.petSelected.addHpPet((int)Utilities.GetValueFromPercent(20f, playerData.petSelected.maxHp));
            playerData.petSelected.addMp((int)Utilities.GetValueFromPercent(20f, playerData.petSelected.maxMp));
            controller.sendMyPetInfo();
            petHpRecovery = Utilities.CurrentTimeMillis + TIME_PET_RECOVERY;
        }
    }

    public virtual void redDialog(String text)
    {
        Message ms = new Message((sbyte)10);
        ms.putString(text);
        ms.cleanup();
        session.sendMessage(ms);
    }

    public void redDialog(String text, params object[] obj)
    {
        redDialog(string.Format(text, obj));
    }

    public void show2FADialog()
    {
        controller.showInputDialog(MenuController.INPUT_OTP_2FA, Language.TwoFALoginTitle, " OTP: ");
    }

    public void ProcessingUser(MySqlConnection conn)
    {
        if (user.role == UserData.ROLE_NON_ACTIVE)
        {
            redDialog(Language.AccountNonAcitve);
            return;
        }
        switch (user.isBaned)
        {
            case UserData.BAN_INFINITE:
                {
                    this.redDialog(string.Format(Language.AccountBanInfinity, user.banReason));
                    Thread.Sleep(100);
                    this.session.Close();
                    conn.Close();
                    return;
                }
            case UserData.BAN_TIME:
                {
                    if (Utilities.CurrentTimeMillis < user.banTime)
                    {
                        long deltaTime = user.banTime - Utilities.CurrentTimeMillis;
                        int hours = (int)(deltaTime / 1000 / 60 / 60);
                        int min = (int)((deltaTime - (hours * 1000 * 60 * 60)) / 1000 / 60);
                        this.redDialog(string.Format(Language.AccountBanTime, user.banReason, hours, min));
                        Thread.Sleep(100);
                        this.session.Close();
                        conn.Close();
                        return;
                    }
                    else
                    {
                        user.isBaned = UserData.BAN_NONE;
                        conn.Execute("update user set isBaned = DEFAULT where user_id = @user_id", new { user_id = user.user_id });
                    }
                    break;
                }
        }
        if (user.secretKey != null && !IsLogin2FAOK)
        {
            show2FADialog();
            return;
        }
        Player player2 = PlayerManager.get(user.user_id);
        if (player2 != null)
        {
            player2.redDialog(Language.AccountLogingDuplicate);
            this.redDialog(Language.AccountLogingDuplicate);
            player2.session.Close();
            this.session.Close();
            return;
        }

        long timeWait = PlayerManager.GetTimeMillisWaitLogin(user.user_id);
        if (timeWait > 0)
        {
            this.redDialog(string.Format(Language.WaitLoging, timeWait / 1000));
            Thread.Sleep(500);
            this.session.Close();
            return;
        }
        using (MySqlConnection gameconn = MYSQLManager.create())
        {
            playerData = gameconn.QueryFirstOrDefault<PlayerData>("SELECT * FROM `player` where user_id = " + user.user_id);
            if (playerData != null)
            {
                if (playerData.FlowerGold == -1)
                    playerData.FlowerGold = Math.Max(0, playerData.NumGiveFlowerGold);
                if (playerData.FlowerCoin == -1)
                    playerData.FlowerCoin = Math.Max(0, playerData.NumGiveFlowerGem);
                PlayerManager.put(this);
                var connList = gameconn.Query<FriendRequest>("SELECT * FROM `request_add_friend` WHERE `request_add_friend`.`targetId` = @userId;", new { userId = user.user_id });
                if (connList.Any())
                {
                    foreach (var item in connList)
                    {
                        if (!playerData.BlockFriendLists.Contains(item.userId) && !playerData.RequestAddFriends.Contains(item.userId) && !playerData.ListFriends.Contains(item.userId))
                        {
                            playerData.RequestAddFriends.Add(item.userId);
                        }

                        gameconn.Execute("DELETE FROM `request_add_friend` WHERE `request_add_friend`.`userId` = @userId AND `request_add_friend`.`targetId` = @targetId;", item);
                    }
                }

                var letterList = gameconn.Query<Letter>("SELECT * FROM `letter` WHERE targetId = @targetId;", new { targetId = playerData.user_id });
                if (letterList.Any())
                {
                    foreach (var letter in letterList)
                    {
                        playerData.addLetter(letter);
                    }
                    gameconn.Execute("DELETE FROM `letter` WHERE `letter`.`targetId` = @targetId", new { targetId = playerData.user_id });
                }

                foreach (var item in playerData.TrashItemBackup.Where(t => t.Key.AddDays(3) < DateTime.Now))
                {
                    playerData.TrashItemBackup.Remove(item.Key);
                }
            }
            var kioskList = gameconn.Query("SELECT * FROM `kiosk_recovery` where user_id = @user_id", new { user_id = this.user.user_id });
            if (kioskList.Any())
            {
                foreach (var item in kioskList)
                {
                    SellItem sellItem = JsonConvert.DeserializeObject<SellItem>(item.item);
                    if (sellItem.pet == null)
                    {
                        addItemToInventory(sellItem.ItemSell);
                    }
                    else
                    {
                        playerData.addPet(sellItem.pet, this);
                    }
                    if (sellItem.sumVal > 0)
                    {
                        addCoin(sellItem.sumVal);
                    }
                }
            }
            gameconn.Execute("DELETE FROM `kiosk_recovery` where user_id = @user_id", new { user_id = this.user.user_id });
            loginOK();
            controller.LoadMap();
            controller.updateAvatar();
            if (playerData != null)
            {
                controller.updateUserInfo();
                showBanner(Language.WarningPlayerWhenLogin);
                getPet()?.applyInfo(this);
                if (ServerSetting.instance.isOnlyAdminLogin)
                {
                    if (!playerData.isAdmin)
                    {
                        redDialog(Language.ServerOnlyForAdmin);
                        session.Close();
                        return;
                    }
                }

                foreach (var taskData in playerData.task)
                {
                    TaskTemplate taskTemp = GopetManager.taskTemplate[taskData.taskTemplateId];
                    if (!playerData.wasTask.ContainsZ(taskTemp.taskNeed) && taskTemp.type == TaskCalculator.TASK_TYPE_MAIN)
                    {
                        playerData.task.remove(taskData);
                        playerData.tasking.remove(taskData.taskTemplateId);
                    }
                }

                int goldPlus = 0;
                JArrayList<String> listIdRemove = new();
                var exhangeGoldData = gameconn.Query("SELECT * FROM `exchange_gold` WHERE `user_id` = @user_id", new { user_id = this.user.user_id });
                foreach (var item in exhangeGoldData)
                {
                    String id = item.id;
                    int g = item.gold;
                    addGold(g);
                    listIdRemove.add(id);
                    goldPlus += g;
                }
                if (!listIdRemove.isEmpty())
                {
                    foreach (String uuidString in listIdRemove)
                    {
                        gameconn.Execute("DELETE FROM `exchange_gold` WHERE `id`  = @id AND `user_id` = @user_id", new { id = uuidString, user_id = this.user.user_id });
                    }
                }
                if (goldPlus > 0)
                {
                    okDialog(string.Format(Language.GetGoldByCard, Utilities.FormatNumber(goldPlus)));
                }
            }
            else
            {
                controller.createChar();
            }
            controller.getTaskCalculator().update();
        }
    }

    public virtual void login(String username, String password, String version, bool SkipIpv4Check = false, bool IsPassOtp = false)
    {
        if (this.user != null && !IsPassOtp)
            return;
        IPEndPoint iPEndPoint = (IPEndPoint)this.session.CSocket.RemoteEndPoint;
        if (!SkipIpv4Check)
        {
            if (PlayerManager.Ipv4Tracker.IsLimited(iPEndPoint.Address.ToString()))
            {
                redDialog(Language.Ipv4Limited);
                return;
            }
        }
        username = username.Trim();
        password = password.Trim();
        version = version.Trim();
        if (ServerSetting.instance.isShowMessageWhenLogin)
        {
            okDialog(ServerSetting.instance.messageWhenLogin);
            return;
        }
        if (!CheckString(username, "^[a-z0-9]+$"))
        {
            redDialog(Language.HaveSpecialChar);
            return;
        }
        PlayerManager.Ipv4Tracker.Add(iPEndPoint.Address.ToString());
        using (MySqlConnection conn = MYSQLManager.createWebMySqlConnection())
        {
            try
            {
                var LockKey = conn.QueryFirstOrDefault("SELECT GET_LOCK(@username, 20) as hasLock;", new { username = "login_lock_" + username });
                UserData userData = conn.QueryFirstOrDefault<UserData>("SELECT * FROM `user` where username = @username",
                new { username = username});
                if (userData != null)
                {
                    if (!GopetHashHelper.VerifyHash(userData.password, password))
                    {
                        userData = null;
                    }
                }
                long numTry = conn.QueryFirst("SELECT COUNT(*) as TimeTryLogin FROM `login_history` WHERE IPAddress = @IPAddress AND `login_history`.`LoginTime` > @LoginTime AND UserName = @UserName", new
                {
                    IPAddress = iPEndPoint.Address.ToString(),
                    LoginTime = DateTime.Now.AddMinutes(-5),
                    UserName = username
                }).TimeTryLogin;
                if (numTry >= 10)
                {
                    if (userData != null && !string.IsNullOrEmpty(userData.email))
                    {
                        string key = userData.email + username + iPEndPoint.Address.ToString();
                        if (!PlayerManager.EmailTracker.IsLimited(key))
                        {
                            PlayerManager.EmailTracker.Add(key);
                            GopetManager.SendHtmlMailAsync(
                                userData.email,
                                "Gopet - Thông báo đăng nhập",
                                $"Tài khoản của bạn đã thử đăng nhập quá nhiều lần trong 5 phút và đã đăng nhập thành công. Nếu không phải bạn vui lòng đổi mật khẩu ngay lập tức. <br> Địa chỉ IP: <b>{iPEndPoint.Address.ToString()}</b>");
                        }
                    }
                    redDialog("Bạn đã thử đăng nhập quá nhiều lần trong 5 phút. Vui lòng thử lại sau 5 phút.");
                    return;
                }
                LoginHistory.InsertToDatabase(new LoginHistory(username, iPEndPoint.Address.ToString(), userData != null, false), conn);
                if (userData != null && LockKey != null)
                {
                    this.user = userData;
                    this.ProcessingUser(conn);
                }
                else
                {
                    loginFailed(Language.IncorrectUsePassword);
                    Thread.Sleep(1000);
                    this.session.Close();
                }
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
            finally
            {
                conn.Execute("DO RELEASE_LOCK(@username);", new { username = "login_lock_" + username });
            }
        }
    }

    public virtual void loginOK()
    {
        controller.loginOK();
        if (playerData != null)
        {
            controller.checkExpire();
            UpdateAntiPK();
        }
        HistoryManager.addHistory(new History(this).setLogin());
    }

    public virtual void loginFailed(String text)
    {
        Message ms = new Message(GopetCMD.LOGIN_FAILED);
        ms.putString(text);
        ms.writer().flush();
        session.sendMessage(ms);
    }

    public virtual void onDisconnected()
    {
        try
        {
            Place place = getPlace();
            if (place != null)
            {
                place.remove(this);
            }
            if (playerData != null)
            {
                playerData.save();
            }
            if (user != null && playerData != null)
            {
                PlayerManager.remove(this);
                HistoryManager.addHistory(new History(this).setLogout());
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    public virtual void Popup(String text)
    {
        Message m = new Message(GopetCMD.SERVER_MESSAGE);
        m.putsbyte(GopetCMD.POPUP_MESSAGE);
        m.putUTF(text);
        m.writer().flush();
        session.sendMessage(m);
    }

    public virtual void showBanner(String text)
    {
        Message m = new Message(GopetCMD.SERVER_MESSAGE);
        m.putsbyte(GopetCMD.BANNER_MESSAGE);
        m.putUTF(text);
        m.writer().flush();
        session.sendMessage(m);
    }

    public virtual void okDialog(String str)
    {
        Message m = new Message(71);
        m.putsbyte(0);
        m.putUTF(str);
        m.writer().flush();
        session.sendMessage(m);
    }

    public void okDialog(String str, params object[] objects)
    {
        okDialog(string.Format(str, objects));
    }

    public virtual void notEnoughHp()
    {
        Popup(Language.NotEnoughtHP);
    }

    public virtual void notEnoughEnergy()
    {
        Popup(Language.NotEnoughtEnergy);
    }

    public virtual void addCoin(long coin)
    {
        playerData.coin += coin;
        controller.updateUserInfo();
    }


    public virtual bool CanAddSpendGold
    {
        get
        {
            return DateTime.Now <= new DateTime(2025, 2, 1, 21, 0, 0);
        }
    }

    public virtual void addGold(long gold)
    {
        playerData.gold += gold;
        controller.updateUserInfo();
        if (gold < 0 && CanAddSpendGold)
        {
            playerData.spendGold -= gold;
            TopData topData = TopSpendGold.Instance.find(playerData.user_id);
            if (topData != null)
            {
                topData.desc = Utilities.Format("Hạng %s: Đã tiêu %s (vang)", TopSpendGold.Instance.datas.IndexOf(topData) + 1, Utilities.FormatNumber(playerData.spendGold));
            }
        }
    }

    public virtual void AddLua(long lua)
    {
        playerData.lua += lua;
        controller.updateUserInfo();
    }

    public virtual void MineLua(long lua)
    {
        playerData.lua -= lua;
        controller.updateUserInfo();
    }

    public virtual void mineCoin(long coin)
    {
        playerData.coin -= coin;
        controller.updateUserInfo();
    }

    public virtual void mineGold(long gold)
    {
        playerData.gold -= gold;

        controller.updateUserInfo();
        if (CanAddSpendGold)
        {
            playerData.spendGold += gold;
            TopData topData = TopSpendGold.Instance.find(playerData.user_id);
            if (topData != null)
            {
                topData.desc = Utilities.Format("Hạng %s: Đã tiêu %s (vang)", TopSpendGold.Instance.datas.IndexOf(topData) + 1, Utilities.FormatNumber(playerData.spendGold));
            }
        }
    }

    public virtual bool checkCoin(long value)
    {
        return playerData.coin >= value;
    }

    public virtual bool checkGold(long value)
    {
        return playerData.gold >= value;
    }

    public virtual bool checkStar(int value)
    {
        return playerData.star >= value;
    }

    public virtual bool checkLua(long value)
    {
        return playerData.lua >= value;
    }

    public virtual void addStar(int star)
    {
        playerData.star += star;
        controller.updateUserInfo();
    }

    public void MineStar(int star)
    {
        playerData.star -= star;
        controller.updateUserInfo();
    }

    public void petNotFollow()
    {
        redDialog(Language.PetNotFollow);
    }

    public Pet getPet()
    {
        return playerData.petSelected;
    }

    public void notEnoughStar()
    {
        redDialog(Language.NotEnoughtStar);
    }

    public void addItemToInventory(Item item, sbyte inventory)
    {
        if (item.getTemp().isStackable)
        {
            if (item.count == 0)
            {
                return;
            }
            Item itemFlag = null;
            foreach (Item item1 in playerData.getInventoryOrCreate(inventory))
            {
                if (item1.itemTemplateId == item.itemTemplateId && item.canTrade == item1.canTrade)
                {
                    itemFlag = item1;
                    break;
                }
            }
            if (itemFlag != null)
            {
                itemFlag.count += item.count;
            }
            else
            {
                playerData.addItem(inventory, item);
            }
        }
        else
        {
            playerData.addItem(inventory, item);
        }
    }

    public void addItemToNormalInventory(Item item)
    {
        addItemToInventory(item, GopetManager.NORMAL_INVENTORY);
    }

    public void addItemToInventory(Item Item)
    {
        if (Item.getTemp().isStackable && Item.count == 0)
        {
            return;
        }

        switch (Item.getTemp().getType())
        {
            case GopetManager.PET_EQUIP_HAT:
            case GopetManager.PET_EQUIP_GLOVE:
            case GopetManager.PET_EQUIP_ARMOUR:
            case GopetManager.PET_EQUIP_WEAPON:
            case GopetManager.PET_EQUIP_SHOE:
                playerData.addItem(GopetManager.EQUIP_PET_INVENTORY, Item);
                break;
            case GopetManager.ITEM_GEM:
                playerData.addItem(GopetManager.GEM_INVENTORY, Item);
                break;
            case GopetManager.WING_ITEM:
                playerData.addItem(GopetManager.WING_INVENTORY, Item);
                break;
            case GopetManager.SKIN_ITEM:
                playerData.addItem(GopetManager.SKIN_INVENTORY, Item);
                break;
            case GopetManager.ITEM_MONEY:
                playerData.addItem(GopetManager.MONEY_INVENTORY, Item);
                break;
            default:
                addItemToNormalInventory(Item);
                break;
        }
    }

    private void updatePkPoint()
    {
        if (playerData.pkPoint > 0)
        {
            if (Utilities.CurrentTimeMillis - GopetManager.TIME_DECREASE_PK_POINT >= playerData.pkPointTime.GetTimeMillis())
            {
                playerData.pkPoint--;
                playerData.pkPointTime = DateTime.Now;
            }
        }
        else
        {
            if (Utilities.CurrentTimeMillis - GopetManager.TIME_DECREASE_PK_POINT >= playerData.pkPointTime.GetTimeMillis())
            {
                playerData.pkPointTime = DateTime.Now;
            }
        }
    }

    public bool checkIsAdmin()
    {
        if (playerData != null)
        {
            return playerData.isAdmin;
        }
        return false;
    }


    public void fastAction()
    {
        redDialog(Language.FastAction);
    }

    public void itemError(string where = "")
    {
        if (string.IsNullOrEmpty(where))
        {
            redDialog(Language.ErrorItem);
        }
        else
        {
            redDialog(Language.ErrorItem + where);
        }
    }

    public void UpdateAntiPK()
    {
        this.AntiPK = false;
        Item wing = playerData.wing;
        if (wing != null)
        {
            UpdateAntiPK(wing.Template.itemOption, wing.Template.itemOptionValue);
        }
    }

    void UpdateAntiPK(int[] optionId, int[] optionValues)
    {
        for (int i = 0; i < optionId.Length; i++)
        {
            switch (optionId[i])
            {
                case ItemInfo.OptionType.OPTION_ANTI_PK:
                    this.AntiPK = optionValues[i] == 1;
                    break;
            }
        }
    }


    public void showNormalServer()
    {
        showListServer(GopetManager.ServerInfos.Where(p => !p.NeedAdmin).ToArray());
    }

    public void showListServer(ServerInfo[] serverInfos)
    {
        ServerInfo[] infos = serverInfos.Where((s) =>
        {
            if (s.GreaterThanEquals != null)
            {
                if (!(this.ApplicationVersion >= s.GreaterThanEquals))
                    return false;
            }
            if (s.LessThan != null)
            {
                if (this.ApplicationVersion > s.LessThan)
                    return false;
            }
            return true;
        }).ToArray();


        Message m = new Message(GopetCMD.SERVER_LIST);
        m.putInt(infos.Length);
        for (int i = 0; i < infos.Length; i++)
        {
            ServerInfo serverInfo = infos[i];
            m.putUTF(serverInfo.Name);
            m.putUTF(serverInfo.IpAddress);
            m.putInt(serverInfo.Port);
            m.putInt(serverInfo.Port);
            m.putInt(serverInfo.Port);
        }

        for (int i = 0; i < infos.Length; i++)
        {
            ServerInfo serverInfo = infos[i];
            m.putbool(true);
            m.putbool(true);
        }
        session.sendMessage(m);
    }
}

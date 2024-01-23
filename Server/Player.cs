 

public class Player : IHandleMessage {

    public static readonly String[] BANNAME = new String[]{"admin", "test", "banquantri", "gofarm"};
    public Session session { get; }
    public sbyte CLIENT_TYPE;
    public int PROVIDER;
    public String version;
    public String info;
    public int displayWidth, displayHeight;
    public String language;
    public String Refcode, ApplicationVersion;
    public UserData user;
    public PlayerData playerData;
    public GameController controller;
    private Place place_;
    public long timeSaveDelta = Utilities.CurrentTimeMillis + TIME_SAVE_DATA;
    public long petHpRecovery = Utilities.CurrentTimeMillis + TIME_PET_RECOVERY;
    public const long TIME_SAVE_DATA = 1000l * 60 * 15;
    public const long TIME_PET_RECOVERY = 1000l * 3;
    public bool isPetRecovery = false;
    public int skillId_learn = -1;

    public Player(Session session_)   {
        session = session_;
        if (session_ == null) {
            throw new NullReferenceException();
        }
        controller = new GameController(this, session_);
        controller.setTaskCalculator(new TaskCalculator(this));
    }

    public void setPlace(Place place) {
        place_ = place;
    }

    public Place getPlace() {
        return place_;
    }

     
    public void onMessage(Message ms) {
//        System.err.println(ms.id);
        try {
            if (!session.clientOK && ms.id != GopetCMD.CLIENT_INFO) {
                session.close();
                return;
            }

            switch (ms.id) {
                case GopetCMD.CLIENT_INFO: {
                    CLIENT_TYPE = ms.reader().readsbyte();
                    PROVIDER = ms.reader().readInt();
                    ApplicationVersion = ms.reader().readUTF();
                    info = ms.reader().readUTF();
                    displayWidth = ms.reader().readInt();
                    displayHeight = ms.reader().readInt();
                    language = ms.reader().readUTF();
                    Refcode = ms.reader().readUTF();
                    session.setClientOK(true);
                    break;
                }
                case GopetCMD.LOGIN: {
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
                default:
                    controller.onMessage(ms);
                    break;
            }
        } catch (Exception e) {
            e.printStackTrace();
            try {
                Thread.Sleep(1000);
            } catch (Exception ex) {
            }
        }
    }

    private void doRegister(String username, String password)   {
        if (ServerSetting.instance.isShowMessageWhenLogin()) {
            okDialog(ServerSetting.instance.getMessageWhenLogin());
            return;
        }
        if (true) {
            redDialog("Chức năng này bị khóa. Để đăng ký tài khoản vui lòng vào trang web\n gopetvn.me vào mục diễn đàn.");
            return;
        }
//        if (CheckString(username, "^[a-z0-9]+$") && CheckString(password, "^[a-z0-9]+$")) {
//            if (username.Length() >= 6 && password.Length() >= 6 && username.Length() < 25 && password.Length() < 60) {
//                for (String string : BANNAME) {
//                    if (username.contains(string)) {
//                        redDialog("Tài khoản không được phép có những từ này : " + String.Join(",", BANNAME));
//                        return;
//                    }
//                }
//                InetSocketAddress netSocket = (InetSocketAddress) session.sc.getRemoteSocketAddress();
//                PreparedStatement preparedStatement = MYSQLManager.createWebMySqlConnection().prepareStatement(Utilities.Format("SELECT * FROM `user` WHERE ipv4Create = '%s' && dayCreate > %s;", netSocket.getHostString(), Utilities.CurrentTimeMillis - (1000l * 60l * 60l * 24l * 7)));
////                try (ResultSet result = preparedStatement.executeQuery()) {
////                    if (result.next()) {
////                        redDialog("Tạo tài khoản cách nhau 1 tuần nhé");
////                        result.close();
////                        preparedStatement.getMySqlConnection().close();
////                        return;
////                    }
////                    result.close();
////                } catch (Exception e) {
////                    e.printStackTrace();
////                }
//                preparedStatement = preparedStatement.getMySqlConnection().prepareStatement(Utilities.Format("SELECT * FROM `user` WHERE username = '%s';", username));
//                try (ResultSet result = preparedStatement.executeQuery()) {
//                    if (result.next()) {
//                        redDialog("Tên tài khoản đã tồn tại rồi");
//                    } else {
//                        preparedStatement.getMySqlConnection().createStatement().execute(Utilities.Format("INSERT INTO `user`(`user_id`, `username`, `password` , `ipv4Create` , `dayCreate`, `avatar`) VALUES (NULL,'%s','%s', '%s', %s, NULL)", username, password, netSocket.getHostString(), Utilities.CurrentTimeMillis));
//                        okDialog("Đăng ký tài khoản thành công mời bạn đăng nhập");
//                    }
//                    result.close();
//                } catch (Exception e) {
//                    e.printStackTrace();
//                }
//                preparedStatement.getMySqlConnection().close();
//
//            } else {
//                redDialog("Tài khoản và mật khẩu phải có số lượng kí tự lớn hơn 6 và bé hơn 25 đối với tài khoản , bé hơn 45 đối với mật khẩu");
//            }
//        } else {
//            redDialog("Tài khoản và mật khẩu phải không chứa các kí tự đặc biệt");
//        }
    }

    public void requestChangePass(int id, String oldPass, String newPass)   {
        if (oldPass.equals(user.password)) {
            if (!CheckString(newPass, "^[a-z0-9]+$")
                    || newPass.Length() < 5) {
                Message m = new Message(GopetCMD.CHANGE_PASSWORD);
                m.putUTF("Mật khẩu phải có số lượng kí tự lớn hơn 5 và không chứa các kí tự đặc biệt");
                m.writer().flush();
                session.sendMessage(m);
                return;
            }
            user.password = newPass;
            MySqlConnection MySqlConnection = MYSQLManager.createWebMySqlConnection();
            try {
                MYSQLManager.updateSql(Utilities.Format("update user set password = '%s' where user_id = %s", newPass, user.user_id), MySqlConnection);
                Message m = new Message(GopetCMD.CHANGE_PASSWORD);
                m.putUTF("Đổi mật khẩu thành công, vui lòng nhớ kỷ thông tin");
                m.writer().flush();
                session.sendMessage(m);
            } catch (Exception e) {
                e.printStackTrace();
            }
            MySqlConnection.close();
        } else {
            Message m = new Message(GopetCMD.CHANGE_PASSWORD);
            m.writer().flush();
            session.sendMessage(m);
        }
    }

    public const void listServer(Session session_)   {
        String[] nameserver = new String[]{"Server Thử nghiệm"};
        String[] ip = new String[]{Utilities.serverIP()};
        int[] port = new int[]{Main.PORT_SERVER};
        int[] maxSession = new int[]{100};
        int[] currentSessionSize = new int[]{0};
        Message ms = new Message((sbyte) 64);
        ms.putInt(nameserver.Length);
        for (int i = 0; i < nameserver.Length; i++) {
            ms.putString(nameserver[i]);
            ms.putString(ip[i]);
            ms.putInt(port[i]);
            ms.putInt(maxSession[i]);
            ms.putInt(currentSessionSize[i]);
        }
        ms.writer().flush();
        session_.sendMessage(ms);
    }

    public const long TIME_GEN = 1000 * 60 * 60 * 6;

    public void update()   {
        UpdateHP_MP();

        if (playerData != null) {
            playerData.buffExp.update();
            updatePkPoint();
        }
    }

    private long currentTimeUpdateHP_MP = Utilities.CurrentTimeMillis;

    private void UpdateHP_MP()   {
        if (isPetRecovery && petHpRecovery < Utilities.CurrentTimeMillis && !playerData.petSelected.petDieByPK) {
            playerData.petSelected.addHp((int) Utilities.getValueFromPercent(20f, playerData.petSelected.maxHp));
            playerData.petSelected.addMp((int) Utilities.getValueFromPercent(20f, playerData.petSelected.maxMp));
            controller.sendMyPetInfo();
            petHpRecovery = Utilities.CurrentTimeMillis + TIME_PET_RECOVERY;
        }
    }

    public void redDialog(String text)   {
        Message ms = new Message((sbyte) 10);
        ms.putString(text);
        ms.cleanup();
        session.sendMessage(ms);
    }

    public void login(String username, String password, String version)   {
        if (ServerSetting.instance.isShowMessageWhenLogin()) {
            okDialog(ServerSetting.instance.getMessageWhenLogin());
            return;
        }
        if (!CheckString(username, "^[a-z0-9]+$") || !CheckString(password, "^[a-z0-9]+$")) {
            redDialog("Tồn tại ký tự lạ");
            return;
        }
        MySqlConnection MySqlConnection = MYSQLManager.createWebMySqlConnection();
        ResultSet result
                = MYSQLManager.jquery(
                        Utilities.Format(
                                "SELECT * FROM `user` where username = '%s' && password = '%s'",
                                username, password), MySqlConnection);
        if (result.next()) {
            user = new UserData();
            user.user_id = result.getInt("user_id");
            user.phone = result.getString("phone");
            user.email = result.getString("email");
            user.banReason = result.getString("banReason");
            user.banTime = result.getBigDecimal("banTime").longValue();
            user.isBanned = result.getsbyte("isBaned");
            user.role = result.getsbyte("role");
            user.username = username;
            user.password = password;
            result.close();
            if (user.role == UserData.ROLE_NON_ACTIVE) {
                redDialog(Utilities.Format("Tài khoản của bạn chưa kích hoạt vui lòng lên trang web %s để kích hoạt tài khoản!", ServerSetting.instance.getWebDomainName()));
                return;
            }

            switch (user.isBanned) {
                case UserData.BAN_INFINITE: {
                    this.redDialog(Utilities.Format("Tài khoản của bạn đã bị khóa vĩnh viên \n Lý do :%s", user.banReason));
                    Thread.Sleep(100);
                    this.session.close();
                    MySqlConnection.close();
                    return;
                }
                case UserData.BAN_TIME: {
                    if (Utilities.CurrentTimeMillis < user.banTime) {
                        long deltaTime = user.banTime - Utilities.CurrentTimeMillis;
                        int hours = (int) (deltaTime / 1000 / 60 / 60);
                        int min = (int) ((deltaTime - (hours * 1000 * 60 * 60)) / 1000 / 60);
                        this.redDialog(Utilities.Format("Tài khoản của bạn đã bị khóa vì %s \n Sau %s giờ %s phút nữa tài khoản sẽ được mở khóa", user.banReason, hours, min));
                        Thread.Sleep(100);
                        this.session.close();
                        MySqlConnection.close();
                        return;
                    } else {
                        user.isBanned = UserData.BAN_NONE;
                        MYSQLManager.updateSql(Utilities.Format("update user set isBaned = DEFAULT where user_id = %s", user.user_id), MySqlConnection);
                    }
                    break;
                }
            }
            Player player2 = PlayerManager.get(user.user_id);
            if (player2 != null) {
                String str = "Người chơi khác đăng nhập vào tài khoản";
                player2.redDialog(str);
                this.redDialog(str);
                player2.session.close();
                player2.onDisconnected();
                this.session.close();
                MySqlConnection.close();
                return;
            }
            long timeWait = PlayerManager.getTimeWaitLogin(user.user_id);
            if (timeWait > 0) {
                String str = Utilities.Format("Vui lòng chờ %s giây nữa để đăng nhập", timeWait / 1000);
                this.redDialog(str);
                Thread.Sleep(500);
                this.session.close();
                MySqlConnection.close();
                return;
            }
            MySqlConnection MySqlConnectionPlayer = MYSQLManager.create();
            try (ResultSet result2 = MYSQLManager.jquery(
                    Utilities.Format("SELECT * FROM `player` where user_id = %s", user.user_id), MySqlConnectionPlayer)) {
                if (result2.next()) {
                    try {
                        playerData = PlayerData.read(result2);
                        PlayerManager.put(this);
                        Pet p = getPet();
                        if (p != null) {
                            p.applyInfo(this);
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                        redDialog("Đăng nhập xảy ra lỗi");
                        Thread.Sleep(100);
                        session.close();
                        return;
                    }
                }
                result2.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
            try {
                ResultSet result2 = MYSQLManager.jquery(
                        Utilities.Format("SELECT * FROM `kiosk_recovery` where user_id = %s", user.user_id), MySqlConnectionPlayer);
                while (result2.next()) {
                    SellItem sellItem = (SellItem) JsonManager.LoadFromJson(result2.getString("item"), SellItem.class);
                    if (sellItem.pet == null) {
                        addItemToInventory(sellItem.ItemSell);
                    } else {
                        playerData.addPet(sellItem.pet, this);
                    }
                }
                result2.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
            MYSQLManager.updateSql(Utilities.Format("DELETE FROM `kiosk_recovery` where user_id = %s", user.user_id), MySqlConnectionPlayer);
            MySqlConnectionPlayer.close();
            loginOK();
            controller.LoadMap();
            controller.sendMail();
            controller.updateAvatar();
            if (playerData != null) {
                controller.updateUserInfo();
                showBanner("Người chơi game quá 180 phút có thể gây ảnh hưởng đến sức khỏe");
                if (ServerSetting.instance.isOnlyAdminLogin()) {
                    if (!playerData.isAdmin) {
                        redDialog("Server này chỉ cho Admin đăng nhập bạn vui lòng không truy cập");
                        session.close();
                        return;
                    }
                }
                int goldPlus = 0;
                MySqlConnection gameMySqlConnection__ = MYSQLManager.create();
                ArrayList<String> listIdRemove = new();
                ResultSet resultSetExchangeGold = MYSQLManager.jquery(Utilities.Format("SELECT * FROM `exchange_gold` WHERE `user_id` = %s", playerData.user_id), gameMySqlConnection__);
                while (resultSetExchangeGold.next()) {
                    String id = resultSetExchangeGold.getString("id");
                    int g = resultSetExchangeGold.getInt("gold");
                    addGold(g);
                    listIdRemove.add(id);
                    goldPlus += g;
                }
                resultSetExchangeGold.close();
                if (!listIdRemove.isEmpty()) {
                    for (String uuidString : listIdRemove) {
                        MYSQLManager.updateSql(Utilities.Format("DELETE FROM `exchange_gold` WHERE `id`  = '%s' AND `user_id` = %s;", uuidString, playerData.user_id), gameMySqlConnection__);
                    }
                }
                gameMySqlConnection__.close();
                if (goldPlus > 0) {
                    okDialog(Utilities.Format("Nhận dược %s (vang) do nạp tiền", Utilities.formatNumber(goldPlus)));
                }
            } else {
                controller.createChar();
            }
            controller.getTaskCalculator().update();
        } else {
            result.close();
            loginFailed("Tài khoản hoặc mật khẩu của bạn không chính xác");
        }
        MySqlConnection.close();
    }

    public void loginOK()   {
        controller.loginOK();
        if (playerData != null) {
            controller.checkExpire();
        }
        HistoryManager.addHistory(new History(this).setLogin());
    }

    public void loginFailed(String text)   {
        Message ms = new Message(GopetCMD.LOGIN_FAILED);
        ms.putString(text);
        ms.writer().flush();
        session.sendMessage(ms);
    }

    public bool hasGift(int NPCID) {
        return true;
    }

    public bool canNPCHelp(int NPCID) {
        return true;
    }

    public static bool CheckString(String str, String c) {
        return Pattern.compile(c).matcher(str).find();
    }

     
    public void onDisconnected() {
        try {
            Place place = getPlace();
            if (place != null) {
                place.remove(this);
            }
            if (playerData != null) {
                playerData.save();
            }
            if (user != null && playerData != null) {
                PlayerManager.remove(this);
                HistoryManager.addHistory(new History(this).setLogout());
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void Popup(String text)   {
        Message m = new Message(GopetCMD.SERVER_MESSAGE);
        m.putsbyte(GopetCMD.POPUP_MESSAGE);
        m.putUTF(text);
        m.writer().flush();
        session.sendMessage(m);
    }

    public void showBanner(String text)   {
        Message m = new Message(GopetCMD.SERVER_MESSAGE);
        m.putsbyte(GopetCMD.BANNER_MESSAGE);
        m.putUTF(text);
        m.writer().flush();
        session.sendMessage(m);
    }

    public void okDialog(String str)   {
        Message m = new Message(71);
        m.putsbyte(0);
        m.putUTF(str);
        m.writer().flush();
        session.sendMessage(m);
    }

    public void notEnoughHp()   {
        Popup("Bạn không đủ máu");
    }

    public void notEnoughEnergy()   {
        Popup("Bạn không đủ thể lực");
    }

    public void addCoin(long coin)   {
        playerData.coin += coin;
        controller.updateUserInfo();
    }

    public void addGold(long gold)   {
        playerData.gold += gold;
        controller.updateUserInfo();
        if (gold < 0 && Utilities.CurrentTimeMillis < 1705330800000L) {
            playerData.spendGold -= gold;
            TopData topData = TopSpendGold.instance.find(playerData.user_id);
            if (topData != null) {
                topData.desc = Utilities.Format("Hạng %s: Đã tiêu %s (vang)", TopSpendGold.instance.datas.indexOf(topData) + 1, Utilities.formatNumber(playerData.spendGold));
            }
        }
    }

    public void mineCoin(long coin)   {
        playerData.coin -= coin;
        controller.updateUserInfo();
    }

    public void mineGold(long gold)   {
        playerData.gold -= gold;

        controller.updateUserInfo();
        if (Utilities.CurrentTimeMillis < 1705330800000L) {
            playerData.spendGold += gold;
            TopData topData = TopSpendGold.instance.find(playerData.user_id);
            if (topData != null) {
                topData.desc = Utilities.Format("Hạng %s: Đã tiêu %s (vang)", TopSpendGold.instance.datas.indexOf(topData) + 1, Utilities.formatNumber(playerData.spendGold));
            }
        }
    }

    public bool checkCoin(long value) {
        return playerData.coin >= value;
    }

    public bool checkGold(long value) {
        return playerData.gold >= value;
    }

    public bool checkStar(int value) {
        return playerData.star >= value;
    }

    public void addStar(int star)   {
        playerData.star += star;
        controller.updateUserInfo();
    }

    public void MineStar(int star)   {
        playerData.star -= star;
        controller.updateUserInfo();
    }

    public void petNotFollow()   {
        redDialog("Pet không đi theo!");
    }

    public Pet getPet() {
        return playerData.petSelected;
    }

    public void notEnoughStar()   {
        redDialog("Bạn không đủ ngôi sao");
    }

    public void addItemToNormalInventory(Item item)   {
        if (item.getTemp().isStackable()) {
            if (item.count == 0) {
                return;
            }
            Item itemFlag = null;
            for (Item item1 : playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY)) {
                if (item1.itemTemplateId == item.itemTemplateId) {
                    itemFlag = item1;
                    break;
                }
            }
            if (itemFlag != null) {
                itemFlag.count += item.count;
            } else {
                playerData.addItem(GopetManager.NORMAL_INVENTORY, item);
            }
        } else {
            playerData.addItem(GopetManager.NORMAL_INVENTORY, item);
        }
    }

    public void addItemToInventory(Item ItemSell)   {
        if (ItemSell.getTemp().isStackable() && ItemSell.count == 0) {
            return;
        }

        switch (ItemSell.getTemp().getType()) {
            case GopetManager.PET_EQUIP_HAT:
            case GopetManager.PET_EQUIP_GLOVE:
            case GopetManager.PET_EQUIP_ARMOUR:
            case GopetManager.PET_EQUIP_WEAPON:
            case GopetManager.PET_EQUIP_SHOE:
                playerData.addItem(GopetManager.EQUIP_PET_INVENTORY, ItemSell);
                break;
            case GopetManager.ITEM_GEM:
                playerData.addItem(GopetManager.GEM_INVENTORY, ItemSell);
                break;
            case GopetManager.WING_ITEM:
                playerData.addItem(GopetManager.WING_INVENTORY, ItemSell);
                break;
            case GopetManager.SKIN_ITEM:
                playerData.addItem(GopetManager.SKIN_INVENTORY, ItemSell);
                break;
            default:
                addItemToNormalInventory(ItemSell);
        }
    }

    private void updatePkPoint() {
        if (playerData.pkPoint > 0) {
            if (Utilities.CurrentTimeMillis - GopetManager.TIME_DECREASE_PK_POINT >= playerData.pkPointTime.getTime()) {
                playerData.pkPoint--;
                playerData.pkPointTime.setTime(Utilities.CurrentTimeMillis);
            }
        } else {
            if (Utilities.CurrentTimeMillis - GopetManager.TIME_DECREASE_PK_POINT >= playerData.pkPointTime.getTime()) {
                playerData.pkPointTime.setTime(Utilities.CurrentTimeMillis);
            }
        }
    }

    public bool checkIsAdmin() {
        if (playerData != null) {
            return playerData.isAdmin;
        }
        return false;
    }
}

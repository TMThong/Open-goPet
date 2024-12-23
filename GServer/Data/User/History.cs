
using Dapper;
using Gopet.Data.User;
using Gopet.Util;
using MySqlConnector;

public class History 
{
    public const int KILL_MOB = 1;
    public Object obj;
    public String log;
    public DateTime DateTime;
    public long currentTime;
    public Player player;
    public int user_id;
    public int spceialType = 0;

    public History(Player player) {
        currentTime = Utilities.CurrentTimeMillis;
        DateTime = DateTime.Now;
        this.player = player;
        setUser_id(player.user.user_id);
    }

    public History(int user_id) {
        currentTime = Utilities.CurrentTimeMillis;
        DateTime = DateTime.Now;
        setUser_id(user_id);
    }

    public string charName(MySqlConnection MySqlConnection) {
        if (this.player == null) {
            using(var conn = MYSQLManager.create())
            {
                dynamic dynamicData = conn.QuerySingleOrDefault("Select name from player where user_id = @user_id", new { user_id = this.user_id });
                if (dynamicData != null) return dynamicData.name;
            }
        } else {
            if (this.player.playerData == null) return "Chưa tạo nhân vật";
            return this.player.playerData.name;
        }
        return "Chưa tạo nhân vật";
    }

    public int getUser_id() {
        return user_id;
    }

    public void setUser_id(int user_id) {
        this.user_id = user_id;
    }

    public History setLogin() {
        log = "Đăng nhập";
        return this;
    }

    public History setLogout() {
        log = "Đăng xuất";
        return this;
    }

    public Object getObj() {
        return obj;
    }

    public History setObj(Object obj) {
        this.obj = obj;
        return this;
    }

    public String getLog() {
        return log;
    }

    public History setLog(String log) {
        this.log = log;
        return this;
    }

    public DateTime getDate() {
        return DateTime;
    }

    public History setDate(DateTime DateTime) {
        this.DateTime = DateTime;
        return this;
    }

    public long getCurrentTime() {
        return currentTime;
    }

    public History setCurrentTime(long currentTime) {
        this.currentTime = currentTime;
        return this;
    }

    public History setLoginTrung() {
        log = "Đăng nhập nhưng có người đang chơi trong tài khoản này";
        return this;
    }

    public History setLoginFailed() {
        log = "Đăng nhập thất bại";
        return this;
    }

    public History Popup(String text) {
        log = "Popup:" + text;
        return this;
    }

    public History setShowBanner(String text) {
        log = "showBanner: " + text;
        return this;
    }

    public History setOkDialog(String str) {
        log = "OK dialog: " + str;
        return this;
    }

    public History setSpceialType(int spceialType) {
        this.spceialType = spceialType;
        return this;
    }

    public int getSpceialType() {
        return spceialType;
    }

    public HistoryMongoDB Get()
    {
        return new HistoryMongoDB(this);
    }

    public class HistoryMongoDB
    {
        public int UserId { get; set; }

        public string charName { get; set; }

        public object obj { get; set; }

        public string log { get; set; }

        public DateTime Time { get; set; }

        public PlayerData PlayerData { get; set; }

        public HistoryMongoDB(History history)
        {
            UserId = history.user_id;
            PlayerData = history.player?.playerData;
            charName = PlayerData?.name;
            obj = history.obj;
            Time = history.DateTime;
            log = history.log;
        }
    }
}

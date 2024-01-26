
using Gopet.Data.User;
using Gopet.Util;
using MySql.Data.MySqlClient;

public class History {

    public const int KILL_MOB = 1;

    private Object obj;
    private String log;
    private DateTime DateTime;
    private long currentTime;
    private Player player;
    private int user_id;
    private int spceialType = 0;

    public History(Player player) {
        currentTime = Utilities.CurrentTimeMillis;
        DateTime = new DateTime(currentTime);
        this.player = player;
        setUser_id(player.user.user_id);
    }

    public History(int user_id) {
        currentTime = Utilities.CurrentTimeMillis;
        DateTime = new DateTime(currentTime);
        setUser_id(user_id);
    }

    public String charName(MySqlConnection MySqlConnection) {
        if (this.player == null) {
            try {
                ResultSet resultSet = MYSQLManager.jquery(Utilities.Format("Select * from player where user_id = %s", this.user_id), MySqlConnection);
                if (resultSet.next()) {
                    String charname = resultSet.getString("name");
                    resultSet.Close();
                    return charname;
                }
                resultSet.Close();
            } catch (Exception e) {
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

 
}

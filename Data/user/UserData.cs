package data.user;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Date;
import manager.MYSQLManager;
import util.Utilities;

public class UserData {

    public int user_id;
    public String username, password, phone, email, banReason, ipv4Create;
    public byte isBanned = BAN_NONE;
    public long banTime = 0;
    public byte role;
    public const byte BAN_NONE = 0;
    public const byte BAN_TIME = 1;
    public const byte BAN_INFINITE = 2;
    public const byte ROLE_NON_ACTIVE = 0;

    public void ban(byte typeBan, String reason, long timeBan)   {
        Connection connection = MYSQLManager.createWebConnection();
        try {
            MYSQLManager.updateSql(String.format("UPDATE `user` SET `user`.`isBaned` = %s , `user`.`banReason` = '%s', `user`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id), connection);
        } finally {
            connection.close();
        }
    }

    public static void banBySQL(byte typeBan, String reason, long timeBan, int user_id)   {
        Connection connection = MYSQLManager.createWebConnection();
        try {
            MYSQLManager.updateSql(String.format("UPDATE `user` SET `user`.`isBaned` = %s , `user`.`banReason` = '%s', `user`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id), connection);
        } finally {
            connection.close();
        }
    }

    public int getCoin() throws SQLException {
        Connection webConnection = null;
        try {
            webConnection = MYSQLManager.createWebConnection();
            ResultSet resultSet = MYSQLManager.jquery(String.format("SELECT   `coin` FROM `user` WHERE `user`.`user_id` = %s;", user_id), webConnection);
            if (resultSet.next()) {
                int coin = resultSet.getInt("coin");
                resultSet.close();
                webConnection.close();
                return coin;
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            webConnection.close();
        }
        return 0;
    }

    public void mineCoin(int coin, int myCOin)   {
        Connection webConnection = null;
        try {
            webConnection = MYSQLManager.createWebConnection();
            MYSQLManager.updateSql(String.format("INSERT INTO `dongtien`(`username`, `sotientruoc`, `sotienthaydoi`, `sotiensau`, `thoigian`, `noidung`) VALUES ('%s', %s, %s, %s, '%s' , '%s')", username, myCOin, coin, myCOin - coin,  Utilities.toDateString(new Date()), String.format("Đổi gold trên game với giá %svnđ", Utilities.formatNumber(coin))), webConnection);
            MYSQLManager.updateSql(String.format("UPDATE `user` set coin = coin - %s where user_id = %s", coin, this.user_id), webConnection);
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            webConnection.close();
        }
    }
}

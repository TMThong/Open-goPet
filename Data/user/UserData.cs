 

public class UserData {

    public int user_id;
    public String username, password, phone, email, banReason, ipv4Create;
    public sbyte isBanned = BAN_NONE;
    public long banTime = 0;
    public sbyte role;
    public const sbyte BAN_NONE = 0;
    public const sbyte BAN_TIME = 1;
    public const sbyte BAN_INFINITE = 2;
    public const sbyte ROLE_NON_ACTIVE = 0;

    public void ban(sbyte typeBan, String reason, long timeBan)   {
        MySqlConnection MySqlConnection = MYSQLManager.createWebMySqlConnection();
        try {
            MYSQLManager.updateSql(Utilities.Format("UPDATE `user` SET `user`.`isBaned` = %s , `user`.`banReason` = '%s', `user`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id), MySqlConnection);
        }  ly {
            MySqlConnection.close();
        }
    }

    public static void banBySQL(sbyte typeBan, String reason, long timeBan, int user_id)   {
        MySqlConnection MySqlConnection = MYSQLManager.createWebMySqlConnection();
        try {
            MYSQLManager.updateSql(Utilities.Format("UPDATE `user` SET `user`.`isBaned` = %s , `user`.`banReason` = '%s', `user`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id), MySqlConnection);
        }  ly {
            MySqlConnection.close();
        }
    }

    public int getCoin()   {
        MySqlConnection webMySqlConnection = null;
        try {
            webMySqlConnection = MYSQLManager.createWebMySqlConnection();
            ResultSet resultSet = MYSQLManager.jquery(Utilities.Format("SELECT   `coin` FROM `user` WHERE `user`.`user_id` = %s;", user_id), webMySqlConnection);
            if (resultSet.next()) {
                int coin = resultSet.getInt("coin");
                resultSet.close();
                webMySqlConnection.close();
                return coin;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }  ly {
            webMySqlConnection.close();
        }
        return 0;
    }

    public void mineCoin(int coin, int myCOin)   {
        MySqlConnection webMySqlConnection = null;
        try {
            webMySqlConnection = MYSQLManager.createWebMySqlConnection();
            MYSQLManager.updateSql(Utilities.Format("INSERT INTO `dongtien`(`username`, `sotientruoc`, `sotienthaydoi`, `sotiensau`, `thoigian`, `noidung`) VALUES ('%s', %s, %s, %s, '%s' , '%s')", username, myCOin, coin, myCOin - coin,  Utilities.toDateString(new Date()), Utilities.Format("Đổi gold trên game với giá %svnđ", Utilities.formatNumber(coin))), webMySqlConnection);
            MYSQLManager.updateSql(Utilities.Format("UPDATE `user` set coin = coin - %s where user_id = %s", coin, this.user_id), webMySqlConnection);
        } catch (Exception e) {
            e.printStackTrace();
        }  ly {
            webMySqlConnection.close();
        }
    }
}

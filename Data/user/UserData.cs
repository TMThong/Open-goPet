

using Dapper;
using Gopet.Data.User;
using Gopet.Util;
using MySql.Data.MySqlClient;


public class UserData
{

    public int user_id;
    public String username, password, phone, email, banReason, ipv4Create;
    public sbyte isBanned = BAN_NONE;
    public long banTime = 0;
    public sbyte role;
    public const sbyte BAN_NONE = 0;
    public const sbyte BAN_TIME = 1;
    public const sbyte BAN_INFINITE = 2;
    public const sbyte ROLE_NON_ACTIVE = 0;

    public void ban(sbyte typeBan, String reason, long timeBan)
    {
        using (var conn = MYSQLManager.createWebMySqlConnection())
        {
            conn.Execute(Utilities.Format("UPDATE `user` SET `user`.`isBaned` = %s , `user`.`banReason` = '%s', `user`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id));
        }
    }

    public static void banBySQL(sbyte typeBan, String reason, long timeBan, int user_id)
    {
        using (var conn = MYSQLManager.createWebMySqlConnection())
        {
            conn.Execute(Utilities.Format("UPDATE `user` SET `user`.`isBaned` = %s , `user`.`banReason` = '%s', `user`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id));
        }
    }

    public int getCoin()
    {
        using (var conn = MYSQLManager.createWebMySqlConnection())
        {
            dynamic coinData = conn.QuerySingleOrDefault("SELECT   `coin` FROM `user` WHERE `user`.`user_id` = @user_id;", new { user_id = user_id });
            if (coinData != null)
            {
                return coinData.coin;
            }
        }
        return 0;
    }

    public void mineCoin(int coin, int myCOin)
    {

        using (var conn = MYSQLManager.createWebMySqlConnection())
        {
            conn.Execute(Utilities.Format("INSERT INTO `dongtien`(`username`, `sotientruoc`, `sotienthaydoi`, `sotiensau`, `thoigian`, `noidung`) VALUES ('%s', %s, %s, %s, '%s' , '%s')", username, myCOin, coin, myCOin - coin, Utilities.ToDateString(Utilities.GetCurrentDate()), Utilities.Format("Đổi gold trên game với giá %svnđ", Utilities.FormatNumber(coin))));
            conn.Execute(Utilities.Format("UPDATE `user` set coin = coin - %s where user_id = %s", coin, this.user_id));
        }
    }
}

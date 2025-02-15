

using Dapper;
using Gopet.Data.User;
using Gopet.Util;
using MySqlConnector;


public class UserData
{

    public int user_id;
    public String username, password, phone, email, banReason, ipv4Create;
    public sbyte isBaned = BAN_NONE;
    public long banTime = 0;
    public sbyte role;
    public const sbyte BAN_NONE = 0;
    public const sbyte BAN_TIME = 1;
    public const sbyte BAN_INFINITE = 2;
    public const sbyte ROLE_NON_ACTIVE = 0;
    public string? secretKey { get; set; } = null;

    public void ban(sbyte typeBan, String reason, long timeBan)
    {
        using (var conn = MYSQLManager.createWebMySqlConnection())
        {
            conn.Execute("UPDATE `user` SET `user`.`isBaned` = @typeBan , `user`.`banReason` = @reason, `user`.`banTime` = @timeBan WHERE user_id = @user_id;", new
            {
                typeBan = typeBan,
                reason = reason,
                timeBan = timeBan,
                user_id = user_id
            });
        }
    }

    public static void banBySQL(sbyte typeBan, String reason, long timeBan, int user_id)
    {
        using (var conn = MYSQLManager.createWebMySqlConnection())
        {
            conn.Execute("UPDATE `user` SET `user`.`isBaned` = @isBaned , `user`.`banReason` = @banReason, `user`.`banTime` = @banTime WHERE user_id = @user_id;", new { isBaned = typeBan, banTime = timeBan, banReason = reason, user_id = user_id });
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
            conn.Execute("UPDATE `user` set coin = coin - @coin where user_id = @user_id", new { coin = coin, user_id = user_id });
        }
    }
}

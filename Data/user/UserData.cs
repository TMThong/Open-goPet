

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
        MySqlConnection conn = MYSQLManager.createWebMySqlConnection();
        try
        {
            MYSQLManager.updateSql(Utilities.Format("UPDATE `User` SET `User`.`isBaned` = %s , `User`.`banReason` = '%s', `User`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id), conn);
        }
        finally
        {
            conn.Close();
        }
    }

    public static void banBySQL(sbyte typeBan, String reason, long timeBan, int user_id)
    {
        MySqlConnection conn = MYSQLManager.createWebMySqlConnection();
        try
        {
            MYSQLManager.updateSql(Utilities.Format("UPDATE `User` SET `User`.`isBaned` = %s , `User`.`banReason` = '%s', `User`.`banTime` = %s WHERE user_id = %s;", typeBan, reason, timeBan, user_id), conn);
        }
        finally
        {
            conn.Close();
        }
    }

    public int getCoin()
    {
        MySqlConnection webMySqlConnection = null;
        try
        {
            webMySqlConnection = MYSQLManager.createWebMySqlConnection();
            ResultSet resultSet = MYSQLManager.jquery(Utilities.Format("SELECT   `coin` FROM `User` WHERE `User`.`user_id` = %s;", user_id), webMySqlConnection);
            if (resultSet.next())
            {
                int coin = resultSet.getInt("coin");
                resultSet.Close();
                webMySqlConnection.Close();
                return coin;
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        finally
        {
            webMySqlConnection.Close();
        }
        return 0;
    }

    public void mineCoin(int coin, int myCOin)
    {
        MySqlConnection webMySqlConnection = null;
        try
        {
            webMySqlConnection = MYSQLManager.createWebMySqlConnection();
            MYSQLManager.updateSql(Utilities.Format("INSERT INTO `dongtien`(`username`, `sotientruoc`, `sotienthaydoi`, `sotiensau`, `thoigian`, `noidung`) VALUES ('%s', %s, %s, %s, '%s' , '%s')", username, myCOin, coin, myCOin - coin, Utilities.ToDateString(Utilities.GetCurrentDate()), Utilities.Format("Đổi gold trên game với giá %svnđ", Utilities.FormatNumber(coin))), webMySqlConnection);
            MYSQLManager.updateSql(Utilities.Format("UPDATE `User` set coin = coin - %s where user_id = %s", coin, this.user_id), webMySqlConnection);
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        finally
        {
            webMySqlConnection.Close();
        }
    }
}

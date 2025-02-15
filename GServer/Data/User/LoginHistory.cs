using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    /// <summary>
    /// Lịch sử đăng nhập
    /// </summary>
    public class LoginHistory
    {
        public int Id { get; set; }
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Thời gian đăng nhập
        /// </summary>
        public DateTime LoginTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Địa chỉ IP
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// Kết quả đăng nhập
        /// </summary>
        public bool IsSuccess { get; set; } = false;
        /// <summary>
        /// Đăng nhập qua web
        /// </summary>
        public bool IsWebLogin { get; set; } = false;

        public LoginHistory() { }

        public LoginHistory(string userName, string ipAddress, bool isSuccess, bool isWebLogin)
        {
            UserName = userName;
            IPAddress = ipAddress;
            IsSuccess = isSuccess;
            IsWebLogin = isWebLogin;
        }

        public static void InsertToDatabase(LoginHistory history, MySqlConnection mySqlConnection)
        {
            mySqlConnection.Execute("INSERT INTO login_history (UserName, LoginTime, IPAddress, IsSuccess, IsWebLogin) VALUES (@UserName, @LoginTime, @IPAddress, @IsSuccess, @IsWebLogin)", history);
        }
    }
}

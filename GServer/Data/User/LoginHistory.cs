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
        /// Mật khẩu đã thử
        /// </summary>
        public string TryPassword { get; set; }
        /// <summary>
        /// Thời gian đăng nhập
        /// </summary>
        public DateTime LoginTime { get; set; }
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
    }
}

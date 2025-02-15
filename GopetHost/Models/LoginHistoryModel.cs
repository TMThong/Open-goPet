using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
    /// <summary>
    /// Lịch sử đăng nhập
    /// </summary>
    [Table("login_history")]
    public class LoginHistoryModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; }
        [Required]
        /// <summary>
        /// Thời gian đăng nhập
        /// </summary>
        public DateTime LoginTime { get; set; } = DateTime.Now;
        [Required]
        /// <summary>
        /// Địa chỉ IP
        /// </summary>
        public string IPAddress { get; set; }
        [Required]
        /// <summary>
        /// Kết quả đăng nhập
        /// </summary>
        public bool IsSuccess { get; set; } = false;
        [Required]
        /// <summary>
        /// Đăng nhập qua web
        /// </summary>
        public bool IsWebLogin { get; set; } = false;
    }
}

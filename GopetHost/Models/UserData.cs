using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GopetHost.Models
{
    [Table("user")]
    public class UserData
    {
        [Key]
        [Required]
        public int user_id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public string email { get; set; }

        public string phone { get; set; }
        [NotMapped]
        public string repassword { get; set; } = string.Empty;
    }
}
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int user_id { get; set; }
        
        [Required]
        public string? username { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Required]
        public string? password { get; set; } = string.Empty;
        [EmailAddress]
        public string? email { get; set; } = string.Empty;
        [Phone]
        public string? phone { get; set; } = string.Empty;
        [NotMapped]
        public string repassword { get; set; } = string.Empty;
        
        public int role { get; set; }

        public int coin { get; set; }
        
        public int isBaned { get; set; }

        public DateTime create_date { get; set; } = DateTime.Now;
        [NotMapped]
        public bool IsAdmin
        {
            get
            {
                return role == 3;
            }
        }

        public int tongnap { get; set; } = 0;


        public string? secretKey { get; set; } = null;
    }
}
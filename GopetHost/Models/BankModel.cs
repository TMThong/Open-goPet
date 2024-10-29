using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
    [Table("bank")]
    public class BankModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string BankId { get; set; }

        public BankType Type { get; set; } = BankType.MOMO;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Logo { get; set; }

        public string Password { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; }

        public string Message { get; set; } = string.Empty;

        public string MessageAttrs { get; set; } = string.Empty;

        public enum BankType
        {
            BANK = 5,
            MOMO = 6,
        }
        [NotMapped]
        public string URL_API
        {
            get
            {
                switch (Type)
                {
                    case BankType.BANK:
                        return $"https://api.web2m.com/historyapimb/{this.Password}/{this.BankId}/{this.Token}";
                    default:
                        return $"https://api.web2m.com/historyapimomo/{this.Token}";
                }
            }
        }
    }
}

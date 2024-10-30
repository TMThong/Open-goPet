using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
    [Table("bank_trans")]
    public class BankTranslationModel
    {
        [Key]
        [Required]
        public string TranId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int AmountReceived { get; set; } = 0;
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public bool IsAddCoin { get; set; } = false;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace GopetHost.Models
{
    [Table("momo_trans")]
    public class MomoTranslationModel
    {
        [Key]
        [Required]
        public string TranId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PartnerName { get; set; }
        [Required]
        public string PartnerId { get; set; }
        [Required]
        public int Amount { get; set; }

        [Required]
        public int AmountReceived { get; set; } = 0;

		[Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Time { get; set; } = DateTime.Now;
        [Required]
        public bool IsAddCoin { get; set; } = false;

        [Required]
        public DateTime TimeCharge { get; set; } = DateTime.Now;
        [Required]
        public string PartnerAvatarUrl { get; set; } = string.Empty;
    }
}

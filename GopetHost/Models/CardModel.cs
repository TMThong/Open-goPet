using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
    [Table("cards")]
    public class CardModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string UUID { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; }
        [Required]
        public int MenhGia { get; set; }
        [Required]
        public int TienGachThe { get; set; }
        [Required]
        public int ThucNhan { get; set; }
        [Required]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [Required]
        public DateTime TimeUpdate { get; set; } = DateTime.Now;
        [Required]
        public CardStatus Status { get; set; } = CardStatus.Proccessing;
        [Required]
        public string Note { get; set; } = "Đang xử lý";
        [Required]
        public string Pin { get; set; } = string.Empty;
        [Required]
        public string Seri { get; set; } = string.Empty;
        [Required]
        public string CardId { get; set; } = string.Empty;

        public enum CardStatus
        {
            Success = 1,
            Fail = 0,
            Proccessing = 3
        }
    }
}

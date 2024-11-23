using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
    [Table("dong_tien")]
    public class DongTienModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Giá trị chệnh lệch
        /// </summary>
        [Required]
        public int Value { get; set; } = 0;

        /// <summary>
        /// Người người
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Ai là người thao tác
        /// Có thể là người hoặc tự động
        /// </summary>
        public string NameSetDongTien { get; set; } = string.Empty;
        /// <summary>
        /// Ngày giao động
        /// </summary>
        [Required]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// Giá trị trước cộng tiền
        /// </summary>
        public int ValueBefore { get; set; } = 0;
        /// <summary>
        /// Giá trị sau cộng tiền
        /// </summary>
        public int ValueAfter { get; set; } = 0;

        /// <summary>
        /// Nội dung thay đổi
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}

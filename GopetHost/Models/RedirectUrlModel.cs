using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
    [Table("redirect_url")]
    public class RedirectUrlModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string RedirectUrl { get; set; }
    }
}

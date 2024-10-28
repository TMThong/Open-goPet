using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
    [Table("web_config")]
    public class WebConfigModel
    {
        [Key]
        [Display]
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; } = string.Empty;
        [Range(0, 5)]
        [Required]
        public TypeValue Type { get; set; } = TypeValue.String;

        [Required]
        public string Comment { get; set; } = string.Empty;

        public enum TypeValue
        {
            String = 0,
            Boolean = 1,
            Int32 = 2,
            Int64 = 3,
            Float = 4,
            Double = 5,
        }
    }
}

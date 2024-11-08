using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
	[Table("tags")]
	public class TagModel
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }
		[Required]
		public string Tag { get; set; } = string.Empty;
		[Required]
		public string CSSClasses { get; set; } = string.Empty;
	}
}

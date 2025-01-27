using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
	[Table("posts")]
	public class PostModel
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public int Views { get; set; } = 0;

		public ICollection<PostTagModel> Tags { get; set; }
		[Required]
		public string AvatarPath { get; set; } = string.Empty;
		[Required]
		public string Username { get; set; } = string.Empty;
		[Required]
		public bool IsPin { get; set; } = false;
		[Required]
		public DateTime TimeCreate { get; set; } = DateTime.Now;

		public DateTime? TimeUnlock { get; set; } = null;
    }
}

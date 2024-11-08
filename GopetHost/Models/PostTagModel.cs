using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
	[Table("post_tags")]
	public class PostTagModel
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }
		[Column("PostId")]
		[Required]
		public int PostId { get; set; }
		[Required]
		public int TagId { get; set; }

        public PostModel Post { get; set; }
        public TagModel Tag { get; set; }
    }
}

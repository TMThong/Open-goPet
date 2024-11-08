namespace GopetHost.Models
{
	public class IndexHomeBagModel
	{
		public ICollection<PostModel> Posts { get; set; }

		public ICollection<TagModel> Tags { get; set; }

		public int CurrentPage { get; set; } = 0;

		public int MaxPage { get; set; } 

		public int? TagId { get; set; }
	}
}

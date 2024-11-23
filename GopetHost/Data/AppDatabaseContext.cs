using GopetHost.Models;
using Microsoft.EntityFrameworkCore;

namespace GopetHost.Data
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        
        public DbSet<UserData> Users { get; set; }

        public DbSet<WebConfigModel> WebConfigs { get; set; }

        public DbSet<BankModel> Banks { get; set; }

        public DbSet<MomoTranslationModel> MomoTranslations { get; set; }
        public DbSet<BankTranslationModel> BankTranslations { get; set; }

        public DbSet<TagModel> Tags { get; set; }

        public DbSet<CardModel> Cards { get; set; }

        public DbSet<PostModel> Posts { get; set; }

        public DbSet<PostTagModel> PostTags { get; set; }

        public DbSet<DongTienModel> DongTiens { get; set; }


		public T LoadWebConfig<T>(string Key, T defaultValue)
		{
            var query = WebConfigs.Where(x => x.Key == Key);
            if (query.Count() > 0)
            {
                return (T)query.First().ObjectAsValue;
            }
			return defaultValue;
		}
	}
}

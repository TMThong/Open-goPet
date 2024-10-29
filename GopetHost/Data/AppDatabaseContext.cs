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

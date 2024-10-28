using GopetHost.Models;
using Microsoft.EntityFrameworkCore;

namespace GopetHost.Data
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        
        public DbSet<UserData> Users { get; set; }

        public DbSet<WebConfigModel> WebConfigs { get; set; }
    }
}

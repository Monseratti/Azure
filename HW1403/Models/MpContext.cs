using Microsoft.EntityFrameworkCore;

namespace HW1403.Models
{
    public class MpContext : DbContext
    {
        public DbSet<mpCategory> Categories { get; set; }
        public DbSet<mpGood> Goods { get; set; }

        public MpContext()
        {
            Database.EnsureCreatedAsync().Wait();
            //Database.MigrateAsync().Wait();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mpCategory>().ToContainer("Categories");
            modelBuilder.Entity<mpGood>().ToContainer("Goods");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "AccountEndpoint=https://monseratti-nosql.documents.azure.com:443/;" +
                "AccountKey=2NexxiVqKuC0Y2L5IAOTYiIPppOLhVZbUODTLWuOch8UUEWEjmmwkB0T0KMheP33DKA6eY807Mw1ACDbJd5QCw==;"
                , "MarketDB");
        }
    }
}

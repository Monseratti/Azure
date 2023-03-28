using Microsoft.EntityFrameworkCore;

namespace HW0703.Models
{
    public class ImageContext: DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User>Users { get; set; }

        public ImageContext()
        {
            Database.EnsureCreatedAsync().Wait();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Image>().ToContainer("Image");
            //modelBuilder.Entity<Tag>().ToContainer("Tags");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos("AccountEndpoint=https://monseratti-nosql.documents.azure.com:443/;" +
                "AccountKey=2NexxiVqKuC0Y2L5IAOTYiIPppOLhVZbUODTLWuOch8UUEWEjmmwkB0T0KMheP33DKA6eY807Mw1ACDbJd5QCw==;",
                "ImageStorage");
        }
    }
}

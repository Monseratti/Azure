using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW1403.Class
{
    public class MyDBContext: DbContext 
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToContainer("Books");
            modelBuilder.Entity<Author>().ToContainer("Authors");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "AccountEndpoint=https://monseratti-nosql.documents.azure.com:443/;" +
                "AccountKey=2NexxiVqKuC0Y2L5IAOTYiIPppOLhVZbUODTLWuOch8UUEWEjmmwkB0T0KMheP33DKA6eY807Mw1ACDbJd5QCw==;",
                "MyDatabase");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.BookDbIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options){}

        // Define DbSet properties for your entities in BookDb
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }

        // Optionally, override OnModelCreating if you need to customize the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here if needed
        }
    }
}

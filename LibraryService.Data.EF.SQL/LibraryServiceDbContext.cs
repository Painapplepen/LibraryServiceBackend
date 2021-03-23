using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.EF.SQL
{
    public class LibraryServiceDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public LibraryServiceDbContext(DbContextOptions<LibraryServiceDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

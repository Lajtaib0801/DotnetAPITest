using DotnetAPITest.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPITest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}

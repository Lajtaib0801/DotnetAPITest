using DotnetAPITest.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPITest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolStock.Models.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(
       DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
    }
}

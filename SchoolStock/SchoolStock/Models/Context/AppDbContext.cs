using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SchoolStock.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
       DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<School> Schools => Set<School>();

        public DbSet<Stock> Stocks => Set<Stock>();

        public DbSet<StockMovement> StockMovements =>
            Set<StockMovement>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Code)
                .IsUnique();

            modelBuilder.Entity<Stock>()
                .HasIndex(s => new
                {
                    s.ProductId,
                    s.SchoolId
                })
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.School)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.SchoolId);
        }
    }
}

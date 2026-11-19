using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(category => category.Id);
            entity.Property(category => category.Id).HasColumnName("id");
            entity.Property(category => category.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(product => product.Id);
            entity.Property(product => product.Id).HasColumnName("id");
            entity.Property(product => product.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(product => product.Price)
                .HasColumnName("price")
                .HasColumnType("numeric(18,2)");
            entity.Property(product => product.Quantity).HasColumnName("quantity");
            entity.Property(product => product.CategoryId).HasColumnName("category_id");
        });
    }
}

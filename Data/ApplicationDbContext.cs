using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Models;


namespace SnackisApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
     public DbSet<Models.Post> Post { get; set; }
     public DbSet<Models.Category> Category { get; set; }
    public DbSet<Comment> Comment { get; set; }
     public DbSet<Models.SubCategory> SubCategory { get; set; }

 protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategory)
            .WithOne(sc => sc.Category)
            .HasForeignKey(sc => sc.CategoryId);

        modelBuilder.Entity<SubCategory>()
            .HasMany(sc => sc.Post)
            .WithOne(p => p.SubCategory)
            .HasForeignKey(p => p.SubCategoryId);
    }
}
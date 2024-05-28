using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Models;


namespace SnackisApp.Data;

public class ApplicationDbContext : IdentityDbContext<SnackisUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
     public DbSet<Models.Post> Post { get; set; }
     public DbSet<Models.Category> Category { get; set; }
    public DbSet<Comment> Comment { get; set; }
     public DbSet<Models.SubCategory> SubCategory { get; set; }

     public DbSet<Models.Message> Message { get; set; }

     public DbSet<Report> Reports { get; set; }

     public DbSet<Philosopher> Philosopher { get; set; }


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
          
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId);

                  modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany()
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

              modelBuilder.Entity<Report>()
                .HasOne(r => r.Post)
                .WithMany()
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.ReportedBy)
                .WithMany()
                .HasForeignKey(r => r.ReportedById)
                .OnDelete(DeleteBehavior.Cascade);

                    modelBuilder.Entity<Report>()
        .Property(r => r.Status)
        .HasDefaultValue(ReportStatus.Pending);


     modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

    }
}
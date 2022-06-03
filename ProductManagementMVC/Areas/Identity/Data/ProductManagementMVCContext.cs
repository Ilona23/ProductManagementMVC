using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagementMVC.Areas.Identity.Data;
using ProductManagementMVC.Entities;

namespace ProductManagementMVC.Data;

public class ProductManagementMVCContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Subject> Subjects { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<FamousQuoteViewModel> FamousQuotes { get; set; }

    public DbSet<UserAchievementsViewModel> UserAchievements { get; set; }
    public ProductManagementMVCContext(DbContextOptions<ProductManagementMVCContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserAchievementsViewModel>().HasKey(c => new { c.QuoteId, c.UserId });
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new SubjectConfiguration());
    }
}

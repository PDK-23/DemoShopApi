using DemoShopApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Role từ enum
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = (int)RoleEnum.Admin, Name = RoleEnum.Admin.ToString() },
            new Role { Id = (int)RoleEnum.User, Name = RoleEnum.User.ToString() }
        );

        // Các cấu hình khác (nếu có)
        base.OnModelCreating(modelBuilder);
    }
}

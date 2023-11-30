using E_Commerce_Template.Models;
using E_Commerce_Template.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Template.Data
{
    public class AppDbContext : IdentityDbContext<AuthUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "admin", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "User", Name = "User", NormalizedName = "User" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}

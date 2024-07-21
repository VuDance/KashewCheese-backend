
using KashewCheese.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        //category
        public DbSet<Category> Category { get; set; }
        //subcategory
        public DbSet<SubCategory> SubCategories { get; set; }
        //product
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            //category
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Slug).IsUnique();
            //subcategory
            modelBuilder.Entity<SubCategory>()
                .HasIndex(sb => sb.Slug).IsUnique();
            modelBuilder.Entity<SubCategory>()
                .HasOne(sb => sb.Category)
                .WithMany(sb => sb.SubCategories)
                .HasForeignKey(sb => sb.CategoryId);
            //product
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Slug).IsUnique();
            modelBuilder.Entity<Product>()
                .HasOne(p=>p.SubCategory)
                .WithMany(p=>p.Products)
                .HasForeignKey(p=>p.SubCategoryId);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}

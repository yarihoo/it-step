using Internet_Market_WebApi.Data.Entities;
using Internet_Market_WebApi.Data.Entities.Card;
using Internet_Market_WebApi.Data.Entities.Identity;
using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Internet_Market_WebApi.Data
{
    public class AppEFContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options) : base(options)
        {
        }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SubcategoryEntity> Subcategories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<BagItem> BagItems { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRoleEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });
        }
    }
}

using AuthServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Persistence
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, string, UserClaim,
            IdentityUserRole<string>,
            IdentityUserLogin<string>,
            RoleClaim,
            IdentityUserToken<string>>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<UserClaim>().HasOne(x => x.Claim)
             .WithMany(y => y.UserClaim)
             .HasForeignKey(a => a.ClaimId);

            modelBuilder.Entity<RoleClaim>().HasOne(x => x.Claim)
            .WithMany(y => y.RoleClaim)
            .HasForeignKey(a => a.ClaimId);

            modelBuilder.Entity<MenuControl>()
            .HasOne(c => c.Claim)
            .WithOne(m => m.MenuControl)
            .HasForeignKey<MenuControl>(c => c.ClaimId);

            modelBuilder.Entity<MenuControl>().HasOne(x => x.Parent)
              .WithMany(y => y.Children)
              .HasForeignKey(a => a.ParentId);

            modelBuilder.Entity<UserClaim>().Ignore(x =>  x.ClaimType);
            modelBuilder.Entity<UserClaim>().Ignore(x =>  x.ClaimValue);

            modelBuilder.Entity<RoleClaim>().Ignore(x => x.ClaimType);
            modelBuilder.Entity<RoleClaim>().Ignore(x => x.ClaimValue);

        }

        public DbSet<ControllerClaim> ControllerClaim { get; set; }
        public DbSet<MenuControl> MenuControl { get; set; }
    }
}

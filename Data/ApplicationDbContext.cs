using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Models;
using Tazkara.ViewModels;

namespace Tazkara.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<CourtSidesRow3> CourtSidesRow3 { get; set; }
        public DbSet<EastPremiumStands> EastPremiumStands { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<EastStands> EastStands { get; set; }
        public DbSet<NorthPremiumStands> NorthPremiumStands { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users","Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");

        }
        public DbSet<Tazkara.ViewModels.UserRoleVM> UserRoleVM { get; set; } = default!;
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}

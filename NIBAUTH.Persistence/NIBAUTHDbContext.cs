using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Persistence
{
    public class NIBAUTHDbContext : IdentityDbContext<User, Role, Guid>, INIBAUTHDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionBranche> RegionBranches { get; set; }
        public DbSet<BrancheBlock> BrancheBlocks { get; set; }

        public DbSet<Camera> Cameras { get; set; }
        public DbSet<UserFinal> UserFinals { get; set; }




        public NIBAUTHDbContext(DbContextOptions<NIBAUTHDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(u => u.DefaultRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.DefaultRoleId)
                .OnDelete(DeleteBehavior.Restrict); 
     
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.RefreshToken).HasMaxLength(500);
                entity.Property(e => e.PhotoUrl).HasMaxLength(500);
            });
        }
    }
}

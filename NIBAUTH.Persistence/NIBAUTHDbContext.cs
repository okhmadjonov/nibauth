using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using NIBAUTH.Domain.Entities.Base;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.DefaultRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.DefaultRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.RefreshToken).HasMaxLength(500);
                entity.Property(e => e.PhotoUrl).HasMaxLength(500);
            });

            modelBuilder.Entity<RegionBranche>(entity =>
            {
                entity.HasOne(rb => rb.CreatedBy)
                      .WithMany()
                      .HasForeignKey(rb => rb.CreatedById)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(rb => rb.UpdatedBy)
                      .WithMany()
                      .HasForeignKey(rb => rb.UpdatedById)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RegionBranche>()
                .HasOne(rb => rb.Region)
                .WithMany(r => r.RegionBranches)
                .HasForeignKey(rb => rb.RegionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.RegionBranch)
                .WithMany(rb => rb.Users)
                .HasForeignKey(u => u.RegionBranchId)
                .OnDelete(DeleteBehavior.Restrict);

      
      

            modelBuilder.Entity<BrancheBlock>(entity =>
            {
                entity.HasOne(bb => bb.CreatedBy)
                      .WithMany()
                      .HasForeignKey(bb => bb.CreatedById)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(bb => bb.UpdatedBy)
                      .WithMany()
                      .HasForeignKey(bb => bb.UpdatedById)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Camera>(entity =>
            {
                entity.HasOne(c => c.CreatedBy)
                      .WithMany()
                      .HasForeignKey(c => c.CreatedById)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.UpdatedBy)
                      .WithMany()
                      .HasForeignKey(c => c.UpdatedById)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserFinal>(entity =>
            {
                entity.HasOne(uf => uf.CreatedBy)
                      .WithMany()
                      .HasForeignKey(uf => uf.CreatedById)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uf => uf.UpdatedBy)
                      .WithMany()
                      .HasForeignKey(uf => uf.UpdatedById)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
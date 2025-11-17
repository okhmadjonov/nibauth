using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NIBAUTH.Domain.Entities;


namespace NIBAUTH.Application.Interfaces
{

    public interface INIBAUTHDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionBranche> RegionBranches { get; set; }

        public DbSet<BrancheBlock> BrancheBlocks { get; set; }

        public DbSet<Camera> Cameras { get; set; }
        public DbSet<UserFinal> UserFinals { get; set; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

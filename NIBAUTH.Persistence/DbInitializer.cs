using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NIBAUTH.Application.Common.Utilities;
using NIBAUTH.Domain;
using NIBAUTH.Domain.Entities;
using NIBAUTH.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NIBAUTH.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(NIBAUTHDbContext context)
        {
            context.Database.EnsureCreated();
        }

        public static async Task RunSeed(ConfigurationManager config, IServiceProvider services)
        {
            var adminPassword = config["Passwords:Admin_Password"];
            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                Console.WriteLine("Admin password not found in configuration");
                return;
            }

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            var dbContext = services.GetRequiredService<NIBAUTHDbContext>();
            var tokenManager = services.GetRequiredService<TokenManager>();

            await dbContext.Database.EnsureCreatedAsync();

            var existingRegions = await dbContext.Regions.ToListAsync();
            if (!existingRegions.Any())
            {
                var andijonRegion = new Region { Code = "AND", Name = "Andijon" };
                var namanganRegion = new Region { Code = "NAM", Name = "Namangan" };
                var fargonaRegion = new Region { Code = "FAR", Name = "Farg'ona" };
                var toshkentViloyatiRegion = new Region { Code = "TV", Name = "Toshkent viloyati" };
                var toshkentShaharRegion = new Region { Code = "TS", Name = "Toshkent shaxri" };
                var sirdaryoRegion = new Region { Code = "SIR", Name = "Sirdaryo" };
                var jizzaxRegion = new Region { Code = "JIZ", Name = "Jizzax" };
                var samarqandRegion = new Region { Code = "SAM", Name = "Samarqand" };
                var surxandaryoRegion = new Region { Code = "SUR", Name = "Surxondaryo" };
                var qashqadaryoRegion = new Region { Code = "QAR", Name = "Qashqadaryo" };
                var navoiyRegion = new Region { Code = "NAV", Name = "Navoiy" };
                var buxoroRegion = new Region { Code = "BUX", Name = "Buxoro" };
                var xorazmRegion = new Region { Code = "XR", Name = "Xorazim" };
                var qqRegion = new Region { Code = "QQ", Name = "Qoraqalpog'iston" };

                await dbContext.Regions.AddRangeAsync(andijonRegion, namanganRegion, fargonaRegion, toshkentViloyatiRegion,
                    toshkentShaharRegion, sirdaryoRegion, jizzaxRegion, samarqandRegion, surxandaryoRegion, qashqadaryoRegion,
                    navoiyRegion, buxoroRegion, xorazmRegion, qqRegion);
                await dbContext.SaveChangesAsync();
                Console.WriteLine("Regions seeded successfully");
            }

            var existingBranches = await dbContext.RegionBranches.ToListAsync();
                var tashkentRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Code == "TS");
            if (!existingBranches.Any())
            {
                if (tashkentRegion != null)
                {
                    var tashkentBranches = new[]
                    {
                        new RegionBranche { Name = "MVD_A", RegionId = tashkentRegion.Id },
                        new RegionBranche { Name = "MVD_B", RegionId = tashkentRegion.Id },
                        new RegionBranche { Name = "MVD_C", RegionId = tashkentRegion.Id }
                    };
                    await dbContext.RegionBranches.AddRangeAsync(tashkentBranches);
                    await dbContext.SaveChangesAsync();
                    Console.WriteLine("Branches seeded successfully");
                }
            }

            await SeedRoles(roleManager);

            //var tashkentRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Code == "TS");
            if (tashkentRegion == null)
            {
                Console.WriteLine("Tashkent region not found!");
                return;
            }

            var mvdABranch = await dbContext.RegionBranches.FirstOrDefaultAsync(b => b.Name == "MVD_A");

            await SeedSuperAdmin(userManager, roleManager, tokenManager, adminPassword, config, dbContext, tashkentRegion, mvdABranch);
        }

        private static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            Console.WriteLine("Seeding roles...");
            var roles = new[] { Roles.SuperAdmin, Roles.Admin, Roles.User };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };
                    await roleManager.CreateAsync(role);
                    Console.WriteLine($"Role {roleName} created");
                }
            }
        }

        private static async Task SeedSuperAdmin(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            TokenManager tokenManager,
            string adminPassword,
            IConfiguration config,
            NIBAUTHDbContext dbContext,
            Region tashkentRegion,
            RegionBranche mvdABranch)
        {
            var superAdminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin);
            if (superAdminRole == null)
            {
                return;
            }

            var user = await userManager.FindByNameAsync("martin");

            if (user == null)
            {
                user = new User
                {
                    UserName = "martin",
                    Email = "martin.iden.jack@london.ru",
                    EmailConfirmed = true,
                    NormalizedUserName = "MARTIN",
                    NormalizedEmail = "MARTIN.IDEN.JACK@LONDON.RU",
                    PhoneNumberConfirmed = true,
                    DefaultRoleId = superAdminRole.Id,
                    CreatedAt = DateTime.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    RefreshToken = tokenManager.GenerateRefreshToken(),
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                        int.TryParse(config["JwtSettings:RefreshTokenValidityInDays"], out var days) ? days : 1),
                    RegionId = tashkentRegion.Id,
                    RegionBranchId = mvdABranch?.Id
                };

                var createResult = await userManager.CreateAsync(user, adminPassword);
                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    return;
                }

                await userManager.AddToRoleAsync(user, Roles.SuperAdmin);
            }
            else
            {
                if (!await userManager.IsInRoleAsync(user, Roles.SuperAdmin))
                    await userManager.AddToRoleAsync(user, Roles.SuperAdmin);

                user.RefreshToken = tokenManager.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                    int.TryParse(config["JwtSettings:RefreshTokenValidityInDays"], out var days) ? days : 1);

                user.RegionId = tashkentRegion.Id;
                user.RegionBranchId = mvdABranch?.Id;

                await userManager.UpdateAsync(user);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
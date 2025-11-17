using NIBAUTH.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace NIBAUTH.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["NIBAUTHDatabase"];
            services.AddDbContext<NIBAUTHDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<INIBAUTHDbContext>(provider => provider.GetService<NIBAUTHDbContext>());
            return services;
        }
    }
}

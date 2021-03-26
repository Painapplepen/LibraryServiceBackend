using Microsoft.Extensions.DependencyInjection;

namespace LibraryService.Data.EF.SQL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLibraryServiceDataAccess(this IServiceCollection services)
        {
            services.AddLibraryServiceDbContext();
        }
        private static void AddLibraryServiceDbContext(this IServiceCollection services)
        {
            services.AddScoped<LibraryServiceDbContext>();
        }
    }
}
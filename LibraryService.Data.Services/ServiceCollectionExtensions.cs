using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryService.Data.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLibraryServices(this IServiceCollection services)
        {
            services.AddLibraryServiceDataAccess();
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            var currentAssembly = typeof(ServiceCollectionExtensions);

            services.Scan(scan => scan.FromAssembliesOf(currentAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IBaseService<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

        }
    }
}
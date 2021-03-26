using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LibraryService.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;
using LibraryService.API.Application.Validation.Abstractions;

namespace LibraryService.API.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLibraryServiceApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddLibraryServices();
            services.AddValidators();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.Scan(
                    x => {
                        var entryAssembly = Assembly.GetEntryAssembly();
                        IEnumerable<Assembly> referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
                        IEnumerable<Assembly> assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);

                        x.FromAssemblies(assemblies)
                            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime();
                    });
        }
    }
}
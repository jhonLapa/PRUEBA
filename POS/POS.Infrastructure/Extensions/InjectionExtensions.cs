using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Infrastructure.Persistences.Repositories;
using SIR.ERP.Infrastructure.FileStorage;

namespace POS.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {

            var assenmbly = typeof(POSContext).Assembly.FullName;

            services.AddDbContext<POSContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("POSConnection"), b => b.MigrationsAssembly(assenmbly)), ServiceLifetime.Transient);
           
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAzureStorage, AzureStorage>();

            return services;
        }
    }
}

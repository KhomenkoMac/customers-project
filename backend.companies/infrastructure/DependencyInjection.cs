using System.Reflection;
using application.Interface;
using infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IAppDbContext, AppDbContext>(provider =>
        {
            provider.UseSqlServer(configuration.GetConnectionString("Default"),
                context => context
                    .MigrationsAssembly(Assembly
                    .GetExecutingAssembly()
                    .FullName));
        });

        services.AddScoped<IExecProcService, ExecSqlProcedureService>();

        return services;
    }
}

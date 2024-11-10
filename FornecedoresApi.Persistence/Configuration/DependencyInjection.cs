using FornecedoresApi.Domain.IRepository;
using FornecedoresApi.Persistence.Context;
using FornecedoresApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FornecedoresApi.Persistence.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<FornecedorDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("FornecedoresDB"),
                                 sqlOptions => sqlOptions.EnableRetryOnFailure());
#if DEBUG
            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
            options.UseLoggerFactory(loggerFactory);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
#endif
        }).RegisterRepositories();

    public static IServiceCollection RegisterRepositories(this IServiceCollection services) =>
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
}
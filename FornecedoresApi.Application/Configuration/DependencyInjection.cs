using FornecedoresApi.Application.Application;
using FornecedoresApi.Domain.IApplication;
using Microsoft.Extensions.DependencyInjection;

namespace FornecedoresApi.Application.Configuration;
public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
        => services.AddScoped<IFornecedorService, FornecedorService>();
}

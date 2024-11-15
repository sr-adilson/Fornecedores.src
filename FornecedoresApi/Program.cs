using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Serilog;
using FornecedoresApi.Application.Configuration;
using FornecedoresApi.Persistence.Configuration;
using FornecedoresApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext();
});

services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
services.AddHealthChecks();
services.AddMvc().AddXmlDataContractSerializerFormatters();

/* AUTHENTICATION */
services.AddHttpContextAccessor();
services.AddAuthentication();
services.AddAuthorization();
services.AddMemoryCache();
/* AUTHENTICATION */

/* IOC */
services.RegisterPersistence(builder.Configuration);
services.RegisterApplication();
/* IOC */

/* SWAGGER */
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fornecedores Api", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FornecedoresApi V1");
        c.RoutePrefix = "swagger";
    });
}
/* SWAGGER */

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});

app.Run();
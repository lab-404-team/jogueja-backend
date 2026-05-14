using Common.DependencyInjection.Extensions;
using Core.Infrastructure.Extensions;
using CorrelationId;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Jogueja.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServicesFromAssemblies(
        builder.Configuration,
        Jogueja.Api.AssemblyReference.Assembly,
        Core.Persistence.AssemblyReference.Assembly)
    .InstallModulesFromAssemblies(
        builder.Configuration,
        Players.Persistence.AssemblyReference.Assembly,
        CourtOwners.Persistence.AssemblyReference.Assembly);

if (!builder.Environment.IsDevelopment())
{
    builder.ConfigureSystemsManager();
}

builder.Services
    .AddHttpContextAccessor();

builder
    .ConfigureLogging(builder.Configuration)
    .ConfigureServiceProvider()
    .ConfigureAppConfiguration();

builder.Services
    .AddCorrelationId();

builder.Services.AddMiddlewares();

builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddHealthCheck(builder.Configuration);

builder.Services.AddHttpLogging(options
    => options.LoggingFields = HttpLoggingFields.All);

builder.Services.AddCommonServiceCollection(builder.Configuration);

builder.Services
    .AddControllers()
    .AddApplicationPart(Jogueja.Api.AssemblyReference.Assembly);

var app = builder.Build();

app.UsePathBase("/api");
app.UseStaticFiles();

app.MapHealthChecks("/api/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).ShortCircuit();

app.UseSwagger()
   .UseSwaggerUI()
   .UseCors(corsPolicyBuilder =>
        corsPolicyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

app.UseCorrelationId();
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging()
   .UseHttpsRedirection();

app.UseMiddlewares();

app.MapControllers();

try
{
    await app.RunAsync();

    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

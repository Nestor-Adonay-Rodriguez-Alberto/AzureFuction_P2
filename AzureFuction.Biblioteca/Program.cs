using DataAccess;
using DataAccess.Repositories;
using Domain.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


var builder = FunctionsApplication.CreateBuilder(args);

// INYECCION DE LA DB:
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("DbConnectionString")
                           ?? builder.Configuration["DbConnectionString"];

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("La cadena de conexión no está configurada.");
    }

    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.CommandTimeout(300));
});

// INYECCIONES DE REPOSITORIOS:
builder.Services.AddScoped<ILibro, LibroRepository>();

// CONFIGURACION SERIALIZACION EN CAMELCASE
builder.Services.AddSingleton<JsonSerializerSettings>(new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    NullValueHandling = NullValueHandling.Ignore,
    Formatting = Formatting.Indented
});

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();

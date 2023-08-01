using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WeatherApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(new WeatherMeter());
var otelEndpoint = Environment.GetEnvironmentVariable("OTEL_COLLECTOR_ENDPOINT");
if (string.IsNullOrEmpty(otelEndpoint))
{
    otelEndpoint = "http://localhost:4317";
}

var resourceBuilder = ResourceBuilder.CreateDefault().AddService("weather-service")
   .AddTelemetrySdk();

builder.Services.AddOpenTelemetry()
   .ConfigureResource(resource => resource.AddService("weather-service"))
   .WithTracing(tracing => tracing
        .AddConsoleExporter()
        .AddAspNetCoreInstrumentation()
        .AddSource("weather-service")
        .AddOtlpExporter(exporter =>exporter.Endpoint = new Uri(otelEndpoint)))
   .WithMetrics(metrics =>
    {
        if (builder.Environment.IsDevelopment())
        {
            metrics.AddConsoleExporter();
        }
        metrics.AddMeter(WeatherMeter.Name)
           .AddOtlpExporter(exporter => exporter.Endpoint = new Uri(otelEndpoint));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
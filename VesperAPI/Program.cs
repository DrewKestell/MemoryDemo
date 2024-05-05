using Microsoft.ApplicationInsights;
using VesperAPI.Configuration;
using VesperAPI.Instrumentation;
using VesperAPI.Repository;
using VesperAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CosmosDbConfig>(builder.Configuration.GetSection(CosmosDbConfig.AppSettingsKey));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddApplicationInsightsTelemetry();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IGameReviewService, GameReviewService>();
builder.Services.AddSingleton<IRepository, Repository>();

var app = builder.Build();

var telemetryClient = app.Services.GetService<TelemetryClient>();
var performanceMetricsEmitter = new PerformanceMetricsEmitter(telemetryClient!);
performanceMetricsEmitter.StartEmittingMetrics();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

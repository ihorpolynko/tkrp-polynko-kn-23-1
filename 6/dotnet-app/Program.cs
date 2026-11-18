using CoursesApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not configured.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapGet(
    "/ready",
    async (AppDbContext db) =>
        await db.Database.CanConnectAsync()
            ? Results.Ok(new { status = "ready" })
            : Results.StatusCode(503));

app.MapGet(
    "/instance",
    () => new
    {
        pod = Environment.GetEnvironmentVariable("HOSTNAME") ?? "unknown",
        version = Environment.GetEnvironmentVariable("APP_VERSION") ?? "1.0"
    });

app.Run();
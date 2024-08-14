using backend.DbContexts;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Environment: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

// Add services to the container.

builder.Services.AddScoped<SimpleService>();

// for entity framework
if (builder.Configuration["DatabaseType"] == "mysql")
{
    // string connectionString = builder.Configuration.GetConnectionString("mysql") ?? "";
    string connStr = Environment.GetEnvironmentVariable("CONNECTIONS_DEFAULT") ?? "";
    if (connStr == "") {
        connStr = builder.Configuration.GetConnectionString("mysql") ?? "";
        Console.WriteLine("Defaulted to builder config.");
    }

    try
    {
        var serverVersion = ServerVersion.AutoDetect(connStr);
        builder.Services.AddDbContext<SimpleDbContext>(options =>
            options.UseMySql(connStr, serverVersion));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to connect to the database ({connStr}): {ex.Message}");
        return;
    }
}

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
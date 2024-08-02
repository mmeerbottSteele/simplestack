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
    string connectionString = builder.Configuration.GetConnectionString("mysql") ?? "";
    var serverVersion = ServerVersion.AutoDetect(connectionString);

    builder.Services.AddDbContext<SimpleDbContext>(options =>
        options.UseMySql(connectionString, serverVersion));
}

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.Run();


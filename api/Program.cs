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
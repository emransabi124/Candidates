
using Candidates.Repositories.Interface;
using Candidates.Repositories.Service;
using CandidatesDataAccess.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using CandidatesDataAccess.Mapper;
using AutoMapper;
using Serilog;

// Create the builder and configure the services
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
// Build the app
var app = builder.Build();

// Apply any pending migrations (custom method added here)
ApplyMigrations(app);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Custom method to apply migrations
void ApplyMigrations(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();

        // Apply any pending migrations
        dbContext.Database.Migrate();

        // Get the server/machine name
        var serverName = Environment.MachineName;

        // Log the server name and any other relevant information
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation($"Migrations applied successfully on server: {serverName}");
    }
    catch (Exception ex)
    {
        // Log errors or handle them as needed
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, $"An error occurred while applying migrations on server: {Environment.MachineName}");
    }
}
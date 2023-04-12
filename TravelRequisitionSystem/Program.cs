using Serilog;
using Serilog.Events;
using TravelRequisitionSystem.Extensions;

Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log-.txt",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateBootstrapLogger();

try
{
    Log.Information("Application is starting");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Add services to the container.

    builder.Services.ConfigureSqlDatabase();
    builder.Services.ConfigureJWT(builder.Configuration);
    builder.Services.ConfigureSqlContext(builder.Configuration);
    builder.Services.ConfigureCors();
    builder.Services.ConfigureRepository();
    builder.Services.ConfigureRepository();
    builder.Services.ConfigureService();
    builder.Services.ConfigureAutoMapper();
    builder.Services.ConfigureRandomGenerator();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("CorsPolicy");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

catch (Exception ex)
{
    Log.Fatal(ex, "Application Failed to Start");
}

finally
{
    Log.CloseAndFlush();
}



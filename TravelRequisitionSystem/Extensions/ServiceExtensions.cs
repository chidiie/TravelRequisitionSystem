using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using TravelRequisitionSystem.Data;
using TravelRequisitionSystem.Profiles;
using TravelRequisitionSystem.Repository.Implementations;
using TravelRequisitionSystem.Repository.Interfaces;
using TravelRequisitionSystem.Services;
using TravelRequisitionSystem.Services.Interfaces;
using TravelRequisitionSystem.Services.Services;

namespace TravelRequisitionSystem.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<TravelDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));


        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
         {
             options.AddPolicy("CorsPolicy", builder =>
             builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());
         });

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient<ITravelRepository, TravelRepository>();
            services.AddTransient<IRequisitionRepository, RequisitionRepository>();
        }
         
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddTransient<IRequisitionService, RequisitionService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IJWTService, JWTService>();
        }


        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(AutoMapperInitializer));

        public static void ConfigureRandomGenerator(this IServiceCollection services) =>
            services.AddScoped<IRandomGenerator, RandomGenerator>();

        public static void ConfigureSqlDatabase(this IServiceCollection services)
        {
            services.AddTransient<IDbConnection>(prov => new SqlConnection(prov.GetService<IConfiguration>().GetConnectionString("sqlConnection")));
            services.AddTransient(typeof(ISqlGenerator<>), typeof(SqlGenerator<>));
            services.AddTransient(typeof(DapperRepository<>));
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidAudience = configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });
        }
    }
}

using APPCOREHORTICOMMAND.APP;
using APPCOREHORTICOMMAND.IAPP;
using CROSSCUTTINGCOREHORTI.MIDDLEWARE;
using DATAACCESSCOREHORTICOMMAND.COMMAND;
using DATAACCESSCOREHORTICOMMAND.ICOMMAND;
using DATACOREHORTICOMMAND;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SERVICECOREHORTICOMMAND.ISERVICE;
using SERVICECOREHORTICOMMAND.SERVICE;
using System.IO.Compression;
using VALIDATIONCOREHORTICOMMAND.APPLICATION;
using VALIDATIONCOREHORTICOMMAND.DOMAIN;

namespace WEBAPICOREHORTICOMMAND
{
    public class Startup
    {
        private IConfiguration iConfiguration { get; }
        private readonly string strCorsConfig = "hortiCorsConfig";
        public Startup(IConfiguration configuration)
        {
            iConfiguration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBHORTICONTEXT>(opt =>
            {
                opt.UseSqlServer(iConfiguration.GetConnectionString("DBHORTICONTEXT"));
                opt.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            });

            services.AddCors(x => x.AddPolicy(strCorsConfig, p => { p.WithHeaders("DN-MR-WASATAIN-COMMAND"); }));

            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.PropertyNamingPolicy = null;
                x.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddResponseCompression(x =>
            {
                x.Providers.Add<BrotliCompressionProvider>();
                x.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<BrotliCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);
            services.Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);

            services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "WS REST - WEB API HORTI COMMAND",
                Title = "WS REST - WEB API HORTI",
                Version = "V1",
            }));

            HortiCommandApplicationServices(services);
            HortiCommandRepositoryServices(services);
            HortiCommandDomainServices(services);
            HortiCommandValidationServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WS REST - HORTI COMMAND");
                opt.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors(strCorsConfig);
            app.UseAuthorization();

            app.UseFatalExceptionMiddleware();
            app.UseValidationExceptionMiddleware();
            app.UseEntityFrameworkExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // CONTAINER DI - APP LAYER
        private void HortiCommandApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IUnitCommandApp, UnitCommandApp>();
        }

        // CONTAINER DI - DOMAIN SERVICE
        private void HortiCommandDomainServices(IServiceCollection services)
        {
            services.AddScoped<IUnitDomainService, UnitDomainService>();
        }

        // CONTAINER DI - REPOSITORY LAYER
        private void HortiCommandRepositoryServices(IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // CONTAINER DI - VALIDATION
        private void HortiCommandValidationServices(IServiceCollection services)
        {
            // APPLICATION
            services.AddSingleton<CreateUnitSignatureValidation>();
            services.AddSingleton<DeleteUnitSignatureValidation>();
            services.AddSingleton<UpdateUnitSignatureValidation>();

            //DOMAIN SERVICE
            services.AddSingleton<UnitDomainServiceValidation>();

        }
    }
}

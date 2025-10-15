using FluentValidation;
using GovConnect.Application;
using GovConnect.Data;
using GovConnect.Infrastructure.Abstractions.Caching;
using GovConnect.Infrastructure.Caching;
using GovConnect.Infrastructure.Mediator.Utils;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Api {
    public static class DependencyInjection {
        public static void ConfigureConfiguration(ConfigurationManager config) {
            config
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        public static void ConfigureHost(ConfigureHostBuilder host, ConfigurationManager config) {
            // Implementation of Serilog up here next
            // ...
        }

        public static void ConfigureServices(IServiceCollection services, ConfigurationManager config) {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(options
               => options.UseSqlServer(config.GetConnectionString("Default")));

            services.AddMediatorFromAssembly(typeof(MediatorAnchor).Assembly);
            services.AddValidatorsFromAssembly(typeof(MediatorAnchor).Assembly);

            services.AddScoped<IReferenceDataCache, ReferenceDataCache>();
        }

        public static void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env) {
            if (!env.IsEnvironment("PROD")) {
                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "GovConnect API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapFallback(context => {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;

                    return context
                        .Response
                        .WriteAsync(string.Empty);
                });
            });

            // Middlewares here
            // ...

            InitializeRequiredServices(app);
        }

        private static void InitializeRequiredServices(IApplicationBuilder app) {
            using var scope = app
                .ApplicationServices
                .CreateScope();

            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>()
                .Database
                .MigrateAsync()
                .GetAwaiter()
                .GetResult();

            scope.ServiceProvider
                .GetRequiredService<IReferenceDataCache>()
                .LoadAsync()
                .GetAwaiter()
                .GetResult();
        }
    }
}

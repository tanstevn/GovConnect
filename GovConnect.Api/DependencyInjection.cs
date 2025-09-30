using FluentValidation;
using GovConnect.Application;
using GovConnect.Data;
using GovConnect.Infrastructure.Mediator.Utils;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Api {
    public static class DependencyInjection {
        public static void ConfigureConfiguration(ConfigurationManager config) {
            config
                .AddJsonFile("appsettings.local.json", optional: false, reloadOnChange: true)
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

            services.AddMediatorFromAssembly(typeof(MediatorAnchor).Assembly);
            services.AddValidatorsFromAssembly(typeof(MediatorAnchor).Assembly);

            // Using In-Memory DB here to validate everthing in code first
            // Before moving into an actual database that will probably be PostgreSQL
            services.AddDbContext<ApplicationDbContext>(options
                => options.UseInMemoryDatabase("GovConnect"));
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
                    context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;

                    return context
                        .Response
                        .WriteAsync(string.Empty);
                });
            });

            // Middlewares here
            // And more
            // ...
        }
    }
}

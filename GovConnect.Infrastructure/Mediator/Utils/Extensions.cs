using GovConnect.Infrastructure.Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GovConnect.Infrastructure.Mediator.Utils {
    public static class Extensions {
        public static void AddMediatorFromAssembly(this IServiceCollection services, Assembly assembly) {
            ArgumentNullException.ThrowIfNull(assembly);

            var assemblyTypes = assembly.GetTypes();

            services.AddSingleton<IMediator, Mediator>();
            services.RegisterRequestHandlers(assemblyTypes);
            services.RegisterPipelineBehaviors(assemblyTypes);
        }

        private static void RegisterRequestHandlers(this IServiceCollection services, Type[] types) {

        }

        private static void RegisterPipelineBehaviors(this IServiceCollection services, Type[] types) {

        }
    }
}

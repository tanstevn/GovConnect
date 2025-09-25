﻿using GovConnect.Infrastructure.Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace GovConnect.Infrastructure.Mediator.Utils {
    [ExcludeFromCodeCoverage(Justification = "")]
    public static class Extensions {
        public static void AddMediatorFromAssembly(this IServiceCollection services, Assembly assembly) {
            ArgumentNullException.ThrowIfNull(assembly);

            var assemblyTypes = assembly.GetTypes();

            services.AddSingleton<IMediator, Mediator>();
            services.RegisterRequestHandlers(assemblyTypes);
            services.RegisterPipelineBehaviors(assemblyTypes);
        }

        private static void RegisterRequestHandlers(this IServiceCollection services, Type[] types) {
            var handlerTypes = types
                .Where(type => type.IsClass && !type.IsAbstract)
                .SelectMany(type => type.GetInterfaces()
                    .Where(@interface => @interface.IsGenericType
                        && @interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)),
                    (type, @interface) => new {
                        Interface = @interface,
                        Implementation = type
                    });

            foreach (var type in handlerTypes) {
                // Registering the Request Handler(s) as Open-Generic (e.g., typeof(IRequestHandler<,>))
                services.AddTransient(type.Interface, type.Implementation);
            }
        }

        private static void RegisterPipelineBehaviors(this IServiceCollection services, Type[] types) {
            var behaviorTypes = types
                .Where(type => type.IsClass && !type.IsAbstract
                    && type.IsGenericTypeDefinition)
                .Where(type => type.GetInterfaces()
                    .Any(@interface => @interface.IsGenericType
                        && @interface.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>)));

            foreach (var type in behaviorTypes) {
                // Same as how Request Handlers are registered
                services.AddTransient(typeof(IPipelineBehavior<,>), type);
            }
        }
    }
}

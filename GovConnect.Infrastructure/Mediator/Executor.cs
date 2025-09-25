using GovConnect.Infrastructure.Mediator.Abstractions;
using GovConnect.Infrastructure.Mediator.Utils;
using GovConnect.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GovConnect.Infrastructure.Mediator {
    internal sealed class Executor<TRequest, TResponse> : IExecutor 
        where TRequest : IRequest<TResponse> {
        public async Task<object> ExecuteAsync(object request, Type responseType, IServiceProvider serviceProvider, CancellationToken cancellationToken) {
            var requestHandler = serviceProvider
                .GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            RequestHandlerDelegate<TResponse> lastHandler = (token)
                => requestHandler.HandleAsync((TRequest)request, token);

            var pipelineBehaviors = serviceProvider
                .GetServices<IPipelineBehavior<TRequest, TResponse>>() ?? [];

            var behaviorsOrder = pipelineBehaviors
                .OrderByDescending(behavior =>
                    behavior.GetType()
                    .GetCustomAttribute<PipelineOrderAttribute>()!
                    .Order);

            var aggregateResult = behaviorsOrder
                .Aggregate(lastHandler,
                    (next, behavior) => (token) => behavior.HandleAsync((TRequest)request, next, token));

            return (await aggregateResult(cancellationToken))!;
        }
    }
}

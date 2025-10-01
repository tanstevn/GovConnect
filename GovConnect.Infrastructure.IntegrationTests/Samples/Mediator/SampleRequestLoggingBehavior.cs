using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Shared.Attributes;

namespace GovConnect.Infrastructure.IntegrationTests.Samples.Mediator {
    [PipelineOrder(1)]
    public sealed class SampleRequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> {
        public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default) {
            return await next(cancellationToken);
        }
    }
}

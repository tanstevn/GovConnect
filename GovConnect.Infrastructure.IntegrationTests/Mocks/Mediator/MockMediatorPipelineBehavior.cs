using GovConnect.Infrastructure.Mediator.Abstractions;
using GovConnect.Infrastructure.Mediator.Utils;
using GovConnect.Shared.Attributes;

namespace GovConnect.Infrastructure.IntegrationTests.Mocks.Mediator {
    [PipelineOrder(1)]
    public class MockMediatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> {
        public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default) {
            return await next(cancellationToken);
        }
    }
}

using GovConnect.Infrastructure.Mediator.Delegates;

namespace GovConnect.Infrastructure.Mediator.Abstractions {
    public interface IPipelineBehavior<in TRequest, TResponse>
        where TRequest : notnull {
        Task HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation = default);
    }
}

namespace GovConnect.Infrastructure.Abstractions.Mediator {
    public interface IPipelineBehavior<in TRequest, TResponse>
        where TRequest : notnull {
        Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default);
    }

    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken cancellationToken = default);
}

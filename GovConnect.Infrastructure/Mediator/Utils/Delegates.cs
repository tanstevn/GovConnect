namespace GovConnect.Infrastructure.Mediator.Utils {
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken cancellationToken = default);
}

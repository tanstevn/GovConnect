namespace GovConnect.Infrastructure.Mediator.Delegates {
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken cancellationToken = default);
}

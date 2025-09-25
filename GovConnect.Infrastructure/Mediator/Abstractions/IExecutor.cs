namespace GovConnect.Infrastructure.Mediator.Abstractions {
    internal interface IExecutor {
        Task<object> ExecuteAsync(object request, Type responseType, IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }
}

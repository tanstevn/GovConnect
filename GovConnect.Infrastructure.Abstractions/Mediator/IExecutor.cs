namespace GovConnect.Infrastructure.Abstractions.Mediator {
    public interface IExecutor {
        Task<object> ExecuteAsync(object request, IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }
}

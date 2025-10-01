namespace GovConnect.Infrastructure.Abstractions.Mediator {
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}

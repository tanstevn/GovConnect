namespace GovConnect.Infrastructure.Mediator.Abstractions {
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}

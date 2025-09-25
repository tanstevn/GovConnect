namespace GovConnect.Infrastructure.Mediator.Abstractions {
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}

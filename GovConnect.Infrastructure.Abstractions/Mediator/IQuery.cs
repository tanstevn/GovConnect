namespace GovConnect.Infrastructure.Abstractions.Mediator {
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}

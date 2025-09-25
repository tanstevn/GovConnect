using GovConnect.Infrastructure.Mediator.Abstractions;

namespace GovConnect.Infrastructure.Mediator {
    public sealed class Mediator : IMediator {
        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) {
            ArgumentNullException.ThrowIfNull(request);

            return default;
        }
    }
}

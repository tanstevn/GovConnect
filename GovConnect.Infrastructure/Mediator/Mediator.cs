using GovConnect.Infrastructure.Mediator.Abstractions;
using System.Collections.Concurrent;

namespace GovConnect.Infrastructure.Mediator {
    public sealed class Mediator : IMediator {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<Type, IExecutor> _requestHandlers;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _requestHandlers = new();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) {
            ArgumentNullException.ThrowIfNull(request);

            var requestHandler = _requestHandlers
                .GetOrAdd(
                    request.GetType(),
                    requestType => {
                        var executorType = typeof(Executor<,>)
                            .MakeGenericType(requestType, typeof(TResponse));

                        return (IExecutor)Activator.CreateInstance(executorType)!;
                    });

            var result = await requestHandler
                .ExecuteAsync(request, typeof(TResponse), _serviceProvider, cancellationToken);

            return (TResponse)result;
        }
    }
}
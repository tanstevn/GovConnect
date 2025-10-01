﻿namespace GovConnect.Infrastructure.Abstractions.Mediator {
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse> {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}

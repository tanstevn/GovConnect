using GovConnect.Infrastructure.Abstractions.Mediator;

namespace GovConnect.Infrastructure.IntegrationTests.Samples.Mediator {
    public class SampleMediatorQuery : IQuery<SampleMediatorQueryResult> { }

    public class SampleMediatorQueryResult {
        public bool IsSuccess { get; set; }
    }

    public class SampleMediatorQueryHandler : IRequestHandler<SampleMediatorQuery, SampleMediatorQueryResult> {
        public Task<SampleMediatorQueryResult> HandleAsync(SampleMediatorQuery request, CancellationToken cancellationToken = default) {
            return Task.FromResult(new SampleMediatorQueryResult {
                IsSuccess = true
            });
        }
    }
}

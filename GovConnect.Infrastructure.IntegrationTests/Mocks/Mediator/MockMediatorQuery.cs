using GovConnect.Infrastructure.Mediator.Abstractions;

namespace GovConnect.Infrastructure.IntegrationTests.Mocks.Mediator {
    public class MockMediatorQuery : IQuery<MockMediatorQueryResult> { }

    public class MockMediatorQueryResult {
        public bool IsSuccess { get; set; }
    }

    public class MockMediatorQueryHandler : IRequestHandler<MockMediatorQuery, MockMediatorQueryResult> {
        public Task<MockMediatorQueryResult> HandleAsync(MockMediatorQuery request, CancellationToken cancellationToken = default) {
            return Task.FromResult(new MockMediatorQueryResult {
                IsSuccess = true
            });
        }
    }
}

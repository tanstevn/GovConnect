using GovConnect.Infrastructure.IntegrationTests.Mocks.Mediator;
using GovConnect.Infrastructure.Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Mediatr = GovConnect.Infrastructure.Mediator.Mediator;

namespace GovConnect.Infrastructure.IntegrationTests {
    public class MediatorTests {
        private IServiceCollection _services;

        public MediatorTests()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<IMediator, Mediatr>();
        }

        [Fact]
        public async void Mediator_Request_Query_SendAsync_Runs_Successfully() {
            // Arrange
            _services.AddTransient<IRequestHandler<MockMediatorQuery, MockMediatorQueryResult>, MockMediatorQueryHandler>();
            _services.AddTransient<IPipelineBehavior<MockMediatorQuery, MockMediatorQueryResult>, MockMediatorPipelineBehavior<MockMediatorQuery, MockMediatorQueryResult>>();

            var serviceProvider = _services.BuildServiceProvider();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new MockMediatorQuery();

            // Act
            var result = await mediator.SendAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}

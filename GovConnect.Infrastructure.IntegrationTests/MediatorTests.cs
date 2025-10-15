using FluentValidation;
using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Infrastructure.IntegrationTests.Samples.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Mediatr = GovConnect.Infrastructure.Mediator.Mediator;
using Xunit;

namespace GovConnect.Infrastructure.IntegrationTests {
    public class MediatorTests {
        private IServiceCollection _services;
        private IMediator _mediator;

        public MediatorTests() {
            _services = new ServiceCollection();
            _services.AddSingleton<IMediator, Mediatr>();
            _services.AddValidatorsFromAssembly(typeof(SampleMediatorAnchor).Assembly);

            // Pipeline behaviors
            _services.AddTransient<IPipelineBehavior<SampleMediatorCommand, SampleMediatorCommandResult>, SampleRequestLoggingBehavior<SampleMediatorCommand, SampleMediatorCommandResult>>();
            _services.AddTransient<IPipelineBehavior<SampleMediatorCommand, SampleMediatorCommandResult>, SampleFluentValidationBehavior<SampleMediatorCommand, SampleMediatorCommandResult>>();

            // Request handlers
            _services.AddTransient<IRequestHandler<SampleMediatorQuery, SampleMediatorQueryResult>, SampleMediatorQueryHandler>();
            _services.AddTransient<IRequestHandler<SampleMediatorCommand, SampleMediatorCommandResult>, SampleMediatorCommandHandler>();

            _mediator = _services
                .BuildServiceProvider()
                .GetRequiredService<IMediator>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async void Mediator_Request_Query_Runs_Successfully() {
            // Arrange
            var request = new SampleMediatorQuery();

            // Act
            var result = await _mediator.SendAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async void Mediator_Request_Command_Runs_Successfully() {
            // Arrange
            var request = new SampleMediatorCommand {
                Id = long.MaxValue
            };

            var expectedResponse = new SampleMediatorCommandResult {
                Message = "Successfully triggered SampleMediatorCommand!"
            };

            // Act
            var result = await _mediator.SendAsync(request);

            // Assert
            Assert.True(!string.IsNullOrWhiteSpace(result.Message));
            Assert.Equivalent(expectedResponse, result);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async void Mediator_Request_Command_Failed_Validation_With_Negative_Value_Runs_Successfully() {
            // Arrange
            var request = new SampleMediatorCommand {
                Id = long.MinValue
            };

            // Act
            var result = _mediator.SendAsync(request);

            // Assert
            var exception = await Assert
                .ThrowsAsync<ValidationException>(async () => await result);

            Assert.True(exception is not null and ValidationException);
            Assert.NotEmpty(exception.Errors);
            Assert.Contains("should not be a negative value", exception.Message);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async void Mediator_Request_Command_Failed_Validation_With_Default_Value_Runs_Successfully() {
            // Arrange
            var request = new SampleMediatorCommand {
                Id = default
            };

            // Act
            var result = _mediator.SendAsync(request);

            // Assert
            var exception = await Assert
                .ThrowsAsync<ValidationException>(async () => await result);

            Assert.True(exception is not null and ValidationException);
            Assert.NotEmpty(exception.Errors);
            Assert.Contains("should not be empty, null, and 0", exception.Message);
        }
    }
}

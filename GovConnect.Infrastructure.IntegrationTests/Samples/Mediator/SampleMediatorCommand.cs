using FluentValidation;
using GovConnect.Infrastructure.Mediator.Abstractions;
using System.Net;

namespace GovConnect.Infrastructure.IntegrationTests.Samples.Mediator {
    public class SampleMediatorCommand : ICommand<SampleMediatorCommandResult> {
        public long Id { get; set; }
    }

    public class SampleMediatorCommandResult {
        public required string Message { get; set; }
    }

    public class SampleMediatorCommandValidator : AbstractValidator<SampleMediatorCommand> {
        public SampleMediatorCommandValidator() {
            RuleFor(param => param.Id)
                .NotEmpty()
                .WithMessage(param => $"{nameof(param.Id)} should not be empty, null, and 0.")
                .GreaterThanOrEqualTo(1)
                .WithMessage(param => $"{nameof(param.Id)} should not be a negative value.");        
        }
    }

    public class SampleMediatorCommandHandler : IRequestHandler<SampleMediatorCommand, SampleMediatorCommandResult> {
        public Task<SampleMediatorCommandResult> HandleAsync(SampleMediatorCommand request, CancellationToken cancellationToken = default) {
            ArgumentNullException.ThrowIfNull(request);

            return Task.FromResult(new SampleMediatorCommandResult {
                Message = "Successfully triggered SampleMediatorCommand!"
            });
        }
    }
}

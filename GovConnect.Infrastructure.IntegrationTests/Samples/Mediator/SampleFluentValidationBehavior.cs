using FluentValidation;
using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Shared.Attributes;

namespace GovConnect.Infrastructure.IntegrationTests.Samples.Mediator {
    [PipelineOrder(2)]
    public sealed class SampleFluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public SampleFluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
            _validators = validators;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default) {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (failures.Count != default) {
                throw new ValidationException(failures);
            }

            return await next(cancellationToken);
        }
    }
}

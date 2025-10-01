using FluentValidation;
using GovConnect.Data;
using GovConnect.Data.Entities;
using GovConnect.Infrastructure.Abstractions.Caching;
using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Application.Requests.Commands {
    public class RequestCommand : ICommand<Result<RequestCommandResult>> {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public long? RequestedByUserId { get; set; }
        public long SubcategoryId { get; set; }
        public long? CityId { get; set; }
        public long? MunicipalId { get; set; }
        public long BarangayId { get; set; }
        public long PriorityLevelId { get; set; }
    }

    public class RequestCommandResult {
        public required long Id { get; set; }
    }

    public class RequestCommandValidator : AbstractValidator<RequestCommand> {
        public RequestCommandValidator(IReferenceDataCache dataCache) {
            RuleFor(prop => prop.Title)
                .NotEmpty()
                .WithMessage(prop => $"{nameof(prop.Title)} cannot be null nor empty.");

            RuleFor(prop => prop.Description)
                .NotEmpty()
                .WithMessage(prop => $"{nameof(prop.Description)} cannot be null nor empty.");

            RuleFor(prop => prop.RequestedByUserId)
                .Must(userId => userId is null or > 1)
                .WithMessage(prop => $"{nameof(prop.RequestedByUserId)} must be either null or a positive value.");

            RuleFor(prop => prop.SubcategoryId)
                .GreaterThanOrEqualTo(1).WithMessage(prop => $"{nameof(prop.SubcategoryId)} should be greater than or equal to 1.")
                .Must(dataCache.SubcategoryIds.Contains)
                .WithMessage(prop => $"{nameof(prop.SubcategoryId)} does not exist.");

            RuleFor(prop => prop.CityId)
                .Must(cityId => cityId is null or > 1)
                .WithMessage(prop => $"{nameof(prop.CityId)} must be either null or a positive value.");

            RuleFor(prop => prop.MunicipalId)
                .Must(municipalId => municipalId is null or > 1)
                .WithMessage(prop => $"{nameof(prop.MunicipalId)} must be either null or a positive value.");

            RuleFor(prop => prop.BarangayId)
                .GreaterThanOrEqualTo(1)
                .WithMessage(prop => $"{nameof(prop.BarangayId)} should not be a negative value.")
                .Must(dataCache.BarangayIds.Contains)
                .WithMessage(prop => $"{nameof(prop.BarangayId)} does not exist.");

            RuleFor(prop => prop.PriorityLevelId)
                .GreaterThanOrEqualTo(1)
                .WithMessage(prop => $"{nameof(prop.PriorityLevelId)} should not be a negative value.")
                .Must(dataCache.PriorityLevelIds.Contains)
                .WithMessage(prop => $"{nameof(prop.PriorityLevelId)} does not exist.");
        }
    }

    public class RequestCommandHandler : IRequestHandler<RequestCommand, Result<RequestCommandResult>> {
        private readonly ApplicationDbContext _dbContext;

        public RequestCommandHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Result<RequestCommandResult>> HandleAsync(RequestCommand request, CancellationToken cancellationToken = default) {
            request.RequestedByUserId ??= await _dbContext
                .Users
                .Where(user => user.Auth0UserId == "auth0|anonymous")
                .Select(user => user.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var requestEntity = new Request {
                Title = request.Title!,
                Description = request.Description!,
                SubcategoryId = request.SubcategoryId,
                BarangayId = request.BarangayId,
                RequestedByUserId = (long)request.RequestedByUserId,
                PriorityLevelId = request.PriorityLevelId,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Requests.AddAsync(requestEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new() {
                Data = new() {
                    Id = requestEntity.Id
                }
            };
        }
    }
}

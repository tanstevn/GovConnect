using GovConnect.Application.Requests.Commands;
using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GovConnect.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase {
        private readonly IMediator _mediator;

        public RequestsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<RequestCommandResult>> CreateRequest([FromBody] RequestCommand command, CancellationToken cancellationToken) {
            return await _mediator.SendAsync(command, cancellationToken);
        }
    }
}

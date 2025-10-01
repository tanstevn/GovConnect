using GovConnect.Application.Requests.Commands;
using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GovConnect.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController(IMediator mediator) : ControllerBase {
        [HttpPost]
        public async Task<Result<RequestCommandResult>> CreateRequest([FromBody] RequestCommand command) {
            return await mediator.SendAsync(command);
        }
    }
}

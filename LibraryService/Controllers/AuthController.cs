using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Queries.AdminQueries;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : MediatingControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        { }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerOperation(Summary = "Check the token", OperationId = "CheckAdminToken")]
        public async Task<IActionResult> CheckAdminToken([FromBody] AdminDTO admin, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new ExistAdminQuery(admin), cancellationToken: cancellationToken);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.AdminCommands;
using LibraryService.API.Application.Queries.AdminQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Admin;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController : MediatingControllerBase
    {
        public AdminController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<FoundAdminDTO>))]
        [SwaggerOperation(Summary = "Search admins", OperationId = "SearchAdmins")]
        public async Task<IActionResult> SearchAdmins([FromBody] AdminSearchCondition searchCondition, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new SearchAdminQuery(searchCondition), cancellationToken: cancellationToken);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        [SwaggerOperation(Summary = "Add a new admin", OperationId = "AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromBody] AdminDTO admin, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new AddAdminCommand(admin), cancellationToken: cancellationToken);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [SwaggerOperation(Summary = "Delete a admin", OperationId = "DeleteAdmin")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new DeleteAuthorCommand(id), cancellationToken: cancellationToken);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdminDTO))]
        [SwaggerOperation(Summary = "Update a admin", OperationId = "UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin([FromRoute] long id, [FromBody] AdminDTO admin, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new UpdateAuthorCommand(id, admin), cancellationToken: cancellationToken);
        }
    }
}

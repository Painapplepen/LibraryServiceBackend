using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.LibraryCommands;
using LibraryService.API.Application.Queries.LibraryQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Library;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Library;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class LibraryController : MediatingControllerBase
    {
        public LibraryController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<FoundLibraryDTO>))]
        [SwaggerOperation(Summary = "Search library", OperationId = "SearchLibrary")]
        public async Task<IActionResult> SearchLibrary([FromBody] LibrarySearchCondition searchCondition, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new SearchLibraryQuery(searchCondition), cancellationToken: cancellationToken);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        [SwaggerOperation(Summary = "Add a new library", OperationId = "AddLibrary")]
        public async Task<IActionResult> AddLibrary([FromBody] LibraryDTO library, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new AddLibraryCommand(library), cancellationToken: cancellationToken);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [SwaggerOperation(Summary = "Delete a library", OperationId = "DeleteLibrary")]
        public async Task<IActionResult> DeleteLibrary([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new DeleteLibraryCommand(id), cancellationToken: cancellationToken);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(LibraryDTO))]
        [SwaggerOperation(Summary = "Update a library", OperationId = "UpdateLibrary")]
        public async Task<IActionResult> UpdateLibrary([FromRoute] long id, [FromBody] LibraryDTO library, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new UpdateLibraryCommand(id, library), cancellationToken: cancellationToken);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(LibraryDTO))]
        [SwaggerOperation(Summary = "Get the details of a library", OperationId = "GetLibrary")]
        public async Task<IActionResult> GetLibrary([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetLibraryQuery(id), cancellationToken: cancellationToken);
        }
    }
}

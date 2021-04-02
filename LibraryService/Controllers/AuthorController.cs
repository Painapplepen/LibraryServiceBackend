using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.AuthorCommands;
using LibraryService.API.Application.Queries.AuthorQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Author;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : MediatingControllerBase
    {
        public AuthorController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<FoundAuthorDTO>))]
        [SwaggerOperation(Summary = "Search author", OperationId = "SearchAuthor")]
        public async Task<IActionResult> SearchAuthor([FromBody] AuthorSearchCondition searchCondition, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new SearchAuthorQuery(searchCondition), cancellationToken: cancellationToken);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        [SwaggerOperation(Summary = "Add a new author", OperationId = "AddAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDTO author, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new AddAuthorCommand(author), cancellationToken: cancellationToken);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [SwaggerOperation(Summary = "Delete a author", OperationId = "DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new DeleteAuthorCommand(id), cancellationToken: cancellationToken);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthorDTO))]
        [SwaggerOperation(Summary = "Update a author", OperationId = "UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] long id, [FromBody] AuthorDTO author, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new UpdateAuthorCommand(id, author), cancellationToken: cancellationToken);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthorDTO))]
        [SwaggerOperation(Summary = "Get the details of a author", OperationId = "GetAuthor")]
        public async Task<IActionResult> GetAuthor([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetAuthorQuery(id), cancellationToken: cancellationToken);
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<FoundAuthorDTO>))]
        [SwaggerOperation(Summary = "Get all authors", OperationId = "GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetAllAuthorQuery(), cancellationToken: cancellationToken);
        }
    }
}

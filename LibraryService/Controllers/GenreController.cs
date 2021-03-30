using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.BookCommands;
using LibraryService.API.Application.Commands.GenreCommands;
using LibraryService.API.Application.Queries.BookQueries;
using LibraryService.API.Application.Queries.GenreQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenreController : MediatingControllerBase
    {
        public GenreController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<FoundBookDTO>))]
        [SwaggerOperation(Summary = "Search genre", OperationId = "SearchGenre")]
        public async Task<IActionResult> SearchGenre([FromBody] GenreSearchCondition searchCondition, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new SearchGenreQuery(searchCondition), cancellationToken: cancellationToken);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        [SwaggerOperation(Summary = "Add a new genre", OperationId = "AddGenre")]
        public async Task<IActionResult> AddGenre([FromBody] GenreDTO genre, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new AddGenreCommand(genre), cancellationToken: cancellationToken);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [SwaggerOperation(Summary = "Delete a genre", OperationId = "DeleteGenre")]
        public async Task<IActionResult> DeleteGenre([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new DeleteGenreCommand(id), cancellationToken: cancellationToken);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdminDTO))]
        [SwaggerOperation(Summary = "Update a genre", OperationId = "UpdateGenre")]
        public async Task<IActionResult> UpdateGenre([FromRoute] long id, [FromBody] GenreDTO book, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new UpdateGenreCommand(id, book), cancellationToken: cancellationToken);
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.BookCommands;
using LibraryService.API.Application.Queries.BookQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/book")]
    [ApiController]
    [Authorize]
    public class BookController : MediatingControllerBase
    {
        public BookController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<FoundBookDTO>))]
        [SwaggerOperation(Summary = "Search books", OperationId = "SearchBook")]
        public async Task<IActionResult> SearchBooks([FromBody] BookSearchCondition searchCondition, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new SearchBookQuery(searchCondition), cancellationToken: cancellationToken);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        [SwaggerOperation(Summary = "Add a new book", OperationId = "AddBook")]
        public async Task<IActionResult> AddBook([FromBody] BookDTO book, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new AddBookCommand(book), cancellationToken: cancellationToken);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [SwaggerOperation(Summary = "Delete a book", OperationId = "DeleteBook")]
        public async Task<IActionResult> DeleteBook([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new DeleteBookCommand(id), cancellationToken: cancellationToken);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [SwaggerOperation(Summary = "Update a book", OperationId = "UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromRoute] long id, [FromBody] BookDTO book, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new UpdateBookCommand(id, book), cancellationToken: cancellationToken);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [SwaggerOperation(Summary = "Get the details of a book", OperationId = "GetBook")]
        public async Task<IActionResult> GetBook([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetBookQuery(id), cancellationToken: cancellationToken);
        }

        [HttpGet("getAll")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<FoundBookDTO>))]
        [SwaggerOperation(Summary = "Get all books", OperationId = "GetAllBooks")]
        public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetAllBookQuery(), cancellationToken: cancellationToken);
        }
    }
}

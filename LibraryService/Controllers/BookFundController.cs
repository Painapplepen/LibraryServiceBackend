using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.BookCommands;
using LibraryService.API.Application.Commands.BookFundCommands;
using LibraryService.API.Application.Queries.BookFundQueries;
using LibraryService.API.Application.Queries.BookQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.BookFund;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.API.Contracts.Outgoing.BookFund;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/bookFund")]
    [ApiController]
    public class BookFundController : MediatingControllerBase
    {
        public BookFundController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<FoundBookFundDTO>))]
        [SwaggerOperation(Summary = "Search book funds", OperationId = "SearchBookFunds")]
        public async Task<IActionResult> SearchBookFunds([FromBody] BookFundSearchCondition searchCondition, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new SearchBookFundQuery(searchCondition), cancellationToken: cancellationToken);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        [SwaggerOperation(Summary = "Add a new book fund", OperationId = "AddBookFund")]
        public async Task<IActionResult> AddBookFund([FromBody] BookFundDTO bookFund, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new AddBookFundCommand(bookFund), cancellationToken: cancellationToken);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [SwaggerOperation(Summary = "Delete a book fund", OperationId = "DeleteBookFund")]
        public async Task<IActionResult> DeleteBookFund([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new DeleteBookFundCommand(id), cancellationToken: cancellationToken);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BookFundDTO))]
        [SwaggerOperation(Summary = "Update a book fund", OperationId = "UpdateBookFund")]
        public async Task<IActionResult> UpdateBookFund([FromRoute] long id, [FromBody] BookFundDTO bookFund, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new UpdateBookFundCommand(id, bookFund), cancellationToken: cancellationToken);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BookFundDTO))]
        [SwaggerOperation(Summary = "Get the details of a book fund", OperationId = "GetBookFund")]
        public async Task<IActionResult> GetBookFund([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetBookFundQuery(id), cancellationToken: cancellationToken);
        }
    }
}

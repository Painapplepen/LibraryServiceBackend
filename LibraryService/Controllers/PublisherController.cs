using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.PublisherCommands;
using LibraryService.API.Application.Queries.PublisherQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Publisher;
using LibraryService.API.Host.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.API.Host.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : MediatingControllerBase
    {
        public PublisherController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<FoundPublisherDTO>))]
        [SwaggerOperation(Summary = "Search publisher", OperationId = "SearchPublisher")]
        public async Task<IActionResult> SearchPublisher([FromBody] PublisherSearchCondition searchCondition, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new SearchPublisherQuery(searchCondition), cancellationToken: cancellationToken);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        [SwaggerOperation(Summary = "Add a new publisher", OperationId = "AddPublisher")]
        public async Task<IActionResult> AddPublisher([FromBody] PublisherDTO publisher, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new AddPublisherCommand(publisher), cancellationToken: cancellationToken);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [SwaggerOperation(Summary = "Delete a publisher", OperationId = "DeletePublisher")]
        public async Task<IActionResult> DeletePublisher([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new DeletePublisherCommand(id), cancellationToken: cancellationToken);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PublisherDTO))]
        [SwaggerOperation(Summary = "Update a publisher", OperationId = "UpdatePublisher")]
        public async Task<IActionResult> UpdatePublisher([FromRoute] long id, [FromBody] PublisherDTO publisher, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandAsync(new UpdatePublisherCommand(id, publisher), cancellationToken: cancellationToken);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PublisherDTO))]
        [SwaggerOperation(Summary = "Get the details of a publisher", OperationId = "GetPublisher")]
        public async Task<IActionResult> GetPublisher([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetPublisherQuery(id), cancellationToken: cancellationToken);
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<FoundPublisherDTO>))]
        [SwaggerOperation(Summary = "Get all publishers", OperationId = "GetAllPublishers")]
        public async Task<IActionResult> GetAllPublishers(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(new GetAllPublisherQuery(), cancellationToken: cancellationToken);
        }
    }
}

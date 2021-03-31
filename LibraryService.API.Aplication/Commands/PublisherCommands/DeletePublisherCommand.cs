using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.PublisherCommands
{
    public class DeletePublisherCommand : IRequest
    {
        public long Id { get; }

        public DeletePublisherCommand(long id)
        {
            Id = id;
        }
    }

    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
    {
        private readonly IPublisherService publisherService;

        public DeletePublisherCommandHandler(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            await publisherService.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}

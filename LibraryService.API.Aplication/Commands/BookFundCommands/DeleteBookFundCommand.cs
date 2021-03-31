using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.BookFundCommands
{
    public class DeleteBookFundCommand : IRequest
    {
        public long Id { get; }

        public DeleteBookFundCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteBookFundCommandHandler : IRequestHandler<DeleteBookFundCommand>
    {
        private readonly IBookFundService bookFundService;

        public DeleteBookFundCommandHandler(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;
        }

        public async Task<Unit> Handle(DeleteBookFundCommand request, CancellationToken cancellationToken)
        {
            await bookFundService.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
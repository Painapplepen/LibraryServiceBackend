using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.BookCommands
{
    public class DeleteBookCommand : IRequest
    {
        public long Id { get; }

        public DeleteBookCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookService bookService;

        public DeleteBookCommandHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await bookService.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
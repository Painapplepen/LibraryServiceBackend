using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.BookCommands;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Book
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        private IBookService bookService;

        public DeleteBookValidator(IBookService bookService)
        {
            this.bookService = bookService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.BookNotFound, cmd.Id));
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await bookService.ExistsAsync(id, cancellationToken);
        }
    }
}

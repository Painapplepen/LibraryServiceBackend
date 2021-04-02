using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Abstractions
{
    public class BookFundValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : BookFundCommandBase<TResponse>
    {
        private readonly ILibraryService libraryService;
        private readonly IBookService bookService;

        public BookFundValidatorBase(IBookService bookService, ILibraryService libraryService)
        {
            this.bookService = bookService;
            this.libraryService = libraryService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Entity)
                .NotNull()
                .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Entity)));

            RuleFor(cmd => cmd.Entity.Amount)
                .Must(NotBeLessThanNull)
                .WithMessage(Resources.Resources.BookFundAmountNotBeLessThanNull);

            RuleFor(cmd => cmd.Entity.BookId)
                .MustAsync(ExistBook)
                .WithMessage(Resources.Resources.GenreNotFound);

            RuleFor(cmd => cmd.Entity.LibraryId)
                .MustAsync(ExistLibrary)
                .WithMessage(Resources.Resources.GenreNotFound);
        }

        private bool NotBeLessThanNull(long value)
        {
            return value > 0;
        }

        private async Task<bool> ExistBook(long id, CancellationToken cancellationToken)
        {
            return await bookService.ExistsAsync(id, cancellationToken);
        }

        private async Task<bool> ExistLibrary(long id, CancellationToken cancellationToken)
        {
            return await libraryService.ExistsAsync(id, cancellationToken);
        }
    }
}
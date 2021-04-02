using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Queries.BookQueries;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Book
{
    public class GetBookValidator : AbstractValidator<GetBookQuery>
    {
        private readonly IBookService bookService;

        public GetBookValidator(IBookService bookService)
        {
            this.bookService = bookService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => string.Format(Resources.Resources.ValueRequired, nameof(query.Id)));

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.BookNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await bookService.ExistsAsync(id, cancellationToken);
        }
    }
}

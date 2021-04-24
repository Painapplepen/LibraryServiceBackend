using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Abstractions
{
    public class BookValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : BookCommandBase<TResponse>
    {
        private readonly IPublisherService publisherService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;

        public BookValidatorBase(IPublisherService publisherService,
            IAuthorService authorService,
            IGenreService genreService)
        {
            this.genreService = genreService;
            this.publisherService = publisherService;
            this.authorService = authorService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Entity)
                .NotNull()
                .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Entity)));

            RuleFor(cmd => cmd.Entity.ISBN)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(Resources.Resources.BookISBNRequired);

            RuleFor(cmd => cmd.Entity.Title)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(Resources.Resources.BookTitleRequired);

            RuleFor(cmd => cmd.Entity.AmountPage)
                .Must(NotBeLessThanNull)
                .WithMessage(Resources.Resources.BookAmountPageNotBeLessThanNull);

            RuleFor(cmd => cmd.Entity.AmountPage)
                .Must(NotBeLessThanNull)
                .WithMessage(Resources.Resources.BookYearNotBeLessThanNull);

            RuleFor(cmd => cmd.Entity.GenreId)
                .MustAsync(ExistGenre)
                .WithMessage(Resources.Resources.GenreNotFound);

            RuleFor(cmd => cmd.Entity.PublisherId)
                .MustAsync(ExistPublisher)
                .WithMessage(Resources.Resources.PublisherNotFound);

            RuleFor(cmd => cmd.Entity.AuthorId)
                .MustAsync(ExistAuthor)
                .WithMessage(Resources.Resources.AuthorNotFound);
        }

        private bool NotBeNullOrWhitespace(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        private bool NotBeLessThanNull(long value)
        {
            return value > 0;
        }

        private async Task<bool> ExistGenre(long id, CancellationToken cancellationToken)
        {
            return await genreService.ExistsAsync(id, cancellationToken);
        }

        private async Task<bool> ExistPublisher(long id, CancellationToken cancellationToken)
        {
            return await publisherService.ExistsAsync(id, cancellationToken);
        }

        private async Task<bool> ExistAuthor(long id, CancellationToken cancellationToken)
        {
            return await authorService.ExistsAsync(id, cancellationToken);
        }
    }
}
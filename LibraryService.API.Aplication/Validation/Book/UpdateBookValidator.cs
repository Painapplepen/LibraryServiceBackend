using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.BookCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Book
{
    public class UpdateBookValidator : BookValidatorBase<UpdateBookCommand, Response>
    {
        private readonly IBookService bookService;

        public UpdateBookValidator(IBookService bookService, 
            IPublisherService publisherService,
            IAuthorService authorService,
            IGenreService genreService) : base(publisherService, authorService, genreService)
        {
            this.bookService = bookService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.BookNotFound);
        }

        private async Task<bool> Exist(long? id, CancellationToken cancellationToken)
        {
            return id.HasValue && await bookService.ExistsAsync(id.Value, cancellationToken);
        }
    }
}

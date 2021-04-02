using LibraryService.API.Application.Commands.BookCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Book
{
    public class AddBookValidator : BookValidatorBase<AddBookCommand, long>
    {
        public AddBookValidator(IPublisherService publisherService,
            IAuthorService authorService,
            IGenreService genreService) : base(publisherService, authorService, genreService)
        { }
    }
}

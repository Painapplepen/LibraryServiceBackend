using LibraryService.API.Application.Commands.BookFundCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.BookFund
{
    public class AddBookFundValidator : BookFundValidatorBase<AddBookFundCommand, long>
    {
        public AddBookFundValidator(IBookService bookService, ILibraryService libraryService) : base(bookService, libraryService)
        { }
    }
}

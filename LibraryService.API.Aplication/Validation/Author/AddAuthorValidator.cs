using LibraryService.API.Application.Commands.AuthorCommands;
using LibraryService.API.Application.Validation.Abstractions;

namespace LibraryService.API.Application.Validation.Author
{
    public class AddAuthorValidator : AuthorValidatorBase<AddAuthorCommand, long>
    {
        public AddAuthorValidator() : base() { }
    }
}

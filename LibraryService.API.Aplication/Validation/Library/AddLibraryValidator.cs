using LibraryService.API.Application.Commands.LibraryCommands;
using LibraryService.API.Application.Validation.Abstractions;

namespace LibraryService.API.Application.Validation.Library
{
    public class AddLibraryValidator : LibraryValidatorBase<AddLibraryCommand, long>
    {
        public AddLibraryValidator() : base() { }
    }
}

using LibraryService.API.Application.Commands.GenreCommands;
using LibraryService.API.Application.Validation.Abstractions;

namespace LibraryService.API.Application.Validation.Genre
{
    public class AddGenreValidator : GenreValidatorBase<AddGenreCommand, long>
    {
        public AddGenreValidator() : base() { }
    }
}

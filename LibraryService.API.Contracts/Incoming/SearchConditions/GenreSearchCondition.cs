using LibraryService.API.Contracts.Incoming.Abstractions;

namespace LibraryService.API.Contracts.Incoming.SearchConditions
{
    public class GenreSearchCondition : PagedDTOBase
    {
        public string[] Name { get; set; }
    }
}

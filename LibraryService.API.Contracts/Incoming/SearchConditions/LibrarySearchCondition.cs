using LibraryService.API.Contracts.Incoming.Abstractions;

namespace LibraryService.API.Contracts.Incoming.SearchConditions
{
    public class LibrarySearchCondition : PagedDTOBase
    {
        public string[] Name { get; set; }
        public string[] Address { get; set; }
        public string[] Telephone { get; set; }
    }
}

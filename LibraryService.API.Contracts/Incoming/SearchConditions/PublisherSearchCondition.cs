using LibraryService.API.Contracts.Incoming.Abstractions;

namespace LibraryService.API.Contracts.Incoming.SearchConditions
{
    public class PublisherSearchCondition : PagedDTOBase
    {
        public string[] Name { get; set; }
    }
}

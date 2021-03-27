using LibraryService.API.Contracts.Incoming.Abstractions;

namespace LibraryService.API.Contracts.Incoming.SearchConditions
{
    public class AdminSearchCondition : PagedDTOBase
    {
        public string[] Login { get; set; }
    }
}
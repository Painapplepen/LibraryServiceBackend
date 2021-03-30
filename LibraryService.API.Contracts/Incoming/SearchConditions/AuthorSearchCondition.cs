using LibraryService.API.Contracts.Incoming.Abstractions;

namespace LibraryService.API.Contracts.Incoming.SearchConditions
{
    public class AuthorSearchCondition : PagedDTOBase
    {
        public string[] Surname { get; set; }
        public string[] Patronymic { get; set; }
        public string[] Name { get; set; }
    }
}

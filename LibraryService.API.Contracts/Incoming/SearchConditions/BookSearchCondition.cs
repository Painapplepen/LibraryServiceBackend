using LibraryService.API.Contracts.Incoming.Abstractions;

namespace LibraryService.API.Contracts.Incoming.SearchConditions
{
    public class BookSearchCondition : PagedDTOBase
    {
        public string[] ISBN { get; set; }
        public string[] Title { get; set; }
        public string[] AuthorSurname { get; set; }
        public string[] AuthorPatronymic { get; set; }
        public string[] AuthorName { get; set; }
        public string[] Publisher { get; set; }
        public string[] Genre { get; set; }
        public long[] AmountPage { get; set; }
        public long[] Year { get; set; }
    }
}

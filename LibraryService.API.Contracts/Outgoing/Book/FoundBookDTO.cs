using LibraryService.API.Contracts.Outgoing.Author;
using LibraryService.API.Contracts.Outgoing.Genre;
using LibraryService.API.Contracts.Outgoing.Publisher;

namespace LibraryService.API.Contracts.Outgoing.Book
{
    public class FoundBookDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorPatronymic { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
    }
}

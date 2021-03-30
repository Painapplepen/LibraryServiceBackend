using LibraryService.API.Contracts.Outgoing.Author;
using LibraryService.API.Contracts.Outgoing.Genre;
using LibraryService.API.Contracts.Outgoing.Publisher;

namespace LibraryService.API.Contracts.Outgoing.Book
{
    public class FoundBookDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public FoundAuthorDTO Author { get; set; }
        public FoundPublisherDTO Publisher { get; set; }
        public FoundGenreDTO Genre { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
    }
}

using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;

namespace LibraryService.API.Contracts.Outgoing.Book
{
    public class FoundBookDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
        public PublisherFoundDTO Publisher { get; set; }
        public GenreDTO Genre { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
    }
}

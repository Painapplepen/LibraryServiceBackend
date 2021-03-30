using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;

namespace LibraryService.API.Contracts.IncomingOutgoing.Book
{
    public class BookDTO
    {
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
        public PublisherDTO Publisher { get; set; }
        public GenreDTO Genre { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
    }
}

namespace LibraryService.API.Contracts.IncomingOutgoing.Book
{
    public class BookDTO
    {
        public string Title { get; set; }
        public long AuthorId { get; set; }
        public long PublisherId { get; set; }
        public long GenreId { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
    }
}

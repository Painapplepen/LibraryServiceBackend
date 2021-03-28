using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Library;

namespace LibraryService.API.Contracts.Outgoing.BookFund
{
    public class FoundBookFundDTO
    {
        public long Id { get; set; }
        public string BookTitle { get; set; }
        public BookDTO Book { get; set; }
        public LibraryDTO Library { get; set; }
    }
}

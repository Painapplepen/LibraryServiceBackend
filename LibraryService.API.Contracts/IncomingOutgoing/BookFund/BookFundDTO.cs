using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Library;

namespace LibraryService.API.Contracts.IncomingOutgoing.BookFund
{
    public class BookFundDTO
    {
        public long Amount { get; set; }
        public BookDTO Book { get; set; }
        public LibraryDTO Library { get; set; }
    }
}

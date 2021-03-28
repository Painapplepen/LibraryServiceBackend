using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Library;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.API.Contracts.Outgoing.Library;

namespace LibraryService.API.Contracts.Outgoing.BookFund
{
    public class FoundBookFundDTO
    {
        public long Id { get; set; }
        public long Amount { get; set; }
        public FoundBookDTO Book { get; set; }
        public FoundLibraryDTO Library { get; set; }
    }
}

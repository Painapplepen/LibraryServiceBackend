namespace LibraryService.API.Contracts.IncomingOutgoing.BookFund
{
    public class BookFundDTO
    {
        public long Amount { get; set; }
        public long BookId { get; set; }
        public long LibraryId { get; set; }
    }
}

namespace LibraryService.API.Contracts.Outgoing.BookFund
{
    public class FoundBookFundDTO
    {
        public long Id { get; set; }
        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public long BookAmountPage { get; set; }
        public long BookYear { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorPatronymic { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string LibraryName { get; set; }
        public string LibraryAddress { get; set; }
        public string LibraryTelephone { get; set; }
        public long Amount { get; set; }
    }
}

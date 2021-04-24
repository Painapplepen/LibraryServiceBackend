namespace LibraryService.Data.Domain.Models
{
    public class BookView : KeyedEntityBase
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorPatronymic { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
    }
}
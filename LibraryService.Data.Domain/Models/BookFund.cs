namespace LibraryService.Data.Domain.Models
{
    public class BookFund : KeyedEntityBase
    {
        public long? BookId { get; set; }
        public Book Book { get; set; }
        public long? LibraryId { get; set; }
        public Library Library { get; set; }
        public long Amount { get; set; }
    }
}

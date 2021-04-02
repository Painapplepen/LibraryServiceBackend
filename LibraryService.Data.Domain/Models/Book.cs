using System.Collections.Generic;

namespace LibraryService.Data.Domain.Models
{
    public class Book : KeyedEntityBase
    {
        public string Title { get; set; }
        public long? AuthorId { get; set; }
        public Author Author { get; set; }
        public long? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public long? GenreId { get; set; }
        public Genre Genre { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
        public IList<BookFund> BookFunds { get; set; } = new List<BookFund>();
    }
}

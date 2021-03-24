using LibraryService.Data.Domain;

namespace LibraryService.Domain.Core.Entities
{
    public class Book : KeyedEntityBase
    {
        public string Title { get; set; }
        public virtual long AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual long PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual long GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
    }
}

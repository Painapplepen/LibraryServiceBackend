using LibraryService.Data.Domain;

namespace LibraryService.Domain.Core.Entities
{
    public class Book : KeyedEntityBase
    {
        public string Title { get; set; }
        public virtual Author BookAuthor { get; set; }
        public virtual Publisher BookPublisher { get; set; }
        public virtual Genre BookGenre { get; set; }
        public long AmountPage { get; set; }
        public long Year { get; set; }
    }
}

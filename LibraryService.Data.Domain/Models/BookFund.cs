using LibraryService.Data.Domain;

namespace LibraryService.Domain.Core.Entities
{
    public class BookFund : KeyedEntityBase
    {
        public virtual long BookId { get; set; }
        public virtual Book Book { get; set; }
        public virtual long LibraryId { get; set; }
        public virtual Library Library { get; set; }
        public long Amount { get; set; }
    }
}

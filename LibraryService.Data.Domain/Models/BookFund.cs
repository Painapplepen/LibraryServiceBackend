using LibraryService.Data.Domain;

namespace CourseWork6sem.Domain.Core.Entities
{
    public class BookFund : KeyedEntityBase
    {
        public virtual Book BookInFund { get; set; }
        public virtual Library Library { get; set; }
        public long Amount { get; set; }
    }
}

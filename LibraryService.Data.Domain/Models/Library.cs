using LibraryService.Data.Domain;

namespace CourseWork6sem.Domain.Core.Entities
{
    public class Library : KeyedEntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
    }
}

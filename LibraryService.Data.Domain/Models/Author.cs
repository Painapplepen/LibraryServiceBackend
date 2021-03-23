using LibraryService.Data.Domain;

namespace CourseWork6sem.Domain.Core.Entities
{
    public class Author : KeyedEntityBase
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; } 
    }
}

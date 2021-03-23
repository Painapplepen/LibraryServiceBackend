using LibraryService.Data.Domain;

namespace CourseWork6sem.Domain.Core.Entities
{
    public class Admin : KeyedEntityBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

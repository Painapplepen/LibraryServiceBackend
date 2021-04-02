using System.Collections.Generic;

namespace LibraryService.Data.Domain.Models
{
    public class Author : KeyedEntityBase
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public IList<Book> Books { get; set; } = new List<Book>();
    }
}

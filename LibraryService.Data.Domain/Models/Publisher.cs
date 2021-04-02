using System.Collections.Generic;

namespace LibraryService.Data.Domain.Models
{
    public class Publisher : KeyedEntityBase
    {
        public string Name { get; set; }
        public IList<Book> Books { get; set; } = new List<Book>();
    }
}

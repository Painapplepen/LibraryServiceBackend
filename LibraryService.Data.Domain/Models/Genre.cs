using LibraryService.Data.Domain;

namespace LibraryService.Domain.Core.Entities
{
    public class Genre : KeyedEntityBase
    {
        public string Name { get; set; }
    }
}

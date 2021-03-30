using LibraryService.Data.Domain;

namespace LibraryService.Domain.Core.Entities
{
    public class Publisher : KeyedEntityBase
    {
        public string Name { get; set; }
    }
}

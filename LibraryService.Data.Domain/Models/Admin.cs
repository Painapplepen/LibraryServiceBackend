using LibraryService.Data.Domain;

namespace LibraryService.Domain.Core.Entities
{
    public class Admin : KeyedEntityBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

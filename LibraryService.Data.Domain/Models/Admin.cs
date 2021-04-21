namespace LibraryService.Data.Domain.Models
{
    public class Admin : KeyedEntityBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

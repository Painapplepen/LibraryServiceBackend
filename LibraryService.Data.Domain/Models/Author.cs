namespace LibraryService.Data.Domain.Models
{
    public class Author : KeyedEntityBase
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; } 
    }
}

namespace LibraryService.Data.Domain.Models
{
    public class Library : KeyedEntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
    }
}

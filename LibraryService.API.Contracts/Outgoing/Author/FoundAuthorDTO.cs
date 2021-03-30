namespace LibraryService.API.Contracts.Outgoing.Author
{
    public class FoundAuthorDTO
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
    }
}

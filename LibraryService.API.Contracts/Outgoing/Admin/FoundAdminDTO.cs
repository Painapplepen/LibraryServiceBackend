namespace LibraryService.API.Contracts.Outgoing.Admin
{
    public class FoundAdminDTO
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
namespace SimpleDataService.BAL
{
    public class AccountVM
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? AccountTypeId { get; set; }

        public AccountTypeVM AccountType { get; set; }
        public UserVM User { get; set; }
    }
}

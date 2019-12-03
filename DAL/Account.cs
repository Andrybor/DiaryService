using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }
        public User User { get; set; }
    }
}

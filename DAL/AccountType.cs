using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class AccountType
    {
        public AccountType()
        {
            Account = new HashSet<Account>();
            News = new HashSet<News>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<Account> Account { get; set; }
        public ICollection<News> News { get; set; }
    }
}

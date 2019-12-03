using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }
    }
}

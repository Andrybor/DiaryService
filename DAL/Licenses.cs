using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Licenses
    {
        public Guid Id { get; set; }
        public bool IsAssigned { get; set; }
    }
}

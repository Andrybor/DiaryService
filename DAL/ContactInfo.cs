﻿using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class ContactInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

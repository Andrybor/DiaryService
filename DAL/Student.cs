using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Student
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public int? Points { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
    }
}

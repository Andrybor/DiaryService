using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Specialization
    {
        public Specialization()
        {
            Course = new HashSet<Course>();
            Group = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Course> Course { get; set; }
        public ICollection<Group> Group { get; set; }
    }
}

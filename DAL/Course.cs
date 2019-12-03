using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Course
    {
        public Course()
        {
            Group = new HashSet<Group>();
            Subject = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? SpecializationId { get; set; }
        public int? AmountOfHours { get; set; }

        public Specialization Specialization { get; set; }
        public ICollection<Group> Group { get; set; }
        public ICollection<Subject> Subject { get; set; }
    }
}

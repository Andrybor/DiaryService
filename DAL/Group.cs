using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Group
    {
        public Group()
        {
            GroupMessage = new HashSet<GroupMessage>();
            Schedule = new HashSet<Schedule>();
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? SpecializationId { get; set; }
        public int? CourseId { get; set; }
        public int? Semester { get; set; }

        public Course Course { get; set; }
        public Specialization Specialization { get; set; }
        public ICollection<GroupMessage> GroupMessage { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}

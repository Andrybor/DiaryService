using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Subject
    {
        public Subject()
        {
            Schedule = new HashSet<Schedule>();
            TeacherSkill = new HashSet<TeacherSkill>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? CourseId { get; set; }

        public Course Course { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<TeacherSkill> TeacherSkill { get; set; }
    }
}

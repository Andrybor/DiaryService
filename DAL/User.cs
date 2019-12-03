using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class User
    {
        public User()
        {
            GroupMessage = new HashSet<GroupMessage>();
            LessonInfo = new HashSet<LessonInfo>();
            Schedule = new HashSet<Schedule>();
            Student = new HashSet<Student>();
            TeacherSkill = new HashSet<TeacherSkill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? AccountId { get; set; }
        public bool? Sex { get; set; }
        public DateTime? CreatingDay { get; set; }
        public byte[] Image { get; set; }

        public Account Account { get; set; }
        public ICollection<GroupMessage> GroupMessage { get; set; }
        public ICollection<LessonInfo> LessonInfo { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<Student> Student { get; set; }
        public ICollection<TeacherSkill> TeacherSkill { get; set; }
    }
}

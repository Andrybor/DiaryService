using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class TeacherSkill
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public Subject Subject { get; set; }
        public User Teacher { get; set; }
    }
}

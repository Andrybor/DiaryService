using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class LessonInfo
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public int? UserId { get; set; }
        public bool IsPresent { get; set; }
        public int? Grade { get; set; }

        public Schedule Schedule { get; set; }
        public User User { get; set; }
    }
}

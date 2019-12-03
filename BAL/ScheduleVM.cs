using System;
using SimpleDataService.DAL;

namespace SimpleDataService.BAL
{
    public class ScheduleVM 
    {
        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public int? GroupId { get; set; }   
        public string Theme { get; set; }
        public int? TeacherId { get; set; }
        public int? AuditoriumId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public AuditoriumVM Auditorium { get; set; }
        public GroupVM Group { get; set; }
        public SubjectVM Subject { get; set; }
        public UserVM Teacher { get; set; }

    }
}

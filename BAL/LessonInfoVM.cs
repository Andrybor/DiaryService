namespace SimpleDataService.BAL
{
    public class LessonInfoVM
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public int? UserId { get; set; }
        public int? Grade { get; set; }
        public bool? IsPresent { get; set; }

        public ScheduleVM Schedule { get; set; }
        public UserVM User { get; set; }
    }
}

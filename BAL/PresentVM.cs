namespace SimpleDataService.BAL
{
    public class PresentVM
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public int? UserId { get; set; }
        public bool? IsPresent { get; set; }
    }
}

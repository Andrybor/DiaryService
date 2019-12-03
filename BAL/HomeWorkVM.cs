using System;

namespace SimpleDataService.BAL
{
    public class HomeWorkVM
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public byte[] Task { get; set; }
        public DateTime Date { get; set; }
        public string Theme { get; set; }
    }
}

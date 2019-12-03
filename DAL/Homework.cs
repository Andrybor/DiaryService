using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Homework
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public byte[] Task { get; set; }

        public Schedule Schedule { get; set; }
    }
}

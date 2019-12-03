using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Auditorium
    {
        public Auditorium()
        {
            Schedule = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Schedule> Schedule { get; set; }
    }
}

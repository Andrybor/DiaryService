using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SimpleDataService.BAL
{
    public class SpecializationVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<CourseVM> Courses { get; set; }
        public ICollection<GroupVM> Groups { get; set; }
    }
}

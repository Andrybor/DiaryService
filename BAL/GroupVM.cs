using System.Collections.Generic;

namespace SimpleDataService.BAL
{
    public class GroupVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SpecializationId { get; set; }
        public int? CourseId { get; set; }
        public int? Semester { get; set; }
        public string Course { get; set; }
        public ICollection<StudentVM> Students { get; set; }
    }
}

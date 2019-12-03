using System.Security.Policy;

namespace SimpleDataService.BAL
{
    public class SubjectVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? CourseId { get; set; }
        public string Course { get; set; }
    }
}

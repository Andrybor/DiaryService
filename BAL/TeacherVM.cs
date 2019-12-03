

namespace SimpleDataService.BAL
{
    public class TeacherVM
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public UserVM User { get; set; }
        public SubjectVM Subject { get; set; }
    }
}

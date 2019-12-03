namespace SimpleDataService.BAL
{
    public class StudentVM
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public int? Points { get; set; }
        public string Group { get; set; }
        public UserVM User { get; set; }
    }
}

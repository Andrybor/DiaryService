namespace SimpleDataService.BAL
{
    public class NewsVM
    {
        public int Id { get; set; }
        public int? AccountTypeId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}

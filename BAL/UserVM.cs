using System;

namespace SimpleDataService.BAL
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? AccountId { get; set; }
        public bool? Sex { get; set; }
        public DateTime? CreatingDay { get; set; }
        public byte[] Image { get; set; }
    }
}

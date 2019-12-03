using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class GroupMessage
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public byte[] Image { get; set; }
        public byte[] File { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SimpleDataService.DAL
{
    public partial class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }
        public byte[] Image { get; set; }
    }
}

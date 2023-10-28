using System;
using System.Collections.Generic;

namespace RBCTest.Models
{
    public partial class File
    {
        public int FileId { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string FileName { get; internal set; }
    }
}

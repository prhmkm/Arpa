using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class TVCover
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string HTMLText { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

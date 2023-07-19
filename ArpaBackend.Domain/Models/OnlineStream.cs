using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class OnlineStream
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public int LanguageId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

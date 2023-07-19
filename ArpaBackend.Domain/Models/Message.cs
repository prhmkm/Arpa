using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsSeen { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}

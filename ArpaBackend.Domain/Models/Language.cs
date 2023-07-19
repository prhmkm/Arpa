using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Language
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Flag { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}

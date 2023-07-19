using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Slider
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string SlideImage { get; set; } = null!;
        public string? URL { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

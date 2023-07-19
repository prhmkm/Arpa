using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string URL { get; set; } = null!;
        public string Poster { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ReleaseDate { get; set; }
        public string? Rating { get; set; }
        public bool? IsPersian { get; set; }
        public int? CategoryId { get; set; }
        public string? GenreId { get; set; }
        public bool? HaveSubtitle { get; set; }
        public string? ManufacturedBy { get; set; }
        public bool? IsTranslated { get; set; }
        public bool? IsMultiLanguage { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

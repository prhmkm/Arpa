
namespace ArpaBackend.Domain.DTOs
{
    public class EventDTO
    {
        public class AddEventRequest
        {

            public string Title { get; set; } = null!;
            public string? URL { get; set; }
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
            public bool? IsPersian { get; set; }
            public int? CategoryId { get; set; }
            public List<int>? GenreId { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public bool? IsTranslated { get; set; }
            public bool? IsMultiLanguage { get; set; }
        }
        public class UpdateEventRequest
        {
            public int Id { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public string? URL { get; set; }
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
            public bool? IsPersian { get; set; }
            public int? CategoryId { get; set; }
            public string? GenreId { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public bool? IsTranslated { get; set; }
            public bool? IsMultiLanguage { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteEventRequest
        {
            public int Id { get; set; }
        }
        public class BOShowEvents
        {
            public int Id { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public string? URL { get; set; }
            public bool? IsPersian { get; set; }
            public string? Category { get; set; }
            public List<string>? GenreName { get; set; }
            public List<int>? GenreId { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public bool? IsTranslated { get; set; }
            public bool? IsMultiLanguage { get; set; }
            public DateTime? CreationDateTime { get; set; }
            public bool? IsActive { get; set; }
        }
        public class BOShowEventsImages
        {
            public int Id { get; set; }
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
        }
        public class ShowEvents
        {
            public int Id { get; set; }
            public string Title { get; set; } = null!;
            public string URL { get; set; } = null!;
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public DateTime? CreationDateTime { get; set; }
            public bool? IsActive { get; set; }
            public bool? IsPersian { get; set; }
            public string? Category { get; set; }
            public List<string>? GenreName { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public bool? IsTranslated { get; set; }
            public bool? IsMultiLanguage { get; set; }
        }
    }
}

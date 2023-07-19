
namespace ArpaBackend.Domain.DTOs
{
    public class MovieDTO
    {
        public class AddMovieRequest
        {
            public string Alias { get; set; }
            public int LanguageId { get; set; }
            public string Title { get; set; } = null!;
            public string? URL { get; set; }
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
            public int CategoryId { get; set; }
            public List<int> GenreId { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public string? Language { get; set; }
            public bool? IsMultiLanguage { get; set; }
        }
        public class UpdateMovieRequest
        {
            public int Id { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public string? URL { get; set; }
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
            public int? CategoryId { get; set; }
            public int[]? GenreId { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public string? Language { get; set; }
            public bool? IsMultiLanguage { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteMovieRequest
        {
            public int Id { get; set; }
        }
        public class BOShowMovies
        {
            public int Id { get; set; }
            public string Alias { get; set; }
            public string LanguageCode { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public string? URL { get; set; }     
            public string? Category { get; set; }
            public int? CategoryId { get; set; }
            public List<string>? GenreName { get; set; }
            public List<int>? GenreId { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public string? Language { get; set; }
            public int LanguageId { get; set; }
            public bool? IsMultiLanguage { get; set; }
            public DateTime? CreationDateTime { get; set; }
            public bool? IsActive { get; set; }
        }
        public class BOShowMoviesImages
        {
            public int Id { get; set; }
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
        }
        public class ShowMovies
        {
            public int Id { get; set; }
            public string LanguageCode { get; set; } = null!;
            public string Alias { get; set; } = null!;
            public string Title { get; set; } = null!;
            public string URL { get; set; } = null!;
            public string Poster { get; set; } = null!;
            public string Cover { get; set; } = null!;
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            public DateTime? CreationDateTime { get; set; }
            public bool? IsActive { get; set; }
            public string? Category { get; set; }
            public List<string>? GenreName { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public string? Language { get; set; }
            public bool? IsMultiLanguage { get; set; }
        }
        public class FilterRequest
        {
            //public int? Id { get; set; }
            public string? Title { get; set; } = null!;
            public string? URL { get; set; } = null!;
            public string? Poster { get; set; } = null!;
            public string? Cover { get; set; } = null!;
            public string? Description { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Rating { get; set; }
            //public DateTime? CreationDateTime { get; set; }
            public bool? IsActive { get; set; }
           
            public string? Category { get; set; }
            public List<string>? GenreName { get; set; }
            public bool? HaveSubtitle { get; set; }
            public string? ManufacturedBy { get; set; }
            public string? Language { get; set; }
            public bool? IsMultiLanguage { get; set; }
        }
        public class MoviesByTitle
        {
            public string? Title { get; set; }
        }
    }
}

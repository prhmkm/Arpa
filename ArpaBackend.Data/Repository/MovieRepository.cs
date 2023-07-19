using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.MovieDTO;

namespace ArpaBackend.Data.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public MovieRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void AddMovie(Movie movie)
        {
            Create(movie);
            Save();
        }

        public List<BOShowMovies> BOGetAllMovies(int id)
        {
            List<BOShowMovies> res = new List<BOShowMovies>();
            if (id != 0)
            {
                var list = _repositoryContext.Movies.Where(w => w.IsDelete == false && w.LanguageId == id).ToList();
                foreach (var r in list)
                {
                    res.Add(new BOShowMovies { Alias = r.Alias, LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId)?.Code, URL = r.URL, IsActive = r.IsActive, CreationDateTime = r.CreationDateTime, Title = r.Title, Description = r.Description, ReleaseDate = r.ReleaseDate, Rating = r.Rating, ManufacturedBy = r.ManufacturedBy, GenreName = r.GenreId.Split(",").ToList(), LanguageId = r.LanguageId, Language = r.Language, IsMultiLanguage = r.IsMultiLanguage, HaveSubtitle = r.HaveSubtitle, Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId)?.Name, CategoryId = r.CategoryId });
                }
                return res;
            }
            else
            {
                var list = _repositoryContext.Movies.Where(w => w.IsDelete == false).ToList();
                foreach (var r in list)
                {
                    res.Add(new BOShowMovies { Alias = r.Alias, LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId)?.Code, URL = r.URL, IsActive = r.IsActive, CreationDateTime = r.CreationDateTime, Title = r.Title, Description = r.Description, ReleaseDate = r.ReleaseDate, Rating = r.Rating, ManufacturedBy = r.ManufacturedBy, GenreName = r.GenreId.Split(",").ToList(), LanguageId = r.LanguageId, Language = r.Language, IsMultiLanguage = r.IsMultiLanguage, HaveSubtitle = r.HaveSubtitle, Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId)?.Name, CategoryId = r.CategoryId });
                }
                return res;
            }
        }
        public List<BOShowMovies> BOGetAllMovies(int pageSize, int pageNumber, FilterRequest filter, int id)
        {
            var response = _repositoryContext.Movies.Where(w => w.IsDelete == false &&
           (id != 0 ? w.LanguageId == id : true) &&
           (string.IsNullOrEmpty(filter.Category) ? true : w.CategoryId == Convert.ToInt32(filter.Category)) &&
           (string.IsNullOrEmpty(filter.Title) ? true : w.Title.ToLower().Contains(filter.Title.ToLower())) &&
           (filter.Language == null ? true : w.Language == filter.Language) &&
           (filter.IsMultiLanguage == null ? true : w.IsMultiLanguage == filter.IsMultiLanguage) &&
           (filter.HaveSubtitle == null ? true : w.HaveSubtitle == filter.HaveSubtitle) &&
           (string.IsNullOrEmpty(filter.ManufacturedBy) ? true : w.ManufacturedBy == filter.ManufacturedBy) &&
           (string.IsNullOrEmpty(filter.Rating) ? true : w.Rating == filter.Rating) &&
           (string.IsNullOrEmpty(filter.ReleaseDate) ? true : w.ReleaseDate == filter.ReleaseDate)
           ).ToList();
            var res = response.Select(s => new BOShowMovies
            {
                Id = s.Id,
                LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId)?.Code,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId)?.Title,
                Alias = s.Alias,
                URL = s.URL,
                IsActive = s.IsActive,
                CreationDateTime = s.CreationDateTime,
                Title = s.Title,
                Description = s.Description,
                ReleaseDate = s.ReleaseDate,
                Rating = s.Rating,
                ManufacturedBy = s.ManufacturedBy,
                GenreName = s.GenreId.Split(',').ToList(),
                Language = s.Language,
                LanguageId = s.LanguageId,
                CategoryId = s.CategoryId,
                IsMultiLanguage = s.IsMultiLanguage,
                HaveSubtitle = s.HaveSubtitle,
                Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == s.CategoryId)?.Name
            }).ToList();
            if (pageSize != 0 && pageNumber != 0)
            {
                return res.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return res.ToList();
            }
        }


        public List<ShowMovies> GetAllMovies(int languageId)
        {
            List<ShowMovies> res = new List<ShowMovies>();
            var list = _repositoryContext.Movies.Where(w => w.IsDelete == false && (languageId != 0 ? w.LanguageId == languageId : true) && w.IsActive == true).ToList();
            foreach (var r in list)
            {
                res.Add(new ShowMovies
                {
                    Id = r.Id,
                    LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId)?.Code,
                    Alias = r.Alias,
                    URL = r.URL,
                    Cover = r.Cover,
                    Poster = r.Poster,
                    IsActive = r.IsActive,
                    CreationDateTime = r.CreationDateTime,
                    Title = r.Title,
                    Description = r.Description,
                    ReleaseDate = r.ReleaseDate,
                    Rating = r.Rating,
                    ManufacturedBy = r.ManufacturedBy,
                    GenreName = r.GenreId?.Split(",").ToList(),
                    Language = r.Language,

                    IsMultiLanguage = r.IsMultiLanguage,
                    HaveSubtitle = r.HaveSubtitle,
                    Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId)?.Name
                });
            }
            return res;
        }
        public List<ShowMovies> GetAllMovies(int pageSize, int pageNumber, FilterRequest filter, int languageId)
        {

            List<ShowMovies> res = new List<ShowMovies>();
            var list = _repositoryContext.Movies.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (languageId != 0 ? w.LanguageId == languageId : true) &&
            (string.IsNullOrEmpty(filter.Category) ? true : w.CategoryId == Convert.ToInt32(filter.Category)) &&
            (string.IsNullOrEmpty(filter.Title) ? true : w.Title.ToLower().Contains(filter.Title.ToLower())) &&
            (filter.Language == null ? true : w.Language == filter.Language) &&
            (filter.IsMultiLanguage == null ? true : w.IsMultiLanguage == filter.IsMultiLanguage) &&
            (filter.HaveSubtitle == null ? true : w.HaveSubtitle == filter.HaveSubtitle) &&
            (string.IsNullOrEmpty(filter.ManufacturedBy) ? true : w.ManufacturedBy == filter.ManufacturedBy) &&
            (string.IsNullOrEmpty(filter.Rating) ? true : w.Rating == filter.Rating) &&
            (string.IsNullOrEmpty(filter.ReleaseDate) ? true : w.ReleaseDate == filter.ReleaseDate)
            ).ToList();
            foreach (var r in list)
            {
                res.Add(new ShowMovies
                {
                    Id = r.Id,
                    LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId)?.Code,
                    Alias = r.Alias,
                    URL = r.URL,
                    Cover = r.Cover,
                    Poster = r.Poster,
                    IsActive = r.IsActive,
                    CreationDateTime = r.CreationDateTime,
                    Title = r.Title,
                    Description = r.Description,
                    ReleaseDate = r.ReleaseDate,
                    Rating = r.Rating,
                    ManufacturedBy = r.ManufacturedBy,
                    GenreName = r.GenreId?.Split(",").ToList(),
                    Language = r.Language,
                    IsMultiLanguage = r.IsMultiLanguage,
                    HaveSubtitle = r.HaveSubtitle,
                    Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId)?.Name
                });
            }
            if (pageSize != 0 && pageNumber != 0)
            {
                return res.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return res.ToList();
            }
        }

        public Movie GetMovie(int Id)
        {
            return FindByCondition(w => w.Id == Id).Where(w => w.IsDelete == false).FirstOrDefault();
        }

        public List<ShowMovies> GetMovieByAlias(string alias, int languageId)
        {
            List<ShowMovies> res = new List<ShowMovies>();
            var list = _repositoryContext.Movies.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (languageId != 0 ? w.LanguageId == languageId : true) &&
            (string.IsNullOrEmpty(alias) ? true : w.Alias == alias)
            ).ToList();
            foreach (var r in list)
            {
                res.Add(new ShowMovies
                {
                    Id = r.Id,
                    LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId)?.Code,
                    Alias = r.Alias,
                    URL = r.URL,
                    Cover = r.Cover,
                    Poster = r.Poster,
                    IsActive = r.IsActive,
                    CreationDateTime = r.CreationDateTime,
                    Title = r.Title,
                    Description = r.Description,
                    ReleaseDate = r.ReleaseDate,
                    Rating = r.Rating,
                    ManufacturedBy = r.ManufacturedBy,
                    GenreName = r.GenreId.Split(",").ToList(),
                    Language = r.Language,
                    IsMultiLanguage = r.IsMultiLanguage,
                    HaveSubtitle = r.HaveSubtitle,
                    Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId)?.Name
                });
            }
            return res;
        }

        public List<BOShowMovies> BOGetMovieByAlias(string alias, int languageId)
        {
            List<BOShowMovies> res = new List<BOShowMovies>();
            foreach (var r in _repositoryContext.Movies.Where(w => w.IsDelete == false &&
            (languageId != 0 ? w.LanguageId == languageId : true) &&
            (string.IsNullOrEmpty(alias) ? true : w.Alias == alias)
            ).ToList())
            {
                res.Add(new BOShowMovies
                {
                    Id = r.Id,
                    LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
                    LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title,
                    Alias = r.Alias,
                    URL = r.URL,
                    IsActive = r.IsActive,
                    CreationDateTime = r.CreationDateTime,
                    Title = r.Title,
                    Description = r.Description,
                    ReleaseDate = r.ReleaseDate,
                    Rating = r.Rating,
                    ManufacturedBy = r.ManufacturedBy,
                    GenreName = r.GenreId.Split(",").ToList(),
                    Language = r.Language,
                    LanguageId = r.LanguageId,
                    CategoryId = r.CategoryId,
                    IsMultiLanguage = r.IsMultiLanguage,
                    HaveSubtitle = r.HaveSubtitle,
                    Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name
                });
            }
            return res;
        }
        public List<Movie> BOGetMovieDLByAlias(string alias, int languageId)
        {
            List<Movie> res = new List<Movie>();
            foreach (var r in _repositoryContext.Movies.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (languageId != 0 ? w.LanguageId == languageId : true) &&
            (string.IsNullOrEmpty(alias) ? true : w.Alias == alias)
            ).ToList())
            {
                res.Add(new Movie
                {
                    //Id = r.Id,
                    //LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
                    //Alias = r.Alias,
                    //URL = r.URL,
                    //IsActive = r.IsActive,
                    //CreationDateTime = r.CreationDateTime,
                    //Title = r.Title,
                    //Description = r.Description,
                    //ReleaseDate = r.ReleaseDate,
                    //Rating = r.Rating,
                    //ManufacturedBy = r.ManufacturedBy,
                    //GenreName = r.GenreId.Split(",").ToList(),
                    //Language = r.Language,
                    //IsMultiLanguage = r.IsMultiLanguage,
                    //HaveSubtitle = r.HaveSubtitle,
                    //Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name
                    IsDelete = r.IsDelete
                });
            }
            return res;
        }

        public BOShowMoviesImages ShowMovieImages(int Id)
        {
            return (from r in _repositoryContext.Movies
                    where r.Id == Id && r.IsDelete == false
                    select new BOShowMoviesImages { Id = r.Id, Poster = r.Poster, Cover = r.Cover }).FirstOrDefault();
        }
        public void UpdateMovie(Movie movie)
        {
            Update(movie);
            Save();
        }

        public List<ShowMovies> GetMovieByCategory(int id)
        {
            List<ShowMovies> res = new List<ShowMovies>();
            foreach (var r in _repositoryContext.Movies.Where(w => w.IsDelete == false && (id != 0 ? w.CategoryId == id : true) && w.IsActive == true).ToList())
            {
                res.Add(new ShowMovies
                {
                    Id = r.Id,
                    LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
                    Alias = r.Alias,
                    URL = r.URL,
                    Cover = r.Cover,
                    Poster = r.Poster,
                    IsActive = r.IsActive,
                    CreationDateTime = r.CreationDateTime,
                    Title = r.Title,
                    Description = r.Description,
                    ReleaseDate = r.ReleaseDate,
                    Rating = r.Rating,
                    ManufacturedBy = r.ManufacturedBy,
                    GenreName = r.GenreId.Split(",").ToList(),
                    Language = r.Language,
                    IsMultiLanguage = r.IsMultiLanguage,
                    HaveSubtitle = r.HaveSubtitle,
                    Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name
                });
            }
            return res;
        }

        public List<ShowMovies> GetMoviesByTitle(string title, int id)
        {
            List<ShowMovies> res = new List<ShowMovies>();
            foreach (var r in _repositoryContext.Movies.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (id != 0 ? w.LanguageId == id : true) &&
            (string.IsNullOrEmpty(title) ? true : w.Title.ToLower().Contains(title.ToLower()))
            ).ToList())
            {
                res.Add(new ShowMovies
                {
                    Id = r.Id,
                    LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
                    Alias = r.Alias,
                    URL = r.URL,
                    Cover = r.Cover,
                    Poster = r.Poster,
                    IsActive = r.IsActive,
                    CreationDateTime = r.CreationDateTime,
                    Title = r.Title,
                    Description = r.Description,
                    ReleaseDate = r.ReleaseDate,
                    Rating = r.Rating,
                    ManufacturedBy = r.ManufacturedBy,
                    GenreName = r.GenreId.Split(",").ToList(),
                    Language = r.Language,
                    IsMultiLanguage = r.IsMultiLanguage,
                    HaveSubtitle = r.HaveSubtitle,
                    Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name
                });
            }
            return res.ToList();
        }
    }
}

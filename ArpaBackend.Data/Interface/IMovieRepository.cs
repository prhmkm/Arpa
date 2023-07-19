using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.MovieDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IMovieRepository
    {
        void AddMovie(Movie movie);
        List<ShowMovies> GetAllMovies(int languageId);
        List<ShowMovies> GetAllMovies(int pagesize, int pageNumber, FilterRequest filter, int languageId);
        List<BOShowMovies> BOGetAllMovies(int id);
        List<BOShowMovies> BOGetAllMovies(int pagesize,int pageNumber,FilterRequest filter, int id);
        void UpdateMovie(Movie movie);
        List<ShowMovies> GetMovieByAlias(string alias, int languageId);
        List<BOShowMovies> BOGetMovieByAlias(string alias, int languageId);
        List<Movie> BOGetMovieDLByAlias(string alias, int languageId);
        Movie GetMovie(int Id);
        BOShowMoviesImages ShowMovieImages(int Id);
        List<ShowMovies> GetMovieByCategory(int id);
        List<ShowMovies> GetMoviesByTitle(string title, int id);

    }
}

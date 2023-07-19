using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ArpaBackend.Domain.DTOs.MovieDTO;

namespace ArpaBackend.Service.Local.Service
{
    public class MovieService : IMovieService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public MovieService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void AddMovie(Movie movie)
        {
            _repository.Movie.AddMovie(movie);
        }

        public List<ShowMovies> GetAllMovies(int languageId)
        {
            return _repository.Movie.GetAllMovies(languageId);
        }

        public List<BOShowMovies> BOGetAllMovies(int id)
        {
            return _repository.Movie.BOGetAllMovies(id);
        }

        public void UpdateMovie(Movie movie)
        {
            _repository.Movie.UpdateMovie(movie);
        }

        public List<ShowMovies> GetMovieByAlias(string alias, int languageId)
        {
            return _repository.Movie.GetMovieByAlias(alias, languageId);
        }

        public BOShowMoviesImages ShowMovieImages(int Id)
        {
            return _repository.Movie.ShowMovieImages(Id);
        }

        public Movie GetMovie(int Id)
        {
            return _repository.Movie.GetMovie(Id);
        }

        public List<ShowMovies> GetAllMovies(int pagesize, int pageNumber, FilterRequest filter, int languageId)
        {
            return _repository.Movie.GetAllMovies(pagesize, pageNumber, filter, languageId);
        }

        public List<BOShowMovies> BOGetAllMovies(int pagesize, int pageNumber, FilterRequest filter, int id)
        {
            return _repository.Movie.BOGetAllMovies(pagesize, pageNumber, filter, id);
        }

        public List<BOShowMovies> BOGetMovieByAlias(string alias, int languageId)
        {
            return _repository.Movie.BOGetMovieByAlias(alias, languageId);
        }

        public List<Movie> BOGetMovieDLByAlias(string alias, int languageId)
        {
            return _repository.Movie.BOGetMovieDLByAlias(alias, languageId);
        }

        public List<ShowMovies> GetMovieByCategory(int id)
        {
            return _repository.Movie.GetMovieByCategory(id);
        }

        public List<ShowMovies> GetMoviesByTitle(string title, int id)
        {
            return _repository.Movie.GetMoviesByTitle(title, id);
        }
    }
}

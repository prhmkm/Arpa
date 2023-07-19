using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class GenreService : IGenreService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public GenreService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public string GetNameById(int id)
        {
            return _repository.Genre.GetNameById(id);
        }

        public List<Genre> GetGenres(int id)
        {
            return _repository.Genre.GetGenres(id);
        }

        public List<Genre> GetAll()
        {
            return _repository.Genre.GetAll();
        }
    }
}

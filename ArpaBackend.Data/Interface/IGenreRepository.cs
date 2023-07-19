using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Interface
{
    public interface IGenreRepository
    {
        List<Genre> GetGenres(int id);
        List<Genre> GetAll();
        string GetNameById(int id);
    }
}

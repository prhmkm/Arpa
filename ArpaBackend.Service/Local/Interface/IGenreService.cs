using ArpaBackend.Domain.Models;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IGenreService
    {
        List<Genre> GetAll();
        List<Genre> GetGenres(int id);
        string GetNameById(int id);
    }
}

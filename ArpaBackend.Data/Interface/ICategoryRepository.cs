
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories(int id);
    }
}

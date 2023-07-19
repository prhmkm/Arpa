

using ArpaBackend.Domain.Models;

namespace ArpaBackend.Service.Local.Interface
{
    public interface ICategoryService
    {
        List<Category> GetCategories(int id);
    }
}

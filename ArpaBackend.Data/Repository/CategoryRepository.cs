using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public CategoryRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public List<Category> GetCategories(int id)
        {
            if (id != 0)
            {
                return FindByCondition(w => w.IsDeleted == false && w.LanguageId == id).ToList();
            }
            else
            {
                return FindByCondition(w => w.IsDeleted == false).ToList();
            }
        }
    }
}

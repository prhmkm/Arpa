using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public GenreRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public List<Genre> GetGenres(int id)
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

        public List<Genre> GetAll()
        {
            return FindAll().ToList();
        }

        public string GetNameById(int id)
        {
            return FindByCondition(w => w.Id == id && w.IsDeleted == false).Select(w => w.Name)?.FirstOrDefault();
        }
    }
}

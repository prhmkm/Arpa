using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Repository
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public LanguageRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public List<Language> GetLanguages()
        {
            return FindByCondition(w=>w.IsActive == true).ToList();
        }

        public Language GetById(int id)
        {
            return FindByCondition(w => w.Id == id).FirstOrDefault();
        }

        public Language GetByCode(string code)
        {
            return FindByCondition(w => w.Code == code).FirstOrDefault();
        }
    }
}

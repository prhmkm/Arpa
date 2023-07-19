using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.RuleDTO;

namespace ArpaBackend.Data.Repository
{
    public class RuleRepository : BaseRepository<Rule>, IRuleRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public RuleRepository(ArpaWebsite_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(Rule rule)
        {
            Create(rule);
            Save();
        }

        public List<BOShowRules> BOGetAll(int langId)
        {
            return _repositoryContext.Rules.Where(s =>
            s.IsDelete == false &&
            (langId != 0 ? s.LanguageId == langId : true)).Select(s => new BOShowRules
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTile = _repositoryContext.Languages.FirstOrDefault(o => o.Id == s.LanguageId).Title,
                RuleTitle = s.RuleTitle,
                RuleAnswer = s.RuleAnswer,
                IsActive = s.IsActive.GetValueOrDefault()
            }).ToList();
        }

        public void Edit(Rule rule)
        {
            Update(rule);
            Save();
        }

        public List<ShowRules> GetAll(int langId)
        {
            return _repositoryContext.Rules.Where(s =>
            s.IsDelete == false &&
            s.IsActive == true &&
            (langId != 0 ? s.LanguageId == langId : true)).Select(s => new ShowRules
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTile = _repositoryContext.Languages.FirstOrDefault(o => o.Id == s.LanguageId).Title,
                RuleTitle = s.RuleTitle,
                RuleAnswer = s.RuleAnswer,
            }).ToList();
        }

        public Rule GetById(int id)
        {
            return _repositoryContext.Rules.Where(s => s.Id == id && s.IsDelete == false).FirstOrDefault();
        }
    }
}

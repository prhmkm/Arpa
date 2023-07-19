using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.QuestionDTO;

namespace ArpaBackend.Data.Repository
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public QuestionRepository(ArpaWebsite_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(Question question)
        {
            Create(question);
            Save();
        }

        public List<BOShowQuestions> BOGetAll(int langId)
        {
            return _repositoryContext.Questions.Where(s =>
            s.IsDelete == false &&
            (langId != 0 ? s.LanguageId == langId : true)).Select(s => new BOShowQuestions
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTile = _repositoryContext.Languages.FirstOrDefault(o => o.Id == s.LanguageId).Title,
                QuestionTitle = s.QuestionTitle,
                QuestionAnswer = s.QuestionAnswer,
                IsActive = s.IsActive.GetValueOrDefault()
            }).ToList();
        }

        public void Edit(Question question)
        {
            Update(question);
            Save();
        }

        public List<ShowQuestions> GetAll(int langId)
        {
            return _repositoryContext.Questions.Where(s =>
            s.IsDelete == false &&
            s.IsActive == true &&
            (langId != 0 ? s.LanguageId == langId : true)).Select(s => new ShowQuestions
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTile = _repositoryContext.Languages.FirstOrDefault(o => o.Id == s.LanguageId).Title,
                QuestionTitle = s.QuestionTitle,
                QuestionAnswer = s.QuestionAnswer,
            }).ToList();
        }

        public Question GetById(int id)
        {
            return _repositoryContext.Questions.Where(s => s.Id == id && s.IsDelete == false).FirstOrDefault();
        }
    }
}

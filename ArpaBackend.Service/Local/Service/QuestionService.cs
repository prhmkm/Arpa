using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ArpaBackend.Domain.DTOs.QuestionDTO;

namespace ArpaBackend.Service.Local.Service
{
    public class QuestionService : IQuestionService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public QuestionService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Question question)
        {
            _repository.Question.Add(question);
        }

        public void Edit(Question question)
        {
            _repository.Question.Edit(question);
        }

        public Question GetById(int id)
        {
            return _repository.Question.GetById(id);
        }

        public List<ShowQuestions> GetAll(int langId)
        {
            return _repository.Question.GetAll(langId);
        }

        public List<BOShowQuestions> BOGetAll(int langId)
        {
            return _repository.Question.BOGetAll(langId);
        }
    }
}

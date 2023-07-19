using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.QuestionDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IQuestionService
    {
        void Add(Question question);
        void Edit(Question question);
        Question GetById(int id);
        List<ShowQuestions> GetAll(int langId);
        List<BOShowQuestions> BOGetAll(int langId);
    }
}

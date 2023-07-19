using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.QuestionDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IQuestionRepository
    {
        void Add(Question question);
        void Edit(Question question);
        Question GetById(int id);
        List<ShowQuestions> GetAll(int langId);
        List<BOShowQuestions> BOGetAll(int langId);

    }
}

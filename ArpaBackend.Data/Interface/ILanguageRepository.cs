using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Interface
{
    public interface ILanguageRepository
    {
        List<Language> GetLanguages();
        Language GetById(int id);
        Language GetByCode(string code);
    }
}

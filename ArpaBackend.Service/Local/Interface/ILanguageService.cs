using ArpaBackend.Domain.Models;

namespace ArpaBackend.Service.Local.Interface
{
    public interface ILanguageService
    {
        List<Language> GetLanguages();
        Language GetById(int id);
        Language GetByCode(string code);
    }
}

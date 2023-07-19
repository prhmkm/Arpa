using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class LanguageService : ILanguageService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public LanguageService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }
        public Language GetById(int id)
        {
            return _repository.Language.GetById(id);
        }

        public Language GetByCode(string code)
        {
            return _repository.Language.GetByCode(code);
        }

        public List<Language> GetLanguages()
        {
            return _repository.Language.GetLanguages();
        }
    }
}

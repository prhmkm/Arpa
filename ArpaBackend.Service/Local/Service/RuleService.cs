using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ArpaBackend.Domain.DTOs.RuleDTO;

namespace ArpaBackend.Service.Local.Service
{
    public class RuleService : IRuleService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public RuleService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Rule rule)
        {
            _repository.Rule.Add(rule);
        }

        public void Edit(Rule rule)
        {
            _repository.Rule.Edit(rule);
        }

        public Rule GetById(int id)
        {
            return _repository.Rule.GetById(id);
        }

        public List<ShowRules> GetAll(int langId)
        {
            return _repository.Rule.GetAll(langId);
        }

        public List<BOShowRules> BOGetAll(int langId)
        {
            return _repository.Rule.BOGetAll(langId);
        }
    }
}

using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.RuleDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IRuleRepository
    {
        void Add(Rule rule);
        void Edit(Rule rule);
        Rule GetById(int id);
        List<ShowRules> GetAll(int langId);
        List<BOShowRules> BOGetAll(int langId);
    }
}

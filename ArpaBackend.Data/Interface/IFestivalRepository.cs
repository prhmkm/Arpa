using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.FestivalDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IFestivalRepository
    {
        void Add(Festival festival);
        void Edit(Festival festival);
        Festival GetById(int id);
        List<BOShowFestivals> BOGetAll(int pagesize, int pageNumber, int langId);
        List<BOShowFestivals> BOGetAll(int langId);
        BOShowFestivals BOGetById(int langId, int festivalId);
    }
}

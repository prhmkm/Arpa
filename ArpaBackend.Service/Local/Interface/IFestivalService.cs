using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.FestivalDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IFestivalService
    {
        void Add(Festival festival);
        void Edit(Festival festival);
        Festival GetById(int id);
        List<BOShowFestivals> BOGetAll(int pagesize, int pageNumber, int langId);
        List<BOShowFestivals> BOGetAll(int langId);
        BOShowFestivals BOGetById(int langId,int festivalId);
    }
}

using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.TVCoverDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface ITVCoverService
    {
        void Add(TVCover tVCover);
        void Edit(TVCover tVCover);
        TVCover GetById(int id);
        List<ShowAllTVCovers> GetAll(int langId);
        List<BOShowAllTVCovers> BOGetAll(int langId);
        List<BOShowAllTVCovers> BOGetAll(int pagesize, int pageNumber, int languageId);
        ShowAllTVCovers GetActiveCover(int languageId);
        bool ExistActiveCover(int languageId);
    }
}

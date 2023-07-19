using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.TVCoverDTO;

namespace ArpaBackend.Data.Interface
{
    public interface ITVCoverRepository
    {
        void Add(TVCover tVCover);
        void Edit(TVCover tVCover);
        TVCover GetById(int id);
        List<ShowAllTVCovers> GetAll(int langId);
        List<BOShowAllTVCovers> BOGetAll(int langId);
        List<BOShowAllTVCovers> BOGetAll(int pageSize, int pageNumber, int languageId2);
        ShowAllTVCovers GetActiveCover(int languageId);
        bool ExistActiveCover(int languageId);
    }
}

using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.FestivalVideoDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IFestivalVideoService
    {
        void Add(FestivalVideo festivalVideo);
        void Edit(FestivalVideo festivalVideo);
        FestivalVideo GetById(int id);
        List<BOShowFestivalVideos> BOGetAll(int langId);
        List<BOShowFestivalVideos> BOGetAll(int languageId, int festivalId);
    }
}

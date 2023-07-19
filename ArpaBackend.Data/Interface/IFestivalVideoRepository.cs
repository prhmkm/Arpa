using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.FestivalVideoDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IFestivalVideoRepository
    {
        void Add(FestivalVideo festivalVideo);
        void Edit(FestivalVideo festivalVideo);
        FestivalVideo GetById(int id);
        List<BOShowFestivalVideos> BOGetAll(int langId);
        BOShowFestivalVideos BOGetById(int langId,int festivalVideoId);
        List<BOShowFestivalVideos> BOGetAll(int langId, int festivalId);
    }
}

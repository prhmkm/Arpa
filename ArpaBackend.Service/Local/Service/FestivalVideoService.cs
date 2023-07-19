using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class FestivalVideoService : IFestivalVideoService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public FestivalVideoService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(FestivalVideo festivalVideo)
        {
            _repository.FestivalVideo.Add(festivalVideo);
        }

        public void Edit(FestivalVideo festivalVideo)
        {
            _repository.FestivalVideo.Edit(festivalVideo);
        }

        public FestivalVideo GetById(int id)
        {
            return _repository.FestivalVideo.GetById(id);
        }

        public List<FestivalVideoDTO.BOShowFestivalVideos> BOGetAll(int langId)
        {
            return _repository.FestivalVideo.BOGetAll(langId);
        }

        public List<FestivalVideoDTO.BOShowFestivalVideos> BOGetAll(int langId, int festivalId)
        {
            return _repository.FestivalVideo.BOGetAll(langId,festivalId);
        }

        public FestivalVideoDTO.BOShowFestivalVideos BOGetById(int langId, int festivalVideoId)
        {
            return _repository.FestivalVideo.BOGetById(langId, festivalVideoId);
        }
    }
}

using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class TVCoverService : ITVCoverService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public TVCoverService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(TVCover tVCover)
        {
            _repository.TVCover.Add(tVCover);
        }

        public void Edit(TVCover tVCover)
        {
            _repository.TVCover.Edit(tVCover);
        }

        public TVCover GetById(int id)
        {
            return _repository.TVCover.GetById(id);
        }

        public List<TVCoverDTO.ShowAllTVCovers> GetAll(int langId)
        {
            return _repository.TVCover.GetAll(langId);
        }

        public List<TVCoverDTO.BOShowAllTVCovers> BOGetAll(int langId)
        {
            return _repository.TVCover.BOGetAll(langId);
        }

        public List<TVCoverDTO.BOShowAllTVCovers> BOGetAll(int pagesize, int pageNumber, int languageId)
        {
            return _repository.TVCover.BOGetAll(pagesize, pageNumber, languageId);
        }

        public TVCoverDTO.ShowAllTVCovers GetActiveCover(int languageId)
        {
            return _repository.TVCover.GetActiveCover(languageId);
        }

        public bool ExistActiveCover(int languageId)
        {
            return _repository.TVCover.ExistActiveCover(languageId);
        }
    }
}

using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class FestivalService : IFestivalService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public FestivalService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Festival festival)
        {
            _repository.Festival.Add(festival);
        }

        public void Edit(Festival festival)
        {
            _repository.Festival.Edit(festival);
        }

        public Festival GetById(int id)
        {
            return _repository.Festival.GetById(id);
        }

        public List<FestivalDTO.BOShowFestivals> BOGetAll(int pagesize, int pageNumber, int langId)
        {
            return _repository.Festival.BOGetAll(pagesize, pageNumber, langId);
        }

        public List<FestivalDTO.BOShowFestivals> BOGetAll(int langId)
        {
            return _repository.Festival.BOGetAll(langId);
        }

        public FestivalDTO.BOShowFestivals BOGetById(int langId,int festivalId)
        {
            return _repository.Festival.BOGetById(langId, festivalId);
        }
    }
}

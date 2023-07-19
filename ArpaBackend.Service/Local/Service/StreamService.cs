using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class StreamService : IStreamService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public StreamService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public List<OnlineStream> GetAllStreams()
        {
            return _repository.Stream.GetAllStreams();
        }

        public List<OnlineStream> BOGetAllStreams()
        {
            return _repository.Stream.BOGetAllStreams();
        }

        public void AddStream(OnlineStream stream)
        {
            _repository.Stream.AddStream(stream);
        }

        public void UpdateStream(OnlineStream stream)
        {
            _repository.Stream.UpdateStream(stream);
        }

        public OnlineStream GetStreamById(int Id)
        {
            return _repository.Stream.GetStreamById(Id);
        }
    }
}

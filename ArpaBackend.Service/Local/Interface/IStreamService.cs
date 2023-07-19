using ArpaBackend.Domain.Models;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IStreamService
    {
        List<OnlineStream> GetAllStreams();
        List<OnlineStream> BOGetAllStreams();
        void AddStream(OnlineStream stream);
        void UpdateStream(OnlineStream stream);
        OnlineStream GetStreamById(int Id);
    }
}

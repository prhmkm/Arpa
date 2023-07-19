using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Interface
{
    public interface IStreamRepository
    {
        List<OnlineStream> GetAllStreams();
        List<OnlineStream> BOGetAllStreams();
        void AddStream(OnlineStream stream);
        void UpdateStream(OnlineStream stream);
        OnlineStream GetStreamById(int Id);
    }
}

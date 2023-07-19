using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.MessageDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IMessageRepository
    {
        List<GetAllMessagesResponse> GetMessages(bool? isSeen, int langId);
        void AddMessage(Message message);
        void EditMessage(Message message);
        GetAllMessagesResponse GetMessageById(int id, int langId);
    }
}

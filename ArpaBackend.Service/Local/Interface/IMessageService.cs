using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.MessageDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IMessageService
    {
        List<GetAllMessagesResponse> GetMessages(bool? isSeen, int langId);
        void AddMessage(Message message);
        void EditMessage(Message message);
        GetAllMessagesResponse GetMessageById(int id, int langId);
    }
}

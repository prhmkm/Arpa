using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ArpaBackend.Domain.DTOs.MessageDTO;

namespace ArpaBackend.Service.Local.Service
{
    public class MessageService : IMessageService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public MessageService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void AddMessage(Message message)
        {
            _repository.Message.AddMessage(message);
        }

        public List<GetAllMessagesResponse> GetMessages(bool? isSeen, int langId)
        {
            return _repository.Message.GetMessages(isSeen, langId);
        }

        public void EditMessage(Message message)
        {
            _repository.Message.EditMessage(message);
        }

        public GetAllMessagesResponse GetMessageById(int id, int langId)
        {
            return _repository.Message.GetMessageById(id,langId);
        }
    }
}

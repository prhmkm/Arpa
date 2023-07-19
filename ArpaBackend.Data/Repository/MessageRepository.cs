using ArpaBackend.Core.Helpers;
using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.MessageDTO;

namespace ArpaBackend.Data.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public MessageRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void AddMessage(Message message)
        {
            Create(message);
            Save();
        }

        public void EditMessage(Message message)
        {
            Update(message);
            Save();
        }

        public GetAllMessagesResponse GetMessageById(int id, int langId)
        {
            var res = _repositoryContext.Messages.Where(s => s.Id == id &&
            (langId != 0 ? s.LanguageId == id : true)).Select(o => new GetAllMessagesResponse
            {
                Description = o.Description,
                LanguageId = o.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == o.LanguageId).Title,
                Email = o.Email,
                FullName = o.FullName,
                Id = o.Id,
                Mobile = o.Mobile,
                Title = o.Title,
                IsSeen = o.IsSeen,
                Date = DateHelpers.ToPersianDate(o.CreationDateTime, false, "/"),
                Time = o.CreationDateTime.ToString("H:mm")
            }).FirstOrDefault();

            var a = _repositoryContext.Messages.Where(s =>
            (langId != 0 ? s.LanguageId == id : true) &&
            s.Id == id).FirstOrDefault();
            a.IsSeen = true;

            EditMessage(a);
            return res;
        }

        public List<GetAllMessagesResponse> GetMessages(bool? isSeen, int langId)
        {
            return _repositoryContext.Messages.Where(s =>
            (isSeen != null ? s.IsSeen == isSeen : true) &&
            (langId != 0 ? s.LanguageId == langId : true))
                .Select(s => new GetAllMessagesResponse
                {
                    Description = s.Description,
                    Email = s.Email,
                    LanguageId = s.LanguageId,
                    LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                    FullName = s.FullName,
                    Id = s.Id,
                    Mobile = s.Mobile,
                    Title = s.Title,
                    IsSeen = s.IsSeen,
                    Date = DateHelpers.ToPersianDate(s.CreationDateTime, false, "/"),
                    Time = s.CreationDateTime.ToString("H:mm")
                }).ToList().OrderBy(s=>s.Date).ThenBy(s=>s.Time).ToList();
        }
    }
}

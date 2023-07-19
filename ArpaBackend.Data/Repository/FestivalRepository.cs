using ArpaBackend.Core.Helpers;
using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using System.Drawing.Printing;
using static ArpaBackend.Domain.DTOs.FestivalDTO;

namespace ArpaBackend.Data.Repository
{
    public class FestivalRepository : BaseRepository<Festival>, IFestivalRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public FestivalRepository(ArpaWebsite_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(Festival festival)
        {
            Create(festival);
            Save();
        }

        public List<BOShowFestivals> BOGetAll(int pageSize, int pageNumber, int langId)
        {
            return _repositoryContext.Festivals.Where(w => (w.IsDelete == false) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new BOShowFestivals
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                CoverURL = s.CoverURL,
                IsActive = s.IsActive.GetValueOrDefault(),
                Description = s.Description,
                Title = s.Title,
                VideoLength = s.VideoLength,
                VideoSize = s.VideoSize,
                CreationDateTime = DateHelpers.ToPersianDate(s.CreationDateTime,true,"/")
            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<BOShowFestivals> BOGetAll(int langId)
        {
            return _repositoryContext.Festivals.Where(w => (w.IsDelete == false) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new BOShowFestivals
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                CoverURL = s.CoverURL,
                IsActive = s.IsActive.GetValueOrDefault(),
                Description = s.Description,
                Title = s.Title,
                VideoLength = s.VideoLength,
                VideoSize = s.VideoSize,
                CreationDateTime = DateHelpers.ToPersianDate(s.CreationDateTime,true,"/")
            }).ToList();
        }

        public BOShowFestivals BOGetById(int langId, int festivalId)
        {
            return _repositoryContext.Festivals.Where(w => (w.IsDelete == false) && (w.Id == festivalId) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new BOShowFestivals
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                CoverURL = s.CoverURL,
                IsActive = s.IsActive.GetValueOrDefault(),
                Description = s.Description,
                Title = s.Title,
                VideoLength = s.VideoLength,
                VideoSize = s.VideoSize,
                CreationDateTime = DateHelpers.ToPersianDate(s.CreationDateTime, true, "/")
            }).FirstOrDefault();
        }

        public void Edit(Festival festival)
        {
            Update(festival);
            Save();
        }

        public Festival GetById(int id)
        {
            return FindByCondition(s => s.Id == id).FirstOrDefault();
        }
    }
}

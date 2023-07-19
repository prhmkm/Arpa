using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.FestivalVideoDTO;

namespace ArpaBackend.Data.Repository
{
    public class FestivalVideoRepository : BaseRepository<FestivalVideo>, IFestivalVideoRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public FestivalVideoRepository(ArpaWebsite_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(FestivalVideo festivalVideo)
        {
            Create(festivalVideo);
            Save();
        }

        public List<BOShowFestivalVideos> BOGetAll(int langId)
        {
            return _repositoryContext.FestivalVideos.Where(w => (w.IsDelete == false) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new BOShowFestivalVideos
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                FirstName = s.FirstName,
                LastName = s.LastName,
                NationalCode = s.NationalCode,
                ProvinceId = s.ProvinceId,
                ProvinceTitle = _repositoryContext.ProvinceCodes.FirstOrDefault(w => w.Id == s.ProvinceId).Province,
                CityId = s.CityId,
                CityTitle = _repositoryContext.Cities.FirstOrDefault(w => w.CityCode == s.CityId).CityName,
                VideoURL = s.VideoURL
            }).ToList();
        }

        public List<BOShowFestivalVideos> BOGetAll(int langId, int festivalId)
        {
            return _repositoryContext.FestivalVideos.Where(w => (w.IsDelete == false) && (w.FestivalId == festivalId) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new BOShowFestivalVideos
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                FirstName = s.FirstName,
                LastName = s.LastName,
                NationalCode = s.NationalCode,
                ProvinceId = s.ProvinceId,
                ProvinceTitle = _repositoryContext.ProvinceCodes.FirstOrDefault(w => w.Id == s.ProvinceId).Province,
                CityId = s.CityId,
                CityTitle = _repositoryContext.Cities.FirstOrDefault(w => w.CityCode == s.CityId).CityName,
                VideoURL = s.VideoURL
            }).ToList();
        }

        public BOShowFestivalVideos BOGetById(int langId, int festivalVideoId)
        {
            return _repositoryContext.FestivalVideos.Where(w => (w.IsDelete == false) && (w.Id == festivalVideoId) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new BOShowFestivalVideos
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                FirstName = s.FirstName,
                LastName = s.LastName,
                NationalCode = s.NationalCode,
                ProvinceId = s.ProvinceId,
                ProvinceTitle = _repositoryContext.ProvinceCodes.FirstOrDefault(w => w.Id == s.ProvinceId).Province,
                CityId = s.CityId,
                CityTitle = _repositoryContext.Cities.FirstOrDefault(w => w.CityCode == s.CityId).CityName,
                VideoURL = s.VideoURL
            }).FirstOrDefault();
        }

        public void Edit(FestivalVideo festivalVideo)
        {
            Update(festivalVideo);
            Save();
        }

        public FestivalVideo GetById(int id)
        {
            return FindByCondition(s => s.Id == id).FirstOrDefault();
        }
    }
}

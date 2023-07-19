using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.SliderDTO;

namespace ArpaBackend.Data.Repository
{
    public class SliderRepository : BaseRepository<Slider> , ISliderRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public SliderRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void AddSlider(Slider slider)
        {
            Create(slider);
            Save();
        }

        public List<ShowSliders> BOGetAllSliders(int languageId)
        {
            return (from r in _repositoryContext.Sliders
                    where r.IsDelete == false &&
                    (languageId != 0 ? r.LanguageId == languageId : true)
                    select new ShowSliders { Id = r.Id , IsDefault = r.IsDefault, LanguageId = r.LanguageId , LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code , LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title ,URL = r.URL, IsActive = r.IsActive , CreationDateTime = r.CreationDateTime }).ToList();
        }

        public List<Slider> GetAllDefaultSliders()
        {
            return FindByCondition(w=>w.IsActive == true && w.IsDelete == false && w.IsDefault == true).ToList();
        }

        //public List<ShowSliders> BOGetSliderByAlias(string alias, int languageId)
        //{
        //    List<ShowSliders> res = new List<ShowSliders>();
        //    foreach (var r in _repositoryContext.Sliders.Where(w => w.IsDelete == false &&
        //    (languageId != 0 ? w.LanguageId == languageId : true)

        //    ).ToList())
        //    {
        //        res.Add(new ShowSliders
        //        {
        //            Id = r.Id,
        //            LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
        //            LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title,
        //            IsDefault = r.IsDefault ,
        //            LanguageId = r.LanguageId,
        //            URL = r.URL,
        //            IsActive = r.IsActive,
        //            CreationDateTime = r.CreationDateTime,
        //        });
        //    }
        //    return res;
        //}

        public List<Slider> GetAllSliders(int languageId)
        {
            return FindAll().Where(o => o.IsDelete == false && o.IsActive == true && (languageId != 0 ? o.LanguageId == languageId : true)).ToList();
        }

        //public List<Slider> GetSliderByAlias(string alias, int languageId)
        //{
        //    List<Slider> res = new List<Slider>();
        //    foreach (var r in _repositoryContext.Sliders.Where(w => w.IsDelete == false &&
        //    w.IsActive == true &&
        //    (languageId != 0 ? w.LanguageId == languageId : true)
        //    ).ToList())
        //    {
        //        res.Add(new Slider
        //        {
        //            Id = r.Id,
        //            LanguageId = r.LanguageId,
        //            SlideImage = r.SlideImage,
        //            URL = r.URL,
        //            IsDefault = r.IsDefault,
        //            IsActive = r.IsActive,
        //            CreationDateTime = r.CreationDateTime,
        //        });
        //    }
        //    return res;
        //}

        public Slider GetSliderById(int Id)
        {
            return FindByCondition(w => w.Id == Id).FirstOrDefault();
        }

        public string ShowSlideImage(int Id)
        {
            return (from r in _repositoryContext.Sliders
                    where r.Id == Id && r.IsDelete == false
                    select r.SlideImage).FirstOrDefault();
        }

        public void UpdateSlider(Slider slider)
        {
            Update(slider);
            Save();
        }
    }
}

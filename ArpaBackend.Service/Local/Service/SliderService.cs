using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ArpaBackend.Domain.DTOs.SliderDTO;

namespace ArpaBackend.Service.Local.Service
{
    public class SliderService : ISliderService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public SliderService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void AddSlider(Slider slider)
        {
            _repository.Slider.AddSlider(slider);
        }

        public List<Slider> GetAllSliders(int languageId)
        {
            return _repository.Slider.GetAllSliders(languageId);
        }

        public List<ShowSliders> BOGetAllSliders(int languageId)
        {
            return _repository.Slider.BOGetAllSliders(languageId);
        }

        public void UpdateSlider(Slider slider)
        {
            _repository.Slider.UpdateSlider(slider);
        }

        public Slider GetSliderById(int Id)
        {
            return _repository.Slider.GetSliderById(Id);
        }

        public string ShowSlideImage(int Id)
        {
            return _repository.Slider.ShowSlideImage(Id);
        }

        public List<Slider> GetAllDefaultSliders()
        {
            return _repository.Slider.GetAllDefaultSliders();
        }

        //public List<Slider> GetSliderByAlias(string alias, int languageId)
        //{
        //    return _repository.Slider.GetSliderByAlias(alias, languageId);
        //}

        //public List<ShowSliders> BOGetSliderByAlias(string alias, int languageId)
        //{
        //    return _repository.Slider.BOGetSliderByAlias(alias, languageId);
        //}
    }
}

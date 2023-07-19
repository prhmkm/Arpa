using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.SliderDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface ISliderService
    {
        void AddSlider(Slider slider);
        List<Slider> GetAllSliders(int languageId);
        List<ShowSliders> BOGetAllSliders(int languageId);
        void UpdateSlider(Slider slider);
        Slider GetSliderById(int Id);
        string ShowSlideImage(int Id);
        List<Slider> GetAllDefaultSliders();

        //List<Slider> GetSliderByAlias(string alias, int languageId);
        //List<ShowSliders> BOGetSliderByAlias(string alias, int languageId);
        //List<Slider> BOGetSliderDLByAlias(string alias, int languageId);
    }
}

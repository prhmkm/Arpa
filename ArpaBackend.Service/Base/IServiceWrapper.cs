using ArpaBackend.Service.Local.Interface;
using ArpaBackend.Service.Remote.Interfaces;

namespace ArpaBackend.Service.Base
{
    public interface IServiceWrapper
    {
        ILanguageService Language { get; }
        IUserService User { get; }
        ISliderService Slider { get; }
        IMovieService Movie { get; }
        ICategoryService Category { get; }
        IGenreService Genre { get; }
        IMessageService Message { get; }
        IStreamService Stream { get; }
        IEventService Event { get; }
        ICustomerService Customer { get; }
        IPictureService Picture { get; }
        IMagfaService MagfaService { get; }
        IIpconverterService IpconverterService { get; }
        IPageDetailService PageDetail { get; }
        IQuestionService Question { get; }
        IRuleService Rule { get; }
        IFestivalService Festival { get; }
        IFestivalVideoService FestivalVideo { get; }
        ITVCoverService TVCover { get; }

        void Save();
    }
}

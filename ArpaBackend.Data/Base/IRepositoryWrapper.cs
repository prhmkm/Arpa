using ArpaBackend.Data.Interface;

namespace ArpaBackend.Data.Base
{
    public interface IRepositoryWrapper
    {
        ILanguageRepository Language { get; }
        IUserRepository User { get; }
        ISliderRepository Slider { get; }
        IMovieRepository Movie { get; }
        ICategoryRepository Category { get; }
        IPictureRepository Picture { get; }
        IGenreRepository Genre { get; }
        IMessageRepository Message { get; }
        IStreamRepository Stream { get; }
        IEventRepository Event { get; }
        ICustomerRepository Customer { get; }
        IIpconverterRepository IpConverter { get; }
        IPageDetailRepository PageDetail { get; } 
        IQuestionRepository Question { get; }
        IRuleRepository Rule { get; }
        IFestivalRepository Festival { get; }
        IFestivalVideoRepository FestivalVideo { get; }
        ITVCoverRepository TVCover { get; }
        void Save();
    }
}

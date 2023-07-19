using Microsoft.Extensions.Options;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Service.Local.Interface;
using ArpaBackend.Service.Local.Service;
using ArpaBackend.Service.Remote.Interfaces;
using ArpaBackend.Service.Remote.Service;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Service.Base
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IOptions<AppSettings> _appSettings;
        private IRepositoryWrapper _repository;
        private ArpaWebsite_DBContext _Context;
        public ServiceWrapper(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            _repository = repository;
        }        
        public ILanguageService Language => new LanguageService(_repository,_appSettings);
        public IUserService User => new UserService(_repository,_appSettings);

        public ISliderService Slider => new SliderService(_repository, _appSettings);

        public IMovieService Movie => new MovieService(_repository, _appSettings);

        public ICategoryService Category => new CategoryService(_repository, _appSettings);

        public IGenreService Genre => new GenreService(_repository, _appSettings);

        public IMessageService Message => new MessageService(_repository, _appSettings);

        public IStreamService Stream => new StreamService(_repository, _appSettings);

        public IEventService Event => new EventService(_repository, _appSettings);

        public ICustomerService Customer => new CustomerService(_repository, _appSettings);
        public IPictureService Picture => new PictureService(_repository, _appSettings);
        
        public IMagfaService MagfaService => new MagfaService(_repository, _appSettings);

        public IIpconverterService IpconverterService => new IpconverterService(_appSettings);

        public IPageDetailService PageDetail => new PageDetailService(_repository, _appSettings);

        public IQuestionService Question => new QuestionService(_repository, _appSettings);

        public IRuleService Rule => new RuleService(_repository, _appSettings);

        public IFestivalService Festival => new FestivalService(_repository, _appSettings);
        public IFestivalVideoService FestivalVideo => new FestivalVideoService(_repository, _appSettings);

        public ITVCoverService TVCover => new TVCoverService(_repository, _appSettings);

        public void Save()
        {
            _repository.Save();
        }
    }
}

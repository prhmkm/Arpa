using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Data.Repository;
using ArpaBackend.Domain.Models;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Data.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ArpaWebsite_DBContext _repoContext;

        public RepositoryWrapper(ArpaWebsite_DBContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public ILanguageRepository Language => new LanguageRepository(_repoContext);

        public IUserRepository User => new UserRepository(_repoContext);

        public ISliderRepository Slider => new SliderRepository(_repoContext);

        public IMovieRepository Movie => new MovieRepository(_repoContext);

        public ICategoryRepository Category => new CategoryRepository(_repoContext);

        public IPictureRepository Picture => new PictureRepository(_repoContext);

        public IGenreRepository Genre => new GenreRepository(_repoContext);

        public IMessageRepository Message => new MessageRepository(_repoContext);

        public IStreamRepository Stream => new StreamRepository(_repoContext);

        public IEventRepository Event => new EventRepository(_repoContext);

        public ICustomerRepository Customer => new CustomerRepository(_repoContext);

        public IIpconverterRepository IpConverter => new IpconverterRepository(_repoContext);

        public IPageDetailRepository PageDetail => new PageDetailRepository(_repoContext);

        public IQuestionRepository Question => new QuestionRepository(_repoContext);

        public IRuleRepository Rule => new RuleRepository(_repoContext);

        public IFestivalRepository Festival => new FestivalRepository(_repoContext);
        public IFestivalVideoRepository FestivalVideo => new FestivalVideoRepository(_repoContext);

        public ITVCoverRepository TVCover => new TVCoverRepository(_repoContext);

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}

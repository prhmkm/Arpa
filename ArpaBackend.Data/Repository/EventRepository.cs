using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.EventDTO;

namespace ArpaBackend.Data.Repository
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public EventRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void AddEvent(Event _event)
        {
            Create(_event);
            Save();
        }

        public List<BOShowEvents> BOGetAllEvents()
        {
            List<BOShowEvents> res = new List<BOShowEvents>();
            var list = _repositoryContext.Events.Where(w => w.IsDelete == false).ToList();
            foreach (var r in list)
            {
                res.Add(new BOShowEvents { Id = r.Id, URL = r.URL, IsActive = r.IsActive, CreationDateTime = r.CreationDateTime, Title = r.Title, Description = r.Description, ReleaseDate = r.ReleaseDate, Rating = r.Rating, IsPersian = r.IsPersian, ManufacturedBy = r.ManufacturedBy, GenreName = r.GenreId.Split(",").ToList(), IsTranslated = r.IsTranslated, IsMultiLanguage = r.IsMultiLanguage, HaveSubtitle = r.HaveSubtitle, Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name });
            }
            return res;
        }

        public List<BOShowEvents> BOGetAllEvents(int pagesize)
        {
            List<BOShowEvents> res = new List<BOShowEvents>();
            foreach (var r in _repositoryContext.Events.Where(w => w.IsDelete == false).ToList())
            {
                res.Add(new BOShowEvents { Id = r.Id, URL = r.URL, IsActive = r.IsActive, CreationDateTime = r.CreationDateTime, Title = r.Title, Description = r.Description, ReleaseDate = r.ReleaseDate, Rating = r.Rating, IsPersian = r.IsPersian, ManufacturedBy = r.ManufacturedBy, GenreName = r.GenreId.Split(",").ToList(), IsTranslated = r.IsTranslated, IsMultiLanguage = r.IsMultiLanguage, HaveSubtitle = r.HaveSubtitle, Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name });
            }
            return res.Take(pagesize).ToList();
        }

        public List<ShowEvents> GetAllEvents()
        {
            List<ShowEvents> res = new List<ShowEvents>();
            foreach (var r in _repositoryContext.Events.Where(w => w.IsDelete == false && w.IsActive == true).ToList())
            {
                res.Add(new ShowEvents { Id = r.Id, URL = r.URL, Cover = r.Cover, Poster = r.Poster, IsActive = r.IsActive, CreationDateTime = r.CreationDateTime, Title = r.Title, Description = r.Description, ReleaseDate = r.ReleaseDate, Rating = r.Rating, IsPersian = r.IsPersian, ManufacturedBy = r.ManufacturedBy, GenreName = r.GenreId.Split(",").ToList(), IsTranslated = r.IsTranslated, IsMultiLanguage = r.IsMultiLanguage, HaveSubtitle = r.HaveSubtitle, Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name });
            }
            return res;
        }

        public List<ShowEvents> GetAllEvents(int pagesize)
        {
            List<ShowEvents> res = new List<ShowEvents>();
            foreach (var r in _repositoryContext.Events.Where(w => w.IsDelete == false && w.IsActive == true).ToList())
            {
                res.Add(new ShowEvents { Id = r.Id, URL = r.URL, Cover = r.Cover, Poster = r.Poster, IsActive = r.IsActive, CreationDateTime = r.CreationDateTime, Title = r.Title, Description = r.Description, ReleaseDate = r.ReleaseDate, Rating = r.Rating, IsPersian = r.IsPersian, ManufacturedBy = r.ManufacturedBy, GenreName = r.GenreId.Split(",").ToList(), IsTranslated = r.IsTranslated, IsMultiLanguage = r.IsMultiLanguage, HaveSubtitle = r.HaveSubtitle, Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name });
            }
            return res.Take(pagesize).ToList();
        }

        public Event GetEvent(int Id)
        {
            return FindByCondition(w => w.Id == Id).Where(w => w.IsDelete == false).FirstOrDefault();
        }

        public ShowEvents GetEventById(int Id)
        {
            ShowEvents res = new ShowEvents();
            foreach (var r in _repositoryContext.Events.Where(w => w.Id == Id && w.IsDelete == false).ToList())
            {
                res = new ShowEvents { Id = r.Id, URL = r.URL, Cover = r.Cover, Poster = r.Poster, IsActive = r.IsActive, CreationDateTime = r.CreationDateTime, Title = r.Title, Description = r.Description, ReleaseDate = r.ReleaseDate, Rating = r.Rating, IsPersian = r.IsPersian, ManufacturedBy = r.ManufacturedBy, GenreName = r.GenreId.Split(",").ToList(), IsTranslated = r.IsTranslated, IsMultiLanguage = r.IsMultiLanguage, HaveSubtitle = r.HaveSubtitle, Category = _repositoryContext.Categories.FirstOrDefault(w => w.Id == r.CategoryId).Name };
            }
            return res;
        }

        public BOShowEventsImages ShowEventImages(int Id)
        {
            return (from r in _repositoryContext.Events
                    where r.Id == Id && r.IsDelete == false
                    select new BOShowEventsImages { Id = r.Id, Poster = r.Poster, Cover = r.Cover }).FirstOrDefault();
        }

        public void UpdateEvent(Event _event)
        {
            Update(_event);
            Save();
        }
    }
}

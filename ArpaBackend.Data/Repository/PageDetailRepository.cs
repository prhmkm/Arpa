using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ArpaBackend.Domain.DTOs.PageDetailDTO;

namespace ArpaBackend.Data.Repository
{
    public class PageDetailRepository : BaseRepository<PageDetail>, IPageDetailRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public PageDetailRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public bool Add(PageDetail pageDetail)
        {
            Create(pageDetail);
            Save();
            return true;
        }

        public List<BOShowPageDetails> BOGetAll(int langId, int pageId)
        {
            return (from a in _repositoryContext.PageDetails
                    join b in _repositoryContext.Pages on a.PageId equals b.Id
                    where (langId != 0 ? a.LanguageId == langId : true) && a.IsDelete == false && (pageId != 0 ? b.Id == pageId : true)
                    select new BOShowPageDetails
                    {
                        Id = a.Id,
                        LanguageId = a.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(l => l.Id == a.LanguageId).Title,
                        PageId = a.PageId,
                        PageName = b.PageName,
                        PageTitle = a.PageTitle,
                        HTML = a.HTML,
                        PageURL = b.PageURL,
                        IsActive = a.IsActive.GetValueOrDefault()
                    }).ToList();
        }

        public void Edit(PageDetail pageDetail)
        {
            Update(pageDetail);
            Save();
        }

        public bool ExistActivePageDetail(int pageId, int languageId)
        {
            return _repositoryContext.PageDetails.Any(s => s.PageId == pageId && s.IsActive == true && s.IsDelete == false && s.LanguageId == languageId);
        }

        public List<ShowPageDetails> GetAll(int langId, string pageURL)
        {
            return (from a in _repositoryContext.PageDetails
                    join b in _repositoryContext.Pages on a.PageId equals b.Id
                    where (langId != 0 ? a.LanguageId == langId : true) && a.IsActive == true && a.IsDelete == false && (pageURL != "" ? b.PageURL == pageURL : true)
                    select new ShowPageDetails
                    {
                        Id = a.Id,
                        LanguageId = a.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(l => l.Id == a.LanguageId).Title,
                        PageId = a.PageId,
                        PageTitle = a.PageTitle,
                        PageName = b.PageName,
                        HTML = a.HTML,
                        PageURL = b.PageURL
                    }).ToList();
        }

        public List<Page> GetAllPages()
        {
            return _repositoryContext.Pages.Where(s => s.IsDelete == false).ToList();
        }

        public PageDetail GetById(int id)
        {
            return _repositoryContext.PageDetails.Where(s => s.Id == id && s.IsDelete == false).FirstOrDefault();
        }

        public Page GetPageByURL(string url)
        {
            return _repositoryContext.Pages.Where(s => s.PageURL == url && s.IsDelete == false && s.IsActive == true).FirstOrDefault();
        }
    }
}

using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Data.Repository
{
    public class TVCoverRepository : BaseRepository<TVCover>, ITVCoverRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public TVCoverRepository(ArpaWebsite_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Add(TVCover tVCover)
        {
            var x = _repositoryContext.TVCovers.Where(w => w.IsDelete == false && w.IsActive == true && w.LanguageId == tVCover.LanguageId).FirstOrDefault();
            if (x != null)
            {
                x.IsActive = false;
                Update(x);
                Create(tVCover);
                Save();
            }
            if (x == null)
            {
                Create(tVCover);
                Save();
            }
        }

        public List<TVCoverDTO.BOShowAllTVCovers> BOGetAll(int langId)
        {
            return _repositoryContext.TVCovers.Where(w => (w.IsDelete == false) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new TVCoverDTO.BOShowAllTVCovers
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                HTMLText = s.HTMLText,
                Title = s.Title,
                IsActive = s.IsActive.GetValueOrDefault()
            }).ToList();
        }

        public List<TVCoverDTO.BOShowAllTVCovers> BOGetAll(int pageSize, int pageNumber, int languageId)
        {
            return _repositoryContext.TVCovers.Where(w => (w.IsDelete == false) &&
            (languageId != 0 ? w.LanguageId == languageId : true)).Select(s => new TVCoverDTO.BOShowAllTVCovers
            {
                Id = s.Id,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                HTMLText = s.HTMLText,
                Title = s.Title,
                IsActive = s.IsActive.GetValueOrDefault()
            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Edit(TVCover tVCover)
        {
            Update(tVCover);
            Save();
        }


        public TVCoverDTO.ShowAllTVCovers GetActiveCover(int languageId)
        {
            var s = _repositoryContext.TVCovers.Where(w => w.LanguageId == languageId && w.IsActive == true && w.IsDelete == false).OrderByDescending(o => o.Id).LastOrDefault();
            return new TVCoverDTO.ShowAllTVCovers { Title = s.Title, HTMLText = s.HTMLText, LanguageId = s.LanguageId, Id = s.Id, LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title };
        }


        public bool ExistActiveCover(int languageId)
        {
            return _repositoryContext.TVCovers.Any(w => w.LanguageId == languageId && w.IsDelete == false && w.IsActive == true);
        }

        public List<TVCoverDTO.ShowAllTVCovers> GetAll(int langId)
        {
            return _repositoryContext.TVCovers.Where(w => (w.IsDelete == false) &&
            (w.IsActive == true) &&
            (langId != 0 ? w.LanguageId == langId : true)).Select(s => new TVCoverDTO.ShowAllTVCovers
            {
                Id = s.Id,
                Title = s.Title,
                LanguageId = s.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                HTMLText = s.HTMLText,
            }).ToList();
        }

        public TVCover GetById(int id)
        {
            return _repositoryContext.TVCovers.FirstOrDefault(w => w.Id == id && w.IsDelete == false);
        }
    }
}

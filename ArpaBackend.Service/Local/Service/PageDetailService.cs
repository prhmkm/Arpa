using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ArpaBackend.Domain.DTOs.PageDetailDTO;

namespace ArpaBackend.Service.Local.Service
{
    public class PageDetailService : IPageDetailService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public PageDetailService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public bool Add(PageDetail pageDetail)
        {
            return _repository.PageDetail.Add(pageDetail);
        }

        public void Edit(PageDetail pageDetail)
        {
            _repository.PageDetail.Edit(pageDetail);
        }

        public List<ShowPageDetails> GetAll(int langId, string pageURL)
        {
            return _repository.PageDetail.GetAll(langId, pageURL);
        }

        public List<BOShowPageDetails> BOGetAll(int langId, int pageId)
        {
            return _repository.PageDetail.BOGetAll(langId, pageId);
        }

        public PageDetail GetById(int id)
        {
            return _repository.PageDetail.GetById(id);
        }

        public Page GetPageByURL(string url)
        {
            return _repository.PageDetail.GetPageByURL(url);
        }

        public List<Page> GetAllPages()
        {
            return _repository.PageDetail.GetAllPages();
        }

        public bool ExistActivePageDetail(int pageId,int languageId)
        {
            return _repository.PageDetail.ExistActivePageDetail(pageId, languageId);
        }
    }
}

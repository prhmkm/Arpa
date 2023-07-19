using ArpaBackend.Domain.Models;
using System.Drawing.Printing;
using static ArpaBackend.Domain.DTOs.PageDetailDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IPageDetailRepository
    {
        bool Add(PageDetail pageDetail);
        void Edit(PageDetail pageDetail);
        PageDetail GetById(int id);
        Page GetPageByURL(string url);
        List<Page> GetAllPages();
        List<ShowPageDetails> GetAll(int langId, string pageURL);
        List<BOShowPageDetails> BOGetAll(int langId, int pageId);
        bool ExistActivePageDetail(int pageId, int languageId);
    }
}

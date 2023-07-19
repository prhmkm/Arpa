using ArpaBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ArpaBackend.Domain.DTOs.PageDetailDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IPageDetailService
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

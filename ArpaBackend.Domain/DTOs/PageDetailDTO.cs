using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class PageDetailDTO
    {
        public class ShowPageDetails
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string PageName { get; set; }
            public int PageId { get; set; }
            public string PageTitle { get; set; }
            public string HTML { get; set; }
            public string PageURL { get; set; }
            public string Title { get; set; }
        }
        public class BOShowPageDetails
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string PageName { get; set; }
            public int PageId { get; set; }
            public string PageTitle { get; set; }
            public string HTML { get; set; }
            public string PageURL { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddPageDetail
        {
            public int LanguageId { get; set; }
            public int PageId { get; set; }
            public string PageTitle { get; set; }
            public string HTML { get; set; }
            public bool IsActive { get; set; }
        }
        public class EditPageDetail
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public int PageId { get; set; }
            public string PageTitle { get; set; }
            public string HTML { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeletePageDetail
        {
            public int Id { get; set; }
        }
    }
}

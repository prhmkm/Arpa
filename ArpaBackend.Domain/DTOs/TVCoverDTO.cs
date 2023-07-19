using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class TVCoverDTO
    {
        public class ShowAllTVCovers
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string HTMLText { get; set; }
        }
        public class BOShowAllTVCovers
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string LanguageTitle { get; set; }
            public string HTMLText { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddTVCoverRequest
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string HTMLText { get; set; }
            public bool IsActive { get; set; }
        }
        public class EditTVCoverRequest
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int? LanguageId { get; set; }
            public string? HTMLText { get; set; }
            public bool? IsActive { get; set; }
        }
    }
}

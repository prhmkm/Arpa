using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class PageDetail
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int PageId { get; set; }
        public string PageTitle { get; set; } = null!;
        public string HTML { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

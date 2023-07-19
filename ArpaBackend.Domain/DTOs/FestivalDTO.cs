using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class FestivalDTO
    {
        public class BOShowFestivals
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string CoverURL { get; set; }
            public string CreationDateTime { get; set; }
            public int VideoSize { get; set; }
            public int VideoLength { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddFestival
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Cover { get; set; }          
            public string CoverTitle { get; set; }          
            public int VideoSize { get; set; }
            public int VideoLength { get; set; }
            public bool IsActive { get; set; }
        }
        public class EditFestival
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Cover { get; set; }
            public string CoverTitle { get; set; }
            public int VideoSize { get; set; }
            public int VideoLength { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeleteFestival
        {
            public int Id { get; set; }
        }
    }
}

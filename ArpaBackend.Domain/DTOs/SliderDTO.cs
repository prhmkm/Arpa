using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class SliderDTO
    {
        public class AddSliderRequest
        {
            public int LanguageId { get; set; }
            public string SlideImage { get; set; } = null!;
            public string? URL { get; set; }
            public bool? IsDefault { get; set; }
        }
        public class UpdateSliderRequest
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string? SlideImage { get; set; }
            public string? URL { get; set; }
            public bool? IsDefault { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteSliderRequest
        {
            public int Id { get; set; }
        }
        public class ShowSliders
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string? URL { get; set; }
            public string LanguageCode { get; set; }
            public string LanguageTitle { get; set; }
            public bool? IsDefault { get; set; }
            public DateTime? CreationDateTime { get; set; }
            public bool? IsActive { get; set; }
        }
    }
}

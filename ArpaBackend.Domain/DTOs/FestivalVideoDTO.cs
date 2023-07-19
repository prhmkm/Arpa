using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class FestivalVideoDTO
    {
        public class BOShowFestivalVideos
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NationalCode { get; set; }
            public int ProvinceId { get; set; }
            public string ProvinceTitle { get; set; }
            public int CityId { get; set; }
            public string CityTitle { get; set; }
            public string VideoURL { get; set; }

        }
        public class AddFestivalVideo
        {
            public string LanguageCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NationalCode { get; set; }
            public int ProvinceId { get; set; }
            public int CityId { get; set; }
            public string Video { get; set; }
            public string VideoTitle { get; set; }
            public int FestivalId { get; set; }
            public int UserId { get; set; }
        }

        public class DeleteFestivalVideo
        {
            public int Id { get; set; }
        }
    }
}

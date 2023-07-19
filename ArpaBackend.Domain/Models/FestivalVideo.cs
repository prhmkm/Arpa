using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class FestivalVideo
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public string VideoURL { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool IsDelete { get; set; }
        public int FestivalId { get; set; }
        public int UserId { get; set; }

        public virtual Festival Festival { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

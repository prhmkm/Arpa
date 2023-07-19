using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public int LanguageId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

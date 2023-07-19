using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class City
    {
        public long CityCode { get; set; }
        public string CityName { get; set; } = null!;
        public int ProvinceId { get; set; }

        public virtual ProvinceCode Province { get; set; } = null!;
    }
}

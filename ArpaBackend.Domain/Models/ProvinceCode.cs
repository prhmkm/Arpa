using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class ProvinceCode
    {
        public int Id { get; set; }
        public string Province { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}

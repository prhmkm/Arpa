using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class GetCountryByIPDTO
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string IP { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
    }
}

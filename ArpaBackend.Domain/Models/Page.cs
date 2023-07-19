using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Page
    {
        public int Id { get; set; }
        public string PageName { get; set; } = null!;
        public string PageURL { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

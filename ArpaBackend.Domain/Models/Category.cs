using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int LanguageId { get; set; }
    }
}

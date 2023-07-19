using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Rule
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string RuleTitle { get; set; } = null!;
        public string RuleAnswer { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

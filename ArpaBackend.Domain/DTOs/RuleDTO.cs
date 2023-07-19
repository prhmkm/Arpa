using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class RuleDTO
    {
        public class ShowRules
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTile { get; set; }
            public string RuleTitle { get; set; }
            public string RuleAnswer { get; set; }
        }
        public class BOShowRules
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTile { get; set; }
            public string RuleTitle { get; set; }
            public string RuleAnswer { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddRule
        {
            public int LanguageId { get; set; }
            public string RuleTitle { get; set; }
            public string RuleAnswer { get; set; }
        }
        public class EditRule
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? RuleTitle { get; set; }
            public string? RuleAnswer { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteRule
        {
            public int Id { get; set; }
        }
    }
}

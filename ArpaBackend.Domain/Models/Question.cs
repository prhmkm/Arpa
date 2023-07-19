using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string QuestionTitle { get; set; } = null!;
        public string QuestionAnswer { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

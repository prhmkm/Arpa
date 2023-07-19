using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class QuestionDTO
    {
        public class ShowQuestions
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTile { get; set; }
            public string QuestionTitle { get; set; }
            public string QuestionAnswer { get; set; }
        }
        public class BOShowQuestions
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTile { get; set; }
            public string QuestionTitle { get; set; }
            public string QuestionAnswer { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddQuestion
        {
            public int LanguageId { get; set; }
            public string QuestionTitle { get; set; }
            public string QuestionAnswer { get; set; }
        }
        public class EditQuestion
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? QuestionTitle { get; set; }
            public string? QuestionAnswer { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteQuestion
        {
            public int Id { get; set; }
        }
    }
}

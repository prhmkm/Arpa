using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class MessageDTO
    {
        public class AddMessageRequest
        {
            public string LanguageCode { get; set; }
            public string FullName { get; set; }
            public string? Mobile { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string? Description { get; set; }
        }
        public class GetAllMessagesResponse
        {
            public int Id { get; set; }
            public string LanguageTitle { get; set; }
            public int LanguageId { get; set; }
            public string FullName { get; set; } = null!;
            public string Mobile { get; set; } = null!;
            public string? Email { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public bool IsSeen { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
        }
    }
}

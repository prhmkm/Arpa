using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string Mobile { get; set; } = null!;
        public string VerificationCode { get; set; } = null!;
        public bool Verify { get; set; }
        public int LanguageId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string? RefreshToken { get; set; }
        public bool RememberMe { get; set; }
    }
}

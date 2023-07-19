using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class CustomerDTO
    {
        public class CLoginRequest
        {
            public string Mobile { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }
        public class CLoginResponse
        {
           
            public string DisplayName { get; set; }
            public string Mobile { get; set; }
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public string Language { get; set; }
            public DateTime CreationDateTime { get; set; }
        }
        public class CRegisterRequest
        {
            public string Mobile { get; set; }
            public string Password { get; set; }
            public string Code { get; set; }
            public string FullName { get; set; }
            public string Language { get; set; }
        }
        public class SendSMSRequest
        {
            public string Mobile { get; set; }
        }
        public class CRefreshTokenRequest
        {
            public string Mobile { get; set; }
            public string RefreshToken { get; set; }
        }
        public class PasswprdRecovey
        {
            public string Mobile { get; set; }
            public string NewPassword { get; set; }
            public string Code { get; set; }
        }
    }
}

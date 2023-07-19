using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArpaBackend.Service.Local.Service
{
    public class CustomerService : ICustomerService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public CustomerService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public Customer GetCustomerLogin(string username, string password)
        {
            return _repository.Customer.GetCustomerLogin(username.FixText(), password);
        }

        public void AddCustomer(Customer customer)
        {
            _repository.Customer.AddCustomer(customer);
        }
        public Token GenToken(Customer customer)
        {
            return new Token(GenerateToken(customer));
        }
        private string GenerateToken(Customer customer, int? tokenValidateInMinutes = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.Mobile),
                    new Claim(ClaimTypes.Role, "Customer"),
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidateInMinutes ?? _appSettings.TokenValidateInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void EditCustomer(Customer customer)
        {
            _repository.Customer.EditCustomer(customer);
        }

        public Customer GetCustomerByMobile(string mobile)
        {
            return _repository.Customer.GetCustomerByMobile(mobile);    
        }
    }
}

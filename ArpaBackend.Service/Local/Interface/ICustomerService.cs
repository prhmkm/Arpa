using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Service.Local.Interface
{
    public interface ICustomerService
    {
        Customer GetCustomerLogin(string username, string password);
        void AddCustomer(Customer customer);
        Token GenToken(Customer customer);
        void EditCustomer(Customer customer);
        Customer GetCustomerByMobile(string mobile);
    }
}

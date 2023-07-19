using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Interface
{
    public interface ICustomerRepository
    {
        Customer GetCustomerLogin(string username, string password);
        void AddCustomer(Customer customer);
        void EditCustomer(Customer customer);
        Customer GetCustomerByMobile(string mobile);

    }
}

using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public CustomerRepository(ArpaWebsite_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void AddCustomer(Customer customer)
        {
            Create(customer);
            Save();
        }

        public void EditCustomer(Customer customer)
        {
            Update(customer);
            Save();
        }

        public Customer GetCustomerByMobile(string mobile)
        {
            return FindByCondition(w => w.Mobile == mobile && w.IsDelete == false).FirstOrDefault();
        }

        public Customer GetCustomerLogin(string username, string password)
        {
            return FindByCondition(w => w.Mobile == username && w.Password == password && w.IsDelete == false).FirstOrDefault();
        }
    }
}

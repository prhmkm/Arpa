using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        void EditUser(User user);
        bool ExistUserByRoleId(int RoleId);
        Tuple<bool, bool> CheckUserNameAndEmailExist(string email, string username);
        int AddUser(User user);
        User LoginUser(string username, string password);
        Token GenToken(User user);
        List<User> GetAllUsersExceptCurrent(int userId);
        User GetUserByUsername(string username);
    }
}

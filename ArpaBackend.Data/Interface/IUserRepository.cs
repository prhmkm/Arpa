using ArpaBackend.Domain.Models;


namespace ArpaBackend.Data.Interface
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        void EditUser(User user);
        bool ExistUserByRoleId(int RoleId);
        void AddUser(User user);
        List<User> GetAllUsersExceptCurrent(int userId);
        bool IsExistEmail(string email);
        bool IsExistUserName(string username);
        User GetUserLogin(string username, string password);
        User GetUserByUsername(string username);
    }
}

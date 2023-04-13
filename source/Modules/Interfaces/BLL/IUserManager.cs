using Modules.Entities;
using Modules.Enums;

namespace Modules.Interfaces.BLL
{
    public interface IUserManager
    {
        IEnumerable<User> GetAllUsers();
        User GetUserBy(Guid id);
        User GetUserBy(string email);

        void AddUser(Guid id, string name, string email, string password, AccountType accountType);
        void UpdateUser(Guid id, string name, string email, string password, AccountType accountType);
        bool DeleteUser(User user);
    }
}
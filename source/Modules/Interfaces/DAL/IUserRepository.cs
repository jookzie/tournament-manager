using Modules.Entities;

namespace Modules.Interfaces.DAL
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetPlayersByTournament(Guid id);
        Dictionary<Guid, List<User>> GetAllRegisteredPlayers();
        User GetUserBy(Guid id);
        User GetUserBy(string email);
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);

    }
}

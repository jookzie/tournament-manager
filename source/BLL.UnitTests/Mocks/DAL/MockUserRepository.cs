using Modules.Entities;
using Modules.Interfaces.DAL;
using System;
using System.Collections.Generic;

namespace BLL.UnitTests.Mocks.DAL
{
    internal class MockUserRepository : IUserRepository
    {
        private List<User> _cache = new();
        public User? GetUserBy(Guid id)
        {
            return _cache.Find(user => user.ID == id);
        }
        public User? GetUserBy(string email)
        {
            return _cache.Find(user => user.Email == email);
        }

        public Dictionary<Guid, List<User>> GetAllRegisteredPlayers()
        {
            return null;
        }

        public bool AddUser(User user)
        {
            _cache.Add(user);
            return true;
        }

        public bool UpdateUser(User user)
        {
            return true;
        }

        public bool DeleteUser(User user)
        {
            return _cache.Remove(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return null;
        }

        public IEnumerable<User> GetPlayersByTournament(Guid id)
        {
            return null;
        }
    }
}

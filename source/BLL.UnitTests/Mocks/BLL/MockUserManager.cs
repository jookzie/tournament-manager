using Modules.Entities;
using Modules.Enums;
using Modules.Interfaces.BLL;
using System;
using System.Collections.Generic;

namespace BLL.UnitTests.Mocks.BLL
{
    internal class MockUserManager : IUserManager
    {
        private List<User> _cache = new();
        public void AddUser(Guid id, string name, string email, string password, AccountType accountType)
        {
            _cache.Add(new User
            {
                ID = id,
                Name = name,
                Email = email,
                HashedPassword = password,
                AccountType = accountType
            });
        }


        public IEnumerable<User> GetAllUsers()
        {
            return _cache;
        }

        public User GetUserBy(Guid id)
        {
            return _cache.Find(u => u.ID == id);
        }

        public User GetUserBy(string email)
        {
            return _cache.Find(u => u.Email == email);
        }

        public void UpdateUser(Guid id, string name, string email, string password, AccountType accountType)
        {
        }

        public bool DeleteUser(User user)
        {
            return _cache.Remove(user);
        }
    }
}

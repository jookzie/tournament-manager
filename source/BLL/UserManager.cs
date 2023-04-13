using Modules.Entities;
using Modules.Enums;
using Modules.Interfaces.BLL;
using Modules.Interfaces.DAL;
using Modules.Interfaces.Utilities;
using System.Data;

namespace BLL
{
    public class UserManager : IUserManager
    {
        public UserManager(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            if (users is not null)
                _cache = users.ToDictionary(x => x.ID, x => x);
            return _cache.Values;
        }

        public User? GetUserBy(Guid id)
        {
            if (_cache.TryGetValue(id, out var user)) 
                return user;
            user = _userRepository.GetUserBy(id);
            if (user is not null) 
                _cache.Add(id, user);
            return user;
        }
        public User? GetUserBy(string email)
        {
            var user = _cache.Values.FirstOrDefault(user => user.Email == email);
            if (user is not null) 
                return user;
            user = _userRepository.GetUserBy(email);
            if (user is not null) 
                _cache.Add(user.ID, user);
            return user;
        }
        public void AddUser(Guid id, string name, string email, string password, AccountType accountType)
        {
            var user = new User
            {
                ID = id,
                Name = name,
                Email = email,
                HashedPassword = _passwordHasher.Hash(ValidatePassword(password)),
                AccountType = accountType
            };
            // Prevent duplication
            if (GetUserBy(email) is not null || !_userRepository.AddUser(user))
                throw new DuplicateNameException($"User already exists with the same email '{email}'.");
            _cache.Add(user.ID, user);
        }
        public void UpdateUser(Guid id, string name, string email, string password, AccountType accountType)
        {
            var existingUser = GetUserBy(id);
            if (existingUser is null)
                throw new ArgumentException($"No such user '{name}' exists to update.");
            var user = new User
            {
                ID = id,
                Name = name,
                Email = email,
                HashedPassword = string.IsNullOrEmpty(password) ? _cache[id].HashedPassword
                        : _passwordHasher.Hash(ValidatePassword(password)),
                AccountType = accountType
            };
            // Prevent having no admin accounts
            if (existingUser.AccountType == AccountType.Admin &&
                user.AccountType == AccountType.User &&
                _cache.Count(u => u.Value.AccountType == AccountType.Admin) == 1)
                throw new InvalidOperationException("Cannot change the last administrator account type to user.");

            // Prevent duplication
            var userWithSameEmail = GetUserBy(email);
            if(userWithSameEmail is not null && userWithSameEmail.ID != id && userWithSameEmail.Email == email)
                throw new DuplicateNameException($"User already exists with the same email '{email}'.");
            //if (!_userRepository.UpdateUser(user))
            //    throw new DuplicateNameException($"User already exists with the same email '{email}'.");
            _userRepository.UpdateUser(user);
            _cache[user.ID] = user;
        }
        public bool DeleteUser(User user)
        {
            // Prevent having no admin accounts
            if (user.AccountType == AccountType.Admin &&
                _cache.Count(pair => pair.Value.AccountType == AccountType.Admin) == 1)
                throw new InvalidOperationException("Cannot delete last administrator account. Consider setting another account to admin privileges before deleting this account.");
            _userRepository.DeleteUser(user);
            return _cache.Remove(user.ID);
        }

        #region Password constraint values
        private const int
          PASSWORD_LENGTH_MAX = 64
        , PASSWORD_LENGTH_MIN = 8
        ;
        #endregion     
        private static string ValidatePassword(string plainPassword)
        {
            if (plainPassword.Length < PASSWORD_LENGTH_MIN)
                throw new ArgumentOutOfRangeException("Password",
                    $"Password cannot be shorter than {PASSWORD_LENGTH_MIN}.");
            if (plainPassword.Length > PASSWORD_LENGTH_MAX)
                throw new ArgumentOutOfRangeException("Password",
                    $"Password cannot be longer than {PASSWORD_LENGTH_MAX}.");
            return plainPassword;
        }

        private Dictionary<Guid, User> _cache = new();
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
    }
}

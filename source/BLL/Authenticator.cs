using Modules.Interfaces.DAL;
using Modules.Interfaces.Utilities;

namespace BLL
{
    public class Authenticator
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public Authenticator(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public bool Authenticate(string email, string password)
        {
            var user = _userRepository.GetUserBy(email);
            if (user is null) return false;
            return _passwordHasher.Verify(password, user.HashedPassword);
        }
    }
}

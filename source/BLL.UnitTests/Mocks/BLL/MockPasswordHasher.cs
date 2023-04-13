using Modules.Interfaces.Utilities;
namespace BLL.UnitTests.Mocks.BLL
{
    internal class MockPasswordHasher : IPasswordHasher
    {
        public string Hash(string password, byte[] salt)
        {
            return password;
        }

        public bool Verify(string plainPassword, string hashedPassword)
        {
            return plainPassword == hashedPassword;
        }
    }
}

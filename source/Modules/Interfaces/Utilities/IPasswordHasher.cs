namespace Modules.Interfaces.Utilities
{
    public interface IPasswordHasher
    {
        string Hash(string password, byte[] salt = null);
        bool Verify(string plainPassword, string hashedPassword);
    }
}
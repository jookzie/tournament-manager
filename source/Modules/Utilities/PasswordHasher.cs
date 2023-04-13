using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Modules.Interfaces.Utilities;
using System.Security.Cryptography;

namespace Modules.Utilities
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Takes a string and outputs a hash and its salt in the format hash:salt 
        /// In case of verification, the salt is specified and the method returns *only* the hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>The hash and salt (string)</returns>

        public string Hash(string password, byte[] salt = null)
        {
            var returnOnlyHash = true;
            if (salt == null)
            {
                returnOnlyHash = false;
                salt = new byte[128 / 8];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (returnOnlyHash)
                return hash;
            return $"{hash}:{Convert.ToBase64String(salt)}";
        }
        /// <summary>
        /// The method gets the hash of the plain password and compares it with the provided hashed password.
        /// </summary>
        /// <param name="plainPassword"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public bool Verify(string plainPassword, string hashedPassword)
        {
            // Retrieve the stored hash:salt
            var hashAndSalt = hashedPassword.Split(':');

            // Check the validity of the hash:salt
            if (hashAndSalt.Length != 2)
                throw new FormatException("The hash and salt must be in the format: hash:salt.");
            if (hashedPassword.Length != 69)
                throw new FormatException($"Expected hashed password length to be 69, it is {hashedPassword.Length}.");

            var salt = Convert.FromBase64String(hashAndSalt[1]);

            // Hash the given password
            var hashPlainPass = Hash(plainPassword, salt);

            // Compare hashes
            return hashAndSalt[0] == hashPlainPass;
        }
    }
}

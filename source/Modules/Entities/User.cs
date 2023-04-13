using Modules.Enums;
using System.Text.RegularExpressions;

namespace Modules.Entities
{
    public class User
    {
        public Guid ID { get; init; } = Guid.NewGuid();
        public AccountType AccountType { get; set; } = AccountType.User;
        public string Name
        {
            get => _name;
            init
            {
                if (value.Length > 64 || value.Length < 2)
                    throw new ArgumentOutOfRangeException("Name", "Name length must be between 2 and 64 characters.");
                _name = value;
            }
        }
        public string Email
        {
            get => _email;
            init
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Email", "Email cannot be empty.");
                if (value.Length > 64)
                    throw new ArgumentOutOfRangeException("Email", "Email length should not exceed 64 characters.");
                var validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
                if (!validateEmailRegex.IsMatch(value))
                    throw new FormatException("Email is not valid.");
                _email = value;
            }
        }
        public string HashedPassword
        {
            get => _hash;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Password", "Password cannot be empty.");
                _hash = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is not User) return false;
            return ((User)obj).ID.Equals(ID);
        }
        public override string ToString() => Name + (AccountType == AccountType.Admin ? " [Admin]" : "");
        private string _name;
        private string _email;
        private string _hash;

    }
}

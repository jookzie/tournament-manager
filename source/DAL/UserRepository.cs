using Modules.Entities;
using Modules.Enums;
using Modules.Interfaces.DAL;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly string _conString;
        public UserRepository(string conString)
        {
            _conString = conString;
        }

        public bool AddUser(User user)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(
                    _conString,
                    "INSERT INTO users(id, name, email, password, accountType)" +
                    "VALUES (@id, @name, @email, @password, @accountType);",
                    new MySqlParameter("@id", user.ID.ToByteArray()),
                    new MySqlParameter("@name", user.Name),
                    new MySqlParameter("@email", user.Email),
                    new MySqlParameter("@password", user.HashedPassword),
                    new MySqlParameter("@accountType", user.AccountType));
                return true;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return false;
            }

        }


        public IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>();

            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT id, name, email, password, accountType FROM users");

            if (!reader.HasRows) return users;
            while (reader.Read())
            {
                users.Add(
                new User
                {
                    ID = reader.GetGuid("id"),
                    Name = reader.GetString("name"),
                    Email = reader.GetString("email"),
                    HashedPassword = reader.GetString("password"),
                    AccountType = Enum.Parse<AccountType>(reader.GetString("accountType"), true)
                });
            }
            return users;
        }
        public Dictionary<Guid, List<User>> GetAllRegisteredPlayers()
        {
            var tournamentPlayers = new Dictionary<Guid, List<User>>();
            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT tournament_id, user_id, name, email, password, accountType " +
                "FROM users_tournaments as ut " +
                "INNER JOIN users as u " +
                "ON ut.user_id = u.id " +
                "ORDER BY tournament_id");
            Guid lastID = Guid.Empty,
                 currentID = Guid.Empty;
            var currentList = new List<User>();
            while (reader.Read())
            {
                currentID = reader.GetGuid("tournament_id");
                if (currentID != lastID && lastID != Guid.Empty)
                {
                    tournamentPlayers.Add(lastID, currentList);
                    currentList = new();
                }
                lastID = currentID;
                currentList.Add(new User
                {
                    ID = reader.GetGuid("user_id"),
                    Name = reader.GetString("name"),
                    Email = reader.GetString("email"),
                    HashedPassword = reader.GetString("password"),
                    AccountType = Enum.Parse<AccountType>(reader.GetString("accountType"), true)
                });
            }
            if (reader.HasRows) tournamentPlayers[currentID] = currentList;
            return tournamentPlayers;
        }
        public IEnumerable<User> GetPlayersByTournament(Guid id)
        {
            var users = new List<User>();

            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT id, name, email, password, accountType " +
                "FROM users INNER JOIN users_tournaments " +
                "ON users.id = users_tournaments.user_id " +
                "WHERE users_tournaments.tournament_id = @id",
                new MySqlParameter("@id", id));

            if (!reader.HasRows) return users;
            while (reader.Read())
            {
                users.Add(
                new User
                {
                    ID = reader.GetGuid("id"),
                    Name = reader.GetString("name"),
                    Email = reader.GetString("email"),
                    HashedPassword = reader.GetString("password"),
                    AccountType = Enum.Parse<AccountType>(reader.GetString("accountType"), true)
                });
            }
            return users;
        }
        public User? GetUserBy(Guid id)
        {
            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT name, email, password, accountType FROM users WHERE id = @id",
                new MySqlParameter("@id", id));
            if (!reader.Read()) return null;
            return new User
            {
                ID = id,
                Name = reader.GetString("name"),
                Email = reader.GetString("email"),
                HashedPassword = reader.GetString("password"),
                AccountType = Enum.Parse<AccountType>(reader.GetString("accountType"), true)
            };
        }
        public User? GetUserBy(string email)
        {
            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT id, name, password, accountType FROM users WHERE email = @email",
                new MySqlParameter("@email", email));
            if (!reader.Read()) return null;
            return new User
            {
                ID = reader.GetGuid("id"),
                Name = reader.GetString("name"),
                Email = email,
                HashedPassword = reader.GetString("password"),
                AccountType = Enum.Parse<AccountType>(reader.GetString("accountType"), true)
            };
        }

        public bool UpdateUser(User user)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(_conString,
                    "UPDATE users SET name = @name, email = @email, password = @password, accountType = @accountType WHERE id = @id",
                    new MySqlParameter("@name", user.Name),
                    new MySqlParameter("@email", user.Email),
                    new MySqlParameter("@password", user.HashedPassword),
                    new MySqlParameter("@accountType", user.AccountType),
                    new MySqlParameter("@id", user.ID));
                return true;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return false;
            }
        }
        public bool DeleteUser(User user)
        {
            return MySqlHelper.ExecuteNonQuery(_conString,
                "DELETE FROM users WHERE id = @id",
                new MySqlParameter("@id", user.ID))
                > 0;
        }
    }
}

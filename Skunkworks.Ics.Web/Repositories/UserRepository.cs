using System.Linq;
using Dapper;
using Skunkworks.Ics.Web.Managers;
using Skunkworks.Ics.Web.Models;

namespace Skunkworks.Ics.Web.Repositories
{
    /// <summary>
    /// Class that represents the Users table in the Database
    /// </summary>
    public class UserRepository<TUser> where TUser : IdentityUser
    {
        private readonly DatabaseManager _db;

        public DatabaseManager DataStore
        {
            get
            {
                return _db;
            }
        }

        /// <summary>
        /// Constructor that takes a DatabaseManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserRepository(DatabaseManager database)
        {
            _db = database;
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(TUser user)
        {
            const string query = @"INSERT INTO [dbo].[User]
                    (UserName, PasswordHash, SecurityStamp, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, AccessFailedCount, LockoutEnabled, LockoutEndDateUtc, TwoFactorEnabled, FirstName, LastName)
                    VALUES (@Username, @PasswordHash, @SecurityStamp, @Email, @EmailConfirmed, @PhoneNumber, @PhoneNumberConfirmed, @AccessFailedCount, @LockoutEnabled, @LockoutEndDateUtc, @TwoFactorEnabled, @FirstName, @LastName)
                    SELECT CAST(SCOPE_IDENTITY() AS int)";
            return DataStore.Connection.ExecuteScalar<int>(query, user);
        }

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserName(int id)
        {
            const string query = @"SELECT UserName FROM [dbo].[User] AS U WHERE U.Id = @Id";
            return DataStore.Connection.ExecuteScalar<string>(query, new { Id = id });
        }

        /// <summary>
        /// Returns a TUser given the Member's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TUser GetUserById(int id)
        {
            const string query = @"SELECT * FROM [dbo].[User] AS U WHERE U.Id = @Id";
            return DataStore.Connection.Query<TUser>(query, new { Id = id }).FirstOrDefault();
        }

        /// <summary>
        /// Returns a TUser instances given a user's username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public TUser GetUserByName(string username)
        {
            const string query = @"SELECT * FROM [dbo].[User] AS U WHERE U.UserName = @Username";
            return DataStore.Connection.Query<TUser>(query, new { Username = username }).FirstOrDefault();
        }

        /// <summary>
        /// Returns a TUser instances given a user's email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public TUser GetUserByEmail(string email)
        {
            const string query = @"SELECT * FROM [dbo].[User] AS U WHERE U.Email = @Email";
            return DataStore.Connection.Query<TUser>(query, new { Email = email }).FirstOrDefault();
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPasswordHash(int id)
        {
            const string query = @"SELECT PasswordHash FROM [dbo].[User] AS U WHERE U.Id = @Id";
            return DataStore.Connection.ExecuteScalar<string>(query, new { Id = id });
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public void SetPasswordHash(int id, string passwordHash)
        {
            const string query = @"UPDATE [dbo].[User] SET PasswordHash = @PasswordHash WHERE Id = @Id";
            DataStore.Connection.Execute(query, new { PasswordHash = passwordHash, Id = id });
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSecurityStamp(int id)
        {
            const string query = @"SELECT SecurityStamp FROM [dbo].[User] AS U WHERE U.Id = @Id";
            return DataStore.Connection.ExecuteScalar<string>(query, new { Id = id });
        }

        /// <summary>
        /// Updates a Member in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Update(TUser user)
        {
            const string query = @"UPDATE [dbo].[User] SET
                UserName = @UserName, PasswordHash = @PasswordHash, SecurityStamp = @SecurityStamp, Email = @Email, EmailConfirmed = @EmailConfirmed,
                PhoneNumber = @PhoneNumber, PhoneNumberConfirmed = @PhoneNumberConfirmed, AccessFailedCount = @AccessFailedCount, LockoutEnabled = @LockoutEnabled,
                LockoutEndDateUtc = @LockoutEndDateUtc, TwoFactorEnabled = @TwoFactorEnabled, FirstName = @FirstName, LastName = @LastName 
                WHERE Id = @Id";

            DataStore.Connection.Execute(query, user);
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private void Delete(int id)
        {
            const string query = @"DELETE FROM [dbo].[User] WHERE Id = @Id";
            DataStore.Connection.Execute(query, new { Id = id });
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Delete(TUser user)
        {
            Delete(user.Id);
        }
    }
}

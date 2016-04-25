using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.AspNet.Identity;
using Skunkworks.Ics.Web.Managers;
using Skunkworks.Ics.Web.Models;
// ReSharper disable RedundantAnonymousTypePropertyName

namespace Skunkworks.Ics.Web.Repositories
{
    public class UserLoginRepository
    {
        private readonly DatabaseManager _db;

        public DatabaseManager DataStore
        {
            get
            {
                return _db;
            }
        }

        public UserLoginRepository(DatabaseManager database)
        {
            _db = database;
        }

        /// <summary>
        /// Inserts a new login in the UserLogins table
        /// </summary>
        /// <param name="user">User to have new login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public void Insert(IdentityUser user, UserLoginInfo login)
        {
            const string query = @"INSERT INTO [dbo].[UserLogin] (UserId, LoginProvider, ProviderKey) VALUES (@UserId, @LoginProvider, @ProviderKey)";
            DataStore.Connection.Execute(query, new
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                UserId = user.Id
            });
        }

        /// <summary>
        /// Return a userId given a user's login
        /// </summary>
        /// <param name="login">The user's login info</param>
        /// <returns></returns>
        public int FindUserIdByLogin(UserLoginInfo login)
        {
            const string query = @"SELECT UserId FROM [dbo].[UserLogin] WHERE LoginProvider = @LoginProvider AND ProviderKey = @ProviderKey";
            return DataStore.Connection.ExecuteScalar<int>(query, new
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey
            });
        }

        /// <summary>
        /// Return a user's login given a userId
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        public List<UserLoginInfo> FindByUserId(IdentityUser user)
        {
            const string query = @"SELECT LoginProvider, ProviderKey FROM [dbo].[UserLogin] WHERE UserId = @UserId";
            return DataStore.Connection.Query<UserLoginInfo>(query, new { UserId = user.Id }).ToList();
        }

        /// <summary>
        /// Deletes a login from a user in the UserLogins table
        /// </summary>
        /// <param name="user">User to have login deleted</param>
        /// <param name="login">Login to be deleted from user</param>
        /// <returns></returns>
        public void Delete(IdentityUser user, UserLoginInfo login)
        {
            const string query = @"DELETE FROM [dbo].[UserLogin] WHERE UserId = @UserId AND LoginProvider = @LoginProvider AND ProviderKey = @ProviderKey";
            DataStore.Connection.Execute(query, new
            {
                UserId = user.Id,
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey
            });
        }

        /// <summary>
        /// Deletes all Logins from a user in the UserLogins table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int userId)
        {
            const string query = @"DELETE FROM [dbo].[UserLogin] WHERE UserId = @UserId";
            DataStore.Connection.Execute(query, new { UserId = userId });
        }
    }
}
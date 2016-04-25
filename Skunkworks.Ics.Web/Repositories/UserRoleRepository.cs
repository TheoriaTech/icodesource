using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Skunkworks.Ics.Web.Managers;
using Skunkworks.Ics.Web.Models;

namespace Skunkworks.Ics.Web.Repositories
{
    public class UserRoleRepository
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
        public UserRoleRepository(DatabaseManager database)
        {
            _db = database;
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(int userId)
        {
            const string query = @"SELECT R.Name FROM [dbo].[UserRole] UR INNER JOIN [dbo].[Role] R ON R.Id = UR.RoleId WHERE UR.UserId = @UserId";
            return DataStore.Connection.Query<string>(query, new { UserId = userId }).ToList();
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int userId)
        {
            const string query = @"DELETE FROM [dbo].[UserRole] WHERE UserId = @UserId";
            DataStore.Connection.Execute(query, new { UserId = userId });
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <param name="roleId">The role id</param>
        /// <returns></returns>
        public void Delete(int userId, int roleId)
        {
            const string query = @"DELETE FROM [dbo].[UserRole] WHERE UserId = @UserId AND RoleId = @RoleId";
            DataStore.Connection.Execute(query, new
            {
                UserId = userId,
                RoleId = roleId
            });
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public void Insert(IdentityUser user, int roleId)
        {
            const string query = @"INSERT INTO [dbo].[UserRole] (UserId, RoleId) VALUES (@UserId, @RoleId)";
            DataStore.Connection.Execute(query, new
            {
                UserId = user.Id,
                RoleId = roleId
            });
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Skunkworks.Ics.Web.Managers;
using Skunkworks.Ics.Web.Models;
using Skunkworks.Ics.Web.Repositories;
// ReSharper disable once ObjectCreationAsStatement

namespace Skunkworks.Ics.Web.Stores
{
    public class RoleStore<TRole> : IRoleStore<TRole, int> where TRole : IdentityRole
    {
        private RoleRepository RoleTable { get; set; }
        public DatabaseManager Database { get; private set; }

        /// <summary>
        /// Default constructor that initializes a new database
        /// instance using the Default Connection string
        /// </summary>
        public RoleStore()
        {
            new RoleStore<TRole>(new DatabaseManager());
        }

        /// <summary>
        /// Constructor that takes a dbmanager as argument 
        /// </summary>
        /// <param name="db"></param>
        public RoleStore(DatabaseManager db)
        {
            Database = db;
            RoleTable = new RoleRepository(db);
        }

        /// <summary>
        /// Insert a new TRole in the RoleTable
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            RoleTable.Insert(role);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an TRole instance based on a roleId query 
        /// </summary>
        /// <param name="roleId">The role's Id</param>
        /// <returns></returns>
        public Task<TRole> FindByIdAsync(int roleId)
        {
            if (roleId <= 0)
            {
                throw new ArgumentNullException("roleId");
            }
            TRole result = RoleTable.GetRoleById(roleId) as TRole;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Returns an TUser instance based on a roleName query 
        /// </summary>
        /// <param name="roleName">The role's name</param>
        /// <returns></returns>
        public Task<TRole> FindByNameAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }
            var result = RoleTable.GetRoleByName(roleName) as TRole;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Updates the RoleTable with the TRole instance values
        /// </summary>
        /// <param name="role">TRole to be updated</param>
        /// <returns></returns>
        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            RoleTable.Update(role);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="role">Role to be deleted</param>
        /// <returns></returns>
        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            RoleTable.Delete(role.Id);

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (Database != null)
            {
                Database.Dispose();
                Database = null;
            }
        }

    }
}
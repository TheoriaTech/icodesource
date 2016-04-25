using Dapper;
using Skunkworks.Ics.Web.Managers;
using Skunkworks.Ics.Web.Models;

namespace Skunkworks.Ics.Web.Repositories
{
    public class RoleRepository
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
        public RoleRepository(DatabaseManager database)
        {
            _db = database;
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public void Insert(IdentityRole role)
        {
            const string query = @"INSERT INTO [dbo].[Role] (Name) VALUES (@Name)";
            DataStore.Connection.Execute(query, role);
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="id">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(int id)
        {
            const string query = @"SELECT Name FROM [dbo].[Role] AS R WHERE R.Id = @Id";
            return DataStore.Connection.ExecuteScalar<string>(query, new { Id = id });
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="name">Role's name</param>
        /// <returns>Role's Id</returns>
        public int GetRoleId(string name)
        {
            const string query = @"SELECT Id FROM [dbo].[Role] AS R WHERE R.Name = @Name";
            return DataStore.Connection.ExecuteScalar<int>(query, new { Name = name });
        }

        /// <summary>
        /// Gets the IdentityRole given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IdentityRole GetRoleById(int roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if (roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId > 0)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        public void Update(IdentityRole role)
        {
            const string query = @"UPDATE [dbo].[Role] SET Name = @Name WHERE Id = @Id";
            DataStore.Connection.Execute(query, role);
        }

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public void Delete(int roleId)
        {
            const string query = @"DELETE FROM [dbo].[Role] WHERE Id = @Id";
            DataStore.Connection.Execute(query, new { id = roleId });
        }
    }
}
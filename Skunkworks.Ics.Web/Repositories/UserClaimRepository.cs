using System;
using System.Security.Claims;
using Dapper;
using Skunkworks.Ics.Web.Managers;
using Skunkworks.Ics.Web.Models;
// ReSharper disable RedundantAnonymousTypePropertyName

namespace Skunkworks.Ics.Web.Repositories
{
    public class UserClaimRepository
    {
        private readonly DatabaseManager _db;

        public DatabaseManager DataStore
        {
            get
            {
                return _db;
            }
        }

        public UserClaimRepository(DatabaseManager database)
        {
            _db = database;
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public void Insert(Claim userClaim, int userId)
        {
            const string query = @"INSERT INTO UserClaim (UserId, ClaimValue, ClaimType) VALUES (@UserId, @Value, @Type)";
            DataStore.Connection.Execute(query, new
            {
                Value = userClaim.Value,
                Type = userClaim.Type,
                UserId = userId
            });
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(int userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            const string query = @"SELECT * FROM UserClaim WHERE UserId=@UserId";
            foreach (var c in DataStore.Connection.Query(query, new { UserId = userId }))
            {
                claims.AddClaim(new Claim(c.ClaimType, c.ClaimValue));
            }

            return claims;
        }

        /// <summary>
        /// Deletes all claims for a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int userId)
        {
            const string query = @"DELETE FROM UserClaim WHERE UserId = @UserId";
            DataStore.Connection.Execute(query, new { UserId = userId });
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public void Delete(IdentityUser user, Claim claim)
        {
            const string query = @"DELETE FROM UserClaim WHERE UserId = @UserId AND ClaimValue = @Value AND ClaimType = @Type";
            DataStore.Connection.Execute(query, new
            {
                UserId = user.Id,
                Value = claim.Value,
                Type = claim.Type
            });
        }
    }
}
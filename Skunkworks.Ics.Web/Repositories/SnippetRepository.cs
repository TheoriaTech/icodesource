using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Skunkworks.Ics.Web.Models;

namespace Skunkworks.Ics.Web.Repositories
{
    public class SnippetRepository
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public void Create(string username, string title, string language, string code)
        {
            string query = "INSERT INTO Snippet (UserName, Title, Language, Code) VALUES (@UserName, @Title, @Language, @Code)";
            conn.Execute(query, new { UserName = username, Title = title, Language = language, Code = code });

        }

        public IEnumerable<SnippetModel> Read()
        {
            string query = "SELECT * FROM Snippet";
            return conn.Query<SnippetModel>(query);
        }

        /// <summary>
        /// READ FOR SPECIFIC ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// 
        public SnippetModel Read(int id)
        {
            string query = "SELECT * FROM Snippet WHERE Id=@Id";
            return conn.Query<SnippetModel>(query, new { Id = id }).FirstOrDefault();
        }

        public void Update(int id, string username, string title, string language,string code)
        {
            string query = "UPDATE Snippet SET UserName = @UserName, Title= @Title, Language=@Language, Code=@Code WHERE Id = @Id";
            conn.Execute(query, new { Id = id, UserName = username, Title = title, Language = language, Code = code });
        }
        

        public void Delete()
        {
            string query = "DELETE FROM Snippet WHERE Id > 0";
            conn.Execute(query);
        }

        /// <summary>
        /// Deletes the specified Id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            string query = "DELETE FROM Snippet WHERE Id = @Id";
            conn.Execute(query, new { Id = id });
        }

    }
    
}
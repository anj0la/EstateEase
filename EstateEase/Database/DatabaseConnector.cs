using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace EstateEase.Database
{
    /// <summary>
    /// Class <c>DatabaseConnector</c> connects a database. There is one public function, <c>GetConnection</c>, which creates and returns a new connection to the database.
    /// </summary>
    public class DatabaseConnector(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        /// <summary>
        /// Method <c>GetConnection</c> returns a connection to the database. 
        /// </summary>
        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
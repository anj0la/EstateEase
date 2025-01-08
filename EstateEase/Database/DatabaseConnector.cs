using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace EstateEase.Database
{
    public class DatabaseConnector(string connectionString)
    {
        private readonly string _connectionString = connectionString;
    

    public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }

}
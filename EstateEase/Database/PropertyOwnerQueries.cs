using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Database;
using EstateEase.Models;

namespace EstateEase.Database
{
    public class PropertyOwnerQueries(DatabaseConnector databaseConnection) : BaseQuery(databaseConnection)
    {
        public PropertyOwner? GetPropertyOwnerFromDatabase(string firstName, string lastName)
        {
            using var connection = _databaseConnection.GetConnection();
            string query = "SELECT first_name, last_name, email, country_code, phone_number FROM PropertyOwners WHERE first_name = @firstName AND last_name = @last_name";

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new PropertyOwner(
                    reader["first_name"].ToString(),
                    reader["last_name"].ToString(),
                    reader["email"].ToString(),
                    reader["country_code"].ToString(),
                    reader["phone_number"].ToString()
                );
            }

            return null; // Return null if no user is found
        }

        public List<PropertyOwner> GetAllPropertyOwners()
        {
            var propertyOwners = new List<PropertyOwner>();
            using var connection = _databaseConnection.GetConnection();

            string query = "SELECT first_name, last_name, email, country_code, phone_number FROM PropertyOwners";
            using var command = new SQLiteCommand(query, connection);
        }
    }
}




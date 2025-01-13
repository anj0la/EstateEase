using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Database;
using EstateEase.Models;
using Windows.System;

namespace EstateEase.Database
{
    public class PropertyOwnerQueries(DatabaseConnector databaseConnection) : BaseQuery(databaseConnection)
    {
        /// <summary>
        /// Method <c>GetPropertyOwnerFromDatabase</c> returns a property owner from the database based on the first name and last name. It is assumed that one property owner is retrieved, as the first and last name should make an unique pair.
        /// </summary>
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

        /// <summary>
        /// Method <c>GetAllPropertyOwnersFromDatabase</c> returns all property owners from the database.
        /// </summary>
        public List<PropertyOwner> GetAllPropertyOwnersFromDatabase()
        {
            var propertyOwners = new List<PropertyOwner>();
            using var connection = _databaseConnection.GetConnection();

            string query = "SELECT first_name, last_name, email, country_code, phone_number FROM PropertyOwners";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var propertyOwner = new PropertyOwner(
                    reader["first_name"].ToString(),
                    reader["last_name"].ToString(),
                    reader["email"].ToString(),
                    reader["country_code"].ToString(),
                    reader["phone_number"].ToString()
                    );

                propertyOwners.Add(propertyOwner);
            }

            return propertyOwners;
        }

        /// <summary>
        /// Method <c>AddPropertyOwnerToDatabase</c> adds a property owner to the database.
        /// </summary>
        public void AddPropertyOwnerToDatabase(string firstName, string lastName, string email, string countryCode, string phoneNumber)
        {
            using var connection = _databaseConnection.GetConnection();
            string query = "INSERT INTO PropertyOwners (first_name, last_name, email, country_code, phone_number) VALUES (@FirstName, @LastName, @Email, @CountryCode, @PhoneNumber)";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@CountryCode", countryCode);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            command.ExecuteNonQuery();
        }

        // Update and Delete will be added after front-end support.

    }
}

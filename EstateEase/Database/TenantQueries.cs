using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Models;

namespace EstateEase.Database
{
    public class TenantQueries(DatabaseConnector databaseConnection) : BaseQuery(databaseConnection)
    {
        /// <summary>
        /// Method <c>GetTenantFromDatabase</c> returns a tenant from the database based on the first name and last name. It is assumed that one tenant is retrieved, as the first and last name should make an unique pair.
        /// </summary>
        public Tenant? GetTenantFromDatabase(string firstName, string lastName)
        {
            using var connection = _databaseConnection.GetConnection();
            string query = "SELECT first_name, last_name, email, country_code, phone_number, lease_start, lease_end, status, rating FROM Tenants WHERE first_name = @firstName AND last_name = @last_name";

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                Status status = (Status)reader.GetInt32(reader.GetOrdinal("status"));
                Rating rating = (Rating)reader.GetInt32(reader.GetOrdinal("rating"));

                return new Tenant(
                    reader["first_name"].ToString(),
                    reader["last_name"].ToString(),
                    reader["email"].ToString(),
                    reader["country_code"].ToString(),
                    reader["phone_number"].ToString(),
                    reader["lease_start"].ToString(),
                    reader["lease_end"].ToString(),
                    status,
                    rating
                );
            }

            return null; // Return null if no user is found
        }

        /// <summary>
        /// Method <c>GetAllTenantsFromDatabase</c> returns all tenants from the database.
        /// </summary>
        public List<Tenant> GetAllTenantsFromDatabase()
        {
            var tenants = new List<Tenant>();
            using var connection = _databaseConnection.GetConnection();

            string query = "SELECT first_name, last_name, email, country_code, phone_number, lease_start, lease_end, status, rating FROM Tenants";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Status status = (Status)reader.GetInt32(reader.GetOrdinal("status"));
                Rating rating = (Rating)reader.GetInt32(reader.GetOrdinal("rating"));

                var tenant = new Tenant(
                    reader["first_name"].ToString(),
                    reader["last_name"].ToString(),
                    reader["email"].ToString(),
                    reader["country_code"].ToString(),
                    reader["phone_number"].ToString(),
                    reader["lease_start"].ToString(),
                    reader["lease_end"].ToString(),
                    status,
                    rating
                );

                tenants.Add(tenant);
            }

            return tenants;
        }

        /// <summary>
        /// Method <c>AddTenantToDatabase</c> adds a tenant to the database.
        /// </summary>
        public void AddTenantToDatabase(string firstName, string lastName, string email, string countryCode, string phoneNumber, string leaseStart, string leastEnd, Status status, Rating rating)
        {
            using var connection = _databaseConnection.GetConnection();
            string query = "INSERT INTO Tenants (first_name, last_name, email, country_code, phone_number, lease_start, lease_end, status, rating) VALUES (@FirstName, @LastName, @Email, @CountryCode, @PhoneNumber, @LeaseStart, @LeaseEnd, @Status, @Rating)";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@CountryCode", countryCode);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            command.Parameters.AddWithValue("@LeaseStart", leaseStart);
            command.Parameters.AddWithValue("@LeaseEnd", leastEnd);
            command.Parameters.AddWithValue("@Status", (int)status);
            command.Parameters.AddWithValue("@Rating", (int)rating);

            command.ExecuteNonQuery();
        }

        // Update and Delete will be added after front-end support.
    }
}



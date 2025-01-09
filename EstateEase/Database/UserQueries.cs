﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Database;
using EstateEase.Models;
using Microsoft.AspNetCore.Identity;

namespace EstateEase.Database
{
    public class UserQueries(DatabaseConnector databaseConnection) : BaseQuery(databaseConnection)
    {
        public User? GetUserFromDatabase()
        {
            using var connection = _databaseConnection.GetConnection();
            connection.Open();

            string query = "SELECT Name, Email, PasswordHash FROM Users LIMIT 1";

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User(
                    reader["Name"].ToString(),
                    reader["Email"].ToString(),
                    reader["PasswordHash"].ToString()
                );
            }

            return null; // Return null if no user is found
        }

        public void SaveUserToDatabase(User user)
        {
            using var connection = _databaseConnection.GetConnection();
            connection.Open();

            string query = "INSERT INTO Users (Name, Email, PasswordHash) VALUES (@Name, @Email, @PasswordHash)";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

            command.ExecuteNonQuery();
        }

        public void UpdateUserInDatabase(User user)
        {
            using var connection = _databaseConnection.GetConnection();
            connection.Open();

            string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE Email = @Email";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@Email", user.Email);

            command.ExecuteNonQuery();
        }
         
        public void AddUser(string name, string email, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var user = new User
                (
                name,
                email
                );

            // Hash the password
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            // Save user to the database
            SaveUserToDatabase(user);
        }

        public void ChangeUserPassword(string password)
        {
            var user = GetUserFromDatabase(); // Assuming there's only one user
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, password);

                // Update user in the database
                UpdateUserInDatabase(user);
            }
        }
    }
}
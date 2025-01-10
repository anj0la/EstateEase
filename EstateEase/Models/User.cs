using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateEase.Models
{
    /// <summary>
    /// Class <c>User</c> models the end-user in the application.
    /// </summary>
    public class User(string firstName, string lastName, string email, string password = "")
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string Email { get; set; } = email;
        public string PasswordHash { get; set; } = password;
    }
}


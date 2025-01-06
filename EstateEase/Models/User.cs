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
    internal class User(int id, string name, string email, string password)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}


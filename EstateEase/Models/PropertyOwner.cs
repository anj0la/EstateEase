using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateEase.Models
{

    /// <summary>
    /// <c>PropertyOwner</c> models a property owner (i.e., someone that owns the actual property) in the application.
    /// </summary>
    public class PropertyOwner(string name, string email, string phoneNumber)
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string PhoneNumber { get; set; } = phoneNumber;
    }

   

}

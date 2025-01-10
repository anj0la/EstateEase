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
    public class PropertyOwner(string firstName, string lastName, string email, string countryCode, string phoneNumber)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string Email { get; set; } = email;
        public string CountryCode { get; set; } = countryCode;
        public string PhoneNumber { get; set; } = phoneNumber;
    }

   

}

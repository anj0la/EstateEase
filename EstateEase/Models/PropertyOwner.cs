using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateEase.Models
{
    /// <summary>
    /// <c>ContactInfo</c> models the contact information of a property owner (e.g., email and phone number).
    /// </summary>
    internal struct ContactInfo(string email, string phoneNumber)
    {
        public string Email { get; set; } = email;
        public string PhoneNumber { get; set; } = phoneNumber;
    }

    /// <summary>
    /// <c>PropertyOwner</c> models a property owner (i.e., someone that owns the actual property) in the application.
    /// </summary>
    internal class PropertyOwner(string name, ContactInfo contactInfo)
    {
        public string Name { get; set; } = name;
        public ContactInfo ContactInfo = contactInfo;
    }

   

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EstateEase.Models
{
    public enum Status
    {
        Archived,
        Active
    }

    public enum Rating
    {
        Bad,
        Neutral,
        Good
    }

    /// <summary>
    /// <c>Tenant</c> models a tenant (i.e., someone that lives in a property owned by a PropertyOwner) in the application.
    /// </summary>
    public class Tenant(string firstName, string lastName, string email, string countryCode, string phoneNumber, string leaseStart, string leaseEnd, Status status, Rating rating)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string Email { get; set; } = email;
        public string CountryCode { get; set; } = countryCode;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string LeaseStart { get; set; } = leaseStart;
        public string LeaseEnd { get; set; } = leaseEnd;
        public Status Status { get; set; } = status;
        public Rating Rating { get; set; } = rating;
    }

}
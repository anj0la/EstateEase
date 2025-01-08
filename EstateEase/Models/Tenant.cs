using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EstateEase.Models
{
    internal enum Status
    {
        Archived,
        Active
    }

    internal enum Rating
    {
        Bad,
        Neutral,
        Good
    }

    internal class Tenant(string name, string email, string phoneNumber, string leaseStart, string leaseEnd, Status status, Rating rating)
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string LeaseStart { get; set; } = leaseStart;
        public string LeaseEnd { get; set; } = leaseEnd;
        public Status Status { get; set; } = status;
        public Rating Rating { get; set; } = rating;
    }

}
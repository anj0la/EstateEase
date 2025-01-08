using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Input;

namespace EstateEase.Models
{
    internal enum PropertyStatus
    {
        Occupied,
        UnderMaintenace,
        Available
      
    }
    internal struct Address(string addressLine, string locality, string adminstrativeArea, string country, string postalCode)
    {
        public string AddressLine { get; set; } = addressLine;
        public string Locality { get; set; } = locality;
        public string AdministrativeAarea { get; set; } = adminstrativeArea;

        public string Country { get; set; } = country;
        public string PostalCode { get; set; } = postalCode;
    }
    internal class Property(Address address, string dateAdded, string dateListed, double rent, PropertyStatus status, double commissionRate, string ownerName, string imagePath = "")
    {
        public Address Address { get; set; } = address;
        public string DateAdded { get; set; } = dateAdded;
        public string DateListed { get; set; } = dateListed;
        public double Rent { get; set; } = rent;
        public PropertyStatus Status { get; set; } = status;
        public double CommissionRate { get; set; } = commissionRate;
        public string OwnerName { get; set; } = ownerName; 
        public string ImagePath { get; set; } = imagePath;
    }
}

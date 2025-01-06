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
        Available,
        Occupied,
        UnderMaintenace
    }
    internal struct Address(string addressLine, string city, string state, string country, string postalCode)
    {
        public string AddressLine { get; set; } = addressLine;
        public string City { get; set; } = city;
        public string State { get; set; } = state;

        public string Country { get; set; } = country;
        public string PostalCode { get; set; } = postalCode;
    }
    internal class Property(Address address, double rent, double expenses, double commission, PropertyStatus status, string imagePath, PropertyOwner owner)
    {
        public Address Address { get; set; } = address;

        public double Rent { get; set; } = rent;

        public double Expenses { get; set; } = expenses;

        public double Commission { get; set; } = commission;
        public PropertyStatus Status { get; set; } = status;

        public string ImagePath { get; set; } = imagePath;

        public PropertyOwner Owner { get; set; } = owner;

    }
}

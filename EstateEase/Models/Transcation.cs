using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EstateEase.Models
{
    internal enum TranscationType
    {
        Expense,
        Income
    }
    internal enum TranscationCategory
    {
        Maintenance,
        Rent
    }
    internal class Transcation(TranscationType type, TranscationCategory category, string date, double amount, string description, string propertyAddress)
    {
        public TranscationType Type { get; set; } = type;
        public TranscationCategory Category { get; set; } = category;
        public string Date { get; set; } = date;
        public double Amount { get; set; } = amount;
        public string Description { get; set; } = description;
        public string PropertyAddress { get; set; } = propertyAddress;
    }
}

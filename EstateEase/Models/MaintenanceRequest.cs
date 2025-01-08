using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateEase.Models
{
   internal enum MaintenanceStatus
    {
        Pending,
        InProgress,
        Completed
    }
    internal class MaintenanceRequest(string dateRequested, string dateCompleted, string description, MaintenanceStatus status, double cost, string vendor = "")
    {
        public string DateRequested { get; set; } = dateRequested;
        public string DateCompleted { get; set; } = dateCompleted;
        public string Description { get; set; } = description;
        public MaintenanceStatus Status { get; set; } = status;
        public double Cost { get; set; } = cost;
        public string vendor { get; set; } = vendor;
    }
}

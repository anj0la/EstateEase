using System;
using System.Collections.Generic;
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

    internal enum Feedback
    {
        Bad = -1,
        Neutral,
        Good
    }


    internal class Tenant(string name, Status status, Feedback feedback)
    {
        public string Name { get; set; } = name;
        public Status Status { get; set; } = status;
        public Feedback Feedback { get; set; } = feedback;
    }

}
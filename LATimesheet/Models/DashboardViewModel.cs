using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LATimesheet.Data.Entities;

namespace LATimesheet.Models
{
    public class DashboardViewModel
    {
        public List<TimeTracker> ClockHistory { get; set; } = new List<TimeTracker>();
        public string Username { get; set; }
        public string Status { get; set; }
    }
}
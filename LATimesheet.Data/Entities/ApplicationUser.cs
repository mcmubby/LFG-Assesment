using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LATimesheet.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string ClockStatus { get; set; }
        public virtual List<TimeTracker> TimeTracker { get; set; }
    }
}

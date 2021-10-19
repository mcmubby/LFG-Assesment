using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LATimesheet.Models;

namespace LATimesheet.Services
{
    public interface ITimeTracker
    {
        Task<DashboardViewModel> CheckIn(string userId);
        Task<DashboardViewModel> CheckOut(string userId);
        Task<DashboardViewModel> GetDetails(string userId);

    }
}
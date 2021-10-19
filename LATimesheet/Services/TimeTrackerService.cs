using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LATimesheet.Data.DbContexts;
using LATimesheet.Data.Entities;
using LATimesheet.Helper;
using LATimesheet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LATimesheet.Services
{
    public class TimeTrackerService : ITimeTracker
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TimeTrackerService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<DashboardViewModel> CheckIn(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = new DashboardViewModel();

            //Check if user exist
            if(user is null){return null;}

            //Fetch user clock history
            var clockHistory = await GetDailyTimeTrackerHistory(userId);

            //If already clocked in do nothing
            if(user.ClockStatus == Status.ClockedIn.ToString()){
                result.Username = user.UserName;
                result.Status = user.ClockStatus;
                result.ClockHistory = clockHistory;
                return result;
            }


            var clockIn = new TimeTracker
            {
                Status = Status.ClockedIn.ToString(),
                Date = DateTime.Now.ToLongDateString(),
                Time = DateTime.Now,
                UserId = user.Id
            };

            //Add the clockIn to db
            _db.TimeTrackers.Add(clockIn);
            clockHistory.Add(clockIn);

            //Set user as clocked in
            user.ClockStatus = Status.ClockedIn.ToString();

            //Set result
            result.ClockHistory = clockHistory;
            result.Username = user.UserName;
            result.Status = user.ClockStatus;

            _db.SaveChanges();

            return result;
        }

        public async Task<DashboardViewModel> CheckOut(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = new DashboardViewModel();

            //Check if user exist or already clocked in
            if(user is null){return null;}

            //Fetch clock history
            var clockHistory = await GetDailyTimeTrackerHistory(userId);

            //If already clocked out do nothing
            if(user.ClockStatus == Status.ClockedOut.ToString()){
                result.Username = user.UserName;
                result.Status = user.ClockStatus;
                result.ClockHistory = clockHistory;
                return result;
            }

            var clockOut = new TimeTracker
            {
                Status = Status.ClockedOut.ToString(),
                Date = DateTime.Now.ToLongDateString(),
                Time = DateTime.Now,
                UserId = user.Id
            };

            //Add the clockIn to db & clockHistory before saving changes
            _db.TimeTrackers.Add(clockOut);
            clockHistory.Add(clockOut);

            //Set user as clocked in
            user.ClockStatus = Status.ClockedOut.ToString();

            //Set result
            result.ClockHistory = clockHistory;
            result.Username = user.UserName;
            result.Status = user.ClockStatus;

            _db.SaveChanges();

            return result;
        }

        public async Task<DashboardViewModel> GetDetails(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            //Check if user exist or already clocked in
            if(user is null){return null;}

            var result = new DashboardViewModel();

            var clockHistory = await GetDailyTimeTrackerHistory(userId);

            result.ClockHistory = clockHistory;
            result.Username = user.UserName;
            result.Status = user.ClockStatus;

            return result;
        }

        private async Task<List<TimeTracker>> GetDailyTimeTrackerHistory(string userId)
        {
            var clockHistory = new List<TimeTracker>();

            clockHistory = await _db.TimeTrackers.Where(o => o.UserId == userId).ToListAsync();

            return clockHistory;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPSyncCollegeRoom2018.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASPSyncCollegeRoom2018.Models
{
    public static class Seed
    {
        public static bool isFirstTime = true;
        public static void Initialize(CalendarDBContext dbContext)
        {

            if (dbContext.ScheduleData.Any())
            {
                return;   // DB has been seeded
            }

            dbContext.ScheduleData.AddRange(
                    new ScheduleData
                    {
                        Subject = "Ultimate1",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddHours(1),
                        RoomId = "1"
                    },

                    new ScheduleData
                    {
                        Subject = "Ultimate1",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddHours(1),
                        RoomId = "2"
                    },

                    new ScheduleData
                    {
                        Subject = "Ultimate1",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddHours(1),
                        RoomId = "3"
                    },

                    new ScheduleData
                    {
                        Subject = "Ultimate1",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddHours(1),
                        RoomId = "4"
                    }
                );
            dbContext.SaveChanges();
        }
    }
}


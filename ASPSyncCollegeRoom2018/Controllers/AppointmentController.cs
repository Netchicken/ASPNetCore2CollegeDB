﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPSyncCollegeRoom2018.Data;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace ASPSyncCollegeRoom2018.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly CalendarDBContext _appDbContext;
        public AppointmentController(CalendarDBContext sampleODataDbContext)
        {
            _appDbContext = sampleODataDbContext;
        }

        //[EnableQuery]
        //public IActionResult Get()
        //{
        //    return Ok(_appDbContext.Appointment.AsQueryable());
        //}

    }
}
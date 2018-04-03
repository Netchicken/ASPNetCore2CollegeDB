#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ASPSyncCollegeRoom2018.Data;
using Microsoft.AspNetCore.Mvc;
using ASPSyncCollegeRoom2018.Models;
using Syncfusion.JavaScript.Models;
using ASPSyncCollegeRoom2018.Business;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ASPSyncCollegeRoom2018.Controllers
{
    // todo https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite


    public class HomeController : Controller
    {

        //https://help.syncfusion.com/aspnet-core/schedule/getting-started

        //CRUD https://help.syncfusion.com/aspnet-core/datamanager/getting-started

        public CalendarDBContext _dbContext { get; }
        private int RoomID = 1;
        // private bool isFirstTime = true;


        public HomeController(CalendarDBContext DBContext)
        {
            _dbContext = DBContext;

            //load seed data
            Seed.Initialize(_dbContext);

        }



        //populate the grid from the db with that ID

        [HttpPost]
        public IActionResult ChooseRoom(string room)
        {
            //get the selected room details from the button click
            ResourceFields SelectedRoom = HomeControllerActions.ChooseRoomFromButtonClick(room);

            //get the ID of the room selected and then call the appointment Data for that room
            RoomID = Convert.ToInt32(SelectedRoom.Id);
            // GetData(RoomID);

            //this works, therefore data is being shown
            ViewBag.RoomTitle = SelectedRoom.Text;
                GetData();
            //reload the index page
            return View("./Index");


        }



        // GET: /<controller>/
        public IActionResult Index()
        {
            var Rooms = HomeControllerActions.LoadRoomDetails();
            var Owners = HomeControllerActions.LoadOwnerDetails(Rooms);

            ViewBag.categorizeData = HomeControllerActions.LoadCategoryValue();

            //ViewBag.Grouping = new List<String>() { "Rooms", "Owners" };
            //ViewBag.RoomData = Rooms;
            //ViewBag.OwnerData = Owners;

            DateTime now = DateTime.Now;
            ViewBag.CurrentDate = now.Date;

            //  ViewBag.appointments = GetData(RoomID);
            //GetData();
            return View();
        }


        //return all saved appointmemts to calendar from ID of room selected
        //public IEnumerable<ScheduleData> GetData(int id)
        //{
        //    List<ScheduleData> data = _dbContext.ScheduleData.Where(d => d.Id == id).ToList();

        //    ViewBag.GetData = data; //not used
        //    return data.ToList();
        //}
        //Original is for the e-datamanager url="Home/GetData"
        public IEnumerable<ScheduleData> GetData()
        {
            List<ScheduleData> SelectedData = new List<ScheduleData>();
            List<ScheduleData> AllData = new List<ScheduleData>();
            if (Seed.isFirstTime == true)
            {
                //load all the data once pass it to the viewbag
                AllData.AddRange(_dbContext.ScheduleData.ToList());
                SelectedData.AddRange(AllData);
                Seed.isFirstTime = false;
            }
            else
            {//call from the loaded list.
                SelectedData.AddRange(AllData.Where(d => d.Id == RoomID).ToList());

            }

            // List<ScheduleData> data =
            //   _dbContext.ScheduleData.ToList();
            //    _dbContext.ScheduleData.Where(d => d.Id == RoomID).ToList();

            //       ViewBag.GetData = SelectedData; //not used
            return SelectedData.ToList();
        }

        public List<ScheduleData> Batch([FromBody] EditParams param)
        {
            if (param.action == "insert" || (param.action == "batch" && (param.added.Count > 0))) // this block of code will execute while inserting the appointments
            {
                ScheduleData appoint = new ScheduleData();
                object result;
                if (param.action == "insert")
                {
                    var value = param.value;
                    foreach (var fieldName in value.GetType().GetProperties())
                    { //Fieldname = {int32 Id}
                        var newName = fieldName.ToString().Split(null);
                        // 0 = Int32  1 = Id
                        
                        if (newName[1] == "Id")
                        {
                            result = 19;
                        }

                        //  (_dbContext.ScheduleData.ToList().Count > 0 ? _dbContext.ScheduleData.ToList().Max(p => p.Id) : 1) + 1;}

                        else if (newName[1] == "StartTime" || newName[1] == "EndTime") result = Convert.ToDateTime(fieldName.GetValue(value));

                        else result = fieldName.GetValue(value);
                        fieldName.SetValue(appoint, result);
                    }
                    _dbContext.ScheduleData.Add(appoint);
                }
                else
                {
                    foreach (var item in param.added.Select((x, i) => new { Value = x, Index = i }))
                    {
                        var value = item.Value;
                        foreach (var fieldName in value.GetType().GetProperties())
                        {
                            var newName = fieldName.ToString().Split(null);
                            if (newName[1] == "Id") result = (_dbContext.ScheduleData.ToList().Count > 0 ? _dbContext.ScheduleData.ToList().Max(p => p.Id) : 1) + 1 + item.Index;
                            else if (newName[1] == "StartTime" || newName[1] == "EndTime") result = Convert.ToDateTime(fieldName.GetValue(value));
                            else result = fieldName.GetValue(value);
                            fieldName.SetValue(appoint, result);
                        }
                        _dbContext.ScheduleData.Add(appoint);
                    }
                }
                _dbContext.SaveChanges();
            }
            if ((param.action == "remove") || (param.action == "batch" && (param.deleted.Count > 0))) // this block of code will execute while removing the appointment
            {
                if (param.action == "remove")
                {
                    ScheduleData app = _dbContext.ScheduleData.Where(c => c.Id == Convert.ToInt32(param.key)).FirstOrDefault();
                    if (app != null) _dbContext.ScheduleData.Remove(app);
                }
                else
                {
                    foreach (var a in param.deleted)
                    {
                        var app = _dbContext.ScheduleData.ToList().Where(c => c.Id == Convert.ToInt32(a.Id)).FirstOrDefault();
                        if (app != null) _dbContext.ScheduleData.Remove(app);
                    }
                }

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception e)
                {//https://stackoverflow.com/questions/24563493/how-to-display-errors-with-asp-net-core


                }

            }
            if (param.action == "update" || (param.action == "batch" && (param.changed.Count > 0))) // this block of code will execute while updating the appointment
            {
                var value = param.action == "update" ? param.value : param.changed[0];
                var filterData = _dbContext.ScheduleData.Where(c => c.Id == Convert.ToInt32(value.Id));
                if (filterData.Count() > 0)
                {
                    ScheduleData appoint = _dbContext.ScheduleData.Single(A => A.Id == Convert.ToInt32(value.Id));
                    appoint.StartTime = Convert.ToDateTime(value.StartTime);
                    appoint.EndTime = Convert.ToDateTime(value.EndTime);
                    appoint.Subject = value.Subject;
                    appoint.Recurrence = value.Recurrence;
                    appoint.AllDay = value.AllDay;
                    appoint.RecurrenceRule = value.RecurrenceRule;
                    appoint.RoomId = value.RoomId;
                    appoint.OwnerId = value.OwnerId;
                }
                _dbContext.SaveChanges();
            }

            List<ScheduleData> data = GetData().ToList();//  _dbContext.ScheduleData.Take(500).ToList();
            return data;
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

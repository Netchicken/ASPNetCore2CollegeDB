using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPSyncCollegeRoom2018.Models;
using Syncfusion.JavaScript.Models;
using Color = System.Drawing.Color;

namespace ASPSyncCollegeRoom2018.Business
{
    public static class HomeControllerActions
    {
        //Pass in the number on the button, return back the room details
        public static ResourceFields ChooseRoomFromButtonClick(string room)
        {
            //http://www.binaryintellect.net/articles/2678a2f2-3236-45a6-a0e5-e6340d9930d5.aspx

            var Rooms = LoadRoomDetails();

            ResourceFields SelectedRoom = null;
            //= (ResourceFields)Rooms.Where(r => r.Id == RoomID.ToString());

            switch (room)
            {
                case "Room1":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "1");
                    break;

                case "Room2":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "2");
                    break;
                case "Room3":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "3");
                    break;
                case "Room4":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "4");
                    break;
                case "Room5":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "5");
                    break;
                case "Room6":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "6");
                    break;
                case "Room7":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "7");
                    break;
                case "Room8":
                    SelectedRoom = (ResourceFields)Rooms.FirstOrDefault(r => r.Id == "8");
                    break;
            }

            return SelectedRoom;
            // return 1;
        }
        public static List<ResourceFields> LoadRoomDetails()
        {
            List<ResourceFields> Rooms = new List<ResourceFields>();
            Rooms.Add(new ResourceFields { Text = "ROOM 1", Id = "1", Color = "#cb6bb2" });
            Rooms.Add(new ResourceFields { Text = "ROOM 2", Id = "2", Color = Color.AntiqueWhite.ToString() });
            Rooms.Add(new ResourceFields { Text = "ROOM 3", Id = "3", Color = Color.Beige.ToString() });
            Rooms.Add(new ResourceFields { Text = "ROOM 4", Id = "4", Color = "#56ca85" });
            Rooms.Add(new ResourceFields { Text = "ROOM 5", Id = "5", Color = "#cb6bb2" });
            Rooms.Add(new ResourceFields { Text = "ROOM 6", Id = "6", Color = "#56ca85" });
            Rooms.Add(new ResourceFields { Text = "ROOM 7", Id = "7", Color = "#cb6bb2" });
            Rooms.Add(new ResourceFields { Text = "ROOM 8", Id = "8", Color = "#56ca85" });
            return Rooms;
        }
        public static List<Categorize> LoadCategoryValue()
        {
            List<Categorize> categorizeValue = new List<Categorize>();
            categorizeValue.Add(new Categorize { text = "Ultimate 1", id = 1, color = "#43b496", fontColor = "#ffffff" });
            categorizeValue.Add(new Categorize { text = "Ultimate 2", id = 2, color = "#7f993e", fontColor = "#ffffff" });
            categorizeValue.Add(new Categorize { text = "Ultimate 3", id = 3, color = "#cc8638", fontColor = "#ffffff" });
            categorizeValue.Add(new Categorize { text = "Ultimate 4", id = 4, color = "#ab54a0", fontColor = "#ffffff" });
            categorizeValue.Add(new Categorize { text = "Counselling 1", id = 5, color = "#dd654e", fontColor = "#ffffff" });
            categorizeValue.Add(new Categorize { text = "Aliens", id = 6, color = "#d0af2b", fontColor = "#ffffff" });
            return categorizeValue;
        }

        public static List<ResourceFields> LoadOwnerDetails(List<ResourceFields> Rooms)
        {
            List<ResourceFields> Owners = new List<ResourceFields>();
            //https://help.syncfusion.com/aspnet-core/schedule/resources
            for (int i = 1; i < Rooms.Count + 1; i++)
            {
                string RoomCount = i.ToString();


                Owners.Add(new ResourceFields { Text = "Ultimate 1", Id = "2", GroupId = RoomCount, Color = "#f8a398" });
                Owners.Add(new ResourceFields { Text = "Ultimate 2", Id = "3", GroupId = RoomCount, Color = "#7499e1" });
                Owners.Add(new ResourceFields { Text = "Ultimate 3", Id = "5", GroupId = RoomCount, Color = "#f8a398" });
                Owners.Add(new ResourceFields { Text = "Ultimate 4", Id = "6", GroupId = RoomCount, Color = "#7499e1" });
                Owners.Add(new ResourceFields { Text = "Counselling 1", Id = "1", GroupId = RoomCount, Color = "#ffaa00" });
                Owners.Add(new ResourceFields { Text = "Counselling 2", Id = "4", GroupId = RoomCount, Color = "#ffaa00" });
            }

            return Owners;
        }

    }
}

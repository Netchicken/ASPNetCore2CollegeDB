using System.Collections.Generic;
using ASPSyncCollegeRoom2018.Models;
using Syncfusion.JavaScript.Models;

namespace ASPSyncCollegeRoom2018.Business
{
    public interface IHomeControllerActions
    {
        ResourceFields ChooseRoomFromButtonClick(string room);
        List<ResourceFields> LoadRoomDetails();
        List<Categorize> LoadCategoryValue();
        List<ResourceFields> LoadOwnerDetails(List<ResourceFields> Rooms);
    }
}
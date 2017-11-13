using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IAppUserDataAccess
    {
        List<FoodTruckBasicInfo> UpdateAppUserLocation(int appUserId, bool isPushNotification, decimal latitude, decimal longitude);
        int CreateAppUser(User user, decimal latitude, decimal longitude);
    }
}

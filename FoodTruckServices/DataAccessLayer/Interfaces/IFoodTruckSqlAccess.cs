using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IFoodTruckSqlAccess
    {
        FoodTruck GetFoodTruckById(int foodTruckId);

        int CreateFoodTruck(FoodTruck foodTruck, int userId);

        void UpdateFoodTruck(FoodTruck foodTruck, int userId);

        FoodTruck SearchFoodTruck(FoodTruckSearchCriteria criteria);

        void DeactivateFoodTruck(int id, int userId);

        #region App
        void ActivateFoodTruck(int foodTruckUserId, decimal latitude, decimal longitude);
        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class FoodTruckSqlAccessImplementation : IFoodTruckSqlAccess
    {
        public int CreateFoodTruck(FoodTruck foodTruck, int userId)
        {
            throw new NotImplementedException();
        }

        public void DeactivateFoodTruck(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public FoodTruck GetFoodTruckById(int foodTruckId)
        {
            throw new NotImplementedException();
        }

        public FoodTruck SearchFoodTruck(FoodTruckSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public void UpdateFoodTruck(FoodTruck foodTruck, int userId)
        {
            throw new NotImplementedException();
        }
    }
}

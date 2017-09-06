﻿using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IFoodTruckCompanySqlAccess
    {
        FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId);

        int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany);

        void InsertWorkDayHour(WorkingDayHour workingDayHour);

        void UpdateFoodTruckCompany(FoodTruckCompany foodTruckCompany);

        List<FoodTruckCompany> SearchFoodTruckCompany(FoodTruckCompanySearchCriteria criteria);
        
           void DeactivateFoodTruckCompany(int foodTruckCompanyId);
    }
}

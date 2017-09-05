using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IDataAccess
    {
        FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId);

        int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany);

        void InsertWorkDayHour(WorkingDayHour workingDayHour);
    }
}

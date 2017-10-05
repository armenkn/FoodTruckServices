using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IFoodTruckCompanySqlAccess
    {
        FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId);

        int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany, int userId);

        void InsertWorkDayHour(WorkingDayHour workingDayHour, int userId);

        void UpdateFoodTruckCompany(FoodTruckCompany foodTruckCompany, int userId);

        List<FoodTruckCompany> SearchFoodTruckCompany(FoodTruckCompanySearchCriteria criteria);
    }
}

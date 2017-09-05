using FoodTruckServices.Model;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class DataAccessImplementation : IDataAccess
    {
        private readonly IFoodTruckCompanySqlAccess _foodTruckCompanySqlAccess;

        public DataAccessImplementation(IFoodTruckCompanySqlAccess foodTruckCompanySqlAccess)
        {
            _foodTruckCompanySqlAccess = foodTruckCompanySqlAccess;
        }

        public int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany)
        {
            return _foodTruckCompanySqlAccess.CreateFoodTruckCompany(foodTruckCompany);
        }

        public FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId)
        {
            if (foodTruckCompanyId == 0)
                return null;

            return _foodTruckCompanySqlAccess.GetFoodTruckCompanyById(foodTruckCompanyId);
        }

        public void InsertWorkDayHour(WorkingDayHour workingDayHour)
        {
            _foodTruckCompanySqlAccess.InsertWorkDayHour(workingDayHour);
        }
    }
}

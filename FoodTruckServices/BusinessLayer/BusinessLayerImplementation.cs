using System.Collections.Generic;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;
using FoodTruckServices.DataAccessLayer;

namespace FoodTruckServices.BusinessLayer.Implementations
{
    public class BusinessLayerImplementation : IBusiness
    {
        private readonly IFoodTruckCompanySqlAccess _foodTruckCompanySqlAccess;
        private readonly IFoodTruckSqlAccess _foodTruckSqlAccess;
        private readonly IAddressSqlAccess _addressSqlAccess;
        private readonly ICoordinationServiceProvider _coordinationServiceProvider;
        public BusinessLayerImplementation(IFoodTruckCompanySqlAccess foodTruckCompanySqlAccess, IFoodTruckSqlAccess foodTruckSqlAccess, IAddressSqlAccess addressSqlAccess, ICoordinationServiceProvider coordinationServiceProvider)
        {
            _foodTruckCompanySqlAccess = foodTruckCompanySqlAccess;
            _foodTruckSqlAccess = foodTruckSqlAccess;
            _addressSqlAccess = addressSqlAccess;
            _coordinationServiceProvider = coordinationServiceProvider;
        }

        public void InsertWorkDayHour(WorkingDayHour workingDayHour)
        {
            _foodTruckCompanySqlAccess.InsertWorkDayHour(workingDayHour);
        }


        #region Food Truck Company
        public int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany)
        {
            return _foodTruckCompanySqlAccess.CreateFoodTruckCompany(foodTruckCompany);
        }

        public void DeactivateFoodTruckCompany(int foodTruckCompanyId)
        {
            _foodTruckCompanySqlAccess.DeactivateFoodTruckCompany(foodTruckCompanyId);
        }

        public FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId)
        {
            if (foodTruckCompanyId == 0)
                return null;

            return _foodTruckCompanySqlAccess.GetFoodTruckCompanyById(foodTruckCompanyId);
        }


        public List<FoodTruckCompany> SearchFoodTruckCompany(FoodTruckCompanySearchCriteria criteria)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateFoodTruckCompany(FoodTruckCompany foodTruckCompany)
        {
            _foodTruckCompanySqlAccess.UpdateFoodTruckCompany(foodTruckCompany);
        }
        #endregion

        #region Food Truck
        public FoodTruck GetFoodTruckById(int foodTruckId)
        {
            return _foodTruckSqlAccess.GetFoodTruckById(foodTruckId);
        }

        public int CreateFoodTruck(FoodTruck foodTruck)
        {
            return _foodTruckSqlAccess.CreateFoodTruck(foodTruck);
        }

        public void UpdateFoodTruck(FoodTruck foodTruck)
        {
            _foodTruckSqlAccess.UpdateFoodTruck(foodTruck);
        }

        public FoodTruck SearchFoodTruck(FoodTruckSearchCriteria criteria)
        {
            return _foodTruckSqlAccess.SearchFoodTruck(criteria);
        }

        public void DeactivateFoodTruck(int id)
        {
            _foodTruckSqlAccess.DeactivateFoodTruck(id);
        }
        #endregion

        #region Address
        public Address GetAddressById(int addressId)
        {
            return _addressSqlAccess.GetAddressById(addressId);
        }

        public int CreateAddress(Address address)
        {
            if(address.Coordination?.Latitude == 0 && address.Coordination?.Longitude == 0)
            {
                var coordination = _coordinationServiceProvider.GetLatAndLongByAddress(address);
                address.Coordination = coordination;
            }
            return _addressSqlAccess.CreateAddress(address);
        }

        public void UpdateAddress(Address address)
        {
            _addressSqlAccess.UpdateAddress(address);
        }

        #endregion
    }
}

﻿using System.Collections.Generic;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;
using FoodTruckServices.DataAccessLayer;
using System.Threading.Tasks;

namespace FoodTruckServices
{
    public class BusinessLayerImplementation : IBusiness
    {
        private readonly IFoodTruckCompanySqlAccess _foodTruckCompanySqlAccess;
        private readonly IFoodTruckSqlAccess _foodTruckSqlAccess;
        private readonly IAddressSqlAccess _addressSqlAccess;
        private readonly ICoordinationServiceProvider _coordinationServiceProvider;
        public BusinessLayerImplementation(IFoodTruckCompanySqlAccess foodTruckCompanySqlAccess, 
            IFoodTruckSqlAccess foodTruckSqlAccess, IAddressSqlAccess addressSqlAccess,
            ICoordinationServiceProvider coordinationServiceProvider)
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
        
        public FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId)
        {
            if (foodTruckCompanyId == 0)
                return null;

            return _foodTruckCompanySqlAccess.GetFoodTruckCompanyById(foodTruckCompanyId);
        }


        public List<FoodTruckCompany> SearchFoodTruckCompany(FoodTruckCompanySearchCriteria criteria)
        {
            return _foodTruckCompanySqlAccess.SearchFoodTruckCompany(criteria);
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

        public async Task<int> CreateAddress(Address address)
        {
            if (address.Coordination == null ||
                (address.Coordination?.Latitude == null && address.Coordination?.Longitude == null))
            {
                var coordination = _coordinationServiceProvider.GetLatAndLongByAddress(address);
                address.Coordination = await coordination;
            }
            return _addressSqlAccess.CreateAddress(address);
        }

        public async void UpdateAddress(Address address)
        {
            if (address.Coordination == null ||
                (address.Coordination?.Latitude == null && address.Coordination?.Longitude == null))
            {
                var coordination = _coordinationServiceProvider.GetLatAndLongByAddress(address);
                address.Coordination = await coordination;
            }
            _addressSqlAccess.UpdateAddress(address);
        }

        public DatabaseResponse DeleteAddress(int id)
        {
            return _addressSqlAccess.DeleteAddress(id);
        }

        #endregion
    }
}

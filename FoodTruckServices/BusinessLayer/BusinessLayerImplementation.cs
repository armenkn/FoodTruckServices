using System.Collections.Generic;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;
using FoodTruckServices.DataAccessLayer;
using System.Threading.Tasks;
using System;

namespace FoodTruckServices
{
    public class BusinessLayerImplementation : IBusiness
    {
        private readonly IFoodTruckCompanySqlAccess _foodTruckCompanySqlAccess;
        private readonly IFoodTruckSqlAccess _foodTruckSqlAccess;
        private readonly IAddressSqlAccess _addressSqlAccess;
        private readonly ICoordinationServiceProvider _coordinationServiceProvider;
        private readonly IContactSqlAccess _contactSqlAccess;
        private readonly IUserSqlAccess _userSqlAccess;

        public BusinessLayerImplementation(IFoodTruckCompanySqlAccess foodTruckCompanySqlAccess, 
            IFoodTruckSqlAccess foodTruckSqlAccess, IAddressSqlAccess addressSqlAccess,
            ICoordinationServiceProvider coordinationServiceProvider,
            IContactSqlAccess contactSqlAccess,
            IUserSqlAccess userSqlAccess)
        {
            _foodTruckCompanySqlAccess = foodTruckCompanySqlAccess;
            _foodTruckSqlAccess = foodTruckSqlAccess;
            _addressSqlAccess = addressSqlAccess;
            _coordinationServiceProvider = coordinationServiceProvider;
            _contactSqlAccess = contactSqlAccess;
            _userSqlAccess = userSqlAccess;
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

        public int CreateContact(ContactInfo contact)
        {
            return _contactSqlAccess.CreateContact(contact);
        }

        public ContactInfo GetContactById(int contactId)
        {
            return _contactSqlAccess.GetContactById(contactId);
        }

        public void UpdateContact(ContactInfo contact)
        {
            _contactSqlAccess.UpdateContact(contact);
        }

        public int CreateUser(User user)
        {
            return _userSqlAccess.CreateUser(user);
        }

        public User GetUserById(int userId)
        {
            return _userSqlAccess.GetUserById(userId);
        }

        public void UpdateUser(User user)
        {
            _userSqlAccess.UpdateUser(user);
        }

        public List<User> GetUsersByRoleId(int userRoleId)
        {
            List<User> result = null;
            try
            {
                var userRole = (UserRoleEnum)userRoleId;
                result = _userSqlAccess.GetUserListByUserRole(userRole);
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        public UserLoginResultEnum Login(string username, string password)
        {
            //Todo: Hash Password
            var hashedPassword = password;
            var loginResult = _userSqlAccess.Login(username, hashedPassword);
            if (loginResult.Item1 != UserLoginResultEnum.Success || 
                loginResult.Item2 == 0)
                return UserLoginResultEnum.InvalidCredentials;

            var accessToken = Guid.NewGuid().ToString();
            var refreshToken = Guid.NewGuid().ToString();
            var accessTokenExpirationDate = DateTime.Now.AddMinutes(Constants.Tokens.AccessTokenExpirationMinutes);
            var refreshTokenExpirationDate = DateTime.Now.AddMinutes(Constants.Tokens.RefreshTokenExpirationMinutes);

            var insertTokenResponse = _userSqlAccess.InsertTokensForUser(loginResult.Item2, accessToken, refreshToken, accessTokenExpirationDate, refreshTokenExpirationDate);

            if (insertTokenResponse != DatabaseResponse.Success)
                return UserLoginResultEnum.UnableToLogin;
            return UserLoginResultEnum.Success;
        }

        #endregion
    }
}

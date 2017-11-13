using System.Collections.Generic;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;
using FoodTruckServices.DataAccessLayer;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using FoodTruckServices.Model.Exceptions;

namespace FoodTruckServices
{
    public class BusinessLayerImplementation : IBusiness
    {
        private readonly IFoodTruckCompanySqlAccess _foodTruckCompanySqlAccess;
        private readonly IFoodTruckSqlAccess _foodTruckSqlAccess;
        private readonly IAddressSqlAccess _addressSqlAccess;
        private readonly ICoordinationServiceProvider _coordinationServiceProvider;
        private readonly IContactSqlAccess _contactSqlAccess;
        private readonly IUserDataAccess _userSqlAccess;
        private readonly IAppUserDataAccess _appUserDataAccess;
        private readonly ITokenProvider _tokenProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AuthenticatedUser _authenticatedUser
        {
            get
            {
                return (AuthenticatedUser)_httpContextAccessor.HttpContext.Items.FirstOrDefault(x => x.Key == Constants.Tokens.UserInfo).Value;
            }
        }

        public BusinessLayerImplementation(IHttpContextAccessor httpContextAccessor,
            IFoodTruckCompanySqlAccess foodTruckCompanySqlAccess,
            IFoodTruckSqlAccess foodTruckSqlAccess, IAddressSqlAccess addressSqlAccess,
            ICoordinationServiceProvider coordinationServiceProvider,
            IContactSqlAccess contactSqlAccess,
            IUserDataAccess userSqlAccess,
            ITokenProvider tokenProvider,
            IAppUserDataAccess appUserDataAccess)
        {
            _foodTruckCompanySqlAccess = foodTruckCompanySqlAccess;
            _foodTruckSqlAccess = foodTruckSqlAccess;
            _addressSqlAccess = addressSqlAccess;
            _coordinationServiceProvider = coordinationServiceProvider;
            _contactSqlAccess = contactSqlAccess;
            _userSqlAccess = userSqlAccess;
            _tokenProvider = tokenProvider;
            _httpContextAccessor = httpContextAccessor;
            _appUserDataAccess = appUserDataAccess;
        }



        public void InsertWorkDayHour(WorkingDayHour workingDayHour)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            _foodTruckCompanySqlAccess.InsertWorkDayHour(workingDayHour, _authenticatedUser.UserId);
        }


        #region Food Truck Company
        public int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _foodTruckCompanySqlAccess.CreateFoodTruckCompany(foodTruckCompany, _authenticatedUser.UserId);
        }

        public FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            if (foodTruckCompanyId == 0)
                return null;

            return _foodTruckCompanySqlAccess.GetFoodTruckCompanyById(foodTruckCompanyId);
        }

        public List<FoodTruckCompany> SearchFoodTruckCompany(FoodTruckCompanySearchCriteria criteria)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _foodTruckCompanySqlAccess.SearchFoodTruckCompany(criteria);
        }

        public void UpdateFoodTruckCompany(FoodTruckCompany foodTruckCompany)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            _foodTruckCompanySqlAccess.UpdateFoodTruckCompany(foodTruckCompany, _authenticatedUser.UserId);
        }
        #endregion

        #region Food Truck
        public FoodTruck GetFoodTruckById(int foodTruckId)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _foodTruckSqlAccess.GetFoodTruckById(foodTruckId);
        }

        public int CreateFoodTruck(FoodTruck foodTruck)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _foodTruckSqlAccess.CreateFoodTruck(foodTruck, _authenticatedUser.UserId);
        }

        public void UpdateFoodTruck(FoodTruck foodTruck)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            _foodTruckSqlAccess.UpdateFoodTruck(foodTruck, _authenticatedUser.UserId);
        }

        public FoodTruck SearchFoodTruck(FoodTruckSearchCriteria criteria)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _foodTruckSqlAccess.SearchFoodTruck(criteria);
        }

        public void DeactivateFoodTruck(int id)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            _foodTruckSqlAccess.DeactivateFoodTruck(id, _authenticatedUser.UserId);
        }
        #endregion

        #region Address
        public Address GetAddressById(int addressId)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _addressSqlAccess.GetAddressById(addressId);
        }

        public async Task<int> CreateAddress(Address address)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            if (address.Coordination == null ||
                (address.Coordination?.Latitude == null && address.Coordination?.Longitude == null))
            {
                var coordination = _coordinationServiceProvider.GetLatAndLongByAddress(address);
                address.Coordination = await coordination;
            }
            return _addressSqlAccess.CreateAddress(address, _authenticatedUser.UserId);
        }

        public async Task UpdateAddress(Address address)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            if (address.Coordination == null ||
                (address.Coordination?.Latitude == null && address.Coordination?.Longitude == null))
            {
                var coordination = _coordinationServiceProvider.GetLatAndLongByAddress(address);
                address.Coordination = await coordination;
            }
            _addressSqlAccess.UpdateAddress(address, _authenticatedUser.UserId);
        }

        public DatabaseResponse DeleteAddress(int id)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _addressSqlAccess.DeleteAddress(id, _authenticatedUser.UserId);
        }

        public int CreateContact(ContactInfo contact)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _contactSqlAccess.CreateContact(contact, _authenticatedUser.UserId);
        }

        public ContactInfo GetContactById(int contactId)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _contactSqlAccess.GetContactById(contactId);
        }

        public void UpdateContact(ContactInfo contact)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            _contactSqlAccess.UpdateContact(contact, _authenticatedUser.UserId);
        }

        public int CreateUser(User user)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _userSqlAccess.CreateUser(user, _authenticatedUser.UserId);
        }

        public User GetUserById(int userId)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            return _userSqlAccess.GetUserById(userId);
        }

        public void UpdateUser(User user)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            _userSqlAccess.UpdateUser(user, _authenticatedUser.UserId);
        }

        public List<User> GetUsersByRoleId(int userRoleId)
        {
            if (_authenticatedUser.Role != UserRoleEnum.Admin)
                throw new AuthenticationException("Invalid Role");

            List<User> result = null;
            try
            {
                var userRole = (UserRoleEnum)userRoleId;
                result = _userSqlAccess.GetUserListByUserRole(userRole);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public UserLoginResponse Login(string username, string password)
        {
            //Todo: Hash Password
            var result = new UserLoginResponse()
            {
                LoginResult = UserLoginResultEnum.None
            };

            var hashedPassword = password;
            var loginResult = _userSqlAccess.Login(username, hashedPassword);

            if (loginResult.Item1 != UserLoginResultEnum.Success ||
                loginResult.Item2 == null || loginResult.Item2.UserId == 0)
            {
                result.LoginResult = UserLoginResultEnum.InvalidCredentials;
                return result;
            }

            var accessToken = _tokenProvider.CreateToken(
                loginResult.Item2,
                _userSqlAccess.GetTokenProviderSecret(Constants.Tokens.Providers.AdminTokenProvider));

            var refreshToken = Guid.NewGuid().ToString();

            var accessTokenExpirationDate = DateTime.Now.AddMinutes(Constants.Tokens.AccessTokenExpirationMinutes);
            var refreshTokenExpirationDate = DateTime.Now.AddMinutes(Constants.Tokens.RefreshTokenExpirationMinutes);

            var insertTokenDatabaseResponse = _userSqlAccess.InsertTokensForUser(
                                                                loginResult.Item2.UserId, accessToken, refreshToken,
                                                                accessTokenExpirationDate, refreshTokenExpirationDate);

            if (insertTokenDatabaseResponse == DatabaseResponse.Success)
            {
                result.LoginResult = UserLoginResultEnum.Success;
                result.JWT = accessToken;
                result.UserId = loginResult.Item2.UserId;

                return result;
            }
            else
                return result;
        }

        public Tuple<AuthenticatedUser, TokenResponseEnum> ValidateToken(string authHeader)
        {
            return _tokenProvider.ValidateToken(authHeader, _userSqlAccess.GetTokenProviderSecret(Constants.Tokens.Providers.AdminTokenProvider));
        }
        #endregion

        #region App
        public void ActivateFoodTruck(decimal latitude, decimal longitude)
        {
            var foodTruckUserId = _authenticatedUser.UserId;
            _foodTruckSqlAccess.ActivateFoodTruck(foodTruckUserId, latitude, longitude);
        }

        public List<FoodTruckBasicInfo> UpdateAppUserLocation(bool isPushNotification, decimal latitude, decimal longitude)
        {
            var appUserId = _authenticatedUser.UserId;
            return _appUserDataAccess.UpdateAppUserLocation(appUserId, isPushNotification, latitude, longitude);

        }

        public int CreateAppUser(User user, Coordination coordination)
        {
            return _appUserDataAccess.CreateAppUser(user, coordination.Latitude, coordination.Longitude);
        }

        #endregion
    }
}

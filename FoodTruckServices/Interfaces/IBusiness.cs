using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace FoodTruckServices.Interfaces
{
    public interface IBusiness
    {
        FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId);
        int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany);
        void InsertWorkDayHour(WorkingDayHour workingDayHour);
        void UpdateFoodTruckCompany(FoodTruckCompany foodTruckCompany);
        List<FoodTruckCompany> SearchFoodTruckCompany(FoodTruckCompanySearchCriteria criteria);

        FoodTruck GetFoodTruckById(int foodTruckId);
        int CreateFoodTruck(FoodTruck foodTruck);
        void UpdateFoodTruck(FoodTruck foodTruck);
        FoodTruck SearchFoodTruck(FoodTruckSearchCriteria criteria);
        void DeactivateFoodTruck(int foodTruckId);
        

        Address GetAddressById(int addressId);
        Task<int> CreateAddress(Address address);        
        Task UpdateAddress(Address address);
        DatabaseResponse DeleteAddress(int id);
        int CreateContact(ContactInfo contact);
        ContactInfo GetContactById(int contactId);
        void UpdateContact(ContactInfo contact);


        int CreateUser(User user);
        User GetUserById(int userId);
        void UpdateUser(User user);
        List<User> GetUsersByRoleId(int userRoleId);
        UserLoginResponse Login(string username, string password);
        Tuple<AuthenticatedUser, TokenResponseEnum> ValidateToken(string token);
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class AppUserDataAccess : IAppUserDataAccess
    {
        private readonly IUserDataAccess _userDataAccess;

        public AppUserDataAccess(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public int CreateAppUser(User user, decimal latitude, decimal longitude)
        {
            var appUserID = _userDataAccess.CreateUser(user, Constants.DefaultUserId);
            if (appUserID == 0)
                return appUserID;

            if (latitude != 0 && longitude != 0)
                UpdateAppUserLocation(appUserID, true, latitude, longitude);

            return appUserID;
        }

        public List<FoodTruckBasicInfo> UpdateAppUserLocation(int appUserId, bool isPushNotification, decimal latitude, decimal longitude)
        {
            List<FoodTruckBasicInfo> result = null;

            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "UpdateAppUserLocation";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", appUserId);
                    cmd.Parameters.AddWithValue("@Latitude", latitude);
                    cmd.Parameters.AddWithValue("@Longitude", longitude);
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            if (isPushNotification)
                using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
                {
                    var spName = "GetActiveFoodTrucksNearUser";
                    using (var cmd = new SqlCommand(spName, sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", appUserId);
                        cmd.Parameters.AddWithValue("@RadiusMiles", Constants.DefaultMapRadiusMiles);
                        sqlConn.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.HasRows)
                        {
                            if (result == null)
                                result = new List<FoodTruckBasicInfo>();

                            var foodTruck = new FoodTruckBasicInfo();
                            foodTruck.FoodTruckID = int.Parse(reader["FoodTruckId"].ToString());
                            foodTruck.Name = reader["FoodTruckName"].ToString();
                            foodTruck.TruckMake = reader["FoodTruckTruckMake"].ToString();
                            foodTruck.TruckModel = reader["FoodTruckTruckModel"].ToString();
                            foodTruck.Year = int.Parse(reader["FoodTruckYear"].ToString());
                            foodTruck.Color = reader["FoodTruckColor"].ToString();
                            foodTruck.CookInfo = new PersonalInfo();

                            if (reader["FoodTruckCookID"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookID"].ToString()))
                                foodTruck.CookInfo.PersonalInfoID = int.Parse(reader["FoodTruckCookID"].ToString());
                            if (reader["FoodTruckCookFirstName"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookFirstName"].ToString()))
                                foodTruck.CookInfo.FirstName = reader["FoodTruckCookFirstName"].ToString();
                            if (reader["FoodTruckCookMiddleName"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookMiddleName"].ToString()))
                                foodTruck.CookInfo.MiddleName = reader["FoodTruckCookMiddleName"].ToString();
                            if (reader["FoodTruckCookLastName"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookLastName"].ToString()))
                                foodTruck.CookInfo.LastName = reader["FoodTruckCookLastName"].ToString();
                            if (reader["FoodTruckCookPersonalInfoID"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookPersonalInfoID"].ToString()))
                                foodTruck.CookInfo.PersonalInfoID = int.Parse(reader["FoodTruckCookPersonalInfoID"].ToString());
                            if (reader["FoodTruckCookRoleId"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookRoleId"].ToString()))
                                foodTruck.CookInfo.Role = (PersonRoleEnum)int.Parse(reader["FoodTruckCookRoleId"].ToString());
                            if (reader["FoodTruckCookSSN"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookSSN"].ToString()))
                                foodTruck.CookInfo.SSN = reader["FoodTruckCookSSN"].ToString();
                            if (reader["FoodTruckCookDateOfBirth"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookDateOfBirth"].ToString()))
                                foodTruck.CookInfo.DateOfBirth = DateTime.Parse(reader["FoodTruckCookDateOfBirth"].ToString());
                            foodTruck.CuisineCategory = (CuisineCategoryEnum)int.Parse(reader["FoodTruckCuisineCategoryId"].ToString());
                            foodTruck.Description = reader["FoodTruckDescription"].ToString();
                            foodTruck.Driver = new PersonalInfo();
                            if (reader["FoodTruckDriverID"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverID"].ToString()))
                                foodTruck.Driver.PersonalInfoID = int.Parse(reader["FoodTruckDriverID"].ToString());
                            if (reader["FoodTruckDriverFirstName"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverFirstName"].ToString()))
                                foodTruck.Driver.FirstName = reader["FoodTruckDriverFirstName"].ToString();
                            if (reader["FoodTruckDriverMiddleName"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverMiddleName"].ToString()))
                                foodTruck.Driver.MiddleName = reader["FoodTruckDriverMiddleName"].ToString();
                            if (reader["FoodTruckDriverLastName"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverLastName"].ToString()))
                                foodTruck.Driver.LastName = reader["FoodTruckDriverLastName"].ToString();
                            if (reader["FoodTruckDriverPersonalInfoID"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverPersonalInfoID"].ToString()))
                                foodTruck.Driver.PersonalInfoID = int.Parse(reader["FoodTruckDriverPersonalInfoID"].ToString());
                            if (reader["FoodTruckDriverRoleId"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverRoleId"].ToString()))
                                foodTruck.Driver.Role = (PersonRoleEnum)int.Parse(reader["FoodTruckDriverRoleId"].ToString());
                            if (reader["FoodTruckDriverSSN"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverSSN"].ToString()))
                                foodTruck.Driver.SSN = reader["FoodTruckDriverSSN"].ToString();
                            if (reader["FoodTruckDriverDateOfBirth"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverDateOfBirth"].ToString()))
                                foodTruck.Driver.DateOfBirth = DateTime.Parse(reader["FoodTruckDriverDateOfBirth"].ToString());
                            foodTruck.LicensePlate = reader["FoodTruckLicensePlate"].ToString();
                            foodTruck.MaxCapacityPerMeal = int.Parse(reader["FoodTruckMaxCapacityPerMeal"].ToString());
                            foodTruck.MealType = (MealTypeEnum)int.Parse(reader["FoodTruckMealTypeId"].ToString());
                            result.Add(foodTruck);
                        }
                    }
                }

            return result;
        }
    }
}

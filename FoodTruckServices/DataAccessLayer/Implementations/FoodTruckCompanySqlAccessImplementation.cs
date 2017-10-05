using System;
using FoodTruckServices.Model;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class FoodTruckCompanySqlAccessImplementation : IFoodTruckCompanySqlAccess
    {
        public FoodTruckCompanySqlAccessImplementation()
        {
        }

        public int CreateFoodTruckCompany(FoodTruckCompany foodTruckCompany, int userId)
        {
            var result = 0;
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "InsertFoodTruckCompany";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BusinessName", foodTruckCompany.BusinessName);
                    cmd.Parameters.AddWithValue("@Description", foodTruckCompany.Description);
                    cmd.Parameters.AddWithValue("@CompanyTypeID", (int)foodTruckCompany.CompanyType);
                    cmd.Parameters.AddWithValue("@PermitNumber", foodTruckCompany.PermitNumber);
                    cmd.Parameters.AddWithValue("@HealthCode", foodTruckCompany.HealthCode);
                    cmd.Parameters.AddWithValue("@HasCatering", foodTruckCompany.HasCatering);
                    cmd.Parameters.AddWithValue("@HasVegeterianFood", foodTruckCompany.HasVegeterianFood);
                    cmd.Parameters.AddWithValue("@HasVigenFood", foodTruckCompany.HasVigenFood);
                    if (foodTruckCompany.AdditionalInfo != null && foodTruckCompany.AdditionalInfo.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@AdditionalInfo", Utilities.SerializeObjectToJson(foodTruckCompany.AdditionalInfo));
                    }
                    if (foodTruckCompany.AreaOfOperation != null && foodTruckCompany.AreaOfOperation.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@AreaOfOperation", foodTruckCompany.AreaOfOperation);
                    }
                    if (foodTruckCompany.OfficeLocations != null && foodTruckCompany.OfficeLocations.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@LocationIDs", string.Join(",", foodTruckCompany.OfficeLocations.Select(x => x.LocationID).ToList()));
                    }
                    if (foodTruckCompany.Contacts != null && foodTruckCompany.Contacts.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@ContactInfoXmlString", Utilities.SerializeObjectToXml(foodTruckCompany.Contacts));
                    }
                    if (foodTruckCompany.OwnerInfo != null)
                    {
                        cmd.Parameters.AddWithValue("@OwnerID", foodTruckCompany.OwnerInfo.PersonalInfoID);
                    }
                    if (foodTruckCompany.FoodTrucks != null && foodTruckCompany.FoodTrucks.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@FoodTruckIDs", string.Join(",", foodTruckCompany.FoodTrucks.Select(x => x.FoodTruckID).ToList()));
                    }
                    if (foodTruckCompany.MealTypes != null && foodTruckCompany.MealTypes.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@MealTypeIDs", string.Join(",", foodTruckCompany.MealTypes.Select(x => (int)x).ToList()));
                    }
                    if (foodTruckCompany.CuisineCategories != null && foodTruckCompany.CuisineCategories.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuisineCategoryIDs", string.Join(",", foodTruckCompany.CuisineCategories.Select(x => (int)x).ToList()));
                    }

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(returnValue);

                    //var a = "";
                    //for (int i = 0; i < cmd.Parameters.Count; i++)
                    //{
                    //    if(cmd.Parameters[i].ParameterName != "@ReturnValue")
                    //        a += cmd.Parameters[i].ParameterName + " = '" + cmd.Parameters[i].Value.ToString() + "'," + Environment.NewLine;
                    //}
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    result = int.Parse(returnValue.Value.ToString());
                }
            }
            return result;
        }

        public FoodTruckCompany GetFoodTruckCompanyById(int foodTruckCompanyId)
        {

            var result = new FoodTruckCompany();
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "GetFoodTruckCompanyById";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FoodTruckCompanyID", foodTruckCompanyId);
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    ReadFoodTrackCompanyInfoFromReader(result, reader);
                }
            }

            return result;
        }

        public void InsertWorkDayHour(WorkingDayHour workingDayHour, int userId)
        {          

            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "InsertWorkingDayHour";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DayOfWeek", workingDayHour.DayOfWeek.ToString());
                    cmd.Parameters.AddWithValue("@OpenTimeHours", workingDayHour.OpenTime.Hours);
                    cmd.Parameters.AddWithValue("@OpenTimeMinutes", workingDayHour.OpenTime.Minutes);
                    cmd.Parameters.AddWithValue("@CloseTimeHours", workingDayHour.CloseTime.Hours);
                    cmd.Parameters.AddWithValue("@CloseTimeMinutes", workingDayHour.CloseTime.Minutes);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public List<FoodTruckCompany> SearchFoodTruckCompany(FoodTruckCompanySearchCriteria criteria)
        {
            var result = new List<FoodTruckCompany>();
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "SearchFoodTruckCompanies";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessName", criteria.BusinessName);
                    cmd.Parameters.AddWithValue("@Zipcode", criteria.Zipcode);
                    cmd.Parameters.AddWithValue("@City", criteria.City);
                    if (criteria.Latitude != 0)
                        cmd.Parameters.AddWithValue("@Latitude", criteria.Latitude);
                    if (criteria.Longitude != 0)
                        cmd.Parameters.AddWithValue("@Longiture", criteria.Longitude);
                    if (criteria.Radius != 0)
                        cmd.Parameters.AddWithValue("@Radius", criteria.Radius);

                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.HasRows)
                    {
                        var ftcItem = new FoodTruckCompany();
                        ReadFoodTrackCompanyInfoFromReader(ftcItem, reader);
                        result.Add(ftcItem);
                    }
                }
            }

            return result;
        }

        public void UpdateFoodTruckCompany(FoodTruckCompany foodTruckCompany, int userId)
        {
            var result = new List<FoodTruckCompany>();
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "UpdateFoodTruckCompany";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FoodTruckCompanyID", foodTruckCompany.FoodTruckCompanyId);
                    cmd.Parameters.AddWithValue("@BusinessName", foodTruckCompany.BusinessName);
                    cmd.Parameters.AddWithValue("@Description", foodTruckCompany.Description);
                    cmd.Parameters.AddWithValue("@CompanyTypeID", (int)foodTruckCompany.CompanyType);
                    cmd.Parameters.AddWithValue("@PermitNumber", foodTruckCompany.PermitNumber);
                    cmd.Parameters.AddWithValue("@HealthCode", foodTruckCompany.HealthCode);
                    cmd.Parameters.AddWithValue("@HasCatering", foodTruckCompany.HasCatering);
                    cmd.Parameters.AddWithValue("@HasVegeterianFood", foodTruckCompany.HasVegeterianFood);
                    cmd.Parameters.AddWithValue("@HasVigenFood", foodTruckCompany.HasVigenFood);
                    if (foodTruckCompany.AdditionalInfo != null && foodTruckCompany.AdditionalInfo.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@AdditionalInfo", Utilities.SerializeObjectToJson(foodTruckCompany.AdditionalInfo));
                    }
                    if (foodTruckCompany.AreaOfOperation != null && foodTruckCompany.AreaOfOperation.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@AreaOfOperation", foodTruckCompany.AreaOfOperation);
                    }
                    if (foodTruckCompany.MealTypes != null && foodTruckCompany.MealTypes.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@MealTypeIDs", string.Join(",", foodTruckCompany.MealTypes.Select(x => (int)x).ToList()));
                    }
                    if (foodTruckCompany.CuisineCategories != null && foodTruckCompany.CuisineCategories.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuisineCategoryIDs", string.Join(",", foodTruckCompany.CuisineCategories.Select(x => (int)x).ToList()));
                    }
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }


        #region Private methods
        private static void ReadFoodTrackCompanyInfoFromReader(FoodTruckCompany result, SqlDataReader reader)
        {
            while (reader.Read())
            {
                result.FoodTruckCompanyId = int.Parse(reader["FoodTruckCompanyId"].ToString());
                result.BusinessName = reader["BusinessName"].ToString();
                result.PermitNumber = reader["FoodTruckCompanyPermitNumber"].ToString();
                result.HealthCode = reader["FoodTruckCompanyHealthCode"].ToString();
                result.HasCatering = bool.Parse(reader["HasCatering"].ToString());
                result.HasVegeterianFood = bool.Parse(reader["HasVegeterianFood"].ToString());
                result.HasVigenFood = bool.Parse(reader["HasVigenFood"].ToString());
                result.Description = reader["FoodTruckCompanyDescription"].ToString();
                //result.AdditionalInfo= reader["FoodTruckCompanyAdditionalInfoString"].ToString();
                //result.AreaOfOperation= reader["FoodTruckCompanyAreaOfOperationString"].ToString();
                result.CompanyType = (CompanyTypeEnum)int.Parse(reader["CompanyTypeID"].ToString());
                var ownerInfo = new PersonalInfo()
                {
                    PersonalInfoID = int.Parse(reader["OwnerId"].ToString()),
                    FirstName = reader["OwnerFirstName"].ToString(),
                    MiddleName = reader["OwnerMiddleName"].ToString(),
                    LastName = reader["OwnerLastName"].ToString(),
                    SSN = reader["OwnerSSN"].ToString(),
                    //DateOfBirth = string.IsNullOrEmpty(reader["OwnerDateOfBirth"].ToString()) ? 
                    //            DateTime.Parse(reader["OwnerDateOfBirth"].ToString()) : null,
                    Role = (PersonRoleEnum)(int.Parse(reader["OwnerRoleID"].ToString()))
                };
            result.OwnerInfo = ownerInfo;
        }

        reader.NextResult();

            result.OfficeLocations = new List<Location>();

            while (reader.Read())
            {
                var location = new Location();
        location.LocationID = int.Parse(reader["LocationId"].ToString());

        var address = new Address();
        address.AddressID = int.Parse(reader["AddressId"].ToString());
        address.Address1 = reader["Address1"].ToString();
        address.Address2 = reader["Address2"].ToString();
        address.AddressType = (AddressTypeEnum) int.Parse(reader["AddressTypeId"].ToString());
        address.City = reader["City"].ToString();

        address.Coordination = new Coordination();
        var latitude = reader["Latitude"].ToString();
        address.Coordination.Latitude = (!string.IsNullOrEmpty(latitude)) ? decimal.Parse(latitude) : 0;

                var longitude = reader["Longitude"].ToString();
        address.Coordination.Longitude = (!string.IsNullOrEmpty(longitude)) ? decimal.Parse(longitude) : 0;

                address.State = reader["State"].ToString();
        address.Zipcode = reader["Zipcode"].ToString();

        var phoneNumbersXml = reader["PhoneNumbers"].ToString();
        location.PhoneNumbers = new List<Phone>();
                if (!string.IsNullOrEmpty(phoneNumbersXml))
                {
                    location.PhoneNumbers = Utilities.DeserializeXmlToObject<List<Phone>>(phoneNumbersXml);
                }

    var workingDayHoursXml = reader["WorkingDayHours"].ToString();
    location.WorkingDayHours = new List<WorkingDayHour>();
                if (!string.IsNullOrEmpty(workingDayHoursXml))
                {
                    var workingDayHoursString = Utilities.DeserializeXmlToObject<List<WorkingDayHourString>>(workingDayHoursXml);
                    foreach (var item in workingDayHoursString)
                    {
                        var workingDayHour = new WorkingDayHour();
    workingDayHour.WorkingDayHourID = item.WorkingDayHourID;
                        workingDayHour.DayOfWeek = item.DayOfWeek;
                        workingDayHour.OpenTime = TimeSpan.Parse($"{item.OpenTimeHours}:{item.OpenTimeMinutes}");
                        workingDayHour.CloseTime = TimeSpan.Parse($"{item.CloseTimeHours}:{item.CloseTimeMinutes}");
                        location.WorkingDayHours.Add(workingDayHour);
                    }
                }

                result.OfficeLocations.Add(location);
            }

            reader.NextResult();

            result.Contacts = new List<ContactInfo>();

            while (reader.Read())
            {
                var contactInfo = new ContactInfo()
                {
                    Contact = reader["Contact"].ToString(),
                    ContactInfoID = int.Parse(reader["ContactInfoId"].ToString()),
                    ContactType = (ContactTypeEnum)int.Parse(reader["ContactTypeId"].ToString()),
                    DisplayOrder = int.Parse(reader["DisplayOrder"].ToString())
                };

result.Contacts.Add(contactInfo);
            }

            reader.NextResult();

            result.CuisineCategories = new List<CuisineCategoryEnum>();

            while (reader.Read())
            {
                result.CuisineCategories.Add((CuisineCategoryEnum)int.Parse(reader["CuisineCategoryID"].ToString()));
            }

            reader.NextResult();

            result.MealTypes = new List<MealTypeEnum>();

            while (reader.Read())
            {
                result.MealTypes.Add((MealTypeEnum)int.Parse(reader["MealTypeID"].ToString()));
            }

            reader.NextResult();

            result.FoodTrucks = new List<FoodTruck>();

            while (reader.Read())
            {
                var foodTruck = new FoodTruck();
foodTruck.FoodTruckID = int.Parse(reader["FoodTruckId"].ToString());
foodTruck.Name = reader["FoodTruckName"].ToString();
foodTruck.StartDate = DateTime.Parse(reader["FoodTruckStartDate"].ToString());
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
                    foodTruck.CookInfo.Role = (PersonRoleEnum) int.Parse(reader["FoodTruckCookRoleId"].ToString());
                if (reader["FoodTruckCookSSN"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookSSN"].ToString()))
                    foodTruck.CookInfo.SSN = reader["FoodTruckCookSSN"].ToString();
                if (reader["FoodTruckCookDateOfBirth"] != null && !string.IsNullOrEmpty(reader["FoodTruckCookDateOfBirth"].ToString()))
                    foodTruck.CookInfo.DateOfBirth = DateTime.Parse(reader["FoodTruckCookDateOfBirth"].ToString());
                foodTruck.CuisineCategory = (CuisineCategoryEnum) int.Parse(reader["FoodTruckCuisineCategoryId"].ToString());
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
                    foodTruck.Driver.Role = (PersonRoleEnum) int.Parse(reader["FoodTruckDriverRoleId"].ToString());
                if (reader["FoodTruckDriverSSN"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverSSN"].ToString()))
                    foodTruck.Driver.SSN = reader["FoodTruckDriverSSN"].ToString();
                if (reader["FoodTruckDriverDateOfBirth"] != null && !string.IsNullOrEmpty(reader["FoodTruckDriverDateOfBirth"].ToString()))
                    foodTruck.Driver.DateOfBirth = DateTime.Parse(reader["FoodTruckDriverDateOfBirth"].ToString());
                foodTruck.HealthCode = reader["FoodTruckHealthCode"].ToString();
foodTruck.LicensePlate = reader["FoodTruckLicensePlate"].ToString();
foodTruck.MaxCapacityPerMeal = int.Parse(reader["FoodTruckMaxCapacityPerMeal"].ToString());
foodTruck.MealType = (MealTypeEnum) int.Parse(reader["FoodTruckMealTypeId"].ToString());
result.FoodTrucks.Add(foodTruck);
            }

            reader.NextResult();

        }

        #endregion

    }
}

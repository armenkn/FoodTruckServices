using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;
using System.Data.SqlClient;
using System.Data;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class FoodTruckSqlAccessImplementation : IFoodTruckSqlAccess
    {
        public FoodTruckSqlAccessImplementation()
        {

        }

        public int CreateFoodTruck(FoodTruck foodTruck, int userId)
        {
            throw new NotImplementedException();
        }

        public void DeactivateFoodTruck(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public FoodTruck GetFoodTruckById(int foodTruckId)
        {
            throw new NotImplementedException();
        }

        public FoodTruck SearchFoodTruck(FoodTruckSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public void UpdateFoodTruck(FoodTruck foodTruck, int userId)
        {
            throw new NotImplementedException();
        }

        #region App

        public void ActivateFoodTruck(int foodTruckUserId, decimal latitude, decimal longitude)
        {
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "ActivateFoodTruck";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", foodTruckUserId);
                    cmd.Parameters.AddWithValue("@Latitude", latitude);
                    cmd.Parameters.AddWithValue("@Longitude", longitude);
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}

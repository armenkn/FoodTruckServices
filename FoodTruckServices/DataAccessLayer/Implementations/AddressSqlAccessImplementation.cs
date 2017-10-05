using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;
using System.Data.SqlClient;
using System.Data;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class AddressSqlAccessImplementation : IAddressSqlAccess
    {
        public int CreateAddress(Address address, int userId)
        {
            var result = 0;
            try
            {
                using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
                {
                    var spName = "InsertAddress";
                    using (var cmd = new SqlCommand(spName, sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Address1", address.Address1);
                        cmd.Parameters.AddWithValue("@Address2", address.Address2);
                        cmd.Parameters.AddWithValue("@City", address.City);
                        cmd.Parameters.AddWithValue("@State", address.State);
                        cmd.Parameters.AddWithValue("@Zipcode", address.Zipcode);
                        cmd.Parameters.AddWithValue("@AddressTypeId", (int)address.AddressType);
                        if (address.Coordination != null)
                        {
                            if (address.Coordination.Latitude != 0)
                                cmd.Parameters.AddWithValue("@Latitude", address.Coordination?.Latitude);

                            if (address.Coordination.Longitude != 0)
                                cmd.Parameters.AddWithValue("@Longitude", address.Coordination?.Longitude);
                        }
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(returnValue);

                        sqlConn.Open();
                        var reader = cmd.ExecuteReader();

                        result = int.Parse(returnValue.Value.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
            }
            return result;
       }

        public DatabaseResponse DeleteAddress(int id, int userId)
        {
            var result = DatabaseResponse.None;
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "DeleteAddress";

                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AddressId", id);
                    var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(returnValue);

                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    result =  (DatabaseResponse)int.Parse(returnValue.Value.ToString());
                }

            }
            return result;
        }

        public Address GetAddressById(int addressId)
        {
            var result = new Address();
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "GetAddressById";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        result.AddressID = int.Parse(reader["AddressId"].ToString());
                        result.Address1 = reader["Address1"].ToString();
                        result.Address2 = reader["Address2"].ToString();
                        result.City = reader["City"].ToString();
                        result.State = reader["State"].ToString();
                        result.Zipcode = reader["Zipcode"].ToString();
                        result.AddressType = (AddressTypeEnum)int.Parse(reader["AddressTypeId"].ToString());
                        if (reader["Latitude"] != null && !string.IsNullOrEmpty(reader["Latitude"].ToString()))
                        {
                            result.Coordination = new Coordination();
                            result.Coordination.Latitude = decimal.Parse(reader["Latitude"].ToString());
                            result.Coordination.Longitude = decimal.Parse(reader["Longitude"].ToString());
                        }
                    }
                }
            }
            return result;
        }

        public void UpdateAddress(Address address, int userId)
        {
            try
            {
                using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
                {
                    var spName = "UpdateAddress";
                    using (var cmd = new SqlCommand(spName, sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AddressId", address.AddressID);
                        cmd.Parameters.AddWithValue("@Address1", address.Address1);
                        cmd.Parameters.AddWithValue("@Address2", address.Address2);
                        cmd.Parameters.AddWithValue("@City", address.City);
                        cmd.Parameters.AddWithValue("@State", address.State);
                        cmd.Parameters.AddWithValue("@Zipcode", address.Zipcode);
                        cmd.Parameters.AddWithValue("@AddressTypeId", (int)address.AddressType);
                        if (address.Coordination != null)
                        {
                            if (address.Coordination.Latitude != 0)
                                cmd.Parameters.AddWithValue("@Latitude", address.Coordination?.Latitude);

                            if (address.Coordination.Longitude != 0)
                                cmd.Parameters.AddWithValue("@Longitude", address.Coordination?.Longitude);
                        }
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        
                        sqlConn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}

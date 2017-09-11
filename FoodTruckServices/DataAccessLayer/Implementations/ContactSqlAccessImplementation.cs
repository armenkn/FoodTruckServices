using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;
using System.Data.SqlClient;
using System.Data;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class ContactSqlAccessImplementation : IContactSqlAccess
    {
        public int CreateContact(ContactInfo contact)
        {
            var result = 0;
            try
            {
                using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
                {
                    var spName = "InsertContact";
                    using (var cmd = new SqlCommand(spName, sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Contact", contact.Contact);
                        cmd.Parameters.AddWithValue("@ContactTypeId", (int)contact.ContactType);
                        cmd.Parameters.AddWithValue("@DisplayOrder", contact.DisplayOrder);
                        cmd.Parameters.AddWithValue("@UserId", Constants.UserId);
                        var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(returnValue);

                        sqlConn.Open();
                        var reader = cmd.ExecuteReader();

                        result = int.Parse(returnValue.Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public ContactInfo GetContactById(int contactId)
        {
            var result = new ContactInfo();
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "GetContactById";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContactId", contactId);
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        result.ContactInfoID = int.Parse(reader["ContactInfoId"].ToString());
                        result.Contact= reader["Contact"].ToString();
                        result.DisplayOrder= int.Parse(reader["DisplayOrder"].ToString());
                        result.ContactType = (ContactTypeEnum)int.Parse(reader["ContactTypeId"].ToString());
                    }
                }
            }
            return result;

        }

        public void UpdateContact(ContactInfo contact)
        {
            try
            {
                using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
                {
                    var spName = "UpdateContact";
                    using (var cmd = new SqlCommand(spName, sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ContactId", contact.ContactInfoID);
                        cmd.Parameters.AddWithValue("@Contact", contact.Contact);
                        cmd.Parameters.AddWithValue("@ContactTypeId", (int)contact.ContactType);
                        cmd.Parameters.AddWithValue("@DisplayOrder", contact.DisplayOrder);
                        cmd.Parameters.AddWithValue("@UserId", Constants.UserId);
                        
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

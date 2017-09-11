using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;
using System.Data.SqlClient;
using System.Data;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class UserSqlAccessImplementation : IUserSqlAccess
    {
        public int CreateUser(User user)
        {
            var result = 0;
            try
            {
                using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
                {
                    var spName = "InsertUser";
                    using (var cmd = new SqlCommand(spName, sqlConn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@HashedPassword", user.HashedPassword);
                        cmd.Parameters.AddWithValue("@UserTypeId", (int)user.UserRole);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@SSN", user.SSN);
                        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
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

        public User GetUserById(int userId)
        {
            var result = new User();
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "GetUserById";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    ReadUserFromReader(result, reader);
                }
            }
            return result;
        }

        public List<User> GetUserListByUserRole(UserRoleEnum userRole)
        {
            var result = new List<User>();
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "GetUserById";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserRoleId", (int)userRole);
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.HasRows)
                    {
                        var userItem = new User();
                        ReadUserFromReader(userItem, reader);
                        result.Add(userItem);
                    }
                }
            }
            return result;
        }

        public void UpdateUser(User user)
        {

        }
        public Tuple<UserLoginResultEnum, int> Login(string username, string hashedPassword)
        {
            var loginResult = UserLoginResultEnum.None;
            var userId = 0;

            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "LogInUserByUsernamePassword";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);

                    var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(returnValue);

                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    userId = int.Parse(returnValue.Value.ToString());
                }
            }

            if (userId == 0)
                loginResult = UserLoginResultEnum.InvalidCredentials;

            return new Tuple<UserLoginResultEnum, int>(loginResult, userId);
        }

        public DatabaseResponse InsertTokensForUser(int userId, string accessToken, string refreshToken, DateTime accessTokenExpiratonDate, DateTime refreshTokenExpirationDate)
        {
            var result = DatabaseResponse.None;
            try
            {
                using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
                {
                    var spName = "InsertTokensForUser";
                    using (var cmd = new SqlCommand(spName, sqlConn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@AccessToken", accessToken);
                        cmd.Parameters.AddWithValue("@RefreshToken", refreshToken);
                        cmd.Parameters.AddWithValue("@AccessTokenExpirationDate", accessTokenExpiratonDate);
                        cmd.Parameters.AddWithValue("@RefreshTokenExpirationDate", refreshTokenExpirationDate);

                        var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(returnValue);

                        sqlConn.Open();
                        var reader = cmd.ExecuteReader();

                        result = (DatabaseResponse)int.Parse(returnValue.Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        #region Private methods
        private static void ReadUserFromReader(User result, SqlDataReader reader)
        {
            while (reader.Read())
            {
                result.UserId = int.Parse(reader["UserId"].ToString());
                result.Username = reader["Username"].ToString();
                result.FirstName = reader["Firstname"].ToString();
                result.LastName = reader["Lastname"].ToString();
                result.MiddleName = reader["MiddleName"].ToString();
                result.SSN = reader["SSN"].ToString();
                result.UserRole = (UserRoleEnum)int.Parse(reader["UserRoleId"].ToString());
                result.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
            }
        }

      

        #endregion
    }
}

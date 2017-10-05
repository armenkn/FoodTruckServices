using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Caching.Memory;

namespace FoodTruckServices.DataAccessLayer.Implementations
{
    public class UserDataAccessImplementation : IUserDataAccess
    {
        private IMemoryCache _memoryCache;

        public UserDataAccessImplementation(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public int CreateUser(User user, int userId)
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
                        cmd.Parameters.AddWithValue("@UserRoleId", (int)user.UserRole);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@SSN", user.SSN);
                 
                        if (user.DateOfBirth.HasValue)
                            cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
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

        public void UpdateUser(User user, int userId)
        {

        }

        public Tuple<UserLoginResultEnum, User> Login(string username, string hashedPassword)
        {
            var loginResult = UserLoginResultEnum.None;
            User user = new User();

            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "LogInUserByUsernamePassword";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();

                    ReadUserFromReader(user, reader);
                }
            }

            if (user.UserId == 0)
                loginResult = UserLoginResultEnum.InvalidCredentials;
            else
                loginResult = UserLoginResultEnum.Success;

            return new Tuple<UserLoginResultEnum, User>(loginResult, user);
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

        public string GetTokenProviderSecret(string tokenProvider)
        {
            var secret = "";
            if(_memoryCache.TryGetValue(tokenProvider, out secret))
            {
                return secret;
            }
            using (var sqlConn = new SqlConnection(Utilities.GetDefaultConnectionString()))
            {
                var spName = "GetTokenProviderSecret";
                using (var cmd = new SqlCommand(spName, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TokenProvider", tokenProvider);
                    sqlConn.Open();
                    var reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        secret = reader["Secret"].ToString();
                    }
                }
            }

            _memoryCache.Set(tokenProvider, secret);

            return secret;
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
                if(!string.IsNullOrEmpty(reader["DateOfBirth"].ToString()))
                    result.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
            }
        }
        
        #endregion
    }
}

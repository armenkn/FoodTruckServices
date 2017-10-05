using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IUserDataAccess
    {
        int CreateUser(User user, int userId);

        User GetUserById(int userId);

        void UpdateUser(User user, int userId);

        List<User> GetUserListByUserRole(UserRoleEnum userRole);

        Tuple<UserLoginResultEnum, User> Login(string username, string hashedPassword);

        DatabaseResponse InsertTokensForUser(int userId, string accessToken, string refreshToken, DateTime accessTokenExpirationDate, DateTime refreshTokenExpirationDate);

        string GetTokenProviderSecret(string tokenProvider);
                
    }
}

using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IUserSqlAccess
    {

        int CreateUser(User user);
        User GetUserById(int userId);
        void UpdateUser(User user);

        List<User> GetUserListByUserRole(UserRoleEnum userRole);

        Tuple<UserLoginResultEnum, int> Login(string username, string hashedPassword);

        DatabaseResponse InsertTokensForUser(int userId, string accessToken, string refreshToken, DateTime accessTokenExpirationDate, DateTime refreshTokenExpirationDate);
    }
}
